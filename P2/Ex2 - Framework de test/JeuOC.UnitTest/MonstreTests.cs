using FluentAssertions;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class MonstreTests
    {
        [TestMethod]
        public void PerdUnCombat_Avec1PV_Perd3PV()
        {
            Monstre monstre = new Monstre(1);

            monstre.PointDeVie.Should().Be(1);

            monstre.PerdUnCombat(3);  // Note that when the monster loses the dice throw, it loses the difference of the dice + 1 hp.

            monstre.PointDeVie.Should().Be(-3);
        }

        [TestMethod]
        public void PerdUnCombat_Avec6PV_Perd4PVPuisPerdEncore1PV()
        {
            Monstre monstre = new Monstre(6);

            monstre.PointDeVie.Should().Be(6);

            monstre.PerdUnCombat(4);  // Note that when the monster loses the dice throw, it loses the difference of the dice + 1 hp.

            monstre.PointDeVie.Should().Be(1);

            monstre.PerdUnCombat(1);  // Note that when the monster loses the dice throw, it loses the difference of the dice + 1 hp.

            monstre.PointDeVie.Should().Be(-1);
        }
    }
}
