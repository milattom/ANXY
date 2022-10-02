using Microsoft.VisualStudio.TestPlatform.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace ANXY.Tests
{
    [TestClass]
    public class Game1Tests
    {
        [TestMethod]
        public void Game1Test()
        {
            Console.WriteLine("first unit test");
            Game1 game1 = new Game1();
            Assert.IsNotNull(game1);
        }
    }
}