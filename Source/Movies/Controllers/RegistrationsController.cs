using System.Web.Mvc;

namespace Movies.Controllers
{
    using Movies.DataAccess;
    using Movies.Models;

    public class RegistrationsController : MoviesController 
    {
        public RegistrationsController(IDataContext ctx)
            : base(ctx)
        { }

        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                DataContext.Users.Save(user);
                return RedirectToAction("Index", "Movies");
            }
            return View(user);
        }
    }
}
