using Microsoft.VisualStudio.TestTools.UnitTesting;
using ANXY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ANXY.Tests
{
    [TestClass()]
    public class Game1Tests
    {
        [TestMethod()]
        public void IsMouseVisible()
        {
            Game1 game1 = new Game1();
            Assert.IsTrue(game1.IsMouseVisible);
        }
    }
}