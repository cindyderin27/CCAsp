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
    public class ProprietaireController : ApiController
    {
        ProprieteEntities db = new ProprieteEntities();

        [HttpGet]
        public IHttpActionResult GetListProprietaire()
        {
            try
            {
                 db.Configuration.ProxyCreationEnabled = false;
                var proprietaires = db.Proprietaires.OrderByDescending(x => x.DateCreation).ToList();
                return Ok(proprietaires);

            }


            catch (Exception ex)

            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]

        public IHttpActionResult GetProprietaire(int id)
        {
            try
            {

                db.Configuration.ProxyCreationEnabled = false;
                var proprietaire = db.Proprietaires.Find(id);

                return Ok(proprietaire);

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
