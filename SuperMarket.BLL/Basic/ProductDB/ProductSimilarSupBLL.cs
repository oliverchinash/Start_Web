using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductSimilarSup的业务逻辑层。
创建时间：2016/9/8 15:01:54
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductSimilarSup的业务逻辑层。
	/// </summary>
	public class ProductSimilarSupBLL
	{
	    #region 实例化
        public static ProductSimilarSupBLL Instance
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
            internal static readonly ProductSimilarSupBLL instance = new ProductSimilarSupBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductSimilarSup，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSimilarSup">要添加的ProductSimilarSup数据实体对象</param>
		public   int AddProductSimilarSup(ProductSimilarSupEntity productSimilarSup)
		{
			  if (productSimilarSup.Id > 0)
            {
                return UpdateProductSimilarSup(productSimilarSup);
            }
          
            else if (ProductSimilarSupBLL.Instance.IsExist(productSimilarSup))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductSimilarSupDA.Instance.AddProductSimilarSup(productSimilarSup);
            }
	 	}

		/// <summary>
		/// 更新一条ProductSimilarSup记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSimilarSup">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductSimilarSup(ProductSimilarSupEntity productSimilarSup)
		{
			return ProductSimilarSupDA.Instance.UpdateProductSimilarSup(productSimilarSup);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductSimilarSupByKey(int id)
        {
            return ProductSimilarSupDA.Instance.DeleteProductSimilarSupByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSimilarSupDisabled()
        {
            return ProductSimilarSupDA.Instance.DeleteProductSimilarSupDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSimilarSupByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductSimilarSupDA.Instance.DeleteProductSimilarSupByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSimilarSupByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductSimilarSupDA.Instance.DisableProductSimilarSupByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductSimilarSup实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductSimilarSup实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductSimilarSupEntity GetProductSimilarSup(int id)
		{
			return ProductSimilarSupDA.Instance.GetProductSimilarSup(id);			
		}
        /// <summary>
        /// 获取分类组合热销产品
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IList<VWProductEntity> GetSupProductsByClassId(int classid)
        {
            IList<VWProductEntity> list = null;
            string _cachekey = "GetSupProductsByClassId_" + classid;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = ProductSimilarSupDA.Instance.GetSupProductsByClassId(classid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<VWProductEntity>)obj;
            }
            return list;
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductSimilarSupEntity> GetProductSimilarSupList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductSimilarSupDA.Instance.GetProductSimilarSupList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetProductSimilarSupAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductSimilarSupListKey";// SysCacheKey.ProductSimilarSupListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductSimilarSupEntity> list = null;
                    list = ProductSimilarSupDA.Instance.GetProductSimilarSupAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductSimilarSupEntity productSimilarSup)
        {
            return ProductSimilarSupDA.Instance.ExistNum(productSimilarSup)>0;
        }
		
	}
}

