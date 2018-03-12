using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SuggestDealLog的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表SuggestDealLog的业务逻辑层。
	/// </summary>
	public class SuggestDealLogBLL
	{
	    #region 实例化
        public static SuggestDealLogBLL Instance
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
            internal static readonly SuggestDealLogBLL instance = new SuggestDealLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SuggestDealLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggestDealLog">要添加的SuggestDealLog数据实体对象</param>
		public   int AddSuggestDealLog(SuggestDealLogEntity suggestDealLog)
		{
			  if (suggestDealLog.Id > 0)
            {
                return UpdateSuggestDealLog(suggestDealLog);
            }
          
            else if (SuggestDealLogBLL.Instance.IsExist(suggestDealLog))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return SuggestDealLogDA.Instance.AddSuggestDealLog(suggestDealLog);
            }
	 	}

		/// <summary>
		/// 更新一条SuggestDealLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggestDealLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSuggestDealLog(SuggestDealLogEntity suggestDealLog)
		{
			return SuggestDealLogDA.Instance.UpdateSuggestDealLog(suggestDealLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSuggestDealLogByKey(int id)
        {
            return SuggestDealLogDA.Instance.DeleteSuggestDealLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestDealLogDisabled()
        {
            return SuggestDealLogDA.Instance.DeleteSuggestDealLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestDealLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SuggestDealLogDA.Instance.DeleteSuggestDealLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestDealLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SuggestDealLogDA.Instance.DisableSuggestDealLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SuggestDealLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SuggestDealLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SuggestDealLogEntity GetSuggestDealLog(int id)
		{
			return SuggestDealLogDA.Instance.GetSuggestDealLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SuggestDealLogEntity> GetSuggestDealLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SuggestDealLogDA.Instance.GetSuggestDealLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSuggestDealLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SuggestDealLogListKey";// SysCacheKey.SuggestDealLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SuggestDealLogEntity> list = null;
                    list = SuggestDealLogDA.Instance.GetSuggestDealLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SuggestDealLogEntity suggestDealLog)
        {
            return SuggestDealLogDA.Instance.ExistNum(suggestDealLog)>0;
        }
		
	}
}

