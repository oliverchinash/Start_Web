using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductWholeSale的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductWholeSale的业务逻辑层。
	/// </summary>
	public class ProductWholeSaleBLL
	{
	    #region 实例化
        public static ProductWholeSaleBLL Instance
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
            internal static readonly ProductWholeSaleBLL instance = new ProductWholeSaleBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductWholeSale，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productWholeSale">要添加的ProductWholeSale数据实体对象</param>
		public   int AddProductWholeSale(ProductWholeSaleEntity productWholeSale)
		{
			  if (productWholeSale.Id > 0)
            {
                return UpdateProductWholeSale(productWholeSale);
            }
          
            else if (ProductWholeSaleBLL.Instance.IsExist(productWholeSale))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductWholeSaleDA.Instance.AddProductWholeSale(productWholeSale);
            }
	 	}

		/// <summary>
		/// 更新一条ProductWholeSale记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productWholeSale">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductWholeSale(ProductWholeSaleEntity productWholeSale)
		{
			return ProductWholeSaleDA.Instance.UpdateProductWholeSale(productWholeSale);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductWholeSaleByKey(int id)
        {
            return ProductWholeSaleDA.Instance.DeleteProductWholeSaleByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductWholeSaleDisabled()
        {
            return ProductWholeSaleDA.Instance.DeleteProductWholeSaleDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductWholeSaleByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductWholeSaleDA.Instance.DeleteProductWholeSaleByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductWholeSaleByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductWholeSaleDA.Instance.DisableProductWholeSaleByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductWholeSale实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductWholeSale实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductWholeSaleEntity GetProductWholeSale(int id)
		{
			return ProductWholeSaleDA.Instance.GetProductWholeSale(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductWholeSaleEntity> GetProductWholeSaleList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductWholeSaleDA.Instance.GetProductWholeSaleList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetProductWholeSaleAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductWholeSaleListKey";// SysCacheKey.ProductWholeSaleListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductWholeSaleEntity> list = null;
                    list = ProductWholeSaleDA.Instance.GetProductWholeSaleAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductWholeSaleEntity productWholeSale)
        {
            return ProductWholeSaleDA.Instance.ExistNum(productWholeSale)>0;
        }
		
	}
}

