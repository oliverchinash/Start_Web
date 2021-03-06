﻿ /*****************************************
Table  Name:ProductFineModule
Create Time:2018/7/11 18:06:25
Creator    :lgzh
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductFineModule
	/// This entiy code is generated by codesmith. this entity is ProductFineModule.
	/// </summary>
	[Serializable()]
	public class ProductFineModuleEntity
	{
	    #region Public Properties	
		// 产品模块管理表
	 	public int Id
		{
			get;
			set;
		}

		// 名称
	 	public string Name
		{
			get;
			set;
		}

		// 排序
	 	public int Sort
		{
			get;
			set;
		}

		// 
	 	public string PicUrl
		{
			get;
			set;
		}

		// 
	 	public int Status
		{
			get;
			set;
		}

		// 模块类型
	 	public int ModuleType
		{
			get;
			set;
		}

		// 
	 	public int SiteId
		{
			get;
			set;
		}

		#endregion				
	}
}
