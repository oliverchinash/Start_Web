using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表Integral的业务逻辑层。
创建时间：2016/12/1 14:30:36
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表Integral的业务逻辑层。
	/// </summary>
	public class IntegralBLL
	{
	    #region 实例化
        public static IntegralBLL Instance
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
            internal static readonly IntegralBLL instance = new IntegralBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Integral，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="integral">要添加的Integral数据实体对象</param>
		public   int AddIntegral(IntegralEntity integral)
		{
			  if (integral.Id > 0)
            {
                return UpdateIntegral(integral);
            }
          
            else if (IntegralBLL.Instance.IsExist(integral))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return IntegralDA.Instance.AddIntegral(integral);
            }
	 	}

		/// <summary>
		/// 更新一条Integral记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="integral">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateIntegral(IntegralEntity integral)
		{
			return IntegralDA.Instance.UpdateIntegral(integral);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteIntegralByKey(int id)
        {
            return IntegralDA.Instance.DeleteIntegralByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteIntegralDisabled()
        {
            return IntegralDA.Instance.DeleteIntegralDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteIntegralByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return IntegralDA.Instance.DeleteIntegralByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableIntegralByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return IntegralDA.Instance.DisableIntegralByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个Integral实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Integral实体</returns>
		/// <param name="columns">要返回的列</param>
		public   IntegralEntity GetIntegral(int id)
		{
			return IntegralDA.Instance.GetIntegral(id);			
		}
        public IntegralEntity GetIntegralByMemId(int memid)
        {
            return IntegralDA.Instance.GetIntegralByMemId(memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<IntegralEntity> GetIntegralList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return IntegralDA.Instance.GetIntegralList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetIntegralAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="IntegralListKey";// SysCacheKey.IntegralListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<IntegralEntity> list = null;
                    list = IntegralDA.Instance.GetIntegralAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(IntegralEntity integral)
        {
            return IntegralDA.Instance.ExistNum(integral)>0;
        }
		
	}
}

