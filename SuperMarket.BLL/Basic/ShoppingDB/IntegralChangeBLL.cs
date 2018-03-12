using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表IntegralChange的业务逻辑层。
创建时间：2016/12/1 14:30:37
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表IntegralChange的业务逻辑层。
	/// </summary>
	public class IntegralChangeBLL
	{
	    #region 实例化
        public static IntegralChangeBLL Instance
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
            internal static readonly IntegralChangeBLL instance = new IntegralChangeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表IntegralChange，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="integralChange">要添加的IntegralChange数据实体对象</param>
		public   int AddIntegralChange(IntegralChangeEntity integralChange)
		{
			  if (integralChange.Id > 0)
            {
                return UpdateIntegralChange(integralChange);
            }
          
            else if (IntegralChangeBLL.Instance.IsExist(integralChange))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return IntegralChangeDA.Instance.AddIntegralChange(integralChange);
            }
	 	}

		/// <summary>
		/// 更新一条IntegralChange记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="integralChange">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateIntegralChange(IntegralChangeEntity integralChange)
		{
			return IntegralChangeDA.Instance.UpdateIntegralChange(integralChange);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteIntegralChangeByKey(int id)
        {
            return IntegralChangeDA.Instance.DeleteIntegralChangeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIntegralChangeDisabled()
        {
            return IntegralChangeDA.Instance.DeleteIntegralChangeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIntegralChangeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return IntegralChangeDA.Instance.DeleteIntegralChangeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIntegralChangeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return IntegralChangeDA.Instance.DisableIntegralChangeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个IntegralChange实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>IntegralChange实体</returns>
		/// <param name="columns">要返回的列</param>
		public   IntegralChangeEntity GetIntegralChange(int id)
		{
			return IntegralChangeDA.Instance.GetIntegralChange(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<IntegralChangeEntity> GetIntegralChangeList(int pageSize, int pageIndex, ref  int recordCount,int memid)
        {
            return IntegralChangeDA.Instance.GetIntegralChangeList(pageSize, pageIndex, ref recordCount, memid);
        }
		
		public async Task GetIntegralChangeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="IntegralChangeListKey";// SysCacheKey.IntegralChangeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<IntegralChangeEntity> list = null;
                    list = IntegralChangeDA.Instance.GetIntegralChangeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(IntegralChangeEntity integralChange)
        {
            return IntegralChangeDA.Instance.ExistNum(integralChange)>0;
        }
		
	}
}

