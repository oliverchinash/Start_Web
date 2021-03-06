﻿using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表FinanceRefund的业务逻辑层。
创建时间：2017/1/16 12:07:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表FinanceRefund的业务逻辑层。
	/// </summary>
	public class FinanceRefundBLL
	{
	    #region 实例化
        public static FinanceRefundBLL Instance
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
            internal static readonly FinanceRefundBLL instance = new FinanceRefundBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表FinanceRefund，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="financeRefund">要添加的FinanceRefund数据实体对象</param>
		public   int AddFinanceRefund(FinanceRefundEntity financeRefund)
		{
			  if (financeRefund.Id > 0)
            {
                return UpdateFinanceRefund(financeRefund);
            }
          
            else if (FinanceRefundBLL.Instance.IsExist(financeRefund))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return FinanceRefundDA.Instance.AddFinanceRefund(financeRefund);
            }
	 	}

		/// <summary>
		/// 更新一条FinanceRefund记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="financeRefund">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdateFinanceRefund(FinanceRefundEntity financeRefund)
		{
			return FinanceRefundDA.Instance.UpdateFinanceRefund(financeRefund);
		}

        public int AddFinanceRefund(int returnXSId)
        {
            return FinanceRefundDA.Instance.AddFinanceRefund(returnXSId);
        }

         /// <summary>
         /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
         /// </summary>
        public int DeleteFinanceRefundByKey(int id)
        {
            return FinanceRefundDA.Instance.DeleteFinanceRefundByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteFinanceRefundDisabled()
        {
            return FinanceRefundDA.Instance.DeleteFinanceRefundDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteFinanceRefundByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return FinanceRefundDA.Instance.DeleteFinanceRefundByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableFinanceRefundByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return FinanceRefundDA.Instance.DisableFinanceRefundByIds(idstr);
        }

      
        /// <summary>
        /// 根据主键获取一个FinanceRefund实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>FinanceRefund实体</returns>
        /// <param name="columns">要返回的列</param>
        public   FinanceRefundEntity GetFinanceRefund(int id)
		{
			return FinanceRefundDA.Instance.GetFinanceRefund(id);			
		}
		 ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<FinanceRefundEntity> GetFinanceRefundList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return FinanceRefundDA.Instance.GetFinanceRefundList(pageSize, pageIndex, ref recordCount);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWFinanceRefundEntity> GetVWFinanceRefundList(int pageSize, int pageIndex, ref int recordCount,long ordercode)
        {
            return FinanceRefundDA.Instance.GetVWFinanceRefundList(pageSize, pageIndex, ref recordCount, ordercode);
        }
        public async Task GetFinanceRefundAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="FinanceRefundListKey";// SysCacheKey.FinanceRefundListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<FinanceRefundEntity> list = null;
                    list = FinanceRefundDA.Instance.GetFinanceRefundAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(FinanceRefundEntity financeRefund)
        {
            return FinanceRefundDA.Instance.ExistNum(financeRefund)>0;
        }
		
	}
}

