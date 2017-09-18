using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Filters;

namespace OdeToFood.Controllers
{
    [Log]
    public class CuisineController : Controller
    {
        //
        // GET: /Cuisine/

        public ActionResult Hello()
        {
            return Content("sdfsdf");
        }

        public ActionResult Search(string name)
        {
            throw new Exception("Something terrible");

            return Content("myMessage");
        }
    }
}