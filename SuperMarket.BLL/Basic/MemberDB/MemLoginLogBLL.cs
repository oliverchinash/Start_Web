using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemLoginLog的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemLoginLog的业务逻辑层。
	/// </summary>
	public class MemLoginLogBLL
	{
	    #region 实例化
        public static MemLoginLogBLL Instance
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
            internal static readonly MemLoginLogBLL instance = new MemLoginLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemLoginLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memLoginLog">要添加的MemLoginLog数据实体对象</param>
		public   int AddMemLoginLog(MemLoginLogEntity memLoginLog)
		{
			  if (memLoginLog.Id > 0)
            {
                return UpdateMemLoginLog(memLoginLog);
            }
          
            else if (MemLoginLogBLL.Instance.IsExist(memLoginLog))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemLoginLogDA.Instance.AddMemLoginLog(memLoginLog);
            }
	 	}

		/// <summary>
		/// 更新一条MemLoginLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memLoginLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemLoginLog(MemLoginLogEntity memLoginLog)
		{
			return MemLoginLogDA.Instance.UpdateMemLoginLog(memLoginLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemLoginLogByKey(int id)
        {
            return MemLoginLogDA.Instance.DeleteMemLoginLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemLoginLogDisabled()
        {
            return MemLoginLogDA.Instance.DeleteMemLoginLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemLoginLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemLoginLogDA.Instance.DeleteMemLoginLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemLoginLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemLoginLogDA.Instance.DisableMemLoginLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemLoginLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemLoginLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemLoginLogEntity GetMemLoginLog(int id)
		{
			return MemLoginLogDA.Instance.GetMemLoginLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemLoginLogEntity> GetMemLoginLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemLoginLogDA.Instance.GetMemLoginLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemLoginLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemLoginLogListKey";// SysCacheKey.MemLoginLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemLoginLogEntity> list = null;
                    list = MemLoginLogDA.Instance.GetMemLoginLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemLoginLogEntity memLoginLog)
        {
            return MemLoginLogDA.Instance.ExistNum(memLoginLog)>0;
        }
		
	}
}

