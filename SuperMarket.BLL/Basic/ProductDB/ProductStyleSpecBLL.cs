using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using System.Linq;

/*****************************************
功能描述：表StyleSpec的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表StyleSpec的业务逻辑层。
	/// </summary>
	public class ProductStyleSpecBLL
    {
	    #region 实例化
        public static ProductStyleSpecBLL Instance
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
            internal static readonly ProductStyleSpecBLL instance = new ProductStyleSpecBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表StyleSpec，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="styleSpec">要添加的StyleSpec数据实体对象</param>
		public   int AddStyleSpec(ProductStyleSpecEntity styleSpec)
		{
			  if (styleSpec.Id > 0)
            {
                return UpdateStyleSpec(styleSpec);
            }
          
            else if (ProductStyleSpecBLL.Instance.IsExist(styleSpec))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ProductStyleSpecDA.Instance.AddStyleSpec(styleSpec);
            }
	 	}

		/// <summary>
		/// 更新一条StyleSpec记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="styleSpec">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateStyleSpec(ProductStyleSpecEntity styleSpec)
		{
			return ProductStyleSpecDA.Instance.UpdateStyleSpec(styleSpec);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteStyleSpecByKey(int id)
        {
            return ProductStyleSpecDA.Instance.DeleteStyleSpecByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteStyleSpecDisabled()
        {
            return ProductStyleSpecDA.Instance.DeleteStyleSpecDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteStyleSpecByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ProductStyleSpecDA.Instance.DeleteStyleSpecByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableStyleSpecByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ProductStyleSpecDA.Instance.DisableStyleSpecByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个StyleSpec实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>StyleSpec实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ProductStyleSpecEntity GetStyleSpec(int id)
		{
			return ProductStyleSpecDA.Instance.GetStyleSpec(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ProductStyleSpecEntity> GetStyleSpecList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ProductStyleSpecDA.Instance.GetStyleSpecList(pageSize, pageIndex, ref recordCount);
        }
        /// <summary>
        /// 根据分类id 获取对应的分类产品属性
        /// </summary>
        /// <param name="classid"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        public IList<VWProductStyleSpecEntity> GetListByStyle(int styleid, int specid)
        {
            string _cachekey = "VWProductStyleSpecEntity"+"_"+ styleid + "_"+ specid;// SysCacheKey.VWProductStyleSpecKey;
            object _objcache = MemCache.GetCache(_cachekey);
            IList<VWProductStyleSpecEntity> _objlistall = new List<VWProductStyleSpecEntity>();
            if (_objcache == null)
            {
                _objlistall = ProductStyleSpecDA.Instance.GetListByStyle(styleid, specid);
                MemCache.AddCache(_cachekey, _objlistall);
            }
            else
            {
                _objlistall = (List<VWProductStyleSpecEntity>)_objcache;
            }
            var templist = from c in _objlistall
                           orderby c.Sort descending, c.SpecDetailName
                           select c;
            if (templist == null) return null;
            else
                return templist.ToList<VWProductStyleSpecEntity>();
        }
        public async Task GetStyleSpecAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "VWProductStyleSpecEntity";// SysCacheKey.VWProductStyleSpecEntity;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ProductStyleSpecEntity> list = null;
                    list = ProductStyleSpecDA.Instance.GetStyleSpecAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ProductStyleSpecEntity styleSpec)
        {
            return ProductStyleSpecDA.Instance.ExistNum(styleSpec)>0;
        }
		
	}
}

