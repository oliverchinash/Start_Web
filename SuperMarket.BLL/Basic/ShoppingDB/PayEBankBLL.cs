using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表PayEBank的业务逻辑层。
创建时间：2016/10/9 10:39:39
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表PayEBank的业务逻辑层。
	/// </summary>
	public class PayEBankBLL
	{
	    #region 实例化
        public static PayEBankBLL Instance
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
            internal static readonly PayEBankBLL instance = new PayEBankBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PayEBank，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payEBank">要添加的PayEBank数据实体对象</param>
		public   int AddPayEBank(PayEBankEntity payEBank)
		{
			  if (payEBank.Id > 0)
            {
                return UpdatePayEBank(payEBank);
            }
				  else if (string.IsNullOrEmpty(payEBank.EBankName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }	 
          
            else if (PayEBankBLL.Instance.IsExist(payEBank))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PayEBankDA.Instance.AddPayEBank(payEBank);
            }
	 	}

		/// <summary>
		/// 更新一条PayEBank记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payEBank">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdatePayEBank(PayEBankEntity payEBank)
		{
			return PayEBankDA.Instance.UpdatePayEBank(payEBank);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayEBankByKey(int id)
        {
            return PayEBankDA.Instance.DeletePayEBankByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayEBankDisabled()
        {
            return PayEBankDA.Instance.DeletePayEBankDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayEBankByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PayEBankDA.Instance.DeletePayEBankByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayEBankByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayEBankDA.Instance.DisablePayEBankByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PayEBank实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PayEBank实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PayEBankEntity GetPayEBank(int id)
		{
			return PayEBankDA.Instance.GetPayEBank(id);			
		}
        public PayEBankEntity GetPayEBankByPayType(int paytype)
        {
            return PayEBankDA.Instance.GetPayEBankByPayType(paytype);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PayEBankEntity> GetPayEBankList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PayEBankDA.Instance.GetPayEBankList(pageSize, pageIndex, ref recordCount);
        }
		
		public async Task GetPayEBankAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PayEBankListKey";// SysCacheKey.PayEBankListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PayEBankEntity> list = null;
                    list = PayEBankDA.Instance.GetPayEBankAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayEBankEntity payEBank)
        {
            return PayEBankDA.Instance.ExistNum(payEBank)>0;
        }
		
	}
}

