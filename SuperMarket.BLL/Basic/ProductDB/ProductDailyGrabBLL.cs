using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductDailyGrab的业务逻辑层。
创建时间：2017/10/10 9:07:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductDailyGrab的业务逻辑层。
	/// </summary>
	public class ProductDailyGrabBLL
	{
	    #region 实例化
        public static ProductDailyGrabBLL Instance
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
            internal static readonly ProductDailyGrabBLL instance = new ProductDailyGrabBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductDailyGrab，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productDailyGrab">要添加的ProductDailyGrab数据实体对象</param>
		public   int AddProductDailyGrab(ProductDailyGrabEntity productDailyGrab)
		{
			  if (productDailyGrab.Id > 0)
            {
                return UpdateProductDailyGrab(productDailyGrab);
            }
          
            else if (ProductDailyGrabBLL.Instance.IsExist(productDailyGrab))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductDailyGrabDA.Instance.AddProductDailyGrab(productDailyGrab);
            }
	 	}

		/// <summary>
		/// 更新一条ProductDailyGrab记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productDailyGrab">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductDailyGrab(ProductDailyGrabEntity productDailyGrab)
		{
			return ProductDailyGrabDA.Instance.UpdateProductDailyGrab(productDailyGrab);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductDailyGrabByKey(int id)
        {
            return ProductDailyGrabDA.Instance.DeleteProductDailyGrabByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductDailyGrabDisabled()
        {
            return ProductDailyGrabDA.Instance.DeleteProductDailyGrabDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductDailyGrabByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductDailyGrabDA.Instance.DeleteProductDailyGrabByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductDailyGrabByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductDailyGrabDA.Instance.DisableProductDailyGrabByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductDailyGrab实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductDailyGrab实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductDailyGrabEntity GetProductDailyGrab(int id)
		{
			return ProductDailyGrabDA.Instance.GetProductDailyGrab(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductDailyGrabEntity> GetProductDailyGrabList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductDailyGrabDA.Instance.GetProductDailyGrabList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<VWProductDailyGrabEntity> GetVWProductDailyGrabAll(int active)
        {
             IList<VWProductDailyGrabEntity> list = null;
             list = ProductDailyGrabDA.Instance.GetVWProductDailyGrabAll(active);
             if(list!=null&& list.Count>0)
             {
                foreach(VWProductDailyGrabEntity vwentity in list)
                {
                    vwentity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(vwentity.ProductDetailId);
                } 
             }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductDailyGrabEntity productDailyGrab)
        {
            return ProductDailyGrabDA.Instance.ExistNum(productDailyGrab)>0;
        }
		
	}
}

