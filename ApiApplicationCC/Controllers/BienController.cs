using ApiApplicationCC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace ApiApplicationCC.Controllers
{
    public class BienController :BaseController
    {
      

        [HttpGet]

        public async Task<IHttpActionResult> Get()
        {
            var items = await db.Biens.OrderByDescending(x => x.DateCreation).ToArrayAsync();
            foreach (var i in items)
            {
                i.Image = Request.RequestUri.GetLeftPart(UriPartial.Authority) +
                    "/Uploads/" + i.Image;
            }
            return Ok(items);
        }

        [HttpGet]
        public async Task<IHttpActionResult> Details(int id)
        {
            var item = await db.Proprietaires.FindAsync(id);
            item.Picture = Request.RequestUri.GetLeftPart(UriPartial.Authority) +
                    "/Uploads/" + item.Picture;

            return Ok(item);
        }


        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Bien item)
        {
            var olditem = await db.Biens.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
            if (olditem != null)
            {

                item.DateCreation = olditem.DateCreation;
                db.Entry(item).State = EntityState.Modified;
                await db.SaveChangesAsync();
            }

            return Ok(item);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            var json = HttpContext.Current.Request.Form["data"];
            Bien item = JsonConvert.DeserializeObject<Bien>(json);

            string uploadFolder = HttpContext.Current.Server.MapPath("~/Uploads");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            if (HttpContext.Current.Request.Files.Count > 0)
            {
                var file = HttpContext.Current.Request.Files[0];
                string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                file.SaveAs(Path.Combine(uploadFolder, filename));
            }
            item.Id = 0;
            item.DateCreation = DateTime.Now;
            db.Biens.Add(item);
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var item = await db.Biens.FindAsync(id);
            if (item != null)
            {
                db.Biens.Remove(item);
                await db.SaveChangesAsync();
                string uploadFolder = HttpContext.Current.Server.MapPath("~/Uploads/");
                if (!string.IsNullOrEmpty(item.Picture) &&
                    File.Exists(Path.Combine(uploadFolder, item.Picture)))
                {
                    File.Delete(Path.Combine(uploadFolder, item.Picture));
                }
              
            }

            return Ok(item);
        }


        //public IHttpActionResult GetListBien()
        //{
        //    try
        //    {
        //        db.Configuration.ProxyCreationEnabled = false;
        //        var Biens = db.Biens.OrderByDescending(x => x.DateCreation).ToList();
        //        return Ok(Biens);

        //    }


        //    catch (Exception ex)

        //    {
        //        return BadRequest(ex.Message);
        //    }

        //}



        //public IHttpActionResult GetBien(int id)
        //{
        //    try
        //    {

        //        db.Configuration.ProxyCreationEnabled = false;
        //        var Bien= db.Biens.Find(id);

        //        return Ok(Bien);

        //    }
        //    catch (DbUpdateException ex)
        //    {
        //        var exception = ex.InnerException?.InnerException as SqlException;
        //        return BadRequest(exception?.Message);

        //    }

        //    catch (Exception ex)

        //    {
        //        return BadRequest(ex.Message);
        //    }


        //}

    }
}

