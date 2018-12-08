using System.Collections.Generic;
using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();

        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            Cities = new List<CityDto>
            {
                new CityDto
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with the big park.",
                    PointsOfInterest = new List<PointsOfInterestDto>
                    {
                        new PointsOfInterestDto
                        {
                            Id= 1,
                            Name = "Central Park",
                            Description="The most visited urban park in the US."
                        },
                        new PointsOfInterestDto
                        {
                            Id= 2,
                            Name = "Empire State Building",
                            Description="A 102-story skyscrapper located in Midtown Manhattan."
                        }
                    }
                },
                new CityDto
                {
                    Id =2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never finished.",
                    PointsOfInterest = new List<PointsOfInterestDto>
                    {
                        new PointsOfInterestDto
                        {
                            Id= 3,
                            Name = "Cathedral of Our Lady",
                            Description="A gothic style cathedral"
                        },
                        new PointsOfInterestDto
                        {
                            Id= 4,
                            Name = "Antwerp Central Station",
                            Description="First train in Belgium"
                        }
                    }
                },
                new CityDto
                {
                    Id =3,
                    Name= "Paris",
                    Description = "The one with that big tower.",
                    PointsOfInterest = new List<PointsOfInterestDto>
                    {
                        new PointsOfInterestDto
                        {
                            Id= 5,
                            Name = "Eiffel Tower",
                            Description="Iron lattice tower"
                        },
                        new PointsOfInterestDto
                        {
                            Id= 6,
                            Name = "The Louvre",
                            Description="The world's largest museum."
                        }
                    }
                }
            };
        }
    }
}