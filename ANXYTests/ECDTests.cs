using Microsoft.VisualStudio.TestTools.UnitTesting;
using ANXY;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ANXY.ECS;
using ANXY.ECS.Components;
using Microsoft.Xna.Framework;

namespace ANXY.Tests
{
    [TestClass()]
    public class ECDTests
    {
        [TestMethod()]
        public void CreateEntityWithTransformComponent()
        {
            Vector2 testVector = new Vector2(0, 0);
            Entity testEntity = new Entity();
            testEntity.AddComponent(new Transform(testVector,0,0));
            Assert.IsNotNull(testEntity.GetComponent<Transform>());
        }

        [TestMethod()]
        public void TestTransformTranslation()
        {
            Vector2 vectorBefore = new Vector2(0, 0);
            Vector2 vectorAfter = new Vector2(2, 0);
            Entity testEntity = new Entity();
            testEntity.AddComponent(new Transform(vectorBefore,0,0));

            testEntity.GetComponent<Transform>().Translate(vectorAfter);

            Assert.AreEqual( vectorAfter, testEntity.GetComponent<Transform>().Position);
        }
    }
}