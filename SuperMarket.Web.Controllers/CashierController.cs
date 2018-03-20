﻿using SuperMarket.BLL;
using SuperMarket.BLL.CGOrderDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.PayDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Pay;
using SuperMarket.Pay.WeiXin;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class CashierController : BaseMemberController
    {
         
        public ActionResult WeiXin()
        { 
            string _paycode = QueryString.SafeQ("paycode");
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(_paycode);
            DateTime dtnow = DateTime.Now ;
            NativePay nativePay = new NativePay();
            WxPayData data = nativePay.GetPayUrl(_payen);
            string url = data.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接

            ViewBag.Url = url; 
            return View();
        }
        public void WeiXinNotify()
        {
            ResultNotify resultNotify = new ResultNotify(); 
            WxPayData notifyData = resultNotify.GetNotifyData();
            LogUtil.Log("微信支付返回接口接收：", notifyData.ToXml());
            if (notifyData.GetValue("return_code")!=null&&(notifyData.GetValue("return_code").ToString() == "SUCCESS")&& notifyData.GetValue("out_trade_no")!=null&& notifyData.GetValue("transaction_id")!=null)
            {
                string paycode = notifyData.GetValue("out_trade_no").ToString();
                decimal fee = StringUtils.GetDbDecimal(notifyData.GetValue("total_fee")) / 100;
                VWPayOrderEntity payentity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
                if (payentity.Id>0&&payentity.Status == 0)//未支付完成
                {
                    payentity.PayTime =DateTime.Now;
                    payentity.PayPrice = fee;
                    payentity.ExternalCode = notifyData.GetValue("transaction_id").ToString();
                    payentity.Status = 1;
                    //先更新业务网站收款记录
                    if (payentity.SysType == (int)SystemType.B2B || payentity.SysType == (int)SystemType.B2BMobile)
                    {
                        VWOrderEntity _order = OrderBLL.Instance.GetVWOrderByCode(StringUtils.GetDbLong(payentity.SysOrderCode));
                        if (_order.Status == (int)OrderStatus.WaitPay)
                        {
                            if (OrderBLL.Instance.PayFinishedForOrder(StringUtils.GetDbLong(payentity.SysOrderCode), fee) > 0)
                            {
                                ///业务网站状态更新后更新支付总表
                                PayOrderBLL.Instance.RecivedPaySuccess(payentity);
                            }
                        }
                    }
                }
            }
           
        }

   }
}
