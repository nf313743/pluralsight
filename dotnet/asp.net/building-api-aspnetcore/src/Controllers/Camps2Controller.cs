using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CoreCodeCamp.Data;
using CoreCodeCamp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace CoreCodeCamp.Controllers
{
    [Route("api/camps")]
    [ApiVersion("2.0")]
    [ApiController]
    public class Camps2Controller : ControllerBase
    {
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly ICampRepository _repository;

        public Camps2Controller(
            ICampRepository repository,
            IMapper mapper,
            LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpDelete("{moniker}")]
        public async Task<IActionResult> Delete(string moniker)
        {
            try
            {
                var oldCamp = await _repository.GetCampAsync(moniker);

                if (oldCamp == null)
                {
                    return NotFound();
                }

                _repository.Delete(oldCamp);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest("Failed to delete camp");
        }

        [HttpGet]
        public async Task<IActionResult> Get(bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);
                var result = new
                {
                    Count = results.Count(),
                    Results = _mapper.Map<CampModel[]>(results)
                };
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);

                if (result == null)
                {
                    return NotFound();
                }

                return _mapper.Map<CampModel>(result);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                var existing = await _repository.GetCampAsync(model.Moniker);

                if (existing != null)
                {
                    return BadRequest("Moniker in use");
                }

                var location = _linkGenerator.GetPathByAction(
                    "Get",
                    "Camps",
                    new { moniker = model.Moniker });

                if (string.IsNullOrEmpty(location))
                {
                    return BadRequest("Could not use current moniker");
                }

                var camp = _mapper.Map<Camp>(model);
                _repository.Add(camp);

                if (await _repository.SaveChangesAsync())
                {
                    return Created(location, _mapper.Map<CampModel>(camp));
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }

        [HttpPut("{moniker}")]
        public async Task<ActionResult<CampModel>> Put(string moniker, CampModel model)
        {
            try
            {
                var oldCamp = await _repository.GetCampAsync(moniker);

                if (oldCamp == null)
                {
                    return NotFound($"Could not find camp with moniker of: {moniker}");
                }

                _mapper.Map(model, oldCamp);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<CampModel>(oldCamp);
                }
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }

            return BadRequest();
        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>> SearchByDate(DateTime theDate, bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(theDate, includeTalks);

                if (!results.Any())
                {
                    return NotFound();
                }

                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database failure");
            }
        }
    }
}