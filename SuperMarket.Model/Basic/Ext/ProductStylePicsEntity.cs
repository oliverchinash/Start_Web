using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public partial class ProductStylePicsEntity
    {
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlOld
        {
            get
            { 
                return PicUrl+"."+PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlBig
        {
            get
            {
                return PicUrl + "Big."+ PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlList
        {
            get
            {
                return PicUrl + "List." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlAd
        {
            get
            {
                return PicUrl + "Ad." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlNormal
        {
            get
            {
                return PicUrl + "Normal." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                return PicUrl + "Small." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlLittle
        {
            get
            {
                return PicUrl + "Little." + PicSuffix;
            }
        }
    }
}
