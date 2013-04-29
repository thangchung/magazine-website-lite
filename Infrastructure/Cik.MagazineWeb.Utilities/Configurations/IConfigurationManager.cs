using System.Collections.Specialized;
using System.Configuration;

namespace Cik.MagazineWeb.Utilities.Configurations
{
    public interface IConfigurationManager
    {
        object GetSection(string sectionName);

        ConnectionStringSettingsCollection GetConnectionStrings();

        NameValueCollection GetAppSettings();

        string GetAppConfigBy(string appConfigName); 
    }
}