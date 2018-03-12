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
功能描述：表PayReback的业务逻辑层。
创建时间：2016/12/9 14:24:14
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表PayReback的业务逻辑层。
    /// </summary>
    public class PayRebackBLL
    {
        #region 实例化
        public static PayRebackBLL Instance
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
            internal static readonly PayRebackBLL instance = new PayRebackBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表PayReback，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="payReback">要添加的PayReback数据实体对象</param>
        public int AddPayReback(PayRebackEntity payReback)
        {
            if (payReback.Id > 0)
            {
                return UpdatePayReback(payReback);
            }
            else if (PayRebackBLL.Instance.IsExist(payReback))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return PayRebackDA.Instance.AddPayReback(payReback);
            }
        }

        /// <summary>
        /// 更新一条PayReback记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="payReback">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdatePayReback(PayRebackEntity payReback)
        {
            return PayRebackDA.Instance.UpdatePayReback(payReback);
        }

        
        /// <summary>
        /// 更新一条PayReback记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="payReback">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdatePayRebackByTradeNoLog(string tradeNoLog,decimal rebackFee)
        {
            return PayRebackDA.Instance.UpdatePayRebackByTradeNoLog(tradeNoLog, rebackFee);
        }

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeletePayRebackByKey(int id)
        {
            return PayRebackDA.Instance.DeletePayRebackByKey(id);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int ProcPayBackRecive(string code, string num, string details)
        {
            return PayRebackDA.Instance.ProcPayBackRecive(  code,   num,   details);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeletePayRebackDisabled()
        {
            return PayRebackDA.Instance.DeletePayRebackDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeletePayRebackByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayRebackDA.Instance.DeletePayRebackByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisablePayRebackByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return PayRebackDA.Instance.DisablePayRebackByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个PayReback实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>PayReback实体</returns>
        /// <param name="columns">要返回的列</param>
        public PayRebackEntity GetPayReback(int id)
        {
            return PayRebackDA.Instance.GetPayReback(id);
        }
        
        /// <summary>
        /// 根据主键获取一个PayReback实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>PayReback实体</returns>
        /// <param name="columns">要返回的列</param>
        public PayRebackEntity GetPayRebackByTradeNoLog(string tradeNoLog)
        {
            return PayRebackDA.Instance.GetPayRebackByTradeNoLog(tradeNoLog);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<PayRebackEntity> GetPayRebackList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _batchno = "";
            int _status = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit _entity in wherelist)
                {
                    if (_entity.FieldName == SearchFieldName.RebackBatchNo)
                    {
                        _batchno = StringUtils.GetDbString(_entity.CompareValue);
                    }
                    if (_entity.FieldName == SearchFieldName.RebackStatus)
                    {
                        _status = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                }
            }
            return PayRebackDA.Instance.GetPayRebackList(pageSize, pageIndex, ref recordCount, _batchno, _status);
        }
        public IList<string> GetBatchTodayWait()
        {
            return PayRebackDA.Instance.GetBatchTodayWait();

        }
        public IList<PayRebackEntity> GetPayRebackAll(string batchno)
        {
            return PayRebackDA.Instance.GetPayRebackAll(batchno);
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(PayRebackEntity payReback)
        {
            return PayRebackDA.Instance.ExistNum(payReback) > 0;
        }

    }
}

