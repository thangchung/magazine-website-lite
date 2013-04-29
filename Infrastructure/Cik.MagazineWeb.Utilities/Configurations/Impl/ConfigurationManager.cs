﻿using System.Collections.Specialized;
using System.Configuration;

namespace Cik.MagazineWeb.Utilities.Configurations.Impl
{
    public class ConfigurationManager : IConfigurationManager
    {
        public object GetSection(string sectionName)
        {
            return System.Configuration.ConfigurationManager.GetSection(sectionName);
        }

        public ConnectionStringSettingsCollection GetConnectionStrings()
        {
            return  System.Configuration.ConfigurationManager.ConnectionStrings;
        }

        public NameValueCollection GetAppSettings()
        {
            return System.Configuration.ConfigurationManager.AppSettings;
        }

        public string GetAppConfigBy(string appConfigName)
        {
            return GetAppSettings()[appConfigName];
        }
    }
}