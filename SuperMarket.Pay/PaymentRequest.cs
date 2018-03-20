using SuperMarket.Model;
using System;
using System.Globalization;
using System.Web;

namespace SuperMarket.Pay
{
    public abstract class PaymentRequest
    {
        private string format_form = "<form id = 'payform' name='payform' action='{0}' method='get'>{1}</form><body>";
        private string format_formpost = "<form id='payform' name='payform' action='{0}' method='post'>{1}</form><body>";
        private string format_input = ("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\">" + Environment.NewLine);

        protected PaymentRequest()
        {
        }

        protected virtual string CreateField(string name, string strValue)
        {
            return string.Format(CultureInfo.InvariantCulture, this.format_input, new object[] { name, strValue });
        }

        protected virtual string CreateForm(string content, string action)
        {
            return string.Format(CultureInfo.InvariantCulture, this.format_form, new object[] { action, content });
        }
        protected virtual string CreateFormPost(string content, string action)
        {
            return string.Format(CultureInfo.InvariantCulture, this.format_formpost, new object[] { action, content });
        }
        public static PaymentRequest Instance(string requestType )
        {
            if (string.IsNullOrEmpty(requestType))
            {
                return null;
            }
            //object[] args = new object[] {  tradeEntity };
            return (Activator.CreateInstance(Type.GetType("SuperMarket.Pay." + requestType.ToLower() + "Request") ) as PaymentRequest);
        }

        protected virtual void RedirectToGateway(string url)
        {
            HttpContext.Current.Response.Redirect(url, true);
        } 
        public abstract void SendRequest(VWPayOrderEntity tradeEntity);
        public abstract void ReturnRequest(PayOrderRebackEntity rebackEntity);
        protected virtual void SubmitPaymentForm(string formContent)
        {
            string s = formContent + "<script language='javascript'>document.getElementById('payform').submit();</script>";
            HttpContext.Current.Response.Write(s);
            HttpContext.Current.Response.End();
        }
    }
}
