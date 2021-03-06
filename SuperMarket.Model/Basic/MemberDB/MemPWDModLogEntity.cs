﻿ /*****************************************
Table  Name:MemPWDModLog
Create Time:2016/8/6 21:12:19
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>MemPWDModLog
	/// This entiy code is generated by codesmith. this entity is MemPWDModLog.
	/// </summary>
	[Serializable()]
	public class MemPWDModLogEntity
	{
	    #region Public Properties	
		// 
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

		// 
		private  int _MemId;
	 	public int MemId
		{
			get
			{
				return _MemId;
			}
			set
			{
				_MemId=value;
			}
		}

		// 旧密码
		private  string _PassWord;
	 	public string PassWord
		{
			get
			{
				return _PassWord;
			}
			set
			{
				_PassWord=value;
			}
		}

		// 新密码
		private  string _NewPassWord;
	 	public string NewPassWord
		{
			get
			{
				return _NewPassWord;
			}
			set
			{
				_NewPassWord=value;
			}
		}

		// 修改时间
		private  DateTime _ModifyTime;
	 	public DateTime ModifyTime
		{
			get
			{
				return _ModifyTime;
			}
			set
			{
				_ModifyTime=value;
			}
		}

		// 修改密码的IP地址
		private  string _IPAddress;
	 	public string IPAddress
		{
			get
			{
				return _IPAddress;
			}
			set
			{
				_IPAddress=value;
			}
		}

		// 客户端
		private  int _ClientType;
	 	public int ClientType
		{
			get
			{
				return _ClientType;
			}
			set
			{
				_ClientType=value;
			}
		}

		#endregion				
	}
}
