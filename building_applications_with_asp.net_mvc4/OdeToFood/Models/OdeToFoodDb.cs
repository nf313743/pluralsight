﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class OdeToFoodDb : DbContext
    {
        public OdeToFoodDb() : base("name=DefaultConnection")
        {
        }

        public DbSet<RestaurantReview> RestaurantReviews { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}