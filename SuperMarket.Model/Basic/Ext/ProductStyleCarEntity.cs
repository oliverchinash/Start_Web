using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    public partial class ProductStyleCarEntity
    {
        /// <summary>
        /// 车型品牌1级：乘用车，商用车。。。BrandCar
        /// <summary>
        private string _BrandCarName1;
        public string BrandCarName1
        {
            get
            {
                return _BrandCarName1;
            }
            set
            {
                _BrandCarName1 = value;
            }
        }

        /// <summary>
        /// 车品牌2：奥迪。。。
        /// <summary>
        private string _BrandCarName2;
        public string BrandCarName2
        {
            get
            {
                return _BrandCarName2;
            }
            set
            {
                _BrandCarName2 = value;
            }
        }

        /// <summary>
        /// 车系3级：Q5...
        /// <summary>
        private string _BrandCarName3;
        public string BrandCarName3
        {
            get
            {
                return _BrandCarName3;
            }
            set
            {
                _BrandCarName3= value;
            }
        }

        /// <summary>
        /// 车型4级：2015 豪华型。。。
        /// <summary>
        private string _BrandCarName4;
        public string BrandCarName4
        {
            get
            {
                return _BrandCarName4;
            }
            set
            {
                _BrandCarName4 = value;
            }
        }
    }
}
