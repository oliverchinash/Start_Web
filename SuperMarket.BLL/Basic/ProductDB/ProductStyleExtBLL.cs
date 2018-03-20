using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductStyleExt的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductStyleExt的业务逻辑层。
	/// </summary>
	public class ProductStyleExtBLL
	{
	    #region 实例化
        public static ProductStyleExtBLL Instance
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
            internal static readonly ProductStyleExtBLL instance = new ProductStyleExtBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductStyleExt，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productStyleExt">要添加的ProductStyleExt数据实体对象</param>
		public   int AddProductStyleExt(ProductStyleExtEntity productStyleExt)
		{
			if (productStyleExt.Id > 0)
            {
                return UpdateProductStyleExt(productStyleExt);
            } 
            else if (ProductStyleExtBLL.Instance.IsExist(productStyleExt))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductStyleExtDA.Instance.AddProductStyleExt(productStyleExt);
            }
	 	}

		/// <summary>
		/// 更新一条ProductStyleExt记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productStyleExt">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductStyleExt(ProductStyleExtEntity productStyleExt)
		{
			return ProductStyleExtDA.Instance.UpdateProductStyleExt(productStyleExt);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductStyleExtByKey(int id)
        {
            return ProductStyleExtDA.Instance.DeleteProductStyleExtByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductStyleExtDisabled()
        {
            return ProductStyleExtDA.Instance.DeleteProductStyleExtDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductStyleExtByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductStyleExtDA.Instance.DeleteProductStyleExtByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductStyleExtByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductStyleExtDA.Instance.DisableProductStyleExtByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductStyleExt实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductStyleExt实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductStyleExtEntity GetProductStyleExt(int id)
		{
			return ProductStyleExtDA.Instance.GetProductStyleExt(id);			
		}
        public ProductStyleExtEntity GetStyleExtByStyleId(int _styleid)
        {
            return ProductStyleExtDA.Instance.GetStyleExtByStyleId(_styleid);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductStyleExtEntity> GetProductStyleExtList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductStyleExtDA.Instance.GetProductStyleExtList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetProductStyleExtAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductStyleExtListKey";// SysCacheKey.ProductStyleExtListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductStyleExtEntity> list = null;
                    list = ProductStyleExtDA.Instance.GetProductStyleExtAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductStyleExtEntity productStyleExt)
        {
            return ProductStyleExtDA.Instance.ExistNum(productStyleExt)>0;
        }
		
	}
}

