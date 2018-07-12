using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;

/*****************************************
功能描述：表ProductFineModule的业务逻辑层。
创建时间：2018/7/11 18:06:31
创 建 人：lgzh
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB 
{
	  
	/// <summary>
	/// 表ProductFineModule的业务逻辑层。
	/// </summary>
	public class ProductFineModuleBLL
	{
	    #region 实例化
        public static ProductFineModuleBLL Instance
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
            internal static readonly ProductFineModuleBLL instance = new ProductFineModuleBLL();
        }
        #endregion
		 #region  自动产生
		/// <summary>
		/// 插入一条记录到表ProductFineModule，返回操作数。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productFineModule">要添加的ProductFineModule数据实体对象</param>
		public   int AddProductFineModule(ProductFineModuleEntity productFineModule)
		{
			return ProductFineModuleDA.Instance.AddProductFineModule(productFineModule);
		}

		/// <summary>
		/// 更新一条ProductFineModule记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productFineModule">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductFineModule(ProductFineModuleEntity productFineModule)
		{
			return ProductFineModuleDA.Instance.UpdateProductFineModule(productFineModule);
		}
				/// <summary>
		/// 根据主键值删除记录。如果数据库不存在这条数据将返回0
		/// </summary>
		public  int  DeleteProductFineModuleByKey(int id)
	    {
		  	return  ProductFineModuleDA.Instance.DeleteProductFineModuleByKey(id);
	
		}
		/// <summary>
		/// 根据主键获取一个ProductFineModule实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductFineModule实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductFineModuleEntity GetProductFineModule(int id)
		{
			return ProductFineModuleDA.Instance.GetProductFineModule(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductFineModuleEntity> GetProductFineModuleList(int? siteid, int moduletype,int status=1)
        {
            if (siteid == null)
                siteid = 1;
            return ProductFineModuleDA.Instance.GetProductFineModuleList((int)siteid,moduletype, status);
        }
	  #endregion   
	}
}
