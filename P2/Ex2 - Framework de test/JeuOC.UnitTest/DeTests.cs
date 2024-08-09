using FluentAssertions;
using Moq;

namespace JeuOC.UnitTest
{
    [TestClass]
    public class DeTests
    {
        [TestMethod]
        public void Lance()
        {
            De de = new De();
            for (int _ = 0; _ < 1000; _++)
                de.Lance().Should().BeInRange(1, 6);
        }
    }
}
