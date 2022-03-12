using TechTalk.SpecFlow;
using FluentAssertions;
using LocationLibrary;
using LocationTest.Fake;
using System.Collections.Generic;

namespace LocationTest.Steps
{
    [Binding]
    public sealed class LocationStepDefinitions
    {

        private readonly ScenarioContext _scenarioContext;

        private string _firstname;
        private string _lastname;
        private string _password;
        private string _datedenaissance;
        private string _lastErrorMessage;
        private string _validMessage;
        private string _choixKm;
        private string _dateDebut;
        private string _dateFin;
        private decimal _montantFacture;
        private Voiture _choixVoiture;
        private Location _target;
        private FakeDataLayer _fakeDataLayer;

        public LocationStepDefinitions(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
            this._fakeDataLayer = new FakeDataLayer();
            this._target = new Location(this._fakeDataLayer);
        }

        #region Background
        [Given(@"ajouter les clients suivants")]
        public void GivenFollowingExistingClients(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Clients.Add(new Client(row[0], row[1], row[2], row[3], row[4], row[5], false));
            }
        }

        [Given(@"ajouter les voitures suivantes")]
        public void GivenFollowingExistingVoitures(Table table)
        {
            foreach (TableRow row in table.Rows)
            {
                this._fakeDataLayer.Voitures.Add(new Voiture(row[0], row[1], row[2], row[3], row[4], row[5], row[6], true));
            }
        }

        #endregion


        #region Given
        [Given(@"le nom est ""(.*)"" ""(.*)""")]
        public void GivenMyUsernameIs(string firstname, string lastname)
        {
            this._firstname = firstname;
            this._lastname = lastname;
        }

        [Given(@"je suis néé le ""(.*)""")]
        public void GivenJeSuisNeeLe(string datedenaissance)
        {
            this._datedenaissance = datedenaissance;
        }


        [Given(@"le mot de passe est ""(.*)""")]
        public void GivenMyPasswordIs(string password)
        {
            this._password = password;
        }

        [Given(@"la voiture que je veux est une ""(.*)"" ""(.*)"" ""(.*)""")]
        public void GivenLaVoitureQueJeVeuxEstUne(string marque, string modele, string couleur)
        {
            Voiture voiture = this._target.ChoixVoiture(marque, modele, couleur);
            if (voiture != null)
                this._choixVoiture = voiture;
            else
                this._lastErrorMessage = "La voiture choisis d'existe pas";
        }

        [Given(@"je prevois de faire ""(.*)"" Km")]
        public void GivenJePrevoisDeFaireKm(string choixKm)
        {
            this._choixKm = choixKm;

        }

        [Given(@"je veux louer le vehicule du ""(.*)"" au ""(.*)""")]
        public void GivenJeVeuxLouerLeVehiculeDuAu(string dateDebut, string dateFin)
        {
            this._dateDebut = dateDebut;
            this._dateFin = dateFin;
        }

        [Given(@"je rends le vehicule loue ""(.*)"" ""(.*)"" ""(.*)""")]
        public void GivenJeRendsLeVehiculeLoue(string marque, string modele, string couleur)
        {
            Voiture voiture = this._target.ChoixVoiture(marque, modele, couleur);
            if (voiture != null)
                this._choixVoiture = voiture;
            else
                this._lastErrorMessage = "La voiture choisis d'existe pas";
        }

        [Given(@"j'ai reserver le vehicule du ""(.*)"" au ""(.*)""")]
        public void GivenJAiReserverLeVehiculeDuAu(string dateDebut, string dateFin)
        {
            this._dateDebut = dateDebut;
            this._dateFin = dateFin;
        }

        [Given(@"j'ai parcouru ""(.*)"" km")]
        public void GivenJAiParcouruKm(string choixKm)
        {
            this._choixKm = choixKm;
        }

        #endregion


        #region When
        [When(@"essaie de connecter au compte")]
        public void WhenITryToConnectToMyAccount()
        {
            this._lastErrorMessage = this._target.ConnexionClient(this._firstname, this._lastname, this._password);
        }

        [When(@"le client est déja connecter")]
        public void WhenEstDejaConnecter()
        {
            this._target.ClientConnecter().Should().BeTrue();
        }

        [When(@"verification de l'age pour les permission de location")]
        public void WhenVerificationDeLAgePourLesPermissionDeLocation()
        {
            this._lastErrorMessage = this._target.VerificationAgeLocation(this._choixVoiture, this._datedenaissance);
        }

        [When(@"la voiture est loue au client")]
        public void WhenLaVoitureEstLoueAuClient()
        {
            this._validMessage = this._target.MiseEnLocation(this._choixVoiture);
        }

        [When(@"le client recoit sa facture")]
        public void WhenLeClientRecoitSaFacture()
        {
           this._montantFacture =  this._target.Facture(this._choixVoiture,this._choixKm);
        }

        #endregion

        #region Then
        [Then(@"la connexion est refusée")]
        public void ThenTheConnectionIsRefused()
        {
            this._target.ClientConnecter().Should().BeFalse();
        }

        [Then(@"le message d'erreur est ""(.*)""")]
        public void ThenTheErrorMessageIs(string errorMessage)
        {
            this._lastErrorMessage.Should().Be(errorMessage);
        }

        [Then(@"la connexion est établie")]
        public void ThenTheConnectionIsEstablished()
        {
            this._target.ClientConnecter().Should().BeTrue();
        }

        [Then(@"le client obtient la liste des vehicules")]
        public void ThenLeClientObtientLaListeDesVehicules()
        {
           this._target.LesVoitures();
        }

        [Then(@"le message est ""(.*)""")]
        public void ThenLeMessageEst(string validMessage)
        {
            this._validMessage.Should().Be(validMessage);
        }

        [Then(@"le montant de la facture est ""(.*)"" euros")]
        public void ThenLeMontantDeLaFactureEst(string montantFacture)
        {
            decimal montant = decimal.Parse(montantFacture);
            this._montantFacture.Should().Be(montant);
        }

        #endregion
    }
}
