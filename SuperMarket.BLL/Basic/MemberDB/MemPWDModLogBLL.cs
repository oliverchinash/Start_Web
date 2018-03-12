using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemPWDModLog的业务逻辑层。
创建时间：2016/8/6 21:02:13
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemPWDModLog的业务逻辑层。
	/// </summary>
	public class MemPWDModLogBLL
	{
	    #region 实例化
        public static MemPWDModLogBLL Instance
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
            internal static readonly MemPWDModLogBLL instance = new MemPWDModLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemPWDModLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memPWDModLog">要添加的MemPWDModLog数据实体对象</param>
		public   int AddMemPWDModLog(MemPWDModLogEntity memPWDModLog)
		{
		        return MemPWDModLogDA.Instance.AddMemPWDModLog(memPWDModLog);
         
	 	}

		/// <summary>
		/// 更新一条MemPWDModLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memPWDModLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemPWDModLog(MemPWDModLogEntity memPWDModLog)
		{
			return MemPWDModLogDA.Instance.UpdateMemPWDModLog(memPWDModLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemPWDModLogByKey(int id)
        {
            return MemPWDModLogDA.Instance.DeleteMemPWDModLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemPWDModLogDisabled()
        {
            return MemPWDModLogDA.Instance.DeleteMemPWDModLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemPWDModLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemPWDModLogDA.Instance.DeleteMemPWDModLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemPWDModLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemPWDModLogDA.Instance.DisableMemPWDModLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemPWDModLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemPWDModLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemPWDModLogEntity GetMemPWDModLog(int id)
		{
			return MemPWDModLogDA.Instance.GetMemPWDModLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemPWDModLogEntity> GetMemPWDModLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemPWDModLogDA.Instance.GetMemPWDModLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemPWDModLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemPWDModLogListKey";// SysCacheKey.MemPWDModLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemPWDModLogEntity> list = null;
                    list = MemPWDModLogDA.Instance.GetMemPWDModLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemPWDModLogEntity memPWDModLog)
        {
            return MemPWDModLogDA.Instance.ExistNum(memPWDModLog)>0;
        }
		
	}
}

