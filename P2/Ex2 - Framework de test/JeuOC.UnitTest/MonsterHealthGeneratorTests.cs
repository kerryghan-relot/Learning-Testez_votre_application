using FluentAssertions;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class MonsterHealthGeneratorTests
    {
        [TestMethod]
        public void Generate()
        {
            MonsterHealthGenerator healthGen = new MonsterHealthGenerator();
            
            for (int _ = 0; _ < 1000; _++)
                healthGen.Generate().Should().BeInRange(1, 9);
        }
    }
}
