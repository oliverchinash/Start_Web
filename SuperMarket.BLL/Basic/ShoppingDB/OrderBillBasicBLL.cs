using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.SysDB;

/*****************************************
功能描述：表OrderBillBasic的业务逻辑层。
创建时间：2016/11/17 22:49:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderBillBasic的业务逻辑层。
	/// </summary>
	public class OrderBillBasicBLL
	{
	    #region 实例化
        public static OrderBillBasicBLL Instance
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
            internal static readonly OrderBillBasicBLL instance = new OrderBillBasicBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderBillBasic，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderBillBasic">要添加的OrderBillBasic数据实体对象</param>
		public   int AddOrderBillBasic(OrderBillBasicEntity orderBillBasic)
		{
			  if (orderBillBasic.Id > 0)
            {
                return UpdateOrderBillBasic(orderBillBasic);
            }
				  else if (string.IsNullOrEmpty(orderBillBasic.CompanyName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(orderBillBasic.ReceiverName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (OrderBillBasicBLL.Instance.IsExist(orderBillBasic))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderBillBasicDA.Instance.AddOrderBillBasic(orderBillBasic);
            }
	 	}

		/// <summary>
		/// 更新一条OrderBillBasic记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderBillBasic">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderBillBasic(OrderBillBasicEntity orderBillBasic)
		{
			return OrderBillBasicDA.Instance.UpdateOrderBillBasic(orderBillBasic);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderBillBasicByKey(int id)
        {
            return OrderBillBasicDA.Instance.DeleteOrderBillBasicByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderBillBasicDisabled()
        {
            return OrderBillBasicDA.Instance.DeleteOrderBillBasicDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderBillBasicByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderBillBasicDA.Instance.DeleteOrderBillBasicByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderBillBasicByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderBillBasicDA.Instance.DisableOrderBillBasicByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderBillBasic实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderBillBasic实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderBillBasicEntity GetOrderBillBasic(int id)
		{
			return OrderBillBasicDA.Instance.GetOrderBillBasic(id);
        }
        public OrderBillBasicEntity GetBillBasicByOrderCode(long code)
        {
            OrderBillBasicEntity bill= OrderBillBasicDA.Instance.GetBillBasicByOrderCode(code);
            if (bill != null && bill.Id > 0 && bill.ReceiverProvince > 0 && bill.ReceiverCity > 0)
            {
                //bill.ReceiverProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(bill.ReceiverProvince.ToString()).Name;
                bill.ReceiverCityName = GYCityBLL.Instance.GetGYCityByCode(bill.ReceiverCity.ToString()).Name;
            }
            return bill;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderBillBasicEntity> GetOrderBillBasicList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderBillBasicDA.Instance.GetOrderBillBasicList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetOrderBillBasicAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderBillBasicListKey";// SysCacheKey.OrderBillBasicListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderBillBasicEntity> list = null;
                    list = OrderBillBasicDA.Instance.GetOrderBillBasicAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderBillBasicEntity orderBillBasic)
        {
            return OrderBillBasicDA.Instance.ExistNum(orderBillBasic)>0;
        }
		
	}
}

