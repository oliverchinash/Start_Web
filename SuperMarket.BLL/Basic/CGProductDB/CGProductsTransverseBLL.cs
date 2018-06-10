using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductsTransverse的业务逻辑层。
创建时间：2016/12/28 15:59:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductsTransverse的业务逻辑层。
	/// </summary>
	public class CGProductsTransverseBLL
	{
	    #region 实例化
        public static CGProductsTransverseBLL Instance
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
            internal static readonly CGProductsTransverseBLL instance = new CGProductsTransverseBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductsTransverse，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productsTransverse">要添加的CGProductsTransverse数据实体对象</param>
		public   int AddCGProductsTransverse(CGProductsTransverseEntity productsTransverse)
		{
			  if (productsTransverse.Id > 0)
            {
                return UpdateCGProductsTransverse(productsTransverse);
            }
          
            else if (CGProductsTransverseBLL.Instance.IsExist(productsTransverse))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductsTransverseDA.Instance.AddCGProductsTransverse(productsTransverse);
            }
	 	}

		/// <summary>
		/// 更新一条CGProductsTransverse记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productsTransverse">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGProductsTransverse(CGProductsTransverseEntity productsTransverse)
		{
			return CGProductsTransverseDA.Instance.UpdateCGProductsTransverse(productsTransverse);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductsTransverseByKey(int id)
        {
            return CGProductsTransverseDA.Instance.DeleteCGProductsTransverseByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductsTransverseDisabled()
        {
            return CGProductsTransverseDA.Instance.DeleteCGProductsTransverseDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductsTransverseByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductsTransverseDA.Instance.DeleteCGProductsTransverseByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductsTransverseByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductsTransverseDA.Instance.DisableCGProductsTransverseByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProductsTransverse实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductsTransverse实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductsTransverseEntity GetCGProductsTransverse(int id)
		{
			return CGProductsTransverseDA.Instance.GetCGProductsTransverse(id);			
		}
        public int ProcAddProductsT(int memid, int classid1, int classid2, int classid3, int classid4, string classname1, string classname2, string classname3, string classname4,int brandid,string brandname)
        {
            return CGProductsTransverseDA.Instance.ProcAddProductsT(memid, classid1, classid2, classid3, classid4, classname1, classname2, classname3, classname4, brandid, brandname);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary> 
        public IList<CGProductsTransverseEntity> GetCGProductsTransverseList( int pageIndex, int pageSize, ref int recordCount, int memid, int haschecked, int iseffect)
        {
            return CGProductsTransverseDA.Instance.GetCGProductsTransverseList(pageIndex, pageSize, ref recordCount,  memid, haschecked, iseffect);
        }
        public async Task GetCGProductsTransverseAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductsTransverseListKey";// SysCacheKey.CGProductsTransverseListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGProductsTransverseEntity> list = null;
                    list = CGProductsTransverseDA.Instance.GetCGProductsTransverseAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductsTransverseEntity productsTransverse)
        {
            return CGProductsTransverseDA.Instance.ExistNum(productsTransverse)>0;
        }
		
	}
}

