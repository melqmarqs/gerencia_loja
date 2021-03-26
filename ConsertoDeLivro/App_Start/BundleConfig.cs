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
                "~/Scripts/jquery-{version}.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.js"
                ));

            bundles.Add(new StyleBundle("~/FormLogin/css").Include(
                "~/Content/FormLogin.css",
                "~/Content/bootstrap.min.css"
                ));

            bundles.Add(new ScriptBundle("~/bumbleblee/js").Include(
                "~/Scripts/bumbleblee.js"
                ));

            bundles.Add(new ScriptBundle("~/cep/js").Include(
                "~/Scripts/cep.js"
                ));
        }
    }
}