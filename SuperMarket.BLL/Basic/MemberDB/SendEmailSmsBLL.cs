using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MemberDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表SendEmailSms的业务逻辑层。
创建时间：2016/8/2 14:16:08
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MemberDB
{
	  
	/// <summary>
	/// 表SendEmailSms的业务逻辑层。
	/// </summary>
	public class SendEmailSmsBLL
	{
	    #region 实例化
        public static SendEmailSmsBLL Instance
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
            internal static readonly SendEmailSmsBLL instance = new SendEmailSmsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表SendEmailSms，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sendEmailSms">要添加的SendEmailSms数据实体对象</param>
		public   int AddSendEmailSms(MemSendEmailSmsEntity sendEmailSms)
		{
			  if (sendEmailSms.Id > 0)
            {
                return UpdateSendEmailSms(sendEmailSms);
            }
          
            else if (SendEmailSmsBLL.Instance.IsExist(sendEmailSms))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return MemSendEmailSmsDA.Instance.AddMemSendEmailSms(sendEmailSms);
            }
	 	}

		/// <summary>
		/// 更新一条SendEmailSms记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="sendEmailSms">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateSendEmailSms(MemSendEmailSmsEntity sendEmailSms)
		{
			return MemSendEmailSmsDA.Instance.UpdateMemSendEmailSms(sendEmailSms);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteSendEmailSmsByKey(int id)
        {
            return MemSendEmailSmsDA.Instance.DeleteMemSendEmailSmsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteSendEmailSmsDisabled()
        {
            return MemSendEmailSmsDA.Instance.DeleteMemSendEmailSmsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteSendEmailSmsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return MemSendEmailSmsDA.Instance.DeleteMemSendEmailSmsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableSendEmailSmsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return MemSendEmailSmsDA.Instance.DisableMemSendEmailSmsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个SendEmailSms实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>SendEmailSms实体</returns>
		/// <param name="columns">要返回的列</param>
		public   MemSendEmailSmsEntity GetSendEmailSms(int id)
		{
			return MemSendEmailSmsDA.Instance.GetMemSendEmailSms(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<MemSendEmailSmsEntity> GetSendEmailSmsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return MemSendEmailSmsDA.Instance.GetMemSendEmailSmsList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetSendEmailSmsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="SendEmailSmsListKey";// SysCacheKey.SendEmailSmsListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<MemSendEmailSmsEntity> list = null;
                    list = MemSendEmailSmsDA.Instance.GetMemSendEmailSmsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(MemSendEmailSmsEntity sendEmailSms)
        {
            return MemSendEmailSmsDA.Instance.ExistNum(sendEmailSms)>0;
        }
		
	}
}

