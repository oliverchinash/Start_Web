using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public class VWOrderPayLineConfirm
    {

        #region Public Properties	
        /// <summary>
        /// 订单线下支付处理确认表
        /// <summary>
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

        /// <summary>
        /// 订单编号
        /// <summary>
        private Int64 _OrderCode;
        public Int64 OrderCode
        {
            get
            {
                return _OrderCode;
            }
            set
            {
                _OrderCode = value;
            }
        }
        /// <summary>
        /// 订单编号
        /// <summary>
        private decimal _ActPrice;
        public decimal ActPrice
        {
            get
            {
                return _ActPrice;
            }
            set
            {
                _ActPrice = value;
            }
        } 
        private string _PayConfirmCode;
        public string PayConfirmCode
        {
            get
            {
                return _PayConfirmCode;
            }
            set
            {
                _PayConfirmCode = value;
            }
        }
        /// <summary>
        /// 线下付款的原因
        /// <summary>
        private string _Reason;
        public string Reason
        {
            get
            {
                return _Reason;
            }
            set
            {
                _Reason = value;
            }
        }

        /// <summary>
        /// 申请时间
        /// <summary>
        private DateTime _CreateTime;
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
            }
        }

        /// <summary>
        /// 申请人Id
        /// <summary>
        private int _CreateManId;
        public int CreateManId
        {
            get
            {
                return _CreateManId;
            }
            set
            {
                _CreateManId = value;
            }
        }

        /// <summary>
        /// 财务审核人ID
        /// <summary>
        private int _FinanceDealManId;
        public int FinanceDealManId
        {
            get
            {
                return _FinanceDealManId;
            }
            set
            {
                _FinanceDealManId = value;
            }
        }

        /// <summary>
        /// 财务审核时间
        /// <summary>
        private DateTime _FinanceDealTime;
        public DateTime FinanceDealTime
        {
            get
            {
                return _FinanceDealTime;
            }
            set
            {
                _FinanceDealTime = value;
            }
        }

        /// <summary>
        /// 处理状态。0待处理，1已处理通过，2处理拒绝
        /// <summary>
        private int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        public string StatusName
        {
            get
            {

                return EnumEntityShow.Instance.GetEnumDes((PayLineConfirm)Status);
            }
        }
        #endregion
    }
}
