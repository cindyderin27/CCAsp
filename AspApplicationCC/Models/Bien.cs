using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspApplicationCC.Models
{
    public class Bien
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Nom { get; set; }
        public int IdProprietaire { get; set; }
        public IEnumerable<SelectListItem> Proprietaires { get; set; }
        public  Proprietaire Proprietaire { get; set; }
        public string Picture { get; set; }

        [JsonIgnore]
        public HttpPostedFileBase Image { get; set; }
       
        //public double Prix { get; set; }
        //public string Description { get; set; }
    }
}