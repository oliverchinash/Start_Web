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
using SuperMarket.BLL.SysDB;

/*****************************************
功能描述：表Order的业务逻辑层。
创建时间：2016/8/4 11:02:05
创 建 人：jc001
变更记录：
******************************************/
namespace SuperMarket.BLL.ShoppingDB
{

    /// <summary>
    /// 表Order的业务逻辑层。
    /// </summary>
    public class OrderBLL
    {
        #region 实例化
        public static OrderBLL Instance
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
            internal static readonly OrderBLL instance = new OrderBLL();
        }
        #endregion
        /// <summary>
        /// 插入一条记录到表Order，如果表中存在自增字段，则返回值为新记录的自增字段值，否则返回0。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="order">要添加的Order数据实体对象</param>
        public int AddOrder(OrderEntity order)
        {
            if (order.Id > 0)
            {
                return UpdateOrder(order);
            }

            else if (OrderBLL.Instance.IsExist(order))
            {
                return (int)CommonStatus.ADD_Fail_Exist;
            }
            else
            {
                return OrderDA.Instance.AddOrder(order);
            }
        }

        /// <summary>
        /// 更新一条Order记录。
        /// 该方法提供给界面等UI层调用
        /// </summary>
        /// <param name="order">待更新的实体对象</param>
        /// <param name="columns">要更新的列名，不提供任何列名时默认将更新主键之外的所有列</param>
        public int UpdateOrder(OrderEntity order)
        {
            return OrderDA.Instance.UpdateOrder(order);
        }
        //public int RecivedStatus(long code)
        //{
        //    return OrderDA.Instance.RecivedStatus(code);
        //}
        //public int PayCheck(long ordercode)
        //{
        //    return OrderDA.Instance.UpdateOrderStatus(ordercode, OrderStatus.WaitDeal, OrderStatus.WaitDeliver);
        //}
        /// <summary>
        /// 修改采购订单状态
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="oldstatus"></param>
        /// <param name="newstatus"></param>
        /// <returns></returns>
        public int UpdateOrderStatus(long ordercode, OrderStatus oldstatus, OrderStatus newstatus)
        {
            return OrderDA.Instance.UpdateOrderStatus(ordercode, oldstatus, newstatus);
        }
        public int OrderFinaceSubmit(string ordercode, int memid)
        {
            return OrderDA.Instance.OrderFinaceSubmit(ordercode, memid);

        }
        
             public int OrderCancelXuQiu(long ordercode, string timecode, string reason, int memid, int _oldstatus)
        {
            return OrderDA.Instance.OrderCancelXuQiu(ordercode, timecode, reason, memid, _oldstatus);
        }
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="reason"></param>
        /// <param name="memid"></param>
        /// <param name="_oldstatus">原定单状态</param>
        /// <returns></returns>
        public int OrderCancel(long ordercode, string timecode, string reason, int memid, int _oldstatus)
        {
            return OrderDA.Instance.OrderCancel(ordercode, timecode, reason, memid, _oldstatus);
        }
        /// <summary>
        /// 收款后，发货前取消订单
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="timecode"></param>
        /// <param name="reason"></param>
        /// <param name="memid"></param>
        /// <param name="_oldstatus"></param>
        /// <returns></returns>
        public int OrderCancel2(long ordercode, string timecode, string reason, int memid, int _oldstatus)
        {
            return OrderDA.Instance.OrderCancel2(ordercode, timecode, reason, memid, _oldstatus);
        }
        public int OrderCancel3(long ordercode, string timecode, string reason, int memid, int _oldstatus)
        {
            return OrderDA.Instance.OrderCancel3(ordercode, timecode, reason, memid, _oldstatus);
        }
        /// <summary>
        /// 取消订单后台审批通过
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="timecode"></param>
        /// <returns></returns>
        public int OrderCancelCheckPass(long ordercode, string timecode)
        {
            return OrderDA.Instance.OrderCancelCheckPass(ordercode, timecode);
        }
        public int OrderCancelCheckReject(long ordercode, string timecode)
        {
            return OrderDA.Instance.OrderCancelCheckReject(ordercode, timecode);
        }
        /// <summary>
        /// 财务退积分
        /// </summary>
        /// <returns></returns>
        public int RebackIntegral(long ordercode, string timecode, int refundid, int rebackintegral, int memid)
        {
            return OrderDA.Instance.RebackIntegral(  ordercode,   timecode,   refundid,   rebackintegral,   memid);

        }
        public int SendOrder(long ordercode)
        {
            return OrderDA.Instance.SendOrder(ordercode );

        }
        public int OrderRecived(long code,int memid)
        {
            return OrderDA.Instance.OrderRecived(code, memid); 
        }
       
