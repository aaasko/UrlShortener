using UrlShortener.Utils;

namespace UrlShortenerTests.Utils
{
    [TestClass]
    public class Base62Tests
    {
        [TestMethod]
        public void Encode_When0_MustReturn0()
        {
            Assert.AreEqual("0", Base62.Encode(0));
        }

        [TestMethod]
        public void Encode_When9_MustReturn9()
        {
            Assert.AreEqual("9", Base62.Encode(9));
        }

        [TestMethod]
        public void Encode_When10_MustReturnUppercaseA()
        {
            Assert.AreEqual("A", Base62.Encode(10));
        }

        [TestMethod]
        public void Encode_When35_MustReturnUppercaseZ()
        {
            Assert.AreEqual("Z", Base62.Encode(35));
        }

        [TestMethod]
        public void Encode_When36_MustReturnLowercaseA()
        {
            Assert.AreEqual("a", Base62.Encode(36));
        }

        [TestMethod]
        public void Encode_When61_MustReturnLowercaseZ()
        {
            Assert.AreEqual("z", Base62.Encode(61));
        }

        [TestMethod]
        public void Encode_When62_MustReturn01()
        {
            Assert.AreEqual("01", Base62.Encode(62));
        }
    }
}