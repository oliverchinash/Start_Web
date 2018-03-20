using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表StoreBusscope的业务逻辑层。
创建时间：2016/8/1 15:04:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表StoreBusscope的业务逻辑层。
	/// </summary>
	public class StoreBusscopeBLL
	{
	    #region 实例化
        public static StoreBusscopeBLL Instance
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
            internal static readonly StoreBusscopeBLL instance = new StoreBusscopeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StoreBusscope，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="supplierBusscope">要添加的StoreBusscope数据实体对象</param>
		public   int AddStoreBusscope(MemStoreBusscopeEntity supplierBusscope)
		{
			  if (supplierBusscope.Id > 0)
            {
                return UpdateStoreBusscope(supplierBusscope);
            }
          
            else if (StoreBusscopeBLL.Instance.IsExist(supplierBusscope))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemSupplierBusscopeDA.Instance.AddStoreBusscope(supplierBusscope);
            }
	 	}

		/// <summary>
		/// 更新一条StoreBusscope记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="supplierBusscope">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStoreBusscope(MemStoreBusscopeEntity supplierBusscope)
		{
			return MemSupplierBusscopeDA.Instance.UpdateStoreBusscope(supplierBusscope);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStoreBusscopeByKey(int id)
        {
            return MemSupplierBusscopeDA.Instance.DeleteStoreBusscopeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStoreBusscopeDisabled()
        {
            return MemSupplierBusscopeDA.Instance.DeleteStoreBusscopeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStoreBusscopeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemSupplierBusscopeDA.Instance.DeleteStoreBusscopeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStoreBusscopeDAByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemSupplierBusscopeDA.Instance.DisableStoreBusscopeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StoreBusscope实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StoreBusscope实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemStoreBusscopeEntity GetStoreBusscope(int id)
		{
			return MemSupplierBusscopeDA.Instance.GetStoreBusscope(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemStoreBusscopeEntity> GetStoreBusscopeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemSupplierBusscopeDA.Instance.GetStoreBusscopeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetStoreBusscopeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="StoreBusscopeListKey";// SysCacheKey.StoreBusscopeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemStoreBusscopeEntity> list = null;
                    list = MemSupplierBusscopeDA.Instance.GetStoreBusscopeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemStoreBusscopeEntity supplierBusscope)
        {
            return MemSupplierBusscopeDA.Instance.ExistNum(supplierBusscope)>0;
        }
		
	}
}

