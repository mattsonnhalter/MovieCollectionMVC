using MovieCollection.Web.Models;
using System.Collections.Generic;

namespace MovieCollection.Web.Viewmodels
{
    public class MovieIndexViewmodel
    {
        public List<Movie> movieModels { get; set; }
        public Search searchModel { get; set; }
    }
}