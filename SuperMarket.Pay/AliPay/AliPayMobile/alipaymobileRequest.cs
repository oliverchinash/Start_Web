using SuperMarket.Model;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Collections.Generic;

namespace SuperMarket.Pay 
{

    public class alipaymobileRequest : PaymentRequest
    {
        private string _input_charset = alipaymobileConfig.input_charset;
        private string body;
        private string gateway = alipaymobileConfig.gateway;
        private string key;
        private string notify_url;
        private string out_trade_no; 

        private string partner;
        private string payment_type = alipaymobileConfig.payment_type;
        private string price;
        private string return_url;
        private string seller_email = "";
        private string service = alipaymobileConfig.service;
        private string show_url;
        private string sign_type = alipaymobileConfig.sign_type;
        private string subject;
        //签名方式
        private static string _sign_type = "";
        /// <summary>
        /// 退款标记
        /// </summary>
        private string batch_no;
        private int batch_num;
        private string refuncd_data;

        public alipaymobileRequest()
        {
            this.return_url =   alipaymobileConfig.return_url ;
            this.notify_url =   alipaymobileConfig.notify_url ;
            this.body = "易店心订单";
            this.show_url = "www.aahama.com";
            _sign_type = alipaymobileConfig.sign_type.Trim().ToUpper();
             this.key = alipaymobileConfig.key;
            this.partner = alipaymobileConfig.partner;
            this.seller_email = alipaymobileConfig.seller_email;
            //this.batch_no = tradeEntity.Batch_No;
            //this.batch_num = tradeEntity.Batch_Num;
            //this.refuncd_data = tradeEntity.Refund_Data;
        }

        public override void SendRequest(VWPayOrderEntity tradeEntity)
        {
            this.out_trade_no = tradeEntity.PayOrderCode.ToString();
            this.subject = "易店心订单号:" + tradeEntity.SysOrderCode.ToString();
            this.price = tradeEntity.NeedPayPrice.ToString();
            string paymethod = "bankPay";
            string sign = "";
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", alipaymobileConfig.service);
            sParaTemp.Add("partner", alipaymobileConfig.partner);
            sParaTemp.Add("seller_id", alipaymobileConfig.seller_id);
            sParaTemp.Add("_input_charset", alipaymobileConfig.input_charset.ToLower());
            sParaTemp.Add("payment_type", alipaymobileConfig.payment_type);
            sParaTemp.Add("notify_url", alipaymobileConfig.notify_url);
            sParaTemp.Add("return_url", alipaymobileConfig.return_url);
            sParaTemp.Add("anti_phishing_key", alipaymobileConfig.anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", alipaymobileConfig.exter_invoke_ip);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", price);
            sParaTemp.Add("body", body);
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = ""; 
            //过滤签名参数数组
            sPara = alipayCore.FilterPara(sParaTemp); 
            //获得签名结果
            mysign =  BuildRequestMysign(sPara);
            sPara.Add("sign", mysign);
            sPara.Add("sign_type",  sign_type);

            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sPara)
            {
                builder.Append(this.CreateField(temp.Key, temp.Value)); 
            } 
            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gateway+ "_input_charset="+ alipaymobileConfig.input_charset.ToLower()));


        }
        public override void ReturnRequest(PayOrderRebackEntity rebackEntity)
        {
            this.out_trade_no = rebackEntity.PayOrderCode.ToString();
            this.subject = "易店心订单号:" + rebackEntity.SysOrderCode.ToString();
            this.price = rebackEntity.RebackPrice.ToString();
            this.batch_no = rebackEntity.PayRebackCode;
            this.batch_num = 1;
            this.refuncd_data = "";// rebackEntity.Refund_Data;

            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", alipaymobileConfig.refundservice);
            sParaTemp.Add("partner", alipaymobileConfig.partner);
            sParaTemp.Add("_input_charset", alipaymobileConfig.input_charset.ToLower());
            sParaTemp.Add("notify_url", alipaymobileConfig.rebacknotify_url);
            sParaTemp.Add("seller_user_id", alipaymobileConfig.seller_id);
            sParaTemp.Add("refund_date", alipaymobileConfig.refund_date);
            sParaTemp.Add("batch_no", batch_no);
            sParaTemp.Add("batch_num", batch_num.ToString());
            sParaTemp.Add("detail_data", refuncd_data);
            ////待请求参数数组
            //Dictionary<string, string> dicPara = new Dictionary<string, string>();
            //dicPara = BuildRequestPara(sParaTemp);


            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";
            //过滤签名参数数组
            sPara = alipayCore.FilterPara(sParaTemp);
            //获得签名结果
            mysign = BuildRequestMysign(sPara);
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", sign_type);
            StringBuilder builder = new StringBuilder();
            foreach (KeyValuePair<string, string> temp in sPara)
            {
                builder.Append(this.CreateField(temp.Key, temp.Value));
            }


            this.SubmitPaymentForm(this.CreateForm(builder.ToString(), this.gateway + "_input_charset=" + alipaymobileConfig.input_charset.ToLower()));
   
        }
        /// <summary>
        /// 生成请求时的签名
        /// </summary>
        /// <param name="sPara">请求给支付宝的参数数组</param>
        /// <returns>签名结果</returns>
        private   string BuildRequestMysign(Dictionary<string, string> sPara)
        {
            //把数组所有元素，按照“参数=参数值”的模式用“&”字符拼接成字符串
            string prestr = alipayCore.CreateLinkString(sPara);

            //把最终的字符串签名，获得签名结果
            string mysign = "";
            switch (sign_type.ToUpper())
            {
                case "MD5":
                    mysign = alipayMD5.Sign(prestr, key.Trim(),  _input_charset.Trim());
                    break;
                default:
                    mysign = "";
                    break;
            }

            return mysign;
        }
        /// <summary>
        /// 生成要请求给支付宝的参数数组
        /// </summary>
        /// <param name="sParaTemp">请求前的参数数组</param>
        /// <returns>要请求的参数数组</returns>
        private  Dictionary<string, string> BuildRequestPara(SortedDictionary<string, string> sParaTemp)
        {
            //待签名请求参数数组
            Dictionary<string, string> sPara = new Dictionary<string, string>();
            //签名结果
            string mysign = "";

            //过滤签名参数数组
            sPara = alipayCore.FilterPara(sParaTemp);

            //获得签名结果
            mysign = BuildRequestMysign(sPara);

            //签名结果与签名方式加入请求提交参数组中
            sPara.Add("sign", mysign);
            sPara.Add("sign_type", _sign_type);

            return sPara;
        }

    }
}
