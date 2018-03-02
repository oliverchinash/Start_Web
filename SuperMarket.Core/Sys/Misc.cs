using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Globalization;
using System.Net;

using System.Text.RegularExpressions;
 

namespace SuperMarket.Core.Sys
{
    public class Misc
    {
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string IPAddress
        {
            get
            {
                string userHostAddress;
                HttpRequest request = HttpContext.Current.Request;
                if (request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
                {
                    userHostAddress = request.ServerVariables["REMOTE_ADDR"];
                }
                else
                {
                    userHostAddress = request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                }
                if (string.IsNullOrEmpty(userHostAddress))
                {
                    userHostAddress = request.UserHostAddress;
                }
                return userHostAddress;
            }
        }

        public string GetIpAddr()
        {
            HttpRequest request = HttpContext.Current.Request;
            string ip = request.Headers["x-forwarded-for"];

            if (ip == null || ip.Count() == 0 || ip.ToLower().Equals("unknown"))
            {
                ip = request.Headers["Proxy-Client-IP"];
            }

            if (ip == null || ip.Count() == 0 || ip.ToLower().Equals("unknown"))
            {
                ip = request.Headers["WL-Proxy-Client-IP"];
            }

            if (ip == null || ip.Count() == 0 || ip.ToLower().Equals("unknown"))
            {
                ip = request.ServerVariables["REMOTE_ADDR"];
            }

            if (ip == null || ip.Count() == 0 || ip.ToLower().Equals("unknown"))
            {
                ip = request.UserHostAddress;
            }

            return ip;
        }


        /// <summary>
        /// 站点URL
        /// </summary>
        public static string WebUrl
        {
            get
            {
                int port = HttpContext.Current.Request.Url.Port;
                if (port == 443)
                {
                    port = 80;
                }
                return "http://" + HttpContext.Current.Request.Url.Host + (port == 80 ? "" : ":" + HttpContext.Current.Request.Url.Port.ToString()) + ApplicationPath + "/";
            }
        }
        /// <summary>
        /// 获取虚拟目录
        /// </summary>
        public static string ApplicationPath
        {
            get
            {
                string applicationPath = "/";
                if (HttpContext.Current != null)
                {
                    applicationPath = HttpContext.Current.Request.ApplicationPath;
                }
                if (applicationPath == "/")
                {
                    return string.Empty;
                }
                return applicationPath.ToLower(CultureInfo.InvariantCulture);
            }
        }

       
 
    }
}
