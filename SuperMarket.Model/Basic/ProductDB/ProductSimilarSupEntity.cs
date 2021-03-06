﻿ /*****************************************
Table  Name:ProductSimilarSup
Create Time:2016/9/8 15:01:43
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductSimilarSup
	/// This entiy code is generated by codesmith. this entity is ProductSimilarSup.
	/// </summary>
	[Serializable()]
	public class ProductSimilarSupEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 互补产品记录表
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
		/// 分类Id
		/// <summary>
		private  int _ClassId;
	 	public int ClassId
		{
			get
			{
				return _ClassId;
			}
			set
			{
				_ClassId=value;
			}
		}

	    /// <summary>
		/// 款式ID
		/// <summary>
		private  int _ProductId;
	 	public int ProductId
        {
			get
			{
				return _ProductId;
			}
			set
			{
                _ProductId = value;
			}
		}

	    /// <summary>
		/// 销量Id
		/// <summary>
		private  int _SalesNum;
	 	public int SalesNum
		{
			get
			{
				return _SalesNum;
			}
			set
			{
				_SalesNum=value;
			}
		}

	    /// <summary>
		/// 
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
