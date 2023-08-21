

using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointOfInterest { get; set; }

        public CityInfoContext(DbContextOptions<CityInfoContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //"Server=.;Database=CityInfoDb;Trusted_Connection=True;"
                _ = optionsBuilder.UseSqlServer(_configuration["ConnectionStrings:CityInfoDb"]);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    Id = 1,
                    Name = "Tbilisi",
                    Description = "capital of Georgia",
                    Adress = "sfdfsfs",
                });
            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    Id = 2,
                    Name = "Tbilisi",
                    Description = "capital of Georgia",
                    Adress = "sfdfsfs",
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    Name = "sdf",
                    Description = "dsfds",
                    CityId = 1 // this sets up the relationship with the City
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
