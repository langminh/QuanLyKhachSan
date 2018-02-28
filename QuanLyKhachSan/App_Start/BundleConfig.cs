using System.Web;
using System.Web.Optimization;

namespace QuanLyKhachSan
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-2.1.3.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));


            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/js").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/npm.js"));

            bundles.Add(new StyleBundle("~/css").Include(
               //"~/Content/site.css",
               //"~/Content/BasicStyle.css",
               //"~/Content/HtmlStyle.css",
               //"~/Content/LayoutStyle.css",
               "~/Content/css/bootstrap.css",
               "~/Content/css/bootstrap-theme.css",
               "~/Content/css/datepicker.css"
               ));
        }
    }
}
