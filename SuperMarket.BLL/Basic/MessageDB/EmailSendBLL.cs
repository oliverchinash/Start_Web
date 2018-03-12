using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.MessageDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表EmailSend的业务逻辑层。
创建时间：2017/2/21 22:36:51
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.MessageDB
{
	  
	/// <summary>
	/// 表EmailSend的业务逻辑层。
	/// </summary>
	public class EmailSendBLL
	{
	    #region 实例化
        public static EmailSendBLL Instance
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
            internal static readonly EmailSendBLL instance = new EmailSendBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表EmailSend，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="emailSend">要添加的EmailSend数据实体对象</param>
		public   int AddEmailSend(EmailSendEntity emailSend)
		{
		       return EmailSendDA.Instance.AddEmailSend(emailSend); 
	 	}
        /// <summary>
        /// 微信发送失败邮件通知
        /// </summary>
        /// <param name="wechatunionid"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public int WeiXinSendFail(string wechatunionid,string url)
        {
            EmailSendEntity email = new EmailSendEntity();
            email.CreateTime = DateTime.Now;
            VWMemberEntity member = MemberBLL.Instance.GetVWMemberByPhone(wechatunionid);

            email.Body = "微信通知用户：" + member.CompanyName + "(" + member.ContactsManName + " " + member.MobilePhone + ")" + member.MemGradeName + "，<br/>网址：" + url;
            var emailsendstr = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.SendMailManager;
            if (emailsendstr == null || string.IsNullOrEmpty(emailsendstr.ToString()))
            {
                email.Email = "20718505@qq.com";
            }
            else
            {
                email.Email = emailsendstr.ToString();
            }
            email.Title = "发送微信通知失败";
            email.Status = 0;
           return  AddEmailSend(email);
        }
        public int OrderRemind(string ordercode)
        {
            return EmailSendDA.Instance.OrderRemind(ordercode); 
        }
        /// <summary>
        /// 更新一条EmailSend记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="emailSend">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateEmailSend(EmailSendEntity emailSend)
		{
			return EmailSendDA.Instance.UpdateEmailSend(emailSend);
		}
        public int UpDateSendStatus(int id)
        {
            return EmailSendDA.Instance.UpDateSendStatus(id);

        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteEmailSendByKey(int id)
        {
            return EmailSendDA.Instance.DeleteEmailSendByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteEmailSendDisabled()
        {
            return EmailSendDA.Instance.DeleteEmailSendDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteEmailSendByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return EmailSendDA.Instance.DeleteEmailSendByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableEmailSendByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return EmailSendDA.Instance.DisableEmailSendByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个EmailSend实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>EmailSend实体</returns>
		/// <param name="columns">要返回的列</param>
		public   EmailSendEntity GetEmailSend(int id)
		{
			return EmailSendDA.Instance.GetEmailSend(id);			
		}
        public IList<EmailSendEntity> GetEmailWaitSend(int num)
        {
            return EmailSendDA.Instance.GetEmailWaitSend(num);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<EmailSendEntity> GetEmailSendList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return EmailSendDA.Instance.GetEmailSendList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetEmailSendAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="EmailSendListKey";// SysCacheKey.EmailSendListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<EmailSendEntity> list = null;
                    list = EmailSendDA.Instance.GetEmailSendAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(EmailSendEntity emailSend)
        {
            return EmailSendDA.Instance.ExistNum(emailSend)>0;
        }
		
	}
}

