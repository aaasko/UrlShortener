using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Controllers;
using UrlShortener.Database;
using UrlShortener.Entities;

namespace UrlShortenerTests.Controllers
{
    [TestClass]
    public class RedirectControllerTests
    {
        private const string AddedLongUrl = "https://example.com";
        private const string AddedShortUrl = "abc";
        private const string ArbitraryUnaddedShortUrl = "def";

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
        public void IndexWithAddedShortUrl_MustRedirectToLongUrl()
        {
            using var context = CreateContext();
            var controller = CreateControllerAtEpochStart(context);
            var redirect = controller.Index(AddedShortUrl) as RedirectResult;

            Assert.IsNotNull(redirect);
            Assert.AreEqual(AddedLongUrl, redirect.Url);
        }

        [TestMethod]
        public void IndexWithUnaddedShortUrl_MustReturn404()
        {
            using var context = CreateContext();
            var controller = CreateControllerAtEpochStart(context);
            var notFound = controller.Index(ArbitraryUnaddedShortUrl) as NotFoundResult;

            Assert.IsNotNull(notFound);
        }

        private static RedirectController CreateControllerAtEpochStart(UrlsContext context)
        {
            return new RedirectController(DbBasedUrlServiceAtEpochStart.Create(context));
        }
    }
}