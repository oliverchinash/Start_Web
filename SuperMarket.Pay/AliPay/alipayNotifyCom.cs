using System;
using System.Collections.Specialized;
using System.Globalization;
using System.Web;
using System.Text; 
namespace SuperMarket.Pay
{

    public class alipayNotifyCom : NotifyQuery
    {
        private string input_charset = "utf-8";
        //private string key = "nq2klm7ljem6gyhyr6v0mlg85u3w5186";
        //private string partner = "2088101977103524";



        private string key = "ljatxtvpwxbfqfc9vjablmd8vu2zy5v6";
        private string partner = "2088601286574104";

        public alipayNotifyCom()
        {

        }

        private string CreateUrl(string notifyid)
        {
            return string.Format(CultureInfo.InvariantCulture, "https://www.alipay.com/cooperate/gateway.do?service=notify_verify&partner={0}&notify_id={1}", new object[] { partner, notifyid });
        }

        public string VerifyReturn(NameValueCollection parameters)
        {

            bool flag;
            try
            {
                flag = bool.Parse(Globals.GetResponse(this.CreateUrl(parameters["notify_id"]), 120000));
            }
            catch (Exception ex)
            {
                flag = false;
            }
            //parameters.Remove("HIGW");
            string[] strArray2 = Globals.BubbleSort(parameters.AllKeys);
            string s = "";
            for (int i = 0; i < strArray2.Length; i++)
            {
                if ((!string.IsNullOrEmpty(parameters[strArray2[i]]) && (strArray2[i] != "sign")) && (strArray2[i] != "sign_type"))
                {
                    if (i == (strArray2.Length - 1))
                    {
                        s = s + strArray2[i] + "=" + parameters[strArray2[i]];
                    }
                    else
                    {
                        s = s + strArray2[i] + "=" + parameters[strArray2[i]] + "&";
                    }
                }
            }
            s = s + key;
            if (flag && parameters["sign"].Equals(Globals.GetMD5(s, this.input_charset)))
            {
                return SetPayResult(parameters["out_trade_no"], decimal.Parse(parameters["total_fee"]), parameters["trade_no"], true);
            }
            else
            {
                return "fail";
            }
        }


        public string VerifyNotify(NameValueCollection parameters)
        {
            return "";
        }
    }
}

