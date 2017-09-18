using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class RestaurantReview
    {
        public string Body { get; set; }
        public int Id { get; set; }
        public string ReviewerName { get; set; }
        public int Rating { get; set; }
        public int RestaurantId { get; set; }
    }
}