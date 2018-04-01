using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Json;
using SuperMarket.Model;
using SuperMarket.BLL;
using SuperMarket.Model.Common;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using System.Threading.Tasks;
using SuperMarket.Core.Util;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.CGOrderDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.Web.CommonControllers;

namespace SuperMarket.SysWeb.Controllers
{
    public class OrderController : BaseMemAdminController
    {

        #region 订单物流信息管理

        /// <summary>
        /// 订单物流信息
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderWLInfo()
        {
            int _id = QueryString.IntSafeQ("orderdetailid", 0);
            long _ordercode = QueryString.LongIntSafeQ("ordercode");
            WLPsOrderDetailEntity _entity = WLPsOrderDetailBLL.Instance.GetWLPsOrderDetailByODId(_id); //依据orderdetailid获取物流信息
            ViewBag.entity = _entity;
            ViewBag.orderdetailid = _id;
            ViewBag.ordercode = _ordercode;
            return View();
        }

        /// <summary>
        /// 订单物流信息提交 
        /// </summary>
        /// <returns></returns>
        public int SubmitOrderWLInfo()
        {

            string _transfercode = FormString.SafeQ("transfercode");
            string _sendmanname = FormString.SafeQ("sendmanname");
            string _sendmanphone = FormString.SafeQ("sendmanphone");
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            int _wlcompany = FormString.IntSafeQ("wlcompany");
            int _hasbill = FormString.IntSafeQ("hasbill");
            int _orderdetailid = FormString.IntSafeQ("orderdetailid");
            int _id = FormString.IntSafeQ("id", 0);

            WLPsOrderDetailEntity _entity = WLPsOrderDetailBLL.Instance.GetWLPsOrderDetail(_id);

            _entity.Id = _id;
            _entity.OrderDetailId = _orderdetailid;
            _entity.HasBill = _hasbill;
            _entity.WLCompanyId = _wlcompany;
            _entity.OrderCode = _ordercode;
            _entity.SendManPhone = _sendmanphone;
            _entity.SendManName = _sendmanname;
            _entity.TransferCode = _transfercode;
            _entity.CreateTime = DateTime.Now;

            int _result = 0;
            if (WLPsOrderDetailBLL.Instance.AddWLPsOrderDetail(_entity) > 0)
            {
                _result = OrderDetailBLL.Instance.SetIsDelivered(_orderdetailid);
            }

            return _result;


        }
        #endregion

