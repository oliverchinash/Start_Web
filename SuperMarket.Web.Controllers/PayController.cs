using SuperMarket.BLL;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.PayDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Pay;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class PayController : BaseCommonController
    {

        /// <summary>
        /// 阿里支付通知
        /// </summary>
        public void PayAliPayNotify()
        {
            SortedDictionary<string, string> sPara = GetRequestPost();
            LogUtil.Log("订单付款回调接口,订单号：", "");

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                alipayNotify aliNotify = new alipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);//验证消息是否是支付宝发出的合法消息

                if (verifyResult)//验证成功
                {
                    LogUtil.Log("订单付款回调成功：", "");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"];
                    //支付宝交易号 
                    string trade_no = Request.Form["trade_no"];
                    LogUtil.Log("订单付款回调成功：", out_trade_no);
                    LogUtil.Log("订单付款回调成功：", trade_no);
                    //交易状态
                    //&trade_no = 2016101021001004470296721955
                    string trade_status = Request.Form["trade_status"];//
                    string buyer_email = Request.Form["buyer_email"];//lgzh306%40126.com
                    string buyer_id = Request.Form["buyer_id"];//2088602184028472
                    string exterface = Request.Form["exterface"];//create_direct_pay_by_user
                    string is_success = Request.Form["is_success"];//T
                    string notify_id = Request.Form["notify_id"];//RqPnCoPT3K9%252Fvwbh3InWeOSzrvky7IAV3JJOfdsL5TFWKbTMC8BGq%252Bm99WAaYWgnOmS6
                    string notify_time = Request.Form["notify_time"];//2016-10-10+15%3A48%3A34
                    string notify_type = Request.Form["notify_type"];//trade_status_sync
                    string payment_type = Request.Form["payment_type"];//1
                    string seller_email = Request.Form["seller_email"];//20718505%40qq.com
                    string seller_id = Request.Form["seller_id"];//2088421564177650
                    string subject = Request.Form["subject"];//111
                    string total_fee = Request.Form["total_fee"];//0.01
                    PayAliResultEntity _entity = new PayAliResultEntity();
                    _entity.Buyeremail = buyer_email;
                    _entity.Buyerid = buyer_id;
                    _entity.CreateTime = DateTime.Now;
                    _entity.HasDeal = 0;
                    _entity.Issuccess = is_success;
                    _entity.Notifytime = notify_time;
                    _entity.Notifytype = notify_type;
                    _entity.Outtradeno = out_trade_no;
                    _entity.Paymenttype = payment_type;
                    _entity.Selleremail = seller_email;
                    _entity.Sellerid = seller_id;
                    _entity.Subject = subject;
                    _entity.Totalfee = total_fee;
                    _entity.Tradeno = trade_no;
                    _entity.Tradestatus = trade_status;
                    LogUtil.Log("订单付款回调成功：", JsonJC.ObjectToJson(_entity));
                    
                    PayAliResultBLL.Instance.AddPayAliResult(_entity);

                    //http://pay.ddbbqp.com/return_url.aspx?buyer_email=lgzh306%40126.com&buyer_id=2088602184028472&exterface=create_direct_pay_by_user&is_success=T&notify_id=RqPnCoPT3K9%252Fvwbh3InWeOSzrvky7IAV3JJOfdsL5TFWKbTMC8BGq%252Bm99WAaYWgnOmS6&notify_time=2016-10-10+15%3A48%3A34&notify_type=trade_status_sync&out_trade_no=test20161010092017&payment_type=1&seller_email=20718505%40qq.com&seller_id=2088421564177650&subject=111&total_fee=0.01&trade_no=2016101021001004470296721955&trade_status=TRADE_SUCCESS&sign=4d4f0c35e95f4dce7c34c1ff70e321fd&sign_type=MD5

                    LogUtil.Log("订单付款回调成功：", "222222");
                    if (Request.Form["trade_status"] == "TRADE_FINISHED" || Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                         LogUtil.Log("订单付款回调成功：", "33333333333");
                        //判断该笔订单是否在商户网站中已经做过处理
                        VWOrderEntity _order = OrderBLL.Instance.GetVWOrderByCode(StringUtils.GetDbLong(out_trade_no));
                        if (_order.Status == (int)OrderStatus.WaitPay)
                        {

                            LogUtil.Log("订单付款回调成功：", "44444444");
                            AssetReChargeEntity _asset = new AssetReChargeEntity();
                            _asset.Amt = StringUtils.GetDbDecimal(_entity.Totalfee);
                            _asset.BankCode = buyer_id;
                            _asset.CardNo = buyer_email;
                            _asset.TranSerialNum = trade_no;
                            _asset.CreateTime = DateTime.Now;
                            _asset.MemId = _order.MemId;
                            _asset.IpAddress = IPAddress.IP;
                            _asset.PayType = 1;

                            LogUtil.Log("订单付款回调成功：", "555555");
                            if (OrderBLL.Instance.PayFinished(StringUtils.GetDbLong(out_trade_no), _asset) > 0)
                            {
                                EmailSendBLL.Instance.OrderRemind(out_trade_no);
                                LogUtil.Log("订单付款成功,订单号：", out_trade_no.ToString());
                            }
                            else
                            {
                                LogUtil.Log("订单付款成功更新失败,订单号：", out_trade_no.ToString());
                            }
                        }
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                    }
                    else
                    {
                        LogUtil.Log("订单付款成功更新失败,订单号：", out_trade_no.ToString() + " 订单状态：" + Request.QueryString["trade_status"]);
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else
                {
                    LogUtil.Log("订单付款支付验证失败", "111");
                }
            }
        }

        /// <summary>
        /// 阿里支付结果
        /// </summary>
        /// <returns></returns>
        public ActionResult PayAliPayResult()
        {

            SortedDictionary<string, string> sPara = GetRequestGet();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                alipayNotify aliNotify = new alipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.QueryString["notify_id"], Request.QueryString["sign"]);//验证消息是否是支付宝发出的合法消息

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中页面跳转同步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.QueryString["out_trade_no"];
                    //支付宝交易号 
                    string trade_no = Request.QueryString["trade_no"];
                    //交易状态 
                    string trade_status = Request.QueryString["trade_status"];//
                    string buyer_email = Request.QueryString["buyer_email"];//lgzh306%40126.com
                    string buyer_id = Request.QueryString["buyer_id"];//2088602184028472
                    string exterface = Request.QueryString["exterface"];//create_direct_pay_by_user
                    string is_success = Request.QueryString["is_success"];//T
                    string notify_id = Request.QueryString["notify_id"];//RqPnCoPT3K9%252Fvwbh3InWeOSzrvky7IAV3JJOfdsL5TFWKbTMC8BGq%252Bm99WAaYWgnOmS6
                    string notify_time = Request.QueryString["notify_time"];//2016-10-10+15%3A48%3A34
                    string notify_type = Request.QueryString["notify_type"];//trade_status_sync
                    string payment_type = Request.QueryString["payment_type"];//1
                    string seller_email = Request.QueryString["seller_email"];//20718505%40qq.com
                    string seller_id = Request.QueryString["seller_id"];//2088421564177650
                    string subject = Request.QueryString["subject"];//111
                    string total_fee = Request.QueryString["total_fee"];//0.01
                   
                    //http://pay.ddbbqp.com/return_url.aspx?buyer_email=lgzh306%40126.com&buyer_id=2088602184028472&exterface=create_direct_pay_by_user&is_success=T&notify_id=RqPnCoPT3K9%252Fvwbh3InWeOSzrvky7IAV3JJOfdsL5TFWKbTMC8BGq%252Bm99WAaYWgnOmS6&notify_time=2016-10-10+15%3A48%3A34&notify_type=trade_status_sync&out_trade_no=test20161010092017&payment_type=1&seller_email=20718505%40qq.com&seller_id=2088421564177650&subject=111&total_fee=0.01&trade_no=2016101021001004470296721955&trade_status=TRADE_SUCCESS&sign=4d4f0c35e95f4dce7c34c1ff70e321fd&sign_type=MD5

                    if (Request.QueryString["trade_status"] == "TRADE_FINISHED" || Request.QueryString["trade_status"] == "TRADE_SUCCESS")
                    {
                        //判断该笔订单是否在商户网站中已经做过处理
                        VWPayOrderEntity payentity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(out_trade_no);
                        if (payentity.Id > 0 && payentity.Status == 0)//未支付完成
                        {
                            PayAliResultEntity _entity = new PayAliResultEntity();
                            _entity.Buyeremail = buyer_email;
                            _entity.Buyerid = buyer_id;
                            _entity.CreateTime = DateTime.Now;
                            _entity.HasDeal = 0;
                            _entity.Issuccess = is_success;
                            _entity.Notifytime = notify_time;
                            _entity.Notifytype = notify_type;
                            _entity.Outtradeno = out_trade_no;
                            _entity.Paymenttype = payment_type;
                            _entity.Selleremail = seller_email;
                            _entity.Sellerid = seller_id;
                            _entity.Subject = subject;
                            _entity.Totalfee = total_fee;
                            _entity.Tradeno = trade_no;
                            _entity.Tradestatus = trade_status;

                            LogUtil.Log("订单付款回调成功：", JsonJC.ObjectToJson(_entity));
                            PayAliResultBLL.Instance.AddPayAliResult(_entity);

                            payentity.PayTime = DateTime.Now;
                            payentity.PayPrice = StringUtils.GetDbDecimal(total_fee);
                            payentity.ExternalCode = trade_no;
                            payentity.Status = 1;
                            //先更新业务网站收款记录 
                            int result = PayOrderBLL.Instance.RecivedPaySuccess(payentity);
                            if (payentity.SysType == (int)SystemType.B2B || payentity.SysType == (int)SystemType.B2BMobile)
                            {
                                VWOrderEntity _order = OrderBLL.Instance.GetVWOrderByCode(StringUtils.GetDbLong(payentity.SysOrderCode));
                                if (_order.Status == (int)OrderStatus.WaitPay)
                                {
                                    if (OrderBLL.Instance.PayFinishedForOrder(StringUtils.GetDbLong(payentity.SysOrderCode), payentity.PayPrice) > 0)
                                    {
                                        ///业务网站状态更新后更新支付总表 
                                        EmailSendBLL.Instance.OrderRemind(out_trade_no);
                                        LogUtil.Log("订单付款成功,订单号：", out_trade_no.ToString());
                                    }
                                }

                            }
                            return Redirect("/Pay/PaySuccess");
                             
                        }
                        else if (payentity.Id > 0 && payentity.Status == 1)
                        {
                            return Redirect("/Pay/PaySuccess");
                        }
                        else
                        { 
                            return Redirect("/Pay/PayError");
                        }
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //如果有做过处理，不执行商户的业务程序
                    }
                    else
                    {
                        Response.Write("trade_status=" + Request.QueryString["trade_status"]);
                    }


                    Response.Write("支付成功<br />");
                    return Redirect("/Pay/PayError");

                    //打印页面

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    //string out_trade_no = Request.QueryString["out_trade_no"];
                    //VWOrderEntity _order = OrderBLL.Instance.GetVWOrderByCode(StringUtils.GetDbLong(out_trade_no));
                    //if (_order.Status == (int)OrderStatus.WaitDeal|| _order.Status == (int)OrderStatus.WaitDeliver)
                    //{
                    //    return Redirect("/Pay/PaySuccess");
                    //}
                    Response.Write("支付验证失败");
                }
            }
            else
            {
                Response.Write("无返回参数");
            }
            return View();
        }

        /// <summary>
        /// 退款结果
        /// </summary>
        public void PayReturnBack()
        {
            LogUtil.Log("支付宝批量退款接口", "qq");
            SortedDictionary<string, string> sPara = GetRequestPost();

            if (sPara.Count > 0)//判断是否有带返回参数
            {
                alipayNotify aliNotify = new alipayNotify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);//验证消息是否是支付宝发出的合法消息

                if (verifyResult)//验证成功
                {
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码


                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //批次号

                    string batch_no = Request.Form["batch_no"];

                    //批量退款数据中转账成功的笔数

                    string success_num = Request.Form["success_num"];

                    //批量退款数据中的详细信息
                    string result_details = Request.Form["result_details"];
                    LogUtil.Log("支付宝批量退款接口，批次号：", "批次号：" + batch_no + ",success_num:" + success_num + ",result_details:" + result_details);

                    //System.Exception: 批次号：201701140007,success_num:1,result_details:2017011321001004470228487114^0.01^TRADE_HAS_CLOSED#2017011421001004470228502268^0.01^SUCCESS
                    //rrrrr			
                    //2017-01-14 00:21:39[39] ERROR index (null) - Title=支付宝批量退款接口，批次号：,LogMsg=批次号：201701140007,success_num:1,result_details:2017011321001004470228487114^0.01^TRADE_HAS_CLOSED#2017011421001004470228502268^0.01^SUCCESS	110.75.225.88		/pay/PayReturnBack
                    //2
                    PayRebackBLL.Instance.ProcPayBackRecive(batch_no, success_num, result_details);
                    //string[] _returnItems = result_details.Split('#');
                    //IList<ReturnItem> _returnItemList = new List<ReturnItem>();
                    //for (var i = 0; i < _returnItems.Length; i++)
                    //{
                    //    string[] _itemStr = _returnItems[i].Split('^');
                    //    ReturnItem _returnItem = new ReturnItem();
                    //    _returnItem.TradeNoLog = _itemStr[0];
                    //    _returnItem.RebackFee = Convert.ToDecimal(_itemStr[1]);
                    //    _returnItem.Status = _itemStr[2];
                    //    _returnItemList.Add(_returnItem);
                    //}

                    //foreach (ReturnItem item in _returnItemList)
                    //{
                    //    if (item.Status == "SUCCESS")
                    //    {
                    //        try
                    //        {
                    //            long _orderCode = PayRebackBLL.Instance.GetPayRebackByTradeNoLog(item.TradeNoLog) == null ? 0 : PayRebackBLL.Instance.GetPayRebackByTradeNoLog(item.TradeNoLog).OrderCode;
                    //            if (_orderCode != 0)
                    //            {
                    //                int _result = PayRebackBLL.Instance.UpdatePayRebackByTradeNoLog(item.TradeNoLog, item.RebackFee);
                    //                _result *= ReturnXSBLL.Instance.UpdateReturnXSByReturnOrderCode(_orderCode);
                    //                _result *= OrderBLL.Instance.UpdateOrderByCode(_orderCode, (int)OrderStatus.HasReturned,item.RebackFee);
                    //                if (_result == 0)
                    //                {
                    //                    throw new Exception("订单号为:" + _orderCode + ",退款失败");
                    //                }
                    //                else if(_result>0)
                    //                {
                    //                    LogUtil.Log("支付宝批量退款接口", "订单号为:" + _orderCode + ",退款成功");
                    //                }

                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            LogUtil.Log("支付宝批量退款接口", ex.Message.ToString());
                    //        }
                    //    }
                    //}


                
                    //判断是否在商户网站中已经做过了这次通知返回的处理
                    //如果没有做过处理，那么执行商户的业务程序
                    //如果有做过处理，那么不执行商户的业务程序

                    LogUtil.Log("支付宝批量退款接口", "success");

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    LogUtil.Log("支付宝批量退款接口", "fail");
                }
            }
            else
            {
                LogUtil.Log("支付宝批量退款接口", "无参数ss");
            }
        }


        #region  获取支付宝信息(GET or POST)

        /// <summary>
        /// 获取支付宝GET过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestGet()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.QueryString;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.QueryString[requestItem[i]]);
            }

            return sArray;
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();//有序字典
            NameValueCollection coll;//键值对
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;//获取表单

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }


        #endregion

        #region 支付状态

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <returns></returns>
        public ActionResult PaySuccess()
        {
            return View();
        }


        /// <summary>
        /// 支付失败
        /// </summary>
        /// <returns></returns>
        public ActionResult PayFail()
        {
            return View();
        }


        /// <summary>
        /// 支付出错
        /// </summary>
        /// <returns></returns>
        public ActionResult PayError()
        {
            return View();
        }

        #endregion



    }
}
