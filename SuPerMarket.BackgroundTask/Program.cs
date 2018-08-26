using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Topshelf;

namespace SuPerMarket.BackgroundTask
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary> 
        static void Main()
        {
            ComPressPic.ComPressProductPic();

            //注册log4net
            //log4net.Config.XmlConfigurator.Configure(
            //   new System.IO.FileInfo(AppDomain.CurrentDomain.BaseDirectory + "log4net.config")
            //);

            //HostFactory.Run(x =>
            //{
            //    x.UseLog4Net();
            //    x.Service<ServiceRunner>();
            //    x.SetDescription("新市场计划任务");
            //    x.SetDisplayName("新市场计划任务");
            //    x.SetServiceName("ComPressPicJob");
            //    x.EnablePauseAndContinue();
            //});
        }
    }
}
