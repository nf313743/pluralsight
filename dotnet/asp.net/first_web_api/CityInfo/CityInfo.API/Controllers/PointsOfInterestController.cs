using System;
using System.Linq;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _repository;

        public PointsOfInterestController(
            ILogger<PointsOfInterestController> logger,
            IMailService mailService,
            ICityInfoRepository repository)
        {
            _logger = logger;
            _mailService = mailService;
            _repository = repository;
        }

        [HttpGet("{cityId}/pointsofinterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            try
            {
                if (!_repository.CityExists(cityId))
                {
                    _logger.LogInformation($"City {cityId} not found.");
                    return NotFound();
                }

                var pointsOfInterestForCity = _repository.GetPointsOfInterestForCity(cityId)
                                                .Select(x => new PointsOfInterestDto
                                                {
                                                    Id = x.Id,
                                                    Name = x.Name,
                                                    Description = x.Description
                                                });

                return Ok(pointsOfInterestForCity);

            }
            catch (Exception ex)
            {
                _logger.LogCritical("EXCEPTION!!", ex);
                return StatusCode(500, "A problem occurred.");
            }
        }

        [HttpGet("{cityId}/pointsofinterest/{id}", Name = nameof(GetPointsOfInterest))]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            if (!_repository.CityExists(cityId))
            {
                _logger.LogInformation($"City {cityId} not found.");
                return NotFound();
            }

            var pointOfInterest = _repository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterest == null)
            {
                return NotFound();
            }

            return Ok(new PointsOfInterestDto
            {
                Id = pointOfInterest.Id,
                Description = pointOfInterest.Description,
                Name = pointOfInterest.Name
            });
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(
            int cityId,
            [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {

            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "Name and description must be different.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var finalPointOfInterest = Mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            _repository.AddPointOfInterestForCity(cityId, finalPointOfInterest);

            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong on save.");
            }

            var createdPointOfInterest = Mapper.Map<Models.PointsOfInterestDto>(finalPointOfInterest);

            return CreatedAtRoute(
                nameof(GetPointsOfInterest),
                new { cityId = cityId, id = finalPointOfInterest.Id },
                createdPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointOfInterest(
             int cityId,
             int id,
             [FromBody] PointsOfInterestForCreationDto pointOfInterest)
        {

            if (pointOfInterest == null)
            {
                return BadRequest();
            }

            if (pointOfInterest.Description == pointOfInterest.Name)
            {
                ModelState.AddModelError("Description", "Name and description must be different.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            Mapper.Map(pointOfInterest, pointOfInterestEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong on save.");
            }

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult PartiallyUpdatePointOfInterest(
            int cityId,
            int id,
            [FromBody] JsonPatchDocument<PointsOfInterestForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                return BadRequest();
            }

            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = Mapper.Map<PointsOfInterestForUpdateDto>(pointOfInterestEntity);


            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (pointOfInterestToPatch.Description == pointOfInterestToPatch.Name)
            {
                ModelState.AddModelError("Description", "Name and description must be different.");
            }

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Mapper.Map(pointOfInterestToPatch, pointOfInterestEntity);

            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong on save.");
            }

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int id)
        {
            if (!_repository.CityExists(cityId))
            {
                return NotFound();
            }

            var pointOfInterestEntity = _repository.GetPointOfInterestForCity(cityId, id);

            if (pointOfInterestEntity == null)
            {
                return NotFound();
            }

            _repository.DeletePointOfInterest(pointOfInterestEntity);


            if (!_repository.Save())
            {
                return StatusCode(500, "Something went wrong on save.");
            }



            _mailService.Send("Item deleted", $"CityId {cityId} deleted");
            return NoContent();
        }
    }
}