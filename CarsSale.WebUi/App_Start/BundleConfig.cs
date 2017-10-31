using System.Web.Optimization;

namespace CarsSale.WebUi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/bootstrap")
                .Include("~/Content/Styles/bootstrap/bootstrap.css",
                    "~/Content/Styles/bootstrap/bootstrap-theme.css"));

            bundles.Add(new ScriptBundle("~/Scripts/bootstrap")
                .Include("~/Content/Scripts/src/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Content/Scripts/src/jquery-{version}.js",
                "~/Content/Scripts/src/jquery.validate.js",
                "~/Content/Scripts/src/jquery.maskedinput.js",
                "~/Content/Scripts/src/jquery.validate.unobtrusive.js",
                "~/Content/Scripts/src/jquery.unobtrusive-ajax.js",
                "~/Content/Scripts/src/jquery.validate.unobtrusive-ajax.js",
                "~/Content/Scripts/src/MicrosoftAjax.js",
                "~/Content/Scripts/src/MicrosoftMvcAjax.js"));
        }
    }
}