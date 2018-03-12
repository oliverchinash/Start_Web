using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGProductsLongitude的业务逻辑层。
创建时间：2016/12/28 15:59:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGProductDB
{
	  
	/// <summary>
	/// 表CGProductsLongitude的业务逻辑层。
	/// </summary>
	public class CGProductsLongitudeBLL
	{
	    #region 实例化
        public static CGProductsLongitudeBLL Instance
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
            internal static readonly CGProductsLongitudeBLL instance = new CGProductsLongitudeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGProductsLongitude，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="productsLongitude">要添加的CGProductsLongitude数据实体对象</param>
		public   int AddCGProductsLongitude(CGProductsLongitudeEntity productsLongitude)
		{
			  if (productsLongitude.Id > 0)
            {
                return UpdateCGProductsLongitude(productsLongitude);
            }
          
            else if (CGProductsLongitudeBLL.Instance.IsExist(productsLongitude))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGProductsLongitudeDA.Instance.AddCGProductsLongitude(productsLongitude);
            }
	 	}

        public int ProcAddProductsL(int memid,int cartypeid1, int cartypeid2, int cartypeid3, int cartypeid4, string cartypename1, string cartypename2, string cartypename3, string cartypename4)
        {
            return CGProductsLongitudeDA.Instance.ProcAddProductsL(memid,   cartypeid1,   cartypeid2,   cartypeid3,   cartypeid4,   cartypename1,   cartypename2,   cartypename3,   cartypename4);
        }
        /// <summary>
        /// 更新一条CGProductsLongitude记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="productsLongitude">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateCGProductsLongitude(CGProductsLongitudeEntity productsLongitude)
		{
			return CGProductsLongitudeDA.Instance.UpdateCGProductsLongitude(productsLongitude);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGProductsLongitudeByKey(int id)
        {
            return CGProductsLongitudeDA.Instance.DeleteCGProductsLongitudeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGProductsLongitudeDisabled()
        {
            return CGProductsLongitudeDA.Instance.DeleteCGProductsLongitudeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGProductsLongitudeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGProductsLongitudeDA.Instance.DeleteCGProductsLongitudeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGProductsLongitudeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGProductsLongitudeDA.Instance.DisableCGProductsLongitudeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGProductsLongitude实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGProductsLongitude实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGProductsLongitudeEntity GetCGProductsLongitude(int id)
		{
			return CGProductsLongitudeDA.Instance.GetCGProductsLongitude(id);			
		}
	    ///// <summary>
        ///// 获得数据列表
        ///// </summary> 
        public IList<CGProductsLongitudeEntity> GetCGProductsLongitudeList(int pageSize, int pageIndex, ref int recordCount, int memid,int haschecked,int iseffect)
        {
            return CGProductsLongitudeDA.Instance.GetCGProductsLongitudeList(pageSize, pageIndex, ref recordCount, memid, haschecked, iseffect);
        }
        public async Task GetCGProductsLongitudeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="CGProductsLongitudeListKey";// SysCacheKey.CGProductsLongitudeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGProductsLongitudeEntity> list = null;
                    list = CGProductsLongitudeDA.Instance.GetCGProductsLongitudeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGProductsLongitudeEntity productsLongitude)
        {
            return CGProductsLongitudeDA.Instance.ExistNum(productsLongitude)>0;
        }
		
	}
}

