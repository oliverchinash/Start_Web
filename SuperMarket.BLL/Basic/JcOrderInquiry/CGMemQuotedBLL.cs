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
功能描述：表CGMemQuoted的业务逻辑层。
创建时间：2017/8/26 11:07:53
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.JcOrderInquiry
{
	  
	/// <summary>
	/// 表CGMemQuoted的业务逻辑层。
	/// </summary>
	public class CGMemQuotedBLL
	{
	    #region 实例化
        public static CGMemQuotedBLL Instance
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
            internal static readonly CGMemQuotedBLL instance = new CGMemQuotedBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGMemQuoted，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGMemQuoted">要添加的CGMemQuoted数据实体对象</param>
		public   int AddCGMemQuoted(CGMemQuotedEntity cGMemQuoted)
		{
			  if (cGMemQuoted.Id > 0)
            {
                return UpdateCGMemQuoted(cGMemQuoted);
            }
          
            else if (CGMemQuotedBLL.Instance.IsExist(cGMemQuoted))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGMemQuotedDA.Instance.AddCGMemQuoted(cGMemQuoted);
            }
	 	}
        public int AddInquiryToCGMemQuoted(string ordercode, string memids)
        {
            return CGMemQuotedDA.Instance.AddInquiryToCGMemQuoted(ordercode, memids);
        }
        /// <summary>
        /// 为询价单选择供应商（虚拟供应商）
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int SelectInquiryOrderCGMem(string ordercode, int memid)
        {
            return CGMemQuotedDA.Instance.SelectInquiryOrderCGMem(ordercode, memid);
        }
        /// <summary>
        /// 为最终订单选择供应商（最终备货供应商)
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int SelectConfirmOrderCGMem(string ordercode,string confirmcode, int memid)
        {
            return CGMemQuotedDA.Instance.SelectConfirmOrderCGMem(ordercode, confirmcode, memid);
        }
        /// <summary>
        /// 新版选择供应商，支持多个供应商
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="confirmcode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public int SelConfirmOrderCGMem(string inquirycode, string confirmcode, string memprices)
        {
            return CGMemQuotedDA.Instance.SelConfirmOrderCGMem(inquirycode, confirmcode, memprices);
        }
        /// <summary>
        /// 更新一条CGMemQuoted记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGMemQuoted">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCGMemQuoted(CGMemQuotedEntity cGMemQuoted)
		{
			return CGMemQuotedDA.Instance.UpdateCGMemQuoted(cGMemQuoted);
		}
       /// <summary>
       /// 报价关闭
       /// </summary>
       /// <param name="ordercode"></param>
       /// <returns></returns>
        public int QuotedCloseByCode(string ordercode)
        {
            return CGMemQuotedDA.Instance.QuotedCloseByCode(ordercode);
        }
        /// <summary>
        /// 发送通知到供应商
        /// </summary>
        /// <param name="cgmemid"></param>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int CGQuotedSend(int cgmemid,string ordercode)
        {
            return CGMemQuotedDA.Instance.CGQuotedSend(cgmemid, ordercode); 
        }
        /// <summary>
        /// 供应商报价完成
        /// </summary>
        /// <param name="cgmemid"></param>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int CGQuotedFinished(int cgmemid, string ordercode)
        {
            return CGMemQuotedDA.Instance.CGQuotedFinished(cgmemid, ordercode);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGMemQuotedByKey(int id)
        {
            return CGMemQuotedDA.Instance.DeleteCGMemQuotedByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGMemQuotedDisabled(string code,int memid)
        {
            return CGMemQuotedDA.Instance.DeleteCGMemQuotedDisabled(code,   memid);
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGMemQuotedByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGMemQuotedDA.Instance.DeleteCGMemQuotedByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGMemQuotedByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGMemQuotedDA.Instance.DisableCGMemQuotedByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGMemQuoted实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGMemQuoted实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGMemQuotedEntity GetCGMemQuoted(int id)
		{
			return CGMemQuotedDA.Instance.GetCGMemQuoted(id);			
		}
        public CGMemQuotedEntity GetCGMemQuotedByCode(string code,int memid)
        { 
            return CGMemQuotedDA.Instance.GetCGMemQuotedByCode(code, memid);
        }
        public CGMemQuotedEntity GetCGMemHasCheckedByCode(string code)
        {
            return CGMemQuotedDA.Instance.GetCGMemHasCheckedByCode(code);
        }
        public string GetQuotedCGMemByCode(string  code)
        {
            string memids= CGMemQuotedDA.Instance.GetQuotedCGMemByCode(code);
            return memids.Trim(',');
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGMemQuotedEntity> GetCGMemQuotedList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return CGMemQuotedDA.Instance.GetCGMemQuotedList(pageSize, pageIndex, ref recordCount);
        }
        public IList<VWCGMemQuotedEntity> GetInquiryCGMemQuotedList(int pageSize, int pageIndex, ref int recordCount, string ordercode,int hasread,int hasquote, int status,int cgmemid)
        {
            IList<VWCGMemQuotedEntity> list = new List<VWCGMemQuotedEntity>();

            list= CGMemQuotedDA.Instance.GetInquiryCGMemQuotedList(pageSize, pageIndex, ref recordCount,   ordercode,   hasread,   hasquote, status,cgmemid);
            if(list!=null&& list.Count>0)
            {
                foreach(VWCGMemQuotedEntity ent in list)
                {
                    ent.CGMemStore = StoreBLL.Instance.GetVWStoreByMemId(ent.CGMemId);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取需要发送到供应商报价的列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<CGMemQuotedEntity> GetCGMemQuotedListSend(int num )
        {
            return CGMemQuotedDA.Instance.GetCGMemQuotedListSend(num);
        }
        public IList<CGMemQuotedEntity> GetCGMemQuotedNeedSend(string  ordercode)
        {
            return CGMemQuotedDA.Instance.GetCGMemQuotedNeedSend(ordercode);
        }
        /// <summary>
        /// 获取对应订单对应状态下的记录数
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int GetCGMemQuotedByStatus(string ordercode,int status)
        {
            return CGMemQuotedDA.Instance.GetCGMemQuotedByStatus(ordercode, status);
        }
        /// <summary>
        /// 获取对应订单对应状态下的记录数
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int GetCGMemNotQuoted(string ordercode)
        {
            return CGMemQuotedDA.Instance.GetCGMemNotQuoted(ordercode);
        }
        /// <summary>
        /// 获取询价单通知的供应商
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public IList<CGMemQuotedEntity> GetCGMemQuotedAllByCode(string ordercode)
        {
            IList<CGMemQuotedEntity> list = null;
            list = CGMemQuotedDA.Instance.GetCGMemQuotedAllByCode(ordercode);
            return list;
        }
        public IList<VWCGMemQuotedEntity> GetVWCGMemQuotedAllByCode(string ordercode)
        {
            IList<VWCGMemQuotedEntity> list = null;
            list = CGMemQuotedDA.Instance.GetVWCGMemQuotedAllByCode(ordercode);
            if(list!=null&& list.Count>0)
            {
                foreach(VWCGMemQuotedEntity vwmem in list)
                {
                    vwmem.CGMemStore = StoreBLL.Instance.GetVWStoreByMemId(vwmem.CGMemId);
                }
            }
            return list;
        }
        /// <summary>
        /// 获取选中的供应商Id
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public  CGMemQuotedEntity  GetHasCheckedCGMem(string ordercode)
        {
             return  CGMemQuotedDA.Instance.GetHasCheckedCGMem(ordercode);
            
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGMemQuotedEntity cGMemQuoted)
        {
            return CGMemQuotedDA.Instance.ExistNum(cGMemQuoted)>0;
        }
		
	}
}

