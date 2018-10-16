using System.Collections.Generic;
using System.Linq;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurants;

        public InMemoryRestaurantData()
        {
            _restaurants = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name = "Scott's Pizza Palace"},
                new Restaurant{Id=2, Name="Tersiguels"},
                new Restaurant{Id=3, Name="King's Contrivance"}
            };
        }

        public Restaurant Add(Restaurant restaurant)
        {
            restaurant.Id = _restaurants.Max(x => x.Id) + 1;
            _restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Get(int id) => _restaurants.FirstOrDefault(x => x.Id == id);

        public IEnumerable<Restaurant> GetAll()
            => _restaurants.OrderBy(x => x.Name);

        public Restaurant Update(Restaurant restaurant)
        {
            throw new System.NotImplementedException();
        }
    }
}