using JeuOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class JeuTests
    {
        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque j'ai un lancer sup�rieur au second, " +
                     "alors le r�sultat est gagn� avec un point sans perdre de points de vie")]
        public void Tour_DeHeroSuperieurDeMonstre_GagneAvecUnPointSansPerdreDePointDeVie()
        {
            // Arrange
            Jeu jeu = new();

            // Act
            var resultat = jeu.Tour(6, 1);

            // Assert
            if (resultat != Resultat.Gagne)
                Assert.Fail();
            if (jeu.Heros.Points != 1)
                Assert.Fail();
            if (jeu.Heros.PointDeVies != 15)
                Assert.Fail();
        }

        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque j'ai un lancer �gal au second, " +
                     "alors le r�sultat est gagn� avec un point sans perdre de points de vie")]
        public void Tour_DeHeroEgalDeMonstre_GagneAvecUnPointSansPerdreDePointDeVie()
        {
            // Arrange
            Jeu jeu = new();

            // Act
            var resultat = jeu.Tour(5, 5);

            // Assert
            if (resultat != Resultat.Gagne)
                Assert.Fail();
            if (jeu.Heros.Points != 1)
                Assert.Fail();
            if (jeu.Heros.PointDeVies != 15)
                Assert.Fail();
        }

        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque j'ai un lancer inf�rieur au second, " +
                     "alors le r�sultat est perdu avec des point de points de vie en moins")]
        public void Tour_DeHeroInferieurDeMonstre_PerdAvecZeroPointEnPerdantDesPointDeVie()
        {
            // Arrange
            Jeu jeu = new();

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert
            if (resultat != Resultat.Perdu)
                Assert.Fail();
            if (jeu.Heros.Points != 0)
                Assert.Fail();
            if (jeu.Heros.PointDeVies != 13)
                Assert.Fail();
        }
    }
}