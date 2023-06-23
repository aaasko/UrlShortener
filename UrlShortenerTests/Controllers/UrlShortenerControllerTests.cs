using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Controllers;
using UrlShortener.Database;
using UrlShortener.Entities;

namespace UrlShortenerTests.Controllers
{
    [TestClass]
    public class UrlShortenerControllerTests
    {
        private const string AddedLongUrl = "https://example.com";
        private const string ArbitraryUnaddedUrl = "https://duckduckgo.com";
        // These tests assume that 0 cannot be encoded as "abc",
        // and that 0 corresponds to the start of the (Snowflake) epoch.
        private const string AddedShortUrl = "abc";

        private DbConnection _connection = null!;
        private DbContextOptions<UrlsContext> _contextOptions = null!;

        [TestInitialize]
        public void Initialize()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
            _contextOptions = new DbContextOptionsBuilder<UrlsContext>()
                .UseSqlite(_connection)
                .Options;

            using var context = new UrlsContext(_contextOptions);

            context.Database.EnsureCreated();
            context.Urls.Add(new Url() { Id = 0, LongUrl = AddedLongUrl, ShortUrl = AddedShortUrl });
            context.SaveChanges();
        }

        [TestCleanup]
        public void Cleanup() => _connection.Dispose();

        UrlsContext CreateContext() => new UrlsContext(_contextOptions);

        [TestMethod]
        public void ShortenAddedLongUrl_MustReturnAddedShortUrl()
        {
            using var context = CreateContext();
            var controller = CreateControllerAtEpochStart(context);
            var shortUrl = controller.Shorten(new ShortenRequest() { LongUrl = AddedLongUrl });

            Assert.AreEqual(AddedShortUrl, shortUrl);
        }

        [TestMethod]
        public void ShortenNewLongUrl_MustReturnNewShortUrl()
        {
            using var context = CreateContext();
            var controller = CreateControllerAtEpochStart(context);
            var shortUrl = controller.Shorten(new ShortenRequest() { LongUrl = ArbitraryUnaddedUrl });

            Assert.AreEqual("0", shortUrl);
        }

        private static UrlShortenerController CreateControllerAtEpochStart(UrlsContext context)
        {
            return new UrlShortenerController(DbBasedUrlServiceAtEpochStart.Create(context));
        }
    }
}