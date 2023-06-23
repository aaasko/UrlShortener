using UrlShortener.Utils;

namespace UrlShortener.Services
{
    public class Base62IdEncoder : IIdEncoder
    {
        public string Encode(long id) => Base62.Encode(id);
    }
}
