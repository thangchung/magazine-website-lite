using System.Collections.Generic;
using System.Reflection;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Cik.MagazineWeb.Init;

namespace Cik.MagazineWeb.WebApp.App_Start
{
    public class WebAppConfig
    {
        public static void Config()
        {
            var webAssembly = typeof (WebAppConfig).Assembly;
            var relatedAssemblies = new List<Assembly> {typeof (CoreModule).Assembly};

            DependencyResolverInitializer.Initialize(webAssembly, relatedAssemblies);

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfig.CustomizeConfig(GlobalConfiguration.Configuration);
        }
    }
}