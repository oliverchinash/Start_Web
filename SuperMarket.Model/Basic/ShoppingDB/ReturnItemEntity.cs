
using System;
using System.Data;
using System.Text; 
 

namespace SuperMarket.Model
{
    //退换货返回记录
    public class ReturnItem
    {
        private string _TradeNoLog;//原付款交易号
        public string TradeNoLog
        {
            get;
            set;
        }

        private decimal _RebackFee;//退款金额
        public decimal RebackFee
        {
            get;
            set;
        }

        private string _Status;//退款操作状态
        public string Status
        {
            get;
            set;
        }
    }
}
