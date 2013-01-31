namespace Movies
{
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using FluentValidation.Mvc;

    using Movies.Initializers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterInitializer.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteInitializer.RegisterRoutes(RouteTable.Routes);
            BundleInitializer.RegisterBundles(BundleTable.Bundles);
            IocInitializer.InitializeObjectFactory();
            ControllerBuilder.Current.SetControllerFactory(typeof(StructureMapControllerFactory));
        }
    }
}
