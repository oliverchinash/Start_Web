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
using SuperMarket.Model.Basic.VW.Shopping;
using SuperMarket.BLL.ProductDB;
using SuperMarket.Core.Linq;
using System.Linq;


/*****************************************
功能描述：表OrderDetail的业务逻辑层。
创建时间：2016/8/6 10:38:23
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表OrderDetail的业务逻辑层。
    /// </summary>
    public class OrderDetailBLL
    {
        #region 实例化
        public static OrderDetailBLL Instance
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
            internal static readonly OrderDetailBLL instance = new OrderDetailBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表OrderDetail，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="orderDetail">要添加的OrderDetail数据实体对象</param>
        public int AddOrderDetail(OrderDetailEntity orderDetail)
        {
            if (orderDetail.Id > 0)
            {
                return UpdateOrderDetail(orderDetail);
            }
            else if (string.IsNullOrEmpty(orderDetail.ProductName))
            {
                return (int)CommonStatus.ADD_Fail_Empty;
            }
            else if (OrderDetailBLL.Instance.IsExist(orderDetail))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderDetailDA.Instance.AddOrderDetail(orderDetail);
            }
        }

        /// <summary>
        /// 更新一条OrderDetail记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="orderDetail">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateOrderDetail(OrderDetailEntity orderDetail)
        {
            return OrderDetailDA.Instance.UpdateOrderDetail(orderDetail);
        }


        /// <summary>
        /// 根据主键值更新是否可以退换货。如果数据库不存在这条数据将返回0
        /// </summary>
        public int UpdateCanReturnbyId(int id,int canreturn)
        {
            return OrderDetailDA.Instance.UpdateCanReturnbyId(id,canreturn);
        }

        /// <summary>
        /// 根据主键值更新是否可以退换货。如果数据库不存在这条数据将返回0
        /// </summary>
        //public int SetReturnNum(int id,int returnnum)
        //{
        //    return OrderDetailDA.Instance.SetReturnNum(id,returnnum);
        //}

        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderDetailByKey(int id)
        {
            return OrderDetailDA.Instance.DeleteOrderDetailByKey(id);
        }

        
        /// <summary>
        /// 根据主键值更新发货记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int SetIsDelivered(int id)
        {
            return OrderDetailDA.Instance.SetIsDelivered(id);
        }

        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDetailDisabled()
        {
            return OrderDetailDA.Instance.DeleteOrderDetailDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderDetailByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderDetailDA.Instance.DeleteOrderDetailByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderDetailByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderDetailDA.Instance.DisableOrderDetailByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个OrderDetail实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>OrderDetail实体</returns>
        /// <param name="columns">要返回的列</param>
        public OrderDetailEntity GetOrderDetail(int id)
        {
            return OrderDetailDA.Instance.GetOrderDetail(id);
        }

        /// <summary>
        /// 设置不可换货
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int SetCanReturnEqualsZero(int id)
        {
            return OrderDetailDA.Instance.SetCanReturnEqualsZero(id);
        }


        /// <summary>
        /// 获取指定订单下可退换货的子订单数目
        /// </summary>
        /// <returns></returns>
        public int GetRemainCanReturnNum(long ordercode)
        {
            return OrderDetailDA.Instance.GetRemainCanReturnNum(ordercode);
        }
        
        public int UpdateOrderDetailReturnNum(int id,int num)
        {
            return OrderDetailDA.Instance.UpdateOrderDetailReturnNum(id,num);
        }

        /// <summary>
        ///  设置订单状态为已退货
        /// </summary>
        /// <returns>OrderDetail实体</returns>
        /// <param name="columns">要返回的列</param>
        public int SetOrderDetailHasReturn(int id)
        {
            return OrderDetailDA.Instance.SetOrderDetailHasReturn(id);
        }

        public VWOrderDetailEntity GetVWOrderDetail(int id, int memid)
        {
            return OrderDetailDA.Instance.GetVWOrderDetail(id, memid);

        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderDetailEntity> GetOrderDetailList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {

            Int64 _ordercode = 0;
            int _hascomment = -1;
            int _status = -1;
            int _memid = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit _entity in wherelist)
                {
                    if (_entity.FieldName == SearchFieldName.OrderCode)
                    {
                        _ordercode = StringUtils.GetDbLong(_entity.CompareValue);
                    }
                    else if (_entity.FieldName == SearchFieldName.HasComment)
                    {
                        _hascomment = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                    else if (_entity.FieldName == SearchFieldName.OrderStatus)
                    {
                        _status = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                    else if (_entity.FieldName == SearchFieldName.MemId)
                    {
                        _memid = StringUtils.GetDbInt(_entity.CompareValue);
                    }
                }
            }

            return OrderDetailDA.Instance.GetOrderDetailList(pageSize, pageIndex, ref recordCount, _memid, _ordercode, _status, _hascomment);
        }

        /// <summary>
        /// 同步到采购订单，获取订单明细
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public IList<OrderDetailEntity> GetSyncDetailsByCode(Int64 code)
        {
            return OrderDetailDA.Instance.GetSyncDetailsByCode(code);

        }

        public int GetWaitEvaluateNum(int memid, int hascomment,int orderstyle)
        {
            return OrderDetailDA.Instance.GetWaitEvaluateNum(memid, hascomment,orderstyle);

        }
        public async Task GetOrderDetailAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "OrderDetailListKey";// SysCacheKey.OrderDetailListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderDetailEntity> list = null;
                    list = OrderDetailDA.Instance.GetOrderDetailAll();
                    MemCache.AddCache(_cachekey, list);
                }

            });
        }

        public IList<OrderDetailEntity> GetOrderDetailWhole()
        {
            //string _cachekey = "OrderDetailListKey";
            //object obj = MemCache.GetCache(_cachekey);
            //if (obj == null)
            //{
            //    IList<OrderDetailEntity> list = null;
            //    list = OrderDetailDA.Instance.GetOrderDetailAll();
            //    MemCache.AddCache(_cachekey, list);
            //    return list;
            //}
            //else
            //{
            //    return (IList<OrderDetailEntity>)obj;
            //}

            return OrderDetailDA.Instance.GetOrderDetailAll(); 
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderDetailEntity orderDetail)
        {
            return OrderDetailDA.Instance.ExistNum(orderDetail) > 0;
        }
        /// <summary>
        /// 根据账号和订单号获取订单详情列表
        /// </summary>
        /// <param name="memid"></param>
        /// <param name="orderid"></param>
        /// <returns></returns>
        public IList<OrderDetailEntity> GetOrderDetailAllByOrder(int memid, long ordercode,bool getOtherAttr=false)
        {
            IList<OrderDetailEntity> entityList = OrderDetailDA.Instance.GetOrderDetailAllByOrder(memid, ordercode);
            if (getOtherAttr)
            {
                if (entityList!=null&& entityList.Count>0)
                {
                    foreach (var item in entityList)
                    {
                        item.OEM = ProductBLL.Instance.GetProduct(item.ProductId).OECode;
                        item.OriginalPlace = ProductBLL.Instance.GetProduct(item.ProductId).PlaceOrigin;
                    }
                }
            }
            return entityList;
        }
        public IList<VWOrderDetailEntity> GetDetailsByOrderCode(long ordercode,string keyword,int canreturn)
        {
            return OrderDetailDA.Instance.GetDetailsByOrderCode(ordercode, keyword, canreturn);
        }

        /// <summary>
        /// 查询我的退换货订单
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="pageindex"></param>
        /// <param name="recordCount"></param>
        /// <param name="wherelist"></param>
        /// <returns></returns>
        public IList<VWReturnEntity> GetMyReturnsList(int pagesize, int pageindex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _memid = 0;
            string _keyword = "";
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.SeachDefault:
                            {
                                _keyword = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                    }
                }
            }

            return OrderDetailDA.Instance.GetMyReturnsList(pagesize, pageindex, ref recordCount, _memid, _keyword);
        }

        public IList<OrderDetailEntity> GetOrderDetailListOfAdmin(int pagesize, int pageindex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _productname = string.Empty;
            string _ordercode = string.Empty;
            int _hascomment = -1;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.ProductName:
                            {
                                _productname = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.HasComment:
                            {
                                _hascomment = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.OrderCode:
                            {
                                _ordercode = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                    }
                }
            }

            return OrderDetailDA.Instance.GetOrderDetailListOfAdmin(pagesize, pageindex, ref recordCount, _productname, _hascomment, _ordercode);
        }
        /// <summary>
        /// 获取订单详情视图表
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="recordCount"></param>
        /// <param name="wherelist"></param>
        /// <returns></returns>
        public IList<VWOrderDetailEntity> GetVWOrderDetailList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _keyword = "";
            int _status = 0;
            int _term = 0;
            int _memid = 0;

            int _hascomment = -1;
            int _orderstyle = -1;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.SeachDefault:
                            {
                                _keyword = StringUtils.GetDbString(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.OrderStatus:
                            {
                                _status = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.OrderTerm:
                            {
                                _term = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.MemId:
                            {
                                _memid = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.HasComment:
                            {
                                _hascomment = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.OrderStyle:
                            {
                                _orderstyle = StringUtils.GetDbInt(entity.CompareValue);
                            }
                            break;
                    }
                }
            }
            return OrderDetailDA.Instance.GetVWOrderDetailList(pageSize, pageIndex, ref recordCount, _keyword, _status, _term, _memid, _hascomment, _orderstyle);
        }

        public IList<VWOrderCommentEntity> GetOrderCommentList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            IList<VWOrderCommentEntity> list = new List<VWOrderCommentEntity>();
            IList<VWOrderDetailEntity> _orderlist = GetVWOrderDetailList(pageSize, pageIndex, ref recordCount, wherelist);

            IList<VWOrderDetailEntity> objcodelist = _orderlist.DistinctBy(p => p.OrderCode).ToList();
            if (objcodelist != null && objcodelist.Count > 0)
            {
                foreach(VWOrderDetailEntity entity in objcodelist)
                {
                    bool hasaddin = false;
                    foreach(VWOrderCommentEntity comment in  list)
                    {
                        if(entity.OrderCode== comment.OrderCode)
                        {
                            if (comment.OrderDetails == null) comment.OrderDetails = new List<VWOrderDetailEntity>();
                            comment.OrderDetails.Add(entity);
                            hasaddin = true;
                        }
                    }
                    if(!hasaddin)
                    {
                        VWOrderCommentEntity comm = new VWOrderCommentEntity();
                        comm.OrderCode = entity.OrderCode;
                        comm.CreateTime = entity.CreateTime;
                        comm.OrderDetails = new List<VWOrderDetailEntity>();
                        comm.OrderDetails.Add(entity);
                        list.Add(comm);
                    }
                } 

            }

            return list;

        }


        public int CommentHasEvaluate(int orderdetailid)
        {
            return OrderDetailDA.Instance.CommentHasEvaluate(orderdetailid);

        }
        public decimal GetTotalPrice(long ordercode,int producttype)
        {
            return OrderDetailDA.Instance.GetTotalPrice(ordercode, producttype); 
        }
    }
}

