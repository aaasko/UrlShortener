using Microsoft.EntityFrameworkCore;
using UrlShortener.Entities;

namespace UrlShortener.Database
{
    public class UrlsContext : DbContext
    {
        private readonly IConfiguration? _configuration;

        public UrlsContext(DbContextOptions<UrlsContext> options, IConfiguration? configuration = null)
            : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured && _configuration != null)
            {
                options.UseNpgsql(_configuration.GetConnectionString("UrlShortenerDatabase"));
            }
        }

        public DbSet<Url> Urls { get; set; } = null!;
    }
}
