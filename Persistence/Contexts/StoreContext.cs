using GenerativeAIChatBot.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GenerativeAIChatBot.Persistence.Contexts
{
    public class StoreContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"))
                .Build();

                var defaultConnectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlite(defaultConnectionString);
            }
        }
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }

        public DbSet<History> History { get; set; }

    }
}
