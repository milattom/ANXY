using ANXY.EntityComponent;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using ANXY.EntityComponent.Components;

namespace ANXY.Tests
{
    [TestClass()]
    public class ANXYGameTests
    {
        private static ANXYGame game;

        [TestInitialize]
        public static void InitializeTests()
        {
            game = new ANXYGame();
        }
        
        [TestMethod()]
        public void IsMouseVisible()
        {
            Assert.IsTrue(game.IsMouseVisible);
        }
    }
}