using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuOC
{
    public class FournisseurMeteo : IFournisseurMeteo
    {
        private readonly Random _random = new Random();

        public Meteo GetWeather()
        {
            var tirage = _random.Next(21);
            if (tirage < 10)
            {
                return Meteo.Soleil;
            }
            if (tirage < 20)
            {
                return Meteo.Pluie;
            }
            return Meteo.Tempete;
        }
    }
}
