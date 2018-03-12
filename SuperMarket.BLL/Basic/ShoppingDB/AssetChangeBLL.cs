using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表AssetChange的业务逻辑层。
创建时间：2016/8/7 17:12:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表AssetChange的业务逻辑层。
	/// </summary>
	public class AssetChangeBLL
	{
	    #region 实例化
        public static AssetChangeBLL Instance
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
            internal static readonly AssetChangeBLL instance = new AssetChangeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表AssetChange，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetChange">要添加的AssetChange数据实体对象</param>
		public   int AddAssetChange(AssetChangeEntity assetChange)
		{
			  if (assetChange.Id > 0)
            {
                return UpdateAssetChange(assetChange);
            }
          
            else if (AssetChangeBLL.Instance.IsExist(assetChange))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return AssetChangeDA.Instance.AddAssetChange(assetChange);
            }
	 	}

		/// <summary>
		/// 更新一条AssetChange记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="assetChange">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateAssetChange(AssetChangeEntity assetChange)
		{
			return AssetChangeDA.Instance.UpdateAssetChange(assetChange);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteAssetChangeByKey(int id)
        {
            return AssetChangeDA.Instance.DeleteAssetChangeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteAssetChangeDisabled()
        {
            return AssetChangeDA.Instance.DeleteAssetChangeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteAssetChangeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return AssetChangeDA.Instance.DeleteAssetChangeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableAssetChangeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return AssetChangeDA.Instance.DisableAssetChangeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个AssetChange实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>AssetChange实体</returns>
		/// <param name="columns">要返回的列</param>
		public   AssetChangeEntity GetAssetChange(int id)
		{
			return AssetChangeDA.Instance.GetAssetChange(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<AssetChangeEntity> GetAssetChangeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        { 
            int _memid = 0;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                      
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                    }
                }
            }
            return AssetChangeDA.Instance.GetAssetChangeList(pageSize, pageIndex, ref recordCount, _memid);
        }
		
		public async Task GetAssetChangeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="AssetChangeListKey";// SysCacheKey.AssetChangeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<AssetChangeEntity> list = null;
                    list = AssetChangeDA.Instance.GetAssetChangeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(AssetChangeEntity assetChange)
        {
            return AssetChangeDA.Instance.ExistNum(assetChange)>0;
        }
		
	}
}

