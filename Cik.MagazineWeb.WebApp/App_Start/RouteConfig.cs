using System.Web.Mvc;
using System.Web.Routing;

namespace Cik.MagazineWeb.WebApp.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Category",
                url: "{controller}/{action}/{name}/{id}",
                defaults: new { controller = "Home", action = "Category", name = UrlParameter.Optional, id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Item",
                url: "{controller}/{action}/{title}/{categoryId}/{itemId}",
                defaults: new { controller = "Home", action = "Details", title = UrlParameter.Optional, categoryId = UrlParameter.Optional, itemId = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}