namespace JeuOC
{
    public class Ihm(IConsole console, ILanceurDeDe de, IFournisseurMeteo meteo, IMonsterHealthGenerator healthGen, IRandomGenerator randomGenerator)
    {
        private readonly IConsole _console = console;
        private readonly ILanceurDeDe _de = de;
        private readonly IFournisseurMeteo _meteo = meteo;
        private readonly IMonsterHealthGenerator _healthGen = healthGen;
        private readonly IRandomGenerator _randomGenerator = randomGenerator;

        public void Demarre()
        {
            var jeu = new Jeu(_meteo, _healthGen, _randomGenerator);
            _console.WriteLine($"A l'attaque : points/vie {jeu.Heros.Points}/{jeu.Heros.PointDeVies}");
            while (jeu.NumberOfMonstersToDefeat > 0 && jeu.Heros.PointDeVies > 0)
            {
                var resultat = jeu.Tour(_de.Lance(), _de.Lance());
                switch (resultat)
                {
                    case Resultat.Gagne:
                        _console.Write($"Monstre battu");
                        break;
                    case Resultat.Perdu:
                        _console.Write($"Combat perdu");
                        break;
                }
                _console.WriteLine($": points/vie {jeu.Heros.Points}/{jeu.Heros.PointDeVies}");
            }
        }
    }
}
