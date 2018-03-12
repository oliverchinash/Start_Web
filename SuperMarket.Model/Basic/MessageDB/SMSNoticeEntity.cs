﻿ /*****************************************
Table  Name:SMSNotice
Create Time:2017/1/18 11:56:46
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>SMSNotice
	/// This entiy code is generated by codesmith. this entity is SMSNotice.
	/// </summary>
	[Serializable()]
	public class SMSNoticeEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 短信通知记录表
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
		/// 手机号码
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
		/// 短信内容
		/// <summary>
		private  string _SMSContent;
	 	public string SMSContent
		{
			get
			{
				return _SMSContent;
			}
			set
			{
				_SMSContent=value;
			}
		}

	    /// <summary>
		/// 状态，0未发，1 已发
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
		/// 发送时间
		/// <summary>
		private  DateTime _SendTime;
	 	public DateTime SendTime
		{
			get
			{
				return _SendTime;
			}
			set
			{
				_SendTime=value;
			}
		}

	    /// <summary>
		/// 系统类别：1B2B平台，2采购平台
		/// <summary>
		private  int _SystemType;
	 	public int SystemType
		{
			get
			{
				return _SystemType;
			}
			set
			{
				_SystemType=value;
			}
		}
        /// <summary>
        /// 
        /// <summary>
        private DateTime _CreateTime;
        public DateTime CreateTime
        {
            get
            {
                return _CreateTime;
            }
            set
            {
                _CreateTime = value;
            }
        }
        /// <summary>
        /// fanhuizhi
        /// <summary>
        private string _ReturnMsg;
        public string ReturnMsg
        {
            get
            {
                return _ReturnMsg;
            }
            set
            {
                _ReturnMsg = value;
            }
        }
        /// <summary>
        /// fanhuizhi
        /// <summary>
        private string _SendProvider;
        public string SendProvider
        {
            get
            {
                return _SendProvider;
            }
            set
            {
                _SendProvider = value;
            }
        }
        #endregion
    }
}
