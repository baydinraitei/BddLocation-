using LocationLibrary;
using System;
using System.Collections.Generic;
using System.Text;

namespace LocationTest.Fake
{
    class FakeDataLayer : IDataLayer
    {
        public List<Client> Clients { get; set; }
        public List<Voiture> Voitures { get; set; }

        public FakeDataLayer()
        {
            this.Clients = new List<Client>();
            this.Voitures = new List<Voiture>();
        }
    }
}
