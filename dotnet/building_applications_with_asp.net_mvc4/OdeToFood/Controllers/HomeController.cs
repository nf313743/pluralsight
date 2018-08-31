using OdeToFood.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OdeToFood.Controllers
{
    public class HomeController : Controller
    {
        private OdeToFoodDb _db = new OdeToFoodDb();

        public ActionResult AutoComplete(string term)
        {
            var model = _db.Restaurants
                .Where(x => x.Name.StartsWith(term))
                .Take(10)
                .Select(x => new
                {
                    label = x.Name
                });

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult About()
        {
            var model = new AboutModel();
            model.Name = "Scott";
            model.Location = "Maryland, USA";

            return View(model);
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [OutputCache(Duration = 360, VaryByHeader = "X-Requested-With;Accept-Language", Location = System.Web.UI.OutputCacheLocation.Server)]
        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            var model = _db.Restaurants
                .Where(x => searchTerm == null || x.Name.StartsWith(searchTerm))
                .OrderByDescending(x => x.Reviews.Average(y => y.Rating))
                .Select(x => new RestaurantListViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    City = x.City,
                    Country = x.Country,
                    CountOfReviews = x.Reviews.Count
                }
                ).ToPagedList(page, 10);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_Restaurants", model);
            }

            return View(model);
        }

        protected override void Dispose(bool disposing)
        {
            if (_db != null)
            {
                _db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}