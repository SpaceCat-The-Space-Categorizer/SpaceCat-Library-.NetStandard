using NUnit.Framework;
using System;
using System.IO;
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

            Console.WriteLine(loadedBuilding.DatabaseHandler.ConstructedFilePath);

            Assert.Pass();
        }

        [Test]
        public void DatabaseInsert()
        {
            Building testBuilding = new("Testicles");
            string filePath = @"URI=file:" + Path.Combine(Persistence.BaseFilePath, "Databases", "Testicles.db");
            DatabaseTests.PopulationTestArea(filePath);

            Assert.Pass();
        }
    }
}