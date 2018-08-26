using SuperMarket.BLL.Cookie;
using SuperMarket.Core;
using SuperMarket.Model.Account; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuPerMarket.Web.WeChatControllers
{
   public class BaseMemberController: BaseCommonController
    {
        protected MemberLoginEntity member = null; 
        protected int memid =0;

        public BaseMemberController()
        {

        }

        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            member = CookieBLL.GetLoginCookie();

            //if (member == null ||  member.MemId == 0)
            //{
            //    filterContext.Result = Redirect("/Home/Login?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.RawUrl));
            //}
            if (member == null || member.MemId == 0)
            {
                var loginurl =  ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl;
                if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
                {
                    loginurl = "/Home/Login";
                }
                loginurl = loginurl.TrimEnd('/');
                filterContext.Result = Redirect(loginurl + "?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString()));
            }
            else if (member.IsSupplier == 1)
            {
                var loginurl = ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl;
                if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
                {
                    loginurl = "";
                }
                loginurl = loginurl.TrimEnd('/') + "/Home/HasNoAuth";
                filterContext.Result = Redirect(loginurl + "?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString()));
            }
            else
            {
                memid = member.MemId;
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
