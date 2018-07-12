using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表ReturnXSWL的业务逻辑层。
创建时间：2017/1/16 12:07:59
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表ReturnXSWL的业务逻辑层。
    /// </summary>
    public class ReturnXSWLBLL
    {
        #region 实例化
        public static ReturnXSWLBLL Instance
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
            internal static readonly ReturnXSWLBLL instance = new ReturnXSWLBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表ReturnXSWL，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXSWL">要添加的ReturnXSWL数据实体对象</param>
        public int AddReturnXSWL(ReturnXSWLEntity returnXSWL)
        {
            if (returnXSWL.Id > 0)
            {
                return UpdateReturnXSWL(returnXSWL);
            }   
            else
            {
                return ReturnXSWLDA.Instance.AddReturnXSWL(returnXSWL);
            }
        }
        public int SaveReturnXSWL(ReturnXSWLEntity returnXSWL)
        {
            return ReturnXSWLDA.Instance.SaveReturnXSWL(returnXSWL);

        }
        /// <summary>
        /// 更新一条ReturnXSWL记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="returnXSWL">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateReturnXSWL(ReturnXSWLEntity returnXSWL)
        {
            return ReturnXSWLDA.Instance.UpdateReturnXSWL(returnXSWL);
        }
        
             public int UpdateReturnXSWLOfPickUp(int returnId,string accepterName,string accepterPhone)
        {
            return ReturnXSWLDA.Instance.UpdateReturnXSWLOfPickUp(returnId,accepterName,accepterPhone);
        }

        public int UpdateReturnXSWLOfExpress(int returnId, string WLCode, string WLComName)
        {
            return ReturnXSWLDA.Instance.UpdateReturnXSWLOfExpress(returnId, WLCode, WLComName);
        }
        /// <summary>
        /// 退换货采购供应商确认回调
        /// </summary>
        /// <param name="returnid"></param>
        /// <param name="cgmemid"></param>
        /// <returns></returns>
        public int ReBackRecivedCG(int returnid,int cgmemid)
        {
            return ReturnXSWLDA.Instance.ReBackRecivedCG(returnid, cgmemid);

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteReturnXSWLByKey(int id)
        {
            return ReturnXSWLDA.Instance.DeleteReturnXSWLByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteReturnXSWLDisabled()
        {
            return ReturnXSWLDA.Instance.DeleteReturnXSWLDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteReturnXSWLByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ReturnXSWLDA.Instance.DeleteReturnXSWLByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableReturnXSWLByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return ReturnXSWLDA.Instance.DisableReturnXSWLByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个ReturnXSWL实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>ReturnXSWL实体</returns>
        /// <param name="columns">要返回的列</param>
        public ReturnXSWLEntity GetReturnXSWL(int id)
        {
            return ReturnXSWLDA.Instance.GetReturnXSWL(id);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<ReturnXSWLEntity> GetReturnXSWLList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            return ReturnXSWLDA.Instance.GetReturnXSWLList(pageSize, pageIndex, ref recordCount);
        }

        public async Task GetReturnXSWLAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "ReturnXSWLListKey";// SysCacheKey.ReturnXSWLListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<ReturnXSWLEntity> list = null;
                    list = ReturnXSWLDA.Instance.GetReturnXSWLAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(ReturnXSWLEntity returnXSWL)
        {
            return ReturnXSWLDA.Instance.ExistNum(returnXSWL) > 0;
        }

    }
}

