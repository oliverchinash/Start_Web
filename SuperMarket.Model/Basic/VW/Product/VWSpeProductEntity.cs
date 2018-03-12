using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Basic.VW.Product
{
    [Serializable]
    /// <summary>
    /// 特价产品视图
    /// </summary>
    public class VWSpeProductEntity
    {

        /// <summary>
        /// 
        /// </summary>
        private int _Id;

        public int Id
        {
            get;
            set;
        }

        /// <summary>
        /// 产品Id
        /// </summary>
        private int _ProductId;
     
        public int ProductId
        {
            get;
            set;
        }


        /// <summary>
        /// 产品标题
        /// </summary>
        private string _Title;
        public string Title
        {
            get;
            set;
        }

        /// <summary>
        /// 产品名称
        /// </summary>
        private string _Name;
        public string Name
        {
            get;
            set;
        }


        /// <summary>
        /// 价格
        /// </summary>
        private decimal _Price;
        public decimal Price
        {
            get;
            set;
        }


        /// <summary>
        /// 批发价格
        /// </summary>
        private decimal _TradePrice;
        public decimal TradePrice
        {
            get;
            set;
        }


        /// <summary>
        /// 库存数量
        /// </summary>
        private int _StockNum;
        public int StockNum
        {
            get;
            set;
        }


        /// <summary>
        /// 销售数量
        /// </summary>
        private int _SaleNum;
        public int SaleNum
        {
            get;
            set;
        }

        /// <summary>
        /// 产品类型
        /// </summary>
        private int _ProductType;
        public int ProductType
        {
            get;
            set;
        }

        /// <summary>
        /// 上架或者下架
        /// </summary>
        private int _Status;
        public int Status
        {
            get;
            set;
        }

 





    }
}
