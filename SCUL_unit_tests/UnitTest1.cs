using NUnit.Framework;

namespace SCUL_unit_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Fail();
            Assert.Pass();
        }
    }
}