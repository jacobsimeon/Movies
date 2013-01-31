using System.Web.Mvc;

namespace Movies.Controllers
{
    public class MoviesController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
