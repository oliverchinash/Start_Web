using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.PayDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WeChatResult的业务逻辑层。
创建时间：2017/11/29 10:53:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.PayDB
{
	  
	/// <summary>
	/// 表WeChatResult的业务逻辑层。
	/// </summary>
	public class WeChatResultBLL
	{
	    #region 实例化
        public static WeChatResultBLL Instance
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
            internal static readonly WeChatResultBLL instance = new WeChatResultBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WeChatResult，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatResult">要添加的WeChatResult数据实体对象</param>
		public   int AddWeChatResult(WeChatResultEntity weChatResult)
		{
		 
                return WeChatResultDA.Instance.AddWeChatResult(weChatResult);
         
	 	}

		/// <summary>
		/// 更新一条WeChatResult记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatResult">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWeChatResult(WeChatResultEntity weChatResult)
		{
			return WeChatResultDA.Instance.UpdateWeChatResult(weChatResult);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWeChatResultByKey(int id)
        {
            return WeChatResultDA.Instance.DeleteWeChatResultByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatResultDisabled()
        {
            return WeChatResultDA.Instance.DeleteWeChatResultDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatResultByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WeChatResultDA.Instance.DeleteWeChatResultByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatResultByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WeChatResultDA.Instance.DisableWeChatResultByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WeChatResult实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WeChatResult实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WeChatResultEntity GetWeChatResult(int id)
		{
			return WeChatResultDA.Instance.GetWeChatResult(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WeChatResultEntity> GetWeChatResultList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WeChatResultDA.Instance.GetWeChatResultList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWeChatResultAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WeChatResultListKey";// SysCacheKey.WeChatResultListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WeChatResultEntity> list = null;
                    list = WeChatResultDA.Instance.GetWeChatResultAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WeChatResultEntity weChatResult)
        {
            return WeChatResultDA.Instance.ExistNum(weChatResult)>0;
        }
		
	}
}