        public int EditOrderCouponsNum(long code,int num)
        {
            return OrderDA.Instance.EditOrderCouponsNum(code, num);

        }
        public int OrderRecived(long code)
        {
            return OrderDA.Instance.OrderRecived(code);

        }
        /// <summary>
        /// 根据主键值删除记录。如果数据库不存在这条数据将返回0
        /// </summary>
        public int DeleteOrderByKey(int id)
        {
            return OrderDA.Instance.DeleteOrderByKey(id);
        }
        /// <summary>
        /// 删除失效记录，默认保留2个月
        /// </summary>
        /// <returns></returns>
        public int DeleteOrderDisabled()
        {
            return OrderDA.Instance.DeleteOrderDisabled();
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderDA.Instance.DeleteOrderByIds(idstr);
        }
        /// <summary>
        /// 做失效处理
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DisableOrderByIds(string ids)
        {
            int[] intArray;
            string[] strids = ids.Split(',');
            intArray = Array.ConvertAll<string, int>(strids, s => int.Parse(s));
            string idstr = String.Join(",", intArray);
            return OrderDA.Instance.DisableOrderByIds(idstr);
        }
        /// <summary>
        /// 根据主键获取一个Order实体记录。
        /// 该方法提供给其他实体的业务逻辑层（Logic）方法调用
        /// </summary>
        /// <returns>Order实体</returns>
        /// <param name="columns">要返回的列</param>
        public OrderEntity GetOrder(int id)
        {
            return OrderDA.Instance.GetOrder(id);
        }
        public OrderEntity GetOrderByCode(long code)
        {
            OrderEntity _order = OrderDA.Instance.GetOrderByCode(code);

            return _order;
        }

        /// <summary>
        /// 激活订单
        /// </summary>
        /// <returns></returns>
        public int ActivateOrder(int id)
        {
            return OrderDA.Instance.ActivateOrder(id);
        }

        /// <summary>
        /// 设置不可退换货
        /// </summary>
        /// <returns></returns>

