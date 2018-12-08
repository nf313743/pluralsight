using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context;
        }

        public void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest)
        {
            var city = GetCity(cityId,false);
            city.PointsOfInterest.Add(pointOfInterest);
        }

        public bool CityExists(int cityId)
            => _context.Cities.Any(x => x.Id == cityId);

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
            => _context.PointsOfInterest.Remove(pointOfInterest);

        public IEnumerable<City> GetCities()
            => _context.Cities.OrderBy(x => x.Name).ToList();


        public City GetCity(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return _context.Cities.Include(x => x.PointsOfInterest)
                                .Where(x => x.Id == cityId)
                                .FirstOrDefault();
            }
            else
            {
                return _context.Cities.Where(x => x.Id == cityId)
                                .FirstOrDefault();
            }
        }


        public PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId)
            => _context.PointsOfInterest.Where(x => x.CityId == cityId
                                                && x.Id == pointOfInterestId)
                                        .FirstOrDefault();
        public IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId)
            => _context.PointsOfInterest.Where(x => x.CityId == cityId).ToList();

        public bool Save()
            => _context.SaveChanges() >= 0;
        
    }
}