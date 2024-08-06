using JeuOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.Extensions;

namespace JeuOC.UnitTest
{
    /// <summary>
    /// Class de test pour la classe Jeu
    /// </summary>
    /// <remarks>
    /// Les trois premi�re methodes font basiquement la m�me chose � quelques valeurs pr�s.
    /// Ainsi pour illustrer la diff�rences entre une impl�mentation na�ve, une avec Assert utilis� correctement et une avec le package NuGet FluentAssertion,
    /// j'ai utilis� chacun d'eux pour ces trois fonctions de test.
    /// </remarks>
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

            // Assert (using naive assertions)
            if (resultat != Resultat.Gagne)
                Assert.Fail("Le Heros devrait gagner!");
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

            // Assert (using built-in assertions)
            Assert.AreEqual(resultat, Resultat.Gagne, "Le Heros devrait gagner!");
            Assert.AreEqual(jeu.Heros.Points, 1);
            Assert.AreEqual(jeu.Heros.PointDeVies, 15);
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

            // Assert (using FluentAssertions)
            resultat.Should().Be(Resultat.Perdu, "Le Heros devrait perdre!");
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(13);
        }

        [TestMethod]
        public void Learning_BuiltInAssertions()
        {
            Assert.AreEqual(1, 1); // �galit� entre entier
            Assert.AreEqual(3.14, 6.28 / 2); // �galit� entre double
            Assert.AreEqual("une chaine", "une " + "chaine"); // �galit� entre cha�nes
            Assert.AreNotEqual(1, 2); // in�galit�
            Assert.IsFalse(1 == 2); // bool�en vaut faux
            Assert.IsTrue(1 <= 2); // bool�en vaut vrai
            Jeu jeu1 = new();
            Jeu jeu2 = jeu1;
            Assert.AreSame(jeu1, jeu2); // les r�f�rences de l'objet sont identiques
            jeu2 = new();
            Assert.AreNotSame(jeu1, jeu2); // les r�f�rences ne sont pas identiques
            Assert.IsInstanceOfType(jeu1, typeof(Jeu)); // comparaison de type
            Assert.IsNotInstanceOfType(jeu1, typeof(De)); // diff�rence de type
            Assert.IsNotNull(jeu1); // diff�rence � null
            int? uneValeurNulle = null;
            Assert.IsNull(uneValeurNulle); // comparaison � null
        }

        [TestMethod]
        public void Learning_FluentAssertions()
        {
            var valeur = -1;
            valeur.Should().BeNegative();
            1.Should().BeGreaterThan(valeur);
            valeur.Should().BeLessThanOrEqualTo(1);
            Math.PI.Should().BeApproximately(3.14, 0.1);
            valeur.Should().BeInRange(-5, 5);
            "chaine".Should().Contain("i").And.Contain("e").And.NotStartWith("p");
            Jeu jeu = new();
            jeu.Should().BeOfType<Jeu>().Which.Heros.PointDeVies.Should().Be(15);
            jeu.Should().NotBeOfType<int>().And.NotBeNull();
            2.Should().BeOneOf([1, 2, 3]);
            string email = "nico@openclassrooms.com";
            email.Should().Match("*@*.com");
            DateTime.Now.Should().BeAfter(new DateTime(2000, 1, 1));
            var dateDeLivraison = DateTime.Now.AddDays(3);
            DateTime.Now.Should().BeAtLeast(2.Days()).Before(dateDeLivraison);
            // And many more...
            // See documention for more information:
            // https://fluentassertions.com/introduction
        }
    }
}