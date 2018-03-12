using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表OrderMsgMem的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderMsgMem的业务逻辑层。
	/// </summary>
	public class OrderMsgMemBLL
	{
	    #region 实例化
        public static OrderMsgMemBLL Instance
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
            internal static readonly OrderMsgMemBLL instance = new OrderMsgMemBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderMsgMem，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderMsgMem">要添加的OrderMsgMem数据实体对象</param>
		public   int AddOrderMsgMem(OrderMsgMemEntity orderMsgMem)
		{
			  if (orderMsgMem.Id > 0)
            {
                return UpdateOrderMsgMem(orderMsgMem);
            }
          
            else if (OrderMsgMemBLL.Instance.IsExist(orderMsgMem))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderMsgMemDA.Instance.AddOrderMsgMem(orderMsgMem);
            }
	 	}

		/// <summary>
		/// 更新一条OrderMsgMem记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderMsgMem">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderMsgMem(OrderMsgMemEntity orderMsgMem)
		{
			return OrderMsgMemDA.Instance.UpdateOrderMsgMem(orderMsgMem);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderMsgMemByKey(int id)
        {
            return OrderMsgMemDA.Instance.DeleteOrderMsgMemByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderMsgMemDisabled()
        {
            return OrderMsgMemDA.Instance.DeleteOrderMsgMemDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderMsgMemByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderMsgMemDA.Instance.DeleteOrderMsgMemByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderMsgMemByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderMsgMemDA.Instance.DisableOrderMsgMemByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个OrderMsgMem实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>OrderMsgMem实体</returns>
		/// <param name="columns">要返回的列</param>
		public   OrderMsgMemEntity GetOrderMsgMem(int id)
		{
			return OrderMsgMemDA.Instance.GetOrderMsgMem(id);			
		}
        public VWOrderMsgEntity GetVWOrderMsgByMemId(int memid)
        {
            ///实时统计
            VWOrderMsgEntity _entity= OrderDA.Instance.GetOrderMsgByMemId(memid);
            //return OrderMsgMemDA.Instance.GetVWOrderMsgByMemId(memid);
            return _entity;
        }
        
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderMsgMemEntity> GetOrderMsgMemList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderMsgMemDA.Instance.GetOrderMsgMemList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetOrderMsgMemAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderMsgMemListKey";// SysCacheKey.OrderMsgMemListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderMsgMemEntity> list = null;
                    list = OrderMsgMemDA.Instance.GetOrderMsgMemAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderMsgMemEntity orderMsgMem)
        {
            return OrderMsgMemDA.Instance.ExistNum(orderMsgMem)>0;
        }
		
	}
}

