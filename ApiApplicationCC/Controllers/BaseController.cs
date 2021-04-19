using ApiApplicationCC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ApiApplicationCC.Controllers
{
    public class BaseController : ApiController
    {
        protected readonly ProprieteEntities db;

        public BaseController()
        {
            db =new ProprieteEntities();
        }
    }
}
