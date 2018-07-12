using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WeChatTokenLog的业务逻辑层。
创建时间：2017/8/26 9:43:29
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表WeChatTokenLog的业务逻辑层。
	/// </summary>
	public class WeChatTokenLogBLL
	{
	    #region 实例化
        public static WeChatTokenLogBLL Instance
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
            internal static readonly WeChatTokenLogBLL instance = new WeChatTokenLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WeChatTokenLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatTokenLog">要添加的WeChatTokenLog数据实体对象</param>
		public   int AddWeChatTokenLog(WeChatTokenLogEntity weChatTokenLog)
		{
		    return WeChatTokenLogDA.Instance.AddWeChatTokenLog(weChatTokenLog);
        }

		/// <summary>
		/// 更新一条WeChatTokenLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatTokenLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWeChatTokenLog(WeChatTokenLogEntity weChatTokenLog)
		{
			return WeChatTokenLogDA.Instance.UpdateWeChatTokenLog(weChatTokenLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWeChatTokenLogByKey(int id)
        {
            return WeChatTokenLogDA.Instance.DeleteWeChatTokenLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatTokenLogDisabled()
        {
            return WeChatTokenLogDA.Instance.DeleteWeChatTokenLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatTokenLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WeChatTokenLogDA.Instance.DeleteWeChatTokenLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatTokenLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WeChatTokenLogDA.Instance.DisableWeChatTokenLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WeChatTokenLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WeChatTokenLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WeChatTokenLogEntity GetWeChatTokenLog(int id)
		{
			return WeChatTokenLogDA.Instance.GetWeChatTokenLog(id);			
		}
        public WeChatTokenLogEntity GetTokenByAppid(string  appid)
        {
            return WeChatTokenLogDA.Instance.GetTokenByAppid(appid);
        }
        public bool RemoveTokenByAppid(string appid)
        {
            return WeChatTokenLogDA.Instance.RemoveTokenByAppid(appid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WeChatTokenLogEntity> GetWeChatTokenLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WeChatTokenLogDA.Instance.GetWeChatTokenLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWeChatTokenLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WeChatTokenLogListKey";// SysCacheKey.WeChatTokenLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WeChatTokenLogEntity> list = null;
                    list = WeChatTokenLogDA.Instance.GetWeChatTokenLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WeChatTokenLogEntity weChatTokenLog)
        {
            return WeChatTokenLogDA.Instance.ExistNum(weChatTokenLog)>0;
        }
		
	}
}

