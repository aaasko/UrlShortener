using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class UrlShortenerController : ControllerBase
    {
        private readonly IUrlService _urlService;

        public UrlShortenerController(IUrlService urlService) {
            _urlService = urlService;
        }

        [HttpPost]
        [Route("data/shorten")]
        public string Shorten(ShortenRequest request)
        {
            return _urlService.Add(request.LongUrl);
        }
    }
}
