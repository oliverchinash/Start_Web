using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InquiryOrderFeedBack的业务逻辑层。
创建时间：2017/10/14 15:02:50
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryOrderFeedBack的业务逻辑层。
	/// </summary>
	public class InquiryOrderFeedBackBLL
	{
	    #region 实例化
        public static InquiryOrderFeedBackBLL Instance
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
            internal static readonly InquiryOrderFeedBackBLL instance = new InquiryOrderFeedBackBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InquiryOrderFeedBack，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderFeedBack">要添加的InquiryOrderFeedBack数据实体对象</param>
		public   int AddInquiryOrderFeedBack(InquiryOrderFeedBackEntity inquiryOrderFeedBack)
		{
			   return InquiryOrderFeedBackDA.Instance.AddInquiryOrderFeedBack(inquiryOrderFeedBack);
     	}

		/// <summary>
		/// 更新一条InquiryOrderFeedBack记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderFeedBack">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInquiryOrderFeedBack(InquiryOrderFeedBackEntity inquiryOrderFeedBack)
		{
			return InquiryOrderFeedBackDA.Instance.UpdateInquiryOrderFeedBack(inquiryOrderFeedBack);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryOrderFeedBackByKey(int id)
        {
            return InquiryOrderFeedBackDA.Instance.DeleteInquiryOrderFeedBackByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderFeedBackDisabled()
        {
            return InquiryOrderFeedBackDA.Instance.DeleteInquiryOrderFeedBackDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderFeedBackByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryOrderFeedBackDA.Instance.DeleteInquiryOrderFeedBackByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderFeedBackByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryOrderFeedBackDA.Instance.DisableInquiryOrderFeedBackByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryOrderFeedBack实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryOrderFeedBack实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryOrderFeedBackEntity GetInquiryOrderFeedBack(int id)
		{
			return InquiryOrderFeedBackDA.Instance.GetInquiryOrderFeedBack(id);			
		}
		///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryOrderFeedBackEntity> GetInquiryOrderFeedBackList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InquiryOrderFeedBackDA.Instance.GetInquiryOrderFeedBackList(pageSize, pageIndex, ref recordCount);
        } 
        public IList<InquiryOrderFeedBackEntity> GetOrderFeedBackAllByCode(string code)
        { 
            IList<InquiryOrderFeedBackEntity> list = null;
            list = InquiryOrderFeedBackDA.Instance.GetOrderFeedBackAllByCode(code);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryOrderFeedBackEntity inquiryOrderFeedBack)
        {
            return InquiryOrderFeedBackDA.Instance.ExistNum(inquiryOrderFeedBack)>0;
        }
		
	}
}

