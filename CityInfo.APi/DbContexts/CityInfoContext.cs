using CityInfo.APi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.APi.DbContexts
{
    public class CityInfoContext : DbContext
    {

        public DbSet<City> Cities { get; set; } = null!;

        public DbSet<PointOfInterest> PointsOfInterest { get; set; } = null!;

        public CityInfoContext(DbContextOptions<CityInfoContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("New York")
                {
                    Id = 1,
                    Description = "The one with the big Park."
                },
                new City("Antwerp")
                {
                    Id = 2,
                    Description = "The one with the cathedral that was never really finished"
                },
                new City("Paris")
                {
                    Id = 3,
                    Description = "The one with the big tower."
                });
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Central Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "The most visited urban park in the united state."
                },
                new PointOfInterest("Empire state building")
                {
                    Id = 2,
                    CityId = 1,
                    Description = "A 102-storey skyscrapper located in midtown manhattan."
                },
                new PointOfInterest("Cathedral")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "A Gothic style cathedral, conceived by architecture jan"
                },
                new PointOfInterest("Antwarp central station")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "The finest example of railway architecture in beliguim"
                },
                new PointOfInterest("Eiffel tower")
                {
                    Id = 5,
                    CityId = 3,
                    Description = "A wrought iron lattice tower on the champ de mars, named"
                },
                new PointOfInterest("The Louvre")
                {
                    Id = 6,
                    CityId = 3,
                    Description = "The world's largest museum."
                });
            base.OnModelCreating(modelBuilder);
        }
    }
}
