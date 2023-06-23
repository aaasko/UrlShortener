using Microsoft.AspNetCore.Mvc;
using UrlShortener.Services;

namespace UrlShortener.Controllers
{
    [Route("api/v1/shortUrl")]
    public class RedirectController : Controller
    {
        private readonly IUrlService _urlService;

        public RedirectController(IUrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet]
        [Route("{shortUrl}")]
        public ActionResult Index(string shortUrl)
        {
            string? longUrl = _urlService.GetLongUrl(shortUrl);

            if (longUrl == null)
            {
                return NotFound();
            }

            return Redirect(longUrl);
        }
    }
}
