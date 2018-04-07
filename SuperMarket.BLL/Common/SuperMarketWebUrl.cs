
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SuperMarket.Core;
using SuperMarket.BLL.WeiXin;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model;

namespace SuperMarket.BLL.Common
{
    /// <summary>
    /// IPAddress 的摘要说明
    /// </summary>
    public class SuperMarketWebUrl : System.Web.UI.Page
    {
        public static string GetSysSelectSiteIdUrl()
        {
            string returnurl = System.Web.HttpContext.Current.Request.Url.ToString();
            string redirecturl = System.Web.HttpContext.Current.Request.Url.Scheme+"://"+ System.Web.HttpContext.Current.Request.Url.Host + "/PageCommon/SiteSelect?returnUrl=" + System.Web.HttpContext.Current.Server.UrlEncode(returnurl);
            return redirecturl;
        }


        /// <summary>
        /// 通知供应商查看询价订单网址生成
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static string InquiryOrderUrl(string ordercode)
        {
            string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryWebUrl + string.Format(WebUrlEnum.InquiryCGMemNote, ordercode);
            return redirecturl;
        }
        /// <summary>
        /// 通知供应商查看询价订单网址 微信入口自动封装后的网址
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static string InquiryOrderSendWeChat(string ordercode)
        {
            string redirecturl = InquiryOrderUrl(ordercode);
            //int navid = WeChatNavigationBLL.Instance.GetIdByUrl(redirecturl);
            //string resulturl = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(redirecturl), "0");
            string resulturl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(redirecturl), "0");
            return resulturl;
        }
    }
}