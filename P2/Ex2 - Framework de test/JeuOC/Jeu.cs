using System.Numerics;

namespace JeuOC
{
    public class Jeu
    {
        private readonly IFournisseurMeteo _fournisseurMeteo;
        private readonly IMonsterHealthGenerator _healthGen;
        private readonly IRandomGenerator _randomGenerator;
        public Heros Heros { get; }
        public Monstre Monstre { get; private set; }
        public int NumberOfMonstersToDefeat { get; private set; }

        public Jeu(IFournisseurMeteo fournisseurMeteo, IMonsterHealthGenerator healthGen, IRandomGenerator randomGenerator)
        {
            _fournisseurMeteo = fournisseurMeteo;
            _healthGen = healthGen;
            _randomGenerator = randomGenerator;
            Heros = new Heros(15);
            Monstre = new Monstre(_healthGen.Generate());
            NumberOfMonstersToDefeat = _randomGenerator.Generate(1, 20);
        }

        public Resultat Tour(int deHeros, int deMonstre)
        {
            if (deHeros < 1 || deHeros > 6)
                throw new ArgumentOutOfRangeException(nameof(deHeros), $"deHeros doit être compris entre 1 et 6 inclu. deHeros:{deHeros}");
            if (deMonstre < 1 || deMonstre > 6)
                throw new ArgumentOutOfRangeException(nameof(deMonstre), $"deMonstre doit être compris entre 1 et 6 inclu. deMonstre:{deMonstre}");

            if (Monstre.PointDeVie <= 0)
                Monstre = new Monstre(_healthGen.Generate());

            if (GagneLeCombat(deHeros, deMonstre))
            {
                Heros.GagneUnCombat();
                Monstre.PerdUnCombat(deHeros - deMonstre);

                if (Monstre.PointDeVie <= 0)
                    NumberOfMonstersToDefeat--;

                return Resultat.Gagne;
            }
            else
            {
                if (_fournisseurMeteo.GetWeather() == Meteo.Tempete)
                    Heros.PerdsUnCombat(2 * (deMonstre - deHeros));
                else
                    Heros.PerdsUnCombat(deMonstre - deHeros);

                return Resultat.Perdu;
            }
        }

        private static bool GagneLeCombat(int de1, int de2)
        {
            return de1 >= de2;
        }

        public bool EnCour()
        {
            return NumberOfMonstersToDefeat > 0 && Heros.PointDeVies > 0;
        }

        public bool GameOver()
        {
            return Heros.PointDeVies <= 0;
        }

        public bool GameWon()
        {
            return NumberOfMonstersToDefeat <= 0 && Heros.PointDeVies > 0;
        }
    }
}
