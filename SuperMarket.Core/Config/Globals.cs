namespace SuperMarket.Core
{
    using System;
    using System.Collections;
    using System.Globalization;
    using System.Net.Mail;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;
    using System.Net;
    using System.IO;
    using Util;

    public sealed class Globals
    { 
        /// <summary>
        /// 组织URL字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="querystring"></param>
        /// <returns></returns>
        public static string AppendQuerystring(string url, string querystring)
        {
            return AppendQuerystring(url, querystring, false);
        }
        /// <summary>
        /// 组织URL字符串
        /// </summary>
        /// <param name="url"></param>
        /// <param name="querystring"></param>
        /// <returns></returns>
        public static string AppendQuerystring(string url, string querystring, bool urlEncoded)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            string str = "?";
            if (url.IndexOf('?') > -1)
            {
                if (!urlEncoded)
                {
                    str = "&";
                }
                else
                {
                    str = "&amp;";
                }
            }
            return (url + str + querystring);
        }
        /// <summary>
        /// 获取完整URL地址
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        public static string FullPath(string local)
        {
            if (string.IsNullOrEmpty(local))
            {
                return local;
            }
            if (local.ToLower(CultureInfo.InvariantCulture).StartsWith("http://"))
            {
                return local;
            }
            if (HttpContext.Current == null)
            {
                return local;
            }
            return FullPath(HostPath(HttpContext.Current.Request.Url), local);
        }
        /// <summary>
        /// 获取完整URL地址
        /// </summary>
        /// <param name="local"></param>
        /// <returns></returns>
        public static string FullPath(string hostPath, string local)
        {
            return (hostPath + local);
        }
        /// <summary>
        /// 站点URL
        /// </summary>
        static public string WebUrl
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
        /// 获取模板路径
        /// </summary>
        public static string GetSkinPath()
        {
            ConfigEntity molaConfig = ConfigCore.Instance.ConfigEntity;
            return (ApplicationPath + "/Themes/" + molaConfig.WebName).ToLower(CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// 获取模板路径
        /// </summary>
        public static string SkinPath
        {
            get
            { 
                return GetSkinPath();
            }
        }
        
        /// <summary>
        /// 站点ID
        /// </summary>
        public static int WebSiteId
        {
            get
            {
                return ConfigCore.Instance.ConfigEntity.SiteId;
            }
        } 

        /// <summary>
        /// 获取URL端口
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static string HostPath(Uri uri)
        {
            if (uri == null)
            {
                return string.Empty;
            }
            string str = (uri.Port == 80) ? string.Empty : (":" + uri.Port.ToString(CultureInfo.InvariantCulture));
            return string.Format(CultureInfo.InvariantCulture, "{0}://{1}{2}", new object[] { uri.Scheme, uri.Host, str });
        }
        /// <summary>
        /// HTML解码
        /// </summary>
        /// <param name="textToFormat"></param>
        /// <returns></returns>
        public static string HtmlDecode(string textToFormat)
        {
            if (string.IsNullOrEmpty(textToFormat))
            {
                return textToFormat;
            }
            return HttpUtility.HtmlDecode(textToFormat);
        }
        /// <summary>
        /// HTML编码
        /// </summary>
        /// <param name="textToFormat"></param>
        /// <returns></returns>
        public static string HtmlEncode(string textToFormat)
        {
            if (string.IsNullOrEmpty(textToFormat))
            {
                return textToFormat;
            }
            return HttpUtility.HtmlEncode(textToFormat);
        }
        /// <summary>
        /// 跳转到SSL地址
        /// </summary>
        /// <param name="context"></param>
        public static void RedirectToSSL(HttpContext context)
        {
            if ((context != null) && !context.Request.IsSecureConnection)
            {
                Uri url = context.Request.Url;
                context.Response.Redirect("https://" + url.ToString().Substring(7));
            }
        }
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        /*public static string GetIp()
        {
            String srcIp = HttpContext.Current.Request.Headers["Cdn-Src-Ip"];

            if (srcIp == null)
            {
                srcIp = HttpContext.Current.Request.UserHostAddress; 
            }
            return srcIp;
        }*/
        /// <summary>
        /// 获取IP
        /// </summary>
        /// <returns></returns>
        public static string GetIp()
        {
            try
            {
                if (System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    return System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(new char[] { ',' })[0];
                else
                    return System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
            catch
            {
                return "";
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strToStrip"></param>
        /// <returns></returns>
        public static string StripAllTags(string strToStrip)
        {
            strToStrip = Regex.Replace(strToStrip, @"</p(?:\s*)>(?:\s*)<p(?:\s*)>", "\n\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, @"<br(?:\s*)/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = Regex.Replace(strToStrip, "\"", "''", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            strToStrip = StripHtmlXmlTags(strToStrip);
            return strToStrip;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripForPreview(string content)
        {
            content = Regex.Replace(content, "<br>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br/>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<br />", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "<p>", "\n", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            content = content.Replace("'", "&#39;");
            return StripHtmlXmlTags(content);
        }
        /// <summary>
        /// 替换HTML标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripHtmlXmlTags(string content)
        {
            return Regex.Replace(content, "<[^>]+>", "", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
        /// <summary>
        /// 替换SCRIPT标签
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static string StripScriptTags(string content)
        {
            content = Regex.Replace(content, "<script((.|\n)*?)</script>", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            content = Regex.Replace(content, "'javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
            return Regex.Replace(content, "\"javascript:", "", RegexOptions.Multiline | RegexOptions.IgnoreCase);
        }
        /// <summary>
        ///  
        /// </summary>
        /// <param name="formattedPost"></param>
        /// <returns></returns>
        public static string UnHtmlEncode(string formattedPost)
        {
            RegexOptions options = RegexOptions.Compiled | RegexOptions.IgnoreCase;
            formattedPost = Regex.Replace(formattedPost, "&quot;", "\"", options);
            formattedPost = Regex.Replace(formattedPost, "&lt;", "<", options);
            formattedPost = Regex.Replace(formattedPost, "&gt;", ">", options);
            return formattedPost;
        }
        /// <summary>
        /// UTF8解码
        /// </summary>
        /// <param name="urlToDecode"></param>
        /// <returns></returns>
        public static string UrlDecode(string urlToDecode)
        {
            if (string.IsNullOrEmpty(urlToDecode))
            {
                return urlToDecode;
            }
            return HttpUtility.UrlDecode(urlToDecode, Encoding.UTF8);
        }
        /// <summary>
        /// UTF8编码
        /// </summary>
        /// <param name="urlToEncode"></param>
        /// <returns></returns>
        public static string UrlEncode(string urlToEncode)
        {
            if (string.IsNullOrEmpty(urlToEncode))
            {
                return urlToEncode;
            }
            return HttpUtility.UrlEncode(urlToEncode, Encoding.UTF8);
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
        /// <summary>
        /// 获取域名
        /// </summary>
        public static string DomainName
        {
            get
            {
                if (HttpContext.Current == null)
                {
                    return string.Empty;
                }
                return HttpContext.Current.Request.Url.Host;
            }
        }
        /// <summary>
        /// 获取IP地址
        /// </summary>
        public static string IPAddress
        {
            get
            {
                string userHostAddress;
                HttpRequest request =HttpContext.Current.Request;
                if (request.ServerVariables["HTTP_X_FORWARDED_FOR"]==null)
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
        /// <summary>
        /// 站点图片地址
        /// </summary>
        static public string PictureUrl
        {
            get
            {
                return ConfigCore.Instance.ConfigCommonEntity.PictureUrl;                
            }

        }
  
      
        public static string GenerateLinkStyle(string kitInd)
        {
            return (kitInd == "False" || kitInd == "0") ? "p-" : "k-";
        }

        /// <summary>
        /// 对字符串进行加密
        /// </summary>
        /// <param name="str">需要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string GetMD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;            
        }
        /// <summary>
        /// 获取URL内容
        /// </summary>
        /// <param name="ContentURL"></param>
        /// <returns></returns>
        public static string GetContent(string ContentURL)
        {
            try
            {
                //Encoding enc = Encoding.UTF8;
                Encoding enc = Encoding.Default;
                Uri uri = new Uri(ContentURL);

                HttpWebRequest hwreq = (HttpWebRequest)WebRequest.Create(uri);
                HttpWebResponse hwrsp = (HttpWebResponse)hwreq.GetResponse();

                byte[] bts = new byte[(int)hwrsp.ContentLength];
                Stream s = hwrsp.GetResponseStream();
                for (int i = 0; i < bts.Length; )
                {
                    i += s.Read(bts, i, bts.Length - i);
                }
                string content = enc.GetString(bts);
                return content;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// 数据库加密串
        /// </summary>
        /// <param name="sqlstr"></param>
        /// <returns></returns>
        public static string Open_Master_key(string sqlstr)
        {//OPEN MASTER KEY DECRYPTION BY PASSWORD = 'wallace';
            sqlstr = "OPEN SYMMETRIC KEY mbskey_cusdata DECRYPTION BY CERTIFICATE cert_mbs;" + sqlstr;
            sqlstr += ";CLOSE SYMMETRIC KEY mbskey_cusdata";
            return sqlstr;        
        }

        public static string FromatStr_NEncryptByKey(string str)
        {
            return "EncryptByKey(Key_GUID('mbskey_cusdata'), N'" + str + "')";
        }

    
         
        public static string GetRegValue(string content, string reg)
        {
            Match m = Regex.Match(content, reg, RegexOptions.Singleline);
            if (m.Success)
            {
                return m.Result("${content}").Trim().Replace("\n", "\r\n").Replace("\r\r", "\r");
            }
            else
            {
                return "";
            }
        }

      
        public static Boolean IsMobileDevice()
        { 
            string[] mobileAgents = {"micromessenger", "iphone", "android", "windowsphone","sonyericsson","mot", "htc", "j2me",  "ipod","phone", "mobile", "wap", "netfront", "java", "opera mobi", "opera mini", "ucweb", "windows ce", "symbian", "series","webos", "sony", "blackberry", "dopod", "nokia", "samsung", "palmsource", "xda", "pieplus", "meizu", "midp", "cldc", "motorola", "foma","docomo", "up.browser", "up.link", "blazer", "helio", "hosin", "huawei", "novarra", "coolpad", "webos", "techfaith", "palmsource", "alcatel","amoi", "ktouch", "nexian", "ericsson", "philips", "sagem", "wellcom", "bunjalloo", "maui", "smartphone", "iemobile", "spice", "bird", "zte-", "longcos", "pantech", "gionee", "portalmmm", "jig browser", "hiptop", "benq", "haier", "^lct", "320x320", "240x320", "176x220",  "w3c ", "acs-", "alav", "alca", "amoi", "audi", "avan", "benq", "bird", "blac", "blaz", "brew", "cell", "cldc", "cmd-", "dang", "doco", "eric", "hipt", "inno", "ipaq", "java", "jigs", "kddi", "keji", "leno","lg", "lg-c", "lg-d", "lg-g", "lge-", "maui", "maxo", "midp", "mits","mmef", "mobi", "mot-", "moto", "mwbp", "nec-", "newt", "noki", "oper", "palm", "pana", "pant", "phil", "play", "port", "prox", "qwap", "sage", "sams", "sany", "sch-", "sec-", "send", "seri", "sgh-", "shar", "sie-", "siem", "smal", "smar", "sony", "sph-", "symb", "t-mo","teli", "tim-", "tosh", "tsm-", "upg1", "upsi", "vk-v", "voda", "wap-", "wapa", "wapi", "wapp", "wapr", "webc", "winw", "winw", "xda", "xda-", "Googlebot-Mobile" };
            bool isMoblie = false;
            if (HttpContext.Current.Request.UserAgent.ToString().ToLower() != null)
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (HttpContext.Current.Request.UserAgent.ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static Boolean IsWeiXinDevice()
        {
            string[] mobileAgents = { "micromessenger"};
            bool isMoblie = false;
            if (HttpContext.Current.Request.UserAgent.ToString().ToLower() != null)
            {
                for (int i = 0; i < mobileAgents.Length; i++)
                {
                    if (HttpContext.Current.Request.UserAgent.ToString().ToLower().IndexOf(mobileAgents[i]) >= 0)
                    {
                        isMoblie = true;
                        break;
                    }
                }
            }
            if (isMoblie)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

