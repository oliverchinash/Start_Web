using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.PayDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Pay.WeiXin;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class MobileOrderController : BaseMemberController
    {

        #region 首页
         
        /// <summary>
        /// 支付通道
        /// </summary>
        /// <returns></returns>
        public ActionResult Cashier()
        {
            string paycode = QueryString.SafeQ("code");

            VWPayOrderEntity _orderentity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            if (_orderentity.PayMethod == (int)PayType.OutLine)
            {
                return RedirectToAction("OrderNotice", new { code = paycode });
            }
            if (Globals.IsWeiXinDevice() && (_orderentity.PayMethod == (int)PayType.AliPay || _orderentity.PayMethod == (int)PayType.AliPayMobile))
            {
                return View();
            }
            if (_orderentity.Status == (int)OrderStatus.WaitPay)
            {
                PayEBankEntity _bank = new PayEBankEntity();
                //PayEBankEntity _bank = PayEBankBLL.Instance.GetPayEBankByPayType(_orderentity.PayType);
                 //PayTradeEntity _trade = PayTradeBLL.Instance.GetPayTradeByOrder(_orderentity.Code);
                //_trade.PayCode = _orderentity.Code.ToString();
                //_trade.OrderCode = _orderentity.Code;
                //_trade.OrderAmount = _orderentity.ActPrice;
                ////_trade.OrderAmount =0.03m;
                //bool savesuccess = false;
                //if (_trade.Id == 0)
                //{
                //if ((_trade.Id = PayTradeBLL.Instance.AddPayTrade(_trade)) > 0)
                //        savesuccess = true;
                //}
                //else if (PayTradeBLL.Instance.UpdatePayTrade(_trade) > 0)
                //{
                //    savesuccess = true;
                //}
                //if (savesuccess)
                //{
                    SuperMarket.Pay.PaymentRequest.Instance(((PayType)_orderentity.PayMethod).ToString()).SendRequest(_orderentity);//请求支付
                //}
            }
            else
            {
                Response.Write("11111");
            }
            return View();
        }

        public ActionResult OrderNotice()
        {

            string paycode = QueryString.SafeQ("code");
            VWPayOrderEntity _orderentity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            //long ordecode = QueryString.LongIntSafeQ("code");
            //VWOrderEntity _orderentity = OrderBLL.Instance.GetVWOrderByCode(ordecode);
            ViewBag.Order = _orderentity;
            return View();
        }
        public ActionResult OrderConfirm()
        {
            string paycode   = QueryString.SafeQ("code");
          
            int success = QueryString.IntSafeQ("s");//是否需要订单成功提醒
            int freshnum = QueryString.IntSafeQ("fn");//循环次数，超过10次自动关闭
            if (freshnum > 10)
            {
                return null;
            }
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            if(_payen.PayMethod==(int)PayType.WeChat)//微信支付
            {
                if (Globals.IsWeiXinDevice())
                {
                    string wechatcode = ""; 
                    wechatcode= QueryString.SafeQ("wechatcode");
                  
                    //是微信客户端走这边
                    if (string.IsNullOrEmpty(wechatcode))
                    {
                        //没有获取微信授权码
                        string url = Request.Url.ToString();
                        string redirecturl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(url), "0");
                        return Redirect(redirecturl);
                    }
                    else
                    {
                        //有微信授权码
                        JsApiPay jsApiPay = new JsApiPay();
                        try
                        { 
                        jsApiPay.GetOpenidAndAccessTokenFromCode(wechatcode);
                        }
                        catch(Exception ex)
                        {
                            //授权码过期
                            string baseurl;
                            Dictionary<string, string> nvc = new Dictionary<string, string>();
                            string oldurl = Request.Url.ToString();
                            StringUtils.ParseUrl(oldurl, out baseurl, out nvc);
                            nvc.Remove("wechatcode"); 
                            if(nvc.ContainsKey("fn"))
                            {
                                nvc["fn"] = (StringUtils.GetDbInt(nvc["fn"]) + 1).ToString();
                            }
                            else
                            { 
                                nvc.Add("fn", "1");
                            }
                            string url = baseurl + "?";
                            foreach (string key in nvc.Keys)
                            {
                                url += "&" + key + "=" + nvc[key];
                            }

                            string redirecturl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(url), "0");
                            return Redirect(redirecturl);
                        }
                        WxPayData paydata = jsApiPay.GetUnifiedOrderResult(_payen);
                        string wxJsApiParam = jsApiPay.GetJsApiParameters();//获取H5调起JS API参数     
                        ViewBag.WeiXinJsApiParam = wxJsApiParam;
                    }
                }
                else
                {
                    //网站类调出微信端支付通道 
                    DateTime dtnow = DateTime.Now;
                    NativePay nativePay = new NativePay();
                    WxPayData data = nativePay.GetPayUrl(_payen, "MWEB");//得到调用微信接口的路径
                    string url = data.GetValue("mweb_url").ToString();
                    return Redirect(url);
                }
            }
            ViewBag.PayOrderEntity= _payen;
            ViewBag.Success = success;
            return View();
        }

        public ActionResult XuQiuSuccess()
        {
            return View();
        }
        #endregion


    }
}
