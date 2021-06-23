using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace vanderBinckesBP
{
    class Accessoire
    {
        public int accessoirenummer;
        public string naam;
        public decimal huurprijs;

        public Accessoire(int accessoirenummer)
        {
            this.accessoirenummer = accessoirenummer;
            this.naam = "";
            this.huurprijs = 0;
        }

        public Accessoire(int accessoirenummer, string naam, decimal huurprijs)
        {
            this.accessoirenummer = accessoirenummer;
            this.naam = naam;
            this.huurprijs = huurprijs;
        }

        public Accessoire GetAccessoire(int accessoirenummer) {
            Accessoire a = new Accessoire(accessoirenummer);
            //Todo: implement code to get accessoire information.
            return a;
        }
    }
}
