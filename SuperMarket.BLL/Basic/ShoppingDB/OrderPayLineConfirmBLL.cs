using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表OrderPayLineConfirm的业务逻辑层。
创建时间：2017/1/16 12:07:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表OrderPayLineConfirm的业务逻辑层。
	/// </summary>
	public class OrderPayLineConfirmBLL
	{
	    #region 实例化
        public static OrderPayLineConfirmBLL Instance
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
            internal static readonly OrderPayLineConfirmBLL instance = new OrderPayLineConfirmBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表OrderPayLineConfirm，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderPayLineConfirm">要添加的OrderPayLineConfirm数据实体对象</param>
		public   int AddOrderPayLineConfirm(OrderPayLineConfirmEntity orderPayLineConfirm)
		{
		 
                return OrderPayLineConfirmDA.Instance.AddOrderPayLineConfirm(orderPayLineConfirm);
           
	 	}

		/// <summary>
		/// 更新一条OrderPayLineConfirm记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="orderPayLineConfirm">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateOrderPayLineConfirm(OrderPayLineConfirmEntity orderPayLineConfirm)
		{
			return OrderPayLineConfirmDA.Instance.UpdateOrderPayLineConfirm(orderPayLineConfirm);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderPayLineConfirmByKey(int id)
        {
            return OrderPayLineConfirmDA.Instance.DeleteOrderPayLineConfirmByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderPayLineConfirmDisabled()
        {
            return OrderPayLineConfirmDA.Instance.DeleteOrderPayLineConfirmDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderPayLineConfirmByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return OrderPayLineConfirmDA.Instance.DeleteOrderPayLineConfirmByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderPayLineConfirmByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderPayLineConfirmDA.Instance.DisableOrderPayLineConfirmByIds(idstr);
        }
	 
        public OrderPayLineConfirmEntity GetPayLineConfirm(int id,long ordercode)
        {
            return OrderPayLineConfirmDA.Instance.GetPayLineConfirm(id, ordercode);
        }

        public int  ConfirmRecived(int confirmid, long ordercode,int sysmemid)
        {
            return OrderPayLineConfirmDA.Instance.ConfirmRecived(confirmid, ordercode, sysmemid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderPayLineConfirmEntity> GetOrderPayLineConfirmList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return OrderPayLineConfirmDA.Instance.GetOrderPayLineConfirmList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWOrderPayLineConfirm> GetVWPayLineConfirmList(int pageSize, int pageIndex, ref int recordCount ,long ordercode,string payconfirmcode)
        {
            return OrderPayLineConfirmDA.Instance.GetVWPayLineConfirmList(pageSize, pageIndex, ref recordCount,   ordercode,   payconfirmcode);
        }
        public async Task GetOrderPayLineConfirmAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="OrderPayLineConfirmListKey";// SysCacheKey.OrderPayLineConfirmListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<OrderPayLineConfirmEntity> list = null;
                    list = OrderPayLineConfirmDA.Instance.GetOrderPayLineConfirmAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(long ordercode,int status)
        {
            return OrderPayLineConfirmDA.Instance.ExistNum(ordercode, status) >0;
        }
		
	}
}

