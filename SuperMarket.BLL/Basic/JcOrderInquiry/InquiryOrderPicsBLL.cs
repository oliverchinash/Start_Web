using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表InquiryOrderPics的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryOrderPics的业务逻辑层。
	/// </summary>
	public class InquiryOrderPicsBLL
	{
	    #region 实例化
        public static InquiryOrderPicsBLL Instance
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
            internal static readonly InquiryOrderPicsBLL instance = new InquiryOrderPicsBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表InquiryOrderPics，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderPics">要添加的InquiryOrderPics数据实体对象</param>
		public   int AddInquiryOrderPics(InquiryOrderPicsEntity inquiryOrderPics)
		{
		 
                return InquiryOrderPicsDA.Instance.AddInquiryOrderPics(inquiryOrderPics);
          
	 	}

		/// <summary>
		/// 更新一条InquiryOrderPics记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrderPics">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInquiryOrderPics(InquiryOrderPicsEntity inquiryOrderPics)
		{
			return InquiryOrderPicsDA.Instance.UpdateInquiryOrderPics(inquiryOrderPics);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryOrderPicsByKey(int id)
        {
            return InquiryOrderPicsDA.Instance.DeleteInquiryOrderPicsByKey(id);
        }
        public int DeletePicsFromOrder(string code,int id)
        {
            return InquiryOrderPicsDA.Instance.DeletePicsFromOrder(code,id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderPicsDisabled()
        {
            return InquiryOrderPicsDA.Instance.DeleteInquiryOrderPicsDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderPicsByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryOrderPicsDA.Instance.DeleteInquiryOrderPicsByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderPicsByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryOrderPicsDA.Instance.DisableInquiryOrderPicsByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个InquiryOrderPics实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>InquiryOrderPics实体</returns>
		/// <param name="columns">要返回的列</param>
		public   InquiryOrderPicsEntity GetInquiryOrderPics(int id)
		{
			return InquiryOrderPicsDA.Instance.GetInquiryOrderPics(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<InquiryOrderPicsEntity> GetInquiryOrderPicsList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return InquiryOrderPicsDA.Instance.GetInquiryOrderPicsList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<InquiryOrderPicsEntity> GetInquiryOrderPicsAllByCode(string ordercode)
        { 
            IList<InquiryOrderPicsEntity> list = null;
            list = InquiryOrderPicsDA.Instance.GetInquiryOrderPicsAllByCode(ordercode);
            return list;


        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryOrderPicsEntity inquiryOrderPics)
        {
            return InquiryOrderPicsDA.Instance.ExistNum(inquiryOrderPics)>0;
        }
		
	}
}

