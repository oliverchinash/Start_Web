﻿ /*****************************************
Table  Name:CGQuotedPrice
Create Time:2017/8/23 11:12:18
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CGQuotedPrice
	/// This entiy code is generated by codesmith. this entity is CGQuotedPrice.
	/// </summary>
	[Serializable()]
	public class CGQuotedPriceEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 供应商报价明细表
		/// <summary>
		private  int _Id;
	 	public int Id
		{
			get
			{
				return _Id;
			}
			set
			{
				_Id=value;
			}
		}

	    /// <summary>
		/// 订单编号
		/// <summary>
		private  string _InquiryOrderCode;
	 	public string InquiryOrderCode
		{
			get
			{
				return _InquiryOrderCode;
			}
			set
			{
				_InquiryOrderCode=value;
			}
		}

	    /// <summary>
		/// 订单产品Id
		/// <summary>
		private  int _InquiryProductId;
	 	public int InquiryProductId
		{
			get
			{
				return _InquiryProductId;
			}
			set
			{
				_InquiryProductId=value;
			}
		}
        /// <summary>
        /// 订单产品子Id
        /// <summary>
        private int _InquiryProductSubId;
        public int InquiryProductSubId
        {
            get
            {
                return _InquiryProductSubId;
            }
            set
            {
                _InquiryProductSubId = value;
            }
        }
        /// <summary>
        /// 供应商Id
        /// <summary>
        private  int _CGMemId;
	 	public int CGMemId
		{
			get
			{
				return _CGMemId;
			}
			set
			{
				_CGMemId=value;
			}
		}

	    /// <summary>
		/// 供应商报价
		/// <summary>
		private  decimal _CGPrice;
	 	public decimal CGPrice
		{
			get
			{
				return _CGPrice;
			}
			set
			{
				_CGPrice=value;
			}
		}

	    /// <summary>
		/// 出售价格
		/// <summary>
		private  decimal _Price;
	 	public decimal Price
		{
			get
			{
				return _Price;
			}
			set
			{
				_Price=value;
			}
		}

	    /// <summary>
		/// 询价产品类型
		/// <summary>
		private  int _InquiryProductType;
	 	public int InquiryProductType
		{
			get
			{
				return _InquiryProductType;
			}
			set
			{
				_InquiryProductType=value;
			}
		}
        public string InquiryProductTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((InquiryProductTypeEnum)InquiryProductType);
            }
        }

        /// <summary>
        /// 产品说明
        /// <summary>
        private string _InquiryProductDes;
	 	public string InquiryProductDes
		{
			get
			{
				return _InquiryProductDes;
			}
			set
			{
				_InquiryProductDes=value;
			}
		}

	    /// <summary>
		/// 报价备注
		/// <summary>
		private  string _Remark;
	 	public string Remark
		{
			get
			{
				return _Remark;
			}
			set
			{
				_Remark=value;
			}
		}

	    /// <summary>
		/// 用户是否选择此产品
		/// <summary>
		private  int _HasSelected;
	 	public int HasSelected
		{
			get
			{
				return _HasSelected;
			}
			set
			{
				_HasSelected=value;
			}
		}

	    /// <summary>
		/// 供应商报价时间
		/// <summary>
		private  DateTime _QuotedTime;
	 	public DateTime QuotedTime
		{
			get
			{
				return _QuotedTime;
			}
			set
			{
				_QuotedTime=value;
			}
		}

	    /// <summary>
		/// 价格审核人Id
		/// <summary>
		private  int _CheckMemId;
	 	public int CheckMemId
		{
			get
			{
				return _CheckMemId;
			}
			set
			{
				_CheckMemId=value;
			}
		}

	    /// <summary>
		/// 价格审核时间
		/// <summary>
		private  string _CheckTime;
	 	public string CheckTime
		{
			get
			{
				return _CheckTime;
			}
			set
			{
				_CheckTime=value;
			}
		}

		#endregion				
	}
}
