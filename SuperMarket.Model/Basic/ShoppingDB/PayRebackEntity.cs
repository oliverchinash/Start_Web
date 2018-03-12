﻿ /*****************************************
Table  Name:PayReback
Create Time:2016/12/9 14:09:58
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>PayReback
	/// This entiy code is generated by codesmith. this entity is PayReback.
	/// </summary>
	[Serializable()]
	public class PayRebackEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 退款记录表
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
		private  Int64 _OrderCode;
	 	public Int64 OrderCode
		{
			get
			{
				return _OrderCode;
			}
			set
			{
				_OrderCode=value;
			}
		}

	    /// <summary>
		/// 退款批次号
		/// <summary>
		private  Int64 _BatchNo;
	 	public Int64 BatchNo
		{
			get
			{
				return _BatchNo;
			}
			set
			{
				_BatchNo=value;
			}
		}

	    /// <summary>
		/// 原付款交易号
		/// <summary>
		private  string _TradeNoLog;
	 	public string TradeNoLog
		{
			get
			{
				return _TradeNoLog;
			}
			set
			{
				_TradeNoLog=value;
			}
		}

	    /// <summary>
		/// 退款金额
		/// <summary>
		private  decimal _RebackFee;
	 	public decimal RebackFee
		{
			get
			{
				return _RebackFee;
			}
			set
			{
				_RebackFee=value;
			}
		}

	    /// <summary>
		/// 备注
		/// <summary>
		private  string _Remack;
	 	public string Remack
		{
			get
			{
				return _Remack;
			}
			set
			{
				_Remack=value;
			}
		}

	    /// <summary>
		/// 
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
        /// 
        /// <summary>
        private int _FinanceRefundId;
        public int FinanceRefundId
        {
            get
            {
                return _FinanceRefundId;
            }
            set
            {
                _FinanceRefundId = value;
            }
        }
        /// <summary>
        /// 
        /// <summary>
        private  DateTime _RebackTime;
	 	public DateTime RebackTime
		{
			get
			{
				return _RebackTime;
			}
			set
			{
				_RebackTime=value;
			}
		}

	    /// <summary>
		/// 状态：0待退款，1已退款
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
		/// 
		/// <summary>
		private  int _ReBackManId;
	 	public int ReBackManId
		{
			get
			{
				return _ReBackManId;
			}
			set
			{
				_ReBackManId=value;
			}
		}

		#endregion				
	}
}
