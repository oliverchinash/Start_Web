﻿ /*****************************************
Table  Name:MemberInfo
Create Time:2016/8/9 10:45:22
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>MemberInfo
	/// This entiy code is generated by codesmith. this entity is MemberInfo.
	/// </summary>
	[Serializable()]
	public class MemberInfoEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 用户基本信息表
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
		/// 用户ID
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
		/// 
		/// <summary>
		private  string _Nickname;
	 	public string Nickname
		{
			get
			{
				return _Nickname;
			}
			set
			{
				_Nickname=value;
			}
		}

	    /// <summary>
		/// 用户姓名
		/// <summary>
		private  string _MemName;
	 	public string MemName
		{
			get
			{
				return _MemName;
			}
			set
			{
				_MemName=value;
			}
		}

	    /// <summary>
		/// 身份证号
		/// <summary>
		private  string _IdentityNo;
	 	public string IdentityNo
		{
			get
			{
				return _IdentityNo;
			}
			set
			{
				_IdentityNo=value;
			}
		}

	    /// <summary>
		/// 是否已实名认证0未实名1认证中2已实名
		/// <summary>
		private  int _IdentityNoCheck;
	 	public int IdentityNoCheck
		{
			get
			{
				return _IdentityNoCheck;
			}
			set
			{
				_IdentityNoCheck=value;
			}
		}

	    /// <summary>
		/// 1男2女
		/// <summary>
		private  int _Sex;
	 	public int Sex
		{
			get
			{
				return _Sex;
			}
			set
			{
				_Sex=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  DateTime _Birthday;
	 	public DateTime Birthday
		{
			get
			{
				return _Birthday;
			}
			set
			{
				_Birthday=value;
			}
		}

	    /// <summary>
		/// 电话
		/// <summary>
		private  string _Telephone;
	 	public string Telephone
		{
			get
			{
				return _Telephone;
			}
			set
			{
				_Telephone=value;
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
		/// 是否验证手机号码
		/// <summary>
		private  int _MobilePhoneCheck;
	 	public int MobilePhoneCheck
		{
			get
			{
				return _MobilePhoneCheck;
			}
			set
			{
				_MobilePhoneCheck=value;
			}
		}

	    /// <summary>
		/// 邮箱地址
		/// <summary>
		private  string _Email;
	 	public string Email
		{
			get
			{
				return _Email;
			}
			set
			{
				_Email=value;
			}
		}

	    /// <summary>
		/// 是否验证邮箱地址
		/// <summary>
		private  int _EmailCheck;
	 	public int EmailCheck
		{
			get
			{
				return _EmailCheck;
			}
			set
			{
				_EmailCheck=value;
			}
		}

	    /// <summary>
		/// QQ号码
		/// <summary>
		private  string _QQ;
	 	public string QQ
		{
			get
			{
				return _QQ;
			}
			set
			{
				_QQ=value;
			}
		}

	    /// <summary>
		/// QQ号码是否验证
		/// <summary>
		private  int _QQCheck;
	 	public int QQCheck
		{
			get
			{
				return _QQCheck;
			}
			set
			{
				_QQCheck=value;
			}
		}

	    /// <summary>
		/// 微信编号
		/// <summary>
		private  string _WebChart;
	 	public string WebChart
		{
			get
			{
				return _WebChart;
			}
			set
			{
				_WebChart=value;
			}
		}

	    /// <summary>
		/// 微信编号是否审核绑定
		/// <summary>
		private  int _WebChartCheck;
	 	public int WebChartCheck
		{
			get
			{
				return _WebChartCheck;
			}
			set
			{
				_WebChartCheck=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _IdentityPre;
	 	public string IdentityPre
		{
			get
			{
				return _IdentityPre;
			}
			set
			{
				_IdentityPre=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _IdentityBeh;
	 	public string IdentityBeh
		{
			get
			{
				return _IdentityBeh;
			}
			set
			{
				_IdentityBeh=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _IdentityAutoName;
	 	public string IdentityAutoName
		{
			get
			{
				return _IdentityAutoName;
			}
			set
			{
				_IdentityAutoName=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _IdentityAutoNo;
	 	public string IdentityAutoNo
		{
			get
			{
				return _IdentityAutoNo;
			}
			set
			{
				_IdentityAutoNo=value;
			}
		}
        /// <summary>
        /// 
        /// <summary>
        private string _HeadPicUrl;
        public string HeadPicUrl
        {
            get
            {
                return _HeadPicUrl;
            }
            set
            {
                _HeadPicUrl = value;
            }
        }

        #endregion
    }
}
