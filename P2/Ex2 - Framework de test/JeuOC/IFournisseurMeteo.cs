using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuOC
{
    public interface IFournisseurMeteo
    {
        Meteo GetWeather();
    }
}
