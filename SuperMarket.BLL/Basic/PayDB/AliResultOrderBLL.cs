using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.PayDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表AliResultOrder的业务逻辑层。
创建时间：2017/11/29 10:53:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.PayDB
{
	  
	/// <summary>
	/// 表AliResultOrder的业务逻辑层。
	/// </summary>
	public class AliResultOrderBLL
	{
	    #region 实例化
        public static AliResultOrderBLL Instance
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
            internal static readonly AliResultOrderBLL instance = new AliResultOrderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表AliResultOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="aliResultOrder">要添加的AliResultOrder数据实体对象</param>
		public   int AddAliResultOrder(AliResultOrderEntity aliResultOrder)
		{
			if (aliResultOrder.Id > 0)
            {
                return UpdateAliResultOrder(aliResultOrder);
            } 
            else if (AliResultOrderBLL.Instance.IsExist(aliResultOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AliResultOrderDA.Instance.AddAliResultOrder(aliResultOrder);
            }
	 	}

		/// <summary>
		/// 更新一条AliResultOrder记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="aliResultOrder">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAliResultOrder(AliResultOrderEntity aliResultOrder)
		{
			return AliResultOrderDA.Instance.UpdateAliResultOrder(aliResultOrder);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAliResultOrderByKey(int id)
        {
            return AliResultOrderDA.Instance.DeleteAliResultOrderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAliResultOrderDisabled()
        {
            return AliResultOrderDA.Instance.DeleteAliResultOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAliResultOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AliResultOrderDA.Instance.DeleteAliResultOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAliResultOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AliResultOrderDA.Instance.DisableAliResultOrderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个AliResultOrder实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>AliResultOrder实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AliResultOrderEntity GetAliResultOrder(int id)
		{
			return AliResultOrderDA.Instance.GetAliResultOrder(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AliResultOrderEntity> GetAliResultOrderList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return AliResultOrderDA.Instance.GetAliResultOrderList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetAliResultOrderAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AliResultOrderListKey";// SysCacheKey.AliResultOrderListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<AliResultOrderEntity> list = null;
                    list = AliResultOrderDA.Instance.GetAliResultOrderAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AliResultOrderEntity aliResultOrder)
        {
            return AliResultOrderDA.Instance.ExistNum(aliResultOrder)>0;
        }
		
	}
}

