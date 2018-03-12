﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{ 
/// <summary>Member
/// This entity code is generated by codesmith. this entity is Member.
/// </summary>
    [Serializable()]
    public partial class VWMemberEntity
    {
        #region Public Properties	
        // 
        private int _MemId;
        public int MemId
        {
            get
            {
                return _MemId;
            }
            set
            {
                _MemId = value;
            }
        }
        
        
        // 账号
        private string _MemCode;
        public string MemCode
        {
            get
            {
                return _MemCode;
            }
            set
            {
                _MemCode = value;
            }
        }
        // 昵称
        private string _NickName;
        public string NickName
        {
            get
            {
                return _NickName;
            }
            set
            {
                _NickName = value;
            }
        }
        // 密码
        private string _PassWord;
        public string PassWord
        {
            get
            {
                return _PassWord;
            }
            set
            {
                _PassWord = value;
            }
        }

        // 注册时间
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

        // 最后登录时间
        private DateTime _LastLoginTime;
        public DateTime LastLoginTime
        {
            get
            {
                return _LastLoginTime;
            }
            set
            {
                _LastLoginTime = value;
            }
        }

        // 登录次数
        private int _LoginNum;
        public int LoginNum
        {
            get
            {
                return _LoginNum;
            }
            set
            {
                _LoginNum = value;
            }
        }

        // 
        private string _CreateIp;
        public string CreateIp
        {
            get
            {
                return _CreateIp;
            }
            set
            {
                _CreateIp = value;
            }
        }

        // 注册的客户端种类，枚举表
        private int _CreateClientType;
        public int CreateClientType
        {
            get
            {
                return _CreateClientType;
            }
            set
            {
                _CreateClientType = value;
            }
        }
        
        // 是否是商家
        private int _IsSysUser;
        public int IsSysUser
        {
            get
            {
                return _IsSysUser;
            }
            set
            {
                _IsSysUser = value;
            }
        }
        
        // 是否是商家
        private int _IsStore;
        public int IsStore
        {
            get
            {
                return _IsStore;
            }
            set
            {
                _IsStore = value;
            }
        }

        // 账号状态：1有效，2待审核，3锁定，
        private int _Status;
        public int Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
        public string StatusName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((MemberStatus)Status);
            }


        }
        // 用户级别(1普通客户，2铜牌客户，3银牌客户，4金牌客户，5钻石客户，10普通商家
        private int _MemGrade;
        public int MemGrade
        {
            get
            {
                return _MemGrade;
            }
            set
            {
                _MemGrade = value;
            }
        }

        // 推荐人Id
        private int _RecommendMemId;
        public int RecommendMemId
        {
            get
            {
                return _RecommendMemId;
            }
            set
            {
                _RecommendMemId = value;
            }
        }

        // 
        private string _RecommendCode = "";
        public string RecommendCode
        {
            get
            {
                return _RecommendCode;
            }
            set
            {
                _RecommendCode = value;
            }
        }
        // 用户姓名
        private string _MemName = "";
        public string MemName
        {
            get
            {
                return _MemName;
            }
            set
            {
                _MemName = value;
            }
        }
        // 图像地址
        private string _HeadPicUrl = "";
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
        
        // 身份证号
        private string _IdentityNo="";
        public string IdentityNo
        {
            get
            {
                return _IdentityNo;
            }
            set
            {
                _IdentityNo = value;
            }
        }
        /// <summary>
        /// 是否已实名认证0未实名1认证中2已实名
        /// <summary>
        private int _IdentityNoCheck;
        public int IdentityNoCheck
        {
            get
            {
                return _IdentityNoCheck;
            }
            set
            {
                _IdentityNoCheck = value;
            }
        }
        // 电话
        private string _Telephone;
        public string Telephone
        {
            get
            {
                return _Telephone;
            }
            set
            {
                _Telephone = value;
            }
        }

        // 手机号码
        private string _MobilePhone = "";
        public string MobilePhone
        {
            get
            {
                return _MobilePhone;
            }
            set
            {
                _MobilePhone = value;
            }
        }

        // 是否验证手机号码
        private int _MobilePhoneCheck;
        public int MobilePhoneCheck
        {
            get
            {
                return _MobilePhoneCheck;
            }
            set
            {
                _MobilePhoneCheck = value;
            }
        }

        // 邮箱地址
        private string _Email = "";
        public string Email
        {
            get
            {
                return _Email;
            }
            set
            {
                _Email = value;
            }
        }

        // 是否验证邮箱地址
        private int _EmailCheck;
        public int EmailCheck
        {
            get
            {
                return _EmailCheck;
            }
            set
            {
                _EmailCheck = value;
            }
        }

        // QQ号码
        private string _QQ = "";
        public string QQ
        {
            get
            {
                return _QQ;
            }
            set
            {
                _QQ = value;
            }
        }
       
        // QQ号码是否验证
        private int _QQCheck;
        public int QQCheck
        {
            get
            {
                return _QQCheck;
            }
            set
            {
                _QQCheck = value;
            }
        }

        // 微信编号
        private string _WeChat = "";
        public string WeChat
        {
            get
            {
                return _WeChat;
            }
            set
            {
                _WeChat = value;
            }
        }
 
        // 账号 
        public string MemGradeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((MemberGrade)MemGrade);
            } 
        }
        // 商家级别(1普通商家，2铜牌商家，3银牌商家，4金牌商家，5钻石商家 
        private int _GradeLevel;
        public int GradeLevel
        {
            get
            {
                return _GradeLevel;
            }
            set
            {
                _GradeLevel = value;
            }
        }
        // 商家级别 
        public string GradeLevelName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((StoreGrade)GradeLevel);
            }
        }
        private string _CompanyName;
        public string CompanyName
        {
            get
            {
                return _CompanyName;
            }
            set
            {
                _CompanyName = value;
            }
        }
        private int _CompanyProvinceId;
        public int CompanyProvinceId
        {
            get
            {
                return _CompanyProvinceId;
            }
            set
            {
                _CompanyProvinceId = value;
            }
        }
        private string _CompanyProvinceName;
        public string CompanyProvinceName
        {
            get
            {
                return _CompanyProvinceName;
            }
            set
            {
                _CompanyProvinceName = value;
            }
        }
        private int _CompanyCityId;
        public int CompanyCityId
        {
            get
            {
                return _CompanyCityId;
            }
            set
            {
                _CompanyCityId = value;
            }
        }
        private string _CompanyCityName;
        public string CompanyCityName
        {
            get
            {
                return _CompanyCityName;
            }
            set
            {
                _CompanyCityName = value;
            }
        }
        private string _CompanyAddress;
        public string CompanyAddress
        {
            get
            {
                return _CompanyAddress;
            }
            set
            {
                _CompanyAddress = value;
            }
        }
        private int _CompanyType;
        public int CompanyType
        {
            get
            {
                return _CompanyType;
            }
            set
            {
                _CompanyType = value;
            }
        }
        private int _IsSupplier;
        public int IsSupplier
        {
            get
            {
                return _IsSupplier;
            }
            set
            {
                _IsSupplier = value;
            }
        }
        public string CompanyTypeName
        {
            get
            { 
                return EnumEntityShow.Instance.GetEnumDes((CompanyType)CompanyType); 
            } 
        }
        private string _ContactsManName;
        public string ContactsManName
        {
            get
            {
                return _ContactsManName;
            }
            set
            {
                _ContactsManName = value;
            }
        }

        private string _ContactsMobile;
        public string ContactsMobile
        {
            get
            {
                return _ContactsMobile;
            }
            set
            {
                _ContactsMobile = value;
            }
        }
        private string _LicensePath;
        public string LicensePath
        {
            get
            {
                return _LicensePath;
            }
            set
            {
                _LicensePath = value;
            }
        }
        private string _ContactsEmail;
        public string ContactsEmail
        {
            get
            {
                return _ContactsEmail;
            }
            set
            {
                _ContactsEmail = value;
            }
        }
        private string _TimeStampTab;
        public string TimeStampTab
        {
            get
            {
                return _TimeStampTab;
            }
            set
            {
                _TimeStampTab = value;
            }
        }
        #endregion
    }
}
