using Moq;
using UrlShortener.Services;

namespace UrlShortenerTests.TestUtils
{
    public class FixedDateTime
    {
        public static IDateTime Create(DateTime dateTime)
        {
            var mock = new Mock<IDateTime>();

            mock.Setup(d => d.UtcNow()).Returns(dateTime);

            return mock.Object;
        }
    }
}
