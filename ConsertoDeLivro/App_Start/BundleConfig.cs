using System.Web.Optimization;

namespace ConsertoDeLivro.App_Start {
    public class BundleConfig {
        public static void RegisterBudles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/modernizr-{version}.js",
                "~/Scripts/jquery-{version}.slim.min.js",
                "~/Scripts/jquery-{version}.min.js",
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/bootbox.js",
                "~/Scripts/popper.min.js"
                ));
        }
    }
}