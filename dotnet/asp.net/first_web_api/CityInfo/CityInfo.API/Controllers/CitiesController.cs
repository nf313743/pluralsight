using System.Collections.Generic;
using System.Linq;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Models;
using AutoMapper;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private readonly ICityInfoRepository _repository;

        public CitiesController(ICityInfoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetCities()
            => Ok(Mapper.Map<IEnumerable<CityWithoutPointsOfInterestDto>>(_repository.GetCities()));


        [HttpGet("{id}")]
        public IActionResult GetCity(int id, bool includePointsOfInterest)
        {
            var city = _repository.GetCity(id, includePointsOfInterest);

            if (city == null)
            {
                return NotFound();
            }
            else
            {
                if (includePointsOfInterest)
                {
                    var cityResult = new CityDto
                    {
                        Id = city.Id,
                        Description = city.Description,
                        Name = city.Name
                    };

                    foreach (var poi in city.PointsOfInterest)
                    {
                        cityResult.PointsOfInterest.Add(
                            new PointsOfInterestDto
                            {
                                Id = poi.Id,
                                Name = poi.Name,
                                Description = poi.Description
                            });
                    }

                    return Ok(cityResult);
                }
                else
                {
                    return Ok(new CityWithoutPointsOfInterestDto
                    {
                        Id = city.Id,
                        Description = city.Description,
                        Name = city.Name
                    });
                }
            }
        }
    }
}