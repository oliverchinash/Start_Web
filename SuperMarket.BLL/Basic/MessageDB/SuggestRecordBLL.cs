using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SuggestRecord的业务逻辑层。
创建时间：2017/5/9 11:44:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表SuggestRecord的业务逻辑层。
	/// </summary>
	public class SuggestRecordBLL
	{
	    #region 实例化
        public static SuggestRecordBLL Instance
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
            internal static readonly SuggestRecordBLL instance = new SuggestRecordBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SuggestRecord，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggestRecord">要添加的SuggestRecord数据实体对象</param>
		public   int AddSuggestRecord(SuggestRecordEntity suggestRecord)
		{
		 
                return SuggestRecordDA.Instance.AddSuggestRecord(suggestRecord);
    
	 	}

		/// <summary>
		/// 更新一条SuggestRecord记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="suggestRecord">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSuggestRecord(SuggestRecordEntity suggestRecord)
		{
			return SuggestRecordDA.Instance.UpdateSuggestRecord(suggestRecord);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSuggestRecordByKey(int id)
        {
            return SuggestRecordDA.Instance.DeleteSuggestRecordByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSuggestRecordDisabled()
        {
            return SuggestRecordDA.Instance.DeleteSuggestRecordDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSuggestRecordByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SuggestRecordDA.Instance.DeleteSuggestRecordByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSuggestRecordByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SuggestRecordDA.Instance.DisableSuggestRecordByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SuggestRecord实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SuggestRecord实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SuggestRecordEntity GetSuggestRecord(int id)
		{
			return SuggestRecordDA.Instance.GetSuggestRecord(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SuggestRecordEntity> GetSuggestRecordList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SuggestRecordDA.Instance.GetSuggestRecordList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSuggestRecordAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SuggestRecordListKey";// SysCacheKey.SuggestRecordListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<SuggestRecordEntity> list = null;
                    list = SuggestRecordDA.Instance.GetSuggestRecordAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SuggestRecordEntity suggestRecord)
        {
            return SuggestRecordDA.Instance.ExistNum(suggestRecord)>0;
        }
		
	}
}

