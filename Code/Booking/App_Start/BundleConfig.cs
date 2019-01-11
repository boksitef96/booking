using System.Web;
using System.Web.Optimization;

namespace Booking
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery-ui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/signalR").Include(
                        "~/Scripts/jquery.signalR-{version}.min.js",
                        "~/Scripts/jquery.signalR-{version}.js",
                        "~/signalR/hubs"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/reservation").Include(
                       "~/Scripts/Reservation/reservation.js"));

            bundles.Add(new ScriptBundle("~/bundles/accomodation").Include(
                  "~/Scripts/Accomodation/accomodationMap.js",
                  "~/Scripts/Accomodation/accomodationWeather.js"
                  ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/jquery-ui.css",
                      "~/Content/site.css",
                      "~/Content/style.css"
                      ));
        }
    }
}
