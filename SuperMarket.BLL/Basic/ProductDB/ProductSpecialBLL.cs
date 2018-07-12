using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductSpecial的业务逻辑层。
创建时间：2017/5/16 11:30:38
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductSpecial的业务逻辑层。
	/// </summary>
	public class ProductSpecialBLL
	{
	    #region 实例化
        public static ProductSpecialBLL Instance
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
            internal static readonly ProductSpecialBLL instance = new ProductSpecialBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductSpecial，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSpecial">要添加的ProductSpecial数据实体对象</param>
		public   int AddProductSpecial(ProductSpecialEntity productSpecial)
		{
			  if (productSpecial.Id > 0)
            {
                return UpdateProductSpecial(productSpecial);
            }
				  else if (string.IsNullOrEmpty(productSpecial.ShortName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(productSpecial.FullName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ProductSpecialBLL.Instance.IsExist(productSpecial))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductSpecialDA.Instance.AddProductSpecial(productSpecial);
            }
	 	}

		/// <summary>
		/// 更新一条ProductSpecial记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSpecial">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductSpecial(ProductSpecialEntity productSpecial)
		{
			return ProductSpecialDA.Instance.UpdateProductSpecial(productSpecial);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductSpecialByKey(int id)
        {
            return ProductSpecialDA.Instance.DeleteProductSpecialByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSpecialDisabled()
        {
            return ProductSpecialDA.Instance.DeleteProductSpecialDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSpecialByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductSpecialDA.Instance.DeleteProductSpecialByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSpecialByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductSpecialDA.Instance.DisableProductSpecialByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductSpecial实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductSpecial实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductSpecialEntity GetProductSpecial(int id)
		{
			return ProductSpecialDA.Instance.GetProductSpecial(id);			
		}
        public ProductSpecialEntity GetProductSpecialByCode(string code)
        {
            return ProductSpecialDA.Instance.GetProductSpecialByCode(code);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductSpecialEntity> GetProductSpecialList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductSpecialDA.Instance.GetProductSpecialList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetProductSpecialAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductSpecialListKey";// SysCacheKey.ProductSpecialListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductSpecialEntity> list = null;
                    list = ProductSpecialDA.Instance.GetProductSpecialAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductSpecialEntity productSpecial)
        {
            return ProductSpecialDA.Instance.ExistNum(productSpecial)>0;
        }
		
	}
}

