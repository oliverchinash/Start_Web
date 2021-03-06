﻿using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Core;
using SuperMarket.Core.Cookie;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SuperMarket.Web.CommonControllers
{
    /// <summary>
    /// 系统管理员
    /// </summary>
    public class BaseMemAdminController : BaseCommonController
    {
        protected MemberLoginEntity member = null;
        protected int memid = 0; 
        public BaseMemAdminController()
        {
            //CookieUtil.SetCookie(SysCookieName.SysTemType, ((int)SystemType.B2BBehind).ToString());
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
            else if (member.IsSysUser != 1)
            {
                var loginurl = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl;
                if (loginurl == null || string.IsNullOrEmpty(loginurl.ToString()))
                {
                    loginurl = "";
                }
                loginurl = loginurl.TrimEnd('/') + "/Home/HasNoAuth";
                filterContext.Result = Redirect(loginurl + "?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(System.Web.HttpContext.Current.Request.Url.ToString()));
            }
            else
            { 
                base.OnActionExecuting(filterContext);
            }
        }
    }
}
