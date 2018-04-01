using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductFine的业务逻辑层。
创建时间：2017/4/19 23:16:55
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductFine的业务逻辑层。
	/// </summary>
	public class ProductFineBLL
	{
	    #region 实例化
        public static ProductFineBLL Instance
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
            internal static readonly ProductFineBLL instance = new ProductFineBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductFine，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productFine">要添加的ProductFine数据实体对象</param>
		public   int AddProductFine(ProductFineEntity productFine)
		{
			  if (productFine.Id > 0)
            {
                return UpdateProductFine(productFine);
            }
          
            else if (ProductFineBLL.Instance.IsExist(productFine))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductFineDA.Instance.AddProductFine(productFine);
            }
	 	}

		/// <summary>
		/// 更新一条ProductFine记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productFine">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductFine(ProductFineEntity productFine)
		{
			return ProductFineDA.Instance.UpdateProductFine(productFine);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductFineByKey(int id)
        {
            return ProductFineDA.Instance.DeleteProductFineByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductFineDisabled()
        {
            return ProductFineDA.Instance.DeleteProductFineDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductFineByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductFineDA.Instance.DeleteProductFineByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductFineByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductFineDA.Instance.DisableProductFineByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductFine实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductFine实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductFineEntity GetProductFine(int id)
		{
			return ProductFineDA.Instance.GetProductFine(id);			
		}
		///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWProductFineEntity> GetProductFineList( int pageIndex, int pageSize, ref  int recordCount,int finetype,bool iscahce=false)
        {
            IList<VWProductFineEntity> list = null;
            if (!iscahce)
            {
                list = ProductFineDA.Instance.GetProductFineList(pageSize, pageIndex, ref recordCount, finetype);
                if (list != null && list.Count > 0)
                {
                    foreach (VWProductFineEntity entity in list)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    }
                }
            }
            else
            {

                string _cachekey = "GetProductFineList" + pageIndex + "_" + pageSize + "_" + finetype;// SysCacheKey.ProductFineListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    list = ProductFineDA.Instance.GetProductFineList(pageSize, pageIndex, ref recordCount, finetype);
                    if (list != null && list.Count > 0)
                    {
                        foreach (VWProductFineEntity entity in list)
                        {
                            entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                        }
                    }
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<VWProductFineEntity>)obj;
                }
            }
            return list;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWProductFineEntity> GetProductFineTopNum(int finetype, int num)
        {
            IList<VWProductFineEntity> list = null;
            string _cachekey = "GetProductFineTopNum" + finetype + "_" + num;// SysCacheKey.ProductFineListKey;
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                list = ProductFineDA.Instance.GetProductFineTopNum(finetype, num);
                if (list != null && list.Count > 0)
                {
                    foreach (VWProductFineEntity entity in list)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    }
                }
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list = (IList<VWProductFineEntity>)obj;
            }
            return list;
        }
        public async Task GetProductFineAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductFineListKey";// SysCacheKey.ProductFineListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<ProductFineEntity> list = null;
                    list = ProductFineDA.Instance.GetProductFineAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductFineEntity productFine)
        {
            return ProductFineDA.Instance.ExistNum(productFine)>0;
        }
		
	}
}

