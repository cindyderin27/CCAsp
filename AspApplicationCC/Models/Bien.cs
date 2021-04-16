using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AspApplicationCC.Models
{
    public class Bien
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Nom { get; set; }
        public int IdProprietaire { get; set; }

        public  Proprietaire Proprietaire { get; set; }
    }
}