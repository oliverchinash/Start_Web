﻿ /*****************************************
Table  Name:AssetChange
Create Time:2016/8/7 17:12:01
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>AssetChange
	/// This entiy code is generated by codesmith. this entity is AssetChange.
	/// </summary>
	[Serializable()]
	public class AssetChangeEntity
	{
	    #region Public Properties	
		// 
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

		// 用户Id
		private  int _MemId;
	 	public int MemId
		{
			get
			{
				return _MemId;
			}
			set
			{
				_MemId=value;
			}
		}

		// 操作种类：1冻结资金变动2余额变动
		private  int _OperateType;
	 	public int OperateType
		{
			get
			{
				return _OperateType;
			}
			set
			{
				_OperateType=value;
			}
		}

		// 影响资金金额（冻结）
		private  decimal _ReChargeAmt;
	 	public decimal ReChargeAmt
		{
			get
			{
				return _ReChargeAmt;
			}
			set
			{
				_ReChargeAmt=value;
			}
		}

		// 支出
		private  decimal _PayAmt;
	 	public decimal PayAmt
		{
			get
			{
				return _PayAmt;
			}
			set
			{
				_PayAmt=value;
			}
		}

		// 原因说明
		private  string _Reason;
	 	public string Reason
		{
			get
			{
				return _Reason;
			}
			set
			{
				_Reason=value;
			}
		}

		// 发生时间
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

		#endregion				
	}
}
