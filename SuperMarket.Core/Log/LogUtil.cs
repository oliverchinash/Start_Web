using System;
using System.Text;
using System.Web;

namespace SuperMarket.Core
{
    /// <summary>
    /// 日志实体信息
    /// </summary>
    public class LogUtil
    {
        /// <summary>
        /// 启动log4net组件（配置位于web.config中）
        /// </summary>
        public static void StartLog4Net()
        {
            log4net.Config.XmlConfigurator.Configure();

            //log4net.Config.XmlConfigurator.Configure(
            //    new System.IO.FileInfo(
            //        System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Configuration\BaseConfig\Log4net.config")));
        }

        public static void Log(string title, string logMsg)
        {
            log4net.ILog log = log4net.LogManager.GetLogger("index");
            //记录错误日志
            log.Error(title, new Exception(logMsg + "\n\t\trrrrr\t\t\t"));
  
            Log(title, logMsg, "index");
        }
        /// <summary>
        /// 写日志
        /// </summary>
        /// <param name="title">标题（如产生错误的方法名）</param>
        /// <param name="logMsg">内容</param>
        /// <param name="loggerName">配置文件的日志名</param>
        public static void Log(string title, string logMsg, string loggerName = "index")
        {
            var entity = new LogUtil { Title = title, LogMsg = logMsg };

            log4net.ILog log = log4net.LogManager.GetLogger(loggerName);

            //记录错误日志
            if (HttpContext.Current != null)
            {
                try
                {
                    entity.LogMsg += entity.Spilt + Sys.Misc.IPAddress + entity.Spilt + Util.StringUtils.GetDbString(HttpContext.Current.Request.UrlReferrer) + entity.Spilt + HttpContext.Current.Request.RawUrl;
                }
                catch
                {
                }
            }

            log.Error(entity);
        }

        public static void Log(Exception exception)
        {
            Log(exception, "index", null);
        }
        public static void Log(Exception exception, string loggerName = "index", string title = null)
        {
            log4net.ILog log = log4net.LogManager.GetLogger(loggerName);

            if (exception == null)
            {
                var errentity = new LogUtil { Title = "写入日志失败！", LogMsg = "参数exception为null！" };
                log.Error(errentity);
                return;
            }

            var entity = new LogUtil { Title = string.IsNullOrWhiteSpace(title) ? exception.Message : title };
            var sb = new StringBuilder();
            sb.Append(BuildMessage(exception));

            var innerExc = exception.InnerException;
            string spilt = "\t";
            while (innerExc != null)
            {
                sb.Append(BuildMessage(innerExc, spilt));
                innerExc = innerExc.InnerException;
                spilt += "\t";
            }

            entity.LogMsg = sb.ToString();

            //记录错误日志
            if (HttpContext.Current != null)
            {
                try
                {
                    entity.LogMsg += entity.Spilt + Sys.Misc.IPAddress + entity.Spilt + Util.StringUtils.GetDbString(HttpContext.Current.Request.UrlReferrer) + entity.Spilt + HttpContext.Current.Request.RawUrl;
                }
                catch
                {
                }
            }

            log.Error(entity);
        }

        public static void LogForWcf(string title, string logMsg)
        {
            LogForWcf(title, logMsg, "index");
        }
        /// <summary>
        /// 写日志WCF
        /// </summary>
        /// <param name="title">标题（如产生错误的方法名）</param>
        /// <param name="logMsg">内容</param>
        /// <param name="loggerName">配置文件的日志名</param>
        public static void LogForWcf(string title, string logMsg, string loggerName = "index")
        {
            var entity = new LogUtil { Title = title, LogMsg = logMsg };

            log4net.ILog log = log4net.LogManager.GetLogger(loggerName);
            //记录错误日志
            log.Error(entity);
        }

        public static void LogForWcf(Exception exception)
        {
            LogForWcf(exception, "index", null);
        }
        /// <summary>
        /// 写日志WCF
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="loggerName"></param>
        /// <param name="title"></param>
        public static void LogForWcf(Exception exception, string loggerName = "index", string title = null)
        {
            Log(exception, loggerName, title);
        }
 
        #region
        /// <summary>
        /// 错误标题（一般为出错的方法名）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 详细错误信息
        /// </summary>
        public string LogMsg { get; set; }

        private string spilt;

        /// <summary>
        /// 日志各项之间的连接符
        /// </summary>
        public string Spilt
        {
            get
            {
                if (string.IsNullOrEmpty(spilt))
                {
                    spilt = "\t";
                }

                return spilt;
            }

            set
            {
                spilt = value;
            }
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(Spilt))
            {
                Spilt = ",";
            }

            var sb = new StringBuilder();

            //sb.Append(string.Format("{1}{0}{2}{0}{3}", Spilt, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), Title, LogMsg));
            sb.Append(string.Format("Title={0},LogMsg={1}", Title, LogMsg));

            return sb.ToString();
        }
        #endregion

        private static string BuildMessage(Exception exception, string spilt = null)
        {
            var sb = new StringBuilder();
            sb.Append(spilt);
            sb.Append("Message:");
            sb.Append(spilt);
            sb.Append(exception.Message);
            sb.Append(Environment.NewLine);
            sb.Append(spilt);
            sb.Append("TargetSite:");
            sb.Append(spilt);
            sb.Append(exception.TargetSite);
            sb.Append(Environment.NewLine);
            sb.Append(spilt);
            sb.Append("Data:");
            sb.Append(spilt);
            sb.Append(exception.Data);
            sb.Append(Environment.NewLine);
            sb.Append(spilt);
            sb.Append("StackTrace:");
            sb.Append(spilt);
            sb.Append(exception.StackTrace);
            sb.Append(Environment.NewLine);
            return sb.ToString();
        }
    }
}
