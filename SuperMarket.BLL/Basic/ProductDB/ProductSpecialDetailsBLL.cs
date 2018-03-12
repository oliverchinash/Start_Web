using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ProductSpecialDetails的业务逻辑层。
创建时间：2017/5/16 11:30:38
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductSpecialDetails的业务逻辑层。
	/// </summary>
	public class ProductSpecialDetailsBLL
	{
	    #region 实例化
        public static ProductSpecialDetailsBLL Instance
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
            internal static readonly ProductSpecialDetailsBLL instance = new ProductSpecialDetailsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductSpecialDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSpecialDetails">要添加的ProductSpecialDetails数据实体对象</param>
		public   int AddProductSpecialDetails(ProductSpecialDetailsEntity productSpecialDetails)
		{
			if (productSpecialDetails.Id > 0)
            {
                return UpdateProductSpecialDetails(productSpecialDetails);
            }  
            else if (ProductSpecialDetailsBLL.Instance.IsExist(productSpecialDetails))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductSpecialDetailsDA.Instance.AddProductSpecialDetails(productSpecialDetails);
            }
	 	}

		/// <summary>
		/// 更新一条ProductSpecialDetails记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productSpecialDetails">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateProductSpecialDetails(ProductSpecialDetailsEntity productSpecialDetails)
		{
			return ProductSpecialDetailsDA.Instance.UpdateProductSpecialDetails(productSpecialDetails);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductSpecialDetailsByKey(int id)
        {
            return ProductSpecialDetailsDA.Instance.DeleteProductSpecialDetailsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductSpecialDetailsDisabled()
        {
            return ProductSpecialDetailsDA.Instance.DeleteProductSpecialDetailsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductSpecialDetailsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductSpecialDetailsDA.Instance.DeleteProductSpecialDetailsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductSpecialDetailsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductSpecialDetailsDA.Instance.DisableProductSpecialDetailsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductSpecialDetails实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductSpecialDetails实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductSpecialDetailsEntity GetProductSpecialDetails(int id)
		{
			return ProductSpecialDetailsDA.Instance.GetProductSpecialDetails(id);			
		}
	    ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWProductSpecialDetailsEntity> GetProductSpecialDetailsList(int pageIndex, int pageSize, ref  int recordCount, int specialid,int isactive)
        {
            IList<VWProductSpecialDetailsEntity> list = null;
            string _cachekey = "GetProductSpecialDetailsList_"+ pageIndex+"_"+ pageSize+"_"+ specialid +"_"+ isactive; 
            string _cachekeynum = "GetProductSpecialDetailsListNum_"+ pageIndex + "_"+ pageSize+"_"+ specialid +"_"+ isactive; 
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
               list = ProductSpecialDetailsDA.Instance.GetProductSpecialDetailsList(pageIndex, pageSize,  ref recordCount, specialid, isactive);
                if (list != null && list.Count > 0)
                {
                    foreach (VWProductSpecialDetailsEntity entity in list)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    }
                }
                MemCache.AddCache(_cachekey, list);
                MemCache.AddCache(_cachekeynum, recordCount);
            }
            else
            {
                list = (IList<VWProductSpecialDetailsEntity>)obj;
                recordCount=StringUtils.GetDbInt(MemCache.GetCache(_cachekeynum)); 
            }
          
            return list;
        }
        /// <summary>
        /// 广告位显示专区数据获取
        /// </summary>
        /// <param name="specialid"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<VWProductSpecialDetailsEntity> GetSpecialDetailsForMenuAD( int specialid, int num=8)
        {
            IList<VWProductSpecialDetailsEntity> list = null;
            string _cachekey = "GetSpecialDetailsForMenuAD_" + specialid + "_" + num; 
            object obj = MemCache.GetCache(_cachekey); ;
            if (obj == null)
            {
                list = ProductSpecialDetailsDA.Instance.GetSpecialDetailsForMenuAD(specialid, num);
                if (list != null && list.Count > 0)
                {
                    foreach (VWProductSpecialDetailsEntity entity in list)
                    {
                        entity.ProductDetail = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    }
                }
                MemCache.AddCache(_cachekey, list); 
            }
            else
            {
                list = (IList<VWProductSpecialDetailsEntity>)obj; 
            }

            return list;
        }

        public async Task GetProductSpecialDetailsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductSpecialDetailsListKey";// SysCacheKey.ProductSpecialDetailsListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<ProductSpecialDetailsEntity> list = null;
                    list = ProductSpecialDetailsDA.Instance.GetProductSpecialDetailsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductSpecialDetailsEntity productSpecialDetails)
        {
            return ProductSpecialDetailsDA.Instance.ExistNum(productSpecialDetails)>0;
        }
		
	}
}

