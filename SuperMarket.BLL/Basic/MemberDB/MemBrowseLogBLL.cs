using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.ProductDB;

/*****************************************
功能描述：表MemBrowseLog的业务逻辑层。
创建时间：2017/5/5 9:15:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemBrowseLog的业务逻辑层。
	/// </summary>
	public class MemBrowseLogBLL
	{
	    #region 实例化
        public static MemBrowseLogBLL Instance
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
            internal static readonly MemBrowseLogBLL instance = new MemBrowseLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemBrowseLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="MemBrowseLog">要添加的MemBrowseLog数据实体对象</param>
		public   int AddMemBrowseLog(MemBrowseLogEntity MemBrowseLog)
		{
		 
                return MemBrowseLogDA.Instance.AddMemBrowseLog(MemBrowseLog);
          
	 	}
        
        public int AddMemBrowseLogStr(string MemBrowsesStr, int memid, int systemid)
        {

            return MemBrowseLogDA.Instance.AddMemBrowseLogStr(MemBrowsesStr,   memid,   systemid);

        }
        /// <summary>
        /// 更新一条MemBrowseLog记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="MemBrowseLog">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateMemBrowseLog(MemBrowseLogEntity MemBrowseLog)
		{
			return MemBrowseLogDA.Instance.UpdateMemBrowseLog(MemBrowseLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemBrowseLogByKey(int id)
        {
            return MemBrowseLogDA.Instance.DeleteMemBrowseLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemBrowseLogDisabled()
        {
            return MemBrowseLogDA.Instance.DeleteMemBrowseLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemBrowseLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemBrowseLogDA.Instance.DeleteMemBrowseLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemBrowseLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemBrowseLogDA.Instance.DisableMemBrowseLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemBrowseLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemBrowseLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemBrowseLogEntity GetMemBrowseLog(int id)
		{
			return MemBrowseLogDA.Instance.GetMemBrowseLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemBrowseLogEntity> GetMemBrowseLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemBrowseLogDA.Instance.GetMemBrowseLogList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWMemBrowseLogEntity> GetVWMemBrowseLogList(int pageIndex, int pageSize, ref int recordCount, int memid )
        {
            IList<VWMemBrowseLogEntity> list = MemBrowseLogDA.Instance.GetVWMemFavoritesList(pageIndex, pageSize, ref recordCount, memid );
            if (list != null && list.Count > 0)
            {
                foreach (VWMemBrowseLogEntity entity in list)
                {
                    VWProductEntity _vwentity = new VWProductEntity();
                    _vwentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    entity.ProductEntity = _vwentity;
                }
            }
            return list;
        }
        public async Task GetMemBrowseLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemBrowseLogListKey";// SysCacheKey.MemBrowseLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemBrowseLogEntity> list = null;
                    list = MemBrowseLogDA.Instance.GetMemBrowseLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemBrowseLogEntity MemBrowseLog)
        {
            return MemBrowseLogDA.Instance.ExistNum(MemBrowseLog)>0;
        }
		
	}
}

