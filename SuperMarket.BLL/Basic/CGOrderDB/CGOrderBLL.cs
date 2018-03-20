using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表CGOrder的业务逻辑层。
创建时间：2016/12/31 9:41:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{
	  
	/// <summary>
	/// 表CGOrder的业务逻辑层。
	/// </summary>
	public class CGOrderBLL
	{
	    #region 实例化
        public static CGOrderBLL Instance
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
            internal static readonly CGOrderBLL instance = new CGOrderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表CGOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrder">要添加的CGOrder数据实体对象</param>
		public   int AddCGOrder(CGOrderEntity cGOrder)
		{
            

              if (cGOrder.Id > 0)
            {
                return UpdateCGOrder(cGOrder);
            }
				 
          
            else if (CGOrderBLL.Instance.IsExist(cGOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return CGOrderDA.Instance.AddCGOrder(cGOrder);
            }
	 	}

		/// <summary>
		/// 更新一条CGOrder记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="cGOrder">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateCGOrder(CGOrderEntity cGOrder)
		{
			return CGOrderDA.Instance.UpdateCGOrder(cGOrder);
		}
        public int CGOrderRecived(long b2bordercode)
        {
            return CGOrderDA.Instance.CGOrderRecived(b2bordercode);

        }
        /// <summary>
        /// 订单取消
        /// </summary>
        /// <param name="b2bordercode"></param>
        /// <returns></returns>
        public int CGOrderCancel(long b2bordercode)
        {
            return CGOrderDA.Instance.CGOrderCancel(b2bordercode);

        }
        public int CGOrderCancelXuQiu(string b2bordercodestrs)
        {
            return CGOrderDA.Instance.CGOrderCancelXuQiu(b2bordercodestrs);
        }
        /// <summary>
        /// 更新一条CGOrder记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrder">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public CGOrderEntity GetCGOrderByOrderCode(long _orderCode)
        {
            return CGOrderDA.Instance.GetCGOrderByOrderCode(_orderCode);
        }
        public VWCGOrderEntity GetVWCGOrderByOrderCode(long _orderCode)
        {
            return CGOrderDA.Instance.GetVWCGOrderByOrderCode(_orderCode);
        }
        public IList<VWCGOrderEntity> GetVWCGOrderBySourceCode(long sourceCode)
        {
            IList<VWCGOrderEntity> entityList= CGOrderDA.Instance.GetVWCGOrderBySourceCode(sourceCode);
            if(entityList!=null && entityList.Count > 0)
            {
                foreach (var item in entityList)
                {
                    item.OrderDetails = CGOrderDetailBLL.Instance.GetCGOrderDetailAllByCode(item.Code);
                }
            }

            return entityList;
        }
        public IList<CGOrderEntity> GetCGOrderBySourceCode(long sourceCode)
        {
            IList<CGOrderEntity> entityList = CGOrderDA.Instance.GetCGOrderBySourceCode(sourceCode);
          
            return entityList;
        }

        /// <summary>
        /// 判断B2B 订单是否可收获，可以返回B2B订单号 
        /// </summary>
        /// <param name="_orderCode"></param>
        /// <returns></returns>
        public long B2BOrderCanRecived(long _orderCode)
        {
            return CGOrderDA.Instance.B2BOrderCanRecived(_orderCode);
        }
        public int GetCGOrderNumByB2BCode(long _b2borderCode)
        {
            return CGOrderDA.Instance.GetCGOrderNumByB2BCode(_b2borderCode);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderByKey(int id)
        {
            return CGOrderDA.Instance.DeleteCGOrderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderDisabled()
        {
            return CGOrderDA.Instance.DeleteCGOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return CGOrderDA.Instance.DeleteCGOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderDA.Instance.DisableCGOrderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个CGOrder实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>CGOrder实体</returns>
		/// <param name="columns">要返回的列</param>
		public   CGOrderEntity GetCGOrder(int id)
		{
			return CGOrderDA.Instance.GetCGOrder(id);			
		}
        public VWCGOrderEntity GetVWCGOrder(int id)
        {
            return CGOrderDA.Instance.GetVWCGOrder(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<CGOrderEntity> GetCGOrderList(int pageSize, int pageIndex, ref int recordCount, int status, int orderstyle, long soursecode, string key)
        { 
            return CGOrderDA.Instance.GetCGOrderList(pageSize, pageIndex, ref recordCount, status, orderstyle, soursecode, key);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGOrderEntity> GetVWCGOrderList(int pageSize, int pageIndex, ref int recordCount, int cgmemid,int status,int orderstyle,long soursecode,string key )
        {
             return CGOrderDA.Instance.GetVWCGOrderList(pageSize, pageIndex, ref recordCount, cgmemid, status, orderstyle, soursecode, key);
        }

        /// <summary>
        /// 获取订单对象
        /// </summary>
        /// <param name="cgcode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public CGOrderEntity GetCGOrderByCode(long cgcode, int memid)
        {
            return CGOrderDA.Instance.GetCGOrderByCode(cgcode, memid);
        }
        /// <summary>
        /// 获取订单视图
        /// </summary>
        /// <param name="cgcode"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public VWCGOrderEntity GetVWCGOrderByCode(long cgcode,int memid)
        {
            return CGOrderDA.Instance.GetVWCGOrderByCode(cgcode, memid); 
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ReportCGOrderEntity> GetReportsGH(int year,int mon,int day)
        {
            IList<ReportCGOrderEntity> list= CGOrderDA.Instance.GetReportsGH(year, mon, day);
            if(list!=null&& list.Count>0)
            {
                foreach(ReportCGOrderEntity entity in list)
                {
                    MemStoreEntity storeen = MemberDB.StoreBLL.Instance.GetStoreByMemId(entity.CgMemId);
                    entity.CgMemName = storeen.ContactsManName;
                    entity.CgMemPhone = storeen.MobilePhone;
                    entity.CgCompanyName = storeen.CompanyName;
                }
            }
            return list;
        }
         
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderEntity cGOrder)
        {
            return CGOrderDA.Instance.ExistNum(cGOrder)>0;
        }


        public int SplitCGOrder(long sourceOrderCode,string orderDetailIdArr)
        {
            return CGOrderDA.Instance.SplitCGOrder(sourceOrderCode, orderDetailIdArr);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CGOrderCode"></param>
        /// <returns></returns>
        public int UpdateCGOrderStatus(long CGOrderCode,int memid,int status )
        {
            return CGOrderDA.Instance.UpdateCGOrderStatus(CGOrderCode,memid,status);
        }
      
        public Dictionary<int,int> GetXuQiuNextStatus()
        {
            Dictionary<int, int> dic = new Dictionary<int, int>();
            dic.Add((int)CGOrderStatus.WaitRecived, (int)CGOrderStatus.HasDelivery);
            dic.Add((int)CGOrderStatus.HasDelivery, (int)CGOrderStatus.Finished);
            dic.Add((int)CGOrderStatus.Finished, (int)CGOrderStatus.Closed);
            return dic;
        }
    }
}

