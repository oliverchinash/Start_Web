﻿ /*****************************************
Table  Name:WeChatTokenLog
Create Time:2017/8/26 9:43:19
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>WeChatTokenLog
	/// This entiy code is generated by codesmith. this entity is WeChatTokenLog.
	/// </summary>
	[Serializable()]
	public class WeChatTokenLogEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 保存每次从微信获取的token
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
		private  string _Appid;
	 	public string Appid
		{
			get
			{
				return _Appid;
			}
			set
			{
				_Appid=value;
			}
		}

	    /// <summary>
		/// Token值
		/// <summary>
		private  string _AccessToken;
	 	public string AccessToken
		{
			get
			{
				return _AccessToken;
			}
			set
			{
				_AccessToken=value;
			}
		}

	    /// <summary>
		/// 生效时间
		/// <summary>
		private  DateTime _CreateTime;
	 	public DateTime CreateTime
		{
			get
			{
				return _CreateTime;
			}
			set
			{
				_CreateTime=value;
			}
		}

	    /// <summary>
		/// 失效时间
		/// <summary>
		private  DateTime _EndTime;
	 	public DateTime EndTime
		{
			get
			{
				return _EndTime;
			}
			set
			{
				_EndTime=value;
			}
		}

		#endregion				
	}
}