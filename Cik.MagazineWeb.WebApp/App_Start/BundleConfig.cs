using System;
using System.Web.Optimization;

namespace Cik.MagazineWeb.WebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/jquery")
                .Include("~/scripts/jquery-{version}.js"));

            // Modernizr goes separate since it loads first
            bundles.Add(new ScriptBundle("~/bundles/modernizr")
                .Include("~/Scripts/modernizr-{version}.js"));

            bundles.Add(
                new ScriptBundle("~/andia-agency/scripts")
                    .Include("~/andia-agency/assets/js/jquery-1.8.2.min.js")
                    .Include("~/andia-agency/assets/bootstrap/js/bootstrap.min.js")
                    .Include("~/andia-agency/assets/js/jquery.flexslider.js")
                    .Include("~/andia-agency/assets/js/jquery.tweet.js")
                    .Include("~/andia-agency/assets/js/jflickrfeed.js")
                    .Include("~/andia-agency/assets/js/jquery.ui.map.min.js")
                    .Include("~/andia-agency/assets/js/jquery.quicksand.js")
                    .Include("~/andia-agency/assets/prettyPhoto/js/jquery.prettyPhoto.js")
                    .Include("~/andia-agency/assets/js/scripts.js"));

            bundles.Add(
              new ScriptBundle("~/scripts/vendor")
                .Include("~/scripts/jquery-{version}.js")
                .Include("~/scripts/jquery.blockUI.js")
                .Include("~/scripts/knockout-{version}.debug.js")
                .Include("~/scripts/sammy-{version}.js")
                .Include("~/scripts/toastr.js")
                .Include("~/scripts/Q.js")
                .Include("~/scripts/breeze.debug.js")
                .Include("~/scripts/bootstrap.js")
                .Include("~/scripts/moment.js")
                .Include("~/scripts/knockout.simpleGrid.1.3.js")
                .Include("~/scripts/jquery.tmpl.js")
                .Include("~/scripts/utilities.js")
              );

            bundles.Add(
                new ScriptBundle("~/andia-agency/css")
                .Include("~/andia-agency/assets/bootstrap/css/bootstrap.min.css")
                .Include("~/andia-agency/assets/prettyPhoto/css/prettyPhoto.css")
                .Include("~/andia-agency/assets/css/flexslider.css")
                .Include("~/andia-agency/assets/css/font-awesome.css")
                .Include("~/andia-agency/assets/css/style.css"));

            bundles.Add(
              new StyleBundle("~/Content/css")
                .Include("~/Content/ie10mobile.css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/bootstrap-responsive.css")
                .Include("~/Content/durandal.css")
                .Include("~/Content/toastr.css")
                .Include("~/Content/app.css")
                // .Include("~/Content/style.css")
              );
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            if (ignoreList == null)
            {
                throw new ArgumentNullException("ignoreList");
            }

            ignoreList.Ignore("*.intellisense.js");
            ignoreList.Ignore("*-vsdoc.js");

            //ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            //ignoreList.Ignore("*.min.js", OptimizationMode.WhenDisabled);
            //ignoreList.Ignore("*.min.css", OptimizationMode.WhenDisabled);
        }
    }
}