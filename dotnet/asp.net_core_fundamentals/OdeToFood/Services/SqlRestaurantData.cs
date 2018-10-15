using System.Collections.Generic;
using System.Linq;
using OdeToFood.Data;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext _context;

        public SqlRestaurantData(OdeToFoodDbContext context)
        {
            _context = context;
        }
        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Get(int id)
            => _context.Restaurants.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Restaurant> GetAll()
            => _context.Restaurants.OrderBy(x => x.Name);
    }
}