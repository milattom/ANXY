using ANXY.EntityComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using ANXY.EntityComponent.Components;
using ANXY.Start;

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