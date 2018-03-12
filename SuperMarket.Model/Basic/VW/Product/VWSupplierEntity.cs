using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public class VWStoreEntity
    {
        /// <summary>
        /// 账号
        /// <summary>
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
 

        /// <summary>
        /// QQ
        /// <summary>
        private string _QQ;
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

        /// <summary>
        /// 微信账号
        /// <summary>
        private string _WeChat;
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

        /// <summary>
        /// 手机号码
        /// <summary>
        private string _MobilePhone;
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
        /// <summary>
        /// 手机号码
        /// <summary>
        private string _StoreMobile;
        public string StoreMobile
        {
            get
            {
                return _StoreMobile;
            }
            set
            {
                _StoreMobile = value;
            }
        }
        
        /// <summary>
        /// 密码
        /// <summary>
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

        /// <summary>
        /// 注册时间
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
        /// 最后登录时间
        /// <summary>
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
        /// <summary>
        /// 法人联系电话
        /// <summary>
        private string _LegalMobilePhone;
        public string LegalMobilePhone
        {
            get
            {
                return _LegalMobilePhone;
            }
            set
            {
                _LegalMobilePhone = value;
            }
        }
        /// <summary>
        /// 登录次数
        /// <summary>
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

        /// <summary>
        /// 
        /// <summary>
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

        /// <summary>
        /// 注册的客户端种类，枚举表
        /// <summary>
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

        /// <summary>
        /// 是否是商家
        /// <summary>
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

        /// <summary>
        /// 账号状态：1有效，2待审核，3锁定，
        /// <summary>
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

        /// <summary>
        /// 用户级别(1普通客户，2铜牌客户，3银牌客户，4金牌客户，5钻石客户，10普通商家
        /// <summary>
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

        /// <summary>
        /// 推荐人Id
        /// <summary>
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

        /// <summary>
        /// 
        /// <summary>
        private string _RecommendCode;
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
        /// <summary>
        /// 用户ID
        /// <summary>
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

        /// <summary>
        /// 
        /// <summary>
        private string _Nickname;
        public string Nickname
        {
            get
            {
                return _Nickname;
            }
            set
            {
                _Nickname = value;
            }
        }

        /// <summary>
        /// 用户姓名
        /// <summary>
        private string _MemName;
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

        /// <summary>
        /// 身份证号
        /// <summary>
        private string _IdentityNo;
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

        /// <summary>
        /// 1男2女
        /// <summary>
        private int _Sex;
        public int Sex
        {
            get
            {
                return _Sex;
            }
            set
            {
                _Sex = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private DateTime _Birthday;
        public DateTime Birthday
        {
            get
            {
                return _Birthday;
            }
            set
            {
                _Birthday = value;
            }
        }

        /// <summary>
        /// 电话
        /// <summary>
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
         
        /// <summary>
        /// 是否验证手机号码
        /// <summary>
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

        /// <summary>
        /// 邮箱地址
        /// <summary>
        private string _Email;
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

        /// <summary>
        /// 是否验证邮箱地址
        /// <summary>
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
 

        /// <summary>
        /// QQ号码是否验证
        /// <summary>
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
         

        /// <summary>
        /// 
        /// <summary>
        private string _IdentityPre;
        public string IdentityPre
        {
            get
            {
                return _IdentityPre;
            }
            set
            {
                _IdentityPre = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _IdentityBeh;
        public string IdentityBeh
        {
            get
            {
                return _IdentityBeh;
            }
            set
            {
                _IdentityBeh = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _IdentityAutoName;
        public string IdentityAutoName
        {
            get
            {
                return _IdentityAutoName;
            }
            set
            {
                _IdentityAutoName = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _IdentityAutoNo;
        public string IdentityAutoNo
        {
            get
            {
                return _IdentityAutoNo;
            }
            set
            {
                _IdentityAutoNo = value;
            }
        }
        /// <summary>
        /// 店铺名称
        /// <summary>
        private string _StoreName;
        public string StoreName
        {
            get
            {
                return _StoreName;
            }
            set
            {
                _StoreName = value;
            }
        }

        /// <summary>
        /// 公司名称
        /// <summary>
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

        /// <summary>
        /// 法人姓名
        /// <summary>
        private string _LegalName;
        public string LegalName
        {
            get
            {
                return _LegalName;
            }
            set
            {
                _LegalName = value;
            }
        }

        /// <summary>
        /// 法人身份证号
        /// <summary>
        private string _LegalIdentityNo;
        public string LegalIdentityNo
        {
            get
            {
                return _LegalIdentityNo;
            }
            set
            {
                _LegalIdentityNo = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _LegalIdentityPre;
        public string LegalIdentityPre
        {
            get
            {
                return _LegalIdentityPre;
            }
            set
            {
                _LegalIdentityPre = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _LegalIdentityBeh;
        public string LegalIdentityBeh
        {
            get
            {
                return _LegalIdentityBeh;
            }
            set
            {
                _LegalIdentityBeh = value;
            }
        }

        

        /// <summary>
        /// 
        /// <summary>
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

        /// <summary>
        /// 国家
        /// <summary>
        private int _Country;
        public int Country
        {
            get
            {
                return _Country;
            }
            set
            {
                _Country = value;
            }
        }

        /// <summary>
        /// 省份
        /// <summary>
        private int _ProvinceId;
        public int ProvinceId
        {
            get
            {
                return _ProvinceId;
            }
            set
            {
                _ProvinceId = value;
            }
        }

        /// <summary>
        /// 城市
        /// <summary>
        private int _CityId;
        public int CityId
        {
            get
            {
                return _CityId;
            }
            set
            {
                _CityId = value;
            }
        }

        /// <summary>
        /// 区县
        /// <summary>
        private string _District;
        public string District
        {
            get
            {
                return _District;
            }
            set
            {
                _District = value;
            }
        }

        /// <summary>
        /// 详细地址
        /// <summary>
        private string _Address;
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
            }
        }

        /// <summary>
        /// 经度
        /// <summary>
        private decimal _Longitude;
        public decimal Longitude
        {
            get
            {
                return _Longitude;
            }
            set
            {
                _Longitude = value;
            }
        }

        /// <summary>
        /// 纬度
        /// <summary>
        private decimal _Latitude;
        public decimal Latitude
        {
            get
            {
                return _Latitude;
            }
            set
            {
                _Latitude = value;
            }
        }
        
        /// <summary>
        /// 营业执照编号
        /// <summary>
        private string _LicenseNo;
        public string LicenseNo
        {
            get
            {
                return _LicenseNo;
            }
            set
            {
                _LicenseNo = value;
            }
        }
        /// <summary>
        /// 营业执照路径
        /// <summary>
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
 
        /// <summary>
        /// 商家级别(1普通商家，2铜牌商家，3银牌商家，4金牌商家，5钻石商家 
        /// <summary>
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
         
    }
}
