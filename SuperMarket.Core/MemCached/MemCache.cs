using SuperMarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core
{
    public class MemCache
    {
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        public static object GetCache(string cachekey)
        {
            try
            {
                cachekey=SetCacheKey(cachekey);
                object obj = MemcachedProviders.Cache.DistCache.Get(cachekey);
                return obj; 
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        public static bool AddCache(string cachekey, object value)
        {
            cachekey = SetCacheKey(cachekey);
           return  MemcachedProviders.Cache.DistCache.Add(cachekey, value,true);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cachekey"></param>
        /// <param name="value"></param>
        /// <param name="seconds">单位：秒</param>
        public static void AddCache(string cachekey, object value, int seconds)
        {
            cachekey = SetCacheKey(cachekey);
            MemcachedProviders.Cache.DistCache.Add(cachekey, value, seconds * 1000);
        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        public static void RemoveCache(string cachekey)
        {
            cachekey = SetCacheKey(cachekey);
            MemcachedProviders.Cache.DistCache.Remove(cachekey);

        }
        /// <summary>
        /// 清除Cookie
        /// </summary>
        public static void RemoveAllCache()
        {           
            MemcachedProviders.Cache.DistCache.RemoveAll();
        }
        /// <summary>
        /// 设置KEY值
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        public static string SetCacheKey(string cachekey)
        {

            cachekey = cachekey.Replace(" ", "").ToLower();
            cachekey = cachekey.Replace(".", "");
            cachekey = cachekey.Replace("|", "");
            cachekey = cachekey.Replace("'", "");
            cachekey = cachekey.Replace(",", "");
            if (cachekey.Length > 250)
            {
                cachekey = cachekey.Substring(0,240);
            }

            cachekey = StringUtils.GBKUrlEncode(cachekey);
            return cachekey;

        }
      
    }
}
