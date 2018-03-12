using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.BLL.WeiXin
{
    public  class WeiXinConfig
    {
        #region 微信支付
        public const string URL_Pay_InWeiXin = "http://paysdk.weixin.qq.com/example/JsApiPayPage.aspx?openid={0}&total_fee={1}";

        #endregion
        /// <summary>
        /// 微信导航转接中枢
        /// </summary>
        public const string URL_WeiXin_Redirect = "http://wechat.aahama.com/Url/AutoTrans?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
        /// <summary>
        /// 客服接口-发消息
        /// </summary>
        public const string Customer_Format_Send = "https://api.weixin.qq.com/cgi-bin/message/custom/send?access_token={0}";
        /// <summary>
        /// 获取token接口
        /// </summary>
        public const string URL_FORMAT_TOKEN = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={0}&secret={1}";
        /// <summary>
        /// TICKET
        /// </summary>
        public const string URL_FORMAT_TICKET = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?access_token={0}&type=jsapi";
        /// <summary>
        /// 发送微信消息
        /// </summary>                            
        public const string URL_FORMAT_SendMsg = "https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={0}";
        /// <summary>
        /// 用户端发起的url
        /// </summary>
        public const string URL_FORMAT_KHRedirect = "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope=snsapi_base&state={2}#wechat_redirect";
        /// <summary>
        /// 根据用户端code获取UnionId等简易信息
        /// </summary>
        public const string URL_FORMAT_SimpleInfo = "https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code";
        /// <summary>
        /// 根据用户端code获取UnionId等简易信息
        /// </summary>
        public const string URL_FORMAT_FullInfo = "https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}&lang=zh_CN";

        public static string GetAppId()
        {
            string appidstr = "";
            var appid = System.Configuration.ConfigurationManager.AppSettings["WeChatAppId"];
            if (appid != null)
                appidstr = appid.ToString();
            return appidstr;
        }
        public static string GetAppSecret()
        {
            string appidstr = "";
            var appid = System.Configuration.ConfigurationManager.AppSettings["WeChatAppSecret"];
            if (appid != null)
                appidstr = appid.ToString();
            return appidstr;
        }
        /// <summary>
        /// 获取用户端发起的URL，给用户点击后自动登录
        /// </summary>
        /// <param name="url">间接访问网址</param>
        /// <param name="method">访问的模块（根据此字段设定最终访问网址）</param>
        /// <returns></returns>
        public static string GetRedirectUrl(string url)
        {
            return string.Format(URL_WeiXin_Redirect, GetAppId(), url);
        }
        /// <summary>
        /// 获取简易个人信息的接口网址
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetSimpleInfoUrl( string code)
        {
            return string.Format(URL_FORMAT_SimpleInfo, GetAppId(), GetAppSecret(), code);
        }
        /// <summary>
        /// 获取详情信息的接口
        /// </summary>
        /// <param name="webaccesstoken"></param>
        /// <param name="openid"></param>
        /// <returns></returns>
        public static string GetFullInfoUrl(string webaccesstoken,string openid)
        {
            return string.Format(URL_FORMAT_FullInfo, webaccesstoken, openid);
        }


    }
}
