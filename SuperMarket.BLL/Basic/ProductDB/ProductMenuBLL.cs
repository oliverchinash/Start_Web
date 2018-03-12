using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.CatograyDB;

/*****************************************
功能描述：表ProductMenu的业务逻辑层。
创建时间：2017/2/15 9:13:45
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductMenu的业务逻辑层。
	/// </summary>
	public class ProductMenuBLL
	{
	    #region 实例化
        public static ProductMenuBLL Instance
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
            internal static readonly ProductMenuBLL instance = new ProductMenuBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductMenu，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productMenu">要添加的ProductMenu数据实体对象</param>
		public   int AddProductMenu(ProductMenuEntity productMenu)
		{
			if (productMenu.Id > 0)
            {
                return UpdateProductMenu(productMenu);
            } 
            else if (ProductMenuBLL.Instance.IsExist(productMenu))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductMenuDA.Instance.AddProductMenu(productMenu);
            }
	 	}

		/// <summary>
		/// 更新一条ProductMenu记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productMenu">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductMenu(ProductMenuEntity productMenu)
		{
			return ProductMenuDA.Instance.UpdateProductMenu(productMenu);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductMenuByKey(int id)
        {
            return ProductMenuDA.Instance.DeleteProductMenuByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductMenuDisabled()
        {
            return ProductMenuDA.Instance.DeleteProductMenuDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductMenuByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductMenuDA.Instance.DeleteProductMenuByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductMenuByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductMenuDA.Instance.DisableProductMenuByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductMenu实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductMenu实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductMenuEntity GetProductMenu(int id)
		{
			return ProductMenuDA.Instance.GetProductMenu(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductMenuEntity> GetProductMenuList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductMenuDA.Instance.GetProductMenuList(pageSize, pageIndex, ref recordCount);
        }

        public IList<VWProductMenuEntity> GetProductMenuAll(int menutype,int isactive)
        {
            IList<VWProductMenuEntity> list = null;

            string _cachekey = "GetProductMenuAll"+ menutype + isactive;// SysCacheKey.ProductMenuListKey;
            object obj = MemCache.GetCache(_cachekey);  
            if (obj == null)
            {
                list = ProductMenuDA.Instance.GetProductMenuAll(menutype, isactive);
                foreach (VWProductMenuEntity entity in list)
                {  
                    if (entity.ProductDetailId > 0)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    } 
                    if (entity.ClassId > 0)
                    {
                        entity.ClassesFound = ClassesFoundBLL.Instance.GetClassesFound(entity.ClassId);
                    }
                    if (entity.BrandId > 0)
                    {
                        entity.Brand = BrandBLL.Instance.GetBrand(entity.BrandId);
                    }
                }
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<VWProductMenuEntity>)obj;
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductMenuEntity productMenu)
        {
            return ProductMenuDA.Instance.ExistNum(productMenu)>0;
        }
		
	}
}

