using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.ProductDB;

/*****************************************
功能描述：表MemFavorites的业务逻辑层。
创建时间：2017/5/5 9:15:04
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表MemFavorites的业务逻辑层。
	/// </summary>
	public class MemFavoritesBLL
	{
	    #region 实例化
        public static MemFavoritesBLL Instance
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
            internal static readonly MemFavoritesBLL instance = new MemFavoritesBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表MemFavorites，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memFavorites">要添加的MemFavorites数据实体对象</param>
		public MemFavoritesEntity AddMemFavorites(MemFavoritesEntity memFavorites)
		{  
          return MemFavoritesDA.Instance.AddMemFavorites(memFavorites);    
	 	}

		/// <summary>
		/// 更新一条MemFavorites记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="memFavorites">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateMemFavorites(MemFavoritesEntity memFavorites)
		{
			return MemFavoritesDA.Instance.UpdateMemFavorites(memFavorites);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteMemFavoritesByKey(int id)
        {
            return MemFavoritesDA.Instance.DeleteMemFavoritesByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteMemFavoritesDisabled()
        {
            return MemFavoritesDA.Instance.DeleteMemFavoritesDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteMemFavoritesByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemFavoritesDA.Instance.DeleteMemFavoritesByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableMemFavoritesByIds(string ids,int memid,int systemid)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemFavoritesDA.Instance.DisableMemFavoritesByIds(idstr, memid, systemid);
        }
        public int DisableMemFavoritesAll( int memid )
        { 
            return MemFavoritesDA.Instance.DisableMemFavoritesAll(memid);
        }
        public int GetMemFavoritesNum(int memid)
        {
            return MemFavoritesDA.Instance.GetMemFavoritesNum(memid);
        }
        /// <summary>
        /// 根据主键获取一个MemFavorites实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>MemFavorites实体</returns>
        /// <param name="columns">要返回的列</param>
        public   MemFavoritesEntity GetMemFavorites(int id)
		{
			return MemFavoritesDA.Instance.GetMemFavorites(id);			
		}
	    ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemFavoritesEntity> GetMemFavoritesList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemFavoritesDA.Instance.GetMemFavoritesList(pageSize, pageIndex, ref recordCount);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWMemFavoritesEntity> GetVWMemFavoritesList(int pageIndex, int pageSize,  ref int recordCount, int memid,int isactive)
        {
            IList<VWMemFavoritesEntity> list= MemFavoritesDA.Instance.GetVWMemFavoritesList(pageIndex, pageSize, ref recordCount, memid, isactive);
            if(list!=null&& list.Count>0)
            {
                foreach(VWMemFavoritesEntity entity in list)
                { 
                    VWProductEntity _vwentity = new VWProductEntity(); 
                    _vwentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProductDetailId);
                    entity.ProductEntity = _vwentity;
                }
            }
            return list;
        }
        public async Task GetMemFavoritesAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="MemFavoritesListKey";// SysCacheKey.MemFavoritesListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemFavoritesEntity> list = null;
                    list = MemFavoritesDA.Instance.GetMemFavoritesAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(int productdetailid,int memid)
        {
            return MemFavoritesDA.Instance.ExistNum(productdetailid,   memid) >0;
        }
        public int  ExistNum(int productdetailid, int memid)
        {
            return MemFavoritesDA.Instance.ExistNum(productdetailid, memid)  ;
        }
    }
}

