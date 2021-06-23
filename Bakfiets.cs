using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vanderBinckesBP
{
    class Bakfiets
    {
        public int bakfietsnummer;
        public string naam;
        public string type;
        public decimal huurprijs;
        public int aantal;
        public int aantalVerhuurd;

        public Bakfiets(int bakfietsnummer)
        {
            this.bakfietsnummer = bakfietsnummer;
        }

        public Bakfiets(int bakfietsnummer, string naam, string type, decimal huurprijs)
        {
            this.bakfietsnummer = bakfietsnummer;
            this.naam = naam;
            this.type = type;
            this.huurprijs = huurprijs;
        }

        public Bakfiets(int bakfietsnummer, string naam, string type, decimal huurprijs, int aantal, int aantalVerhuurd)
        {
            this.bakfietsnummer = bakfietsnummer;
            this.naam = naam;
            this.type = type;
            this.huurprijs = huurprijs;
            this.aantal = aantal;
            this.aantalVerhuurd = aantalVerhuurd;
        }

        public Bakfiets GetBakfiets(int bakfietsnummer) {
            Bakfiets a = new Bakfiets(bakfietsnummer);
            return a;
        }
    }
}
