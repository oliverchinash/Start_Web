using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.ShoppingDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表WLPsOrder的业务逻辑层。
创建时间：2016/9/19 11:39:57
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表WLPsOrder的业务逻辑层。
    /// </summary>
    public class WLPsOrderBLL
    {
        #region 实例化
        public static WLPsOrderBLL Instance
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
            internal static readonly WLPsOrderBLL instance = new WLPsOrderBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表WLPsOrder，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="wLPsOrder">要添加的WLPsOrder数据实体对象</param>
        public int AddWLPsOrder(WLPsOrderEntity wLPsOrder)
        {
            if (wLPsOrder.Id > 0)
            {
                return UpdateWLPsOrder(wLPsOrder);
            }

            else if (WLPsOrderBLL.Instance.IsExist(wLPsOrder))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return WLPsOrderDA.Instance.AddWLPsOrder(wLPsOrder);
            }
        }

        /// <summary>
        /// 更新一条WLPsOrder记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="wLPsOrder">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateWLPsOrder(WLPsOrderEntity wLPsOrder)
        {
            return WLPsOrderDA.Instance.UpdateWLPsOrder(wLPsOrder);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteWLPsOrderByKey(int id)
        {
            return WLPsOrderDA.Instance.DeleteWLPsOrderByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteWLPsOrderDisabled()
        {
            return WLPsOrderDA.Instance.DeleteWLPsOrderDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteWLPsOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WLPsOrderDA.Instance.DeleteWLPsOrderByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableWLPsOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return WLPsOrderDA.Instance.DisableWLPsOrderByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个WLPsOrder实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>WLPsOrder实体</returns>
        /// <param name="columns">要返回的列</param>
        public WLPsOrderEntity GetWLPsOrder(int id)
        {
            return WLPsOrderDA.Instance.GetWLPsOrder(id);
        }

        public WLPsOrderEntity GetWLPsOrderByCode(Int64 code)
        {
            return WLPsOrderDA.Instance.GetWLPsOrderByCode(code);
        }
      

        public WLPsOrderEntity GetWLPsOrderByOrderCode(string ordercode)
        {
            return WLPsOrderDA.Instance.GetWLPsOrderByOrderCode(ordercode);
        }
        



        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<WLPsOrderEntity> GetWLPsOrderList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            return WLPsOrderDA.Instance.GetWLPsOrderList(pageSize, pageIndex, ref recordCount);
        }

        public async Task GetWLPsOrderAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "WLPsOrderListKey";// SysCacheKey.WLPsOrderListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<WLPsOrderEntity> list = null;
                    list = WLPsOrderDA.Instance.GetWLPsOrderAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(WLPsOrderEntity wLPsOrder)
        {
            return WLPsOrderDA.Instance.ExistNum(wLPsOrder) > 0;
        }

        public IList<WLPsOrderEntity> NewGetWLPsOrderAll()
        {
            return WLPsOrderDA.Instance.GetWLPsOrderAll();
        }

    }
}

