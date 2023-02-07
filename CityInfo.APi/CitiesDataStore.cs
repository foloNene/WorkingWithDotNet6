using CityInfo.APi.Models;

namespace CityInfo.APi
{
    public class CitiesDataStore
    {
        public List<CityDto> Cities { get; set; }

        // return an instance //singleton pattern at work
        //public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Central Park",
                            Description = "The most visited urban park in the united states."
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Empire state Building",
                            Description = "A 102-storey skyscrapper located in midtown manhatten"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathederal that was never really finish",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 3,
                            Name = "Cathedral of Our lady",
                            Description = "A Gothic style cathedral, conceived by architects"
                        },
                         new PointOfInterestDto()
                        {
                            Id = 4,
                            Name = "Antwarp centeral station",
                            Description = "The first finesr example of railway architecture in"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The romance city.",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 5,
                            Name = "Effel tower",
                            Description = "A wrough iro lattice town on the cheap de mars, named after"
                        },
                        new PointOfInterestDto()
                        {
                            Id =6,
                            Name = "The Louvre",
                            Description = "The world's ;argest meseum"
                        },
                    }
                }
            };
        }
    }
}
