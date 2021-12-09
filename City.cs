using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSpace
{
    class City
    {
        public int Index { get; set; }
        public string Paese { get; set; }
        public string Location { get; set; }
        public string Longitudine { get; set; }
        public string Latitudine { get; set; }
        public double Longitudine1 { get; set; }
        public double Latitudine1 { get; set; }
        public string Nome { get; set; }

        public City(string Paese,string Location, string Nome, string Longitudine, string Latitudine, int Indice)
        {
            this.Paese = Paese;
            this.Nome = Nome;
            this.Location = Location;
            this.Longitudine = Longitudine;
            this.Latitudine = Latitudine;
            this.Index = Indice;

            if (Longitudine.Contains('E') == true)
            {
                this.Longitudine1 = Convert.ToDouble(Longitudine.Substring(0,3)) + (Convert.ToDouble(Longitudine.Substring(3,2))/60.0);
            }
            if (Longitudine.Contains('W') == true)
            {
                this.Longitudine1 = -(Convert.ToDouble(Longitudine.Substring(0, 3)) + (Convert.ToDouble(Longitudine.Substring(3, 2)) / 60.0));
            }

            if (Latitudine.Contains('N') == true)
            {
                this.Latitudine1 = Convert.ToDouble(Latitudine.Substring(0, 2)) + (Convert.ToDouble(Latitudine.Substring(2, 2)) / 60.0);
            }
            if (Latitudine.Contains('S') == true)
            {
                this.Latitudine1 = -(Convert.ToDouble(Latitudine.Substring(0, 2)) + (Convert.ToDouble(Latitudine.Substring(2, 2)) / 60.0));
            }

        }
    }
}
