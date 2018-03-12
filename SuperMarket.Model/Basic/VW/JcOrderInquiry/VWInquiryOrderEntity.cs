using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
   public class VWInquiryOrderEntity
    {
        #region Public Properties	
        /// <summary>
        /// 询价订单记录表
        /// <summary>
        private int _InquiryOrderId;
        public int InquiryOrderId
        {
            get
            {
                return _InquiryOrderId;
            }
            set
            {
                _InquiryOrderId = value;
            }
        }

        /// <summary>
        /// 订单编号
        /// <summary>
        private string _InquiryOrderCode;
        public string InquiryOrderCode
        {
            get
            {
                return _InquiryOrderCode;
            }
            set
            {
                _InquiryOrderCode = value;
            }
        }
        private int _NeedDay;
        public int NeedDay
        {
            get
            {
                return _NeedDay;
            }
            set
            {
                _NeedDay = value;
            }
        }
        /// <summary>
        /// 如果注册，订购用户Id
        /// <summary>
        private int _ProductTypeId;
        public int ProductTypeId
        {
            get
            {
                return _ProductTypeId;
            }
            set
            {
                _ProductTypeId = value;
            }
        }
        public string ProductTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((InquiryProductTypeEnum)ProductTypeId);
            }
        }
        /// <summary>
        /// Vin码
        /// <summary>
        private string _VinNo;
        public string VinNo
        {
            get
            {
                return _VinNo;
            }
            set
            {
                _VinNo = value;
            }
        }
        private string _VinPic;
        public string VinPic
        {
            get
            {
                return _VinPic;
            }
            set
            {
                _VinPic = value;
            }
        }

        /// <summary>
        /// 发动机编号
        /// </summary>
        private string _EngineModelNo;
        public string EngineModelNo
        {
            get
            {
                return _EngineModelNo;
            }
            set
            {
                _EngineModelNo = value;
            }
        }

        /// <summary>
        /// 发动机照片
        /// <summary>
        private string _EngineModelPic;
        public string EngineModelPic
        {
            get
            {
                return _EngineModelPic;
            }
            set
            {
                _EngineModelPic = value;
            }
        }

        private int _CarBrandId;
        public int CarBrandId
        {
            get
            {
                return _CarBrandId;
            }
            set
            {
                _CarBrandId = value;
            }
        }
        private string _CarBrandName;
        public string CarBrandName
        {
            get
            {
                return _CarBrandName;
            }
            set
            {
                _CarBrandName = value;
            }
        }
        private int _CarSeriesId;
        public int CarSeriesId
        {
            get
            {
                return _CarSeriesId;
            }
            set
            {
                _CarSeriesId = value;
            }
        }
        private string _CarSeriesName;
        public string CarSeriesName
        {
            get
            {
                return _CarSeriesName;
            }
            set
            {
                _CarSeriesName = value;
            }
        }
        /// <summary>
        /// 车型匹配Id
        /// <summary>
        private int _CarTypeModelId;
        public int CarTypeModelId
        {
            get
            {
                return _CarTypeModelId;
            }
            set
            {
                _CarTypeModelId = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _CarTypeModelName;
        public string CarTypeModelName
        {
            get
            {
                return _CarTypeModelName;
            }
            set
            {
                _CarTypeModelName = value;
            }
        }


        /// <summary>
        /// 备注
        /// <summary>
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

        private string _WLRemark;
        public string WLRemark
        {
            get
            {
                return _WLRemark;
            }
            set
            {
                _WLRemark = value;
            }
        }

        private decimal _CGTotalPrice;
        public decimal CGTotalPrice
        {
            get
            {
                return _CGTotalPrice;
            }
            set
            {
                _CGTotalPrice = value;
            }
        }

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

        /// <summary>
        /// 提交人Id
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
        /// 建立时间
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
        /// 订单接洽员Id
        /// <summary>
        private int _CheckManId;
        public int CheckManId
        {
            get
            {
                return _CheckManId;
            }
            set
            {
                _CheckManId = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private DateTime _CheckTime;
        public DateTime CheckTime
        {
            get
            {
                return _CheckTime;
            }
            set
            {
                _CheckTime = value;
            }
        }

        /// <summary>
        /// 首次发送价格给用户确认时间
        /// <summary>
        private DateTime _PreQuoteTime;
        public DateTime PreQuoteTime
        {
            get
            {
                return _PreQuoteTime;
            }
            set
            {
                _PreQuoteTime = value;
            }
        }

        /// <summary>
        /// 用户首次确认时间
        /// <summary>
        private DateTime _UserSubmitFirstTime;
        public DateTime UserSubmitFirstTime
        {
            get
            {
                return _UserSubmitFirstTime;
            }
            set
            {
                _UserSubmitFirstTime = value;
            }
        }

        /// <summary>
        /// 最后一次提醒用户确认时间（用户反馈后成交前，或取消前的一次）
        /// <summary>
        private DateTime _SendUserEndTime;
        public DateTime SendUserEndTime
        {
            get
            {
                return _SendUserEndTime;
            }
            set
            {
                _SendUserEndTime = value;
            }
        }

        /// <summary>
        /// 用户最后一次确认时间
        /// <summary>
        private DateTime _UserSubmitEndTime;
        public DateTime UserSubmitEndTime
        {
            get
            {
                return _UserSubmitEndTime;
            }
            set
            {
                _UserSubmitEndTime = value;
            }
        }

        /// <summary>
        /// 发货员Id
        /// <summary>
        private int _DeliveryManId;
        public int DeliveryManId
        {
            get
            {
                return _DeliveryManId;
            }
            set
            {
                _DeliveryManId = value;
            }
        }

        /// <summary>
        /// 发货时间
        /// <summary>
        private DateTime _DeliveryTime;
        public DateTime DeliveryTime
        {
            get
            {
                return _DeliveryTime;
            }
            set
            {
                _DeliveryTime = value;
            }
        }

        /// <summary>
        /// 订单完成时间
        /// <summary>
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

        /// <summary>
        /// 状态 1用户提交，2接洽员审核，3供应商报价，4销售价格审核，5用户确认订单，6物流配送完成，99订单关闭
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
                return EnumEntityShow.Instance.GetEnumDes((OrderInquiryStatusEnum)Status); 
            }
        }
        private int _StatusForMem ;
        public int StatusForMem 
        {
            get
            {
                return _StatusForMem ;
            }
            set
            {
                _StatusForMem  = value;
            }
        }
        public string StatusForMemName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((InquiryStatusForMemEnum)StatusForMem);
            }
        }
        

        public IList<InquiryOrderPicsEntity> OrderPics;
        public IList<InquiryProductEntity> OrderProducts;
        public IList<InquiryProductSubEntity> OrderProductSubs;
        public string OrderPicPaths;

        /// <summary>
        /// 如果注册，订购用户Id
        /// <summary>
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

        /// <summary>
        /// 人员名
        /// <summary>
        private string _MemName;
        public string MemName
        {
            get
            {
                return _MemName;
            }
            set
            {
                _MemName = value;
            }
        }

        /// <summary>
        /// 联系手机
        /// <summary>
        private string _MemPhone;
        public string MemPhone
        {
            get
            {
                return _MemPhone;
            }
            set
            {
                _MemPhone = value;
            }
        }

        /// <summary>
        /// 地址
        /// <summary>
        private string _CompanyAddress;
        public string CompanyAddress
        {
            get
            {
                return _CompanyAddress;
            }
            set
            {
                _CompanyAddress = value;
            }
        }

        /// <summary>
        /// 公司名称
        /// <summary>
        private string _CompanyName;
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        /// <summary>
        /// 供应商列表展示的时候记录此供应商是否报价
        /// </summary>
        private int _HasQuote;
        public int HasQuote
        {
            get
            {
                return _HasQuote;
            }
            set
            {
                _HasQuote = value;
            }
        }
        private DateTime _QuoteTime;
        public DateTime QuoteTime
        {
            get
            {
                return _QuoteTime;
            }
            set
            {
                _QuoteTime = value;
            }
        }
        private int _ScopeType;
        public int ScopeType
        {
            get
            {
                return _ScopeType;
            }
            set
            {
                _ScopeType = value;
            }
        }
        #endregion
    }
}