        #region 订单管理
        /// <summary>
        /// 订单信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderManage()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _ordercode = QueryString.SafeQ("ordercode");
            int _status = QueryString.IntSafeQ("status", 0);
            int _term = QueryString.IntSafeQ("term", 0);
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderCode, CompareValue = _ordercode });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = _status.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term.ToString() });
            IList<OrderEntity> _entitylist = OrderBLL.Instance.GetManageOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            IList<OrderDetailEntity> _entitylist2 = OrderDetailBLL.Instance.GetOrderDetailWhole();

            string _url = "/Order/OrderManage?ordercode=" + _ordercode + "&status=" + _status + "&term=" + _term;
            string _pageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            ViewBag.orderdetailentitylist = _entitylist2;

            return View();
        }


        /// <summary>
        /// 订单详情管理
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetailManage()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _ordercode = QueryString.SafeQ("ordercode");//需要保存
            string _productname = QueryString.SafeQ("productname");
            string _opration = QueryString.SafeQ("opration");

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderCode, CompareValue = _ordercode.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ProductName, CompareValue = _productname.ToString() });
            IList<OrderDetailEntity> _entitylist = OrderDetailBLL.Instance.GetOrderDetailListOfAdmin(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Order/OrderDetailManage?ordercode=" + _ordercode + "&productname=" + _productname + "&opration=" + _opration;
            string _pageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.ordercode = _ordercode;
            ViewBag.opration = _opration;
            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            return View();

        }

        /// <summary>
        /// 评价管理
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluationManage()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _productname = QueryString.SafeQ("productname");
            int _hascomment = QueryString.IntSafeQ("hascomment", 0);

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ProductName, CompareValue = _productname.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.HasComment, CompareValue = _hascomment.ToString() });
            IList<OrderDetailEntity> _entitylist = OrderDetailBLL.Instance.GetOrderDetailListOfAdmin(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Order/EvaluationManage?productname=" + _productname + "&hascomment=" + _hascomment;
            string _pageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            return View();

        }


        /// <summary>
        /// 已完成订单是否可以退换货审核
        /// </summary>
        /// <returns></returns>
        public int OrderCanReturnCheck()
        {
            int _id = FormString.IntSafeQ("id", 0);
            int _canreturn = FormString.IntSafeQ("canreturn", 0);
            return OrderDetailBLL.Instance.UpdateCanReturnbyId(_id, _canreturn);
        }


        /// <summary>
        /// 收货
        /// </summary>
        /// <returns></returns>
        public string Recepit()
        {
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            int _result = OrderBLL.Instance.UpdateOrderByCode(_ordercode, (int)OrderStatus.Finished);
            if (_result > 0)
            {
                VWOrderEntity _entity = OrderBLL.Instance.GetVWOrderByCode(_ordercode);
                return JsonJC.ObjectToJson(_entity);
            }
            return null;
        }

        /// <summary>
        /// 支付审核
        /// </summary>
        /// <returns></returns>
        public string PayCheck()
        {
            string _ordercode = FormString.SafeQ("ordercode");
            int _orderid = FormString.IntSafeQ("orderid");

            if (OrderBLL.Instance.OrderFinaceSubmit(_ordercode, memid) > 0)
            {
                OrderEntity _entity = OrderBLL.Instance.GetOrder(_orderid);
                return JsonJC.ObjectToJson(_entity);
            }
            return null;

        }

        /// <summary>
        /// 取消订单确认通过
        /// </summary>
        /// <returns></returns>
        public string CancelOrderCheckPass()
        {
            ResultObj result = new ResultObj();
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            OrderEntity _order = OrderBLL.Instance.GetOrderByCode(_ordercode);
            result.Status = (int)CommonStatus.Fail;
            if (_order.Status == (int)OrderStatus.CancelApp)
            {
                if (CGOrderBLL.Instance.GetCGOrderNumByB2BCode(_ordercode) > 0)
                {
                    if (CGOrderBLL.Instance.CGOrderCancel(_ordercode) > 0)
                    {
                        if (OrderBLL.Instance.OrderCancelCheckPass(_ordercode, _order.TimeStampCode) > 0)
                        {
                            string productdetails = "";
                            IList<OrderDetailEntity> _listproduct = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, _ordercode, false);
                            foreach (OrderDetailEntity _entity in _listproduct)
                            {
                                productdetails += "|" + _entity.ProductDetailId.ToString() + "_" + _entity.Num.ToString();
                            }
                            if (productdetails != "")
                            {
                                productdetails = productdetails.TrimStart('|');
                                ProductDetailBLL.Instance.ReleaseStock(productdetails);
                            }
                            result.Status = (int)CommonStatus.Success;
                        }
                    }
                }
                else
                {
                    if (OrderBLL.Instance.OrderCancelCheckPass(_ordercode, _order.TimeStampCode) > 0)
                    {
                        result.Status = (int)CommonStatus.Success;
                    }
                }
               

            } 
            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 取消订单确认通过
        /// </summary>
        /// <returns></returns>
        public string CancelOrderCheckReject()
        {
            ResultObj result = new ResultObj();
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            OrderEntity _order = OrderBLL.Instance.GetOrderByCode(_ordercode);
            result.Status = (int)CommonStatus.Fail;
            if (_order.Status == (int)OrderStatus.CancelApp)
            {
                 if (OrderBLL.Instance.OrderCancelCheckReject(_ordercode, _order.TimeStampCode) > 0)
                        {
                            result.Status = (int)CommonStatus.Success;
                        } 

            }
            return JsonJC.ObjectToJson(result);
        }


        public string AddToLineConfirm()
        {
            ResultObj result = new ResultObj();
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            if (OrderPayLineConfirmBLL.Instance.IsExist(_ordercode, (int)PayLineConfirm.Default))
            {
                result.Status = (int)CommonStatus.HasAddToLineConfirm;
            }
            else
            {
                OrderPayLineConfirmEntity confirmentity = new OrderPayLineConfirmEntity();
                confirmentity.OrderCode = _ordercode;
                confirmentity.Reason = "待付款订单线下确认";
                confirmentity.CreateManId = memid;
                confirmentity.Status = (int)PayLineConfirm.Default;
                if (OrderPayLineConfirmBLL.Instance.AddOrderPayLineConfirm(confirmentity) > 0)
                {

                    result.Status = (int)CommonStatus.Success;
                }
                else
                {
                    result.Status = (int)CommonStatus.Fail;
                }
            }
            return JsonJC.ObjectToJson(result);

        }
        #endregion

        #region 退换货管理
        /// <summary>
        /// 退换货订单审核
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnOrderCheck()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            long _ordercode = QueryString.LongIntSafeQ("code");
            int _returnType = QueryString.IntSafeQ("returntype", -1);
            int _status = QueryString.IntSafeQ("status", -1);

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>(); 
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _ordercode.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ReturnType, CompareValue = _returnType.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ReturnOrderStatus, CompareValue = _status.ToString() });
            IList<ReturnXSEntity> _entitylist = ReturnXSBLL.Instance.GetReturnXSList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Order/ReturnOrderCheck/?code=" + _ordercode + "&returntype=" + _returnType + "&status=" + _status;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);

            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            return View();
        }


        /// <summary>
        /// 退换货订单审核通过
        /// </summary>
        /// <returns></returns>
        public int AdoptReturnApply()
        {
            int _id = FormString.IntSafeQ("returnId");
            string _accepterName = FormString.SafeQ("accepterName");
            string _accepterPhone = FormString.SafeQ("accepterPhone");
            int _result = ReturnXSBLL.Instance.UpdateReturnXSStatus(_id, (int)ReturnOrderStatus.Adopt);

            if (_result > 0)
            {
                ReturnXSWLEntity entity = new ReturnXSWLEntity();
                entity.AccepterName = _accepterName;
                entity.AccepterPhone = _accepterPhone;
                entity.ReturnId = _id;
                entity.WLTime = DateTime.Now.AddDays(30);
                return ReturnXSWLBLL.Instance.AddReturnXSWL(entity);

            }

            return _result;

        }


        /// <summary>
        /// 添加退换货物流信息
        /// </summary>
        /// <returns></returns>
        public int AddReturnXSWLInfo()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            //string _AahamaName = FormString.SafeQ("AahamaName");
            //string _AahamaPhone = FormString.SafeQ("AahamaPhone");
            //int _Province = FormString.IntSafeQ("Province");
            //int _City = FormString.IntSafeQ("City");
            //string _AahamaAddress = FormString.SafeQ("AahamaAddress");
            //string _AahamaPost = FormString.SafeQ("AahamaPost");
            //string _Remark = FormString.SafeQ("Remark");

            //ReturnXSWLEntity entity = new ReturnXSWLEntity();
            //entity.AahamaName = _AahamaName;
            //entity.AahamaPhone = _AahamaPhone;
            //entity.AahamaProvince = _Province;
            //entity.AahamaCity = _City;
            //entity.AahamaAddress = _AahamaAddress;
            //entity.AahamaPost = _AahamaPost;
            //entity.Remark = _Remark;
            //entity.WLTime = DateTime.Now.AddDays(15);
            //entity.ReturnId = _returnId;

            int _result1 = 0;
            int _result2 = 0;

            //if ((_result1 = ReturnXSWLBLL.Instance.AddReturnXSWL(entity)) > 0)
            //{
            _result2 = ReturnXSBLL.Instance.UpdateReturnXSStatus(_returnId, (int)ReturnOrderStatus.Adopt);
                if (_result2 > 0)
                {
                    return _result2;
                }
                //else
                //{
                //    return ReturnXSWLBLL.Instance.DeleteReturnXSWLByKey(_result1);
                //}

            //}

            return _result2;


        }


        public int AcceptReturnXSApp()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            int _result2 = 0;
            _result2 = ReturnXSBLL.Instance.UpdateReturnXSStatus(_returnId, (int)ReturnOrderStatus.Adopt);
            if (_result2 > 0)
            {
                return _result2;
            } 

            return _result2;
        }
        /// <summary>
        /// 编辑上门取件的退换货信息
        /// </summary>
        /// <returns></returns>
        public int AuditReturnInfoOfPickUp()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            string _accepterName = FormString.SafeQ("accepterName");
            string _accepterPhone = FormString.SafeQ("accepterPhone");

            return ReturnXSWLBLL.Instance.UpdateReturnXSWLOfPickUp(_returnId, _accepterName, _accepterPhone);
        }

        /// <summary>
        /// 编辑邮寄快递的退换货信息
        /// </summary>
        /// <returns></returns>
        public int AuditReturnInfoOfExpress()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            string _WLCode = FormString.SafeQ("WLCode");
            string _WLComName = FormString.SafeQ("WLComName");

            return ReturnXSWLBLL.Instance.UpdateReturnXSWLOfExpress(_returnId, _WLCode, _WLComName);
        }

        /// <summary>
        /// 添加退款信息
        /// </summary>
        /// <returns></returns>
        public int AddFinanceRefundInfo()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            if (FinanceRefundBLL.Instance.AddFinanceRefund(_returnId) > 0)
            {
                 return ReturnXSBLL.Instance.UpdateReturnXSStatus(_returnId, (int)ReturnOrderStatus.WaitForRefund);
            }
            return 0;
        }

        /// <summary>
        /// 更新退换货状态
        /// </summary>
        /// <returns></returns>
        public int UpdateReturnXSStatus()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            return ReturnXSBLL.Instance.UpdateReturnXSStatus(_returnId, (int)ReturnOrderStatus.WaitForDelivery);
        }


        /// <summary>
        /// 保存新订单号
        /// </summary>
        /// <returns></returns>
        public int SaveNewOrderCode()
        {
            int _returnId = FormString.IntSafeQ("returnId");
            long _orderCode = ReturnXSBLL.Instance.GetReturnXS(_returnId, -1).ReturnOrderCode;
            if (OrderCreateLogBLL.Instance.IsExist(_orderCode))
            {
                string _newCode = FormString.SafeQ("newCode");
                return ReturnXSBLL.Instance.UpdateReturnXSNewOrderCode(_returnId, _newCode, (int)ReturnOrderStatus.HadDeliveried);
            }

            return 0;

        }

        /// <summary>
        /// 退换货订单审核拒绝
        /// </summary>
        /// <returns></returns>
        public int RefuseReturnApply()
        {
            int _id = FormString.IntSafeQ("id");
            string _rejectreason = FormString.SafeQ("rejectreason", 1000);

            ReturnXSEntity _entity = ReturnXSBLL.Instance.GetReturnXS(_id, -1);
            _entity.CheckManId = memid.ToString();
            _entity.CheckTime = DateTime.Now;
            _entity.Status = (int)ReturnOrderStatus.Reject;
            _entity.RejectReason = _rejectreason;

            return ReturnXSBLL.Instance.UpdateReturnXS(_entity);

        }
        #endregion


        /// <summary>
        /// 订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetails()
        {
            long _ordercode = QueryString.LongIntSafeQ("ordercode");
            int _orderDetailId = QueryString.IntSafeQ("orderDetailId");
            VWOrderEntity _entity = OrderBLL.Instance.GetVWOrderByCode(_ordercode);
            IList<OrderDetailEntity> _detailEntitys = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(_entity.MemId,_ordercode,false);
            OrderBillBasicEntity _billEntity = OrderBillBasicBLL.Instance.GetBillBasicByOrderCode(_ordercode);
            VWMemberEntity mem= BLL.MemberDB.MemberBLL.Instance.GetVWMember(_entity.MemId);
            string _status = EnumShow.Instance.GetEnumDes((OrderStatus)_entity.Status);
            ViewBag.entity = _entity;
            ViewBag.VWMem = mem;
            ViewBag.detailEntity = _detailEntitys;
            ViewBag.status = _status;
            ViewBag.billEntity = _billEntity;
            return View();
        }

        public ActionResult OrderCreate()
        { 
            int _pagesize = CommonKey.PageSizeOrder;
            int record = 0;
            int index = QueryString.IntSafeQ("pageindex");
            if (index == 0) index = 1;
            long oldcode = QueryString.IntSafeQ("oldcode");
            IList<OrderCreateLogEntity> list = new List<OrderCreateLogEntity>();
            list = OrderCreateLogBLL.Instance.GetOrderCreateLogList(_pagesize, index, ref record, oldcode);
            ViewBag.CreateList = list;
            string _url = "/Order/OrderCreate";
            string _pageStr = HTMLPage.SetProductListPage(record, _pagesize, index, _url);
            ViewBag.PageStr = _pageStr;
            return View();
        }
        public string CreateOrderByCart()
        {
            ResultObj result = new ResultObj();
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            long _cartcode = FormString.LongIntSafeQ("cartcode");

            OrderAddressEntity _address = new OrderAddressEntity();
            _address = OrderAddressBLL.Instance.GetOrderAddressByOrderCode(_ordercode);
            OrderBillBasicEntity _billentity = new OrderBillBasicEntity();
            _billentity = OrderBillBasicBLL.Instance.GetBillBasicByOrderCode(_ordercode);
            VWOrderEntity _vworder = OrderDetailPreTempBLL.Instance.GetVWOrderByTempCode(_cartcode);
            OrderEntity _orderentity = OrderBLL.Instance.GetOrderByCode(_ordercode);
            if (_address != null && _address.Id > 0 && _billentity != null && _billentity.Id > 0 && _vworder != null && _vworder.ActPrice > 0)
            {
                _vworder.MemId = _orderentity.MemId;
                _vworder.MemLevel = _orderentity.MemLevel;
                _vworder.IsStore = _orderentity.IsStore;
                _vworder.Integral = 0;
                _vworder.IntegralFee = 0;
                string newordercode = OrderBLL.Instance.CreateOrder(_vworder, _address, _billentity);
                OrderCreateLogEntity log = new OrderCreateLogEntity();
                log.OldOrderCode = _ordercode;
                log.PreTempCode = _cartcode;
                log.CreateManId = memid;
                log.NewOrderCode = StringUtils.GetDbLong(newordercode);
                OrderCreateLogBLL.Instance.AddOrderCreateLog(log);
                result.Status = (int)SuperMarket.Model.CommonStatus.Success;
            }
            else
            {
                result.Status = (int)SuperMarket.Model.CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(result);

        }
        /// <summary>
        /// 评价详情
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluationDetail()
        {
            int _orderdetailid = QueryString.IntSafeQ("orderdetailid", 0);
            CommentEntity _entity = CommentBLL.Instance.GetCommentByOrderDetailId(_orderdetailid);
            ViewBag.entity = _entity;
            return View();
        }

        /// <summary>
        /// 评价审核通过
        /// </summary>
        /// <returns></returns>
        public int AdoptEvaluation()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = CommentBLL.Instance.UpdateCommentStatus(_id, (int)CommentStatus.AuditPublished);
            return _result;

        }

        /// <summary>
        /// 评价审核拒绝
        /// </summary>
        /// <returns></returns>
        public int RejectEvaluation()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = CommentBLL.Instance.UpdateCommentStatus(_id, (int)CommentStatus.AuditNotPublished);
            return _result;

        }


        /// <summary>
        /// 激活订单为可换货状态
        /// </summary>
        /// <returns></returns>
        public int ActivateOrder()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = OrderBLL.Instance.ActivateOrder(_id);
            return _result;
        }

        /// <summary>
        /// 分配收货供应商
        /// </summary>
        /// <returns></returns>
        public ActionResult SuppliersReceiving()
        {
            int _returnXSId = QueryString.IntSafeQ("returnXSId");
            ReturnXSEntity entity = ReturnXSBLL.Instance.GetReturnXS(_returnXSId, -1);

            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            if (_pageindex == 0) _pageindex = 1;
            int _recordCount = 0;

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.B2BOrderCode, CompareValue = entity.ReturnOrderCode.ToString() });//订单编号
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ProductId, CompareValue = entity.ProductId.ToString() });//产品编号

            IList<VWCGOrderReturnEntity> _entitylist = CGOrderTakeBLL.Instance.GetCGOrderSuppliers(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Order/SuppliersReceiving/?returnXSId=" + _returnXSId;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.ReturnXSId = _returnXSId;
            ViewBag.ReturnType = entity.ReturnType;
            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            return View();


        }
        /// <summary>
        /// 通知供应商收货
        /// </summary>
        /// <returns></returns>
        public string  NoteMsgToCGMem()
        {
            ResultObj result = new ResultObj();
            int returnId = FormString.IntSafeQ("returnId");
            if(!ReturnXSBLL.Instance.ReturnNumEqDist(returnId))
            {
                result.Status = (int)CommonStatus.RebackNumNoValid;
            }
            else
            {
                if (ReturnXSBLL.Instance.UpdateReturnXSStatus(returnId, (int)ReturnOrderStatus.Returning, (int)ReturnOrderStatus.Adopt) > 0)
                {
                    //采购系统接口，
                    if (CGReturnBLL.Instance.NoteMsgToCGMems(returnId) > 0)
                    { 
                        result.Status = (int)CommonStatus.Success;
                    }
                }
                else
                {
                    ReturnXSEntity en = ReturnXSBLL.Instance.GetReturnXS(returnId, -1);
                    if(en.Status==(int)ReturnOrderStatus.Returning)
                    {
                        //采购系统接口，
                        if (CGReturnBLL.Instance.NoteMsgToCGMems(returnId) > 0)
                        {
                            result.Status = (int)CommonStatus.Success;
                        }
                    }
                    else
                    {
                        result.Status = (int)CommonStatus.Fail; 
                    }
                }
            } 
            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 添加退换货信息
        /// </summary>
        /// <returns></returns>
        public int AddCGReturnInfo()
        {
            int _CGMemId = FormString.IntSafeQ("CGMemId");
            int _ReturnXSId = FormString.IntSafeQ("ReturnXSId");
            int _returnNum = FormString.IntSafeQ("ReturnNum");

            CGReturnEntity entity = new CGReturnEntity();
            entity.CGMemId = _CGMemId;
            entity.B2BReturnXSId = _ReturnXSId ;
            entity.ReturnNum = _returnNum;
            entity.Status = 0;

            return CGReturnBLL.Instance.AddCGReturn(entity);
        }



    }
}