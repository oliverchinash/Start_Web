﻿ /*****************************************
Table  Name:StoreBusscope
Create Time:2016/8/1 13:49:39
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>StoreBusscope
	/// This entiy code is generated by codesmith. this entity is StoreBusscope.
	/// </summary>
	[Serializable()]
	public class MemStoreBusscopeEntity
    {
	    #region Public Properties	
		// 商家经营范围
	 	public int Id
		{
			get;
			set;
		}

		// 商家ID
	 	public int MemId
		{
			get;
			set;
		}

		// 分类ID
	 	public int ClassId
		{
			get;
			set;
		}

		#endregion				
	}
}