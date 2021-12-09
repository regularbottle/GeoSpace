using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoSpace
{
    class MoreCities : List<City>
    {
        public MoreCities()
        {
            string percorso = "CoordinateCittà.csv";
            StreamReader sr = new StreamReader(percorso);
            string linea = "";
            do
            {
                linea = sr.ReadLine();
                if (linea != null)
                {
                    string[] campi = linea.Split(';');
                    string[] ciao = campi[3].Split(' ');
                    if (ciao.Count() > 1)
                    {
                        City City = new City(campi[0],campi[1] , campi[2], ciao[1], ciao[0], Convert.ToInt32(campi[4]));
                        Add(City);
                    }
                }
            }
            while (linea != null);
        }

        public IEnumerable Contiene(string filtro)
        {
            return from City C in this
                   where C.Nome.Contains(filtro)
                   orderby C.Paese
                   select C;
        }
    }
}
