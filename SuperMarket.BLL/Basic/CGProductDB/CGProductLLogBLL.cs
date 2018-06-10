using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductLLog的业务逻辑层。
创建时间：2017/1/5 14:30:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductLLog的业务逻辑层。
	/// </summary>
	public class CGProductLLogBLL
	{
	    #region 实例化
        public static CGProductLLogBLL Instance
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
            internal static readonly CGProductLLogBLL instance = new CGProductLLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductLLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productLLog">要添加的CGProductLLog数据实体对象</param>
		public   int AddCGProductLLog(CGProductLLogEntity productLLog)
		{
			  if (productLLog.Id > 0)
            {
                return UpdateCGProductLLog(productLLog);
            }
          
            else if (CGProductLLogBLL.Instance.IsExist(productLLog))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductLLogDA.Instance.AddCGProductLLog(productLLog);
            }
	 	}

		/// <summary>
		/// 更新一条CGProductLLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productLLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProductLLog(CGProductLLogEntity productLLog)
		{
			return CGProductLLogDA.Instance.UpdateCGProductLLog(productLLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductLLogByKey(int id)
        {
            return CGProductLLogDA.Instance.DeleteCGProductLLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductLLogDisabled()
        {
            return CGProductLLogDA.Instance.DeleteCGProductLLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductLLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductLLogDA.Instance.DeleteCGProductLLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductLLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductLLogDA.Instance.DisableCGProductLLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProductLLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductLLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductLLogEntity GetCGProductLLog(int id)
		{
			return CGProductLLogDA.Instance.GetCGProductLLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGProductLLogEntity> GetCGProductLLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGProductLLogDA.Instance.GetCGProductLLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetCGProductLLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductLLogListKey";// SysCacheKey.CGProductLLogListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<CGProductLLogEntity> list = null;
                    list = CGProductLLogDA.Instance.GetCGProductLLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductLLogEntity productLLog)
        {
            return CGProductLLogDA.Instance.ExistNum(productLLog)>0;
        }
		
	}
}

