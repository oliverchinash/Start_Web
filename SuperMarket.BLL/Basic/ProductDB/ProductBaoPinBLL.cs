using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ProductBaoPin的业务逻辑层。
创建时间：2017/4/15 15:37:31
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductBaoPin的业务逻辑层。
	/// </summary>
	public class ProductBaoPinBLL
	{
	    #region 实例化
        public static ProductBaoPinBLL Instance
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
            internal static readonly ProductBaoPinBLL instance = new ProductBaoPinBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductBaoPin，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productBaoPin">要添加的ProductBaoPin数据实体对象</param>
		public   int AddProductBaoPin(ProductBaoPinEntity productBaoPin)
		{
			  if (productBaoPin.Id > 0)
            {
                return UpdateProductBaoPin(productBaoPin);
            }
				  else if (string.IsNullOrEmpty(productBaoPin.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ProductBaoPinBLL.Instance.IsExist(productBaoPin))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductBaoPinDA.Instance.AddProductBaoPin(productBaoPin);
            }
	 	}

		/// <summary>
		/// 更新一条ProductBaoPin记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productBaoPin">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductBaoPin(ProductBaoPinEntity productBaoPin)
		{
			return ProductBaoPinDA.Instance.UpdateProductBaoPin(productBaoPin);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductBaoPinByKey(int id)
        {
            return ProductBaoPinDA.Instance.DeleteProductBaoPinByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductBaoPinDisabled()
        {
            return ProductBaoPinDA.Instance.DeleteProductBaoPinDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductBaoPinByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductBaoPinDA.Instance.DeleteProductBaoPinByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductBaoPinByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductBaoPinDA.Instance.DisableProductBaoPinByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductBaoPin实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductBaoPin实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductBaoPinEntity GetProductBaoPin(int id)
		{
			return ProductBaoPinDA.Instance.GetProductBaoPin(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWProductBaoPinEntity> GetProductBaoPinList(int pageIndex, int pageSize,  ref  int recordCount,int isactive,int showproduct)
        {
            IList<VWProductBaoPinEntity> list = null;
            string _cachekey = "GetProductBaoPinList_"+ pageIndex+"_"+ pageSize+"_"+ isactive+ showproduct; 
            string _cachekeynum = "GetProductBaoPinListNum_"+ pageIndex+"_"+ pageSize+"_"+ isactive+ showproduct; 
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                list = ProductBaoPinDA.Instance.GetProductBaoPinList(pageSize, pageIndex, ref recordCount,  isactive,showproduct);
                foreach (VWProductBaoPinEntity entity in list)
                {
                    if (entity.ProductDetailId > 0)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                        entity.ProductId = entity.ProductDetail.ProductId;
                        if (entity.ProductDetail.BrandId > 0)
                        {
                            entity.Brand = BrandBLL.Instance.GetBrand(entity.ProductDetail.BrandId);
                        }
                    }
                }
                MemCache.AddCache(_cachekey, list);
                MemCache.AddCache(_cachekeynum, recordCount);
            }
            else
            {
                list = (IList<VWProductBaoPinEntity>)obj;
                recordCount=StringUtils.GetDbInt(MemCache.GetCache(_cachekeynum));  
            }
            return list;
        }
		//取显示出来的爆品列表
		public IList<VWProductBaoPinEntity> GetProductBaoPinShowList(bool iscache=false)
        {
            IList<VWProductBaoPinEntity> list = null;
            if (!iscache)
            {
                list = ProductBaoPinDA.Instance.GetProductBaoPinShowList();
                foreach (VWProductBaoPinEntity entity in list)
                {
                    if (entity.ProductDetailId > 0)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                        entity.ProductId = entity.ProductDetail.ProductId;
                        if (entity.ProductDetail.BrandId > 0)
                        {
                            entity.Brand = BrandBLL.Instance.GetBrand(entity.ProductDetail.BrandId);
                        }
                    }
                }
            }
            else
            {

                string _cachekey = "GetProductBaoPinShowList";// SysCacheKey.ProductBaoPinListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    list = ProductBaoPinDA.Instance.GetProductBaoPinShowList();
                    foreach (VWProductBaoPinEntity entity in list)
                    {
                        if (entity.ProductDetailId > 0)
                        {
                            entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                            entity.ProductId = entity.ProductDetail.ProductId;
                            if (entity.ProductDetail.BrandId > 0)
                            {
                                entity.Brand = BrandBLL.Instance.GetBrand(entity.ProductDetail.BrandId);
                            }
                        }
                    }
                    MemCache.AddCache(_cachekey, list);
                }
                else
                {
                    list = (IList<VWProductBaoPinEntity>)obj;
                }
            }
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductBaoPinEntity productBaoPin)
        {
            return ProductBaoPinDA.Instance.ExistNum(productBaoPin)>0;
        }
		
	}
}

