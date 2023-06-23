using Microsoft.EntityFrameworkCore;

namespace UrlShortener.Entities
{
    [Index(nameof(LongUrl))]
    [Index(nameof(ShortUrl))]
    public class Url
    {
        public required long Id { get; set; }
        public required string LongUrl { get; set; }
        public required string ShortUrl { get; set; }
    }
}
