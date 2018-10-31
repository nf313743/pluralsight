using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {

        public CitiesController()
        {
        }

        [HttpGet]
        public IActionResult GetCities()
            => Ok(CitiesDataStore.Current.Cities);

        [HttpGet("{id}")]
        public IActionResult GetCity(int id)
        {
            var cityToReturn = CitiesDataStore.Current.Cities.FirstOrDefault(x=>x.Id == id);

            if(cityToReturn == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cityToReturn);
            }
        }
    }
}