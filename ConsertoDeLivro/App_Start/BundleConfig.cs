using System.Web.Optimization;

namespace ConsertoDeLivro.App_Start {
    public class BundleConfig {
        public static void RegisterBudles(BundleCollection bundles) {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.min.css",
                "~/Content/Site.css"
                ));

            bundles.Add(new ScriptBundle("~/bundles/js").Include(
                "~/Scripts/jquery-3.5.1.min.js",
                "~/Scripts/DataTables/jquery.dataTables.min.js",
                "~/Scripts/popper.js",
                "~/Scripts/bootstrap.js",
                "~/Scripts/bootbox.js"
                ));
        }
    }
}