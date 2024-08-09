using FluentAssertions;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class HerosTests
    {
        [TestMethod]
        public void GagneUnCombat_FullVie_GagneUnPointSansPerdreDeVie()
        {
            Heros heros = new Heros(15);

            heros.PointDeVies.Should().Be(15);
            heros.Points.Should().Be(0);

            heros.GagneUnCombat();

            Assert.AreEqual(heros.PointDeVies, 15);
            Assert.AreEqual(heros.Points, 1);

        }

        [TestMethod]
        public void PerdUnCombat_Avec9PV_Perd3PV()
        {
            Heros heros = new Heros(9);

            heros.PointDeVies.Should().Be(9);
            heros.Points.Should().Be(0);

            heros.PerdsUnCombat(3);

            Assert.AreEqual(heros.PointDeVies, 6);
            Assert.AreEqual(heros.Points, 0);
        }
    }
}
