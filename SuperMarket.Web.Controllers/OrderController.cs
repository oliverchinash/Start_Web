using SuperMarket.BLL;
using SuperMarket.BLL.CGOrderDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.PayDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Pay;
using SuperMarket.Pay.WeiXin;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class OrderController : BaseMemberController
    {

        #region  订单管理

        /// <summary>
        /// 预订单
        /// </summary>
        /// <returns></returns>
        public ActionResult PreOrder()
        { 
            long _preordercode = QueryString.LongIntSafeQ("code");
            int _jishi = QueryString.IntSafeQ("js");
            if (_jishi == (int)JiShiSongEnum.JiShi)
            {
                XuQiuPreOrderMethod(_preordercode);
            }
            else
            {
                ///可正常销售的产品
                IList<OrderDetailPreTempEntity> _listproduct = OrderDetailPreTempBLL.Instance.GetOrderPreTempByCode(_preordercode, 1);

                //不能正常销售的产品
                IList<OrderDetailPreTempEntity> _listproductless = OrderDetailPreTempBLL.Instance.GetOrderPreTempByCode(_preordercode, 0);
                ViewBag.ProductList = _listproduct;
                ViewBag.ProductListLess = _listproductless;
                ViewBag.MemId = member.MemId;
                ViewBag.PreOrderCode = _preordercode;
                VWOrderEntity _order = OrderCommonBLL.Instance.GetTransFeeDisCount(_listproduct);
                ViewBag.Order = _order;

                IntegralEntity _entity = IntegralBLL.Instance.GetIntegralByMemId(memid);
                ViewBag.Integral = _entity;
            }
            ViewBag.JiShiSong = _jishi;
            return View();
        }
        /// <summary>
        /// 预订单
        /// </summary>
        /// <returns></returns>
        public ActionResult PreOrderXuQiu( )
        {
            long _preordercode = QueryString.LongIntSafeQ("code");
            XuQiuPreOrderMethod(_preordercode);
            //ViewBag.Order = _order;
            return View();
        }

        public void XuQiuPreOrderMethod(long _preordercode)
        {
            Dictionary<int, VWOrderEntity> listsdic = new Dictionary<int, VWOrderEntity>();//产品字典
            IList<VWOrderDetailEntity> _listproductless = new List<VWOrderDetailEntity>();//下架产品字典
            decimal AmountAll = 0;
            decimal AmountTransFeeAll = 0;
            decimal AmountDiscountAll = 0;
            ///可正常销售的产品
            IList<VWOrderDetailEntity> _listproduct = OrderDetailPreTempBLL.Instance.GetOrderDetailsByCode(_preordercode);
            if (_listproduct != null && _listproduct.Count > 0)
            {
                foreach (VWOrderDetailEntity entity in _listproduct)
                {
                    if (entity.Status > 0)
                    {
                        if (!listsdic.ContainsKey(entity.CGMemId))
                        {
                            listsdic[entity.CGMemId] = new VWOrderEntity();
                        }
                        listsdic[entity.CGMemId].Details.Add(entity);
                    }
                    else
                    {
                        _listproductless.Add(entity);
                    }
                }
                if (listsdic.Keys.Count > 0)
                {
                    foreach (int okey in listsdic.Keys)
                    {
                        listsdic[okey].CGMemId = okey;
                        listsdic[okey].CGMemNickName = MemberInfoBLL.Instance.GetNickNameByMemId(okey);
                        OrderCommonBLL.Instance.GetTransFeeForOrder(listsdic[okey]);
                        AmountAll += listsdic[okey].PreDisCountPrice;
                        AmountTransFeeAll += listsdic[okey].TransFee;
                        AmountDiscountAll += listsdic[okey].DisCountFee;
                    }
                }
            }
            //IList<PostAddressEntity> _listaddress = PostAddressBLL.Instance.GetPostListByMemId(memid);
            ViewBag.ProductListDic = listsdic;
            ViewBag.ProductListLess = _listproductless;
            //ViewBag.AddressList = _listaddress;
            ViewBag.MemId = member.MemId;
            ViewBag.PreOrderCode = _preordercode;
            ViewBag.AmountAll = AmountAll;
            ViewBag.AmountTransFeeAll = AmountTransFeeAll;
            ViewBag.AmountDiscountAll = AmountDiscountAll;
        }

        public ActionResult XuQiuNotice()
        {
            long ordecode = QueryString.LongIntSafeQ("code");
            decimal _amount = OrderBLL.Instance.GetPriceByVisualCode(ordecode,memid);

            ViewBag.VisualOrderCode = ordecode; 
            ViewBag.Amount  = _amount; 
            return View();
        }
        public ActionResult PaySel()
        {
            long  code = QueryString.LongIntSafeQ("code");
            OrderEntity order = OrderBLL.Instance.GetOrderByCode(code); 
            ViewBag.Order = order;
            return View();
        }
        public string PaySelectSubmit()
        {
            ResultObj _result = new ResultObj();
            long _syscode = FormString.LongIntSafeQ("syscode"); 
            int _paytype = FormString.IntSafeQ("paytype");
            int _systype = FormString.IntSafeQ("systype");
            if (_systype == 0) _systype = (int)SystemType.B2B;
            OrderEntity order = OrderBLL.Instance.GetOrderByCode(_syscode);
            string payconfirm = "";
            if (_paytype == (int)PayType.OutLine)
            {
                payconfirm = StringUtils.GetRandomString(12);
            }
            PayOrderEntity payen = PayOrderBLL.Instance.GetPayOrderBySysCode(_systype, _syscode.ToString(), _paytype);
            if (payen.Id > 0 && payen.Status == 1)
            {
                _result.Status = (int)CommonStatus.HasPayed;
                _result.Obj = payen;
            }
            else 
            {
                payen = PayOrderBLL.Instance.CreatePayOrder(_systype, _syscode.ToString(), order.ActPrice, _paytype, memid, payconfirm);
                _result.Status = (int)CommonStatus.Success;
                _result.Obj = payen;
            } 
            return JsonJC.ObjectToJson(_result);
        }
        /// <summary>
        /// 生成预订单
        /// </summary>
        /// <returns></returns>
        public string CreatePreOrder()
        {
            string _productdetails = FormString.SafeQ("productdetails",8000);

            string randcode = OrderDetailPreTempBLL.Instance.CreateOrderPre(_productdetails,member.IsStore, member.Status, member.MemGrade,member.StoreType);

            return randcode.ToString();
        }

        /// <summary>
        /// 订单提交
        /// </summary>
        /// <returns></returns>
        public string OrderSubmit()
        {
            return "";
        }

        /// <summary>
        /// 生成真实订单
        /// </summary>
        /// <returns></returns>
        public string CreateOrder()
        {
            ResultObj _result = new ResultObj();
            int _resultstatus = (int)CommonStatus.Fail;
            long _preordercode = FormString.LongIntSafeQ("preordercode");
            int _addressid = FormString.IntSafeQ("addressid");
            //int _paytype = FormString.IntSafeQ("paytype");
            int _systype = FormString.IntSafeQ("systype");
            if (_systype == 0) _systype = (int)SystemType.B2B;
            string _remark = FormString.SafeQ("remark");
            string acceptername = FormString.SafeQ("acceptername");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            string address = FormString.SafeQ("address", 500);
            string mobilephone = FormString.SafeQ("mobilephone");
            int jifen = FormString.IntSafeQ("jifen"); 
            int memcouponid = FormString.IntSafeQ("memcouponid");
            int expressid = FormString.IntSafeQ("expressid");   
            int ordertype = FormString.IntSafeQ("ordertype",-1);
            if (jifen % 100 != 0) jifen = jifen / 100 * 100;
            if (jifen > 0 && !AssetBLL.Instance.CheckIntegralEnough(memid, jifen))
            {
                jifen = 0;
            }
            int billtype = FormString.IntSafeQ("billtype");
            OrderBillBasicEntity _billentity = new OrderBillBasicEntity();

            _billentity.BillType = billtype;
            if (billtype == (int)BillType.Normal)
            {
                string title = FormString.SafeQ("billtitle", 200);
                _billentity.CompanyName = title;
            }
            else if (billtype == (int)BillType.VAT)
            {
                _billentity.BillId = FormString.IntSafeQ("billvatid");

                MemBillVATEntity _mementity = MemBillVATBLL.Instance.GetMemBillVAT(memid);
                //if (_mementity.Status != 1)
                //{
                //    resultstatus = (int)CommonStatus.BillVATNoCheck;
                //    _result.Status = resultstatus;
                //    return JsonJC.ObjectToJson(_result);
                //}
                _billentity.ReceiverName = FormString.SafeQ("billvatrename");
                _billentity.ReceiverPhone = FormString.SafeQ("billvatrephone");
                _billentity.ReceiverProvince = FormString.IntSafeQ("billvatreprovince");
                _billentity.ReceiverCity = FormString.IntSafeQ("billvatrecity");
                _billentity.ReceiverAddress = FormString.SafeQ("billvatreaddress", 300);
                _billentity.CompanyName = _mementity.CompanyName;
                _billentity.CompanyPhone = _mementity.CompanyPhone;
                _billentity.CompanyCode = _mementity.CompanyCode;
                _billentity.CompanyBank = _mementity.CompanyBank;
                _billentity.CompanyAddress = _mementity.CompanyAddress;
                _billentity.BankAccount = _mementity.BankAccount;
                _billentity.Status = _mementity.Status;
            }


            VWOrderEntity _vworder = OrderDetailPreTempBLL.Instance.GetVWOrderByTempCode(_preordercode);
            if (ordertype != -1) _vworder.OrderType = ordertype;
            _vworder.OrderStyle = (int)OrderStyleEnum.Normal;


            _vworder.DisCountFee = _vworder.DisCountFee;
            decimal tempprice = _vworder.PreDisCountPrice - _vworder.DisCountFee;
            if (tempprice>1)
            {
                decimal jifenamt = OrderCommonBLL.Instance.GetJiFenAmt(jifen);
                _vworder.Integral = jifen;
                _vworder.IntegralFee = jifenamt;
                tempprice = tempprice - jifenamt;
            }
            else
            {
               _vworder.Integral = 0;
               _vworder.IntegralFee = 0; 
            }
            if(memcouponid>0)
            {
                MemCouponsEntity couponen = MemCouponsBLL.Instance.GetCouponByMemCouponId(memid,memcouponid);
                if(couponen!=null&& couponen.Id== memcouponid&& couponen.EndTime>DateTime.Now)
                {
                    DicCouponsEntity dicen = couponen.DicCoupons;
                    if (dicen.CouponType ==(int)CouponTypeEnum.Money&& dicen.MinimumReqAmount< tempprice)
                    {
                        _vworder.MemCouponsId = memcouponid;
                        _vworder.CouponsFee = dicen.CouponValue;
                        tempprice = tempprice - dicen.CouponValue;
                    }
                }
            }
            _vworder.ActPrice = tempprice; 
            //_vworder.PayType = _paytype;
            _vworder.ExpressCom = expressid;
            
            _vworder.Remark = _remark;
            _vworder.MemId = memid;
            _vworder.MemLevel = member.MemGrade;
            _vworder.IsStore = member.IsStore; 
            //if (_paytype == (int)PayType.OutLine)
            //{
            //    _vworder.PayConfirmCode = StringUtils.GetRandomString(12);
            //}
            //if (billtype == 1)
            //{
            //    _vworder.BillType = (int)BillType.Normal;
            //}
            //else if (billtype == 2)
            //{
            //    _vworder.BillType = (int)BillType.VAT;
            //}
            OrderAddressEntity _address = new OrderAddressEntity();
            _address.CityId = city;
            _address.AccepterName = acceptername;
            _address.ProvinceId = province;
            _address.Address = address;
            _address.MobilePhone = mobilephone;
            //_vworder.AcceptAddress = _address;
            if (_vworder.ActPrice >= 1)
            {
                List<int> listpdids = new List<int>();
                string productdetails = "";
                if (_vworder != null && _vworder.Details != null && _vworder.Details.Count > 0)
                {
                    foreach (VWOrderDetailEntity ordetailentity in _vworder.Details)
                    { 
                        listpdids.Add(ordetailentity.ProductDetailId);
                        productdetails += "|" + ordetailentity.ProductDetailId.ToString() + "_" + ordetailentity.Num.ToString();
                    }
                    if (productdetails != "")
                    {
                        productdetails = productdetails.TrimStart('|');
                        if (ProductStyleBLL.Instance.ProductsEnough(productdetails))
                        {
                            string ordercode = OrderBLL.Instance.CreateOrder(_vworder, _address, _billentity);
                            if (!string.IsNullOrEmpty(ordercode))
                            {
                                //IList<OrderDetailEntity> _listproduct = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, StringUtils.GetDbLong(ordercode), false);
                                //foreach (OrderDetailEntity _entity in _listproduct)
                                //{
                                //    productdetails += "|" + _entity.ProductDetailId.ToString() + "_" + _entity.Num.ToString();
                                //}
                                if (productdetails != "")
                                {
                                    if (ProductStyleBLL.Instance.ProductsToOrder(productdetails) > 0)
                                    {
                                        VWShoppingCartInfo ShoppingCartentity = ShoppingCartProcessor.GetShoppingCart();
                                        ShoppingCartProcessor.RemoveCartItems(ShoppingCartentity, listpdids);
                                        //if (_vworder.PayType == (int)PayType.WeChat)
                                        //{
                                             _result.Obj = ordercode;
                                        //}
                                        //else
                                        //{
                                            
                                        //    _result.Obj = ordercode;
                                        //}
                                        _result.Status = (int)CommonStatus.Success;

                                        return JsonJC.ObjectToJson(_result);
                                    }
                                    else
                                    {
                                        _result.Status = (int)CommonStatus.ProductLess;
                                        _result.Obj = "";
                                        return JsonJC.ObjectToJson(_result);
                                    }
                                }
                                else
                                {
                                    _result.Status = (int)CommonStatus.Success;
                                    _result.Obj = ordercode;
                                    return JsonJC.ObjectToJson(_result);
                                }
                            }
                        }
                        else
                        {
                            _result.Status = (int)CommonStatus.ProductLess;
                            _result.Obj = "";
                            return JsonJC.ObjectToJson(_result);
                        }
                    }
                }
               
                 
            }
            _result.Status = (int)CommonStatus.Fail;
            _result.Obj = "";
            return JsonJC.ObjectToJson(_result);

        }
      
        /// <summary>
        /// 生成真实订单
        /// </summary>
        /// <returns></returns>
        public string CreateOrderXuQiu()
        {
            ResultObj _result = new ResultObj();
            int _resultstatus = (int)CommonStatus.Fail;
            long _preordercode = FormString.LongIntSafeQ("preordercode");
            int _addressid = FormString.IntSafeQ("addressid");
            int _paytype = FormString.IntSafeQ("paytype");
            string _remark = FormString.SafeQ("remark", 80000);
            string acceptername = FormString.SafeQ("acceptername");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            string address = FormString.SafeQ("address", 500);
            string mobilephone = FormString.SafeQ("mobilephone");
            int jifen = FormString.IntSafeQ("jifen");
            int memcouponid = FormString.IntSafeQ("memcouponid");//折扣券Id
            int expressid = FormString.IntSafeQ("expressid");
            int ordertype = FormString.IntSafeQ("ordertype", -1);
            if (jifen % 100 != 0) jifen = jifen / 100 * 100;
            if (jifen > 0 && !AssetBLL.Instance.CheckIntegralEnough(memid, jifen))
            {
                jifen = 0;
            }
            int billtype = FormString.IntSafeQ("billtype");
            OrderBillBasicEntity _billentity = new OrderBillBasicEntity();

            _billentity.BillType = billtype;
            if (billtype == (int)BillType.Normal)
            {
                string title = FormString.SafeQ("billtitle", 200);
                _billentity.CompanyName = title;
            }
            else if (billtype == (int)BillType.VAT)
            {
                _billentity.BillId = FormString.IntSafeQ("billvatid");

                MemBillVATEntity _mementity = MemBillVATBLL.Instance.GetMemBillVAT(memid); 
                _billentity.ReceiverName = FormString.SafeQ("billvatrename");
                _billentity.ReceiverPhone = FormString.SafeQ("billvatrephone");
                _billentity.ReceiverProvince = FormString.IntSafeQ("billvatreprovince");
                _billentity.ReceiverCity = FormString.IntSafeQ("billvatrecity");
                _billentity.ReceiverAddress = FormString.SafeQ("billvatreaddress", 300);
                _billentity.CompanyName = _mementity.CompanyName;
                _billentity.CompanyPhone = _mementity.CompanyPhone;
                _billentity.CompanyCode = _mementity.CompanyCode;
                _billentity.CompanyBank = _mementity.CompanyBank;
                _billentity.CompanyAddress = _mementity.CompanyAddress;
                _billentity.BankAccount = _mementity.BankAccount;
                _billentity.Status = _mementity.Status;
            }
            OrderAddressEntity _address = new OrderAddressEntity();
            _address.CityId = city;
            _address.AccepterName = acceptername;
            _address.ProvinceId = province;
            _address.Address = address;
            _address.MobilePhone = mobilephone;

            Dictionary<int, VWOrderEntity> _vworderdic = OrderDetailPreTempBLL.Instance.GetVWOrdersByTempCode(_preordercode);

            List<int> listpdids = new List<int>();
            string productdetails = "";
            if (_vworderdic.Keys.Count > 0)
            {
                IList<VWOrderRemarkEntity> remarklist = new List<VWOrderRemarkEntity>();
                if (!string.IsNullOrEmpty(_remark))
                {
                    remarklist = JsonJC.JsonToObject<List<VWOrderRemarkEntity>>(_remark.Replace("＂", "\""));
                }
                Dictionary<int, string> remarkdic = new Dictionary<int, string>();
                foreach (VWOrderRemarkEntity reenti in remarklist)
                {
                    remarkdic.Add(StringUtils.GetDbInt(reenti.CGMemId), reenti.Remark);
                }
                foreach (int okey in _vworderdic.Keys)
                {

                    if (okey > 0)
                    {
                        foreach (VWOrderDetailEntity oden in _vworderdic[okey].Details)
                        {
                            listpdids.Add(oden.ProductDetailId);
                            productdetails += "|" + oden.ProductDetailId.ToString() + "_" + oden.Num.ToString();
                        }
                        productdetails = productdetails.TrimStart('|');
                        OrderCommonBLL.Instance.GetTransFeeForOrder(_vworderdic[okey]);
                        _vworderdic[okey].CGMemId = okey;
                        _vworderdic[okey].PreOrderCode = _preordercode;
                        _vworderdic[okey].OrderType = (int)OrderType.OnLine;
                        _vworderdic[okey].NeedDeliver = 1;//需要发货
                        _vworderdic[okey].PayPrice = 0;//支付价格0
                        _vworderdic[okey].PayType = _paytype;
                        _vworderdic[okey].ExpressCom = 0;//普通配送
                        _vworderdic[okey].MemId = memid;
                        _vworderdic[okey].MemLevel = member.MemGrade;
                        _vworderdic[okey].IsStore = member.IsStore; 
                        _vworderdic[okey].OrderStyle = (int)OrderStyleEnum.XuQiu;
                        if (remarkdic.ContainsKey(okey))
                        {
                            _vworderdic[okey].Remark = remarkdic[okey];
                        }
                    }
                    else
                    {
                        foreach (VWOrderDetailEntity oden in _vworderdic[okey].Details)
                        {
                            listpdids.Add(oden.ProductDetailId);
                        }
                    }
                }
                if (ProductStyleBLL.Instance.ProductsEnough(productdetails))
                {
                    string ordercode = OrderBLL.Instance.CreateOrderList(_vworderdic, _address, _billentity);
                    if (!string.IsNullOrEmpty(ordercode))
                    {
                        if (productdetails != "")
                        {
                            if (ProductStyleBLL.Instance.ProductsToOrder(productdetails) > 0)
                            {
                                VWShoppingCartInfo ShoppingCartentity = ShoppingXuQiuProcessor.GetShoppingXuQiu();
                                ShoppingXuQiuProcessor.RemoveCartXuQiuItems(ShoppingCartentity, listpdids);
                                _result.Status = (int)CommonStatus.Success;
                                _result.Obj = ordercode;
                                return JsonJC.ObjectToJson(_result);
                            }
                            else
                            {
                                _result.Status = (int)CommonStatus.ProductLess;
                                _result.Obj = "";
                                return JsonJC.ObjectToJson(_result);
                            }
                        }
                        else
                        {
                            _result.Status = (int)CommonStatus.Success;
                            _result.Obj = ordercode;
                            return JsonJC.ObjectToJson(_result);
                        }
                    }
                }
                else
                {
                    _result.Status = (int)CommonStatus.ProductLess;
                    _result.Obj = "";
                    return JsonJC.ObjectToJson(_result);
                }
            } 
            _result.Status = (int)CommonStatus.Fail;
            _result.Obj = "";
            return JsonJC.ObjectToJson(_result);

        }

       
        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        public string OrderCancel()
        {
            ResultObj _result = new ResultObj();
            long _code = FormString.LongIntSafeQ("code");
            string _reason = FormString.SafeQ("reason");
            OrderEntity _order = OrderBLL.Instance.GetOrderByCode(_code);
            if (_order.MemId == memid)
            {
               if (_order.OrderStyle == (int)OrderStyleEnum.XuQiu)
                {
                    IList<CGOrderEntity> cgorder = CGOrderBLL.Instance.GetCGOrderBySourceCode(_order.Code);
                    string cgorderstr = "";
                    bool cancancel = true;
                    foreach(CGOrderEntity cgentiy in cgorder)
                    {
                        if(cgentiy.Status==(int)CGOrderStatus.WaitRecived|| cgentiy.Status == (int)CGOrderStatus.Cancel)
                        {
                            cgorderstr += cgentiy.Code+"|";
                        }
                        else
                        {
                            cancancel = false;
                        }
                    }
                    if(cancancel)
                    {
                        cgorderstr = cgorderstr.TrimEnd('|');
                        if (CGOrderBLL.Instance.CGOrderCancelXuQiu(cgorderstr) > 0)
                        {
                            if (OrderBLL.Instance.OrderCancelXuQiu(_order.Code, _order.TimeStampCode, _reason, memid, _order.Status) > 0)
                            {
                                string productdetails = "";
                                IList<OrderDetailEntity> _listproduct = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, StringUtils.GetDbLong(_order.Code), false);
                                foreach (OrderDetailEntity _entity in _listproduct)
                                {
                                    productdetails += "|" + _entity.ProductDetailId.ToString() + "_" + _entity.Num.ToString();
                                }
                                if (productdetails != "")
                                {
                                    productdetails = productdetails.TrimStart('|');
                                    ProductDetailBLL.Instance.ReleaseStock(productdetails);
                                }

                                _result.Status = (int)CommonStatus.Success;
                                _result.Obj = OrderBLL.Instance.GetVWOrderByCode(_code);
                            }
                        }
                    }
                    else
                    { 
                        _result.Status = (int)CommonStatus.OrderNoCancel;
                    } 
                }
                else
                {


                    if (_order.Status == (int)OrderStatus.Cancel)
                    {
                        _result.Status = (int)CommonStatus.OrderHasCancel;
                    }
                    else if (_order.Status == (int)OrderStatus.WaitPay)//没付钱
                    {
                        if (OrderBLL.Instance.OrderCancel(_order.Code, _order.TimeStampCode, _reason, memid, _order.Status) > 0)
                        {
                            string productdetails = "";
                            IList<OrderDetailEntity> _listproduct = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, StringUtils.GetDbLong(_order.Code), false);
                            foreach (OrderDetailEntity _entity in _listproduct)
                            {
                                productdetails += "|" + _entity.ProductDetailId.ToString() + "_" + _entity.Num.ToString();
                            }
                            if (productdetails != "")
                            {
                                productdetails = productdetails.TrimStart('|');
                                ProductDetailBLL.Instance.ReleaseStock(productdetails);
                            }

                            _result.Status = (int)CommonStatus.Success;
                            _result.Obj = OrderBLL.Instance.GetVWOrderByCode(_code);
                        }
                        else
                        {
                            _result.Status = (int)CommonStatus.Fail;
                            _result.Obj = _order.Code;
                        }
                    }
                    //else if (_order.Status == (int)OrderStatus.WaitDeal )//付过钱未确认
                    //{
                    //    if (OrderBLL.Instance.OrderCancel2(_order.Code, _order.TimeStampCode, _reason, memid, _order.Status) > 0)
                    //    {
                    //        _result.Status = (int)CommonStatus.Success;
                    //        _result.Obj = _order.Code;
                    //    }
                    //    else
                    //    {
                    //        _result.Status = (int)CommonStatus.Fail;
                    //        _result.Obj = _order.Code;
                    //    }
                    //}
                    else if (_order.Status == (int)OrderStatus.WaitDeal || _order.Status == (int)OrderStatus.WaitDeliver || _order.Status == (int)OrderStatus.WaitRecive)//已确认收款有可能以分配供应商
                    {
                        if (OrderBLL.Instance.OrderCancel3(_order.Code, _order.TimeStampCode, _reason, memid, _order.Status) > 0)
                        {
                            _result.Status = (int)CommonStatus.Success;
                            _result.Obj = OrderBLL.Instance.GetVWOrderByCode(_code);
                        }
                        else
                        {
                            _result.Status = (int)CommonStatus.Fail;
                            _result.Obj = _order.Code;
                        }
                    }
                    else
                    {
                        _result.Status = (int)CommonStatus.OrderNoCancel;

                    }
                }
            }
            else
            {
                _result.Status = (int)CommonStatus.Fail;
                _result.Obj = _order.Code;
            }
            return JsonJC.ObjectToJson(_result);
        }

        /// <summary>
        /// 确认收货
        /// </summary>
        /// <returns></returns>
        public string OrderRecived()
        {
            ResultObj _result = new ResultObj();
            long _code = FormString.LongIntSafeQ("code");
            if (OrderBLL.Instance.OrderRecived(_code, memid) > 0)
            {
                //采购系统接口
                if (CGOrderBLL.Instance.CGOrderRecived(_code) == 0)
                {
                    LogUtil.Log("采购系统数据同步出错", "订单确认收货同步，订单号" + _code);
                }
                //优惠券发放
                int num = MemCouponsBLL.Instance.ProvideCouponsByOrder(_code);
                if (num > 0)
                { 
                    OrderBLL.Instance.EditOrderCouponsNum(_code, num);
                }
                _result.Status = (int)CommonStatus.Success;
                _result.Obj = OrderBLL.Instance.GetVWOrderByCode(_code);
            }
            else
            {
                _result.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(_result);
        }

        /// <summary>
        /// 支付通道,手机网站同
        /// </summary>
        /// <returns></returns>
        public ActionResult Cashier()
        {

            string paycode = QueryString.SafeQ("code");

            VWPayOrderEntity _orderentity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            if (_orderentity.PayMethod == (int)PayType.OutLine)
            {
                return RedirectToAction("OrderNotice", new { code = _orderentity.SysOrderCode });
            }
            if (Globals.IsWeiXinDevice() && (_orderentity.PayMethod == (int)PayType.AliPay || _orderentity.PayMethod == (int)PayType.AliPayMobile))
            {
                return View();
            }
            //if (_orderentity.Status == (int)OrderStatus.WaitPay)
            if (_orderentity.Status == 0)
            {
                //PayEBankEntity _bank = new PayEBankEntity();
                //PayEBankEntity _bank = PayEBankBLL.Instance.GetPayEBankByPayType(_orderentity.PayType);
                //PayTradeEntity _trade = PayTradeBLL.Instance.GetPayTradeByOrder(_orderentity.Code);
                //_trade.PayCode = _orderentity.Code.ToString();
                //_trade.OrderCode = _orderentity.Code;
                //_trade.OrderAmount = _orderentity.ActPrice;
                ////_trade.OrderAmount =0.03m;
                //bool savesuccess = false;
                //if (_trade.Id == 0)
                //{
                //if ((_trade.Id = PayTradeBLL.Instance.AddPayTrade(_trade)) > 0)
                //        savesuccess = true;
                //}
                //else if (PayTradeBLL.Instance.UpdatePayTrade(_trade) > 0)
                //{
                //    savesuccess = true;
                //}
                //if (savesuccess)
                //{
                SuperMarket.Pay.PaymentRequest.Instance(((PayType)_orderentity.PayMethod).ToString()).SendRequest(_orderentity);//请求支付
                //}
            }
            else if(_orderentity.Status == 1)
             {

                Response.Write("已支付");
            }
            else
            {
                Response.Write("11111");
            }
            return View();
        }

        public ActionResult OrderNotice()
        {
            string paycode = QueryString.SafeQ("code");
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            //long ordecode = QueryString.LongIntSafeQ("code");
            //VWOrderEntity _orderentity = OrderBLL.Instance.GetVWOrderByCode(ordecode);
            ViewBag.Order = _payen;
            return View();
        }
         
        public ActionResult OrderConfirmPay()
        {
            string paycode = QueryString.SafeQ("code");
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            if (_payen.PayMethod == (int)PayType.AliPay|| _payen.PayMethod == (int)PayType.AliPayMobile)
            {
                return RedirectToAction("Cashier", new { code = _payen.PayOrderCode });
            }
            else if (_payen.PayMethod == (int)PayType.WeChat)
            {
                return RedirectToAction("WeiXin", new { paycode = _payen.PayOrderCode });
            }
            else if (_payen.PayMethod == (int)PayType.OutLine)
            {
                return RedirectToAction("OrderNotice", new { code = _payen.PayOrderCode });
            }
            return View();
        }
        public ActionResult WeiXin()
        {
            string _paycode = QueryString.SafeQ("paycode");
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(_paycode);
            DateTime dtnow = DateTime.Now;
            NativePay nativePay = new NativePay();
            WxPayData data = nativePay.GetPayUrl(_payen);
            if (data.GetValue("result_code").ToString().ToLower() == "success")
            {
                string url = data.GetValue("code_url").ToString();//获得统一下单接口返回的二维码链接 
                ViewBag.Url = url;
            }
            else
            {
                string error = data.GetValue("err_code_des").ToString();
                ViewBag.ErrorMsg = error;
            }
            ViewBag.PayEntity = _payen;
            return View();
        }

        public string GetPayStatus()
        {
            ResultObj _result = new ResultObj();
            string _paycode = FormString.SafeQ("paycode");
            VWPayOrderEntity _payen = PayOrderBLL.Instance.GetVWPayOrderByPayCode(_paycode);
            if(_payen!=null&& _payen.Id>0)
            {
                _result.Status= (int)CommonStatus.Success;
                _result.Obj= _payen.Status;
            }
            return JsonJC.ObjectToJson(_result);
        }
        public string PayMethodUpdate()
        {
            ResultObj result = new ResultObj();
            string paycode = QueryString.SafeQ("paycode");
            long ordecode = QueryString.LongIntSafeQ("code");
            int paytype = QueryString.IntSafeQ("paytype"); 
            int exnum= OrderBLL.Instance.PayMethodUpdate(ordecode, memid, paytype);
            if (exnum > 0)
            {
                if(!string.IsNullOrEmpty(paycode))
                {
                    PayOrderBLL.Instance.PayTypeUpdate(paycode, paytype, memid);
                }
                result.Status = (int)CommonStatus.Success;
            }
            else
            { 
                result.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(result);
        }


        #endregion


    }
}
