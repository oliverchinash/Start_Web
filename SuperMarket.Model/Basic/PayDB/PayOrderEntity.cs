﻿ /*****************************************
Table  Name:PayOrder
Create Time:2017/11/16 8:34:26
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>PayOrder
	/// This entiy code is generated by codesmith. this entity is PayOrder.
	/// </summary>
	[Serializable()]
	public class PayOrderEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 支付总表
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
		/// 支付唯一码
		/// <summary>
		private  string _PayOrderCode;
	 	public string PayOrderCode
		{
			get
			{
				return _PayOrderCode;
			}
			set
			{
				_PayOrderCode=value;
			}
		}

	    /// <summary>
		/// 订单编号
		/// <summary>
		private  string _SysOrderCode;
	 	public string SysOrderCode
		{
			get
			{
				return _SysOrderCode;
			}
			set
			{
				_SysOrderCode=value;
			}
		}

	    /// <summary>
		/// 订单来源系统Id
		/// <summary>
		private  int _SysType;
	 	public int SysType
		{
			get
			{
				return _SysType;
			}
			set
			{
				_SysType=value;
			}
		}

	    /// <summary>
		/// 付款金额
		/// <summary>
		private  decimal _NeedPayPrice;
	 	public decimal NeedPayPrice
		{
			get
			{
				return _NeedPayPrice;
			}
			set
			{
				_NeedPayPrice=value;
			}
		}

	    /// <summary>
		/// 付款方式
		/// <summary>
		private  int _PayMethod;
	 	public int PayMethod
		{
			get
			{
				return _PayMethod;
			}
			set
			{
				_PayMethod=value;
			}
		}

	    /// <summary>
		/// 创建时间
		/// <summary>
		private  DateTime _CreateTime;
	 	public DateTime CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime=value;
			}
		}

	    /// <summary>
		/// 创建人Id
		/// <summary>
		private  int _CreateManId;
	 	public int CreateManId
		{
			get
			{
				return _CreateManId;
			}
			set
			{
				_CreateManId=value;
			}
		}

	    /// <summary>
		/// 状态：0未付款，1已付款
		/// <summary>
		private  int _Status;
	 	public int Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status=value;
			}
		}

	    /// <summary>
		/// 支付时间
		/// <summary>
		private  DateTime _PayTime;
	 	public DateTime PayTime
		{
			get
			{
				return _PayTime;
			}
			set
			{
				_PayTime=value;
			}
		}
        /// <summary>
        /// 付款金额
        /// <summary>
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

        private string _ExternalCode;
        public string ExternalCode
        {
            get
            {
                return _ExternalCode;
            }
            set
            {
                _ExternalCode = value;
            }
        }
        
        #endregion
    }
}
