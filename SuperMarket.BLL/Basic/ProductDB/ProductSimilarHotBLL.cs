using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ProductSimilarHot的业务逻辑层。
创建时间：2016/9/8 15:01:54
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductSimilarHot的业务逻辑层。
	/// </summary>
	public class ProductSimilarHotBLL
	{
	    #region 实例化
        public static ProductSimilarHotBLL Instance
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
            internal static readonly ProductSimilarHotBLL instance = new ProductSimilarHotBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductSimilarHot，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSimilarHot">要添加的ProductSimilarHot数据实体对象</param>
		public   int AddProductSimilarHot(ProductSimilarHotEntity productSimilarHot)
		{
			  if (productSimilarHot.Id > 0)
            {
                return UpdateProductSimilarHot(productSimilarHot);
            }
          
            else if (ProductSimilarHotBLL.Instance.IsExist(productSimilarHot))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductSimilarHotDA.Instance.AddProductSimilarHot(productSimilarHot);
            }
	 	}

		/// <summary>
		/// 更新一条ProductSimilarHot记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSimilarHot">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductSimilarHot(ProductSimilarHotEntity productSimilarHot)
		{
			return ProductSimilarHotDA.Instance.UpdateProductSimilarHot(productSimilarHot);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductSimilarHotByKey(int id)
        {
            return ProductSimilarHotDA.Instance.DeleteProductSimilarHotByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSimilarHotDisabled()
        {
            return ProductSimilarHotDA.Instance.DeleteProductSimilarHotDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSimilarHotByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductSimilarHotDA.Instance.DeleteProductSimilarHotByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSimilarHotByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductSimilarHotDA.Instance.DisableProductSimilarHotByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductSimilarHot实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductSimilarHot实体</returns>
		/// <param name="columns">要返回的列</param>
		public    ProductSimilarHotEntity  GetProductSimilarHot(int id)
		{
			return ProductSimilarHotDA.Instance.GetProductSimilarHot(id);			
		}
        /// <summary>
        /// 获取分类同类热销产品
        /// </summary>
        /// <param name="classid"></param>
        /// <returns></returns>
        public IList<VWProductSimilarHotEntity> GetHotProductsByClassId(int classid,int num)
        {
            IList<VWProductSimilarHotEntity> list = null;
            string _cachekey = "GetHotProductsByClassId_" + classid+"_"+ num; 
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = ProductSimilarHotDA.Instance.GetHotProductsByClassId(classid,num);
                if (list != null && list.Count > 0)
                {
                    foreach (VWProductSimilarHotEntity entity in list)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    }
                }
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<VWProductSimilarHotEntity>)obj;
            }
            return list; 
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductSimilarHotEntity> GetProductSimilarHotList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductSimilarHotDA.Instance.GetProductSimilarHotList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWProductEntity> GetHotProducts(int classid)
        {
            int size =StringUtils.GetDbInt( CommonKey.HotStyleShowNum);
            return ProductSimilarHotDA.Instance.GetHotProducts(classid, size); 
        }
        public async Task GetProductSimilarHotAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductSimilarHotListKey";// SysCacheKey.ProductSimilarHotListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductSimilarHotEntity> list = null;
                    list = ProductSimilarHotDA.Instance.GetProductSimilarHotAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductSimilarHotEntity productSimilarHot)
        {
            return ProductSimilarHotDA.Instance.ExistNum(productSimilarHot)>0;
        }
		
	}
}

