using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WeiXinQNews的业务逻辑层。
创建时间：2017/7/15 14:16:41
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表WeiXinQNews的业务逻辑层。
	/// </summary>
	public class WeiXinQNewsBLL
	{
	    #region 实例化
        public static WeiXinQNewsBLL Instance
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
            internal static readonly WeiXinQNewsBLL instance = new WeiXinQNewsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WeiXinQNews，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weiXinQNews">要添加的WeiXinQNews数据实体对象</param>
		public   int AddWeiXinQNews(WeiXinQNewsEntity weiXinQNews)
		{
            if(weiXinQNews.Id>0)
            {
              return  UpdateWeiXinQNews(weiXinQNews);
            }
	        return WeiXinQNewsDA.Instance.AddWeiXinQNews(weiXinQNews);
           
	 	}

		/// <summary>
		/// 更新一条WeiXinQNews记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weiXinQNews">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWeiXinQNews(WeiXinQNewsEntity weiXinQNews)
		{
			return WeiXinQNewsDA.Instance.UpdateWeiXinQNews(weiXinQNews);
		}
        public int WeiXinQNewsPublish(int nid)
        {
            return WeiXinQNewsDA.Instance.WeiXinQNewsPublish(nid);
        }
        
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWeiXinQNewsByKey(int id)
        {
            return WeiXinQNewsDA.Instance.DeleteWeiXinQNewsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeiXinQNewsDisabled()
        {
            return WeiXinQNewsDA.Instance.DeleteWeiXinQNewsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeiXinQNewsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WeiXinQNewsDA.Instance.DeleteWeiXinQNewsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeiXinQNewsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WeiXinQNewsDA.Instance.DisableWeiXinQNewsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WeiXinQNews实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WeiXinQNews实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WeiXinQNewsEntity GetWeiXinQNews(int id)
		{
			return WeiXinQNewsDA.Instance.GetWeiXinQNews(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WeiXinQNewsEntity> GetWeiXinQNewsList(int pageSize, int pageIndex, ref  int recordCount,int newstype,string key)
        {
            return WeiXinQNewsDA.Instance.GetWeiXinQNewsList(pageSize, pageIndex, ref recordCount,newstype,   key);
        }
		
		public async Task GetWeiXinQNewsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WeiXinQNewsListKey";// SysCacheKey.WeiXinQNewsListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<WeiXinQNewsEntity> list = null;
                    list = WeiXinQNewsDA.Instance.GetWeiXinQNewsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WeiXinQNewsEntity weiXinQNews)
        {
            return WeiXinQNewsDA.Instance.ExistNum(weiXinQNews)>0;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExistTitle(string title)
        {
            return WeiXinQNewsDA.Instance.IsExistTitle(title) ;
        }
    }
}

