using System.Web.Mvc;
using TMDbLib.Client;
using Movie = MovieCollection.Web.Models.Movie;

namespace MovieCollection.Web.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            TMDbClient client = new TMDbClient("1d51304d2d0506fca98f49b582707408");
            TMDbLib.Objects.Movies.Movie movieFromApi = client.GetMovieAsync(47964).Result;

            Movie movie = new Movie();
            movie.Id = movieFromApi.Id;
            movie.Title = movieFromApi.Title;
            movie.ReleaseDate = movieFromApi.ReleaseDate;
            movie.Tagline = movieFromApi.Tagline;
            movie.ImageUrl = movieFromApi.PosterPath;        

            return View(movie);
        }        
    }
}