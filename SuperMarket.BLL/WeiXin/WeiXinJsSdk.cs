using SuperMarket.Core;
using SuperMarket.Core.WebService;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Model;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Core.Json;

namespace SuperMarket.BLL.WeiXin
{
    public class WeiXinJsSdk
    {
        #region 实例化
        public static WeiXinJsSdk Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly WeiXinJsSdk instance = new WeiXinJsSdk();
        }
        public string GetAccessToken(bool cache=false)
        {
            string token = "";
            string _cachekey = "GetWeChatAccessToken";
           
            object obj = MemCache.GetCache(_cachekey);
            //从缓存获取
            if(cache&&obj != null)
            {
                WeChatTokenLogEntity log = (WeChatTokenLogEntity)obj;
                if (log.EndTime > DateTime.Now)
                {
                    token = log.AccessToken;
                }
            }
            //从数据库获取
            if (token == "")
            {
                string appid = WeiXinConfig.GetAppId();
                WeChatTokenLogEntity log = WeChatTokenLogBLL.Instance.GetTokenByAppid(appid);
                if (log != null && log.Id > 0 && log.EndTime > DateTime.Now)
                {
                    token = log.AccessToken;
                    TimeSpan ts1 = log.EndTime - DateTime.Now;
                    int tsSen = ts1.Seconds;
                    if(cache)
                    MemCache.AddCache(_cachekey, log, tsSen);
                }
                if (token == "")
                {
                    string result = WebServiceClient.QueryGetWebService(string.Format(WeiXinConfig.URL_FORMAT_TOKEN, WeiXinConfig.GetAppId(), WeiXinConfig.GetAppSecret()), "", null);
                    JavaScriptSerializer serializer = new JavaScriptSerializer();
                    Dictionary<string, object> jsonObj = serializer.Deserialize<dynamic>(result);
                    if (jsonObj.ContainsKey("access_token"))
                    {
                        token = jsonObj["access_token"].ToString();
                        WeChatTokenLogEntity tokenlog = new WeChatTokenLogEntity();
                        tokenlog.Appid = appid;
                        tokenlog.CreateTime = DateTime.Now;
                        tokenlog.EndTime = DateTime.Now.AddSeconds(7000);
                        tokenlog.AccessToken = token;
                        WeChatTokenLogBLL.Instance.AddWeChatTokenLog(tokenlog);
                        if (cache)
                            MemCache.AddCache(_cachekey, tokenlog, 7000);
                     }
                    else
                    {
                        token = "";
                     }
                }
            }
            
            return token;
        }


        public void RemoveAccessToken()
        { 
            string _cachekey = "GetWeChatAccessToken"; 
            MemCache.RemoveCache(_cachekey);
            string appid = WeiXinConfig.GetAppId();
            WeChatTokenLogBLL.Instance.RemoveTokenByAppid(appid); 
        }

        public string SendOrderMsg()
        {
            return "";
        //    string url = string.Format(WeiXinConfig.URL_FORMAT_SendMsg, GetAccessToken());
        //    Hashtable param = new Hashtable();
        //    param.Add("",)
        //    WebServiceClient.QueryPostWebService(url,);

        //    {
        //        "touser":"OPENID",
        //   "template_id":"ngqIpbwh8bUfcSsECmogfXcV14J0tQlEpBO27izEYtY",
        //   "url":"http://weixin.qq.com/download",  
        //   "miniprogram":{
        //            "appid":"xiaochengxuappid12345",
        //     "pagepath":"index?foo=bar"
        //   },          
        //   "data":{
        //            "first": {
        //                "value":"恭喜你购买成功！",
        //               "color":"#173177"
        //            },
        //           "keynote1":{
        //                "value":"巧克力",
        //               "color":"#173177"
        //           },
        //           "keynote2": {
        //                "value":"39.8元",
        //               "color":"#173177"
        //           },
        //           "keynote3": {
        //                "value":"2014年9月22日",
        //               "color":"#173177"
        //           },
        //           "remark":{
        //                "value":"欢迎再次购买！",
        //               "color":"#173177"
        //           }
        //        }
        //    }
         }
        /// <summary>
        /// 获取用户基本信息
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public MemWeChatMsgEntity GetWeChatShortInfo(string code)
        {
            MemWeChatMsgEntity entity = new MemWeChatMsgEntity();
            string simpleurl = WeiXinConfig.GetSimpleInfoUrl(code);
              string result=  WebServiceClient.QueryGetWebService(simpleurl, "", null);
             JavaScriptSerializer serializer = new JavaScriptSerializer();
            Dictionary<string, object> jsonObj = serializer.Deserialize<dynamic>(result);
            if (jsonObj.ContainsKey("unionid"))
            {
                entity.UnionId = jsonObj["unionid"].ToString();  
            }
            if (jsonObj.ContainsKey("openid"))
            {
                entity.OpenId = jsonObj["openid"].ToString();
            }
            if (jsonObj.ContainsKey("access_token"))
            {
                entity.Access_Token = jsonObj["access_token"].ToString();
            }
            if (jsonObj.ContainsKey("refresh_token"))
            {
                entity.Refresh_Token = jsonObj["refresh_token"].ToString();
            }
            
            return entity;
        }
        /// <summary>
        /// 发送微信通知
        /// </summary>
        /// <param name="WeChatUnionId"></param>
        /// <param name="redirecturl"></param>
        /// <param name="templetid"></param>
        /// <param name="hashen"></param>
        /// <returns></returns>

