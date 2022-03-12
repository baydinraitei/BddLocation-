using System;

namespace LocationLibrary
{
    public class Client
    {
        public string Prenom { get; private set; }
        public string Nom { get; private set; }
        public string MotDePasse { get; private set; }
        public string DateDeNaissance { get; private set; }       
        public string NumeroPermis { get; private set; }
        public string DateDePermis { get; private set; }
        public bool Reservation { get; private set; }

        public Client(string prenom, string nom, string motdepasse, string datedenaissance, string numeropermis, string datedepermis, bool reservation)
        {
            this.Prenom = prenom;
            this.Nom = nom;
            this.MotDePasse = motdepasse;
            this.DateDeNaissance = datedenaissance;
            this.NumeroPermis = numeropermis;
            this.DateDePermis = datedepermis;
            this.Reservation = reservation;
        }
    }
}