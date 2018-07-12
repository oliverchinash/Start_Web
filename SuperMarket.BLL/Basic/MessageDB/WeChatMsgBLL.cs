using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WeChatMsg的业务逻辑层。
创建时间：2017/8/26 16:57:28
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表WeChatMsg的业务逻辑层。
	/// </summary>
	public class WeChatMsgBLL
	{
	    #region 实例化
        public static WeChatMsgBLL Instance
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
            internal static readonly WeChatMsgBLL instance = new WeChatMsgBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表WeChatMsg，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatMsg">要添加的WeChatMsg数据实体对象</param>
		public   int AddWeChatMsg(WeChatMsgEntity weChatMsg)
		{
			 
                return WeChatMsgDA.Instance.AddWeChatMsg(weChatMsg);
            
	 	}

		/// <summary>
		/// 更新一条WeChatMsg记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="weChatMsg">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateWeChatMsg(WeChatMsgEntity weChatMsg)
		{
			return WeChatMsgDA.Instance.UpdateWeChatMsg(weChatMsg);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWeChatMsgByKey(int id)
        {
            return WeChatMsgDA.Instance.DeleteWeChatMsgByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWeChatMsgDisabled()
        {
            return WeChatMsgDA.Instance.DeleteWeChatMsgDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWeChatMsgByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return WeChatMsgDA.Instance.DeleteWeChatMsgByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWeChatMsgByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WeChatMsgDA.Instance.DisableWeChatMsgByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个WeChatMsg实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>WeChatMsg实体</returns>
		/// <param name="columns">要返回的列</param>
		public   WeChatMsgEntity GetWeChatMsg(int id)
		{
			return WeChatMsgDA.Instance.GetWeChatMsg(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WeChatMsgEntity> GetWeChatMsgList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return WeChatMsgDA.Instance.GetWeChatMsgList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetWeChatMsgAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="WeChatMsgListKey";// SysCacheKey.WeChatMsgListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WeChatMsgEntity> list = null;
                    list = WeChatMsgDA.Instance.GetWeChatMsgAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WeChatMsgEntity weChatMsg)
        {
            return WeChatMsgDA.Instance.ExistNum(weChatMsg)>0;
        }
		
	}
}

