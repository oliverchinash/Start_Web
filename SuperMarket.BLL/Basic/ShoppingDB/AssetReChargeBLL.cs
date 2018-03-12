using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表AssetReCharge的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表AssetReCharge的业务逻辑层。
	/// </summary>
	public class AssetReChargeBLL
	{
	    #region 实例化
        public static AssetReChargeBLL Instance
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
            internal static readonly AssetReChargeBLL instance = new AssetReChargeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表AssetReCharge，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetReCharge">要添加的AssetReCharge数据实体对象</param>
		public   int AddAssetReCharge(AssetReChargeEntity assetReCharge)
		{
			  if (assetReCharge.Id > 0)
            {
                return UpdateAssetReCharge(assetReCharge);
            }
          
            else if (AssetReChargeBLL.Instance.IsExist(assetReCharge))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AssetReChargeDA.Instance.AddAssetReCharge(assetReCharge);
            }
	 	}

		/// <summary>
		/// 更新一条AssetReCharge记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetReCharge">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAssetReCharge(AssetReChargeEntity assetReCharge)
		{
			return AssetReChargeDA.Instance.UpdateAssetReCharge(assetReCharge);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAssetReChargeByKey(int id)
        {
            return AssetReChargeDA.Instance.DeleteAssetReChargeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetReChargeDisabled()
        {
            return AssetReChargeDA.Instance.DeleteAssetReChargeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetReChargeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AssetReChargeDA.Instance.DeleteAssetReChargeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetReChargeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AssetReChargeDA.Instance.DisableAssetReChargeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个AssetReCharge实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>AssetReCharge实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AssetReChargeEntity GetAssetReCharge(int id)
		{
			return AssetReChargeDA.Instance.GetAssetReCharge(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AssetReChargeEntity> GetAssetReChargeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return AssetReChargeDA.Instance.GetAssetReChargeList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetAssetReChargeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AssetReChargeListKey";// SysCacheKey.AssetReChargeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<AssetReChargeEntity> list = null;
                    list = AssetReChargeDA.Instance.GetAssetReChargeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AssetReChargeEntity assetReCharge)
        {
            return AssetReChargeDA.Instance.ExistNum(assetReCharge)>0;
        }
		
	}
}

