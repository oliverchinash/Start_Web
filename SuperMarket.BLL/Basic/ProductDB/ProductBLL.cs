using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表Product的业务逻辑层。
创建时间：2016/10/31 16:28:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表Product的业务逻辑层。
	/// </summary>
	public class ProductBLL
	{
	    #region 实例化
        public static ProductBLL Instance
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
            internal static readonly ProductBLL instance = new ProductBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Product，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="product">要添加的Product数据实体对象</param>
		public   int AddProduct(ProductEntity product)
		{
		    if (product.Id > 0)
            {
                return UpdateProduct(product);
            }  
            else
            {
                return ProductDA.Instance.AddProduct(product);
            }
	 	}
        public int AddProductProc(ProcProductEntity _entity)
        {
            return ProductDA.Instance.AddProductProc(_entity);
        }


        /// <summary>
        /// 给VWProduct其他字段赋值
        /// </summary>
        /// <returns></returns>
        public int Assignment(ref VWProductEntity entity,int id)
        {
            return ProductDA.Instance.Assignment(ref entity, id);
        }


        /// <summary>
        /// 上架或者下架
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int ChangeProductStatus(int id,int status)
        {
            return ProductDA.Instance.ChangeProductStatus(id, status);
        }


        /// <summary>
        /// 更新一条Product记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="product">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateProduct(ProductEntity product)
		{
			return ProductDA.Instance.UpdateProduct(product);
		}
        public int UpdateProductName(ProductEntity product)
        {
            return ProductDA.Instance.UpdateProductName(product);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductByKey(int id)
        {
            return ProductDA.Instance.DeleteProductByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductDisabled()
        {
            return ProductDA.Instance.DeleteProductDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductDA.Instance.DeleteProductByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductDA.Instance.DisableProductByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个Product实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Product实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductEntity GetProduct(int id)
		{
			return ProductDA.Instance.GetProduct(id);			
		}
        public VWCGSyncOrder SplitOrder(string proids)
        {
            VWCGSyncOrder syncentity = ProductDA.Instance.SplitOrder(proids);
            if(syncentity != null)
            {
                if(syncentity.TYClassIds!=null&& syncentity.TYClassIds.Count>0&& syncentity.TYClassPros!=null&& syncentity.TYClassPros.Count>0)
                {
                    string classstrs = "";
                    foreach(int classidentity in syncentity.TYClassIds)
                    {
                        classstrs += "|" + classidentity.ToString();
                    }  

                }
            }
            return null;
            //if (syncentity != null &&  (syncentity.NotTYCarTypes != null && syncentity.NotTYCarTypes.Count > 0 && syncentity.NotTYProCarTypes != null && syncentity.NotTYProCarTypes.Count > 0))
            //{
            //    string productnotty = ""; 
            //    foreach (VWProCarType notyentity in syncentity.NotTYProCarTypes)
            //    {
            //        productnotty += "|" + notyentity.ProductId.ToString(); 
            //    } 
            //    if(productnotty!="")
            //    {
            //        productnotty = productnotty.Trim('|');
            //    }
            //}
        }



        public VWProductEntity GetProductVW(int productid,bool iscache=false)
        {
            VWProductEntity _obj = new VWProductEntity();
            if (!iscache)
                {
                _obj = ProductDA.Instance.GetProductVW(productid);
                if (_obj != null && _obj.Unit > 0)
                {
                    _obj.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(_obj.Unit).Name;
                }
                else
                {
                    _obj.UnitName = "件";
                }
            }
            else
            {
  string _cachekey = "GetProductVW_" + productid.ToString() ; 
            object _objcache = MemCache.GetCache(_cachekey);
            if (_objcache == null)
            {
                _obj = ProductDA.Instance.GetProductVW(productid );
                if (_obj != null && _obj.Unit > 0)
                {
                    _obj.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(_obj.Unit).Name;
                }
                else
                {
                    _obj.UnitName = "件";
                }
                MemCache.AddCache(_cachekey, _obj);

            }
            else
            {
                _obj = (VWProductEntity)_objcache;
            }
            }
          
            return _obj;
        }
        public VWProductEntity GetProVWByDetailId(int productdetailid,bool iscache=false)
        {
            VWProductEntity _obj = new VWProductEntity();
            if (!iscache)
            {
                _obj = ProductDA.Instance.GetProVWByDetailId(productdetailid);
                _obj.CGMemNickName = MemberInfoBLL.Instance.GetNickNameByMemId(_obj.CGMemId);
                if (_obj != null && _obj.Unit > 0)
                {
                    _obj.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(_obj.Unit).Name;
                }
                else
                {
                    _obj.UnitName = "件";
                }
            }
            else
            {

           
                string _cachekey = "GetProVWByDetailId_" + productdetailid.ToString() ;
            object _objcache = MemCache.GetCache(_cachekey);
            if (_objcache == null)
            {
                _obj = ProductDA.Instance.GetProVWByDetailId(productdetailid);
                _obj.CGMemNickName = MemberInfoBLL.Instance.GetNickNameByMemId(_obj.CGMemId);
                if (_obj!=null&& _obj.Unit>0)
                {
                    _obj.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(_obj.Unit).Name;
                }
                else
                {
                    _obj.UnitName = "件";
                }
                MemCache.AddCache(_cachekey, _obj);
            }
            else
            {
                _obj = (VWProductEntity)_objcache;
                }
            }
            return _obj;
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductEntity> GetProductList(int pageSize, int pageIndex, ref  int recordCount,string proname,string classidstr,int styleid)
        {
            string productName = string.Empty;
            int styleId = 0;
             
            return ProductDA.Instance.GetProductList(pageSize, pageIndex, ref recordCount, productName, classidstr,styleId);
        }

        public DataTable GetDataTableByClassId(int classid,int producttype,int productstatus)
        {
            DataTable dt=new DataTable();
            DataSet ds= ProductDA.Instance.GetDataTableByClassId(classid, producttype, productstatus); 
            if(ds!=null&&ds.Tables!=null&& ds.Tables.Count>0)
            {
                dt = ds.Tables[0];
            }
            return dt;
        }
        public IList<VWProductEntity> GetListSpecsByStyleId(int styleid,int producttype ,int cgmemid,bool iscache=false)
        {
            IList<VWProductEntity> list = null;
            if (!iscache)
            {
                list = ProductDA.Instance.GetListSpecsByStyleId(styleid, producttype, cgmemid);

            }
            else
            {

            string _cachekey = "GetListProductByStyleId_"+ styleid+"_"+ producttype+ "_" + cgmemid;// SysCacheKey.ProductListKey;
            object obj = MemCache.GetCache(_cachekey);
            if (obj == null)
            {
                 list = ProductDA.Instance.GetListSpecsByStyleId(styleid, producttype, cgmemid);
                MemCache.AddCache(_cachekey, list);
            }
            else
            {
                list=(IList<VWProductEntity>)obj;
                }
            }
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="classtypeid">分类类别</param>
        /// <param name="producttype">产品类型:常规，限量了购等</param>
        /// <param name="productstatus">产品状态</param>
        /// <returns></returns>
        public IList<ClassesFoundEntity> GetClassesListAll(int classtypeid,int producttype=0,int productstatus=1)
        {
            IList<ClassesFoundEntity> list = null;
            list = ProductDA.Instance.GetClassesListAll(classtypeid, producttype, productstatus);
            return list;

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        //public IList<VWProductEntity> GetProductListFormProc(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        //{
        //    string classidstr = "";
        //    int brandid = 0;
        //    string propertistr = "";
        //    int orderbytype = 0;
        //    int producttype = 1;
        //    int cartypeid1 = 0;
        //    int cartypeid2 = 0;
        //    int cartypeid3 = 0;
        //    int cartypeid4 = 0; 
        //    int status = 1;
        //    if (wherelist != null && wherelist.Count > 0)
        //    {
        //        foreach (ConditionUnit entity in wherelist)
        //        {
        //            if (entity.CompareValue!=null)
        //            {
        //                switch (entity.FieldName)
        //                {
        //                    case SearchFieldName.ClassId:
        //                        {
        //                            IList<int> classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(StringUtils.GetDbInt(entity.CompareValue));
        //                           if(classintlist!=null&& classintlist.Count>0)
        //                            {
        //                                classidstr = string.Join("_", classintlist);  
        //                            }
        //                            else
        //                            {
        //                                classidstr = StringUtils.GetDbString(entity.CompareValue) ; 
        //                            } 
        //                        }
        //                        break;
        //                    case SearchFieldName.BrandId:
        //                        {
        //                            brandid = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.OrderByType:
        //                        {
        //                            orderbytype = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.BasicSitePropertiesStr:
        //                        {
        //                            propertistr = StringUtils.GetDbString(entity.CompareValue) ;
        //                        }
        //                        break;
        //                    case SearchFieldName.ProductType:
        //                        {
        //                            producttype = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.CarTypeId1:
        //                        {
        //                            cartypeid1 = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.CarTypeId2:
        //                        {
        //                            cartypeid2 = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.CarTypeId3:
        //                        {
        //                            cartypeid3 = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                    case SearchFieldName.CarTypeId4:
        //                        {
        //                            cartypeid4 = StringUtils.GetDbInt(entity.CompareValue);
        //                        }
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    return ProductDA.Instance.GetProductListFormProc(pageSize, pageIndex, ref recordCount,classidstr,brandid,propertistr,orderbytype, producttype, cartypeid1, cartypeid2,cartypeid3, cartypeid4, null,status);
        //}
        /// <summary>
        /// 电脑版获取产品列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="classidstr"></param>
        /// <param name="brandid"></param>
        /// <param name="propertstr"></param>
        /// <param name="orderbytype"></param>
        /// <param name="producttype"></param>
        /// <param name="cartypeid1"></param>
        /// <param name="cartypeid2"></param>
        /// <param name="cartypeid3"></param>
        /// <param name="cartypeid4"></param>
        /// <param name="key"></param>
        /// <param name="_classtype"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<VWProductEntity> GetProductListProc(int pageIndex , int pageSize, ref int recordCount, string classidstr, int brandid, string propertstr, int orderbytype, int producttype, string key,int jishi,int status =1)
        {
            IList<VWProductEntity> list= ProductDA.Instance.GetProductListFormProc(pageIndex, pageSize, ref recordCount, classidstr, brandid, propertstr, orderbytype, producttype,   key, jishi, status);
            if (list != null && list.Count > 0)
            {
                foreach (VWProductEntity entity in list)
                { 
                    entity.CGMemNickName = MemberInfoBLL.Instance.GetNickNameByMemId(entity.CGMemId);
                    entity.BrandName = BrandBLL.Instance.GetBrand(entity.BrandId).Name;
                    if(entity.Unit>0)
                    { 
                        entity.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.Unit).Name;
                    }
                    else
                    {
                        entity.UnitName = "件";
                    }
                }
            } 
            return list; 
        }


        /// <summary>
        /// 手机版乘用车获取产品列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="classidstr"></param>
        /// <param name="brandid"></param>
        /// <param name="propertstr"></param>
        /// <param name="orderbytype"></param>
        /// <param name="producttype"></param>
        /// <param name="cartypeid1"></param>
        /// <param name="cartypeid2"></param>
        /// <param name="cartypeid3"></param>
        /// <param name="cartypeid4"></param>
        /// <param name="key"></param>
        /// <param name="_classtype"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<VWProductEntity> GetProductListProcCYC(int pageSize, int pageIndex, ref int recordCount, string classidstr, int brandid, string propertstr, int orderbytype, int producttype, int cartype ,    int jishi, int status = 1)
        {
            IList<VWProductEntity> list = ProductDA.Instance.GetProductListProcCYC(pageSize, pageIndex, ref recordCount, classidstr, brandid, propertstr, orderbytype, producttype, cartype , jishi, status);
            if (list != null && list.Count > 0)
            {
                foreach (VWProductEntity entity in list)
                {
                    if (entity.Unit > 0)
                    {
                        entity.UnitName = DicUnitEnumBLL.Instance.GetDicUnitEnum(entity.Unit).Name;
                    }
                    else
                    {
                        entity.UnitName = "件";
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 获取产品列表，仅仅产品，不包括价格等
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="classidstr"></param>
        /// <param name="brandid"></param>
        /// <param name="propertstr"></param>
        /// <param name="orderbytype"></param>
        /// <param name="producttype"></param>
        /// <param name="cartypeid1"></param>
        /// <param name="cartypeid2"></param>
        /// <param name="cartypeid3"></param>
        /// <param name="cartypeid4"></param>
        /// <param name="key"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public IList<VWProductEntity> GetProMsgListProc(int pageSize, int pageIndex, ref int recordCount, string classidstr, int brandid,   int orderbytype, int cartypeid1, int cartypeid2, int cartypeid3, int cartypeid4, string key, int memid, int cgappstatus,int classtype)
        {
            return ProductDA.Instance.GetProMsgListProc(pageSize, pageIndex, ref recordCount, classidstr, brandid, orderbytype,cartypeid1, cartypeid2, cartypeid3, cartypeid4, key,   memid, cgappstatus, classtype);
        }
        public async Task GetProductAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductListKey";// SysCacheKey.ProductListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductEntity> list = null;
                    list = ProductDA.Instance.GetProductAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductEntity product)
        {
            return ProductDA.Instance.ExistNum(product)>0;
        }
        public int GetProductIdByName(ProductEntity product)
        {
            return ProductDA.Instance.GetProductIdByName(product)  ;
        }
        
    }
}