        public bool SendWeiXinMsgNote(string WeChatUnionId, string redirecturl, string templetid, Hashtable hashen)
        {
            bool success = false;
            //获取链接导航Id
            //int navid = WeChatNavigationBLL.Instance.GetIdByUrl(redirecturl); 
            MemWeChatMsgEntity wecharmsg = MemWeChatMsgBLL.Instance.GetMsgByAppUnionId(WeiXinConfig.GetAppId(), WeChatUnionId);
            if (wecharmsg != null && !string.IsNullOrEmpty(wecharmsg.OpenId))
            {
                WeiXinSendMsgEntity send = new WeiXinSendMsgEntity();
                send.touser = wecharmsg.OpenId;
                send.template_id = templetid;
                string resulturl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(redirecturl), "0");
                send.url = resulturl;
                send.data = hashen;
                string json = JsonJC.ObjectToJson(send);
                string url = string.Format(WeiXinConfig.URL_FORMAT_SendMsg, WeiXinJsSdk.Instance.GetAccessToken(false));
                ///发送微信备案
                WeChatMsgEntity msg = new WeChatMsgEntity();
                msg.ParamStr = json;
                msg.WeChatOpenId = wecharmsg.OpenId;
                msg.RedirectUrl = redirecturl;
                msg.WeChatUrl = url;
                msg.TemplateIid = templetid;
                msg.Id = WeChatMsgBLL.Instance.AddWeChatMsg(msg);
                string result = WebServiceClient.QueryPostWebServiceJson(url, json);
                WeiXinFailEntity resulten = JsonJC.JsonToObject<WeiXinFailEntity>(result);
                msg.Result = result;
                WeChatMsgBLL.Instance.UpdateWeChatMsg(msg);
                if (resulten.errmsg.ToLower() == "ok")
                {
                    success = true;
                }
                else if( resulten.errcode=="40001"&& resulten.errmsg.Contains("access_token is invalid or not latest hint"))
                {
                    ///发现accesstoken过期后再次发送
                    WeiXinJsSdk.Instance.RemoveAccessToken();
                    url = string.Format(WeiXinConfig.URL_FORMAT_SendMsg, WeiXinJsSdk.Instance.GetAccessToken(false));
                    ///发送微信备案  
                    msg.WeChatUrl = url; 
                    msg.Result = "";
                    msg.Id = 0;
                    msg.Id = WeChatMsgBLL.Instance.AddWeChatMsg(msg);
                    result = WebServiceClient.QueryPostWebServiceJson(url, json);
                    resulten = JsonJC.JsonToObject<WeiXinFailEntity>(result);
                    msg.Result = result;
                    WeChatMsgBLL.Instance.UpdateWeChatMsg(msg);
                    if (resulten.errmsg.ToLower() == "ok")
                    {
                        success = true;
                    }
                    else
                    {
                        //需发邮件提醒  
                        EmailSendBLL.Instance.WeiXinSendFail(WeChatUnionId, redirecturl);
                    }
                }
                else
                {
                    //需发邮件提醒  
                    EmailSendBLL.Instance.WeiXinSendFail(WeChatUnionId, redirecturl);
                }
            }
            return success;
        }

        public bool SendWeiXinCustomerNote(WeiXinCustomerEntity customer)
        {
            bool success = false;
            string url = string.Format(WeiXinConfig.Customer_Format_Send, WeiXinJsSdk.Instance.GetAccessToken(false));

            string json = JsonJC.ObjectToJson(customer);
            string   result = WebServiceClient.QueryPostWebServiceJson(url, json);
            WeiXinFailEntity resulten = JsonJC.JsonToObject<WeiXinFailEntity>(result);
            ///发送微信备案
            WeChatMsgEntity msg = new WeChatMsgEntity();
            msg.ParamStr = json;
            msg.WeChatOpenId ="";
            msg.RedirectUrl = "";
            msg.WeChatUrl = url;
            msg.TemplateIid = "";
            msg.Result = result;
            msg.Id = WeChatMsgBLL.Instance.AddWeChatMsg(msg);
            if (resulten.errmsg.ToLower() == "ok")
            {
                success = true;
            }
            return success;
        }
        #endregion
    }
}
