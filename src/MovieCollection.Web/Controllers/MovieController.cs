using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieCollection.Web.Models;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieCollection.Web.Controllers
{
    public class MovieController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string movieSearch)
        {
            TMDbClient client = new TMDbClient("1d51304d2d0506fca98f49b582707408");
            SearchContainer<SearchMovie> movieFromApi = client.SearchMovieAsync(movieSearch).Result;
            
            var movieList = new List<Movie>();

            foreach (var m in movieFromApi.Results)
            {
                Movie localMovie = new Movie();
               
                localMovie.Id = m.Id;
                localMovie.Title = m.Title;
                localMovie.ReleaseDate = m.ReleaseDate;
                localMovie.ImageUrl = m.PosterPath;

                movieList.Add(localMovie);
            }

            return View(movieList);
        }
    }
}