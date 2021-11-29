using NUnit.Framework;
using System;
using System.Text.Json;

namespace SpaceCat.Testing
{
    [TestFixture]
    public class PersistenceTests
    {
        [Test]
        public void TestValidateAndFix()
        {
            Assert.IsTrue(Persistence.ValidateEnvironment());
        }

        [Test]
        public void BuildingSaved()
        {
            Building testBuilding = new("test");
            Assert.IsTrue(Persistence.SaveBuilding(testBuilding));
        }

        [Test]
        public void BuildingSavedAndLoaded()
        {
            Building savedBuilding = new("test");
            Building loadedBuilding;

            Persistence.SaveBuilding(savedBuilding);
            loadedBuilding = Persistence.LoadBuilding("test");

            Assert.AreSame(savedBuilding, loadedBuilding);
        }
    }
}