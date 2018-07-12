using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表ConfirmOrder的业务逻辑层。
创建时间：2017/10/12 14:15:20
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表ConfirmOrder的业务逻辑层。
	/// </summary>
	public class ConfirmOrderBLL
	{
	    #region 实例化
        public static ConfirmOrderBLL Instance
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
            internal static readonly ConfirmOrderBLL instance = new ConfirmOrderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表ConfirmOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrder">要添加的ConfirmOrder数据实体对象</param>
		public   int AddConfirmOrder(ConfirmOrderEntity confirmOrder)
		{
			  if (confirmOrder.Id > 0)
            {
                return UpdateConfirmOrder(confirmOrder);
            }
				  else if (string.IsNullOrEmpty(confirmOrder.CarBrandName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(confirmOrder.CarSeriesName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
				  else if (string.IsNullOrEmpty(confirmOrder.CarTypeModelName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (ConfirmOrderBLL.Instance.IsExist(confirmOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return ConfirmOrderDA.Instance.AddConfirmOrder(confirmOrder);
            }
	 	}

		/// <summary>
		/// 更新一条ConfirmOrder记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="confirmOrder">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateConfirmOrder(ConfirmOrderEntity confirmOrder)
		{
			return ConfirmOrderDA.Instance.UpdateConfirmOrder(confirmOrder);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteConfirmOrderByKey(int id)
        {
            return ConfirmOrderDA.Instance.DeleteConfirmOrderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteConfirmOrderDisabled()
        {
            return ConfirmOrderDA.Instance.DeleteConfirmOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteConfirmOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return ConfirmOrderDA.Instance.DeleteConfirmOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableConfirmOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ConfirmOrderDA.Instance.DisableConfirmOrderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个ConfirmOrder实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>ConfirmOrder实体</returns>
		/// <param name="columns">要返回的列</param>
		public   ConfirmOrderEntity GetConfirmOrder(int id)
		{
			return ConfirmOrderDA.Instance.GetConfirmOrder(id);			
		}
        public ConfirmOrderEntity GetConfirmOrderByCode(string  code)
        {
            return ConfirmOrderDA.Instance.GetConfirmOrderByCode(code);
        }
        public VWConfirmOrderEntity GetVWConfirmOrderByCode(string code)
        {
            return ConfirmOrderDA.Instance.GetVWConfirmOrderByCode(code);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWConfirmOrderEntity> GetConfirmOrderList(int pageSize, int pageIndex, ref  int recordCount, int memid,int cgmemid, int status, string key,string inquirycode)
        {
            return ConfirmOrderDA.Instance.GetConfirmOrderList(pageSize, pageIndex, ref recordCount, memid, cgmemid, status,   key, inquirycode);
        }
        public IList<ReportInquiryOrderEntity> GetReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {
            return ConfirmOrderDA.Instance.GetReportDaily(startdate, enddate, status, reporttype, orderby);

        }
        /// <summary>
        ///  供应商订单统计报表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="status"></param>
        /// <param name="reporttype"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IList<ReportInquiryOrderEntity> GetCGReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {

            IList<ReportInquiryOrderEntity> list= ConfirmOrderDA.Instance.GetCGReportDaily(startdate, enddate, status, reporttype, orderby);
            if(list!=null&& list.Count>0)
            {
                foreach(ReportInquiryOrderEntity en in list)
                {
                    if(en.CGMemId>0)
                    {
                        MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(en.CGMemId);
                        en.CGCompanyName = store.CompanyName;
                        en.CGMemName = store.ContactsManName; 
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 发货
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int OrderDeliverySubmit(string ordercode, string wlremark)
        {
            int result = ConfirmOrderDA.Instance.OrderDeliverySubmit(ordercode, wlremark);
            return result;
        }
        /// <summary>
        /// 分配送货员
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="wlremark"></param>
        /// <param name="deliverymemid"></param>
        /// <returns></returns>
        public int OrderDeliveryAssign(string ordercode, string wlremark,int deliverymemid)
        {
            int result = ConfirmOrderDA.Instance.OrderDeliveryAssign(ordercode, wlremark, deliverymemid);
            return result;
        }
        public int OrderDeliveryReback(string ordercode, string wlremark, int deliverymemid)
        {
            int result = ConfirmOrderDA.Instance.OrderDeliveryReback(ordercode, wlremark, deliverymemid);
            return result;
        }
        public async Task GetConfirmOrderAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="ConfirmOrderListKey";// SysCacheKey.ConfirmOrderListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ConfirmOrderEntity> list = null;
                    list = ConfirmOrderDA.Instance.GetConfirmOrderAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ConfirmOrderEntity confirmOrder)
        {
            return ConfirmOrderDA.Instance.ExistNum(confirmOrder)>0;
        }
		
	}
}

