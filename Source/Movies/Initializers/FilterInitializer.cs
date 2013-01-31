namespace Movies.Initializers
{
    using System.Web.Mvc;

    public class FilterInitializer
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}