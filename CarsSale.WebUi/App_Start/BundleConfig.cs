using System.Web.Optimization;

namespace CarsSale.WebUi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                "~/Content/Styles/bootstrap/bootstrap.css",
                "~/Content/Styles/bootstrap/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap").Include(
                "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.maskedinput.js",
                "~/Scripts/jquery.validate.unobtrusive.js"));
        }
    }
}