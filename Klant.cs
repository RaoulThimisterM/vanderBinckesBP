using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vanderBinckesBP
{
    class Klant
    {
        public int klantnummer;
        public string voornaam;
        public string achternaam;
        public string postcode;
        public int huisnummer;
        public string huisnummerToev;
        public string opmerkingen;

        public Klant(int klantnummer, string voornaam, string achternaam)
        {
            this.klantnummer = klantnummer;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
        }

        public Klant(int klantnummer, string voornaam, string achternaam, string postcode, int huisnummer, string huisnummerToev, string opmerkingen)
        {
            this.klantnummer = klantnummer;
            this.voornaam = voornaam;
            this.achternaam = achternaam;
            this.postcode = postcode;
            this.huisnummer = huisnummer;
            this.huisnummerToev = huisnummerToev;
            this.opmerkingen = opmerkingen;
        }

        //public Klant GetKlant(int klantnummer)
        //{
        //    Klant a = new Klant();
        //    //Todo: implement function to get customer information;
        //    return a;
        //}
    }
}
