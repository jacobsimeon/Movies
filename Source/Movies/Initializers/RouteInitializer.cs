namespace Movies.Initializers
{
    using System.Web.Mvc;
    using System.Web.Routing;

    public class RouteInitializer
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Ratings",
                url: "Movies/{id}/Rating",
                defaults: new { controller = "Ratings", action="Edit" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}