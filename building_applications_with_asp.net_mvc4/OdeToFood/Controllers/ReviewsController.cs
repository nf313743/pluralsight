using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OdeToFood.Models;

namespace OdeToFood.Controllers
{
    public class ReviewsController : Controller
    {
        OdeToFoodDb _db = new OdeToFoodDb();


        public ActionResult Index([Bind(Prefix="id")] int restaurantId)
        {
            var restaurant = _db.Restaurants.Find(restaurantId);
            if(restaurant != null)
            {
                return View(restaurant);
            }
            return HttpNotFound();
        }

        [HttpPost]
        public ActionResult Create(RestaurantReview review)
        {
            if(ModelState.IsValid)
            {
                _db.RestaurantReviews.Add(review);
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        [HttpPost]
        public ActionResult Edit(RestaurantReview review)
        {
            if(ModelState.IsValid)
            {
                _db.Entry(review).State = System.Data.EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index", new { id = review.RestaurantId });
            }
            return View(review);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = _db.RestaurantReviews.Find(id);
            return View(model);
        }

        [HttpGet]
        public ActionResult Create(int restaurantId)
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            _db.Dispose();
            base.Dispose(disposing);
        }

    }
}