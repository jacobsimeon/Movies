using System.Web.Mvc;

namespace Movies.Controllers
{
    using System;
    using System.Globalization;
    using System.Web;

    using Movies.DataAccess;
    using Movies.Models;

    public class MmdbController : Controller
    {
        private const string CurrentUserCookieName = "CurrentUser";
        protected IDataContext DataContext;

        public MmdbController(IDataContext ctx)
        {
            DataContext = ctx;
        }

        private User _CurrentUser;
        public User CurrentUser
        {
            get
            {
                if (_CurrentUser == null)
                {
                    var cookie = HttpContext.Request.Cookies[CurrentUserCookieName];
                    if(cookie != null)
                    {
                        int id;
                        var didParse = int.TryParse(cookie.Value, out id);
                        if (didParse)
                        {
                            _CurrentUser = DataContext.Users.Find(id);
                        }
                    }
                }
                return _CurrentUser;
            }
            set
            {
                if (value == null)
                {
                    if (Response.Cookies[CurrentUserCookieName] != null)
                    {
                        Response.Cookies[CurrentUserCookieName].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies.Remove(CurrentUserCookieName);
                    }
                    if (Request.Cookies[CurrentUserCookieName] != null)
                    {
                        Request.Cookies[CurrentUserCookieName].Expires = DateTime.Now.AddDays(-1);
                        Request.Cookies.Remove(CurrentUserCookieName);
                    }
                }
                else
                {
                    var cookie = Request.Cookies[CurrentUserCookieName] ?? new HttpCookie(CurrentUserCookieName);
                    cookie.Value = value.Id.ToString(CultureInfo.InvariantCulture);
                    cookie.Expires = DateTime.Now.AddMonths(1);
                    Response.Cookies.Add(cookie);
                }

                _CurrentUser = value;
            }
        }

        public bool IsUserLoggedIn
        {
            get
            {
                return CurrentUser != null;
            }
        }
    }
}
