using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace vanderBinckesBP
{
    class Medewerker
    {
        public int medewerkernummer;
        public string voornaam { get; }
        public string achternaam { get; }
        public DateTime datumInDienst;

        public Medewerker(string voornaam, string achternaam)
        {
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.medewerkernummer = 999;
            this.datumInDienst = DateTime.Today;
        }
        public Medewerker(int medewerkernummer, string voornaam, string achternaam)
        {
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.medewerkernummer = medewerkernummer;
            this.datumInDienst = DateTime.MinValue;
        }

        public Medewerker(int medewerkernummer, string voornaam, string achternaam, DateTime datumInDienst)
        {
            this.medewerkernummer = medewerkernummer;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.datumInDienst = datumInDienst;
        }

        public Medewerker GetMedewerker(int medewerkernummer)
        {
            Console.WriteLine(voornaam + " " + achternaam);
            return new Medewerker(voornaam, achternaam);
        }
    }
}
