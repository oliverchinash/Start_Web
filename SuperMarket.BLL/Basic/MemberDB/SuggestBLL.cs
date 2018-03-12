using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表Suggest的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表Suggest的业务逻辑层。
	/// </summary>
	public class SuggestBLL
	{
	    #region 实例化
        public static SuggestBLL Instance
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
            internal static readonly SuggestBLL instance = new SuggestBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Suggest，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggest">要添加的Suggest数据实体对象</param>
		public   int AddSuggest(SuggestEntity suggest)
		{
			  if (suggest.Id > 0)
            {
                return UpdateSuggest(suggest);
            }
				  else if (string.IsNullOrEmpty(suggest.ManName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (SuggestBLL.Instance.IsExist(suggest))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return SuggestDA.Instance.AddSuggest(suggest);
            }
	 	}

		/// <summary>
		/// 更新一条Suggest记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggest">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSuggest(SuggestEntity suggest)
		{
			return SuggestDA.Instance.UpdateSuggest(suggest);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSuggestByKey(int id)
        {
            return SuggestDA.Instance.DeleteSuggestByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestDisabled()
        {
            return SuggestDA.Instance.DeleteSuggestDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SuggestDA.Instance.DeleteSuggestByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SuggestDA.Instance.DisableSuggestByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个Suggest实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Suggest实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SuggestEntity GetSuggest(int id)
		{
			return SuggestDA.Instance.GetSuggest(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SuggestEntity> GetSuggestList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SuggestDA.Instance.GetSuggestList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSuggestAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SuggestListKey";// SysCacheKey.SuggestListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SuggestEntity> list = null;
                    list = SuggestDA.Instance.GetSuggestAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SuggestEntity suggest)
        {
            return SuggestDA.Instance.ExistNum(suggest)>0;
        }
		
	}
}

