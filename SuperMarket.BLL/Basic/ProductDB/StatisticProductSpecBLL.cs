using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StatisticProductSpec的业务逻辑层。
创建时间：2016/11/10 16:04:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表StatisticProductSpec的业务逻辑层。
	/// </summary>
	public class StatisticProductSpecBLL
	{
	    #region 实例化
        public static StatisticProductSpecBLL Instance
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
            internal static readonly StatisticProductSpecBLL instance = new StatisticProductSpecBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StatisticProductSpec，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticProductSpec">要添加的StatisticProductSpec数据实体对象</param>
		public   int AddStatisticProductSpec(StatisticProductSpecEntity statisticProductSpec)
		{
			  if (statisticProductSpec.Id > 0)
            {
                return UpdateStatisticProductSpec(statisticProductSpec);
            }
          
            else if (StatisticProductSpecBLL.Instance.IsExist(statisticProductSpec))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return StatisticProductSpecDA.Instance.AddStatisticProductSpec(statisticProductSpec);
            }
	 	}

		/// <summary>
		/// 更新一条StatisticProductSpec记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="statisticProductSpec">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStatisticProductSpec(StatisticProductSpecEntity statisticProductSpec)
		{
			return StatisticProductSpecDA.Instance.UpdateStatisticProductSpec(statisticProductSpec);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStatisticProductSpecByKey(int id)
        {
            return StatisticProductSpecDA.Instance.DeleteStatisticProductSpecByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStatisticProductSpecDisabled()
        {
            return StatisticProductSpecDA.Instance.DeleteStatisticProductSpecDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStatisticProductSpecByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return StatisticProductSpecDA.Instance.DeleteStatisticProductSpecByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStatisticProductSpecByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return StatisticProductSpecDA.Instance.DisableStatisticProductSpecByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StatisticProductSpec实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StatisticProductSpec实体</returns>
		/// <param name="columns">要返回的列</param>
		public   StatisticProductSpecEntity GetStatisticProductSpec(int id)
		{
			return StatisticProductSpecDA.Instance.GetStatisticProductSpec(id);			
		}
        public StatisticProductSpecEntity GetStatisticSpecsByStyleId(int styleid)
        {
            string _cachekey = "GetStatisticSpecsByStyleId_"+ styleid; 
            object obj = MemCache.GetCache(_cachekey);
            StatisticProductSpecEntity entity = new StatisticProductSpecEntity();
            if (obj == null)
            {
                entity = StatisticProductSpecDA.Instance.GetSpecsByStyleId(styleid);
                MemCache.AddCache(_cachekey, entity);
            }
            else
            {
                entity = (StatisticProductSpecEntity)obj;  
            }
            return entity;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<StatisticProductSpecEntity> GetStatisticProductSpecList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return StatisticProductSpecDA.Instance.GetStatisticProductSpecList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStatisticProductSpecAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StatisticProductSpecListKey";// SysCacheKey.StatisticProductSpecListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<StatisticProductSpecEntity> list = null;
                    list = StatisticProductSpecDA.Instance.GetStatisticProductSpecAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(StatisticProductSpecEntity statisticProductSpec)
        {
            return StatisticProductSpecDA.Instance.ExistNum(statisticProductSpec)>0;
        }
		
	}
}

