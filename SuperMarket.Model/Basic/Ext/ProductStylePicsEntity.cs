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
                return PicUrl + "800_800."+ PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlList
        {
            get
            {
                return PicUrl + "200_200." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlAd
        {
            get
            {
                return PicUrl + "60_60." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlNormal
        {
            get
            {
                return PicUrl + "360_360." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlSmall
        {
            get
            {
                return PicUrl + "60_60." + PicSuffix;
            }
        }
        /// <summary>
        /// 列表页显示图片地址
        /// <summary> 
        public string PicUrlLittle
        {
            get
            {
                return PicUrl + "36_36." + PicSuffix;
            }
        }
    }
}
