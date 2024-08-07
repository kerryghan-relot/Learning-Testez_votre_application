using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JeuOC.UnitTest
{
    public class FauxDe : ILanceurDeDe
    {
        private readonly int[] _listJets = [4, 5, 1, 1, 4, 3, 5, 6, 6, 6, 1, 2, 4, 2, 3, 2, 6, 4, 5, 1, 1, 4, 3, 5, 6, 6, 6, 1, 2, 4, 2, 3, 2, 6];
        private int _compteur = 0;

        public int Lance()
        {
            int tirage = _listJets[_compteur];
            _compteur++;
            return tirage;
        }
    }
}
