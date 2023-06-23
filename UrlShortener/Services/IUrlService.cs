namespace UrlShortener.Services
{
    public interface IUrlService
    {
        /// <summary>
        /// Registers an URL and returns a corresponding short URL.
        /// Returns an existing short URL if the long URL is already registered.
        /// </summary>
        /// <param name="longUrl"></param>
        /// <returns></returns>
        string Add(string longUrl);

        /// <summary>
        /// Returns a long URL if it was registered.
        /// </summary>
        /// <param name="shortUrl"></param>
        /// <returns></returns>
        string? GetLongUrl(string shortUrl);
    }
}
