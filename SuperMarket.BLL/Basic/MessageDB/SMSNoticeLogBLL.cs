using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB; 
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SMSNoticeLog的业务逻辑层。
创建时间：2017/1/18 14:05:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表SMSNoticeLog的业务逻辑层。
	/// </summary>
	public class SMSNoticeLogBLL
	{
	    #region 实例化
        public static SMSNoticeLogBLL Instance
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
            internal static readonly SMSNoticeLogBLL instance = new SMSNoticeLogBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SMSNoticeLog，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sMSNoticeLog">要添加的SMSNoticeLog数据实体对象</param>
		public   int AddSMSNoticeLog(SMSNoticeLogEntity sMSNoticeLog)
		{
			if (sMSNoticeLog.Id > 0)
            {
                return UpdateSMSNoticeLog(sMSNoticeLog);
            }
            else
            {
                return SMSNoticeLogDA.Instance.AddSMSNoticeLog(sMSNoticeLog);
            }
	 	}

		/// <summary>
		/// 更新一条SMSNoticeLog记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sMSNoticeLog">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSMSNoticeLog(SMSNoticeLogEntity sMSNoticeLog)
		{
			return SMSNoticeLogDA.Instance.UpdateSMSNoticeLog(sMSNoticeLog);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSMSNoticeLogByKey(int id)
        {
            return SMSNoticeLogDA.Instance.DeleteSMSNoticeLogByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSMSNoticeLogDisabled()
        {
            return SMSNoticeLogDA.Instance.DeleteSMSNoticeLogDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSMSNoticeLogByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SMSNoticeLogDA.Instance.DeleteSMSNoticeLogByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSMSNoticeLogByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SMSNoticeLogDA.Instance.DisableSMSNoticeLogByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SMSNoticeLog实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SMSNoticeLog实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SMSNoticeLogEntity GetSMSNoticeLog(int id)
		{
			return SMSNoticeLogDA.Instance.GetSMSNoticeLog(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SMSNoticeLogEntity> GetSMSNoticeLogList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return SMSNoticeLogDA.Instance.GetSMSNoticeLogList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSMSNoticeLogAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SMSNoticeLogListKey";// SysCacheKey.SMSNoticeLogListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SMSNoticeLogEntity> list = null;
                    list = SMSNoticeLogDA.Instance.GetSMSNoticeLogAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SMSNoticeLogEntity sMSNoticeLog)
        {
            return SMSNoticeLogDA.Instance.ExistNum(sMSNoticeLog)>0;
        }
		
	}
}

