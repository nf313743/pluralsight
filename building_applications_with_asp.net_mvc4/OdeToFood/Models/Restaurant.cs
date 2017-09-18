﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class Restaurant
    {
        public string City { get; set; }
        public string Country { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<RestaurantReview> Reviews { get; set; }
    }
}