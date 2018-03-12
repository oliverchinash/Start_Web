using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductStock的业务逻辑层。
创建时间：2017/1/17 11:53:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductStock的业务逻辑层。
	/// </summary>
	public class CGProductStockBLL
	{
	    #region 实例化
        public static CGProductStockBLL Instance
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
            internal static readonly CGProductStockBLL instance = new CGProductStockBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductStock，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProductStock">要添加的CGProductStock数据实体对象</param>
		public   int AddCGProductStock(CGProductStockEntity cGProductStock)
		{
			  if (cGProductStock.Id > 0)
            {
                return UpdateCGProductStock(cGProductStock);
            }
          
            else if (CGProductStockBLL.Instance.IsExist(cGProductStock))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductStockDA.Instance.AddCGProductStock(cGProductStock);
            }
	 	}

		/// <summary>
		/// 更新一条CGProductStock记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGProductStock">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProductStock(CGProductStockEntity cGProductStock)
		{
			return CGProductStockDA.Instance.UpdateCGProductStock(cGProductStock);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductStockByKey(int id)
        {
            return CGProductStockDA.Instance.DeleteCGProductStockByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductStockDisabled()
        {
            return CGProductStockDA.Instance.DeleteCGProductStockDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductStockByIds(string ids )
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductStockDA.Instance.DeleteCGProductStockByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStockByProIds(string ids,int memid)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductStockDA.Instance.DisableStockByProIds(idstr,memid);
        }
		/// <summary>
		/// 根据主键获取一个CGProductStock实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductStock实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductStockEntity GetCGProductStock(int id)
		{
			return CGProductStockDA.Instance.GetCGProductStock(id);			
		}
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGProductStockEntity> GetCGProductStockList(int pageSize, int pageIndex, ref int recordCount, int memid, int status, int classid, int brandid, string key)
        {
            return CGProductStockDA.Instance.GetVWCGProductStockList(pageSize, pageIndex, ref recordCount, memid, status, classid, brandid, key);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGProductStockEntity> GetVWCGProductStockList(int pageSize, int pageIndex, ref  int recordCount,int memid,int status,int classid,int brandid,string key)
        {
            return CGProductStockDA.Instance.GetVWCGProductStockList(pageSize, pageIndex, ref recordCount ,memid,   status,classid,brandid,   key);
        }
        public IList<CGProductStockEntity> GetCGProductStockList(int pageSize, int pageIndex, ref int recordCount, int _memid, int _status)
        {
            return CGProductStockDA.Instance.GetCGProductStockList(pageSize, pageIndex, ref recordCount, _memid, _status);
        }
        public IList<CGProductStockEntity> GetCGStockByProIds(string products,int memid)
        {
            return CGProductStockDA.Instance.GetCGStockByProIds(products, memid);
        }
        public int AcceptCGStockByProIds(string productids, int memid)
        {
            return CGProductStockDA.Instance.AcceptCGStockByProIds(productids, memid);
        }
        public int AcceptCGStockByMemId( int memid)
        {
            return CGProductStockDA.Instance.AcceptCGStockByMemId( memid);
        }
        public int RejectCGStockByProIds(string productids, int memid)
        {
            return CGProductStockDA.Instance.RejectCGStockByProIds(productids, memid);
        }
        /// <summary>
        /// 执行编辑产品库存和建议价格
        /// </summary>
        /// <param name="products"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int ProcEditCGStock(string products, int memid)
        {
            return CGProductStockDA.Instance.ProcEditCGStock(products, memid);

        }
        public async Task GetCGProductStockAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductStockListKey";// SysCacheKey.CGProductStockListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<CGProductStockEntity> list = null;
                    list = CGProductStockDA.Instance.GetCGProductStockAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductStockEntity cGProductStock)
        {
            return CGProductStockDA.Instance.ExistNum(cGProductStock)>0;
        }
		
	}
}

