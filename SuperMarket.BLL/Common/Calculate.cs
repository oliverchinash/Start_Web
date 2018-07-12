using SuperMarket.BLL.Cookie;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.BLL.Common
{
    public class Calculate
    {
        /// <summary>
        /// 计算产品价格（根据不同用户显示不同价格）0表示价格不显示
        /// </summary>
        /// <param name="isstore"></param>
        /// <param name="memtype">用户类型：维修厂，经销商。。。</param>
        /// <param name="memlevel"></param>
        /// <param name="cost"></param>
        /// <param name="tradprice"></param>
        /// <param name="price"></param>
        /// <returns></returns>
        public static decimal GetPrice(int status,int isstore,int memtype,int memlevel,  decimal tradprice, decimal price,int isbp, decimal dealerprice)
        {
            if (status == (int)MemberStatus.Active )
            {
                //if(memtype==(int)CompanyType.Agency)//经销商
                //{
                //    return dealerprice;
                //}  
                return price;
            }
            else
            {
                return 0;
            }
        } 
    }
}
