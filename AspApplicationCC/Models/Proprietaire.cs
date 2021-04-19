using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AspApplicationCC.Models
{
    public class Proprietaire
    {
        public Proprietaire()
        {
            
        }

        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Nom { get; set; }

        public IEnumerable<SelectListItem> Biens { get; set; }
    }
}