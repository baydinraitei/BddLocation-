using System;
using System.Collections.Generic;
using System.Linq;

namespace LocationLibrary
{
    public class Location
    {
        private IDataLayer _dataLayer;

        public bool clientConnecter { get; private set; }

        public Location()
        {
            this._dataLayer = new DataLayer();
        }

        public Location(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        public string ConnexionClient(string prenom, string nom, string motdepasse)
        {
            Client client = this._dataLayer.Clients.SingleOrDefault(_ => _.Prenom == prenom && _.Nom == nom);
            if (client == null)
            {
                this.clientConnecter = false;
                return "Nom d'utilisateur inconnu";
            }
            else
            {
                if (client.MotDePasse == motdepasse)
                {
                    this.clientConnecter = true;
                    return "Connexion réussis !";
                }
                                 
                else
                {
                    this.clientConnecter = false;
                    return "Mot de passe incorrect";
                }
            }
        }
        public bool ClientConnecter()
        {
            return this.clientConnecter;
        }

        public List<Voiture> LesVoitures()
        {
            return this._dataLayer.Voitures;
        }
        public Voiture ChoixVoiture(string marque, string modele, string couleur)
        {
            Voiture voiture = this._dataLayer.Voitures.SingleOrDefault(x => x.Marque == marque && x.Modele == modele && x.Couleur == couleur);
            if (voiture == null)
            {
                this.clientConnecter = false;
                return null;
            }
            else
                return voiture;
        }

        public string VerificationAgeLocation(Voiture voiture, string datedenaissance)
        {
            //23 ans & 12CHF

            int maintenant = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            DateTime dateDeNaissance = DateTime.Parse(datedenaissance);
            int naissance = int.Parse(dateDeNaissance.ToString("yyyyMMdd"));
            int age = (maintenant - naissance) / 10000;

            if(age >= 18)
            {
                if (voiture.ChevauxFiscaux < 8)
                {
                    if (age < 21)
                        return "Le client na pas le bon age pour louer ce vehicule";
                    else
                        return "Le client peux louer ce vehicule";
                }
                else if (voiture.ChevauxFiscaux >= 8 && voiture.ChevauxFiscaux <= 13)
                {
                    if (age >= 21)
                        return "Le client peux louer ce vehicule";                 
                    else
                        return "Le client na pas le bon age pour louer ce vehicule";
                }
                return "Le vehicule est trop puissant, il ne peut pas louer ce vehicule";
            }
            else
                return "Le client est mineur, il ne peut pas louer ce vehicule";
        }

        public string MiseEnLocation(Voiture voiture)
        {
            if (voiture != null)
            {
                voiture.Disponible = true;
                return "La voiture est louer";
            }
            else
                return "La voiture n'existe pas";
        }

        public decimal Facture(Voiture voiture, string choixKm)
        {

            decimal montantTotale;
            if (voiture != null)
            {
                montantTotale = voiture.PrixBase + (voiture.PrixKm * Convert.ToDecimal(choixKm)); 
                return montantTotale;
            }
            else
                return 0;
        }
    }
}
