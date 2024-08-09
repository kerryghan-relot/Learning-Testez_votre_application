using FluentAssertions;
using FluentAssertions.Extensions;
using Moq;

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
                     "lorsque j'ai un lancer sup�rieur au second et que la m�t�o est soleil, " +
                     "alors le r�sultat est gagn� avec un point sans perdre de points de vie")]
        public void Tour_DeHeroSuperieurDeMonstre_AvecSoleil_AvecMonstreFacileInfini_GagneAvecUnPointSansPerdreDePointDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Soleil);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            var fauxNumberGenerator = Mock.Of<IRandomGenerator>();
            Mock.Get(fauxNumberGenerator).Setup(n => n.Generate(It.IsAny<int>(), It.IsAny<int>())).Returns(999);  // Run the game as long as the Heros lives
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, fauxNumberGenerator);

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
                     "lorsque j'ai un lancer sup�rieur au second et que la m�t�o est tempete, " +
                     "alors le r�sultat est gagn� avec un point sans perdre de points de vie")]
        public void Tour_DeHeroSuperieurDeMonstre_AvecTempete_AvecMonstreFacileInfini_GagneAvecUnPointSansPerdreDePointDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Tempete);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, new RandomGenerator());
            // As we are only emulating a single turn, we donc care about the number of monsters generated.

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
                     "lorsque j'ai un lancer �gal au second et que la m�t�o est soleil, " +
                     "alors le r�sultat est gagn� avec un point sans perdre de points de vie")]
        public void Tour_DeHeroEgalDeMonstre_AvecSoleil_AvecMonstreFacileInfini_GagneAvecUnPointSansPerdreDePointDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Soleil);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, new RandomGenerator());
            // As we are only emulating a single turn, we donc care about the number of monsters generated.

            // Act
            var resultat = jeu.Tour(5, 5);

            // Assert (using built-in assertions)
            Assert.AreEqual(resultat, Resultat.Gagne, "Le Heros devrait gagner!");
            Assert.AreEqual(jeu.Heros.Points, 1);
            Assert.AreEqual(jeu.Heros.PointDeVies, 15);
        }

        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque j'ai un lancer inf�rieur au second et que la m�t�o est soleil, " +
                     "alors le r�sultat est perdu avec des point de points de vie en moins")]
        public void Tour_DeHeroInferieurDeMonstre_AvecSoleil_AvecMonstreFacileInfini_PerdAvecZeroPointEnPerdantDesPointDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Soleil);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, new RandomGenerator());
            // As we are only emulating a single turn, we donc care about the number of monsters generated.

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert (using FluentAssertions)
            resultat.Should().Be(Resultat.Perdu, "Le Heros devrait perdre!");
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(13);
        }

        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque j'ai un lancer inf�rieur au second et que la m�t�o est tempete, " +
                     "alors le r�sultat est perdu avec des point de points de vie en moins")]
        public void Tour_DeHeroInferieurDeMonstre_AvecTempete_AvecMonstreFacileInfini_PerdAvecZeroPointEnPerdantDesPointDeVie()
        {
            // Arrange
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Tempete);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(1);
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, new RandomGenerator());
            // As we are only emulating a single turn, we donc care about the number of monsters generated.

            // Act
            var resultat = jeu.Tour(2, 4);

            // Assert (using FluentAssertions)
            resultat.Should().Be(Resultat.Perdu, "Le Heros devrait perdre!");
            jeu.Heros.Points.Should().Be(0);
            jeu.Heros.PointDeVies.Should().Be(11);
        }

        [TestMethod]
        [Description("Etant donn� un tour de jeu, " +
                     "lorsque la m�t�o est temp�te et que le monstre a 4PV, " +
                     "on fait 5 tour de jeu avec 3 victoires pour 2 d�faites pour le Heros.")]
        public void Tour_DeHeroInferieurDeMonstre_AvecTempete_AvecMonstre4HP_MonstrePerdAuBoutDeCinqLances()
        {
            var fournisseurMeteo = Mock.Of<IFournisseurMeteo>();
            Mock.Get(fournisseurMeteo).Setup(m => m.GetWeather()).Returns(Meteo.Tempete);
            var fauxHealthGen = Mock.Of<IMonsterHealthGenerator>();
            Mock.Get(fauxHealthGen).Setup(h => h.Generate()).Returns(4);
            Jeu jeu = new Jeu(fournisseurMeteo, fauxHealthGen, new RandomGenerator());
            // As we are only emulating a single turn, we donc care about the number of monsters generated.

            jeu.Heros.PointDeVies.Should().Be(15);
            jeu.Heros.Points.Should().Be(0);
            jeu.Monstre.PointDeVie.Should().Be(4);

            var resultat = jeu.Tour(2, 4);

            resultat.Should().Be(Resultat.Perdu, "Le Heros devrait perdre!");
            jeu.Heros.PointDeVies.Should().Be(11);
            jeu.Heros.Points.Should().Be(0);
            jeu.Monstre.PointDeVie.Should().Be(4);

            resultat = jeu.Tour(5, 5);

            resultat.Should().Be(Resultat.Gagne, "Le Heros devrait gagner!");
            jeu.Heros.PointDeVies.Should().Be(11);
            jeu.Heros.Points.Should().Be(1);
            jeu.Monstre.PointDeVie.Should().Be(3);

            resultat = jeu.Tour(6, 5);

            resultat.Should().Be(Resultat.Gagne, "Le Heros devrait gagner!");
            jeu.Heros.PointDeVies.Should().Be(11);
            jeu.Heros.Points.Should().Be(2);
            jeu.Monstre.PointDeVie.Should().Be(1);

            resultat = jeu.Tour(1, 4);

            resultat.Should().Be(Resultat.Perdu, "Le Heros devrait perdre!");
            jeu.Heros.PointDeVies.Should().Be(5);
            jeu.Heros.Points.Should().Be(2);
            jeu.Monstre.PointDeVie.Should().Be(1);

            resultat = jeu.Tour(6, 1);

            resultat.Should().Be(Resultat.Gagne, "Le Heros devrait gagner!");
            jeu.Heros.PointDeVies.Should().Be(5);
            jeu.Heros.Points.Should().Be(3);
            jeu.Monstre.PointDeVie.Should().Be(-5);
        }

        // Pour les �tats du jeu on pourrait d'avantage tester (par exemple lorsque le heros bat un ou plusieurs monstres, apr�s les avoir tous batus, etc...)
        // Mais ces fonctions �tant assez simple et n'ayant pas trop envie de m'embeter avec des tests redondants on va en rester la.
        public void EnCour_QuandLeJeuVientJusteDeD�marrer()
        {
            // Peu importe l'�tat du jeu (m�t�o, nombre et sant� des monstres) le jeu doit toujours �tre dans l'�tat en cours quand il d�marre
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());

            jeu.EnCour().Should().BeTrue();
        }

        public void GameOver_QuandLeJeuVientJusteDeD�marrer()
        {
            // Peu importe l'�tat du jeu (m�t�o, nombre et sant� des monstres) le jeu ne doit jamais �tre dans m'�tat game over quand il d�marre
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());

            jeu.GameOver().Should().BeFalse();
        }

        public void GameWon_QuandLeJeuVientJusteDeD�marrer()
        {
            // Peu importe l'�tat du jeu (m�t�o, nombre et sant� des monstres) le jeu ne doit jamais �tre dans m'�tat HerosWins quand il d�marre
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());

            jeu.GameWon().Should().BeFalse();
        }

        public void JeuHerosPointDeVie_QuandLeJeuVientJusteDeD�marrer()
        {
            // Peu importe l'�tat du jeu (m�t�o, nombre et sant� des monstres) le heros d�marre toujours le jeu full vie
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());

            jeu.Heros.PointDeVies.Should().Be(15);
        }

        public void JeuHerosPoints_QuandLeJeuVientJusteDeD�marrer()
        {
            // Peu importe l'�tat du jeu (m�t�o, nombre et sant� des monstres) le heros d�marre toujours le jeu avec 0 point
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());

            jeu.Heros.Points.Should().Be(0);
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
            Jeu jeu1 = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator()); 
            Jeu jeu2 = jeu1; // Ici le test ne se fait pas sur le comportement de la classe mais simplement sur son type, donc je n'ai pas besoin de cr�er de simulacre.
            Assert.AreSame(jeu1, jeu2); // les r�f�rences de l'objet sont identiques
            jeu2 = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());
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
            Jeu jeu = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator()); // Ici le test ne se fait pas sur le comportement de la classe mais simplement sur son type, donc je n'ai pas besoin de cr�er de simulacre.
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
            _ = autreEntier / entier;
        }

        [TestMethod]
        public void Learning_ManageExceptionWithFluentAssertionUsingAction()
        {
            int entier = 0;
            int autreEntier = 5;

            // We could use actions that should throw excptions
            Action division1 = () => { int resultat = autreEntier / entier; };
            division1.Should().Throw<DivideByZeroException>();

            static int DummyFunctionThatThowAnException()
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
            static int DummyFunctionThatThowAnException()
            {
                int entier = 0;
                int autreEntier = 5;
                return autreEntier / entier;
            }

            // Or also using Invoking()
            this.Invoking(t => DummyFunctionThatThowAnException()).Should().Throw<DivideByZeroException>();

            // But the true power of invoking comes when you are testing objects and their methods
            // Ici peut importe la m�t�o, les d�s n'�tant pas entre 1 et 6, on devrait toujours avoir une exception quelque soit la m�t�o.
            new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator()).Invoking(j => j.Tour(0, 5)).Should().Throw<ArgumentOutOfRangeException>();

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Learning_ManageExceptionWithBuiltInAttributeOnClassesAndMethods()
        {
            // However, we can achieve the same thing using Built-In Attributes
            // And for now, I do think that it is more natural than the equivalent with FluentAssertion above.
            // Same here about the weather.
            Jeu j = new Jeu(new FournisseurMeteo(), new MonsterHealthGenerator(), new RandomGenerator());
            _ = j.Tour(0, 5);
            _ = j.Tour(4, -1);
            _ = j.Tour(7, -3);
        }
    }
}