using System.Web.Mvc;

namespace Movies.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using FlashMessage;

    using Movies.DataAccess;
    using Movies.Models;

    public class SessionsController : MmdbController 
    {
        public SessionsController(IDataContext ctx)
            : base(ctx)
        { }

        public ActionResult Create()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            // don't user default user validations
            // since we don't need password confirmation
            ModelState.Clear();

            if (string.IsNullOrEmpty(user.Name))
            {
                ModelState.AddModelError("Username", "Please enter your username.");
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Password", "Please enter your password.");
            }

            var userByName = DataContext.Users.Where(u => u.Name == user.Name).FirstOrDefault();
            if (userByName == null || !user.Authenticate(user.Password))
            {
                ModelState.AddModelError("Username", "Invalid username or password");
            }

            if (ModelState.IsValid)
            {
                CurrentUser = userByName;
                var message = string.Format("Successfully signed in as {0}", userByName.Name);
                return RedirectToAction("Index", "Movies").WithFlash(new { notice = message });
            }
            return View(user);
        }

        [HttpPost]
        public ActionResult Delete()
        {
            CurrentUser = null;
            return RedirectToAction("Index", "Home");
        }
    }
}
