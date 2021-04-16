using ApiApplicationCC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApplicationCC.Controllers
{
    public class BienController : ApiController
    {
        ProprieteEntities db = new ProprieteEntities();

        [HttpGet]
        public IHttpActionResult GetListBien()
        {
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var Biens = db.Biens.OrderByDescending(x => x.DateCreation).ToList();
                return Ok(Biens);

            }


            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]

        public IHttpActionResult GetBien(int id)
        {
            try
            {

                db.Configuration.ProxyCreationEnabled = false;
                var Bien= db.Biens.Find(id);

                return Ok(Bien);

            }
            catch (DbUpdateException ex)
            {
                var exception = ex.InnerException?.InnerException as SqlException;
                return BadRequest(exception?.Message);

            }

            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }


        }

    }
}

