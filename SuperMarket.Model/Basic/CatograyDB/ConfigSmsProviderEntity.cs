﻿ /*****************************************
Table  Name:ConfigSmsProvider
Create Time:2017/4/18 14:23:04
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ConfigSmsProvider
	/// This entiy code is generated by codesmith. this entity is ConfigSmsProvider.
	/// </summary>
	[Serializable()]
	public class ConfigSmsProviderEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 短信发送提供商配置信息表
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
		/// 服务号
		/// <summary>
		private  string _AppId;
	 	public string AppId
		{
			get
			{
				return _AppId;
			}
			set
			{
				_AppId=value;
			}
		}

	    /// <summary>
		/// 用户号码
		/// <summary>
		private  string _UserCode;
	 	public string UserCode
		{
			get
			{
				return _UserCode;
			}
			set
			{
				_UserCode=value;
			}
		}

	    /// <summary>
		/// 名称标注:唯一键值,与程序对接
		/// <summary>
		private  string _Name;
	 	public string Name
		{
			get
			{
				return _Name;
			}
			set
			{
				_Name=value;
			}
		}

	    /// <summary>
		/// 调用接口
		/// <summary>
		private  string _Url;
	 	public string Url
		{
			get
			{
				return _Url;
			}
			set
			{
				_Url=value;
			}
		}

	    /// <summary>
		/// 密码
		/// <summary>
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
		/// 是否有效
		/// <summary>
		private  int _IsActive;
	 	public int IsActive
		{
			get
			{
				return _IsActive;
			}
			set
			{
				_IsActive=value;
			}
		}

	    /// <summary>
		/// 排序
		/// <summary>
		private  int _Sort;
	 	public int Sort
		{
			get
			{
				return _Sort;
			}
			set
			{
				_Sort=value;
			}
		}
        /// <summary>
        /// 通道类型
        /// <summary>
        private int _SMSType;
        public int SMSType
        {
            get
            {
                return _SMSType;
            }
            set
            {
                _SMSType = value;
            }
        }
        
        #endregion
    }
}
