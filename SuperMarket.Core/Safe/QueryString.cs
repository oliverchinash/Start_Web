
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using SuperMarket.Core.Util;

namespace  SuperMarket.Core.Safe
{
    public class QueryString
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
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static int IntSafeQ(string key, int defaultvale)
        {
            return StringUtils.GetDbInt((HttpContext.Current.Request.QueryString[key]), defaultvale);
        }
        /// <summary>
        /// 接收字符串并转为Int型
        /// </summary>
        /// <param name="key">键值</param>
        /// <returns></returns>
        public static Int64 LongIntSafeQ(string key)
        {
            return StringUtils.GetDbLong((HttpContext.Current.Request.QueryString[key]) );

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
        public static string SafeQNo(string key)
        {
            return SafeQNo(key, 0, 50);
        }
        /// <summary>
        /// 接收字符串转为String
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="type">是否转为小写 1转为小写</param>
        /// <param name="len">长度</param>
        /// <returns></returns>
        public static string SafeQNo(string key, int len)
        {
            string obj = StringUtils.GetDbString(HttpContext.Current.Request.QueryString[key]).Trim();
            if (obj.Length > len)
            {
                return "";
            }
            obj = StringUtils.SafeCodeSmall(obj);
            return obj;
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
            string obj = StringUtils.GetDbString(HttpContext.Current.Request.QueryString[key]).Trim();
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
        public static string SafeQNo(string key, int type, int len)
        {
            string obj = StringUtils.GetDbString(HttpContext.Current.Request.QueryString[key]).Trim();
            if (obj.Length > len)
            {
                return "";
            }
            if (type == 1)
            {
                obj = obj.ToString().ToLower();
            }
            obj = StringUtils.SafeCodeSmall(obj);
            return obj;
        }
    }
}
