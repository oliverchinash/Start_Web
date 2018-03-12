using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CommentDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.MessageDB;

/*****************************************
功能描述：表InquiryRecords的业务逻辑层。
创建时间：2017/7/4 15:23:09
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL
{
	  
	/// <summary>
	/// 表InquiryRecords的业务逻辑层。
	/// </summary>
	public class InquiryRecordsBLL
	{
	    #region 实例化
        public static InquiryRecordsBLL Instance
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
            internal static readonly InquiryRecordsBLL instance = new InquiryRecordsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InquiryRecords，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryRecords">要添加的InquiryRecords数据实体对象</param>
		public   int AddInquiryRecords(InquiryRecordsEntity inquiryRecords)
		{
            int result= InquiryRecordsDA.Instance.AddInquiryRecords(inquiryRecords);
            if(result>0)
            {
                VWProductEntity P = ProductBLL.Instance.GetProVWByDetailId(inquiryRecords.ProductDetailId);
                EmailSendEntity email = new EmailSendEntity();
                email.CreateTime = DateTime.Now;
                email.Body = "产品名称：" + P.AdTitle + "</br>用户联系手机："+ inquiryRecords.MobilePhone + "</br>备注：" + inquiryRecords.Remark;
                var emailsendstr = System.Configuration.ConfigurationManager.AppSettings["SendMailManager"];
                if(emailsendstr == null||string.IsNullOrEmpty(emailsendstr.ToString()))
                {
                email.Email = "20718505@qq.com;";
                }
                else
                {
                    email.Email = emailsendstr.ToString();
                }
                email.Title = "客户询价";
                email.Status = 0;
                EmailSendBLL.Instance.AddEmailSend(email);
            }
            return result;

         }

		/// <summary>
		/// 更新一条InquiryRecords记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryRecords">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInquiryRecords(InquiryRecordsEntity inquiryRecords)
		{
			return InquiryRecordsDA.Instance.UpdateInquiryRecords(inquiryRecords);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryRecordsByKey(int id)
        {
            return InquiryRecordsDA.Instance.DeleteInquiryRecordsByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryRecordsDisabled()
        {
            return InquiryRecordsDA.Instance.DeleteInquiryRecordsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryRecordsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryRecordsDA.Instance.DeleteInquiryRecordsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryRecordsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryRecordsDA.Instance.DisableInquiryRecordsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryRecords实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryRecords实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryRecordsEntity GetInquiryRecords(int id)
		{
			return InquiryRecordsDA.Instance.GetInquiryRecords(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryRecordsEntity> GetInquiryRecordsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InquiryRecordsDA.Instance.GetInquiryRecordsList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetInquiryRecordsAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="InquiryRecordsListKey";// SysCacheKey.InquiryRecordsListKey;
                object obj = MemCache.GetCache(_cachekey); ;
                if (obj == null)
                {
                    IList<InquiryRecordsEntity> list = null;
                    list = InquiryRecordsDA.Instance.GetInquiryRecordsAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryRecordsEntity inquiryRecords)
        {
            return InquiryRecordsDA.Instance.ExistNum(inquiryRecords)>0;
        }
		
	}
}

