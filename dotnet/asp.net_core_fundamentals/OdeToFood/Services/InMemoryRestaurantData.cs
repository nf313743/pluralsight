using System.Collections.Generic;
using System.Linq;
using OdeToFood.Models;

namespace OdeToFood.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        List<Restaurant> _restaurant;

        public InMemoryRestaurantData()
        {
            _restaurant = new List<Restaurant>
            {
                new Restaurant{Id = 1, Name = "Scott's Pizza Palace"},
                new Restaurant{Id=2, Name="Tersiguels"},
                new Restaurant{Id=3, Name="King's Contrivance"}
            };
        }

        public IEnumerable<Restaurant> GetAll()
            => _restaurant.OrderBy(x => x.Name);
    }
}