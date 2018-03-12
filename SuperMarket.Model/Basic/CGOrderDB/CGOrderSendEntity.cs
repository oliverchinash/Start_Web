﻿ /*****************************************
Table  Name:CGOrderSend
Create Time:2016/12/31 9:41:06
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CGOrderSend
	/// This entiy code is generated by codesmith. this entity is CGOrderSend.
	/// </summary>
	[Serializable()]
	public class CGOrderSendEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 采购单发送报价记录
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
		private  Int64 _CGOrderCode;
	 	public Int64 CGOrderCode
		{
			get
			{
				return _CGOrderCode;
			}
			set
			{
				_CGOrderCode=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _CGMemId;
	 	public int CGMemId
		{
			get
			{
				return _CGMemId;
			}
			set
			{
				_CGMemId=value;
			}
		}

	    /// <summary>
		/// 
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
		/// 
		/// <summary>
		private  int _HasRead;
	 	public int HasRead
		{
			get
			{
				return _HasRead;
			}
			set
			{
				_HasRead=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  DateTime _ReadTime;
	 	public DateTime ReadTime
		{
			get
			{
				return _ReadTime;
			}
			set
			{
				_ReadTime=value;
			}
		}

		#endregion				
	}
}
