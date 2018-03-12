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

/*****************************************
功能描述：表ProductStyle的业务逻辑层。
创建时间：2016/8/23 16:50:10
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductStyle的业务逻辑层。
	/// </summary>
	public class ProductStyleBLL
	{
	    #region 实例化
        public static ProductStyleBLL Instance
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
            internal static readonly ProductStyleBLL instance = new ProductStyleBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductStyle，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productStyle">要添加的ProductStyle数据实体对象</param>
		public int AddProductStyle( ProductStyleEntity productStyle)
		{
            if (productStyle.Id > 0)
            {
                return UpdateProductStyle(productStyle);
            }
			 else if (productStyle.BrandId==0|| productStyle.ClassId==0)
            {
                return (int)CommonStatus.ADD_Fail_EmptyClass;
            }	  
            else
            {
                int styleid= ProductStyleDA.Instance.AddProductStyle(productStyle);
               
                return styleid;


            }
	 	}


        public int AddProductProc(ProcProductStyleEntity _entity)
        {
            return ProductStyleDA.Instance.AddProductProc(_entity);
        }
        public int AddProductStyleProc(VWProductStyleEntity _entity)
        {
            return ProductStyleDA.Instance.AddProductStyleProc(_entity);
        }
        /// <summary>
        /// 更新一条ProductStyle记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productStyle">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateProductStyle(ProductStyleEntity productStyle)
		{
			return ProductStyleDA.Instance.UpdateProductStyle(productStyle);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteProductStyleByKey(int id)
        {
            return ProductStyleDA.Instance.DeleteProductStyleByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductStyleDisabled()
        {
            return ProductStyleDA.Instance.DeleteProductStyleDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductStyleByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductStyleDA.Instance.DeleteProductStyleByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductStyleByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductStyleDA.Instance.DisableProductStyleByIds(idstr);
        }

        /// <summary>
        /// 根据主键获取一个ProductStyle实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ProductStyle实体</returns>
        /// <param name="columns">要返回的列</param>
        public VWProductStyleEntity GetProductStyle(int id)
        {
            return ProductStyleDA.Instance.GetProductStyle(id);
        }

        public VWProductStyleEntity GetProduct(int id)
        {
            return ProductStyleDA.Instance.GetProduct(id);
        }
        public ProductStyleEntity GetProductStyleByName(int classid, int brandid, string adtitle)
        {
            ProductStyleEntity list = new ProductStyleEntity();
               list = ProductStyleDA.Instance.GetProductStyleByName(classid, brandid, adtitle); 
             
            return list;

        }
        public ProductStyleEntity GetProductStyleByName( string adtitle)
        {
            ProductStyleEntity list = new ProductStyleEntity();
            list = ProductStyleDA.Instance.GetProductStyleByName( adtitle);

            return list;

        }
        /// <summary>
        /// 获取结算页产品明细
        /// </summary>
        /// <param name="products">产品StyleId和ProductId,数量 如1_3_2|3_4_2</param>
        /// <returns></returns>
        public IList<OrderDetailPreTempEntity> GetOrderProducts(string productdetails)
        {
            return ProductStyleDA.Instance.GetOrderProducts(productdetails);
        }
        public int ProductsToOrder(string productdetails)
        {
            return ProductStyleDA.Instance.ProductsToOrder(productdetails);
        }
        public bool ProductsEnough(string productdetails)
        {
            return ProductStyleDA.Instance.ProductsEnough(productdetails);
        }
       
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductStyleEntity> GetProductStyleList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int classid = 0;
            string properties = "";
            string stylename = "";
            int brandid = 0;
            int orderbytype = 0;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    if (entity.CompareValue!=null)
                    {
                        switch (entity.FieldName)
                        {
                            case SearchFieldName.ClassId:
                                { 
                                    classid = StringUtils.GetDbInt(entity.CompareValue);
                                } 
                                break; 
                            case SearchFieldName.BrandId:
                                {
                                    brandid = StringUtils.GetDbInt(entity.CompareValue);
                                }
                                break;
                            case SearchFieldName.OrderByType:
                                {
                                    orderbytype = StringUtils.GetDbInt(entity.CompareValue);
                                }
                                break;
                            case SearchFieldName.StyleName:
                                {
                                    stylename = StringUtils.GetDbString(entity.CompareValue);
                                }
                                break;
                        }
                    }
                }
            }
            IList<ProductStyleEntity> _list= ProductStyleDA.Instance.GetProductStyleList(pageSize, pageIndex, ref recordCount,classid,brandid, orderbytype,stylename);
            if (_list!=null&&_list.Count>0)
            {
                foreach (var item in _list)
                {
                    item.BrandName = BrandBLL.Instance.GetBrand(item.BrandId,true).Name;
                    item.ClassName = ClassesFoundBLL.Instance.GetClassesFound(item.ClassId, true).Name;
                }
            }

            return _list;
            
        }

        public IList<ProductStyleEntity> GetProductStyleByClassFoundId(int ClassId)
        {
            return ProductStyleDA.Instance.GetProductStyleByClassFoundId(ClassId);
        }


        public async Task GetProductStyleAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductStyleListKey";// SysCacheKey.ProductStyleListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductStyleEntity> list = null;
                    list = ProductStyleDA.Instance.GetProductStyleAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductStyleEntity productStyle)
        {
            return ProductStyleDA.Instance.ExistNum(productStyle)>0;
        }
        public bool ExistTitleNum(string adtitle)
        {
            return ProductStyleDA.Instance.ExistTitleNum(adtitle) > 0;
        }
    }
}

