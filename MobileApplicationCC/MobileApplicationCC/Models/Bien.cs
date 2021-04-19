using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApplicationCC.Models
{
    class Bien
    {
        private string v;

        public int Id { get; set; }
        public DateTime DateCreation { get; set; }
        public string Nom { get; set; }
        public int IdProprietaire { get; set; }
        [Newtonsoft.Json.JsonIgnore]
        public HttpStyleUriParser Image { get; set; }
        public Proprietaire Proprietaire { get; set; }

        public Bien(int id, DateTime dateCreation, string nom, int idProprietaire, HttpStyleUriParser image, Proprietaire proprietaire)
        {
            Id = id;
            DateCreation = dateCreation;
            Nom = nom;
            IdProprietaire = idProprietaire;
            Image = image;
            Proprietaire = proprietaire;
        }

        public Bien()
        {
        }

        public Bien(string v)
        {
            this.v = v;
        }
    }
}
