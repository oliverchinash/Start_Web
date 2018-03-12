using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.PayDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model.Common;

/*****************************************
功能描述：表PayOrderReback的业务逻辑层。
创建时间：2017/11/29 16:19:45
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.PayDB
{
	  
	/// <summary>
	/// 表PayOrderReback的业务逻辑层。
	/// </summary>
	public class PayOrderRebackBLL
	{
	    #region 实例化
        public static PayOrderRebackBLL Instance
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
            internal static readonly PayOrderRebackBLL instance = new PayOrderRebackBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PayOrderReback，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payOrderReback">要添加的PayOrderReback数据实体对象</param>
		public PayOrderRebackEntity AddPayOrderReback(PayOrderRebackEntity payOrderReback)
        {
            Random rd = new Random();
            payOrderReback.PayRebackCode = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.PayRebackDayNo) + rd.Next(100, 999).ToString();
            payOrderReback.CreateTime = DateTime.Now;
            payOrderReback.Id= PayOrderRebackDA.Instance.AddPayOrderReback(payOrderReback);
            return payOrderReback;

         }

		/// <summary>
		/// 更新一条PayOrderReback记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payOrderReback">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdatePayOrderReback(PayOrderRebackEntity payOrderReback)
		{
			return PayOrderRebackDA.Instance.UpdatePayOrderReback(payOrderReback);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayOrderRebackByKey(int id)
        {
            return PayOrderRebackDA.Instance.DeletePayOrderRebackByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayOrderRebackDisabled()
        {
            return PayOrderRebackDA.Instance.DeletePayOrderRebackDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayOrderRebackByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PayOrderRebackDA.Instance.DeletePayOrderRebackByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayOrderRebackByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayOrderRebackDA.Instance.DisablePayOrderRebackByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PayOrderReback实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PayOrderReback实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PayOrderRebackEntity GetPayOrderReback(int id)
		{
			return PayOrderRebackDA.Instance.GetPayOrderReback(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PayOrderRebackEntity> GetPayOrderRebackList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PayOrderRebackDA.Instance.GetPayOrderRebackList(pageSize, pageIndex, ref recordCount);
        }
		
		public IList<PayOrderRebackEntity> GetRebackByPayCode(string paycode)
        { 
            IList<PayOrderRebackEntity> list = null;
            list = PayOrderRebackDA.Instance.GetRebackByPayCode(paycode);
            return list;
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayOrderRebackEntity payOrderReback)
        {
            return PayOrderRebackDA.Instance.ExistNum(payOrderReback)>0;
        }
		
	}
}

