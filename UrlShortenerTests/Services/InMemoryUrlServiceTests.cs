using Moq;
using UrlShortener.Services;

namespace UrlShortenerTests.Services
{
    [TestClass]
    public class InMemoryUrlServiceTests
    {
        [TestMethod]
        public void AddNewUrl_MustReturnEncodedId()
        {
            var encoded = "encoded";
            var inMemoryUrlService = new InMemoryUrlService(FixedIdEncoder(encoded));
            var shortUrl = inMemoryUrlService.Add("https://example.com/");

            Assert.AreEqual(encoded, shortUrl);
        }

        [TestMethod]
        public void TwiceAddSameUrl_MustReturnSameShortUrls()
        {
            var encoded = "encoded";
            var inMemoryUrlService = new InMemoryUrlService(FixedIdEncoder(encoded));
            var shortUrl1 = inMemoryUrlService.Add("https://example.com/");
            var shortUrl2 = inMemoryUrlService.Add("https://example.com/");

            Assert.AreEqual(encoded, shortUrl1);
            Assert.AreEqual(encoded, shortUrl2);
        }

        [TestMethod]
        public void AddDifferentUrls_MustReturnDifferentShortUrls()
        {
            var idEncoder = new Mock<IIdEncoder>();
            var encoded1 = "encoded1";
            var encoded2 = "encoded2";
            
            idEncoder.SetupSequence(e => e.Encode(It.IsAny<long>()))
                .Returns(encoded1)
                .Returns(encoded2);

            var inMemoryUrlService = new InMemoryUrlService(idEncoder.Object);
            var shortUrl1 = inMemoryUrlService.Add("https://example.com/");
            var shortUrl2 = inMemoryUrlService.Add("https://google.com/");

            Assert.AreEqual(encoded1, shortUrl1);
            Assert.AreEqual(encoded2, shortUrl2);
        }

        [TestMethod]
        public void GetLongUrl_MustReturnAddedUrl()
        {
            var encoded = "encoded";
            var inMemoryUrlService = new InMemoryUrlService(FixedIdEncoder(encoded));
            var addedUrl = "https://example.com/";
            var shortUrl = inMemoryUrlService.Add(addedUrl);
            var longUrl = inMemoryUrlService.GetLongUrl(shortUrl);

            Assert.AreEqual(addedUrl, longUrl);
        }

        [TestMethod]
        public void GetLongUrl_MustNotReturnNotAddedUrl()
        {
            var inMemoryUrlService = new InMemoryUrlService(new Mock<IIdEncoder>().Object);
            var longUrl = inMemoryUrlService.GetLongUrl("shortUrl");

            Assert.IsNull(longUrl);
        }

        private static IIdEncoder FixedIdEncoder(string encoded)
        {
            var idEncoder = new Mock<IIdEncoder>();

            idEncoder.Setup(e => e.Encode(It.IsAny<long>())).Returns(encoded);

            return idEncoder.Object;
        }
    }
}