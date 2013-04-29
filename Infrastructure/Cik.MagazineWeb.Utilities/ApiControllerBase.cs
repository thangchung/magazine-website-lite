using System.Web.Http;
using System.Web.Mvc;
using Cik.MagazineWeb.Utilities.Configurations;
using Cik.MagazineWeb.Utilities.Extensions;

namespace Cik.MagazineWeb.Utilities
{
    public class ApiControllerBase : ApiController
    {
        protected readonly IConfigurationManager ConfigurationManager;

        protected ApiControllerBase()
            : this(DependencyResolver.Current.GetService<IConfigurationManager>())
        {
        }

        protected ApiControllerBase(IConfigurationManager configurationManager)
        {
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            ConfigurationManager = configurationManager;
        }

        protected int PageSize
        {

            get { return ConfigurationManager.GetAppConfigBy("PageSize").ConvertToInteger(); }
        }
    }
}