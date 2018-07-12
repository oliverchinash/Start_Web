using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.PayDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Core.Util;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model.Common;

/*****************************************
功能描述：表PayOrder的业务逻辑层。
创建时间：2017/11/16 8:34:18
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.PayDB
{
	  
	/// <summary>
	/// 表PayOrder的业务逻辑层。
	/// </summary>
	public class PayOrderBLL
	{
	    #region 实例化
        public static PayOrderBLL Instance
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
            internal static readonly PayOrderBLL instance = new PayOrderBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PayOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payOrder">要添加的PayOrder数据实体对象</param>
		public   int AddPayOrder(PayOrderEntity payOrder)
		{
			  if (payOrder.Id > 0)
            {
                return UpdatePayOrder(payOrder);
            }
          
            else if (PayOrderBLL.Instance.IsExist(payOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PayOrderDA.Instance.AddPayOrder(payOrder);
            }
	 	}
        public PayOrderEntity CreatePayOrder(int systemtype,string ordercode,decimal amountprice,int paymethod,int memid,string externalcode)
        {
            PayOrderEntity entity = new PayOrderEntity();
            entity = GetPayOrderBySysCode(systemtype, ordercode, paymethod);
            
            if(entity.Id>0&& !string.IsNullOrEmpty(entity.PayOrderCode)) 
            {
                 return entity; 
            } 
            Random rd = new Random();
            entity.PayOrderCode =  XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.PayOrderDayNo) + rd.Next(100, 999).ToString() ;
            entity.CreateManId = memid; 
            entity.CreateTime = DateTime.Now;
            entity.NeedPayPrice = amountprice;
            entity.PayMethod = paymethod;
            entity.Status = 0;
            entity.SysOrderCode = ordercode;
            entity.SysType = systemtype;
            entity.ExternalCode = externalcode;
            entity.Id= PayOrderDA.Instance.AddPayOrder(entity);
            return entity;
        }

        /// <summary>
        /// 更新一条PayOrder记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="payOrder">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdatePayOrder(PayOrderEntity payOrder)
		{
			return PayOrderDA.Instance.UpdatePayOrder(payOrder);
		}
        /// <summary>
        /// 修改支付方式
        /// </summary>
        /// <param name="paycode"></param>
        /// <param name="paytype"></param>
        /// <returns></returns>
        public int  PayTypeUpdate(string paycode,int paytype,int memid)
        {
            return PayOrderDA.Instance.PayTypeUpdate(paycode, paytype,memid);
        }
        /// <summary>
        /// 收到支付回调修改状态
        /// </summary>
        /// <param name="payOrder"></param>
        /// <returns></returns>
        public int RecivedPaySuccess(VWPayOrderEntity payOrder)
        { 
            return PayOrderDA.Instance.RecivedPaySuccess(payOrder);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayOrderByKey(int id)
        {
            return PayOrderDA.Instance.DeletePayOrderByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayOrderDisabled()
        {
            return PayOrderDA.Instance.DeletePayOrderDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayOrderByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PayOrderDA.Instance.DeletePayOrderByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayOrderDA.Instance.DisablePayOrderByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PayOrder实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PayOrder实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PayOrderEntity GetPayOrder(int id)
		{
			return PayOrderDA.Instance.GetPayOrder(id);			
		}
        public VWPayOrderEntity GetVWPayOrderByPayCode(string payordercode)
        {
            return PayOrderDA.Instance.GetVWPayOrderByPayCode(payordercode);
        }
        /// <summary>
        /// 根据订单编号获取已支付的支付信息
        /// </summary>
        /// <param name="systype"></param>
        /// <param name="syscode"></param>
        /// <returns></returns>
        public  PayOrderEntity GetPayOrderBySysCode(int systype,string syscode,int paytype)
        { 
            return PayOrderDA.Instance.GetPayOrderBySysCode(systype,   syscode, paytype);
        }
        public IList<VWPayOrderEntity> GetVWPayOrderList(int pageSize, int pageIndex, ref int recordCount,string paycode,string syscode,int paymethod,int systype)
        {
            return PayOrderDA.Instance.GetVWPayOrderList(pageSize, pageIndex, ref recordCount, paycode, syscode, paymethod,   systype);
        }
        public async Task GetPayOrderAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PayOrderListKey";// SysCacheKey.PayOrderListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PayOrderEntity> list = null;
                    list = PayOrderDA.Instance.GetPayOrderAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayOrderEntity payOrder)
        {
            return PayOrderDA.Instance.ExistNum(payOrder)>0;
        }
		
	}
}

