﻿ /*****************************************
Table  Name:Store
Create Time:2016/8/11 13:00:10
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>Store
	/// This entiy code is generated by codesmith. this entity is Store.
	/// </summary>
	[Serializable()]
	public class StoreEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 
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
		/// 账号ID
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
        /// 店铺名称
        /// <summary>
        private  string _StoreName;
	 	public string StoreName
		{
			get
			{
				return _StoreName;
			}
			set
			{
				_StoreName=value;
			}
		}

	    /// <summary>
		/// 公司名称
		/// <summary>
		private  string _CompanyName;
	 	public string CompanyName
		{
			get
			{
				return _CompanyName;
			}
			set
			{
				_CompanyName=value;
			}
		}

	    /// <summary>
		/// 法人姓名
		/// <summary>
		private  string _LegalName;
	 	public string LegalName
		{
			get
			{
				return _LegalName;
			}
			set
			{
				_LegalName=value;
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
        /// 法人身份证号
        /// <summary>
        private  string _LegalIdentityNo;
	 	public string LegalIdentityNo
		{
			get
			{
				return _LegalIdentityNo;
			}
			set
			{
				_LegalIdentityNo=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _LegalIdentityPre;
	 	public string LegalIdentityPre
		{
			get
			{
				return _LegalIdentityPre;
			}
			set
			{
				_LegalIdentityPre=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _LegalIdentityBeh;
	 	public string LegalIdentityBeh
		{
			get
			{
				return _LegalIdentityBeh;
			}
			set
			{
				_LegalIdentityBeh=value;
			}
		}

	    /// <summary>
		/// 联系电话
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
		/// 
		/// <summary>
		private  string _ContactsManName;
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
		private  int _Country;
	 	public int Country
		{
			get
			{
				return _Country;
			}
			set
			{
				_Country=value;
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
				_District=value;
			}
		}

	    /// <summary>
		/// 详细地址
		/// <summary>
		private  string _Address;
	 	public string Address
		{
			get
			{
				return _Address;
			}
			set
			{
				_Address=value;
			}
		}

	    /// <summary>
		/// 经度
		/// <summary>
		private  decimal _Longitude;
	 	public decimal Longitude
		{
			get
			{
				return _Longitude;
			}
			set
			{
				_Longitude=value;
			}
		}

	    /// <summary>
		/// 纬度
		/// <summary>
		private  decimal _Latitude;
	 	public decimal Latitude
		{
			get
			{
				return _Latitude;
			}
			set
			{
				_Latitude=value;
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
        private  string _LicensePath;
	 	public string LicensePath
		{
			get
			{
				return _LicensePath;
			}
			set
			{
				_LicensePath=value;
			}
		}

	    /// <summary>
		/// 状态 0待审核，1审核通过，2审核中3审核拒绝
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
        public string StatusName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((MemberStatus)Status);
            }
        }
        /// <summary>
        /// 商家级别(1普通商家，2铜牌商家，3银牌商家，4金牌商家，5钻石商家 
        /// <summary>
        private  int _GradeLevel;
	 	public int GradeLevel
		{
			get
			{
				return _GradeLevel;
			}
			set
			{
				_GradeLevel=value;
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
        /// 企业类型
        /// <summary>
        private int _StoreType;
        public int StoreType
        {
            get
            {
                return _StoreType;
            }
            set
            {
                _StoreType = value;
            }
        } 
        public string StoreTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((CompanyType)StoreType);
            }
        }
        #endregion
    }
}