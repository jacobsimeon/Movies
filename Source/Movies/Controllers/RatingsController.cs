using System.Web.Mvc;

namespace Movies.Controllers
{
    using Movies.DataAccess;

    public class RatingsController : MmdbController
    {
        public RatingsController(IDataContext ctx)
            : base(ctx)
        { }

        [HttpPost]
        public ActionResult Edit(int id, int rating)
        {
            var movie = DataContext.Movies.Find(id);
            if (movie != null)
            {
                movie.Rating = rating;
                DataContext.Movies.Save(movie);
            }
            return RedirectToAction("Index", "Movies");
        }
    }
}
