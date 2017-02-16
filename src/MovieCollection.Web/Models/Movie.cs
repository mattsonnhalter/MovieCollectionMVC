using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieCollection.Web.Models
{
    public class Movie
    {
        [Required]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string Tagline { get; set; }
        
        public string ImageUrl { get; set; } 
    }
}