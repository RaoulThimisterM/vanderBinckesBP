using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vanderBinckesBP
{
    class Verhuur
    {
        public int verhuurnummer;
        public DateTime verhuurdatum;
        public int bakfietsnummer;
        public int verhuurdagen;
        public decimal huurprijs;
        public int klantnummer;
        public int medewerker;

        public Verhuur(int verhuurnummer)
        {
            this.verhuurnummer = verhuurnummer;
        }

        public Verhuur(DateTime verhuurdatum, int bakfietsnummer, int verhuurdagen, decimal huurprijs, int klantnummer, int medewerker)
        {
            this.verhuurdatum = verhuurdatum;
            this.bakfietsnummer = bakfietsnummer;
            this.verhuurdagen = verhuurdagen;
            this.huurprijs = huurprijs;
            this.klantnummer = klantnummer;
            this.medewerker = medewerker;
        }

        public Verhuur(int verhuurnummer, DateTime verhuurdatum, int bakfietsnummer, int verhuurdagen, decimal huurprijs, int klantnummer, int medewerker)
        {
            this.verhuurnummer = verhuurnummer;
            this.verhuurdatum = verhuurdatum;
            this.bakfietsnummer = bakfietsnummer;
            this.verhuurdagen = verhuurdagen;
            this.huurprijs = huurprijs;
            this.klantnummer = klantnummer;
            this.medewerker = medewerker;
        }

        public Verhuur GetVerhuur(int verhuurnummer) {
            Verhuur a = new Verhuur(verhuurnummer);
            //Todo: Implement function to get rental information.
            return a;
        }
    }
}
