﻿ /*****************************************
Table  Name:StatisticTrade
Create Time:2017/6/17 11:02:09
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>StatisticTrade
	/// This entiy code is generated by codesmith. this entity is StatisticTrade.
	/// </summary>
	[Serializable()]
	public class StatisticTradeEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 临时使用的统计交易额
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
		/// 
		/// <summary>
		private  DateTime _StatisticDate;
	 	public DateTime StatisticDate
		{
			get
			{
				return _StatisticDate;
			}
			set
			{
				_StatisticDate=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  decimal _TradeAmount;
	 	public decimal TradeAmount
		{
			get
			{
				return _TradeAmount;
			}
			set
			{
				_TradeAmount=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _TradeNum;
	 	public int TradeNum
		{
			get
			{
				return _TradeNum;
			}
			set
			{
				_TradeNum=value;
			}
		}

		#endregion				
	}
}
