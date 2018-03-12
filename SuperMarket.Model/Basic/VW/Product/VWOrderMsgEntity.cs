using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWOrderMsgEntity
    {
        // 
        private int _MemId;
        public int MemId
        {
            get
            {
                return _MemId;
            }
            set
            {
                _MemId = value;
            }
        }

        // 总订单数量
        private int _PurchaseTotalNum;
        public int PurchaseTotalNum
        {
            get
            {
                return _PurchaseTotalNum;
            }
            set
            {
                _PurchaseTotalNum = value;
            }
        }

        // 待支付订单数量
        private int _WaitPayNum;
        public int WaitPayNum
        {
            get
            {
                return _WaitPayNum;
            }
            set
            {
                _WaitPayNum = value;
            }
        }
        // 待卖家发货订单数量
        private int _WaitSellerDeliverNum;
        public int WaitSellerDeliverNum
        {
            get
            {
                return _WaitSellerDeliverNum;
            }
            set
            {
                _WaitSellerDeliverNum = value;
            }
        }
        
        // 待发货订单数量
        private int _WaitReciveNum;
        public int WaitReciveNum
        {
            get
            {
                return _WaitReciveNum;
            }
            set
            {
                _WaitReciveNum = value;
            }
        }

        // 待评价数量
        private int _WaitCommentNum;
        public int WaitCommentNum
        {
            get
            {
                return _WaitCommentNum;
            }
            set
            {
                _WaitCommentNum = value;
            }
        }

        // 取消订单数量
        private int _CancelNum;
        public int CancelNum
        {
            get
            {
                return _CancelNum;
            }
            set
            {
                _CancelNum = value;
            }
        } 
        // 订单总数
        private int _SellTotallNum;
        public int SellTotallNum
        {
            get
            {
                return _SellTotallNum;
            }
            set
            {
                _SellTotallNum = value;
            }
        }

        // 待处理确认订单数量
        private int _WaitDealNum;
        public int WaitDealNum
        {
            get
            {
                return _WaitDealNum;
            }
            set
            {
                _WaitDealNum = value;
            }
        }

        // 待发货订单数量
        private int _WaitDeliverNum;
        public int WaitDeliverNum
        {
            get
            {
                return _WaitDeliverNum;
            }
            set
            {
                _WaitDeliverNum = value;
            }
        }

        // 退货订单数量
        private int _ReturnNum;
        public int ReturnNum
        {
            get
            {
                return _ReturnNum;
            }
            set
            {
                _ReturnNum = value;
            }
        }

        // 换货订单数量
        private int _ExchangeNum;
        public int ExchangeNum
        {
            get
            {
                return _ExchangeNum;
            }
            set
            {
                _ExchangeNum = value;
            }
        }

        // 已完成订单数量
        private int _FinishedNum;
        public int FinishedNum
        {
            get
            {
                return _FinishedNum;
            }
            set
            {
                _FinishedNum = value;
            }
        }

        // 拒绝订单数量
        private int _RejectNum;
        public int RejectNum
        {
            get
            {
                return _RejectNum;
            }
            set
            {
                _RejectNum = value;
            }
        }
    }
}
