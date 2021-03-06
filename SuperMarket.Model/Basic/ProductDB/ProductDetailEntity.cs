﻿ /*****************************************
Table  Name:ProductDetail
Create Time:2016/12/15 10:16:57
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>ProductDetail
	/// This entiy code is generated by codesmith. this entity is ProductDetail.
	/// </summary>
	[Serializable()]
	public class ProductDetailEntity
	{
	    #region Public Properties	
	    /// <summary>
		/// 记录表特价商品
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
		/// 产品ID
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
		/// 特价价格
		/// <summary>
		private  decimal _Price;
	 	public decimal Price
		{
			get
			{
				return _Price;
			}
			set
			{
				_Price=value;
			}
		}
   
        /// <summary>
        /// 批发价
        /// <summary>
        private  decimal _TradePrice;
	 	public decimal TradePrice
		{
			get
			{
				return _TradePrice;
			}
			set
			{
				_TradePrice=value;
			}
		}
        /// <summary>
        /// 经销商购买价
        /// </summary>
        private decimal _DealerPrice;
        public decimal DealerPrice
        {
            get
            {
                return _DealerPrice;
            }
            set
            {
                _DealerPrice = value;
            }
        }
        /// <summary>
        /// 库存
        /// <summary>
        private  int _StockNum;
	 	public int StockNum
		{
			get
			{
				return _StockNum;
			}
			set
			{
				_StockNum=value;
			}
		}

	    /// <summary>
		/// 已售出件数
		/// <summary>
		private  int _SaleNum;
	 	public int SaleNum
		{
			get
			{
				return _SaleNum;
			}
			set
			{
				_SaleNum=value;
			}
		}

	    /// <summary>
		/// 产品种类1普通商品2特价商品
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

	    /// <summary>
		/// 状态（0已下架，1已上架）
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
        private int _IsBP;
        public int IsBP
        {
            get
            {
                return _IsBP;
            }
            set
            {
                _IsBP = value;
            }
        }

        /// <summary>
        /// 
        /// <summary>
        private string _TimeStampCode;
	 	public string TimeStampCode
		{
			get
			{
				return _TimeStampCode;
			}
			set
			{
				_TimeStampCode=value;
			}
		}
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
        #endregion
    }
}
