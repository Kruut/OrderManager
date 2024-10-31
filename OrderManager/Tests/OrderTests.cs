using NUnit.Framework;

namespace OrderManager.Tests
{
    [TestFixture]
    internal class OrderTests
    {
        [Test]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", null)]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", null, "2560494", "2024-10-29 17:54:23")]
        [TestCase(null, "8,4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "-8,4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", "999999999999999999-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "256049hgf4", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", "2024-10-29 25:67:83")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", "2024-10-29 17:^54::23")]
        [TestCase("212D43D8-2B25-4A86--", "8,4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", "2024-10-29 17-54-23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "2560494", "2024:10:29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4,5", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8.4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8/4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("12345", "8,4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,,,,4", "2560494", "2024-10-29 17:54:23")]
        [TestCase("212D43D8-2B25-4A86-B286-F7F4D53AE27E", "8,4", "hhj86", "2024 + 10 - 29 17:54:23")]

        public void OrderCreationArgumentException(string id, string weight, string districtId, string time)
        { 
            Assert.Throws<ArgumentException>(() => new Order(id, weight, districtId, time));
            
        }
       
    }
}
