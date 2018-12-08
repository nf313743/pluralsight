using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;

namespace CityInfo.API
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context)
        {
            if(context.Cities.Any())
            {
                return;
            }

            var cities = new List<City>
            {
                new City
                {
                    Name = "New York City",
                    Description = "The one with the big park.",
                    PointsOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Name = "Central Park",
                            Description="The most visited urban park in the US."
                        },
                        new PointOfInterest
                        {
                            Name = "Empire State Building",
                            Description="A 102-story skyscrapper located in Midtown Manhattan."
                        }
                    }
                },
                new City
                {
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never finished.",
                    PointsOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Name = "Cathedral of Our Lady",
                            Description="A gothic style cathedral"
                        },
                        new PointOfInterest
                        {
                            Name = "Antwerp Central Station",
                            Description="First train in Belgium"
                        }
                    }
                },
                new City
                {
                    Name= "Paris",
                    Description = "The one with that big tower.",
                    PointsOfInterest = new List<PointOfInterest>
                    {
                        new PointOfInterest
                        {
                            Name = "Eiffel Tower",
                            Description="Iron lattice tower"
                        },
                        new PointOfInterest
                        {
                            Name = "The Louvre",
                            Description="The world's largest museum."
                        }
                    }
                }
            };

            context.AddRange(cities);
            context.SaveChanges();
        }
    }
}