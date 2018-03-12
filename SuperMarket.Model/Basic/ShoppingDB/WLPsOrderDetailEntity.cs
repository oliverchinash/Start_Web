﻿ /*****************************************
Table  Name:WLPsOrderDetail
Create Time:2016/12/1 21:47:59
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>WLPsOrderDetail
	/// This entiy code is generated by codesmith. this entity is WLPsOrderDetail.
	/// </summary>
	[Serializable()]
	public class WLPsOrderDetailEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 物流配送明细表
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
		/// 订单明细Id
		/// <summary>
		private  int _OrderDetailId;
	 	public int OrderDetailId
		{
			get
			{
				return _OrderDetailId;
			}
			set
			{
				_OrderDetailId=value;
			}
		}

	    /// <summary>
		/// 送货人名称
		/// <summary>
		private  string _SendManName;
	 	public string SendManName
		{
			get
			{
				return _SendManName;
			}
			set
			{
				_SendManName=value;
			}
		}

	    /// <summary>
		/// 送货人联系方式
		/// <summary>
		private  string _SendManPhone;
	 	public string SendManPhone
		{
			get
			{
				return _SendManPhone;
			}
			set
			{
				_SendManPhone=value;
			}
		}

	    /// <summary>
		/// 备注
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
		/// 物流单号码
		/// <summary>
		private  string _TransferCode;
	 	public string TransferCode
		{
			get
			{
				return _TransferCode;
			}
			set
			{
				_TransferCode=value;
			}
		}

	    /// <summary>
		/// 物流公司的Id
		/// <summary>
		private  int _WLCompanyId;
	 	public int WLCompanyId
		{
			get
			{
				return _WLCompanyId;
			}
			set
			{
				_WLCompanyId=value;
			}
		}

	    /// <summary>
		/// 是否携带发票
		/// <summary>
		private  int _HasBill;
	 	public int HasBill
		{
			get
			{
				return _HasBill;
			}
			set
			{
				_HasBill=value;
			}
		}

	    /// <summary>
		/// 默认有效，遇到特殊情况，后台手动设置为失效
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

		#endregion				
	}
}
