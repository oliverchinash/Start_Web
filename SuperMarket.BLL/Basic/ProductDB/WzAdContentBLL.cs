using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ProductDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WzAdContent的业务逻辑层。
创建时间：2016/8/16 13:54:40
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ProductDB
{
	  
	/// <summary>
	/// 表WzAdContent的业务逻辑层。
	/// </summary>
	public class WzAdContentBLL
	{
	    #region 实例化
        public static WzAdContentBLL Instance
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
            internal static readonly WzAdContentBLL instance = new WzAdContentBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WzAdContent，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wzAdContent">要添加的WzAdContent数据实体对象</param>
		public   int AddWzAdContent(WzAdContentEntity wzAdContent)
		{
			  if (wzAdContent.Id > 0)
            {
                return UpdateWzAdContent(wzAdContent);
            }
          
            else if (WzAdContentBLL.Instance.IsExist(wzAdContent))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return WzAdContentDA.Instance.AddWzAdContent(wzAdContent);
            }
	 	}

		/// <summary>
		/// 更新一条WzAdContent记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wzAdContent">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWzAdContent(WzAdContentEntity wzAdContent)
		{
			return WzAdContentDA.Instance.UpdateWzAdContent(wzAdContent);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWzAdContentByKey(int id)
        {
            return WzAdContentDA.Instance.DeleteWzAdContentByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWzAdContentDisabled()
        {
            return WzAdContentDA.Instance.DeleteWzAdContentDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWzAdContentByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WzAdContentDA.Instance.DeleteWzAdContentByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWzAdContentByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WzAdContentDA.Instance.DisableWzAdContentByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WzAdContent实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WzAdContent实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WzAdContentEntity GetWzAdContent(int id)
		{
			return WzAdContentDA.Instance.GetWzAdContent(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WzAdContentEntity> GetWzAdContentList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WzAdContentDA.Instance.GetWzAdContentList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWzAdContentAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WzAdContentListKey";// SysCacheKey.WzAdContentListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WzAdContentEntity> list = null;
                    list = WzAdContentDA.Instance.GetWzAdContentAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WzAdContentEntity wzAdContent)
        {
            return WzAdContentDA.Instance.ExistNum(wzAdContent)>0;
        }
		
	}
}

