using JeuOC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.Extensions;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography.X509Certificates;

namespace JeuOC.UnitTest
{
    /// <summary>
    /// Class de test pour la classe Jeu
    /// </summary>
    /// <remarks>
    /// Les trois première methodes font basiquement la même chose à quelques valeurs près.
    /// Ainsi pour illustrer la différences entre une implémentation naïve, une avec Assert utilisé correctement et une avec le package NuGet FluentAssertion,
    /// j'ai utilisé chacun d'eux pour ces trois fonctions de test.
    /// </remarks>
    [TestClass]
    public class JeuTests
    {
        [TestMethod]
        [Description("Etant donné un tour de jeu, " +
                     "lorsque j'ai un lancer supérieur au second, " +
                     "alors le résultat est gagné avec un point sans perdre de points de vie")]
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
        [Description("Etant donné un tour de jeu, " +
                     "lorsque j'ai un lancer égal au second, " +
                     "alors le résultat est gagné avec un point sans perdre de points de vie")]
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
        [Description("Etant donné un tour de jeu, " +
                     "lorsque j'ai un lancer inférieur au second, " +
                     "alors le résultat est perdu avec des point de points de vie en moins")]
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
            Assert.AreEqual(1, 1); // égalité entre entier
            Assert.AreEqual(3.14, 6.28 / 2); // égalité entre double
            Assert.AreEqual("une chaine", "une " + "chaine"); // égalité entre chaînes
            Assert.AreNotEqual(1, 2); // inégalité
            Assert.IsFalse(1 == 2); // booléen vaut faux
            Assert.IsTrue(1 <= 2); // booléen vaut vrai
            Jeu jeu1 = new();
            Jeu jeu2 = jeu1;
            Assert.AreSame(jeu1, jeu2); // les références de l'objet sont identiques
            jeu2 = new();
            Assert.AreNotSame(jeu1, jeu2); // les références ne sont pas identiques
            Assert.IsInstanceOfType(jeu1, typeof(Jeu)); // comparaison de type
            Assert.IsNotInstanceOfType(jeu1, typeof(De)); // différence de type
            Assert.IsNotNull(jeu1); // différence à null
            int? uneValeurNulle = null;
            Assert.IsNull(uneValeurNulle); // comparaison à null
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

        [TestMethod]
        public void Learning_ManageExceptionNaiveWay()
        {
            int entier = 0;
            int autreEntier = 5;

            try
            {
                int division = autreEntier / entier;
                Assert.Fail("The previous operation should throw an exception");  // This is to ensure that in the event where the previous assignement would
                                                                                  // not throw an exception, we on purpose fail the current test because it should.
            }
            catch (DivideByZeroException ex)
            {
                ex.Message.Should().NotBeNullOrEmpty();
            }
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "The previous operation should throw an exception")]
        public void Learning_ManageExceptionWithBuiltInAttributes()
        {
            int entier = 0;
            int autreEntier = 5;
            int division = autreEntier / entier;
        }

        [TestMethod]
        public void Learning_ManageExceptionWithFluentAssertionUsingAction()
        {
            int entier = 0;
            int autreEntier = 5;

            // We could use actions that should throw excptions
            Action division1 = () => { int resultat = autreEntier / entier; };
            division1.Should().Throw<DivideByZeroException>();

            int DummyFunctionThatThowAnException()
            {
                int entier = 0;
                int autreEntier = 5;
                return autreEntier / entier;
            }

            // The same thing can be done with a function call in the Action.
            Action division2 = () => DummyFunctionThatThowAnException();
            division2.Should().Throw<DivideByZeroException>();
        }

        [TestMethod]
        public void Learning_ManageExceptionWithFluentAssertionUsingInvoking()
        {
            int DummyFunctionThatThowAnException()
            {
                int entier = 0;
                int autreEntier = 5;
                return autreEntier / entier;
            }

            // Or also using Invoking()
            this.Invoking(t => DummyFunctionThatThowAnException()).Should().Throw<DivideByZeroException>();

            // But the true power of invoking comes when you are testing objects and their methods
            new Jeu().Invoking(j => j.Tour(0, 5)).Should().Throw<ArgumentOutOfRangeException>();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Learning_ManageExceptionWithBuiltInAttributeOnClassesAndMethods()
        {
            // However, we can achieve the same thing using Built-In Attributes
            // And for now, I do think that it is more natural than the equivalent with FluentAssertion above.
            Jeu j = new();
            Resultat resultat1 = j.Tour(0, 5);
            Resultat resultat2 = j.Tour(4, -1);
            Resultat resultat3 = j.Tour(7, -3);
        }
    }
}