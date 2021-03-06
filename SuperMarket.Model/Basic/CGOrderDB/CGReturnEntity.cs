﻿ /*****************************************
Table  Name:CGReturn
Create Time:2017/1/20 16:26:09
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>CGReturn
	/// This entiy code is generated by codesmith. this entity is CGReturn.
	/// </summary>
	[Serializable()]
	public class CGReturnEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 采购退换货接口表
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
		private int _ProductId;
        public int ProductId
        {
            get
            {
                return _ProductId;
            }
            set
            {
                _ProductId = value;
            }
        }
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
            }
        }
       
        /// <summary>
        /// B2B平台的退换货Id
        /// <summary>
        private int _B2BReturnXSId;
	 	public int B2BReturnXSId
		{
			get
			{
				return _B2BReturnXSId;
			}
			set
			{
				_B2BReturnXSId=value;
			}
		}
        private long _B2BNewOrderCode;
        public long B2BNewOrderCode
        {
            get
            {
                return _B2BNewOrderCode;
            }
            set
            {
                _B2BNewOrderCode = value;
            }
        }
        private int _ReturnType;
        public int ReturnType
        {
            get
            {
                return _ReturnType;
            }
            set
            {
                _ReturnType = value;
            }
        }

        private int _ReturnNum;
        public int ReturnNum
        {
            get
            {
                return _ReturnNum;
            }
            set
            {
                _ReturnNum = value;
            }
        }
        private int _Num;
        public int Num
        {
            get
            {
                return _Num;
            }
            set
            {
                _Num = value;
            }
        }
        /// <summary>
        /// 状态，0待申请，1申请通过，2拒绝，3已收货，4已寄出，100结束
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
        private string _Remark;
        public string Remark
        {
            get
            {
                return _Remark;
            }
            set
            {
                _Remark = value;
            }
        }
        public long CGOrderCode
        {
            get;set;
        }
        public int ProvinceId
        {
            get; set;
        }

        public string ProvinceName
        {
            get; set;
        }

        public int CityId
        {
            get; set;
        }

        public string CityName
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }
        public string ManName
        {
            get; set;
        }
        public string Phone
        {
            get; set;
        }
        public DateTime CreateTime
        {
            get; set;
        }
         
        #endregion
    }
}
