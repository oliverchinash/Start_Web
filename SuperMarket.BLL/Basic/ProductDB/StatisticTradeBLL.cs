using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StatisticTrade的业务逻辑层。
创建时间：2017/6/17 11:08:46
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表StatisticTrade的业务逻辑层。
	/// </summary>
	public class StatisticTradeBLL
	{
	    #region 实例化
        public static StatisticTradeBLL Instance
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
            internal static readonly StatisticTradeBLL instance = new StatisticTradeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StatisticTrade，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticTrade">要添加的StatisticTrade数据实体对象</param>
		public   int AddStatisticTrade(StatisticTradeEntity statisticTrade)
		{
			  if (statisticTrade.Id > 0)
            {
                return UpdateStatisticTrade(statisticTrade);
            }
          
            else if (StatisticTradeBLL.Instance.IsExist(statisticTrade))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StatisticTradeDA.Instance.AddStatisticTrade(statisticTrade);
            }
	 	}

		/// <summary>
		/// 更新一条StatisticTrade记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticTrade">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStatisticTrade(StatisticTradeEntity statisticTrade)
		{
			return StatisticTradeDA.Instance.UpdateStatisticTrade(statisticTrade);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStatisticTradeByKey(int id)
        {
            return StatisticTradeDA.Instance.DeleteStatisticTradeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticTradeDisabled()
        {
            return StatisticTradeDA.Instance.DeleteStatisticTradeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticTradeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StatisticTradeDA.Instance.DeleteStatisticTradeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticTradeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StatisticTradeDA.Instance.DisableStatisticTradeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StatisticTrade实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StatisticTrade实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StatisticTradeEntity GetStatisticTrade(int id)
		{
			return StatisticTradeDA.Instance.GetStatisticTrade(id);			
		}
        public StatisticTradeEntity GetStatisticTradeToday()
        {
            return StatisticTradeDA.Instance.GetStatisticTradeToday();
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StatisticTradeEntity> GetStatisticTradeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StatisticTradeDA.Instance.GetStatisticTradeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStatisticTradeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StatisticTradeListKey";// SysCacheKey.StatisticTradeListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<StatisticTradeEntity> list = null;
                    list = StatisticTradeDA.Instance.GetStatisticTradeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StatisticTradeEntity statisticTrade)
        {
            return StatisticTradeDA.Instance.ExistNum(statisticTrade)>0;
        }
		
	}
}

