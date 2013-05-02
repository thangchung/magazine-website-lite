using System.Web.Mvc;
using Cik.MagazineWeb.Utilities.Configurations;
using Cik.MagazineWeb.Utilities.Extensions;

namespace Cik.MagazineWeb.Utilities
{
    public class ControllerBase : Controller
    {
         protected readonly IConfigurationManager ConfigurationManager;

        protected ControllerBase()
            : this(DependencyResolver.Current.GetService<IConfigurationManager>())
        {
        }

        protected ControllerBase(IConfigurationManager configurationManager)
        {
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            ConfigurationManager = configurationManager;
        }

        protected int PageSize
        {

            get { return ConfigurationManager.GetAppConfigBy("PageSize").ConvertToInteger(); }
        }

        public int NumOfItemOnHomePage
        {
            get { return ConfigurationManager.GetAppConfigBy("NumOfItemOnHomePage").ConvertToInteger(); }
        }
    }
}