using System;
using System.Data;

using System.Collections.Generic;
using SuperMarket.Model;
using SuperMarket.Data.CGOrderDB;
using System.Threading.Tasks;
using SuperMarket.Model.Cache;
using SuperMarket.Core;

/*****************************************
功能描述：表CGOrderSend的业务逻辑层。
创建时间：2016/12/31 9:41:02
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.CGOrderDB
{

    /// <summary>
    /// 表CGOrderSend的业务逻辑层。
    /// </summary>
    public class CGOrderSendBLL
    {
        #region 实例化
        public static CGOrderSendBLL Instance
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
            internal static readonly CGOrderSendBLL instance = new CGOrderSendBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表CGOrderSend，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderSend">要添加的CGOrderSend数据实体对象</param>
        public int AddCGOrderSend(CGOrderSendEntity cGOrderSend)
        {
            if (cGOrderSend.Id > 0)
            {
                return UpdateCGOrderSend(cGOrderSend);
            }

            //else if (CGOrderSendBLL.Instance.IsExist(cGOrderSend))
            //{
            //    return (int)CommonStatus.ADD_Fail_Exist;
            //}
            else
            {
                return CGOrderSendDA.Instance.AddCGOrderSend(cGOrderSend);
            }
        }

        /// <summary>
        /// 更新一条CGOrderSend记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="cGOrderSend">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateCGOrderSend(CGOrderSendEntity cGOrderSend)
        {
            return CGOrderSendDA.Instance.UpdateCGOrderSend(cGOrderSend);
        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteCGOrderSendByKey(int id)
        {
            return CGOrderSendDA.Instance.DeleteCGOrderSendByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteCGOrderSendDisabled()
        {
            return CGOrderSendDA.Instance.DeleteCGOrderSendDisabled();
        }
        /// <summary>
        /// 供应商报价
        /// </summary>
        /// <param name="cgcode"></param>
        /// <param name="price"></param>
        /// <param name="transfee"></param>
        /// <param name="_PriceStr"></param>
        /// <param name="cgmemid"></param>
        /// <returns></returns>
        public int OrderQuotationToOffer(long cgcode, decimal price, decimal transfee, string _PriceStr, int cgmemid)
        {
            return CGOrderSendDA.Instance.OrderQuotationToOffer(cgcode, price, transfee, _PriceStr, cgmemid);

        }
        /// <summary>
        /// 判断供应商是否已报价
        /// </summary>
        /// <param name="cgcode"></param>
        /// <returns></returns>
        public bool HasQuotation(long cgcode,int cgmemid)
        {
            return CGOrderSendDA.Instance.HasQuotation(cgcode, cgmemid);

        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteCGOrderSendByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderSendDA.Instance.DeleteCGOrderSendByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableCGOrderSendByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return CGOrderSendDA.Instance.DisableCGOrderSendByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个CGOrderSend实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>CGOrderSend实体</returns>
        /// <param name="columns">要返回的列</param>
        public CGOrderSendEntity GetCGOrderSend(int id)
        {
            return CGOrderSendDA.Instance.GetCGOrderSend(id);
        }
        public CGOrderSendEntity GetCGOrderSendByCode(long code, int memid)
        {
            return CGOrderSendDA.Instance.GetCGOrderSendByCode(code, memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGOrderEntity> GetCGOrderSendList(int pageSize, int pageIndex, ref int recordCount, string keyword, int memid, int isclose)
        {
            return CGOrderSendDA.Instance.GetCGOrderSendList(pageSize, pageIndex, ref recordCount, keyword, memid, isclose);
        }


        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGOrderEntity> GetCGOrderOfferList(int pageSize, int pageIndex, ref int recordCount, string keyword, int memid, int status = 0)
        {
            return CGOrderSendDA.Instance.GetCGOrderOfferList(pageSize, pageIndex, ref recordCount, keyword, memid, status);
        }

        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWCGOrderEntity> GetCGOrderTakeList(int pageSize, int pageIndex, ref int recordCount, string keyword, int memid)
        {
            return CGOrderSendDA.Instance.GetCGOrderTakeList(pageSize, pageIndex, ref recordCount, keyword, memid);
        }




        public async Task GetCGOrderSendAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "CGOrderSendListKey";// SysCacheKey.CGOrderSendListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<CGOrderSendEntity> list = null;
                    list = CGOrderSendDA.Instance.GetCGOrderSendAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(CGOrderSendEntity cGOrderSend)
        {
            return CGOrderSendDA.Instance.ExistNum(cGOrderSend) > 0;
        }
         
        public int SendCGOrderToCGMem(long cgordercode, int cgmemid, int memid)
        {
            return CGOrderSendDA.Instance.SendCGOrderToCGMem(cgordercode, cgmemid, memid);

        }

        /// <summary>
        /// 获取待报价订单数量
        /// </summary>
        /// <returns></returns>
        public int GetCGOrderSendOfWaitBJByMemId(int memid)
        {
            return CGOrderSendDA.Instance.GetCGOrderSendOfWaitBJByMemId(memid);
        }
    }
}
