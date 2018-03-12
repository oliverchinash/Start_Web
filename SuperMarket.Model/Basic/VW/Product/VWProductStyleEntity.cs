using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable()]
    public class VWProductStyleEntity
    {
        #region Public Properties	
        /// <summary>
        /// 
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
        /// 
        /// </summary>
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
        /// 
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
        /// 品牌 
        /// <summary>
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

        /// <summary>
        /// OE编码
        /// </summary>
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
        /// 显示价格
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

        /// <summary>
        /// 市场价
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
        /// 图片后缀名
        /// </summary>
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
        private string _PicUrl;
        public string PicUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(_PicSuffix))
                {
                    return _PicUrl+"."+_PicSuffix;
                }
                return _PicUrl;
            }
            set
            {
                _PicUrl = value;
            }
        }
 

        /// <summary>
        /// 大图片路径
        /// <summary> 
        public string PicUrlBig
        {
            get
            {
                if (!string.IsNullOrEmpty(_PicUrl) && !string.IsNullOrEmpty(_PicSuffix))
                {
                    return _PicUrl + "Big."+_PicSuffix;
                }
                return PicUrl;
            } 
        }


        /// <summary>
        /// 正常图片路径
        /// <summary> 
        public string PicUrlNormal
        {
            get
            {
                if (!string.IsNullOrEmpty(_PicUrl)&&!string.IsNullOrEmpty(_PicSuffix))
                {
                    return _PicUrl + "Normal."+_PicSuffix;
                }
                return PicUrl;
            }
        }


        /// <summary>
        /// 小图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                if (!string.IsNullOrEmpty(_PicUrl) && !string.IsNullOrEmpty(_PicSuffix))
                {
                    return _PicUrl + "Small." + _PicSuffix;
                }
                return PicUrl;
            }
        }


        /// <summary>
        /// 袖珍图片地址
        /// <summary> 
        public string PicUrlLittle
        {
            get
            {
                if (!string.IsNullOrEmpty(_PicUrl) && !string.IsNullOrEmpty(_PicSuffix))
                {
                    return _PicUrl + "Little." + _PicSuffix;
                }
                return PicUrl;
            }
        }


        /// <summary>
        /// 默认的产品Id，列表页的连接
        /// <summary>
        private int _DefaultProductId;
        public int DefaultProductId
        {
            get
            {
                return _DefaultProductId;
            }
            set
            {
                _DefaultProductId = value;
            }
        }

        /// <summary>
        /// 终极分类Id
        /// <summary>
        private int _ClassFoundId;
        public int ClassId
        {
            get
            {
                return _ClassFoundId;
            }
            set
            {
                _ClassFoundId = value;
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
        /// <summary>
        /// 是否有Product
        /// <summary>
        private int _HasProduct;
        public int HasProduct
        {
            get
            {
                return _HasProduct;
            }
            set
            {
                _HasProduct = value;
            }
        }
        /// <summary>
        /// 排序
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
        /// <summary>
        /// 是否有Product
        /// <summary>
        private string _PropertiesContent;
        public string PropertiesContent
        {
            get
            {
                return _PropertiesContent;
            }
            set
            {
                _PropertiesContent = value;
            }
        }
        /// <summary>
        /// SaleNum
        /// <summary>
        private decimal _SaleNum;
        public decimal SaleNum
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
        /// ContentCms
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
        /// <summary>
        /// StylePicsStr
        /// <summary>
        private string _StylePicsStr;
        public string StylePicsStr
        {
            get
            {
                return _StylePicsStr;
            }
            set
            {
                _StylePicsStr = value;
            }
        }
        public IList<ProductStylePicsEntity> StylePics;
        #endregion
    }
}
