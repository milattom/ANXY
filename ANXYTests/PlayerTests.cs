using ANXY.ECS.Components;
using ANXY.ECS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANXY.GameObjects;
using Microsoft.Xna.Framework;

namespace ANXYTests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void CreatePlayer()
        {
            Player player = new Player();
            Assert.AreEqual( "Bob", player.Name);
        }
    }
}
