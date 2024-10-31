using NUnit.Framework;

namespace OrderManager.Tests
{
    internal class ConfigureDistrictsManagerTests
    {
        [Test]
        [TestCase("апрккыы")]
        [TestCase("sortedOrders.txt")]
        public void ConfigureDistrictsManagerNoDistrictsFile(string path)
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => FileManager.ConfigureDistrictsManager(path));
                Assert.That(DistrictsManager<int>.IsDistrictsSpecified, Is.False);
            });
        }

        [Test]
        public void ConfigureDistrictsManagerEmptyDistrictsFile()
        {
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => FileManager.ConfigureDistrictsManager("Tests\\EmptyDistrictsFile.txt"));
                Assert.That(DistrictsManager<int>.IsDistrictsSpecified, Is.False);
            });

        }

        [Test]
        public void ConfigureDistrictsManagerValidDistrictsFile()
        {
            FileManager.ConfigureDistrictsManager("Tests\\ValidDistrictsFile.txt");

            Assert.That(DistrictsManager<int>.GetDistrictList().Count, Is.EqualTo(5));
        }
    }
}
