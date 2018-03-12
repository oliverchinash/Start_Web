using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.Core;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.LoginWebControllers
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

            if (member == null ||  member.MemId == 0)
            {
                var loginurl = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl;
                if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
                {
                    loginurl = "/Home/Login";
                } 
                filterContext.Result = Redirect(loginurl+"?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString()));
            }
            else
            {
                memid = member.MemId;
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
