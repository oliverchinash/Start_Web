using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable]
    public class VWOrderEntity
    {
        public VWOrderEntity()
        {
            Details = new List<VWOrderDetailEntity>();

        }
        #region Public Properties	
        // 
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

        // 订单编号
        private Int64 _Code;
        public Int64 Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }

        // 
        private long _OrderVisualCode;
        public long OrderVisualCode
        {
            get
            {
                return _OrderVisualCode;
            }
            set
            {
                _OrderVisualCode = value;
            }
        }

        // 购货客户Id
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
        // 售货客户Id
        private int _CGMemId;
        public int CGMemId
        {
            get
            {
                return _CGMemId;
            }
            set
            {
                _CGMemId = value;
            }
        }
        // 售货客户Id
        private int _IsAhmTake;
        public int IsAhmTake
        {
            get
            {
                return _IsAhmTake;
            }
            set
            {
                _IsAhmTake = value;
            }
        }
        // 售货客户Id
        private string _CGMemNickName;
        public string CGMemNickName
        {
            get
            {
                return _CGMemNickName;
            }
            set
            {
                _CGMemNickName = value;
            }
        }
        // 订单状态（待付款，待发货，发货，签收，评价）
        private int _CanReturn;
        public int CanReturn
        {
            get
            {
                return _CanReturn;
            }
            set
            {
                _CanReturn = value;
            }
        }
        // 订单状态（待付款，待发货，发货，签收，评价）
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
        private int _FinancialStatus;
        public int FinancialStatus
        {
            get
            {
                return _FinancialStatus;
            }
            set
            {
                _FinancialStatus = value;
            }
        }
        
        public string StatusName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((OrderStatus)Status);
            }
           
        } 
        public string PayTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((PayType)PayType);
            }

        }
        
        // 
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
        /// 支付时间
        /// </summary>
        private DateTime _PayTime;
        public DateTime PayTime
        {
            get
            {
                return _PayTime;
            }
            set
            {
                _PayTime = value;
            }
        }
        

        // 订单类型：电话，网上，线下，换货等
        private int _OrderType;
        public int OrderType
        {
            get
            {
                return _OrderType;
            }
            set
            {
                _OrderType = value;
            }
        }

        // 邮费
        private decimal _TransFee;
        public decimal TransFee
        {
            get
            {
                return _TransFee;
            }
            set
            {
                _TransFee = value;
            }
        }

        // 是否需要发货
        private int _NeedDeliver;
        public int NeedDeliver
        {
            get
            {
                return _NeedDeliver;
            }
            set
            {
                _NeedDeliver = value;
            }
        }

        // 折扣Id
        private int _Integral;
        public int Integral
        {
            get
            {
                return _Integral;
            }
            set
            {
                _Integral = value;
            }
        }

        // 
        private decimal _TotalPrice;
        public decimal TotalPrice
        {
            get
            {
                return _TotalPrice;
            }
            set
            {
                _TotalPrice = value;
            }
        }

        // 
        private decimal _TotalMarketPrice;
        public decimal TotalMarketPrice
        {
            get
            {
                return _TotalMarketPrice;
            }
            set
            {
                _TotalMarketPrice = value;
            }
        }
        // 
        private decimal _PreDisCountPrice;
        public decimal PreDisCountPrice
        {
            get
            {
                return _PreDisCountPrice;
            }
            set
            {
                _PreDisCountPrice = value;
            }
        }
        private decimal _DisCountFee; 
        public decimal DisCountFee
        {
            get
            {
                return _DisCountFee;
            }
            set
            {
                _DisCountFee = value;
            }
        }
        /// <summary>
        /// 优惠券省的金额
        /// </summary>
        private decimal _CouponsFee; 
        public decimal CouponsFee
        {
            get
            {
                return _CouponsFee;
            }
            set
            {
                _CouponsFee = value;
            }
        }
        private decimal _AllNum;
        public decimal AllNum
        {
            get
            {
                return _AllNum;
            }
            set
            {
                _AllNum = value;
            }
        }
        // 
        private decimal _IntegralFee;
        public decimal IntegralFee
        {
            get
            {
                return _IntegralFee;
            }
            set
            {
                _IntegralFee = value;
            }
        }
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
        private decimal _PayPrice;
        public decimal PayPrice
        {
            get
            {
                return _PayPrice;
            }
            set
            {
                _PayPrice = value;
            }
        }
        private string _Remark;
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }
        public int _PayType;
        public int PayType
        {
            get
            {
                return _PayType;
            }
            set
            {
                _PayType = value;
            }
        }
        public int _ExpressCom;
        public int ExpressCom
        {
            get
            {
                return _ExpressCom;
            }
            set
            {
                _ExpressCom = value;
            }
        }
        public string ExpressComName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((WLComPany)ExpressCom);
            }
        }

        private int _MemLevel;
        /// <summary>
        /// 购买者会员级别
        /// </summary>
        public int MemLevel
        {
            get
            {
                return _MemLevel;
            }
            set
            {
                _MemLevel = value;
            }
        }
        // 是否商家
        private int _IsStore;
        public int IsStore
        {
            get
            {
                return _IsStore;
            }
            set
            {
                _IsStore = value;
            }
        }
        // 是否商家
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
        
        // 生成订单时的临时订单编码
        private Int64 _PreOrderCode;
        public Int64 PreOrderCode
        {
            get
            {
                return _PreOrderCode;
            }
            set
            {
                _PreOrderCode = value;
            }
        }
   
        // 处理时间
        private DateTime _DealTime;
        public DateTime DealTime
        {
            get
            {
                return _DealTime;
            }
            set
            {
                _DealTime = value;
            }
        }

        // 发货时间
        private DateTime _DeliverTime;
        public DateTime DeliverTime
        {
            get
            {
                return _DeliverTime;
            }
            set
            {
                _DeliverTime = value;
            }
        }

        // 收货时间
        private DateTime _ReciveTime;
        public DateTime ReciveTime
        {
            get
            {
                return _ReciveTime;
            }
            set
            {
                _ReciveTime = value;
            }
        }
        // 取消时间
        private DateTime _CancelTime;
        public DateTime CancelTime
        {
            get
            {
                return _CancelTime;
            }
            set
            {
                _CancelTime = value;
            }
        }
        private string _ReasonBack;
        public string ReasonBack
        {
            get
            {
                return _ReasonBack;
            }
            set
            {
                _ReasonBack = value;
            }
        }
        // 是否已开发票
        private int _MakeBill;
        public int MakeBill
        {
            get
            {
                return _MakeBill;
            }
            set
            {
                _MakeBill = value;
            }
        }
        // 是否已开发票 
        public string MakeBillName
        {
            get
            {
                return MakeBill == 1 ? "已开" : "未开";
            }
        }
        // 完成时间
        private DateTime _FinishedTime;
        public DateTime FinishedTime
        {
            get
            {
                return _FinishedTime;
            }
            set
            {
                _FinishedTime = value;
            }
        }
        // 生成订单时的临时订单编码
        private string _AccepterName;
        public string AccepterName
        {
            get
            {
                return _AccepterName;
            }
            set
            {
                _AccepterName = value;
            }
        }
        // 生成订单时的临时订单编码
        private string _AccepterAddress;
        public string AccepterAddress
        {
            get
            {
                return _AccepterAddress;
            }
            set
            {
                _AccepterAddress = value;
            }
        }
        private string _AccepterPhone;
        public string AccepterPhone
        {
            get
            {
                return _AccepterPhone;
            }
            set
            {
                _AccepterPhone = value;
            }
        }
        private int _AccepterProvinceId;
        public int AccepterProvinceId
        {
            get
            {
                return _AccepterProvinceId;
            }
            set
            {
                _AccepterProvinceId = value;
            }
        }

        private int _AccepterCityId;
        public int AccepterCityId
        {
            get
            {
                return _AccepterCityId;
            }
            set
            {
                _AccepterCityId = value;
            }
        }
        private string _AccepterProvinceName;
        public string AccepterProvinceName
        {
            get
            {
                return _AccepterProvinceName;
            }
            set
            {
                _AccepterProvinceName = value;
            }
        } 
        private string _AccepterCityName;
        public string AccepterCityName
        {
            get
            {
                return _AccepterCityName;
            }
            set
            {
                _AccepterCityName = value;
            }
        }
        
        private int _BillType;

        public int BillType
        {
            get
            {
                return _BillType;
            }
            set
            {
                _BillType = value;
            }
        }
        public string BillTypeName 
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((BillType)BillType);
            }

        }
        private string _BillName; 
        public string BillName
        {
            get
            {
                return _BillName;
            }
            set
            {
                _BillName = value;
            }
        }
        private string _BillAddress;
        public string BillAddress
        {
            get
            {
                return _BillAddress;
            }
            set
            {
                _BillAddress = value;
            }
        }
        private string _BillPhone;
        public string BillPhone
        {
            get
            {
                return _BillPhone;
            }
            set
            {
                _BillPhone = value;
            }
        }
        private string _BillBank;
        public string BillBank
        {
            get
            {
                return _BillBank;
            }
            set
            {
                _BillBank = value;
            }
        }
        private string _BillAccount;
        public string BillAccount
        {
            get
            {
                return _BillAccount;
            }
            set
            {
                _BillAccount = value;
            }
        }
        private int _MemCouponsId;
        public int MemCouponsId
        {
            get
            {
                return _MemCouponsId;
            }
            set
            {
                _MemCouponsId = value;
            }
        }
        private int _OrderStyle;
        public int OrderStyle
        {
            get
            {
                return _OrderStyle;
            }
            set
            {
                _OrderStyle = value;
            }
        }
        
        //public OrderAddressEntity AcceptAddress;
        public IList<VWOrderDetailEntity> Details;
        #endregion
    }
}
