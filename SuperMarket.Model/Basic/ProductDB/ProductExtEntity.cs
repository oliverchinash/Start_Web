﻿ /*****************************************
Table  Name:ProductExt
Create Time:2017/3/15 0:42:16
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductExt
	/// This entiy code is generated by codesmith. this entity is ProductExt.
	/// </summary>
	[Serializable()]
	public class ProductExtEntity
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
		/// 
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
		/// 
		/// <summary>
		private  string _DetailDescrip;
	 	public string DetailDescrip
		{
			get
			{
				return _DetailDescrip;
			}
			set
			{
				_DetailDescrip=value;
			}
		}
         
		#endregion				
	}
}
