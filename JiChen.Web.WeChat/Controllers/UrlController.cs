using SuPerMarket.BLL.Cookie;
using SuPerMarket.BLL.MemberDB;
using SuPerMarket.BLL.SysDB;
using SuPerMarket.BLL.WeiXin;
using SuPerMarket.Core;
using SuPerMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuPerMarket.Web.WeChatControllers
{
    public class UrlController : Controller
    {
        public ActionResult Transfer()
        {

            string code = QueryString.SafeQNo("code"); 
            int state = QueryString.IntSafeQ("state");
            string behindfix= System.Web.HttpContext.Current.Request.Url.Query;
            if (state>0)
            {
                WeChatNavigationEntity nav = WeChatNavigationBLL.Instance.GetWeChatNavigation(state);
                behindfix = behindfix.Replace("state=" + state, "").Replace("code=","wechatcode=");
                //CookieBLL.SetWeiXinWebCode(code);
                if(nav.RedirectUrl.Contains("?"))
                {
                    behindfix = behindfix.Replace("?", "&");
                }
                return Redirect(nav.RedirectUrl + behindfix); 
            }
            //MemWeChatMsgEntity shortmsg = WeiXinJsSdk.Instance.GetWeChatShortInfo(code);
            //if(!string.IsNullOrEmpty(shortmsg.OpenId)&& !string.IsNullOrEmpty(shortmsg.UnionId))
            //{
            //    shortmsg.AppId = WeiXinConfig.GetAppId();
            //    shortmsg.Status =(int)WeChatStatus.ShortInfo;
            //    MemWeChatMsgBLL.Instance.GetMsgByAppUnionId(shortmsg.AppId, shortmsg.UnionId);
            //}
            return View();
        }

        public ActionResult AutoTrans()
        {
            string url= Request.Url.ToString(); 
            string code = QueryString.SafeQ("code");
            string state = QueryString.SafeQ("state");
            if (string.IsNullOrEmpty(code))
            {
                string appid = QueryString.SafeQ("appid"); 
                //string resulturl = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, appid, System.Web.HttpContext.Current.Server.UrlEncode(url), state);
                string resulturl = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, appid, System.Web.HttpContext.Current.Server.UrlEncode(url), state);
                return Redirect(resulturl);
            }
            else
            {
                string redirecturi = QueryString.SafeQNo("redirect_uri",500);
                Dictionary<string, string> dicparam = new Dictionary<string, string>();
                dicparam.Add("wechatcode", code);
                //dicparam.Add("state", state);
                if (!string.IsNullOrEmpty(redirecturi))
                {
                    string newurl = StringUtils.AppendParams(redirecturi, dicparam);
                    return Redirect(newurl);
                }
                return Redirect(SuPerMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MainMobileWebUrl);
            }
       
            //string code = QueryString.SafeQNo("code");
            //int state = QueryString.IntSafeQ("state");
            //string behindfix = System.Web.HttpContext.Current.Request.Url.Query;
            //if (state > 0)
            //{
            //    WeChatNavigationEntity nav = WeChatNavigationBLL.Instance.GetWeChatNavigation(state);
            //    behindfix = behindfix.Replace("state=" + state, "").Replace("code=", "wechatcode=");
            //    //CookieBLL.SetWeiXinWebCode(code);
            //    if (nav.RedirectUrl.Contains("?"))
            //    {
            //        behindfix = behindfix.Replace("?", "&");
            //    }
            //    return Redirect(nav.RedirectUrl + behindfix);
            //}
            //return View();
        }

    }
}