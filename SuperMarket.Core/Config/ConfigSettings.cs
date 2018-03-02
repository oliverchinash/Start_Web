using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core
{
    public class ConfigSettings
    {
        public static string GetLoginMobileWebUrl()
        {
            var loginurl = System.Configuration.ConfigurationManager.AppSettings["WebUrlLoginMobile"];
            if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
            {
                return "";
            }
            return loginurl;
        }
        public static string GetCaiGouMibileWebUrl()
        {
            var loginurl = System.Configuration.ConfigurationManager.AppSettings["WebUrlCGMobile"];
            if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
            {
                return "";
            }
            return loginurl;
        }
    }
}
