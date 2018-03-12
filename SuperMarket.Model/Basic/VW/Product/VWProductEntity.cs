using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable]
    public class VWProductEntity
    {
        #region Public Properties	
        /// <summary>
        /// 
        /// <summary>
        /// 


        //产品Id
        private int _Id;
        public int Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
            }
        }

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
        /// <summary>
        /// 
        /// <summary>
        private int _ProductDetailId;
        public int ProductDetailId
        {
            get
            {
                return _ProductDetailId;
            }
            set
            {
                _ProductDetailId = value;
            }
        }
        /// <summary>
        /// 编号
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
        private string _Title;
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
            }
        }

        /// <summary>
        /// 标题广告
        /// <summary>
        private string _AdTitle;
        public string AdTitle
        {
            get
            {
                return _AdTitle;
            }
            set
            {
                _AdTitle = value;
            }
        }
        /// <summary>
        /// OECode
        /// <summary>
        private string _OECode;
        public string OECode
        {
            get
            {
                return _OECode;
            }
            set
            {
                _OECode = value;
            }
        }
       
        /// <summary>
        /// 款式Id
        /// <summary>
        private int _StyleId;
        public int StyleId
        {
            get
            {
                return _StyleId;
            }
            set
            {
                _StyleId = value;
            }
        }

        /// <summary>
        /// 是否已生成对应的产品静态页或缓存等
        /// <summary>
        private int _HasHtml;
        public int HasHtml
        {
            get
            {
                return _HasHtml;
            }
            set
            {
                _HasHtml = value;
            }
        }
        private decimal _Cost;
        /// <summary>
        /// 成本
        /// </summary>
        public decimal Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
            }
        }
       

        private decimal _MarketPrice;
        /// <summary>
        /// 批发价
        /// </summary>
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
        private decimal _Price;
        /// <summary>
        /// 批发价
        /// </summary>
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
        private decimal _TradePrice;
        /// <summary>
        /// 批发价
        /// </summary>
        public decimal TradePrice
        {
            get
            {
                return _TradePrice;
            }
            set
            {
                _TradePrice = value;
            }
        }
        private decimal _DealerPrice;
        /// <summary>
        /// 经销商批发价
        /// </summary>
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
        
        private int _IsOEM;
        /// <summary>
        /// 批发价
        /// </summary>
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
        private int _ProductType;
        /// <summary>
        /// 批发价
        /// </summary>
        public int ProductType
        {
            get
            {
                return _ProductType;
            }
            set
            {
                _ProductType = value;
            }
        }
        private decimal _ActualPrice;
        /// <summary>
        /// 实际售价
        /// </summary>
        public decimal ActualPrice
        {
            get
            {
                return _ActualPrice;
            }
            set
            {
                _ActualPrice = value;
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
        
        private int _ClassId;
        public int ClassId
        {
            get
            {
                return _ClassId;
            }
            set
            {
                _ClassId = value;
            }
        }
        private int _BrandId;
        public int BrandId
        {
            get
            {
                return _BrandId;
            }
            set
            {
                _BrandId = value;
            }
        }
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
        /// 是否包邮
        /// <summary>
        private int _TransFeeType;
        public int TransFeeType
        {
            get
            {
                return _TransFeeType;
            }
            set
            {
                _TransFeeType = value;
            }
        }
        /// <summary>
        /// 邮费
        /// <summary>
        private decimal _TransFee;
        public decimal TransFee
        {
            get
            {
                return _TransFee;
            }
            set
            {
                _TransFee = value;
            }
        }

        public string TransFeeTypeName
        {
            get
            {
                return EnumEntityShow.Instance.GetEnumDes((TransFeeTypeEnum)TransFeeType);
            }
        }
        /// <summary>
        /// 是否支持零售
        /// <summary>
        private int _Retail;
        public int Retail
        {
            get
            {
                return _Retail;
            }
            set
            {
                _Retail = value;
            }
        }
        /// <summary>
        /// 图片显示类型：1，2，3
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
        /// 序号
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
        private int _ListShowMethod;
        public int ListShowMethod
        {
            get
            {
                return _ListShowMethod;
            }
            set
            {
                _ListShowMethod = value;
            }
        }
        
        private string _UnitName;
        public string UnitName
        {
            get
            {
                return _UnitName;
            }
            set
            {
                _UnitName = value;
            }
        }
        /// <summary>
        /// 序号
        /// <summary>
        private int _SaleNum;
        public int SaleNum
        {
            get
            {
                return _SaleNum;
            }
            set
            {
                _SaleNum = value;
            }
        }
        private int _StockNum;
        /// <summary>
        /// 库存
        /// </summary>
        public int StockNum
        {
            get
            {
                return _StockNum;
            }
            set
            {
                _StockNum = value;
            }
        }
        /// <summary>
        /// 序号
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
        /// 序号
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
        /// 规格1
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
        /// 规格2
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
        /// 规格2
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
        /// 默认图片
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
        /// <summary>
        ///  图片后缀
        /// <summary>
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
        
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlOld
        {
            get
            {
                return  string.IsNullOrEmpty(PicUrl) ?"":PicUrl + "." + PicSuffix;
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
   
        /// <summary>
        /// 支持批发
        /// <summary>
        private int _Wholesale;
        public int Wholesale
        {
            get
            {
                return _Wholesale;
            }
            set
            {
                _Wholesale = value;
            }
        }

        /// <summary>
        /// 是否需要显示尺码表
        /// <summary>
        private int _ShowSizeChart;
        public int ShowSizeChart
        {
            get
            {
                return _ShowSizeChart;
            }
            set
            {
                _ShowSizeChart = value;
            }
        }

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
        public string CGMemNickName
        {
            get;
            set;
        }
        private int _IsAhmTake;
        public int IsAhmTake
        {
            get
            {
                return _IsAhmTake;
            }
            set
            {
                _IsAhmTake = value;
            }
        }
        private int _JiShiSong;
        public int JiShiSong
        {
            get
            {
                return _JiShiSong;
            }
            set
            {
                _JiShiSong = value;
            }
        }
        

        /// <summary>
        /// 介绍
        /// <summary>
        private string _ContentCms;
        public string ContentCms
        {
            get
            {
                return _ContentCms;
            }
            set
            {
                _ContentCms = value;
            }
        }
        

        public IList<ProductStylePicsEntity> ProductPics;
        public IList<ProductPropertyEntity> ProductPropertys;
        public IList<ProductCarTypeEntity> ProductCarTypes;
        #endregion
    }
}
