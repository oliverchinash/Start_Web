﻿ /*****************************************
Table  Name:ProductWholeSale
Create Time:2016/8/16 13:54:29
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductWholeSale
	/// This entiy code is generated by codesmith. this entity is ProductWholeSale.
	/// </summary>
	[Serializable()]
	public class ProductWholeSaleEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 
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
		/// 产品Id
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
				_ProductId=value;
			}
		}

	    /// <summary>
		/// 第一级批发数量
		/// <summary>
		private  int _BatchNum1;
	 	public int BatchNum1
		{
			get
			{
				return _BatchNum1;
			}
			set
			{
				_BatchNum1=value;
			}
		}

	    /// <summary>
		/// 第一级批发数量对应价格
		/// <summary>
		private  decimal _Price1;
	 	public decimal Price1
		{
			get
			{
				return _Price1;
			}
			set
			{
				_Price1=value;
			}
		}

	    /// <summary>
		/// 第二级批发数量
		/// <summary>
		private  int _BatchNum2;
	 	public int BatchNum2
		{
			get
			{
				return _BatchNum2;
			}
			set
			{
				_BatchNum2=value;
			}
		}

	    /// <summary>
		/// 第二级批发数量对应价格
		/// <summary>
		private  decimal _Price2;
	 	public decimal Price2
		{
			get
			{
				return _Price2;
			}
			set
			{
				_Price2=value;
			}
		}

	    /// <summary>
		/// 第三级批发数量
		/// <summary>
		private  int _BatchNum3;
	 	public int BatchNum3
		{
			get
			{
				return _BatchNum3;
			}
			set
			{
				_BatchNum3=value;
			}
		}

	    /// <summary>
		/// 第三级批发数量对应价格
		/// <summary>
		private  decimal _Price3;
	 	public decimal Price3
		{
			get
			{
				return _Price3;
			}
			set
			{
				_Price3=value;
			}
		}

		#endregion				
	}
}