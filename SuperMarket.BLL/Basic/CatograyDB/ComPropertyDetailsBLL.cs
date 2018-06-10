using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CatograyDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ComPropertyDetails的业务逻辑层。
创建时间：2016/10/31 13:00:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CatograyDB
{
	  
	/// <summary>
	/// 表ComPropertyDetails的业务逻辑层。
	/// </summary>
	public class ComPropertyDetailsBLL
	{
	    #region 实例化
        public static ComPropertyDetailsBLL Instance
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
            internal static readonly ComPropertyDetailsBLL instance = new ComPropertyDetailsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ComPropertyDetails，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="comPropertyDetails">要添加的ComPropertyDetails数据实体对象</param>
		public   int AddComPropertyDetails(ComPropertyDetailsEntity comPropertyDetails)
		{
			  if (comPropertyDetails.Id > 0)
            {
                return UpdateComPropertyDetails(comPropertyDetails);
            }
				  else if (string.IsNullOrEmpty(comPropertyDetails.Name))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ComPropertyDetailsBLL.Instance.IsExist(comPropertyDetails))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ComPropertyDetailsDA.Instance.AddComPropertyDetails(comPropertyDetails);
            }
	 	}

		/// <summary>
		/// 更新一条ComPropertyDetails记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="comPropertyDetails">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateComPropertyDetails(ComPropertyDetailsEntity comPropertyDetails)
		{
			return ComPropertyDetailsDA.Instance.UpdateComPropertyDetails(comPropertyDetails);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteComPropertyDetailsByKey(int id)
        {
            return ComPropertyDetailsDA.Instance.DeleteComPropertyDetailsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteComPropertyDetailsDisabled()
        {
            return ComPropertyDetailsDA.Instance.DeleteComPropertyDetailsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteComPropertyDetailsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ComPropertyDetailsDA.Instance.DeleteComPropertyDetailsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableComPropertyDetailsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ComPropertyDetailsDA.Instance.DisableComPropertyDetailsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ComPropertyDetails实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ComPropertyDetails实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ComPropertyDetailsEntity GetComPropertyDetails(int id)
		{
			return ComPropertyDetailsDA.Instance.GetComPropertyDetails(id);			
		}
        public IList<ComPropertyDetailsEntity> GetListByPropertyId(int propertyid,int pid)
        {
            return ComPropertyDetailsDA.Instance.GetListByPropertyId(propertyid, pid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ComPropertyDetailsEntity> GetComPropertyDetailsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return ComPropertyDetailsDA.Instance.GetComPropertyDetailsList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetComPropertyDetailsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ComPropertyDetailsListKey";// SysCacheKey.ComPropertyDetailsListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ComPropertyDetailsEntity> list = null;
                    list = ComPropertyDetailsDA.Instance.GetComPropertyDetailsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ComPropertyDetailsEntity comPropertyDetails)
        {
            return ComPropertyDetailsDA.Instance.ExistNum(comPropertyDetails)>0;
        }
		
	}
}

