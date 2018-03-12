using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表OrderCreateLog的业务逻辑层。
创建时间：2017/1/16 14:22:32
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderCreateLog的业务逻辑层。
	/// </summary>
	public class OrderCreateLogBLL
	{
	    #region 实例化
        public static OrderCreateLogBLL Instance
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
            internal static readonly OrderCreateLogBLL instance = new OrderCreateLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderCreateLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderCreateLog">要添加的OrderCreateLog数据实体对象</param>
		public   int AddOrderCreateLog(OrderCreateLogEntity orderCreateLog)
		{
		     return OrderCreateLogDA.Instance.AddOrderCreateLog(orderCreateLog);
            
	 	}

		/// <summary>
		/// 更新一条OrderCreateLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderCreateLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderCreateLog(OrderCreateLogEntity orderCreateLog)
		{
			return OrderCreateLogDA.Instance.UpdateOrderCreateLog(orderCreateLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderCreateLogByKey(int id)
        {
            return OrderCreateLogDA.Instance.DeleteOrderCreateLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderCreateLogDisabled()
        {
            return OrderCreateLogDA.Instance.DeleteOrderCreateLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderCreateLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderCreateLogDA.Instance.DeleteOrderCreateLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderCreateLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderCreateLogDA.Instance.DisableOrderCreateLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderCreateLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderCreateLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderCreateLogEntity GetOrderCreateLog(int id)
		{
			return OrderCreateLogDA.Instance.GetOrderCreateLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderCreateLogEntity> GetOrderCreateLogList(int pageSize, int pageIndex, ref  int recordCount,long oldcode)
        {
            return OrderCreateLogDA.Instance.GetOrderCreateLogList(pageSize, pageIndex, ref recordCount, oldcode);
        }
		
		public async Task GetOrderCreateLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderCreateLogListKey";// SysCacheKey.OrderCreateLogListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<OrderCreateLogEntity> list = null;
                    list = OrderCreateLogDA.Instance.GetOrderCreateLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(long oldOrderCode)
        {
            return OrderCreateLogDA.Instance.ExistNum(oldOrderCode) >0;
        }
		
	}
}

