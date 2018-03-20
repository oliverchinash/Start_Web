using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.Core;
using SuperMarket.Core.Safe;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Web.CommonControllers
{ 
    /// <summary>
   /// 终端用户
   /// </summary>
    public class BaseMemberController : BaseCommonController
    {
        protected MemberLoginEntity member = null; 
        protected int memid =0;

        public BaseMemberController()
        {

        } 
        protected override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            member = CookieBLL.GetLoginCookie();

            if (member == null || member.MemId == 0)
            {
                var loginurl = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl;
                if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
                {
                    loginurl = "/Home/Login";
                }
                loginurl = loginurl.TrimEnd('/');
                string wechatcode = QueryString.SafeQ("wechatcode");
                string returnurl = System.Web.HttpContext.Current.Request.Url.ToString();
                string redirecturl = loginurl + "?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(returnurl);
                if (!string.IsNullOrEmpty(wechatcode))
                {
                    returnurl = returnurl.Replace("wechatcode=" + wechatcode, "");
                    redirecturl = loginurl + "?wechatcode=" + wechatcode + "&returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(returnurl);
                }
                filterContext.Result = Redirect(redirecturl);
            } 
            else
            {
                memid = member.MemId;
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
