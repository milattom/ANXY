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
    public class ANXYGameTests
    {
        [TestMethod()]
        public void IsMouseVisible()
        {
            ANXYGame anxyGame = new ANXYGame();
            Assert.IsTrue(anxyGame.IsMouseVisible);
        }
    }
}