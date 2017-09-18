//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Mvc;
//using OdeToFood.Models;

//namespace OdeToFood.Controllers
//{
//    public class ReviewsController : Controller
//    {
//        //
//        // GET: /Reviews/

//        private static List<RestaurantReview> _reviews = new List<RestaurantReview>
//        {
//            new RestaurantReview
//            {
//                Id = 1,
//                Name= "Cinnamon Club",
//                City = "London",
//                Country = "UK",
//                Rating = 10
//            },
//            new RestaurantReview
//            {
//                Id = 2,
//                Name= "Marrakesh",
//                City = "D.C",
//                Country = "USA",
//                Rating = 10
//            },
//            new RestaurantReview
//            {
//                Id = 3,
//                Name= "The House of Elliot",
//                City = "Ghent",
//                Country = "Belgium",
//                Rating = 10
//            }
//        };

//        [ChildActionOnly]
//        public ActionResult BestReview()
//        {
//            var bestReview = _reviews.OrderByDescending(x => x.Rating).First();
//            return PartialView("_Review", bestReview);
//        }

//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Create(FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add insert logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        public ActionResult Details(int id)
//        {
//            return View();
//        }

//        public ActionResult Edit(int id)
//        {
//            var review = _reviews.Single(x => x.Id == id);

//            return View(review);
//        }

//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            var review = _reviews.Single(x => x.Id == id);

//            if (TryUpdateModel(review))
//            {
//                return RedirectToAction("Index");
//            }

//            return View(review);
//        }

//        public ActionResult Index()
//        {
//            var model = _reviews.OrderBy(x => x.Country);

//            return View(model);
//        }

//        //
//        // GET: /Reviews/Details/5
//        //
//        // GET: /Reviews/Create
//        //
//        // POST: /Reviews/Create
//        //
//        // GET: /Reviews/Edit/5
//        //
//        // POST: /Reviews/Edit/5
//        //
//        // GET: /Reviews/Delete/5
//        //
//        // POST: /Reviews/Delete/5
//    }
//}