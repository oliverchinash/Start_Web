﻿ /*****************************************
Table  Name:Product
Create Time:2016/8/23 16:49:59
Creator    :jc001
******************************************/
using System;
using System.Data;
using System.Text; 
 
//?DataEntityattrbute
namespace SuperMarket.Model
{ 	
	/// <summary>Product
	/// This entiy code is generated by codesmith. this entity is Product.
	/// </summary>
	[Serializable()]
	public class ProductEntity
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
        /// 标题
        /// <summary>
        private string _Code;
        public string Code
        {
            get
            {
                return _Code;
            }
            set
            {
                _Code = value;
            }
        }
        /// <summary>
        /// 商品名称
        /// <summary>
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
        /// 标题
        /// <summary>
        private  string _Title;
	 	public string Title
		{
			get
			{
				return _Title;
			}
			set
			{
				_Title=value;
			}
		}

	    /// <summary>
		/// 标题广告
		/// <summary>
		private  string _AdTitle;
	 	public string AdTitle
		{
			get
			{
				return _AdTitle;
			}
			set
			{
				_AdTitle=value;
			}
		}
        /// <summary>
        ///  
        /// <summary>
        private int _IsOEM;
        public int IsOEM
        {
            get
            {
                return _IsOEM;
            }
            set
            {
                _IsOEM = value;
            }
        }
         
        public int ClassId;
        public int BrandId;
        /// <summary>
        /// 出售价格单品
        /// <summary>
        private decimal _MarketPrice;
        public decimal MarketPrice
        {
            get
            {
                return _MarketPrice;
            }
            set
            {
                _MarketPrice = value;
            }
        }
      
        /// <summary>
        /// 成本
        /// <summary>
        private  decimal _Cost;
	 	public decimal Cost
		{
			get
			{
				return _Cost;
			}
			set
			{
				_Cost=value;
			}
		}

	    /// <summary>
		/// 商家Id
		/// <summary>
		private  int _SellerId;
	 	public int SellerId
		{
			get
			{
				return _SellerId;
			}
			set
			{
				_SellerId=value;
			}
		}

	    /// <summary>
		/// 数量(999999代表不限数量)
		/// <summary>
		private  int _Num;
	 	public int Num
		{
			get
			{
				return _Num;
			}
			set
			{
				_Num=value;
			}
		}
        private int _Unit;
        public int Unit
        {
            get
            {
                return _Unit;
            }
            set
            {
                _Unit = value;
            }
        }
        /// <summary>
        /// 包装单位
        /// <summary>
        private decimal _PackUnit;
        public decimal PackUnit
        {
            get
            {
                return _PackUnit;
            }
            set
            {
                _PackUnit = value;
            }
        }
        /// <summary>
        /// 包装包含数量
        /// <summary>
        private decimal _PackNum;
        public decimal PackNum
        {
            get
            {
                return _PackNum;
            }
            set
            {
                _PackNum = value;
            }
        }
        /// <summary>
        ///属性1
        /// <summary>
        private string _Spec1;
        public string Spec1
        {
            get
            {
                return _Spec1;
            }
            set
            {
                _Spec1 = value;
            }
        }
        /// <summary>
        ///属性2
        /// <summary>
        private string _Spec2;
        public string Spec2
        {
            get
            {
                return _Spec2;
            }
            set
            {
                _Spec2 = value;
            }
        }
        /// <summary>
        ///属性3
        /// <summary>
        private string _Spec3;
        public string Spec3
        {
            get
            {
                return _Spec3;
            }
            set
            {
                _Spec3 = value;
            }
        }
        /// <summary>
        /// 图片显示类型1显示独立图片和款式图片的合集，2独立上传图片，3按照款式显示相同图片
        /// <summary>
        private int _ShowPicType;
        public int ShowPicType
        {
            get
            {
                return _ShowPicType;
            }
            set
            {
                _ShowPicType = value;
            }
        }
        /// <summary>
        /// 默认图片路径
        /// <summary>
        private string _PicUrl;
        public string PicUrl
        {
            get
            {
                return _PicUrl;
            }
            set
            {
                _PicUrl = value;
            }
        }
        private string _PicSuffix;
        public string PicSuffix
        {
            get
            {
                return _PicSuffix;
            }
            set
            {
                _PicSuffix = value;
            }
        }
        private int _CarTypeRelated;
        public int CarTypeRelated
        {
            get
            {
                return _CarTypeRelated;
            }
            set
            {
                _CarTypeRelated = value;
            }
        }
        /// <summary>
        /// 是否支持零售
        /// <summary>
        private  int _Retail;
	 	public int Retail
		{
			get
			{
				return _Retail;
			}
			set
			{
				_Retail=value;
			}
		}

	    /// <summary>
		/// 支持批发
		/// <summary>
		private  int _Wholesale;
	 	public int Wholesale
		{
			get
			{
				return _Wholesale;
			}
			set
			{
				_Wholesale=value;
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
        
        private decimal _GrossWeight;
        /// <summary>
        /// 毛重
        /// </summary>
        public decimal GrossWeight
        {
            get
            {
                return _GrossWeight;
            }
            set
            {
                _GrossWeight = value;
            }
        }
    
        /// <summary>
        /// 产地
        /// <summary>
        private string _PlaceOrigin;
        public string PlaceOrigin
        {
            get
            {
                return _PlaceOrigin;
            }
            set
            {
                _PlaceOrigin = value;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlOld
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlBig
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "Big." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlNormal
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "Normal." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "Small." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlLittle
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "Little." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlList
        {
            get
            {
                return string.IsNullOrEmpty(PicUrl) ? "" : PicUrl + "List." + PicSuffix;
            }
        }
       
        #endregion
    }
}
