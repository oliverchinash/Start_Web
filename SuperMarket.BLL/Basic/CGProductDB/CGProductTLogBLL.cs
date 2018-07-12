using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductTLog的业务逻辑层。
创建时间：2017/1/5 14:30:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductTLog的业务逻辑层。
	/// </summary>
	public class CGProductTLogBLL
	{
	    #region 实例化
        public static CGProductTLogBLL Instance
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
            internal static readonly CGProductTLogBLL instance = new CGProductTLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductTLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productTLog">要添加的CGProductTLog数据实体对象</param>
		public   int AddCGProductTLog(CGProductTLogEntity productTLog)
		{
			  if (productTLog.Id > 0)
            {
                return UpdateCGProductTLog(productTLog);
            }
          
            else if (CGProductTLogBLL.Instance.IsExist(productTLog))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductTLogDA.Instance.AddCGProductTLog(productTLog);
            }
	 	}

		/// <summary>
		/// 更新一条CGProductTLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productTLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProductTLog(CGProductTLogEntity productTLog)
		{
			return CGProductTLogDA.Instance.UpdateCGProductTLog(productTLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductTLogByKey(int id)
        {
            return CGProductTLogDA.Instance.DeleteCGProductTLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductTLogDisabled()
        {
            return CGProductTLogDA.Instance.DeleteCGProductTLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductTLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductTLogDA.Instance.DeleteCGProductTLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductTLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductTLogDA.Instance.DisableCGProductTLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProductTLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductTLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductTLogEntity GetCGProductTLog(int id)
		{
			return CGProductTLogDA.Instance.GetCGProductTLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGProductTLogEntity> GetCGProductTLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGProductTLogDA.Instance.GetCGProductTLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetCGProductTLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductTLogListKey";// SysCacheKey.CGProductTLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGProductTLogEntity> list = null;
                    list = CGProductTLogDA.Instance.GetCGProductTLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductTLogEntity productTLog)
        {
            return CGProductTLogDA.Instance.ExistNum(productTLog)>0;
        }
		
	}
}

