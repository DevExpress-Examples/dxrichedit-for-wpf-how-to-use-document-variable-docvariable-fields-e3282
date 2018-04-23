using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DocumentVariablesExample {
    class SampleData : ArrayList {
        public SampleData() {
            Add(new AddresseeRecord("Maria", "Alfreds Futterkiste", "Obere Str. 57, Berlin", "Berlin"));
            Add(new AddresseeRecord("Laurence", "Bon app'", "12, rue des Bouchers, Marseille", "Marseille"));
            Add(new AddresseeRecord("Patricio", "Cactus Comidas para llevar", "Cerrito 333, Buenos Aires", "Buenos Aires"));
            Add(new AddresseeRecord("Thomas", "Around the Horn", "120 Hanover Sq., London", "London"));
            Add(new AddresseeRecord("Boris", "Express Developers", "Krasnoarmeiskiy prospect 25, Tula", "Tula"));
            Add(new AddresseeRecord("Christina", "Berglunds snabbkop", "Berguvsvagen  8, Lulea", "Lulea"));
            Add(new AddresseeRecord("Hanna", "Blauer See Delikatessen", "Forsterstr. 57, Mannheim", "Mannheim"));
            Add(new AddresseeRecord("Frederique", "Blondel pere et fils", "24, place Kleber, Strasbourg", "Strasbourg"));
            Add(new AddresseeRecord("Antonio", "Antonio Moreno Taqueria", "Mataderos  2312, Mexico D.F.", "Mexico"));
            Add(new AddresseeRecord("Martin", "Bolido Comidas preparadas", "C/ Araquil, 67, Madrid", "Madrid"));
            Add(new AddresseeRecord("Elizabeth", "Bottom-Dollar Markets", "23 Tsawassen Blvd., Tsawwassen", "Tsawwassen"));
            Add(new AddresseeRecord("Victoria", "B's Beverages", "Fauntleroy Circus, London", "London"));
            Add(new AddresseeRecord("Yang", "Chop-suey Chinese", "Hauptstr. 29, Bern", "Bern"));
            Add(new AddresseeRecord("Pedro", "Comercio Mineiro", "Av. dos Lusiadas, 23, Sao Paulo", "Sao Paulo"));
            Add(new AddresseeRecord("Elizabeth","Consolidated Holdings","Berkeley Gardens12  Brewery , London", "London"));
            Add(new AddresseeRecord("Sven","Drachenblut Delikatessen","Walserweg 21, Aachen", "Aachen"));
            Add(new AddresseeRecord("Janine", "Du monde entier", "67, rue des Cinquante Otages, Nantes", "Nantes"));
            Add(new AddresseeRecord("Zbyszek", "Wolski  Zajazd", "ul. Filtrowa 68, Warszawa", "Warszawa"));
        }
    }

    public class AddresseeRecord {
        private string _Name;
        private string _Company;
        private string _Address;
        private string _City;

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }
        public string Company {
            get { return _Company; }
            set { _Company = value; }
        }
        public string Address {
            get { return _Address; }
            set { _Address = value; }
        }
        public string City
        {
            get { return _City; }
            set { _City = value; }
        }

        public AddresseeRecord(string _Name, string _Company, string _Address, string _City) {
            this._Name = _Name;            
            this._Company = _Company;
            this._Address = _Address;
            this._City = _City;
        }
    }
}
