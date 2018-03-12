using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;

/*****************************************
功能描述：表PayAliResult的业务逻辑层。
创建时间：2016/10/10 16:27:08
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{
	  
	/// <summary>
	/// 表PayAliResult的业务逻辑层。
	/// </summary>
	public class PayAliResultBLL
	{
	    #region 实例化
        public static PayAliResultBLL Instance
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
            internal static readonly PayAliResultBLL instance = new PayAliResultBLL();
        }
        #endregion
		/// <summary>
		/// 插入一条记录到表PayAliResult，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payAliResult">要添加的PayAliResult数据实体对象</param>
		public   int AddPayAliResult(PayAliResultEntity payAliResult)
		{
			 return PayAliResultDA.Instance.AddPayAliResult(payAliResult);
          
	 	}

		/// <summary>
		/// 更新一条PayAliResult记录。
		/// 该方法提供给界面等UI层调用
		/// </summary>
		/// <param name="payAliResult">待更新的实体对象</param>
		/// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
		public   int UpdatePayAliResult(PayAliResultEntity payAliResult)
		{
			return PayAliResultDA.Instance.UpdatePayAliResult(payAliResult);
		}
		 /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayAliResultByKey(int id)
        {
            return PayAliResultDA.Instance.DeletePayAliResultByKey(id);
        }
		 /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayAliResultDisabled()
        {
            return PayAliResultDA.Instance.DeletePayAliResultDisabled();
        }
		 /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayAliResultByIds(string ids)
        {
             int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray); 
            return PayAliResultDA.Instance.DeletePayAliResultByIds(idstr); 
        }
	    /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayAliResultByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayAliResultDA.Instance.DisablePayAliResultByIds(idstr);
        }
		/// <summary>
		/// 根据主键获取一个PayAliResult实体记录。
		/// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
		/// </summary>
		/// <returns>PayAliResult实体</returns>
		/// <param name="columns">要返回的列</param>
		public   PayAliResultEntity GetPayAliResult(int id)
		{
			return PayAliResultDA.Instance.GetPayAliResult(id);			
		}
		  ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PayAliResultEntity> GetPayAliResultList(int pageSize, int pageIndex, ref  int recordCount,IList<ConditionUnit> wherelist)
        {
            return PayAliResultDA.Instance.GetPayAliResultList(pageSize, pageIndex, ref recordCount);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWPayAliResultEntity> GetVWPayAliResultList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            long _ordercode = 0; 
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit _entity in wherelist)
                {
                    if (_entity.FieldName == SearchFieldName.OrderCode)
                    {
                        _ordercode = StringUtils.GetDbLong(_entity.CompareValue);
                    }
                    
                }
            }
            return PayAliResultDA.Instance.GetVWPayAliResultList(pageSize, pageIndex, ref recordCount, _ordercode);
        }
        public async Task GetPayAliResultAll()
        {
            await Task.Run(() =>
            {
                string _cachekey ="PayAliResultListKey";// SysCacheKey.PayAliResultListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<PayAliResultEntity> list = null;
                    list = PayAliResultDA.Instance.GetPayAliResultAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
		/// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayAliResultEntity payAliResult)
        {
            return PayAliResultDA.Instance.ExistNum(payAliResult)>0;
        }
		
	}
}

