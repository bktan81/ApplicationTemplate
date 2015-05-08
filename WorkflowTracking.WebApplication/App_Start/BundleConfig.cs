using System.Web;
using System.Web.Optimization;

namespace WorkflowTracking.WebApplication
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
                      "~/Content/site.css"));

            /////////////////////////////////////////////
            //http://www.telerik.com/forums/kendo-projects-and-mvc-bundling

            bundles.Add(new ScriptBundle("~/bundles/kendo")
         .Include("~/Scripts/kendo.web.min.js")
         .Include("~/Scripts/kendo/cultures/kendo.culture.en-US.min.js")
         .Include("~/Scripts/kendo/cultures/kendo.culture.zh-CN.min.js")
    );


            bundles.Add(new ScriptBundle("~/bundles/blockUI")
                     .Include("~/Scripts/jquery.blockUI.min.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/knockout")
                     .Include("~/Scripts/knockout-3.1.0.js")
                     .Include("~/Scripts/knockout.mapping-latest.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/sammy")
                    .Include("~/Scripts/sammy-0.7.4.js")
                );

            ////////////

            bundles.Add(new ScriptBundle("~/bundles/login")
                .Include("~/Scripts/jquery-1.9.1.min.js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.blockUI.min.js")
                .Include("~/Scripts/jquery-ui-1.10.4.min.js")
                .Include("~/Scripts/gApp.js")
                .Include("~/Scripts/AppCode.js")
                 .Include("~/Scripts/kendo/cultures/kendo.culture.en-US.min.js")
                .Include("~/Scripts/kendo/cultures/kendo.culture.zh-CN.min.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/main")
                .Include("~/Scripts/jquery-1.9.1.min.js")
                .Include("~/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.blockUI.min.js")
                .Include("~/Scripts/kendo.web.min.js")
                .Include("~/Scripts/kendo/cultures/kendo.culture.en-US.min.js")
                .Include("~/Scripts/kendo/cultures/kendo.culture.zh-CN.min.js")
                .Include("~/Scripts/knockout-3.1.0.js")
                .Include("~/Scripts/knockout.mapping-latest.js")
                .Include("~/Scripts/sammy-0.7.4.js")
                .Include("~/Scripts/gApp.js")
                .Include("~/Scripts/AppCode.js")
            );

            bundles.Add(new ScriptBundle("~/bundles/lang")
              .Include("~/Scripts/cultures/langResx.js")
              .Include("~/Scripts/cultures/langResx.zh-CN.js")
          );

            ////////////

            bundles.Add(new StyleBundle("~/Content/kendo/css")

      .Include("~/Content/kendo/kendo.common.min.css")

      .Include("~/Content/kendo/kendo.default.min.css")

    );





            //////////////////////////////////////////////

            // Clear all items from the default ignore list to allow minified CSS and JavaScript files to be included in debug mode
            bundles.IgnoreList.Clear();

            // Add back the default ignore list rules sans the ones which affect minified files and debug mode
            bundles.IgnoreList.Ignore("*.intellisense.js");
            bundles.IgnoreList.Ignore("*-vsdoc.js");
            bundles.IgnoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);






            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            BundleTable.EnableOptimizations = true;
        }
    }
}
