using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.PayDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表AliResult的业务逻辑层。
创建时间：2017/11/29 10:53:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.PayDB
{
	  
	/// <summary>
	/// 表AliResult的业务逻辑层。
	/// </summary>
	public class AliResultBLL
	{
	    #region 实例化
        public static AliResultBLL Instance
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
            internal static readonly AliResultBLL instance = new AliResultBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表AliResult，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="aliResult">要添加的AliResult数据实体对象</param>
		public   int AddAliResult(AliResultEntity aliResult)
		{
			if (aliResult.Id > 0)
            {
                return UpdateAliResult(aliResult);
            } 
            else if (AliResultBLL.Instance.IsExist(aliResult))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AliResultDA.Instance.AddAliResult(aliResult);
            }
	 	}

		/// <summary>
		/// 更新一条AliResult记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="aliResult">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAliResult(AliResultEntity aliResult)
		{
			return AliResultDA.Instance.UpdateAliResult(aliResult);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAliResultByKey(int id)
        {
            return AliResultDA.Instance.DeleteAliResultByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAliResultDisabled()
        {
            return AliResultDA.Instance.DeleteAliResultDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAliResultByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AliResultDA.Instance.DeleteAliResultByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAliResultByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AliResultDA.Instance.DisableAliResultByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个AliResult实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>AliResult实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AliResultEntity GetAliResult(int id)
		{
			return AliResultDA.Instance.GetAliResult(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AliResultEntity> GetAliResultList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return AliResultDA.Instance.GetAliResultList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetAliResultAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AliResultListKey";// SysCacheKey.AliResultListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<AliResultEntity> list = null;
                    list = AliResultDA.Instance.GetAliResultAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AliResultEntity aliResult)
        {
            return AliResultDA.Instance.ExistNum(aliResult)>0;
        }
		
	}
}

