using SuperMarket.Core.Util;
using SuperMarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.BLL.ShoppingDB
{
    public class OrderCommonBLL
    {
        #region 实例化
        public static OrderCommonBLL Instance
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
            internal static readonly OrderCommonBLL instance = new OrderCommonBLL();
        }
        #endregion

        public VWOrderEntity GetTransFeeDisCount(IList<OrderDetailPreTempEntity> _list)
        {
            VWOrderEntity _order = new VWOrderEntity();  
            //decimal _ActPrice = 0;
            //decimal _discountfee = 0;
            int num = 0;
            decimal _totalmarketprice = 0;
            decimal _totalprice = 0;
            if (_list != null && _list.Count > 0)
            {  
                foreach (OrderDetailPreTempEntity _entity in _list)
                {
                    //if (_entity.TransFeeType == (int)TransFeeType.Free || _entity.TransFeeType == (int)TransFeeType.PayBehind)
                    //{
                    //    hasfree = true;
                    //}
                    _totalmarketprice += _entity.MarketPrice * _entity.Num;
                    _totalprice += _entity.ActualPrice * _entity.Num;
                    num += _entity.Num;
                }
            } 
            _order.TransFee = 0;
            _order.TotalPrice = _totalprice;
            _order.TotalMarketPrice = _totalmarketprice;

            //折扣 
            _order.PreDisCountPrice = _order.TotalPrice + _order.TransFee;
            _order.ActPrice = _order.PreDisCountPrice; 
            _order.AllNum = num;
            
            return _order;
        }
        public VWOrderEntity GetTransFeeForOrder(VWOrderEntity _order)
        { 
            //decimal _ActPrice = 0;
            //decimal _discountfee = 0;
            int num = 0;
            decimal _totalmarketprice = 0;
            decimal _totalprice = 0;
            if (_order.Details != null && _order.Details.Count > 0)
            {
                foreach (VWOrderDetailEntity _entity in _order.Details)
                {
                    //if (_entity.TransFeeType == (int)TransFeeType.Free || _entity.TransFeeType == (int)TransFeeType.PayBehind)
                    //{
                    //    hasfree = true;
                    //}
                    //_totalmarketprice += _entity.MarketPrice * _entity.Num;
                    _totalprice += _entity.ActualPrice * _entity.Num;
                    num += _entity.Num;
                }
            }
            _order.TransFee = 0;
            _order.TotalPrice = _totalprice;
            _order.TotalMarketPrice = _totalmarketprice;

            //折扣 
            _order.PreDisCountPrice = _order.TotalPrice + _order.TransFee;
            _order.ActPrice = _order.PreDisCountPrice;
            _order.AllNum = num;

            return _order;
        }

        public decimal GetJiFenAmt(int jifen)
        {
            return StringUtils.GetDbDecimal(jifen) / 100;
        }
    }
}
