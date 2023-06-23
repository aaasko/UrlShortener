namespace UrlShortener.Services
{
    public class CurrentDateTime : IDateTime
    {
        public DateTime UtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}
