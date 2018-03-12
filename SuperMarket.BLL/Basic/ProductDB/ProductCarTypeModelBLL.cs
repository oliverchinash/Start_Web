using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ProductCarTypeModel的业务逻辑层。
创建时间：2017/6/20 16:42:38
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductCarTypeModel的业务逻辑层。
	/// </summary>
	public class ProductCarTypeModelBLL
	{
	    #region 实例化
        public static ProductCarTypeModelBLL Instance
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
            internal static readonly ProductCarTypeModelBLL instance = new ProductCarTypeModelBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductCarTypeModel，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productCarTypeModel">要添加的ProductCarTypeModel数据实体对象</param>
		public   int AddProductCarTypeModel(ProductCarTypeModelEntity productCarTypeModel)
		{
			  if (productCarTypeModel.Id > 0)
            {
                return UpdateProductCarTypeModel(productCarTypeModel);
            }
          
            else if (ProductCarTypeModelBLL.Instance.IsExist(productCarTypeModel))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductCarTypeModelDA.Instance.AddProductCarTypeModel(productCarTypeModel);
            }
	 	}

		/// <summary>
		/// 更新一条ProductCarTypeModel记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productCarTypeModel">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductCarTypeModel(ProductCarTypeModelEntity productCarTypeModel)
		{
			return ProductCarTypeModelDA.Instance.UpdateProductCarTypeModel(productCarTypeModel);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductCarTypeModelByKey(int id)
        {
            return ProductCarTypeModelDA.Instance.DeleteProductCarTypeModelByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductCarTypeModelDisabled()
        {
            return ProductCarTypeModelDA.Instance.DeleteProductCarTypeModelDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductCarTypeModelByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductCarTypeModelDA.Instance.DeleteProductCarTypeModelByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductCarTypeModelByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductCarTypeModelDA.Instance.DisableProductCarTypeModelByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductCarTypeModel实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductCarTypeModel实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductCarTypeModelEntity GetProductCarTypeModel(int id)
		{
			return ProductCarTypeModelDA.Instance.GetProductCarTypeModel(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductCarTypeModelEntity> GetProductCarTypeModelList(int pageSize, int pageIndex, ref  int recordCount,int _pid)
        {
            return ProductCarTypeModelDA.Instance.GetProductCarTypeModelList(pageSize, pageIndex, ref recordCount, _pid);
        }
		
		public async Task GetProductCarTypeModelAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductCarTypeModelListKey";// SysCacheKey.ProductCarTypeModelListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<ProductCarTypeModelEntity> list = null;
                    list = ProductCarTypeModelDA.Instance.GetProductCarTypeModelAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductCarTypeModelEntity productCarTypeModel)
        {
            return ProductCarTypeModelDA.Instance.ExistNum(productCarTypeModel)>0;
        }
		
	}
}

