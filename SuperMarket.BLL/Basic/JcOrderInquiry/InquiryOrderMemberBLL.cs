using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InquiryOrderMember的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryOrderMember的业务逻辑层。
	/// </summary>
	public class InquiryOrderMemberBLL
	{
	    #region 实例化
        public static InquiryOrderMemberBLL Instance
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
            internal static readonly InquiryOrderMemberBLL instance = new InquiryOrderMemberBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InquiryOrderMember，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderMember">要添加的InquiryOrderMember数据实体对象</param>
		public   int AddInquiryOrderMember(InquiryOrderMemberEntity inquiryOrderMember)
		{
                return InquiryOrderMemberDA.Instance.AddInquiryOrderMember(inquiryOrderMember);
	 	}

		/// <summary>
		/// 更新一条InquiryOrderMember记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderMember">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInquiryOrderMember(InquiryOrderMemberEntity inquiryOrderMember)
		{
			return InquiryOrderMemberDA.Instance.UpdateInquiryOrderMember(inquiryOrderMember);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryOrderMemberByKey(int id)
        {
            return InquiryOrderMemberDA.Instance.DeleteInquiryOrderMemberByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderMemberDisabled()
        {
            return InquiryOrderMemberDA.Instance.DeleteInquiryOrderMemberDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderMemberByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryOrderMemberDA.Instance.DeleteInquiryOrderMemberByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderMemberByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryOrderMemberDA.Instance.DisableInquiryOrderMemberByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryOrderMember实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryOrderMember实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryOrderMemberEntity GetInquiryOrderMember(int id)
		{
			return InquiryOrderMemberDA.Instance.GetInquiryOrderMember(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryOrderMemberEntity> GetInquiryOrderMemberList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InquiryOrderMemberDA.Instance.GetInquiryOrderMemberList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetInquiryOrderMemberAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="InquiryOrderMemberListKey";// SysCacheKey.InquiryOrderMemberListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<InquiryOrderMemberEntity> list = null;
                    list = InquiryOrderMemberDA.Instance.GetInquiryOrderMemberAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryOrderMemberEntity inquiryOrderMember)
        {
            return InquiryOrderMemberDA.Instance.ExistNum(inquiryOrderMember)>0;
        }
		
	}
}

