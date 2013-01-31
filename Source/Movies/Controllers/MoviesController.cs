using System.Web.Mvc;

namespace Movies.Controllers
{
    using Movies.DataAccess;
    using Movies.Models;

    public class MoviesController : MmdbController
    {
        public MoviesController(IDataContext ctx)
            : base(ctx)
        { }

        public ActionResult Index()
        {
            return View(CurrentUser.Movies);
        }

        public ActionResult Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                movie.User = CurrentUser;
                DataContext.Movies.Save(movie);
                return RedirectToAction("Index");
            }
            return View("Index", CurrentUser.Movies);
        }

        public ActionResult Delete(int id)
        {
            var movie = DataContext.Movies.Find(id);
            DataContext.Movies.Delete(movie);
            return RedirectToAction("Index");
        }
    }
}
