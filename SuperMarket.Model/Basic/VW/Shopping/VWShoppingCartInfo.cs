using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model 
{
    [Serializable]
    public class VWShoppingCartInfo
    {    
        /// <summary>
         /// 购物车订单数据列表
       /// </summary>
        public IList<ShoppCookie> CartItems
        {
            get;
            set;
        }
        public IList<MemShoppCarEntity> CartItemsL
        {
            get;
            set;
        }
        /// <summary>
        /// 商品总金额
        /// </summary>
        public decimal TotalAmount
        {
            get
            {
                decimal dTotalAmount = 0;
                foreach (MemShoppCarEntity m in CartItemsL)
                {
                    if (m.Check)
                    {
                        dTotalAmount += m.Price * m.Num;
                    }
                }
                if (dTotalAmount == 0 && CartItemsL != null && CartItemsL.Count > 0)
                {
                    CartItemsL[0].Check = true;
                    dTotalAmount = CartItemsL[0].Price * CartItemsL[0].Num;
                }
                return dTotalAmount;
            }
        }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public decimal DiscountAmount
        {
            get;
            set;
        }
        /// <summary>
        /// 邮费
        /// </summary>
        public decimal PostFree
        {
            get;
            set;
        }
        /// <summary>
        /// 订单总金额（商品总金额+邮费-折扣金额）
        /// </summary>
        public decimal OrderAmount
        {

            get
            {
                return TotalAmount + PostFree - DiscountAmount;
            }
        }
       
         
        /// <summary>
        /// 购物车商品数量
        /// </summary>
        public int CartCount
        {
            get
            {
                int count = 0;
                foreach (ShoppCookie item in CartItems)
                {
                    count += item.Num;
                }
                return count;
            }
        }

        public decimal dDisRate
        {
            get;
            set;
        }
    }
}
