using ApiApplicationCC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace ApiApplicationCC.Controllers
{
    public class ProprietaireController : BaseController
    {

        [HttpGet]

        public async Task<IHttpActionResult> Get()
        {
            return Ok(await db.Proprietaires.OrderByDescending(x => x.DateCreation).ToArrayAsync());
        }

        [HttpGet]
        public async Task<IHttpActionResult> Details(int id)
        {
            return Ok(await db.Proprietaires.FindAsync(id));
        }


        [HttpPut]
        public async Task<IHttpActionResult> Put([FromBody] Proprietaire item)
        {
            var olditem = await db.Proprietaires.AsNoTracking().FirstOrDefaultAsync(x => x.Id == item.Id);
            if (olditem != null)
            {

                item.DateCreation = olditem.DateCreation;
                db.Entry(item).State=EntityState.Modified;
                await db.SaveChangesAsync();
            }
          
            return Ok(item);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post ([FromBody] Proprietaire item)
        {
            item.DateCreation = DateTime.Now;
            db.Proprietaires.Add(item);
            await db.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var item = await db.Proprietaires.FindAsync(id);
            if(item != null)
            {
               db.Proprietaires.Remove(item);
                await db.SaveChangesAsync();
            }
            return Ok(item);
        }
        //    ProprieteEntities db = new ProprieteEntities();

        //    [HttpGet]
        //    public IHttpActionResult GetListProprietaire()
        //    {
        //        try
        //        {
        //             db.Configuration.ProxyCreationEnabled = false;
        //            var proprietaires = db.Proprietaires.OrderByDescending(x => x.DateCreation).ToList();
        //            return Ok(proprietaires);

        //        }


        //        catch (Exception ex)

        //        {
        //            return BadRequest(ex.Message);
        //        }

        //    }

        //    [HttpGet]

        //    public IHttpActionResult GetProprietaire(int id)
        //    {
        //        try
        //        {

        //            db.Configuration.ProxyCreationEnabled = false;
        //            var proprietaire = db.Proprietaires.Find(id);

        //            return Ok(proprietaire);

        //        }
        //        catch (DbUpdateException ex)
        //        {
        //            var exception = ex.InnerException?.InnerException as SqlException;
        //            return BadRequest(exception?.Message);

        //        }

        //        catch (Exception ex)

        //        {
        //            return BadRequest(ex.Message);
        //        }


        //    }

    }
}
