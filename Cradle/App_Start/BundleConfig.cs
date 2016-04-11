using System.Web;
using System.Web.Optimization;

namespace Cradle
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/font-awesome.css",
                      "~/Content/bootstrap-social.css",
                      "~/Content/bootstrap-formhelpers.css"
                      ));

            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                    "~/Scripts/register.js"
                ));
            bundles.Add(new ScriptBundle("~/bundles/formhelpers").Include(
                    "~/Scripts/bootstrap-formhelpers.js",
                    "~/Scripts/bootstrap-formhelpers-countries.js",
                    "~/Scripts/bootstrap-formhelpers-datepicker.js",
                    "~/Scripts/bootstrap-formhelpers-phone.js",
                    "~/Scripts/bootstrap-formhelpers-selectbox.js"
                ));
        }
    }
}
