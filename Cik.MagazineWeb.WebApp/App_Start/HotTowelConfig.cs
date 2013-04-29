using System.Web.Optimization;

[assembly: WebActivator.PostApplicationStartMethod(
    typeof(Cik.MagazineWeb.WebApp.App_Start.HotTowelConfig), "PreStart")]

namespace Cik.MagazineWeb.WebApp.App_Start
{
    public static class HotTowelConfig
    {
        public static void PreStart()
        {
            // Add your start logic here
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}