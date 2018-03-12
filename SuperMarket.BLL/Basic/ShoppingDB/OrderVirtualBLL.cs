using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表OrderVirtual的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderVirtual的业务逻辑层。
	/// </summary>
	public class OrderVirtualBLL
	{
	    #region 实例化
        public static OrderVirtualBLL Instance
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
            internal static readonly OrderVirtualBLL instance = new OrderVirtualBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderVirtual，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderVirtual">要添加的OrderVirtual数据实体对象</param>
		public   int AddOrderVirtual(OrderVirtualEntity orderVirtual)
		{
			  if (orderVirtual.Id > 0)
            {
                return UpdateOrderVirtual(orderVirtual);
            }
          
            else if (OrderVirtualBLL.Instance.IsExist(orderVirtual))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderVirtualDA.Instance.AddOrderVirtual(orderVirtual);
            }
	 	}

		/// <summary>
		/// 更新一条OrderVirtual记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderVirtual">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderVirtual(OrderVirtualEntity orderVirtual)
		{
			return OrderVirtualDA.Instance.UpdateOrderVirtual(orderVirtual);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderVirtualByKey(int id)
        {
            return OrderVirtualDA.Instance.DeleteOrderVirtualByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderVirtualDisabled()
        {
            return OrderVirtualDA.Instance.DeleteOrderVirtualDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderVirtualByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderVirtualDA.Instance.DeleteOrderVirtualByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderVirtualByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderVirtualDA.Instance.DisableOrderVirtualByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderVirtual实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderVirtual实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderVirtualEntity GetOrderVirtual(int id)
		{
			return OrderVirtualDA.Instance.GetOrderVirtual(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderVirtualEntity> GetOrderVirtualList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderVirtualDA.Instance.GetOrderVirtualList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetOrderVirtualAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderVirtualListKey";// SysCacheKey.OrderVirtualListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderVirtualEntity> list = null;
                    list = OrderVirtualDA.Instance.GetOrderVirtualAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderVirtualEntity orderVirtual)
        {
            return OrderVirtualDA.Instance.ExistNum(orderVirtual)>0;
        }
		
	}
}

