using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表OrderAddress的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderAddress的业务逻辑层。
	/// </summary>
	public class OrderAddressBLL
	{
	    #region 实例化
        public static OrderAddressBLL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly OrderAddressBLL instance = new OrderAddressBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderAddress，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderAddress">要添加的OrderAddress数据实体对象</param>
		public   int AddOrderAddress(OrderAddressEntity orderAddress)
		{
			  if (orderAddress.Id > 0)
            {
                return UpdateOrderAddress(orderAddress);
            }
				  else if (string.IsNullOrEmpty(orderAddress.AccepterName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(orderAddress.CountryName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (OrderAddressBLL.Instance.IsExist(orderAddress))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderAddressDA.Instance.AddOrderAddress(orderAddress);
            }
	 	}

		/// <summary>
		/// 更新一条OrderAddress记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderAddress">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderAddress(OrderAddressEntity orderAddress)
		{
			return OrderAddressDA.Instance.UpdateOrderAddress(orderAddress);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderAddressByKey(int id)
        {
            return OrderAddressDA.Instance.DeleteOrderAddressByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderAddressDisabled()
        {
            return OrderAddressDA.Instance.DeleteOrderAddressDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderAddressByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderAddressDA.Instance.DeleteOrderAddressByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderAddressByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderAddressDA.Instance.DisableOrderAddressByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderAddress实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderAddress实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderAddressEntity GetOrderAddress(int id)
		{
			return OrderAddressDA.Instance.GetOrderAddress(id);			
		}

        /// <summary>
        /// 根据主键获取一个OrderAddress实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>OrderAddress实体</returns>
        /// <param name="columns">要返回的列</param>
        public OrderAddressEntity GetOrderAddressByOrderCode(long ordercode)
        {
            return OrderAddressDA.Instance.GetOrderAddressByOrderCode(ordercode);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderAddressEntity> GetOrderAddressList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderAddressDA.Instance.GetOrderAddressList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetOrderAddressAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderAddressListKey";// SysCacheKey.OrderAddressListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderAddressEntity> list = null;
                    list = OrderAddressDA.Instance.GetOrderAddressAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderAddressEntity orderAddress)
        {
            return OrderAddressDA.Instance.ExistNum(orderAddress)>0;
        }
		
	}
}

