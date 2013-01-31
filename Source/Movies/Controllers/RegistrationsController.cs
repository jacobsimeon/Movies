using System.Web.Mvc;

namespace Movies.Controllers
{
    using System.Linq;

    using Movies.DataAccess;
    using Movies.Models;

    public class RegistrationsController : MmdbController 
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
            if(DataContext.Users.Where(u => u.Name == user.Name).Any())
            {
                ModelState.AddModelError("Name", "A user with that name already exists.");
            }
            if (user.Password != user.PasswordConfirmation)
            {
                ModelState.AddModelError("Password", "Password must match confirmation.");
            }

            if (ModelState.IsValid)
            {
                DataContext.Users.Save(user);
                CurrentUser = user;
                return RedirectToAction("Index", "Movies");
            }

            return View(user);
        }
    }
}
