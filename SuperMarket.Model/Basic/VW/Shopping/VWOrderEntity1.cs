using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Basic.VW.Shopping
{
    public   class VWOrderEntity1
    {
        public IList<OrderDetailEntity> OrderDetails;
        public decimal PostFee; 
        public decimal TotalPrice;
        public decimal PreDisCountPrice;
        public decimal DisCountPrice;
    }
}
