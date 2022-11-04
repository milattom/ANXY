using ANXY.ECS;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;
using ANXY.EntityComponent;
using ANXY.EntityComponent.Components;

namespace ANXY.Tests
{
    [TestClass()]
    public class ECDTests
    {
        private static ANXYGame game;

        [TestInitialize]
        public static void InitializeTests()
        {
            game = new ANXYGame();
        }

        [TestMethod()]
        public void TestEntity()
        {
            Vector2 testVector = new Vector2(2, 0);
            Entity testEntity = new Entity();
            testEntity.Position = testVector;
            Console.WriteLine("testEntity ID: " + testEntity.ID);
            Assert.AreEqual( testVector, testEntity.Position);
        }

        [TestMethod()]
        public void TestGetComponent()
        {
            Vector2 testVector = new Vector2(2, 0);
            Entity testEntity = new Entity();
            Component testComponent = new Player(); ;
            testEntity.Position = testVector;
            testEntity.AddComponent(testComponent);

            Thread.Sleep(2000);

            Assert.AreEqual( testComponent, testEntity.GetComponent<Player>());
            Assert.AreEqual( testComponent, testEntity.GetComponent(testComponent.GetType()));

        }
    }
}