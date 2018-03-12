﻿ /*****************************************
Table  Name:ProductSpec
Create Time:2016/8/23 16:49:59
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductSpec
	/// This entiy code is generated by codesmith. this entity is ProductSpec.
	/// </summary>
	[Serializable()]
	public partial class ProductSpecEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 产品规格表
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
		/// 规格Id
		/// <summary>
		private  int _SpecId;
	 	public int SpecId
		{
			get
			{
				return _SpecId;
			}
			set
			{
				_SpecId=value;
			}
		}

	    /// <summary>
		/// 规格值的Id
		/// <summary>
		private  int _SpecDetailId;
	 	public int SpecDetailId
		{
			get
			{
				return _SpecDetailId;
			}
			set
			{
				_SpecDetailId=value;
			}
		}

	    /// <summary>
		/// 产品Id
		/// <summary>
		private  int _ProId;
	 	public int ProId
		{
			get
			{
				return _ProId;
			}
			set
			{
				_ProId=value;
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

		#endregion				
	}
}