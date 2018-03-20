using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.JcOrderInquiry;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model.Common;
using SuperMarket.BLL.MemberDB;

/*****************************************
功能描述：表InquiryOrder的业务逻辑层。
创建时间：2017/8/23 11:12:11
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表InquiryOrder的业务逻辑层。
	/// </summary>
	public class InquiryOrderBLL 
	{
	    #region 实例化
        public static InquiryOrderBLL Instance
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
            internal static readonly InquiryOrderBLL instance = new InquiryOrderBLL();
        }
        #endregion
		 
		/// <summary>
		/// 更新一条InquiryOrder记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="inquiryOrder">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateInquiryOrder(InquiryOrderEntity inquiryOrder)
		{
			return InquiryOrderDA.Instance.UpdateInquiryOrder(inquiryOrder);
		}
        public int UpdateQuoteStatus(string code,int quotestatus)
        {
            return InquiryOrderDA.Instance.UpdateQuoteStatus(code, quotestatus);
        }
        
        /// <summary>
        /// 报价价格审核销售价
        /// </summary>
        /// <param name="code"></param>
        /// <param name="pricestr"></param>
        /// <returns></returns>
        public int QuotePriceSet(string code,string pricestr, int status, int statusformem)
        {
            return InquiryOrderDA.Instance.QuotePriceSet(code,pricestr,   status,   statusformem);

        }
        /// <summary>
        /// 售价审核通过
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int QuotePriceCheck(string code )
        {
            return InquiryOrderDA.Instance.QuotePriceCheck(code ); 
        }
        public IList<InquiryOrderEntity> GetInquiryOrderToCGQuote(int num )
        {
            return InquiryOrderDA.Instance.GetInquiryOrderToCGQuote(num ); 
        }
        /// <summary>
        /// 订单编辑方法
        /// </summary>
        /// <param name="inquiryOrder"></param>
        /// <returns></returns>
        public string  InquiryOrderEdit(VWInquiryOrderEntity inquiryOrder)
        {
            Random rd = new Random();
            if (inquiryOrder.InquiryOrderId == 0 && string.IsNullOrEmpty(inquiryOrder.InquiryOrderCode))
            {
                inquiryOrder.InquiryOrderCode = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.OrderDayNo) + rd.Next(100, 999).ToString();
                inquiryOrder.Status = (int)OrderInquiryStatusEnum.Edit;
                inquiryOrder.StatusForMem = (int)InquiryStatusForMemEnum.Edit;
            }
            inquiryOrder.CreateTime = DateTime.Now;
            inquiryOrder.CheckTime = DateTime.Now;
             
            if( InquiryOrderDA.Instance.InquiryOrderEdit(inquiryOrder)>0)
            {
                return inquiryOrder.InquiryOrderCode;
            }
            return "";
        }

        public string InquiryOrderEdit1(InquiryOrderEntity order)
        {
            bool needmember = false;
            if(string.IsNullOrEmpty(order.Code))
            { 
                Random rd = new Random();
                order.Code = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.OrderDayNo) + rd.Next(100, 999).ToString();
                needmember = true;
            }
            order.Code = InquiryOrderDA.Instance.InquiryOrderEdit1(order);
            if(needmember)
            {
                MemStoreEntity stor = StoreBLL.Instance.GetStoreByMemId(order.MemId);
                InquiryOrderMemberEntity memen = new InquiryOrderMemberEntity();
                memen.MemId = order.MemId;
                memen.InquiryOrderCode = order.Code;
                memen.MemName = stor.ContactsManName;
                memen.MemPhone = stor.MobilePhone;
                memen.CompanyName = stor.CompanyName;
                memen.CompanyAddress = stor.Address;
                InquiryOrderMemberBLL.Instance.AddInquiryOrderMember(memen);
            }
            return order.Code;
        }
        public int InquiryOrderEdit2(string code, string carmodelid, string carmodelname, int memid)
        {
           
            return InquiryOrderDA.Instance.InquiryOrderEdit2(code, carmodelid, carmodelname, memid);

        }
        /// <summary>
        /// 提交询价
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int InquiryOrderSubmit(string ordercode)
        {
          int result=  InquiryOrderDA.Instance.InquiryOrderSubmit(ordercode);
            return result;
        }
        public int InquiryOrderCancel(string ordercode,int resonid,string remark)
        {
            int result = InquiryOrderDA.Instance.InquiryOrderCancel(ordercode,resonid,   remark);
            return result;
        }
        /// <summary>
        /// 接收询价
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int InquiryOrderAccept(string ordercode)
        {
            int result = InquiryOrderDA.Instance.InquiryOrderAccept(ordercode);
            return result;
        }
        public int InquiryOrderToQuote(string ordercode,int status,int statusformem)
        {
            int result = InquiryOrderDA.Instance.InquiryOrderToQuote(ordercode, status, statusformem);
            return result;
        }
      
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteInquiryOrderByKey(string code)
        {
            return InquiryOrderDA.Instance.DeleteInquiryOrderByKey(code);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteInquiryOrderDisabled()
        {
            return InquiryOrderDA.Instance.DeleteInquiryOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteInquiryOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return InquiryOrderDA.Instance.DeleteInquiryOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableInquiryOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return InquiryOrderDA.Instance.DisableInquiryOrderByIds(idstr);
        }
	 
        public InquiryOrderEntity GetInquiryOrderByCode(string code)
        {
            return InquiryOrderDA.Instance.GetInquiryOrderByCode(code);
        }
        public VWInquiryOrderEntity GetVWInquiryOrderByCode(string code)
        {
            VWInquiryOrderEntity order= InquiryOrderDA.Instance.GetVWInquiryOrderByCode(code);
            
            return order;
        }
        public VWInquiryOrderEntity GetInquiryOrderDataByCode(string code)
        {
            VWInquiryOrderEntity order = InquiryOrderDA.Instance.GetInquiryOrderDataByCode(code);

            return order;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWInquiryOrderEntity> GetInquiryOrderList(int pageSize, int pageIndex, ref  int recordCount,int memid, int status, int statusformem, string key)
        {
            return InquiryOrderDA.Instance.GetInquiryOrderList(pageSize, pageIndex, ref recordCount, memid, status, statusformem, key);
        }
        /// <summary>
        /// 带供应商的参数查询列表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="memid"></param>
        /// <param name="status"></param>
        /// <param name="statusformem"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public IList<VWInquiryOrderEntity> GetInquiryListCG(int pageSize, int pageIndex, ref int recordCount, int memid, int status, int statusformem, string key)
        {
            return InquiryOrderDA.Instance.GetInquiryListCG(pageSize, pageIndex, ref recordCount, memid, status, statusformem, key);
        }
        /// <summary>
        /// 获取询价单报表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="status"></param>
        /// <param name="reporttype"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IList<ReportInquiryOrderEntity> GetReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {
            return InquiryOrderDA.Instance.GetReportDaily(startdate, enddate, status,   reporttype,   orderby);

        }
        /// <summary>
        /// 获取询价单供应商统计报表
        /// </summary>
        /// <param name="startdate"></param>
        /// <param name="enddate"></param>
        /// <param name="status"></param>
        /// <param name="reporttype"></param>
        /// <param name="orderby"></param>
        /// <returns></returns>
        public IList<ReportInquiryOrderEntity> GetCGReportDaily(string startdate, string enddate, int status, int reporttype, int orderby)
        {
            IList<ReportInquiryOrderEntity> list = InquiryOrderDA.Instance.GetCGReportDaily(startdate, enddate, status, reporttype, orderby);
            if (list != null && list.Count > 0)
            {
                foreach (ReportInquiryOrderEntity en in list)
                {
                    if (en.CGMemId > 0)
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
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(InquiryOrderEntity inquiryOrder)
        {
            return InquiryOrderDA.Instance.ExistNum(inquiryOrder)>0;
        }
		
	}
}

