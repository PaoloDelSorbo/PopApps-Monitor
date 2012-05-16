using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Collections.Specialized;

namespace PopApps.Monitor.Helpers
{
    public static class ConfigHelper
    {
        public static string Get(string configName)
        {
            return AppSettings[configName];
        }

        static NameValueCollection AppSettings = ConfigurationManager.AppSettings;

        public static string CdnUrl { get { return Get("CdnUrl"); } }

    }
}