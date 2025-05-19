using Microsoft.EntityFrameworkCore;
namespace TT12052025_ManagedIdentityTest.Models
{
    public class WeatherForecastContext: DbContext
    {
        public readonly IConfiguration _config;
        public WeatherForecastContext(DbContextOptions<WeatherForecastContext> options, IConfiguration config)
            : base(options)
        {
            _config = config;
        }
        public DbSet<WeatherForecast> WeatherForecasts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.GetConnectionString("WeatherForecastDb"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherForecast>(entity => 
            {
                entity.HasData(
                    new WeatherForecast
                    {
                        Id = 1, 
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        TemperatureC = 25,
                        Summary = "Sunny"
                    },
                    new WeatherForecast
                    {
                        Id = 2,
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
                        TemperatureC = 30,
                        Summary = "Hot"
                    },
                    new WeatherForecast
                    {
                        Id = 3,
                        Date = DateOnly.FromDateTime(DateTime.Now.AddDays(2)),
                        TemperatureC = 20,
                        Summary = "Cool"
                    }
                );
            });
        }
    }
}
