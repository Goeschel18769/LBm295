using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LBm295.Models
{
    public class PublisherDB : DbContext
    {
        private readonly IConfiguration _configuration;

        public PublisherDB()
        {
        }

        public PublisherDB(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
