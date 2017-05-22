using System.Collections.Generic;
using System.Web.Mvc;
using MovieCollection.Web.Models;
using MovieCollection.Web.Viewmodels;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;
using System.Configuration;

namespace MovieCollection.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            MovieIndexViewModel popularMovies = new MovieIndexViewModel();
            popularMovies.popularMovieModel = new List<MoviePopular>();

            TMDbClient client = new TMDbClient(ConfigurationManager.AppSettings["TMDbKey"]);
            SearchContainer<SearchMovie> movieApiResults = client.GetMoviePopularListAsync().Result;

            foreach (var newMovie in movieApiResults.Results)
            {
                MoviePopular popularMovie = new MoviePopular();

                popularMovie.Id = newMovie.Id;
                popularMovie.Title = newMovie.Title;
                popularMovie.ReleaseDate = newMovie.ReleaseDate;
                popularMovie.ImageUrl = "http://image.tmdb.org/t/p/w185/" + newMovie.PosterPath;

                popularMovies.popularMovieModel.Add(popularMovie);
            }

            return View(popularMovies);
        }

        [HttpPost]
        public ActionResult Index(MovieIndexViewModel movieViewModel)
        {
            movieViewModel.searchMovieModel = new List<MovieSearch>();

            TMDbClient client = new TMDbClient(ConfigurationManager.AppSettings["TMDbKey"]);
            SearchContainer<SearchMovie> movieApiResults = client.SearchMovieAsync(movieViewModel.searchModel.Title).Result;

            foreach (var movie in movieApiResults.Results)
            {
                MovieSearch localMovie = new MovieSearch();

                localMovie.Id = movie.Id;
                localMovie.Title = movie.Title;
                localMovie.ReleaseDate = movie.ReleaseDate;
                localMovie.ImageUrl = "http://image.tmdb.org/t/p/w185/" + movie.PosterPath;

                movieViewModel.searchMovieModel.Add(localMovie);
            }

            return View(movieViewModel);
        }
    }
}