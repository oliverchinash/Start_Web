﻿ /*****************************************
Table  Name:InquiryRecords
Create Time:2017/7/4 15:23:05
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>InquiryRecords
	/// This entiy code is generated by codesmith. this entity is InquiryRecords.
	/// </summary>
	[Serializable()]
	public class InquiryRecordsEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 客户询价表
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
		/// 客户Id
		/// <summary>
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

	    /// <summary>
		/// 联系手机号码
		/// <summary>
		private  string _MobilePhone;
	 	public string MobilePhone
		{
			get
			{
				return _MobilePhone;
			}
			set
			{
				_MobilePhone=value;
			}
		}

	    /// <summary>
		/// 备注
		/// <summary>
		private  string _Remark;
	 	public string Remark
		{
			get
			{
				return _Remark;
			}
			set
			{
				_Remark=value;
			}
		}

	    /// <summary>
		/// 产品Id
		/// <summary>
		private  int _ProductDetailId;
	 	public int ProductDetailId
		{
			get
			{
				return _ProductDetailId;
			}
			set
			{
				_ProductDetailId=value;
			}
		}

	    /// <summary>
		/// 询价时间
		/// <summary>
		private  DateTime _CreatTime;
	 	public DateTime CreatTime
		{
			get
			{
				return _CreatTime;
			}
			set
			{
				_CreatTime=value;
			}
		}

	    /// <summary>
		/// 状态：1询价中，2已回复，3作废
		/// <summary>
		private  int _Status;
	 	public int Status
		{
			get
			{
				return _Status;
			}
			set
			{
				_Status=value;
			}
		}

	    /// <summary>
		/// 结果反馈内容
		/// <summary>
		private  string _ReplyContent;
	 	public string ReplyContent
		{
			get
			{
				return _ReplyContent;
			}
			set
			{
				_ReplyContent=value;
			}
		}

		#endregion				
	}
}
