using System;

namespace JeuOC
{
    public class Ihm(IConsole console, ILanceurDeDe de)
    {
        private readonly IConsole _console = console;
        private readonly ILanceurDeDe _de = de;

        public void Demarre()
        {
            var jeu = new Jeu(new FournisseurMeteo());
            _console.WriteLine($"A l'attaque : points/vie {jeu.Heros.Points}/{jeu.Heros.PointDeVies}");
            while (jeu.Heros.PointDeVies > 0)
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
