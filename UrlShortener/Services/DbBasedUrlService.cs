using UrlShortener.Database;
using UrlShortener.Entities;

namespace UrlShortener.Services
{
    public class DbBasedUrlService : IUrlService
    {
        private readonly IIdEncoder _idEncoder;
        private readonly IIdGenerator _idGenerator;
        private readonly UrlsContext _context;

        public DbBasedUrlService(
            IIdEncoder idEncoder,
            IIdGenerator idGenerator,
            UrlsContext urlsContext)
        {
            _idEncoder = idEncoder;
            _idGenerator = idGenerator;
            _context = urlsContext;
        }

        public string Add(string longUrl)
        {
            var existingUrl = _context.Urls.FirstOrDefault(url => url.LongUrl == longUrl);

            if (existingUrl != null)
            {
                return existingUrl.ShortUrl;
            }

            var id = _idGenerator.GenerateId();
            var shortUrl = _idEncoder.Encode(id);
            var url = new Url { Id = id, ShortUrl = shortUrl, LongUrl = longUrl };
            
            _context.Urls.Add(url);
            _context.SaveChanges();

            return shortUrl;
        }

        public string? GetLongUrl(string shortUrl)
        {
            return _context.Urls.FirstOrDefault(url => url.ShortUrl == shortUrl)?.LongUrl;
        }
    }
}