        public int SetCanReturnEqualsZero(long ordercode)
        {
            return OrderDA.Instance.SetCanReturnEqualsZero(ordercode);
        } 
        public VWOrderEntity GetVWOrderByCode(Int64 code)
        {
            VWOrderEntity _order = OrderDA.Instance.GetVWOrderByCode(code);
            if (_order != null && _order.Id > 0 && _order.AccepterProvinceId > 0 && _order.AccepterCityId > 0)
            {
                //_order.AccepterProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_order.AccepterProvinceId.ToString()).Name;
                _order.AccepterCityName = GYCityBLL.Instance.GetGYCityByCode(_order.AccepterCityId.ToString()).Name;
                _order.Details = OrderDetailBLL.Instance.GetDetailsByOrderCode(_order.Code, "", -1); 
            }
            return _order;
        }
        public VWOrderEntity GetOrderByMemId(long code, int memid)
        {
            VWOrderEntity _order = OrderDA.Instance.GetVWOrderByCode(code, memid);
            if (_order != null && _order.Id > 0)
            {
                //_order.AccepterProvinceName = GYProvinceBLL.Instance.GetGYProvinceByCode(_order.AccepterProvinceId.ToString()).Name;
                //_order.AccepterCityName = GYProvinceBLL.Instance.GetGYProvinceByCode(_order.AccepterCityId.ToString()).Name;
                 _order.Details = OrderDetailBLL.Instance.GetDetailsByOrderCode(code,"",-1); 
            }
            return _order;
        }
        /// <summary>
        /// 付款完成
        /// </summary>
        /// <param name="ordercode"></param>
        /// <param name="_asset"></param>
        /// <returns></returns>
        public int PayFinished(long ordercode, AssetReChargeEntity _asset)
        {
            return OrderDA.Instance.PayFinished(ordercode, _asset);
        }
        /// <summary>
        /// 付款完成，不添加充值记录
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public int PayFinishedForOrder(long ordercode,decimal amt )
        {
            return OrderDA.Instance.PayFinishedForOrder(ordercode,amt);
        }
        public int PayMethodUpdate(long ordercode,int memid,int paytype)
        {
            return OrderDA.Instance.PayMethodUpdate(ordercode, memid, paytype);
        }
         /// <summary>
         /// 获取需要同步的订单
         /// </summary>
         /// <returns></returns>
         public IList<OrderEntity> GetNeedSyncOrder()
        {
            return OrderDA.Instance.GetNeedSyncOrder( );

        }
        public int UpdateOrderRebackFee(long orderCode,decimal returnFee)
        {
            return OrderDA.Instance.UpdateOrderRebackFee(orderCode, returnFee);
        }
        /// <summary>
        /// 生成订单
        /// </summary>
        /// <param name="products"></param>
        /// <param name="memid"></param>
        /// <returns></returns>
        public string CreateOrder(VWOrderEntity order, OrderAddressEntity _address, OrderBillBasicEntity _billentity)
        {
            Random rd = new Random();
            order.Code = StringUtils.GetDbLong(XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.OrderDayNo) + rd.Next(100, 999).ToString());
            order.OrderVisualCode = order.Code;
            order.CreateTime = DateTime.Now; 
            order.Status = (int)OrderStatus.WaitPay;
            return OrderDA.Instance.CreateOrder(order, _address, _billentity);
        }

        public string CreateOrderList(Dictionary<int, VWOrderEntity> orderlist, OrderAddressEntity _address, OrderBillBasicEntity _billentity)
        {
            Random rd = new Random();
            string vbatchno= XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.OrderDayNo)+rd.Next(1, 9).ToString();
            int suborderi = 0;
            foreach(int orderkey in orderlist.Keys)
            {
                suborderi++;
                VWOrderEntity order = orderlist[orderkey];
                order.Code = StringUtils.GetDbLong(vbatchno + suborderi.ToString().PadLeft(2, '0'));
                order.OrderVisualCode = StringUtils.GetDbLong(vbatchno);  
                order.CreateTime = DateTime.Now; 
                order.Status = (int)OrderStatus.XuQiuSubmit;
                OrderDA.Instance.CreateOrderXuQiu(order, _address, _billentity);
            }
            return vbatchno;
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<OrderEntity> GetOrderList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _keyword = "";
            int _status = 0;
            int _term = 0;
            int _memid = 0;

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
                    }
                }
            }
            return OrderDA.Instance.GetOrderList(pageSize, pageIndex, ref recordCount, _keyword, _status, _term, _memid);
        }
        public IList<VWOrderEntity> GetOrdersByMemIdTop3(int memid)
        {
            IList<VWOrderEntity> list = OrderDA.Instance.GetOrdersByMemIdTop3(memid);
            if (list != null && list.Count > 0)
            {
                foreach (VWOrderEntity _entity in list)
                {
                    IList<VWOrderDetailEntity> _productd = OrderDetailBLL.Instance.GetDetailsByOrderCode(_entity.Code,"",-1);
                    _entity.Details = _productd;
                }
            }
            return list;
        }
       public decimal GetPriceByVisualCode(long visualcode,int memid)
        {
            return OrderDA.Instance.GetPriceByVisualCode(visualcode,memid);
        }
        ///// <summary>
        ///// 获得数据列表
        ///// </summary>
        public IList<VWOrderEntity> GetVWOrderList(int pageSize, int pageIndex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            long _ordercode = 0;
            string _keyword = string.Empty;
            int _status = 0;
            int _term = 0;
            int _memid = 0;
            int _isreturn = -1;
            int _orderstyle = -1;
            long _visualordercode = 0;

            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.OrderCode:
                            {
                                _ordercode = StringUtils.GetDbLong(entity.CompareValue);
                            }
                            break;
                        case SearchFieldName.VisualOrderCode:
                            {
                                _visualordercode = StringUtils.GetDbLong(entity.CompareValue);
                            }
                            break;
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
                        case SearchFieldName.CanReturn:
                            {
                                _isreturn = StringUtils.GetDbInt(entity.CompareValue);
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

            IList<VWOrderEntity> _list = OrderDA.Instance.GetVWOrderList(pageSize, pageIndex, ref recordCount, _keyword, _status, _term, _memid,_isreturn,_ordercode, _visualordercode,_orderstyle);

            if (_list != null && _list.Count > 0)
            {
                foreach (VWOrderEntity _entity in _list)
                {
                    _entity.Details = OrderDetailBLL.Instance.GetDetailsByOrderCode(_entity.Code, "",-1);
                }
            }

            return _list;
        }
    

        public async Task GetOrderAll()
        {
            await Task.Run(() =>
            {
                string _cachekey = "OrderListKey";// SysCacheKey.OrderListKey;
                object obj = MemCache.GetCache(_cachekey);
                if (obj == null)
                {
                    IList<OrderEntity> list = null;
                    list = OrderDA.Instance.GetOrderAll();
                    MemCache.AddCache(_cachekey, list);
                }
            });
        }
        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="dicEnum"></param>
        /// <returns></returns>
        public bool IsExist(OrderEntity order)
        {
            return OrderDA.Instance.ExistNum(order) > 0;
        }

        public IList<OrderEntity> NewGetOrderAll(int pagesize, int pageindex, ref int recordCount)
        {
            return OrderDA.Instance.GetOrderList(pagesize, pageindex, ref recordCount);
        }

        public IList<OrderEntity> GetOrderListByStatus(int pagesize, int pageindex, ref int recordCount, int Status)
        {
            return OrderDA.Instance.GetOrderListByStatus(pagesize, pageindex, ref recordCount, Status);
        }

        public int UpdateOrderByCode(Int64 code, int status,decimal rebackFee=0.00M)
        {
            return OrderDA.Instance.UpdateOrderByCode(code, status,rebackFee);
        }
        public int UpdateOrderByOrderId(int orderid, int status)
        {
            return OrderDA.Instance.UpdateOrderByOrderId(orderid, status);
        }

        //public IList<OrderEntity> GetOrderListByHasComment(int pagesize, int pageindex, ref int recordCount, int HasComment, int Status)
        //{
        //    return OrderDA.Instance.GetOrderListByHasComment(pagesize, pageindex, ref recordCount, HasComment, Status);
        //}

        public IList<VWOrderDetailEntity> GetVWOrderDetailList(int pagesize, int pageindex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            int _memid = 0;
            string _keyword = "";
            int _term = -2;
            int _hascomment = -1;

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
                    case SearchFieldName.OrderTerm:
                        {
                            _term = StringUtils.GetDbInt(entity.CompareValue);
                            break;
                        }
                    case SearchFieldName.HasComment:
                        {
                            _hascomment = StringUtils.GetDbInt(entity.CompareValue);
                            break;
                        }
                }
            }
            return OrderDA.Instance.GetVWOrderDetailList(pagesize, pageindex, ref recordCount, _memid, _keyword, _term, _hascomment);
        }

        public IList<OrderEntity> GetManageOrderList(int pagesize, int pageindex, ref int recordCount, IList<ConditionUnit> wherelist)
        {
            string _ordercode = "";
            int _term = -2;
            int _status = 0;
            if (wherelist != null && wherelist.Count > 0)
            {
                foreach (ConditionUnit entity in wherelist)
                {
                    switch (entity.FieldName)
                    {
                        case SearchFieldName.OrderCode:
                            {
                                _ordercode = StringUtils.GetDbString(entity.CompareValue);
                                break;
                            }
                        case SearchFieldName.OrderTerm:
                            {
                                _term = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }

                        case SearchFieldName.OrderStatus:
                            {
                                _status = StringUtils.GetDbInt(entity.CompareValue);
                                break;
                            }
                    }
                }
            }
            return OrderDA.Instance.GetManageOrderList(pagesize, pageindex, ref recordCount, _ordercode, _term, _status);
        }
    }
}

