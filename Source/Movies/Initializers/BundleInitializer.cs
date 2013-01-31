namespace Movies.Initializers
{
    using System.Web.Optimization;

    public class BundleInitializer
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/jquery-1.7.2.js",
                "~/Scripts/bootstrap.js"));

			bundles.Add(new StyleBundle("~/Content/bootstrap") .Include(
                "~/Content/bootstrap.css",
                "~/Content/app.css",
                "~/Content/bootstrap-responsive.css"));
        }
    }
}