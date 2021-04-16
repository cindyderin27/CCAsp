using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationCC.Models
{
    class Proprietaire
    {
        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Nom { get; set; }

        public ICollection<Bien> Biens { get; set; }
    }
}
