using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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

        public  ICollection<Bien> Biens { get; set; }
    }
}