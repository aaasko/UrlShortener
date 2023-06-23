using UrlShortener.Database;
using UrlShortener.Options;
using UrlShortener.Services;
using FixedOptions = Microsoft.Extensions.Options.Options;
using UrlShortenerTests.TestUtils;

namespace UrlShortenerTests.Controllers
{
    internal class DbBasedUrlServiceAtEpochStart
    {
        public static DbBasedUrlService Create(UrlsContext context)
        {
            return new DbBasedUrlService(
                    new Base62IdEncoder(),
                    new SnowflakeIdGenerator(
                        FixedDateTime.Create(SnowflakeIdGenerator.EpochStart),
                        FixedOptions.Create(new MachineOptions())),
                    context);
        }
    }
}
