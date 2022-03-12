using System;

namespace LocationLibrary
{
    public class Voiture
    {
        public string Immatriculation { get; set; }
        public string Marque { get; set; }
        public string Modele { get; set; }
        public string Couleur { get; set; }
        public decimal PrixBase { get; set; }
        public decimal PrixKm { get; set; }
        public int ChevauxFiscaux { get; set; }
        public bool Disponible { get; set; }

        public Voiture(string immatriculation, string marque, string modele, string couleur, string prixbase, string prixkm, string chevauxfiscaux, bool disponible)
        {
            this.Immatriculation = immatriculation;
            this.Marque = marque;
            this.Modele = modele;
            this.Couleur = couleur;
            this.PrixBase = Convert.ToDecimal(prixbase);
            this.PrixKm = Convert.ToDecimal(prixkm);
            this.ChevauxFiscaux = Convert.ToInt32(chevauxfiscaux);
            this.Disponible = disponible;
        }
    }
}