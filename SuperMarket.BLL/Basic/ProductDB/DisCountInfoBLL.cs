using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表DisCountInfo的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表DisCountInfo的业务逻辑层。
	/// </summary>
	public class DisCountInfoBLL
	{
	    #region 实例化
        public static DisCountInfoBLL Instance
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
            internal static readonly DisCountInfoBLL instance = new DisCountInfoBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表DisCountInfo，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="disCountInfo">要添加的DisCountInfo数据实体对象</param>
		public   int AddDisCountInfo(DisCountInfoEntity disCountInfo)
		{
			  if (disCountInfo.Id > 0)
            {
                return UpdateDisCountInfo(disCountInfo);
            }
          
            else if (DisCountInfoBLL.Instance.IsExist(disCountInfo))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return DisCountInfoDA.Instance.AddDisCountInfo(disCountInfo);
            }
	 	}

		/// <summary>
		/// 更新一条DisCountInfo记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="disCountInfo">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateDisCountInfo(DisCountInfoEntity disCountInfo)
		{
			return DisCountInfoDA.Instance.UpdateDisCountInfo(disCountInfo);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteDisCountInfoByKey(int id)
        {
            return DisCountInfoDA.Instance.DeleteDisCountInfoByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteDisCountInfoDisabled()
        {
            return DisCountInfoDA.Instance.DeleteDisCountInfoDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteDisCountInfoByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return DisCountInfoDA.Instance.DeleteDisCountInfoByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableDisCountInfoByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return DisCountInfoDA.Instance.DisableDisCountInfoByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个DisCountInfo实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>DisCountInfo实体</returns>
		/// <param name="columns">要返回的列</param>
		public   DisCountInfoEntity GetDisCountInfo(int id)
		{
			return DisCountInfoDA.Instance.GetDisCountInfo(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<DisCountInfoEntity> GetDisCountInfoList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return DisCountInfoDA.Instance.GetDisCountInfoList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetDisCountInfoAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="DisCountInfoListKey";// SysCacheKey.DisCountInfoListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<DisCountInfoEntity> list = null;
                    list = DisCountInfoDA.Instance.GetDisCountInfoAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(DisCountInfoEntity disCountInfo)
        {
            return DisCountInfoDA.Instance.ExistNum(disCountInfo)>0;
        }
		
	}
}

