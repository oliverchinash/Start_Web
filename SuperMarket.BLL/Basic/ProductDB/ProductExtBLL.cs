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
功能描述：表ProductExt的业务逻辑层。
创建时间：2017/3/15 0:42:28
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductExt的业务逻辑层。
	/// </summary>
	public class ProductExtBLL
	{
	    #region 实例化
        public static ProductExtBLL Instance
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
            internal static readonly ProductExtBLL instance = new ProductExtBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductExt，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productExt">要添加的ProductExt数据实体对象</param>
		public   int AddProductExt(ProductExtEntity productExt)
		{
		    if (productExt.Id > 0)
            {
                return UpdateProductExt(productExt);
            } 
            else
            {
                return ProductExtDA.Instance.AddProductExt(productExt);
            }
	 	}
        /// <summary>
        /// 根据主键获取一个ProductExt实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ProductExt实体</returns>
        /// <param name="columns">要返回的列</param>
        public ProductExtEntity GetProductExtByProductId(int pid)
        {
            return ProductExtDA.Instance.GetProductExtByProductId(pid);
        }
        public VWProductNomalParamEntity GetProductNormalParamById(int pid)
        {
            VWProductNomalParamEntity entity = new VWProductNomalParamEntity();
            string _cachekey = "GetProductNormalParamById"+ pid;// SysCacheKey.ProductExtListKey;
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                entity= ProductExtDA.Instance.GetParamById(pid);
                if(entity!=null&& entity.ProductId>0)
                {
                    if(entity.ClassId>0)
                    {
                        entity.ClassName = ClassesFoundBLL.Instance.GetClassesFound(entity.ClassId).Name;
                    }
                    if(entity.BrandId > 0)
                    {
                        BrandEntity brand= BrandBLL.Instance.GetBrand(entity.BrandId) ;
                        if(brand!=null&& brand.Id>0 )
                        {
                            entity.BrandName = brand.Name;
                            if (string.IsNullOrEmpty(entity.Factory))
                            { 
                                entity.Factory = brand.Manufacturer;
                            }
                        }
                    }
                    if(entity.UnitId>0)
                    {
                        DicUnitEnumEntity unit = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.UnitId);
                        if(unit!=null&& unit.Id>0)
                        {
                            entity.UnitName = unit.Name;
                        }
                        else
                        { 
                            entity.UnitName = "件";
                        }
                    }
                    MemCache.AddCache(_cachekey, entity);
                }
            }
            else
            {
                entity = (VWProductNomalParamEntity)obj;
            }
            return entity;
        }
        /// <summary>
        /// 更新一条ProductExt记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productExt">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateProductExt(ProductExtEntity productExt)
		{
			return ProductExtDA.Instance.UpdateProductExt(productExt);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductExtByKey(int id)
        {
            return ProductExtDA.Instance.DeleteProductExtByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductExtDisabled()
        {
            return ProductExtDA.Instance.DeleteProductExtDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductExtByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductExtDA.Instance.DeleteProductExtByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductExtByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductExtDA.Instance.DisableProductExtByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductExt实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductExt实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductExtEntity GetProductExt(int id)
		{
			return ProductExtDA.Instance.GetProductExt(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductExtEntity> GetProductExtList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductExtDA.Instance.GetProductExtList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetProductExtAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductExtListKey";// SysCacheKey.ProductExtListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<ProductExtEntity> list = null;
                    list = ProductExtDA.Instance.GetProductExtAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductExtEntity productExt)
        {
            return ProductExtDA.Instance.ExistNum(productExt)>0;
        }
		
	}
}

