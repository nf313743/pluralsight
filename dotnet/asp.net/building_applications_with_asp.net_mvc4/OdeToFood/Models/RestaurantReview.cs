﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OdeToFood.Models
{
    public class RestaurantReview
    {
        [Required]
        [StringLength(1024)]
        public string Body { get; set; }
        public int Id { get; set; }

        [Display(Name = "User Name")]
        [DisplayFormat(NullDisplayText = "Anonymous")]
        public string ReviewerName { get; set; }

        [Range(1,10)]
        [Required]
        public int Rating { get; set; }
        public int RestaurantId { get; set; }
    }
}