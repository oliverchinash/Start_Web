﻿ /*****************************************
Table  Name:ConfirmOrderProduct
Create Time:2017/10/12 14:15:17
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ConfirmOrderProduct
	/// This entiy code is generated by codesmith. this entity is ConfirmOrderProduct.
	/// </summary>
	[Serializable()]
	public class ConfirmOrderProductEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 询价产品记录
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
		///  订单编号
		/// <summary>
		private  string _ConfirmOrderCode;
	 	public string ConfirmOrderCode
        {
			get
			{
				return _ConfirmOrderCode;
			}
			set
			{
                _ConfirmOrderCode = value;
			}
		}
        /// <summary>
        ///  询价单编号
        /// <summary>
        private string _InquiryOrderCode;
        public string InquiryOrderCode
        {
            get
            {
                return _InquiryOrderCode;
            }
            set
            {
                _InquiryOrderCode = value;
            }
        }
        /// <summary>
        /// 产品编码
        /// <summary>
        private  string _ProductCode;
	 	public string ProductCode
		{
			get
			{
				return _ProductCode;
			}
			set
			{
				_ProductCode=value;
			}
		}

	    /// <summary>
		/// 产品对应所属元器件Id
		/// <summary>
		private  int _ClassesId;
	 	public int ClassesId
		{
			get
			{
				return _ClassesId;
			}
			set
			{
				_ClassesId=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  string _ClassesName;
	 	public string ClassesName
		{
			get
			{
				return _ClassesName;
			}
			set
			{
				_ClassesName=value;
			}
		}

	    /// <summary>
		/// 
		/// <summary>
		private  int _ProductId;
	 	public int ProductId
		{
			get
			{
				return _ProductId;
			}
			set
			{
				_ProductId=value;
			}
		}

	    /// <summary>
		/// 产品名称
		/// <summary>
		private  string _ProductName;
	 	public string ProductName
		{
			get
			{
				return _ProductName;
			}
			set
			{
				_ProductName=value;
			}
		}

	    /// <summary>
		///  配件品质
		/// <summary>
		private  int _ProductType;
	 	public int ProductType
		{
			get
			{
				return _ProductType;
			}
			set
			{
				_ProductType=value;
			}
		}
        public string  ProductTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((InquiryProductTypeEnum)ProductType);
            }
        }
        /// <summary>
        /// 数量
        /// <summary>
        private  int _ProductNum;
	 	public int ProductNum
		{
			get
			{
				return _ProductNum;
			}
			set
			{
				_ProductNum=value;
			}
		}

	    /// <summary>
		/// 单位
		/// <summary>
		private  string _ProductUnitName;
	 	public string ProductUnitName
		{
			get
			{
				return _ProductUnitName;
			}
			set
			{
				_ProductUnitName=value;
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
		/// 
		/// <summary>
		private  int _CreateManId;
	 	public int CreateManId
		{
			get
			{
				return _CreateManId;
			}
			set
			{
				_CreateManId=value;
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
        private decimal _CGPrice;
        public decimal CGPrice
        {
            get
            {
                return _CGPrice;
            }
            set
            {
                _CGPrice = value;
            }
        }    /// <summary>
             /// 
             /// <summary>
        private int _CGMemId;
        public int CGMemId
        {
            get
            {
                return _CGMemId;
            }
            set
            {
                _CGMemId = value;
            }
        }
        /// 
        /// <summary>
        private decimal _Price;
        public decimal Price
        {
            get
            {
                return _Price;
            }
            set
            {
                _Price = value;
            }
        }
        #endregion
    }
}
