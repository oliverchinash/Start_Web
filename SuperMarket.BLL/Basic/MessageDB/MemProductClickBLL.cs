using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表MemProductClick的业务逻辑层。
创建时间：2017/5/5 9:15:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表MemProductClick的业务逻辑层。
	/// </summary>
	public class MemProductClickBLL
	{
	    #region 实例化
        public static MemProductClickBLL Instance
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
            internal static readonly MemProductClickBLL instance = new MemProductClickBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemProductClick，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="MemProductClick">要添加的MemProductClick数据实体对象</param>
		public   int AddMemProductClick(MemProductClickEntity MemProductClick)
		{
		 
                return MemProductClickDA.Instance.AddMemProductClick(MemProductClick);
          
	 	}

		/// <summary>
		/// 更新一条MemProductClick记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="MemProductClick">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemProductClick(MemProductClickEntity MemProductClick)
		{
			return MemProductClickDA.Instance.UpdateMemProductClick(MemProductClick);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemProductClickByKey(int id)
        {
            return MemProductClickDA.Instance.DeleteMemProductClickByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemProductClickDisabled()
        {
            return MemProductClickDA.Instance.DeleteMemProductClickDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemProductClickByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemProductClickDA.Instance.DeleteMemProductClickByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemProductClickByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemProductClickDA.Instance.DisableMemProductClickByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个MemProductClick实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>MemProductClick实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemProductClickEntity GetMemProductClick(int id)
		{
			return MemProductClickDA.Instance.GetMemProductClick(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemProductClickEntity> GetMemProductClickList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemProductClickDA.Instance.GetMemProductClickList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetMemProductClickAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemProductClickListKey";// SysCacheKey.MemProductClickListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<MemProductClickEntity> list = null;
                    list = MemProductClickDA.Instance.GetMemProductClickAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemProductClickEntity MemProductClick)
        {
            return MemProductClickDA.Instance.ExistNum(MemProductClick)>0;
        }
		
	}
}

