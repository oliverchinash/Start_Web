using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StatisticData的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表StatisticData的业务逻辑层。
	/// </summary>
	public class StatisticDataBLL
	{
	    #region 实例化
        public static StatisticDataBLL Instance
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
            internal static readonly StatisticDataBLL instance = new StatisticDataBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StatisticData，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticData">要添加的StatisticData数据实体对象</param>
		public   int AddStatisticData(StatisticDataEntity statisticData)
		{
			  if (statisticData.Id > 0)
            {
                return UpdateStatisticData(statisticData);
            }
          
            else if (StatisticDataBLL.Instance.IsExist(statisticData))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StatisticDataDA.Instance.AddStatisticData(statisticData);
            }
	 	}

		/// <summary>
		/// 更新一条StatisticData记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticData">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStatisticData(StatisticDataEntity statisticData)
		{
			return StatisticDataDA.Instance.UpdateStatisticData(statisticData);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStatisticDataByKey(int id)
        {
            return StatisticDataDA.Instance.DeleteStatisticDataByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticDataDisabled()
        {
            return StatisticDataDA.Instance.DeleteStatisticDataDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticDataByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StatisticDataDA.Instance.DeleteStatisticDataByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticDataByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StatisticDataDA.Instance.DisableStatisticDataByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StatisticData实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StatisticData实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StatisticDataEntity GetStatisticData(int id)
		{
			return StatisticDataDA.Instance.GetStatisticData(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StatisticDataEntity> GetStatisticDataList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StatisticDataDA.Instance.GetStatisticDataList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStatisticDataAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StatisticDataListKey";// SysCacheKey.StatisticDataListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<StatisticDataEntity> list = null;
                    list = StatisticDataDA.Instance.GetStatisticDataAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StatisticDataEntity statisticData)
        {
            return StatisticDataDA.Instance.ExistNum(statisticData)>0;
        }
		
	}
}

