using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.SysDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CacheConfig的业务逻辑层。
创建时间：2016/7/30 10:41:58
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.SysDB
{
	  
	/// <summary>
	/// 表CacheConfig的业务逻辑层。
	/// </summary>
	public class CacheConfigBLL
	{
	    #region 实例化
        public static CacheConfigBLL Instance
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
            internal static readonly CacheConfigBLL instance = new CacheConfigBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CacheConfig，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cacheConfig">要添加的CacheConfig数据实体对象</param>
		public   int AddCacheConfig(CacheConfigEntity cacheConfig)
		{
			  if (cacheConfig.Id > 0)
            {
                return UpdateCacheConfig(cacheConfig);
            }
          
            else if (CacheConfigBLL.Instance.IsExist(cacheConfig))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CacheConfigDA.Instance.AddCacheConfig(cacheConfig);
            }
	 	}

		/// <summary>
		/// 更新一条CacheConfig记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cacheConfig">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCacheConfig(CacheConfigEntity cacheConfig)
		{
			return CacheConfigDA.Instance.UpdateCacheConfig(cacheConfig);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCacheConfigByKey(int id)
        {
            return CacheConfigDA.Instance.DeleteCacheConfigByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCacheConfigDisabled()
        {
            return CacheConfigDA.Instance.DeleteCacheConfigDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCacheConfigByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CacheConfigDA.Instance.DeleteCacheConfigByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCacheConfigByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CacheConfigDA.Instance.DisableCacheConfigByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CacheConfig实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CacheConfig实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CacheConfigEntity GetCacheConfig(int id)
		{
			return CacheConfigDA.Instance.GetCacheConfig(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CacheConfigEntity> GetCacheConfigList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CacheConfigDA.Instance.GetCacheConfigList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetCacheConfigAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CacheConfigListKey";// SysCacheKey.CacheConfigListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CacheConfigEntity> list = null;
                    list = CacheConfigDA.Instance.GetCacheConfigAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CacheConfigEntity cacheConfig)
        {
            return CacheConfigDA.Instance.ExistNum(cacheConfig)>0;
        }
		
	}
}

