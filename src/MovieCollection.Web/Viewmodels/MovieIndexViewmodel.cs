using MovieCollection.Web.Models;
using System.Collections.Generic;

namespace MovieCollection.Web.Viewmodels
{
    public class MovieIndexViewModel
    {
        public List<MoviePopular> popularMovieModel { get; set; }
        public List<MovieSearch> searchMovieModel { get; set; }
        public Search searchModel { get; set; }
    }
}