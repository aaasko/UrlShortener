namespace UrlShortener.Services
{
    public interface IIdEncoder
    {
        string Encode(long id);
    }
}
