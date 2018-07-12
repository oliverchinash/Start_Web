using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表SMSNotice的业务逻辑层。
创建时间：2017/1/18 11:56:55
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表SMSNotice的业务逻辑层。
	/// </summary>
	public class SMSNoticeBLL
	{
	    #region 实例化
        public static SMSNoticeBLL Instance
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
            internal static readonly SMSNoticeBLL instance = new SMSNoticeBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SMSNotice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sMSNotice">要添加的SMSNotice数据实体对象</param>
		public   int AddSMSNotice(SMSNoticeEntity sMSNotice)
		{
			  if (sMSNotice.Id > 0)
            {
                return UpdateSMSNotice(sMSNotice);
            }
          
            //else if (SMSNoticeBLL.Instance.IsExist(sMSNotice))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return SMSNoticeDA.Instance.AddSMSNotice(sMSNotice);
            }
	 	}
        public int AddSMSNoticeByPhone(string phone, string smstype)
        {
            SMSTempletEntity enti = SMSTempletBLL.Instance.GetSMSTempletByCode(smstype);
            string body = enti.SMSTContent;
            return SMSNoticeDA.Instance.AddSMSNoticeByPhone(phone, body);

        }
        /// <summary>
        /// 更新一条SMSNotice记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="sMSNotice">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public   int UpdateSMSNotice(SMSNoticeEntity sMSNotice)
		{
			return SMSNoticeDA.Instance.UpdateSMSNotice(sMSNotice);
		}
        public int SMSNoticeSendAccess(SMSNoticeEntity entity)
        {
            return SMSNoticeDA.Instance.SMSNoticeSendAccess(entity);
        }
        public int SMSNoticeSendError(int id, string msg)
        {
            return SMSNoticeDA.Instance.SMSNoticeSendError(id, msg);
        }
        public int SMSNoticeAdd(string mobile, string msg,int systemtype)
        {
          
            return SMSNoticeDA.Instance.SMSNoticeAdd(mobile, msg,systemtype);

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSMSNoticeByKey(int id)
        {
            return SMSNoticeDA.Instance.DeleteSMSNoticeByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSMSNoticeDisabled()
        {
            return SMSNoticeDA.Instance.DeleteSMSNoticeDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSMSNoticeByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return SMSNoticeDA.Instance.DeleteSMSNoticeByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSMSNoticeByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return SMSNoticeDA.Instance.DisableSMSNoticeByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SMSNotice实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SMSNotice实体</returns>
		/// <param name="columns">要返回的列</param>
		public   SMSNoticeEntity GetSMSNotice(int id)
		{
			return SMSNoticeDA.Instance.GetSMSNotice(id);			
		} 
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<SMSNoticeEntity> GetSMSNoticeList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            int _systemType = 0;
            int _status = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (var item in wherelist)
                {
                    switch (item.FieldName)
                    {

                        case SearchFieldName.SystemType:
                            {
                                _systemType = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }

                        case SearchFieldName.SmsNoticeStatus:
                            {
                                _status = StringUtils.GetDbInt(item.CompareValue);
                                break;
                            }

                    }
                }
            }
            
            return SMSNoticeDA.Instance.GetSMSNoticeList(pageSize, pageIndex, ref recordCount,_systemType,_status);
        }
        public IList<SMSNoticeEntity> GetSMSList(int  num,int status)
        {
            return SMSNoticeDA.Instance.GetSMSList(num, status);
        }
        public IList<SMSNoticeEntity> GetSMSWaitSend(int Num_i)
        {
            return SMSNoticeDA.Instance.GetSMSWaitSend(Num_i);
        }
        public async Task GetSMSNoticeAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SMSNoticeListKey";// SysCacheKey.SMSNoticeListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<SMSNoticeEntity> list = null;
                    list = SMSNoticeDA.Instance.GetSMSNoticeAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(SMSNoticeEntity sMSNotice)
        {
            return SMSNoticeDA.Instance.ExistNum(sMSNotice)>0;
        }
		
	}
}

