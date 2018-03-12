using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.LoginWebControllers
{
    public class BaseCommonController : Controller
    {
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        { 
      
            Response.AddHeader("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            Response.CacheControl = "no-cache";
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;
            base.OnActionExecuting(filterContext);
         

        }
        protected override void OnException(ExceptionContext filterContext)
        {
            if (Request.Url.Host == "localhost")
            {
                base.OnException(filterContext);
            }
            else
            {
                string errPage;
                Exception ex = filterContext.Exception;
                errPage = "/error.aspx";
                log4net.ILog log = log4net.LogManager.GetLogger("index"); 
                //记录错误日志
                if (ex != null)
                {
                    log.Error("basepageerror", new Exception(ex.ToString() + "\n\t\t" + ex.StackTrace + "\n\t\t" + this.Request.UserHostAddress + "\t\t\t" + this.Request.RawUrl));
                }
                else
                {
                    log.Error("未知错误!");
                }
                System.Web.HttpContext.Current.Response.Redirect(errPage);
            }
        }

       
    }
}
