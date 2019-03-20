using System.Web.Http;

namespace PTC.Controllers
{
    [RoutePrefix("api/productApi")]
    public class ProductApiController : ApiController
    {
        public IHttpActionResult Get()
        {
            IHttpActionResult ret;
            PTCViewModel vm = new PTCViewModel();

            vm.Get();

            if (vm.Products.Count > 0)
            {
                ret = Ok(vm.Products);
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

        [Route("Search")]
        [HttpPost]
        public IHttpActionResult Search([FromBody]ProductSearch search)
        {
            var vm = new PTCViewModel();
            vm.SearchEntity = search;
            vm.Search();
            if(vm.LastException != null)
            {
                return BadRequest(vm.Message);
            }
            else
            {
                return Ok(vm.Products);
            }
        }
    }
}