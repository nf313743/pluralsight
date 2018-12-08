using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {

        public CityInfoContext(DbContextOptions options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<City> Cities {get;set;}
        public DbSet<PointsOfInterest> PointsOfInterest {get;set;}

    }
}