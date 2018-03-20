using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model.Common;

/*****************************************
功能描述：表CGQuotedPrice的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表CGQuotedPrice的业务逻辑层。
	/// </summary>
	public class CGQuotedPriceBLL
	{
	    #region 实例化
        public static CGQuotedPriceBLL Instance
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
            internal static readonly CGQuotedPriceBLL instance = new CGQuotedPriceBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGQuotedPrice，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGQuotedPrice">要添加的CGQuotedPrice数据实体对象</param>
		public   int AddCGQuotedPrice(CGQuotedPriceEntity cGQuotedPrice)
		{
			  if (cGQuotedPrice.Id > 0)
            {
                return UpdateCGQuotedPrice(cGQuotedPrice);
            }
          
            else if (CGQuotedPriceBLL.Instance.IsExist(cGQuotedPrice))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGQuotedPriceDA.Instance.AddCGQuotedPrice(cGQuotedPrice);
            }
	 	}
        public int  QuotedPriceCG(string ordercode, int hasinstock, string cgmemremark, int cgmemid,string prices)
        {
            return CGQuotedPriceDA.Instance.QuotedPriceCG(ordercode, hasinstock, cgmemremark, cgmemid, prices);

        }
        /// <summary>
        /// 更新一条CGQuotedPrice记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGQuotedPrice">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCGQuotedPrice(CGQuotedPriceEntity cGQuotedPrice)
		{
			return CGQuotedPriceDA.Instance.UpdateCGQuotedPrice(cGQuotedPrice);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGQuotedPriceByKey(int id)
        {
            return CGQuotedPriceDA.Instance.DeleteCGQuotedPriceByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGQuotedPriceDisabled()
        {
            return CGQuotedPriceDA.Instance.DeleteCGQuotedPriceDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGQuotedPriceByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGQuotedPriceDA.Instance.DeleteCGQuotedPriceByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGQuotedPriceByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGQuotedPriceDA.Instance.DisableCGQuotedPriceByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGQuotedPrice实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGQuotedPrice实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGQuotedPriceEntity GetCGQuotedPrice(int id)
		{
			return CGQuotedPriceDA.Instance.GetCGQuotedPrice(id);			
		}
        /// <summary>
        /// 用户选择订货的产品
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <param name="productselectstr"></param>
        /// <returns></returns>
        public int UserSelectQuote(string ordercode,string confirmcode, string productselectstr)
        {
          
            return CGQuotedPriceDA.Instance.UserSelectQuote(ordercode, confirmcode, productselectstr); 
        }
        public int InquirySelectQuote(string ordercode,   string prices)
        {

            return CGQuotedPriceDA.Instance.InquirySelectQuote(ordercode, prices);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGQuotedPriceEntity> GetCGQuotedPriceList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGQuotedPriceDA.Instance.GetCGQuotedPriceList(pageSize, pageIndex, ref recordCount);
        }

        public IList<CGQuotedPriceEntity> GetCGQuotedPriceAll(string ordercode,int cgmemid, bool hascgprice = false, bool hasprice = false, bool hasselected = false)
        {
            IList<CGQuotedPriceEntity> list = null;
            list = CGQuotedPriceDA.Instance.GetCGQuotedPriceAll(ordercode, cgmemid, hascgprice, hasprice, hasselected);
            return list;
        }
        public IList<VWCGQuotedPriceEntity> GetVWCGQuotedPriceAll(string ordercode, int cgmemid, bool hascgprice = false, bool hasprice = false, bool hasselected = false)
        {
            IList<VWCGQuotedPriceEntity> list = null;
            list = CGQuotedPriceDA.Instance.GetVWCGQuotedPriceAll(ordercode, cgmemid, hascgprice, hasprice, hasselected);
            if(list!=null&& list.Count>0)
            {
                foreach(VWCGQuotedPriceEntity entity in list)
                {
                    MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(entity.CGMemId);
                    entity.CGComName = store.CompanyName; 
                    entity.CGMemName = store.ContactsManName; 
                    entity.CGMemPhone = store.MobilePhone;  
                    entity.CGComAddress = store.Address;
                }
            }
            return list;
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGQuotedPriceEntity cGQuotedPrice)
        {
            return CGQuotedPriceDA.Instance.ExistNum(cGQuotedPrice)>0;
        }
		
	}
}

