﻿ /*****************************************
Table  Name:GYAuth
Create Time:2016/7/30 10:41:22
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>GYAuth
	/// This entiy code is generated by codesmith. this entity is GYAuth.
	/// </summary>
	[Serializable()]
	public class GYAuthEntity
	{
	    #region Public Properties	
		// 
	 	public int Id
		{
			get;
			set;
		}

		// 权限编码-与网站绑定的编号（不可重复）
	 	public string AuthCode
		{
			get;
			set;
		}

		// 权限名称
	 	public string AuthName
		{
			get;
			set;
		}

		// 是否有效
	 	public int IsActive
		{
			get;
			set;
		}

		// 备注
	 	public string Remark
		{
			get;
			set;
		}

		// 建立时间
	 	public DateTime CreateTime
		{
			get;
			set;
		}

		// 修改时间
	 	public DateTime UpdateTime
		{
			get;
			set;
		}

		// 修改人Id
	 	public int UpdateManId
		{
			get;
			set;
		}

		#endregion				
	}
}
