using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表Asset的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表Asset的业务逻辑层。
	/// </summary>
	public class AssetBLL
	{
	    #region 实例化
        public static AssetBLL Instance
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
            internal static readonly AssetBLL instance = new AssetBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表Asset，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="asset">要添加的Asset数据实体对象</param>
		public   int AddAsset(AssetEntity asset)
		{
			  if (asset.Id > 0)
            {
                return UpdateAsset(asset);
            }
          
            else if (AssetBLL.Instance.IsExist(asset))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AssetDA.Instance.AddAsset(asset);
            }
	 	}

		/// <summary>
		/// 更新一条Asset记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="asset">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAsset(AssetEntity asset)
		{
			return AssetDA.Instance.UpdateAsset(asset);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAssetByKey(int id)
        {
            return AssetDA.Instance.DeleteAssetByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetDisabled()
        {
            return AssetDA.Instance.DeleteAssetDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AssetDA.Instance.DeleteAssetByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AssetDA.Instance.DisableAssetByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个Asset实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>Asset实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AssetEntity GetAsset(int id)
		{
			return AssetDA.Instance.GetAsset(id);			
		} 
        /// <summary>
        /// 根据主键获取一个Asset实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Asset实体</returns>
        /// <param name="columns">要返回的列</param>
        public AssetEntity GetAssetByMemId(int memid)
        {
            return AssetDA.Instance.GetAssetByMemId(memid);
        }
        public bool CheckIntegralEnough(int memid,int integral)
        {
            return AssetDA.Instance.CheckIntegralEnough(memid, integral);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AssetEntity> GetAssetList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return AssetDA.Instance.GetAssetList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetAssetAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AssetListKey";// SysCacheKey.AssetListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<AssetEntity> list = null;
                    list = AssetDA.Instance.GetAssetAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AssetEntity asset)
        {
            return AssetDA.Instance.ExistNum(asset)>0;
        }
		
	}
}

