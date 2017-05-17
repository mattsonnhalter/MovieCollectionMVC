using System.Collections.Generic;
using System.Web.Mvc;
using MovieCollection.Web.Models;
using MovieCollection.Web.Viewmodels;
using TMDbLib.Client;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Search;

namespace MovieCollection.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            MovieIndexViewModel popularMovies = new MovieIndexViewModel();
            popularMovies.movieModels = new List<Movie>();

            TMDbClient client = new TMDbClient("1d51304d2d0506fca98f49b582707408"); //TODO: Eventually make this key a config value
            SearchContainer<SearchMovie> movieApiResults = client.GetMoviePopularListAsync().Result;

            foreach (var newMovie in movieApiResults.Results)
            {
                Movie popularMovie = new Movie();

                popularMovie.Id = newMovie.Id;
                popularMovie.Title = newMovie.Title;
                popularMovie.ReleaseDate = newMovie.ReleaseDate;
                popularMovie.ImageUrl = "http://image.tmdb.org/t/p/w185/" + newMovie.PosterPath;

                popularMovies.movieModels.Add(popularMovie);
            }

            return View(popularMovies);
        }

        [HttpPost]
        public ActionResult Index(MovieIndexViewModel movieViewModel)
        {
            movieViewModel.movieModels = new List<Movie>();

            TMDbClient client = new TMDbClient("1d51304d2d0506fca98f49b582707408"); //TODO: Eventually make this key a config value
            SearchContainer<SearchMovie> movieApiResults = client.SearchMovieAsync(movieViewModel.searchModel.Title).Result;

            foreach (var m in movieApiResults.Results)
            {
                Movie localMovie = new Movie();

                localMovie.Id = m.Id;
                localMovie.Title = m.Title;
                localMovie.ReleaseDate = m.ReleaseDate;
                localMovie.ImageUrl = "http://image.tmdb.org/t/p/w185/" + m.PosterPath;

                movieViewModel.movieModels.Add(localMovie);
            }

            return View(movieViewModel);
        }
    }
}