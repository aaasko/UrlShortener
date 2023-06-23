namespace UrlShortener.Services
{
    public class InMemoryUrlService : IUrlService
    {
        private readonly IIdEncoder _idEncoder;
        private uint _lastId;
        private readonly Dictionary<string, string> _shortUrlByLongUrl = new();
        private readonly Dictionary<string, string> _longUrlByShortUrl = new();

        public InMemoryUrlService(IIdEncoder idEncoder)
        {
            _idEncoder = idEncoder;
        }

        public string Add(string longUrl)
        {
            if (_shortUrlByLongUrl.TryGetValue(longUrl, out string? existingShortUrl))
            {
                return existingShortUrl;
            }

            var id = ++_lastId;
            var newShortUrl = _idEncoder.Encode(id);
            _longUrlByShortUrl.Add(newShortUrl, longUrl);
            _shortUrlByLongUrl.Add(longUrl, newShortUrl);

            return newShortUrl;
        }
        
        public string? GetLongUrl(string shortUrl)
        {
            return _longUrlByShortUrl.GetValueOrDefault(shortUrl);
        }
    }
}
