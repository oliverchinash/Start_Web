 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

using SuperMarket.Core.Util; 

namespace SuperMarket.Core.Safe
{
    public class FormString
    {
        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static int IntSafeQ(string key)
        {
            return IntSafeQ(key, 0);
        }

        /// <summary>
        /// 获取decimal类型值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static decimal DecimalSafeQ(string key)
        {
            return StringUtils.GetDbDecimal(HttpContext.Current.Request.Form[key],0);
        }

        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static int IntSafeQ(string key, int defaultvale)
        {
            return StringUtils.GetDbInt((HttpContext.Current.Request.Form[key]), defaultvale);
        }
        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static long LongIntSafeQ(string key)
        {
            return StringUtils.GetDbLong((HttpContext.Current.Request.Form[key]));
        }
        
        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static string SafeQ(string key)
        {
            return SafeQ(key, 0, 50);
        }

        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string SafeQ(string key, int len)
        {
             return SafeQ(key, 0, len);
        }
         
       /// <summary>
        /// 接收字符串转为String
       /// </summary>
        /// <param name="key">键值</param>
       /// <param name="type">是否转为小写 1转为小写</param>
       /// <param name="len">长度</param>
       /// <returns></returns>
        public static string SafeQ(string key, int type, int len)
        {
            string obj = StringUtils.GetDbString(HttpContext.Current.Request.Form[key]).Trim();
            if (obj.Length > len)
            {
                return "";
            }
            if (type == 1)
            {
                obj = obj.ToString().ToLower();
            }
            obj = StringUtils.SafeCode(obj);
            return obj;
        }
        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="type">是否转为小写 1转为小写</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string SafeQNo(string key , int len)
        {
            string obj = StringUtils.GetDbString(HttpContext.Current.Request.Form[key]).Trim();
            if (obj.Length > len)
            {
                return "";
            } 
            obj = StringUtils.SafeCodeSmall(obj);
            return obj;
        }
        /// <summary>
        /// 获取不为Null值的Request
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetNonNullRequest(string key)
        {
            if (HttpContext.Current.Request[key] != null)
            {
                return HttpContext.Current.Request[key].ToString();
            }

            return "";
        }

        /// <summary>
        /// 检查是否从服务器自身提交数据
        /// </summary>
        /// <returns></returns>
        public static bool CheckService()
        {
            string server_v1 = StringUtils.GetDbString(HttpContext.Current.Request.ServerVariables["HTTP_REFERER"]);
            string server_v2 = StringUtils.GetDbString(HttpContext.Current.Request.ServerVariables["SERVER_NAME"]);
            int changdu = server_v2.Length;
            return (server_v1.Length > 7 && server_v1.Substring(7, changdu) == server_v2);
        }
    }
}
