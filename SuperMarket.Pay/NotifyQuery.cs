using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web;

namespace SuperMarket.Pay
{
    public abstract class NotifyQuery
    {
        protected NotifyQuery()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payRequestID">支付请求编号</param>
        /// <param name="paymentID"></param>
        /// <param name="payAmount"></param>
        /// <param name="bankTransNum"></param>
        /// <param name="isSucceed"></param>
        /// <param name="eBankID"></param>
        /// <param name="payRequestEntity"></param>
        /// <returns></returns>
        public string SetPayResult(string payRequestID, decimal payAmount, string bankTransNum, bool isSucceed)
        {
            string OrderCode="";
            return SetPayResult(payRequestID, payAmount, bankTransNum, isSucceed,out  OrderCode);
        }
        public string SetPayResult(string payRequestID, decimal payAmount, string bankTransNum, bool isSucceed,out string OrderCode)
        {
            OrderCode = "";
            //decimal dRate = Mola.BLL.Gy_Currency.Instance.GetFXRateByCurrencyId(Mola.Core.Globals.CurrencyID);

            ////decimal dRate = 1;

            //Mola.Model.XS_PayRequestEntity payRequestEntity= Mola.BLL.XS_PayRequest.Instance.GetPayRequestDetail("PayRequestID='" + payRequestID+"'");
            //if (payRequestEntity == null)
            //{
            //    return "fail";
            //}

            //OrderCode = payRequestEntity.OrderCode;

            //if (payRequestEntity.Status==1)
            //{
            //    return "true";
            //}
            //if (payRequestEntity == null)//如果没有找到申请，则插入非正常转账
            //{                
            //    return "fail";
            //}            
            //if (isSucceed)
            //{
            //    payRequestEntity.Status = 1;//成功                
            //    //if (payAmount == (decimal)payRequestEntity.FXRequestAmt)
            //    //{
            //    //    payRequestEntity.Status = 1;//成功                    
            //    //}
            //    //else if (payAmount == (decimal)payRequestEntity.RequestAmt)  //PAYPAL 特殊情况
            //    //{
            //    //    payAmount = payRequestEntity.FXRequestAmt;
            //    //    payRequestEntity.Status = 1;//成功                    
            //    //}
            //    //else
            //    //{
            //    //    payRequestEntity.Status = 2;//非正常成功                                     
            //    //}
            //}
            //else
            //{
            //    payRequestEntity.Status = 0;//失败                
            //}            
            //payRequestEntity.ResponseTime = DateTime.Now;
            //payRequestEntity.BankTranSerialNum = bankTransNum;
            //payRequestEntity.FXResponseAmt = payRequestEntity.FXRequestAmt;
            //payRequestEntity.ResponseAmt = payRequestEntity.RequestAmt;
            //Mola.BLL.XS_PayRequest.Instance.UpdatePayRequest(payRequestEntity);                            
                
            //Mola.Model.Xs_OrderEntity orderEntity=Mola.BLL.Xs_Order.Instance.GetOrderDetail(payRequestEntity.OrderCode);
            
            //Mola.Model.KH_CustomerEntity customerEntity=Mola.BLL.KH_Customer.Instance.GetCustomerById(" where CusCode='" + orderEntity.CusCode + "'");
            //if (customerEntity.Email == "")
            //{
            //    return "true";
            //}
            //Mola.Model.WZ_EBankEntity eBankEntity = Mola.BLL.WZ_EBank.Instance.GetEBankDetail("EBankId='" + payRequestEntity.BankCode+ "'");

            //string contenturl = "";
            //string title = "";
            //if (isSucceed)
            //{
            //    contenturl = Mola.Core.Globals.WebUrl + "Email/WebPaySuccess.aspx?OrderID=" + payRequestEntity.OrderCode
            //        + "&OrderNet=" +
            //        ((decimal)orderEntity.FXSumAmt - (decimal)orderEntity.FXDiscount).ToString("#,##0.00")
            //        + "&LastName=" + System.Web.HttpContext.Current.Server.UrlEncode(customerEntity.CusName)
            //        + "&ResponseAmount=" + ((decimal)payRequestEntity.ResponseAmt).ToString("#,##0.00")
            //        + "&BankTransNum=" + payRequestEntity.BankTranSerialNum
            //        + "&BankName=" + System.Web.HttpContext.Current.Server.UrlEncode(eBankEntity.EBankName)
            //        + "&ResponseTime=" + payRequestEntity.ResponseTime + "&PayTransID=" + payRequestEntity.TransCode;
            //    title = "支付成功通知";
            //}
            //else
            //{
            //    contenturl = Mola.Core.Globals.WebUrl + "Email/WebPayFailed.aspx?OrderID=" + payRequestEntity.OrderCode
            //        + "&OrderNet=" +
            //        ((decimal)orderEntity.FXSumAmt - (decimal)orderEntity.FXDiscount).ToString("#,##0.00")
            //        + "&LastName=" + System.Web.HttpContext.Current.Server.UrlEncode(customerEntity.CusName)
            //        + "&ResponseAmount=" + ((decimal)payRequestEntity.ResponseAmt).ToString("#,##0.00")
            //        + "&BankTransNum=" + payRequestEntity.BankTranSerialNum
            //        + "&BankName=" + System.Web.HttpContext.Current.Server.UrlEncode(eBankEntity.EBankName)
            //        + "&RequestTime=" + payRequestEntity.RequestTime
            //        + "&BankMessage="; //+ System.Web.HttpContext.Current.Server.UrlEncode(cdEBankTrans.BankMessage);
            //    title = "支付失败通知";
            //}
            //try
            //{

            //    Model.KH_SendEmailEntity sendemail = new Model.KH_SendEmailEntity();
            //    sendemail.Title = title;
            //    sendemail.SendTo = customerEntity.Email; 
            //    sendemail.CreateTime = DateTime.Now;
            //    sendemail.CreateBy = customerEntity.CusName;
            //    sendemail.Status = 0;
            //    sendemail.CusCode = customerEntity.CusCode;
            //    sendemail.Content =Mola.Core.StringUtils.GetContent(contenturl);
            //    sendemail.PriorIndex = 100;
            //    Mola.BLL.KH_SendEmail.Instance.AddSendEmail(sendemail);

            //    //Mola.Components.MJ_Public.SendEmailSmtp(cdContact.Email, MBS.Components.MJ_Public.GetContent(contenturl), title);
            //}
            //catch
            //{
            //}
            return "true";
        }


    }
}

