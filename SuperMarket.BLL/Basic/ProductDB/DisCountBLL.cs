using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表DisCount的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表DisCount的业务逻辑层。
	/// </summary>
	public class DisCountBLL
	{
	    #region 实例化
        public static DisCountBLL Instance
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
            internal static readonly DisCountBLL instance = new DisCountBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表DisCount，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="disCount">要添加的DisCount数据实体对象</param>
		public   int AddDisCount(DisCountEntity disCount)
		{
			  if (disCount.Id > 0)
            {
                return UpdateDisCount(disCount);
            }
				  else if (string.IsNullOrEmpty(disCount.DisCountName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (DisCountBLL.Instance.IsExist(disCount))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return DisCountDA.Instance.AddDisCount(disCount);
            }
	 	}

		/// <summary>
		/// 更新一条DisCount记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="disCount">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateDisCount(DisCountEntity disCount)
		{
			return DisCountDA.Instance.UpdateDisCount(disCount);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDisCountByKey(int id)
        {
            return DisCountDA.Instance.DeleteDisCountByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDisCountDisabled()
        {
            return DisCountDA.Instance.DeleteDisCountDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDisCountByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DisCountDA.Instance.DeleteDisCountByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDisCountByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DisCountDA.Instance.DisableDisCountByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个DisCount实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>DisCount实体</returns>
		/// <param name="columns">要返回的列</param>
		public   DisCountEntity GetDisCount(int id)
		{
			return DisCountDA.Instance.GetDisCount(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<DisCountEntity> GetDisCountList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return DisCountDA.Instance.GetDisCountList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetDisCountAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="DisCountListKey";// SysCacheKey.DisCountListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<DisCountEntity> list = null;
                    list = DisCountDA.Instance.GetDisCountAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DisCountEntity disCount)
        {
            return DisCountDA.Instance.ExistNum(disCount)>0;
        }
		
	}
}

