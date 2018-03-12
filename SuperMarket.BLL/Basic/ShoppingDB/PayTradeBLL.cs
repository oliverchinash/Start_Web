using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表PayTrade的业务逻辑层。
创建时间：2016/10/9 10:39:39
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表PayTrade的业务逻辑层。
	/// </summary>
	public class PayTradeBLL
	{
	    #region 实例化
        public static PayTradeBLL Instance
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
            internal static readonly PayTradeBLL instance = new PayTradeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PayTrade，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payTrade">要添加的PayTrade数据实体对象</param>
		public  int AddPayTrade(PayTradeEntity payTrade)
		{
			if (payTrade.Id > 0)
            {
                return UpdatePayTrade(payTrade);
            } 
            else if (PayTradeBLL.Instance.IsExist(payTrade))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PayTradeDA.Instance.AddPayTrade(payTrade);
            }
	 	}

		/// <summary>
		/// 更新一条PayTrade记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payTrade">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdatePayTrade(PayTradeEntity payTrade)
		{
			return PayTradeDA.Instance.UpdatePayTrade(payTrade);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayTradeByKey(int id)
        {
            return PayTradeDA.Instance.DeletePayTradeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayTradeDisabled()
        {
            return PayTradeDA.Instance.DeletePayTradeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayTradeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PayTradeDA.Instance.DeletePayTradeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayTradeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayTradeDA.Instance.DisablePayTradeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PayTrade实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PayTrade实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PayTradeEntity GetPayTrade(int id)
		{
			return PayTradeDA.Instance.GetPayTrade(id);			
		}
        public PayTradeEntity GetPayTradeByOrder(Int64 paycode)
        {
			return PayTradeDA.Instance.GetPayTradeByOrder(paycode);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PayTradeEntity> GetPayTradeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PayTradeDA.Instance.GetPayTradeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetPayTradeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PayTradeListKey";// SysCacheKey.PayTradeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PayTradeEntity> list = null;
                    list = PayTradeDA.Instance.GetPayTradeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayTradeEntity payTrade)
        {
            return PayTradeDA.Instance.ExistNum(payTrade)>0;
        }
		
	}
}

