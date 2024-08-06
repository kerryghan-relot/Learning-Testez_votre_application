﻿using System;
using System.Threading.Tasks;

namespace Jeu7
{
    public class Ihm
    {
        private readonly IConsole _console;
        private readonly ILanceurDeDe _lanceurDeDe;
        private readonly IFournisseurMeteo _fournisseurMeteo;

        public Ihm(IConsole console, ILanceurDeDe lanceurDeDe, IFournisseurMeteo fournisseurMeteo)
        {
            _console = console;
            _lanceurDeDe = lanceurDeDe;
            _fournisseurMeteo = fournisseurMeteo;
        }

        public async Task Demarre()
        {
            var jeu = new Jeu(_fournisseurMeteo);
            _console.EcrireLigne($"A l'attaque : points/vie {jeu.Heros.Points}/{jeu.Heros.PointDeVies}");
            while (jeu.Heros.PointDeVies > 0)
            {
                var resultat = await jeu.Tour(_lanceurDeDe.Lance(), _lanceurDeDe.Lance());
                switch (resultat)
                {
                    case Resultat.Gagne:
                        _console.Ecrire($"Monstre battu");
                        break;
                    case Resultat.Perdu:
                        _console.Ecrire($"Combat perdu");
                        break;
                    default:
                        throw new NotImplementedException();
                }
                _console.EcrireLigne($": points/vie {jeu.Heros.Points}/{jeu.Heros.PointDeVies}");
            }
        }
    }

    //public class FournisseurMeteo : IFournisseurMeteo
    //{
    //    private static HttpClient _httpClient = new HttpClient();

    //    public Meteo QuelTempsFaitIl(DateTime dateSouhaitee)
    //    {
    //        _httpClient.GetAsync()
    //        throw new NotImplementedException();
    //    }
    //}
}