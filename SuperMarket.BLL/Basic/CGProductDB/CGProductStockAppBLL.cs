using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductStockApp的业务逻辑层。
创建时间：2017/1/17 11:53:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductStockApp的业务逻辑层。
	/// </summary>
	public class CGProductStockAppBLL
	{
	    #region 实例化
        public static CGProductStockAppBLL Instance
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
            internal static readonly CGProductStockAppBLL instance = new CGProductStockAppBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductStockApp，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProductStockApp">要添加的CGProductStockApp数据实体对象</param>
		public   int AddCGProductStockApp(CGProductStockAppEntity cGProductStockApp)
		{
			  if (cGProductStockApp.Id > 0)
            {
                return UpdateCGProductStockApp(cGProductStockApp);
            }
          
            else if (CGProductStockAppBLL.Instance.IsExist(cGProductStockApp))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductStockAppDA.Instance.AddCGProductStockApp(cGProductStockApp);
            }
	 	}

		/// <summary>
		/// 更新一条CGProductStockApp记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProductStockApp">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProductStockApp(CGProductStockAppEntity cGProductStockApp)
		{
			return CGProductStockAppDA.Instance.UpdateCGProductStockApp(cGProductStockApp);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductStockAppByKey(int id)
        {
            return CGProductStockAppDA.Instance.DeleteCGProductStockAppByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductStockAppDisabled()
        {
            return CGProductStockAppDA.Instance.DeleteCGProductStockAppDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductStockAppByIds(string ids,int memid)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductStockAppDA.Instance.DeleteCGProductStockAppByIds(idstr, memid); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductStockAppByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductStockAppDA.Instance.DisableCGProductStockAppByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProductStockApp实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductStockApp实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductStockAppEntity GetCGProductStockApp(int id)
		{
			return CGProductStockAppDA.Instance.GetCGProductStockApp(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGProductStockAppEntity> GetCGProductStockAppList(int pageSize, int pageIndex, ref  int recordCount,int _memid,int _status)
        {
            return CGProductStockAppDA.Instance.GetCGProductStockAppList(pageSize, pageIndex, ref recordCount, _memid, _status);
        }
        public IList<int> GetCGMemIdList(int pageSize, int pageIndex, ref int recordCount ,int status)
        {
            return CGProductStockAppDA.Instance.GetCGMemIdList(pageSize, pageIndex, ref recordCount, status);
        }
        public int ProcAddCGStockApp(string products,int memid)
        {
            return CGProductStockAppDA.Instance.ProcAddCGStockApp(products, memid);

        }
        public int ProcEditCGStockApp(string products, int memid)
        {
            return CGProductStockAppDA.Instance.ProcEditCGStockApp(products, memid);

        }
        public async Task GetCGProductStockAppAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductStockAppListKey";// SysCacheKey.CGProductStockAppListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<CGProductStockAppEntity> list = null;
                    list = CGProductStockAppDA.Instance.GetCGProductStockAppAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductStockAppEntity cGProductStockApp)
        {
            return CGProductStockAppDA.Instance.ExistNum(cGProductStockApp)>0;
        }
		
	}
}

