﻿ /*****************************************
Table  Name:PartsCarBrand
Create Time:2017/9/4 10:28:29
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>PartsCarBrand
	/// This entiy code is generated by codesmith. this entity is PartsCarBrand.
	/// </summary>
	[Serializable()]
	public class PartsCarBrandEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 对应车型配件相关选择次数
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
		/// 车型品牌Id
		/// <summary>
		private  int _CarBrandId;
	 	public int CarBrandId
		{
			get
			{
				return _CarBrandId;
			}
			set
			{
				_CarBrandId=value;
			}
		}

	    /// <summary>
		/// 配件基础表Id
		/// <summary>
		private  int _PartFoundId;
	 	public int PartFoundId
		{
			get
			{
				return _PartFoundId;
			}
			set
			{
				_PartFoundId=value;
			}
		}

	    /// <summary>
		/// 选择次数
		/// <summary>
		private  int _BuyTimes;
	 	public int BuyTimes
		{
			get
			{
				return _BuyTimes;
			}
			set
			{
				_BuyTimes=value;
			}
		}

	    /// <summary>
		/// 排序
		/// <summary>
		private  int _Sort;
	 	public int Sort
		{
			get
			{
				return _Sort;
			}
			set
			{
				_Sort=value;
			}
		}

	    /// <summary>
		/// 热门
		/// <summary>
		private  int _IsHot;
	 	public int IsHot
		{
			get
			{
				return _IsHot;
			}
			set
			{
				_IsHot=value;
			}
		}

		#endregion				
	}
}
