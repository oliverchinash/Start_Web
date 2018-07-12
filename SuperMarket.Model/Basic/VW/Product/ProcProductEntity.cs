using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public  class ProcProductEntity
    {
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
        /// <summary>
        /// 商品名称
        /// <summary>
        private string _ProductName;
        public string ProductName
        {
            get
            {
                return _ProductName;
            }
            set
            {
                _ProductName = value;
            }
        }
        /// <summary>
        /// 
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
        /// 
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
        /// 
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
        /// 
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
                  ///  
                  /// <summary>
        private int _SiteId;
        public int SiteId
        {
            get
            {
                return _SiteId;
            }
            set
            {
                _SiteId = value;
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
        /// 显示价格
        /// <summary>
        private decimal _Cost;
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
        /// 市场价
        /// <summary>
        private decimal _TradePrice;
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

        /// <summary>
        /// 列表页显示图片地址
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
        /// 终极分类Id
        /// <summary>
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
        /// <summary>
        /// 终极分类Id
        /// <summary>
        private int _ClassId1;
        public int ClassId1
        {
            get
            {
                return _ClassId1;
            }
            set
            {
                _ClassId1 = value;
            }
        }
        /// <summary>
        /// 终极分类Id
        /// <summary>
        private int _ClassId2;
        public int ClassId2
        {
            get
            {
                return _ClassId2;
            }
            set
            {
                _ClassId2 = value;
            }
        }
        /// <summary>
        /// 终极分类Id
        /// <summary>
        private int _ClassId3;
        public int ClassId3
        {
            get
            {
                return _ClassId3;
            }
            set
            {
                _ClassId3 = value;
            }
        }
        /// <summary>
        ///  
        /// <summary>
        private int _CreateManId;
        public int CreateManId
        {
            get
            {
                return _CreateManId;
            }
            set
            {
                _CreateManId = value;
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
        /// <summary>
        /// 显示价格
        /// <summary>
        private decimal _Num;
        public decimal Num
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
        /// 毛重
        /// <summary>
        private decimal _GrossWeight;
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
        private string _ProductPropertysStr;
        public string ProductPropertysStr
        {
            get
            {
                return _ProductPropertysStr;
            }
            set
            {
                _ProductPropertysStr = value;
            }
        }
        private string _ProductPicsStr;
        public string ProductPicsStr
        {
            get
            {
                return _ProductPicsStr;
            }
            set
            {
                _ProductPicsStr = value;
            }
        }
        private int _ProductType;
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
        
    }
}
