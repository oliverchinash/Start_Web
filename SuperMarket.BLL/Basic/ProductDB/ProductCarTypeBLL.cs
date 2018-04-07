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
功能描述：表ProductCarType的业务逻辑层。
创建时间：2016/10/31 16:28:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表ProductCarType的业务逻辑层。
	/// </summary>
	public class ProductCarTypeBLL
	{
	    #region 实例化
        public static ProductCarTypeBLL Instance
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
            internal static readonly ProductCarTypeBLL instance = new ProductCarTypeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ProductCarType，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productCarType">要添加的ProductCarType数据实体对象</param>
		public   int AddProductCarType(ProductCarTypeEntity productCarType)
		{
			if (productCarType.Id > 0)
            {
                return UpdateProductCarType(productCarType);
            } 
            else
            {
                return ProductCarTypeDA.Instance.AddProductCarType(productCarType);
            }
	 	}

        /// <summary>
        /// 更新一条ProductCarType记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productCarType">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateProductCarType(ProductCarTypeEntity productCarType)
		{
			return ProductCarTypeDA.Instance.UpdateProductCarType(productCarType);
		}

        public int ExecProcProductCarTypeAdd(ProductCarTypeEntity entity)
        {
            return ProductCarTypeDA.Instance.ExecProcProductCarTypeAdd(entity);
        }
         /// <summary>
         /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
         /// </summary>
        public int DeleteProductCarTypeByKey(int id)
        {
            return ProductCarTypeDA.Instance.DeleteProductCarTypeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteProductCarTypeDisabled()
        {
            return ProductCarTypeDA.Instance.DeleteProductCarTypeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteProductCarTypeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductCarTypeDA.Instance.DeleteProductCarTypeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableProductCarTypeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductCarTypeDA.Instance.DisableProductCarTypeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ProductCarType实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ProductCarType实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductCarTypeEntity GetProductCarType(int id)
		{
            ProductCarTypeEntity item = null;
            item = ProductCarTypeDA.Instance.GetProductCarType(id);
            if (item != null)
            {
                item.CarType1Name = CarTypeBLL.Instance.GetCarType(item.CarType1).Name;
                item.CarType2Name = CarTypeBLL.Instance.GetCarType(item.CarType2).Name;
                item.CarType3Name = CarTypeBLL.Instance.GetCarType(item.CarType3).Name;
                item.CarType4Name = CarTypeBLL.Instance.GetCarType(item.CarType4).Name;
            }

            return item;			
		}

        public ProductCarTypeEntity GetProductCarTypeByPid(int pid)
        {
            return ProductCarTypeDA.Instance.GetProductCarTypeByPid(pid);

        }
        public int  ProcEditProductCarType(int productid,string staticids)
        {
            return ProductCarTypeDA.Instance.ProcEditProductCarType(productid,staticids);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductCarTypeEntity> GetProductCarTypeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            int _pid = 0;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit item in wherelist)
                {
                    switch (item.FieldName)
                    {
                        case SearchFieldName.ProductId:
                            {
                                _pid = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }
                    }
                }
            }
          
            IList<ProductCarTypeEntity> _entityList = ProductCarTypeDA.Instance.GetProductCarTypeList(pageSize, pageIndex, ref recordCount, _pid);

            if (_entityList != null && _entityList.Count > 0)
            {
                foreach (var item in _entityList)
                {
                    item.CarType1Name = CarTypeBLL.Instance.GetCarType(item.CarType1).Name;
                    item.CarType2Name = CarTypeBLL.Instance.GetCarType(item.CarType2).Name;
                    item.CarType3Name = CarTypeBLL.Instance.GetCarType(item.CarType3).Name;
                    item.CarType4Name = CarTypeBLL.Instance.GetCarType(item.CarType4).Name;
                }
            }

            return _entityList;
        }
        public IList<ProductCarTypeEntity> GetListByProductId(int productid)
        { 
            IList<ProductCarTypeEntity> list = null;
            list = ProductCarTypeDA.Instance.GetListByProductId(productid);
          
            return list;
        }
        public async Task GetProductCarTypeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ProductCarTypeListKey";// SysCacheKey.ProductCarTypeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductCarTypeEntity> list = null;
                    list = ProductCarTypeDA.Instance.GetProductCarTypeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductCarTypeEntity productCarType)
        {
            return ProductCarTypeDA.Instance.ExistNum(productCarType)>0;
        }
		
	}
}

