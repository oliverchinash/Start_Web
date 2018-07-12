﻿ /*****************************************
Table  Name:ProductBaoPin
Create Time:2017/4/15 15:37:40
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductBaoPin
	/// This entiy code is generated by codesmith. this entity is ProductBaoPin.
	/// </summary>
	[Serializable()]
	public class VWProductFineEntity
    {
	    #region Public Properties	
	    /// <summary>
		/// 爆品记录表
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
		/// 产品价格明细表Id
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

        public VWProductEntity ProductDetail;
      
	    /// <summary>
		/// 精选开始时间
		/// <summary>
		private  DateTime _BeginTime;
	 	public DateTime BeginTime
		{
			get
			{
				return _BeginTime;
			}
			set
			{
				_BeginTime=value;
			}
		}

        /// <summary>
        /// 精选结束时间
        /// <summary>
        private DateTime _EndTime;
	 	public DateTime EndTime
		{
			get
			{
				return _EndTime;
			}
			set
			{
				_EndTime=value;
			}
		}
        private decimal _Price;
        public  decimal Price
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
        private int _Sort;
        public int Sort
        {
            get
            {
                return _Sort;
            }
            set
            {
                _Sort = value;
            }
        }
        private int _FineModuleId;
        public int FineModuleId
        {
            get
            {
                return _FineModuleId;
            }
            set
            {
                _FineModuleId = value;
            }
        }
        
        #endregion
    }
}
