using System.Web.Mvc;
using TMDbLib.Client;
using Movie = MovieCollection.Web.Models.Movie;

namespace MovieCollection.Web.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {       
            return View();
        }
    }
}