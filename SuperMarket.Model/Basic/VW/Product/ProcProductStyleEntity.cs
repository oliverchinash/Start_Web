using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    [Serializable]
    public  class ProcProductStyleEntity
    {
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
        /// OE编码
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
        /// 品牌名称
        /// <summary>
        private string _BrandId;
        public string BrandId
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
        /// <summary>
        /// 显示价格
        /// <summary>
        private decimal _SellerId;
        public decimal SellerId
        {
            get
            {
                return _SellerId;
            }
            set
            {
                _SellerId = value;
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
        private string _StylePropertys;
        public string StylePropertys
        {
            get
            {
                return _StylePropertys;
            }
            set
            {
                _StylePropertys = value;
            }
        }
        private string _BrandCars;
        public string BrandCars
        {
            get
            {
                return _BrandCars;
            }
            set
            {
                _BrandCars = value;
            }
        }
        private string _StylePics;
        public string StylePics
        {
            get
            {
                return _StylePics;
            }
            set
            {
                _StylePics = value;
            }
        }
        
    }
}
