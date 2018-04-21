using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Basic.VW.Product;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表ProductDetail的业务逻辑层。
创建时间：2016/12/15 10:16:52
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{

    /// <summary>
    /// 表ProductDetail的业务逻辑层。
    /// </summary>
    public class ProductDetailBLL
    {
        #region 实例化
        public static ProductDetailBLL Instance
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
            internal static readonly ProductDetailBLL instance = new ProductDetailBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表ProductDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productDetail">要添加的ProductDetail数据实体对象</param>
        public int AddProductDetail(ProductDetailEntity productDetail)
        {
            if (productDetail.Id > 0)
            {
                return UpdateProductDetail(productDetail);
            } 
            else if (ProductDetailBLL.Instance.IsExist(productDetail))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductDetailDA.Instance.AddProductDetail(productDetail);
            }
        }

        /// <summary>
        /// 更新一条ProductDetail记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productDetail">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateProductDetail(ProductDetailEntity productDetail)
        {
            return ProductDetailDA.Instance.UpdateProductDetail(productDetail);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductDetailByKey(int id)
        {
            return ProductDetailDA.Instance.DeleteProductDetailByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductDetailDisabled()
        {
            return ProductDetailDA.Instance.DeleteProductDetailDisabled();
        }

        public int UpdatePDetailStatus(int id, int status)
        {
            return ProductDetailDA.Instance.UpdatePDetailStatus(id, status);
        }

        /// <summary>
        /// 获取普通产品
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        public IList<VWConProductEntity> GetConProductList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> whereList)
        {
            int _productType = 0;
            string _productName = string.Empty;
            if (whereList != null && whereList.Count > 0)
            {
                foreach (var item in whereList)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.ProductType:
                            {
                                _productType = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.ProductName:
                            {
                                _productName = StringUtils.GetDbString(item.CompareValue);
                                break;
                            }
                    }
                }
            }

            return ProductDetailDA.Instance.GetConProductList(pageSize, pageIndex, ref recordCount, _productType, _productName);

        }

        /// <summary>
        /// 获取特价产品
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="whereList"></param>
        /// <returns></returns>
        public IList<VWSpeProductEntity> GetSpeProductList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> whereList)
        {
            int _productType = 0;
            string _productName = string.Empty;
            if (whereList != null && whereList.Count > 0)
            {
                foreach (var item in whereList)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.ProductType:
                            {
                                _productType = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                        case SearchFieldName.ProductName:
                            {
                                _productName = StringUtils.GetDbString(item.CompareValue);
                                break;
                            }
                    }
                }
            }

            return ProductDetailDA.Instance.GetSpeProductList(pageSize, pageIndex, ref recordCount, _productType, _productName);

        }

        

        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductDetailByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductDetailDA.Instance.DeleteProductDetailByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductDetailByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductDetailDA.Instance.DisableProductDetailByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个ProductDetail实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ProductDetail实体</returns>
        /// <param name="columns">要返回的列</param>
        public VWProductDetailEntity GetProductDetail(int id)
        {
            return ProductDetailDA.Instance.GetProductDetail(id);
        }
        public VWProductDetailEntity GetProductDetailByPId(int productid)
        {
            return ProductDetailDA.Instance.GetProductDetailByPId(productid);
        }
        public int  ReleaseStock(string productdetails)
        {
            return ProductDetailDA.Instance.ReleaseStock(productdetails); 
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductDetailEntity> GetProductDetailList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            return ProductDetailDA.Instance.GetProductDetailList(pageSize, pageIndex, ref recordCount);
        }

        public async Task GetProductDetailAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "ProductDetailListKey";// SysCacheKey.ProductDetailListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductDetailEntity> list = null;
                    list = ProductDetailDA.Instance.GetProductDetailAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductDetailEntity productDetail)
        {
            return ProductDetailDA.Instance.ExistNum(productDetail) > 0;
        }

    }
}

