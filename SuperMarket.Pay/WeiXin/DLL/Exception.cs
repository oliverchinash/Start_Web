using System;
using System.Collections.Generic;
using System.Web;

namespace SuperMarket.Pay.WeiXin
{
    public class WxPayException : Exception 
    {
        public WxPayException(string msg) : base(msg) 
        {

        }
     }
}