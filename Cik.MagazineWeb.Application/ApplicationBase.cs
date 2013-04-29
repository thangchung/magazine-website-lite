using System.Web.Mvc;
using Cik.MagazineWeb.Utilities;
using Cik.MagazineWeb.Utilities.Configurations;

namespace Cik.MagazineWeb.Application
{
    public abstract class ApplicationBase
    {
        protected readonly IConfigurationManager ConfigurationManager;

        protected ApplicationBase()
            : this(DependencyResolver.Current.GetService<IConfigurationManager>())
        {
        }

        protected ApplicationBase(IConfigurationManager configurationManager)
        {
            Guard.ArgumentNotNull(configurationManager, "ConfigurationManager");

            ConfigurationManager = configurationManager;
        }
    }
}