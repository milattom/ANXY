using ANXY.Start;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ANXY.Tests
{
    [TestClass()]
    public class ANXYGameTests
    {

        [TestMethod()]
        public void IsMouseVisible()
        {
            ANXYGame game = new ANXYGame();

            Assert.IsTrue(game.IsMouseVisible);
        }
    }
}