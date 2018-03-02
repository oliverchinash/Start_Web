using SuperMarket.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace SuperMarket.Core
{
    public class ConfigCore
    {
        private ConfigCore() { }//私有化配置构造函数
        private static ConfigCore instance = new ConfigCore();//私有化一个配置字段 饿汗式的单例模式
        public readonly string ConfigPath = HttpContext.Current != null ? HttpContext.Current.Server.MapPath("/JiChen.config") : System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "JiChen.config");
        public readonly string ConfigCommonPath = ConfigurationManager.AppSettings["JiChenConfigCommon"] ==null?"": ConfigurationManager.AppSettings["JiChenConfigCommon"].ToString();
        private static Dictionary<int, string> _ConfigPathDic = new Dictionary<int, string>();
        public static ConfigCore Instance//属性
        {
            get
            {
                return instance;
            }
        }

        public ConfigEntity ConfigEntity//属性
        {
            get
            {
                return this.ReadConfig();
            }
        }

        public ConfigCommonEntity ConfigCommonEntity//属性
        {
            get
            {
                return this.ReadConfigCommon();
            }
        }
        /// <summary>
        /// 获取配置文件信息(读)
        /// </summary>
        /// <returns></returns>
        public ConfigEntity ReadConfig()
        {

            object microConfigCache = HttpContext.Current != null ? HttpContext.Current.Cache["JiChenConfigCache"] : null;
            if (microConfigCache != null)
            {
                return (ConfigEntity)microConfigCache;
            }
            else
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigEntity));
                System.IO.Stream stream = new System.IO.FileStream(ConfigPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                object obj = xmlSerializer.Deserialize(stream);
                stream.Dispose();
                if (obj != null)
                {
                    if (HttpContext.Current != null)
                        HttpContext.Current.Cache.Insert("JiChenConfigCache", obj, new System.Web.Caching.CacheDependency(ConfigPath));
                    return (ConfigEntity)obj;
                }
                return null;
            }
        }

        /// <summary>
        /// 获取配置文件信息(读)
        /// </summary>
        /// <returns></returns>
        public ConfigCommonEntity ReadConfigCommon()
        {

            object microConfigCache = HttpContext.Current != null ? HttpContext.Current.Cache["JiChenConfigCommonCache"] : null;
            if (microConfigCache != null)
            {
                return (ConfigCommonEntity)microConfigCache;
            }
            else
            {
                if (!string.IsNullOrEmpty(ConfigCommonPath))
                {
                    System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigCommonEntity));
                    System.IO.Stream stream = new System.IO.FileStream(ConfigCommonPath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                    object obj = xmlSerializer.Deserialize(stream);
                    stream.Dispose();
                    if (obj != null)
                    {
                        if (HttpContext.Current != null)
                            HttpContext.Current.Cache.Insert("JiChenConfigCommonCache", obj, new System.Web.Caching.CacheDependency(ConfigCommonPath));
                        return (ConfigCommonEntity)obj;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// 修改配置文件信息(写)
        /// </summary>
        /// <returns></returns>
        public void WriteConfig(ConfigEntity config)
        {
            string MicroConfigPath = System.Web.HttpContext.Current.Server.MapPath("/JiChen.config");
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(ConfigEntity));
            System.IO.Stream stream = new System.IO.FileStream(MicroConfigPath, System.IO.FileMode.Open, System.IO.FileAccess.Write);
            xmlSerializer.Serialize(stream, config);
            stream.Dispose();
        }


        public string GetImagesSysPath(int code)
        {
            object microConfigCache = HttpContext.Current != null ? HttpContext.Current.Cache["JiChenImagePathCache"] : null;

            Dictionary<int, string> dic = new Dictionary<int, string>();
            if (microConfigCache != null)
            {
                dic = (Dictionary<int, string>)microConfigCache;
                if (dic.ContainsKey(code))
                    return dic[code];
            }
            else
            {
                ConfigCommonEntity config = ReadConfigCommon();
                if (config != null && config.FtpImagesSysPathList != null && config.FtpImagesSysPathList.Count > 0)
                {
                    if (HttpContext.Current != null)
                    {
                        foreach (FtpImagesSysPath entity in config.FtpImagesSysPathList)
                        {
                            dic.Add(entity.Code, entity.Path);
                        }
                        HttpContext.Current.Cache.Insert("JiChenImagePathCache", dic, new System.Web.Caching.CacheDependency(ConfigPath));
                        if (dic.ContainsKey(code))
                            return dic[code];
                    }
                    else
                    {
                        var path = config.FtpImagesSysPathList.Where(p => p.Code == code).FirstOrDefault();
                        if (path != null)
                        {
                            FtpImagesSysPath entity = (FtpImagesSysPath)path;
                            return entity.Path;
                        }
                    }
                }
            }
            return "";
        }
    }
}
