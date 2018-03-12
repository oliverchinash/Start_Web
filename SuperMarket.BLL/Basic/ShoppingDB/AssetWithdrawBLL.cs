using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表AssetWithdraw的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表AssetWithdraw的业务逻辑层。
	/// </summary>
	public class AssetWithdrawBLL
	{
	    #region 实例化
        public static AssetWithdrawBLL Instance
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
            internal static readonly AssetWithdrawBLL instance = new AssetWithdrawBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表AssetWithdraw，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetWithdraw">要添加的AssetWithdraw数据实体对象</param>
		public   int AddAssetWithdraw(AssetWithdrawEntity assetWithdraw)
		{
			  if (assetWithdraw.Id > 0)
            {
                return UpdateAssetWithdraw(assetWithdraw);
            }
				  else if (string.IsNullOrEmpty(assetWithdraw.BankName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(assetWithdraw.BankSubName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(assetWithdraw.CardName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (AssetWithdrawBLL.Instance.IsExist(assetWithdraw))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AssetWithdrawDA.Instance.AddAssetWithdraw(assetWithdraw);
            }
	 	}

		/// <summary>
		/// 更新一条AssetWithdraw记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetWithdraw">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAssetWithdraw(AssetWithdrawEntity assetWithdraw)
		{
			return AssetWithdrawDA.Instance.UpdateAssetWithdraw(assetWithdraw);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAssetWithdrawByKey(int id)
        {
            return AssetWithdrawDA.Instance.DeleteAssetWithdrawByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetWithdrawDisabled()
        {
            return AssetWithdrawDA.Instance.DeleteAssetWithdrawDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetWithdrawByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AssetWithdrawDA.Instance.DeleteAssetWithdrawByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetWithdrawByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AssetWithdrawDA.Instance.DisableAssetWithdrawByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个AssetWithdraw实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>AssetWithdraw实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AssetWithdrawEntity GetAssetWithdraw(int id)
		{
			return AssetWithdrawDA.Instance.GetAssetWithdraw(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AssetWithdrawEntity> GetAssetWithdrawList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return AssetWithdrawDA.Instance.GetAssetWithdrawList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetAssetWithdrawAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AssetWithdrawListKey";// SysCacheKey.AssetWithdrawListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<AssetWithdrawEntity> list = null;
                    list = AssetWithdrawDA.Instance.GetAssetWithdrawAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AssetWithdrawEntity assetWithdraw)
        {
            return AssetWithdrawDA.Instance.ExistNum(assetWithdraw)>0;
        }
		
	}
}

