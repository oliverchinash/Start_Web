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
功能描述：表ProductProperties的业务逻辑层。
创建时间：2018/4/14 16:58:52
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductProperties的业务逻辑层。
	/// </summary>
	public class ProductPropertiesBLL
	{
	    #region 实例化
        public static ProductPropertiesBLL Instance
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
            internal static readonly ProductPropertiesBLL instance = new ProductPropertiesBLL();
        }
        #endregion
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductProperties，返回操作数。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productProperties">要添加的ProductProperties数据实体对象</param>
		public   int AddProductProperties(ProductPropertiesEntity productProperties)
		{
			return ProductPropertiesDA.Instance.AddProductProperties(productProperties);
		}

		/// <summary>
		/// 更新一条ProductProperties记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productProperties">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductProperties(ProductPropertiesEntity productProperties)
		{
			return ProductPropertiesDA.Instance.UpdateProductProperties(productProperties);
		}
				/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductPropertiesByKey(int id)
	    {
		  	return  ProductPropertiesDA.Instance.DeleteProductPropertiesByKey(id);
	
		}
		/// <summary>
		/// 根据主键获取一个ProductProperties实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductProperties实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductPropertiesEntity GetProductProperties(int id)
		{
			return ProductPropertiesDA.Instance.GetProductProperties(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductPropertiesEntity> GetProductPropertiesList(int pageSize, int pageIndex, ref  int recordCount)
        {
            return ProductPropertiesDA.Instance.GetProductPropertiesList(pageSize, pageIndex, ref recordCount);
        }
	  #endregion   
	}
}
