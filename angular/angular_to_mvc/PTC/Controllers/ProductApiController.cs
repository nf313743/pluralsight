using PTC.Models;
using System.Linq;
using System.Web.Http;
using System.Web.Http.ModelBinding;

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

        [HttpPost]
        public IHttpActionResult Post(Product product)
        {
            IHttpActionResult ret = null;
            var vm = new PTCViewModel();

            if (product != null)
            {
                vm.Entity = product;
                vm.PageMode = PDSAPageModeEnum.Add;
                vm.Save();
                if (vm.IsValid)
                {
                    return Created(Request.RequestUri + vm.Entity.ProductId.ToString(), vm.Entity);
                }
                else
                {
                    if (vm.Messages.Count > 0)
                    {
                        return BadRequest(ConvertToModelState(vm.Messages));
                    }
                    else
                    {
                        return BadRequest(vm.Message);
                    }
                }
            }

            return NotFound();
        }

        private ModelStateDictionary ConvertToModelState(System.Web.Mvc.ModelStateDictionary state)
        {
            var ret = new ModelStateDictionary();

            foreach (var list in state.ToList())
            {
                for (int i = 0; i < list.Value.Errors.Count; ++i)
                {
                    ret.AddModelError(list.Key, list.Value.Errors[i].ErrorMessage);
                }
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
            if (vm.LastException != null)
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