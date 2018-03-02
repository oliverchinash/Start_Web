
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using System.Net.NetworkInformation;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace SuperMarket.Core.Config
{
    /// <summary>
    /// IPAddress 的摘要说明
    /// </summary>
    public class IPAddress : System.Web.UI.Page
    {
        public static Int64 toDenaryIp(string ip)
        {
            Int64 _Int64 = 0;
            string _ip = ip;
            if (_ip.LastIndexOf(".") > -1)
            {
                string[] _iparray = _ip.Split('.');
                _Int64 = Int64.Parse(_iparray.GetValue(0).ToString()) * 256 * 256 * 256 + Int64.Parse(_iparray.GetValue(1).ToString()) * 256 * 256 + Int64.Parse(_iparray.GetValue(2).ToString()) * 256 + Int64.Parse(_iparray.GetValue(3).ToString()) - 1;
            }
            return _Int64;
        }
        /// <summary>
        /// /ip十进制
        /// </summary>
        public static long DenaryIp
        {
            get
            {
                long _Int64 = 0;
                string _ip = IP;
                if (_ip.LastIndexOf(".") > -1)
                {
                    string[] _iparray = _ip.Split('.');
                    _Int64 = Int64.Parse(_iparray.GetValue(0).ToString()) * 256 * 256 * 256 + Int64.Parse(_iparray.GetValue(1).ToString()) * 256 * 256 + Int64.Parse(_iparray.GetValue(2).ToString()) * 256 + Int64.Parse(_iparray.GetValue(3).ToString()) - 1;
                }
                return _Int64;
            }
        }
        public static string IP
        {
            get
            {
                string result = String.Empty;
                result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (result != null && result != String.Empty)
                {
                    //可能有代理
                    if (result.IndexOf(".") == -1) //没有"."肯定是非IPv4格式
                        result = null;
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有","，估计多个代理。取第一个不是内网的IP。
                            result = result.Replace(" ", "").Replace("", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (IsIPAddress(temparyip[i])
                                        && temparyip[i].Substring(0, 3) != "10."
                                        && temparyip[i].Substring(0, 7) != "192.168"
                                        && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i]; //找到不是内网的地址
                                }
                            }
                        }
                        else if (IsIPAddress(result)) //代理即是IP格式
                            return result;
                        else
                            result = null; //代理中的内容 非IP，取IP
                    }
                }
                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty)?  HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (null == result || result == String.Empty)
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                if (result == null || result == String.Empty)
                    result = HttpContext.Current.Request.UserHostAddress;
                if (result == "::1")
                {
                    result = "127.0.0.1";
                }
                return result;
            }
        }
        //是否ip格式
        public static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;
            string regformat = @"^\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}[\\.]\\d{1,3}$";
            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        ///<summary>
        /// 通过NetworkInterface读取网卡Mac
        ///</summary>
        ///<returns></returns>
        public static string  GetMacByNetworkInterface()
        {
            string macstr = "";
            List<string> macs = new List<string>();
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                macstr += ni.GetPhysicalAddress().ToString();
                macs.Add(ni.GetPhysicalAddress().ToString());
            }
            return macstr;
        }

        /// <summary>         
        /// 得到真实IP以及所在地详细信息（Porschev）         
        /// </summary>         
        /// <returns></returns>         
        public  static Dictionary<string, object> IPGetCity( )
            {
                try
                {
                    WebClient MyWebClient = new WebClient();
                    MyWebClient.Credentials = CredentialCache.DefaultCredentials;//获取或设置用于向Internet资源的请求进行身份验证的网络凭据  
                    Byte[] pageData = MyWebClient.DownloadData("http://ip.taobao.com/service/getIpInfo.php?ip=" + GetIP()); //从指定网站下载数据  
                    string pageHtml = Encoding.Default.GetString(pageData);
                    Dictionary<string, object> dic = Json.JsonJC.JsonToDictionary(pageHtml);
                    if (dic["code"].ToString() == "0")
                    {
                        Dictionary<string, object> dic_data = (Dictionary<string, object>)dic["data"];
                        return dic_data;
                    }
                }
                catch (WebException webEx)
                {
                }
                return null;
            }
        private static string GetIP()
        {
            string ip = string.Empty;
            if (!string.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',')[0]);
            if (string.IsNullOrEmpty(ip))
                ip = Convert.ToString(System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
            if (string.IsNullOrEmpty(ip))
                ip = System.Web.HttpContext.Current.Request.UserHostAddress;
            return ip;
        }
        /// <summary>
        /// 获取HTML源码信息(Porschev)
        /// </summary>
        /// <param name="url">获取地址</param>
        /// <returns>HTML源码</returns>
        public static string GetHtml(string url)
        {
            string str = "";
            try
            {
                Uri uri = new Uri(url);
                WebRequest wr = WebRequest.Create(uri);
                Stream s = wr.GetResponse().GetResponseStream();
                StreamReader sr = new StreamReader(s, Encoding.Default);
                str = sr.ReadToEnd();
            }
            catch (Exception e)
            {
            }
            return str;
        }
    }
}