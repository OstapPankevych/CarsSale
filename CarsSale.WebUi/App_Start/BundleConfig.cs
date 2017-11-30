using System.Web.Optimization;

namespace CarsSale.WebUi
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery/js").Include(
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/jquery-{version}.intellisense.js",
                "~/Scripts/jquery.mask.js",
                "~/Scripts/jquery.inputmask.bundle.js",
               "~/Scripts/jquery.validate.js",
               "~/Scripts/jquery.validate.unobtrusive.js"
               ));

            bundles.Add(new ScriptBundle("~/bundles/jquery-validate/js").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js")
                .Include("~/Scripts/umd/popper.js",
                    "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/bundles/bootstrap/css")
                .Include("~/Content/bootstrap.css",
                    "~/Content/bootstrap-grid.css",
                    "~/Content/bootstrap-reboot.css"));
        }
    }
}