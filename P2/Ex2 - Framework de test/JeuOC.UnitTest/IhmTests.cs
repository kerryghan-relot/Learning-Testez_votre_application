﻿using FluentAssertions;
using Moq;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class IhmTests
    {
        [TestMethod]
        public void Ihm_AvecUnJeuDeDePredefini_EtMeteoSoleil_EtMonstreFacileInfinis_LeJoueurGagne()
        {
            // arrange
            var fausseConsole = new FausseConsole();

            var fauxDe = Mock.Of<ILanceurDeDe>();
            var sequence = Mock.Get(fauxDe).SetupSequence(d => d.Lance());
            List<int> jetDeDe = [4, 5, 1, 1, 4, 3, 5, 6, 6, 6, 1, 2, 4, 2, 3, 2, 6, 4, 5, 1, 1, 4, 3, 5, 6, 6, 6, 1, 2, 4, 2, 3, 2, 6];
            foreach (var lancer in jetDeDe)
            {
                sequence.Returns(lancer);
            }


            var fausseMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fausseMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Soleil);

            // Generate a mock for the Health Generator and Number Generator.
            // That way, the original test will remained unchanged
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            var fauxNumberGenerator = Mock.Of<IRandomGenerator>();
            Mock.Get(fauxNumberGenerator).Setup(n => n.Generate(1, 20)).Returns(999);
            Ihm ihm = new(fausseConsole, fauxDe, fausseMeteo, fauxHealthGen, fauxNumberGenerator);

            // act
            ihm.Demarre();

            // assert
            var resultat = fausseConsole.StringBuilder.ToString();
            resultat.Should().StartWith("A l'attaque : points/vie 0/15");
            resultat.Should().EndWith("Combat perdu: points/vie 9/0\r\n");
            resultat.Should().HaveLength(560);
        }
        [TestMethod]
        public void Ihm_AvecUnJeuDeDePredefini_EtMeteoTempete_EtMonstreFacileInfini_LeJoueurGagne()
        {
            /// Ici la fonction aura les même lancer de dés.
            /// Les seuls différences qu'il y a sont :
            /// 1°) La météo
            /// 2°) Mais surtout, la manière dont est déclarée le faux dé.
            ///     Ici, c'est avec un bouchon, et au dessus c'est avec un simulacre.

            // arrange
            var fausseConsole = new FausseConsole();
            var fauxDe = new FauxDe();
            var fausseMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fausseMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Tempete);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            var fauxNumberGenerator = Mock.Of<IRandomGenerator>();
            Mock.Get(fauxNumberGenerator).Setup(n => n.Generate(1, 20)).Returns(999);
            Ihm ihm = new(fausseConsole, fauxDe, fausseMeteo, fauxHealthGen, fauxNumberGenerator);

            // act
            ihm.Demarre();

            // assert
            var resultat = fausseConsole.StringBuilder.ToString();
            resultat.Should().StartWith("A l'attaque : points/vie 0/15");
            resultat.Should().EndWith("Combat perdu: points/vie 7/-1\r\n");
            resultat.Should().HaveLength(404);  // Evidement le résultat change un peu puisque là, la météo joue en notre défaveur.
        }
    }
}
