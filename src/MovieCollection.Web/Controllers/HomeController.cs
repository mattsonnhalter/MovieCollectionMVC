using System;
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
            MovieIndexViewModel indexViewModel = new MovieIndexViewModel();

            GetPopularMovies(indexViewModel);

            return View(indexViewModel);
        }
    
        [HttpPost]
        public ActionResult Index(MovieIndexViewModel indexViewModel)
        {
            GetPopularMovies(indexViewModel);
            GetSearchResults(indexViewModel);

            return View(indexViewModel);
        }

        private void GetSearchResults(MovieIndexViewModel indexViewModel)
        {
            indexViewModel.searchMovieModel = new List<MovieSearch>();

            TMDbClient client = new TMDbClient(ConfigurationManager.AppSettings["TMDbKey"]);
            SearchContainer<SearchMovie> movieApiResults = client.SearchMovieAsync(indexViewModel.searchModel.Title).Result;
            if (movieApiResults.TotalResults >= 1)
            {
                foreach (var movie in movieApiResults.Results)
                {
                    MovieSearch localMovie = new MovieSearch
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        ReleaseDate = movie.ReleaseDate,
                        ImageUrl = "http://image.tmdb.org/t/p/w185/" + movie.PosterPath
                    };

                    indexViewModel.searchMovieModel.Add(localMovie);
                }
            }
            else
            {
                MovieSearch noResultsMovie = new MovieSearch
                {
                    Id = 0,
                    Title = "No Title",
                    ReleaseDate = DateTime.Now,
                    ImageUrl = "http://image.tmdb.org/t/p/w185/"
                };

                indexViewModel.searchMovieModel.Add(noResultsMovie);
            }
        }

        private void GetPopularMovies(MovieIndexViewModel indexViewModel)
        {
            indexViewModel.popularMovieModel = new List<MoviePopular>();

            TMDbClient client = new TMDbClient(ConfigurationManager.AppSettings["TMDbKey"]);
            SearchContainer<SearchMovie> movieApiResults = client.GetMoviePopularListAsync().Result;

            foreach (var newMovie in movieApiResults.Results)
            {
                MoviePopular popularMovie = new MoviePopular
                {
                    Id = newMovie.Id,
                    Title = newMovie.Title,
                    ReleaseDate = newMovie.ReleaseDate,
                    ImageUrl = "http://image.tmdb.org/t/p/w185/" + newMovie.PosterPath
                };

                indexViewModel.popularMovieModel.Add(popularMovie);
            }
        }
    }
}