using System.Web.Http;

namespace PTC.Controllers
{
    [RoutePrefix("api/CategoryApi")]
    public class CategoryApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            var vm = new PTCViewModel();
            vm.LoadCategories();

            if (vm.Categories.Count > 0)
            {
                return Ok(vm.Categories);
            }
            else if (vm.LastException != null)
            {
                return BadRequest(vm.Message);
            }

            return NotFound();
        }

        [HttpPost]
        [Route("SearchCategories")]
        public IHttpActionResult GetSearchCategories()
        {
            IHttpActionResult ret;
            var vm = new PTCViewModel();

            vm.LoadSearchCategories();

            if (vm.SearchCategories.Count > 0)
            {
                ret = Ok(vm.SearchCategories);
            }
            else if (vm.LastException != null)
            {
                ret = BadRequest(vm.Message);
            }
            else
            {
                ret = NotFound();
            }

            return ret;
        }
    }
}