﻿ /*****************************************
Table  Name:DisCountInfo
Create Time:2016/8/16 13:54:28
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>DisCountInfo
	/// This entiy code is generated by codesmith. this entity is DisCountInfo.
	/// </summary>
	[Serializable()]
	public class DisCountInfoEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 折扣码表
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
		/// 优惠码
		/// <summary>
		private  string _PreferentialCode;
	 	public string PreferentialCode
		{
			get
			{
				return _PreferentialCode;
			}
			set
			{
				_PreferentialCode=value;
			}
		}

		#endregion				
	}
}
