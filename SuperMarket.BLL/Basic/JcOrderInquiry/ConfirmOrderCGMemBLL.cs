using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ConfirmOrderCGMem的业务逻辑层。
创建时间：2017/10/18 16:28:47
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表ConfirmOrderCGMem的业务逻辑层。
	/// </summary>
	public class ConfirmOrderCGMemBLL
	{
	    #region 实例化
        public static ConfirmOrderCGMemBLL Instance
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
            internal static readonly ConfirmOrderCGMemBLL instance = new ConfirmOrderCGMemBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ConfirmOrderCGMem，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrderCGMem">要添加的ConfirmOrderCGMem数据实体对象</param>
		public   int AddConfirmOrderCGMem(ConfirmOrderCGMemEntity confirmOrderCGMem)
		{
			  if (confirmOrderCGMem.Id > 0)
            {
                return UpdateConfirmOrderCGMem(confirmOrderCGMem);
            }
          
            else if (ConfirmOrderCGMemBLL.Instance.IsExist(confirmOrderCGMem))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ConfirmOrderCGMemDA.Instance.AddConfirmOrderCGMem(confirmOrderCGMem);
            }
	 	}

		/// <summary>
		/// 更新一条ConfirmOrderCGMem记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrderCGMem">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfirmOrderCGMem(ConfirmOrderCGMemEntity confirmOrderCGMem)
		{
			return ConfirmOrderCGMemDA.Instance.UpdateConfirmOrderCGMem(confirmOrderCGMem);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteConfirmOrderCGMemByKey(int id)
        {
            return ConfirmOrderCGMemDA.Instance.DeleteConfirmOrderCGMemByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderCGMemDisabled()
        {
            return ConfirmOrderCGMemDA.Instance.DeleteConfirmOrderCGMemDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderCGMemByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ConfirmOrderCGMemDA.Instance.DeleteConfirmOrderCGMemByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderCGMemByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ConfirmOrderCGMemDA.Instance.DisableConfirmOrderCGMemByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ConfirmOrderCGMem实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfirmOrderCGMem实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfirmOrderCGMemEntity GetConfirmOrderCGMem(int id)
		{
			return ConfirmOrderCGMemDA.Instance.GetConfirmOrderCGMem(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ConfirmOrderCGMemEntity> GetConfirmOrderCGMemList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ConfirmOrderCGMemDA.Instance.GetConfirmOrderCGMemList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetConfirmOrderCGMemAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ConfirmOrderCGMemListKey";// SysCacheKey.ConfirmOrderCGMemListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<ConfirmOrderCGMemEntity> list = null;
                    list = ConfirmOrderCGMemDA.Instance.GetConfirmOrderCGMemAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ConfirmOrderCGMemEntity confirmOrderCGMem)
        {
            return ConfirmOrderCGMemDA.Instance.ExistNum(confirmOrderCGMem)>0;
        }
		
	}
}

