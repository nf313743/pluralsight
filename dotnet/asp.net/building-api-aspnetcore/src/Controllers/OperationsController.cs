using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public OperationsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpOptions("reloadconfig")]
        public ActionResult<bool> ReloadConfig()
        {
            try
            {
                var root = (IConfigurationRoot)_configuration;
                root.Reload();
                return Ok();
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}