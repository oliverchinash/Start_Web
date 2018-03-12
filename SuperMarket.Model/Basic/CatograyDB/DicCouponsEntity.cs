﻿ /*****************************************
Table  Name:DicCoupons
Create Time:2017/3/25 18:44:42
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>DicCoupons
	/// This entiy code is generated by codesmith. this entity is DicCoupons.
	/// </summary>
	[Serializable()]
	public class DicCouponsEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 优惠券记录表
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
		/// 编码(以备后面用户直接输入编号扩展使用)
		/// <summary>
		private  string _Code;
	 	public string Code
		{
			get
			{
				return _Code;
			}
			set
			{
				_Code=value;
			}
		}

	    /// <summary>
		/// 名称，页面显示的优惠券名称
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
		/// 优惠券类型：1金额优惠券，2折扣券
		/// <summary>
		private  int _CouponType;
	 	public int CouponType
		{
			get
			{
				return _CouponType;
			}
			set
			{
				_CouponType=value;
			}
		}

	    /// <summary>
		/// 优惠券值
		/// <summary>
		private  decimal _CouponValue;
	 	public decimal CouponValue
		{
			get
			{
				return _CouponValue;
			}
			set
			{
				_CouponValue=value;
			}
		}

	    /// <summary>
		/// 最低使用金额
		/// <summary>
		private  decimal _MinimumReqAmount;
	 	public decimal MinimumReqAmount
		{
			get
			{
				return _MinimumReqAmount;
			}
			set
			{
				_MinimumReqAmount=value;
			}
		}

	    /// <summary>
		/// 适用范围 ，1全订单适用，2固定产品类别适用，3固定品牌适用，4固定产品适用 
		/// <summary>
		private  int _ClassId;
	 	public int ClassId
		{
			get
			{
				return _ClassId;
			}
			set
			{
				_ClassId=value;
			}
		}
        /// <summary>
        /// 品类品类名称 
        /// <summary>
        private string _ClassName;
        public string ClassName
        {
            get
            {
                return _ClassName;
            }
            set
            {
                _ClassName = value;
            }
        }
        /// <summary>
        /// 限品牌
        /// <summary>
        private  int _BrandId;
	 	public int BrandId
		{
			get
			{
				return _BrandId;
			}
			set
			{
				_BrandId=value;
			}
		}
        /// <summary>
        /// 限品牌
        /// <summary>
        private string _BrandName;
        public string BrandName
        {
            get
            {
                return _BrandName;
            }
            set
            {
                _BrandName = value;
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
		/// 排序（倒叙）
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
		/// 有效时间:数量
		/// <summary>
		private  int _EffectiveTime;
	 	public int EffectiveTime
		{
			get
			{
				return _EffectiveTime;
			}
			set
			{
				_EffectiveTime=value;
			}
		}

	    /// <summary>
		/// 有效时间单位:1天,2月,3年,4时
		/// <summary>
		private  int _EffectiveUnit;
	 	public int EffectiveUnit
		{
			get
			{
				return _EffectiveUnit;
			}
			set
			{
				_EffectiveUnit=value;
			}
		}
        private int _EffectiveType;
        public int EffectiveType
        {
            get
            {
                return _EffectiveType;
            }
            set
            {
                _EffectiveType = value;
            }
        }
        private DateTime _EffectiveEndTime;
        public DateTime EffectiveEndTime
        {
            get
            {
                return _EffectiveEndTime;
            }
            set
            {
                _EffectiveEndTime = value;
            }
        }
        #endregion
    }
}
