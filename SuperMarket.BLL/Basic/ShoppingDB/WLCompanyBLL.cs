using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WLCompany的业务逻辑层。
创建时间：2016/9/19 11:39:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表WLCompany的业务逻辑层。
	/// </summary>
	public class WLCompanyBLL
	{
	    #region 实例化
        public static WLCompanyBLL Instance
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
            internal static readonly WLCompanyBLL instance = new WLCompanyBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WLCompany，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wLCompany">要添加的WLCompany数据实体对象</param>
		public   int AddWLCompany(WLCompanyEntity wLCompany)
		{
			  if (wLCompany.Id > 0)
            {
                return UpdateWLCompany(wLCompany);
            }
				  else if (string.IsNullOrEmpty(wLCompany.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (WLCompanyBLL.Instance.IsExist(wLCompany))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return WLCompanyDA.Instance.AddWLCompany(wLCompany);
            }
	 	}

		/// <summary>
		/// 更新一条WLCompany记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="wLCompany">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWLCompany(WLCompanyEntity wLCompany)
		{
			return WLCompanyDA.Instance.UpdateWLCompany(wLCompany);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWLCompanyByKey(int id)
        {
            return WLCompanyDA.Instance.DeleteWLCompanyByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWLCompanyDisabled()
        {
            return WLCompanyDA.Instance.DeleteWLCompanyDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWLCompanyByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WLCompanyDA.Instance.DeleteWLCompanyByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWLCompanyByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WLCompanyDA.Instance.DisableWLCompanyByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WLCompany实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WLCompany实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WLCompanyEntity GetWLCompany(int id)
		{
			return WLCompanyDA.Instance.GetWLCompany(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WLCompanyEntity> GetWLCompanyList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WLCompanyDA.Instance.GetWLCompanyList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWLCompanyAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WLCompanyListKey";// SysCacheKey.WLCompanyListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WLCompanyEntity> list = null;
                    list = WLCompanyDA.Instance.GetWLCompanyAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WLCompanyEntity wLCompany)
        {
            return WLCompanyDA.Instance.ExistNum(wLCompany)>0;
        }
		
	}
}

