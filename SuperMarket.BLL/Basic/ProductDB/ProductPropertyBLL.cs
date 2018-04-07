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
功能描述：表ProductProperty的业务逻辑层。
创建时间：2016/10/31 16:28:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductProperty的业务逻辑层。
	/// </summary>
	public class ProductPropertyBLL
	{
	    #region 实例化
        public static ProductPropertyBLL Instance
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
            internal static readonly ProductPropertyBLL instance = new ProductPropertyBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductProperty，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productProperty">要添加的ProductProperty数据实体对象</param>
		public   int AddProductProperty(ProductPropertyEntity productProperty)
		{
			  if (productProperty.Id > 0)
            {
                return UpdateProductProperty(productProperty);
            }
			else if (string.IsNullOrEmpty(productProperty.PropertyName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	  
            else if (ProductPropertyBLL.Instance.IsExist(productProperty))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductPropertyDA.Instance.AddProductProperty(productProperty);
            }
	 	}
        public int ProcAddProperties(int classid,int productid,string propertiesstr)
        {
            return ProductPropertyDA.Instance.ProcAddProperties(classid,productid, propertiesstr);

        }
    
        /// <summary>
        /// 更新一条ProductProperty记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productProperty">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateProductProperty(ProductPropertyEntity productProperty)
		{
			return ProductPropertyDA.Instance.UpdateProductProperty(productProperty);
		}
        /// <summary>
        /// 判断产品对应的属性值是否设置，没有则设置，有则修改
        /// </summary>
        /// <param name="productid"></param>
        /// <param name="propertyid"></param>
        /// <param name="propertdetailid"></param>
        /// <returns></returns>
        public int EditProductProperty(int productid,int propertyid,int propertdetailid)
        {
            return ProductPropertyDA.Instance.EditProductProperty(productid, propertyid, propertdetailid); 
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductPropertyByKey(int id)
        {
            return ProductPropertyDA.Instance.DeleteProductPropertyByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductPropertyDisabled()
        {
            return ProductPropertyDA.Instance.DeleteProductPropertyDisabled();
        } 
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductPropertyByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductPropertyDA.Instance.DeleteProductPropertyByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductPropertyByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductPropertyDA.Instance.DisableProductPropertyByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductProperty实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductProperty实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductPropertyEntity GetProductProperty(int id)
		{
			return ProductPropertyDA.Instance.GetProductProperty(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductPropertyEntity> GetProductPropertyList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductPropertyDA.Instance.GetProductPropertyList(pageSize, pageIndex, ref recordCount);
        }
        public IList<ProductPropertyEntity> GetListByProductId(int productid,bool cache=false)
        {
            IList<ProductPropertyEntity> list = null;
            if (cache)
            {
                string _cachekey = "ProductPropertyList_" + productid;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = ProductPropertyDA.Instance.GetPropertyByProductId(productid);
                    foreach (ProductPropertyEntity _entity in list)
                    {
                        _entity.PropertyName = BasicSitePropertiesBLL.Instance.GetBasicSiteProperties(_entity.PropertyId).Name;
                        _entity.PropertyDetailName = BasicSiteProDetailsBLL.Instance.GetBasicSiteProDetails(_entity.PropertyDetailId).Name;
                    }
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<ProductPropertyEntity>)obj;
                }
            }
            else
            {
                list = ProductPropertyDA.Instance.GetPropertyByProductId(productid);
                foreach (ProductPropertyEntity _entity in list)
                {
                    _entity.PropertyName = BasicSitePropertiesBLL.Instance.GetBasicSiteProperties(_entity.PropertyId,false).Name;
                    _entity.PropertyDetailName = BasicSiteProDetailsBLL.Instance.GetBasicSiteProDetails(_entity.PropertyDetailId, false).Name;
                }
            }
         
            return list;
        }
        public async Task GetProductPropertyAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductPropertyListKey";// SysCacheKey.ProductPropertyListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductPropertyEntity> list = null;
                    list = ProductPropertyDA.Instance.GetProductPropertyAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductPropertyEntity productProperty)
        {
            return ProductPropertyDA.Instance.ExistNum(productProperty)>0;
        }
		
	}
}

