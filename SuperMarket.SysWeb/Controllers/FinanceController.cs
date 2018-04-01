using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.Core;
using SuperMarket.Core.BaiduMap;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Picture;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using SuperMarket.Pay;
using SuperMarket.Web.CommonControllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SuperMarket.SysWeb.Controllers
{
    public class FinanceController : BaseMemAdminController
    {

        public ActionResult RecivedConfirm()
        {
            long ordercode = QueryString.LongIntSafeQ("code");//订单号
            int pageindex = QueryString.IntSafeQ("pageindex", 1); 
            string  confirmcode = QueryString.SafeQ("cfcode");
            int pagesize = CommonKey.PageSizeFinance;
            int record = 0;
            IList<VWOrderPayLineConfirm> list = OrderPayLineConfirmBLL.Instance.GetVWPayLineConfirmList(pagesize, pageindex, ref record, ordercode, confirmcode);
            ViewBag.List = list;
             string _url = "/Finance/RecivedConfirm?code=" + ordercode + "&cfcode=" + confirmcode;
            string _pageStr = HTMLPage.SetOrderListPage(record, pagesize, pageindex, _url);
            ViewBag.PageStr = _pageStr;
            return View();

        }
        public string PayLineConfirm()
        {
            ResultObj result = new ResultObj();
            int _id = FormString.IntSafeQ("id");
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            OrderPayLineConfirmEntity entity = OrderPayLineConfirmBLL.Instance.GetPayLineConfirm(_id, _ordercode);
            if(entity!=null&& entity.Id>0)
            {
                OrderPayLineConfirmBLL.Instance.ConfirmRecived(_id, _ordercode,memid);
                result.Status = (int)CommonStatus.Success;
            }
            else
            { 
                result.Status = (int)CommonStatus.Fail;
            }

            return JsonJC.ObjectToJson(result);
        }
        public ActionResult DoRefundManage()
        {
            string bactchno = QueryString.SafeQ("bno");//退款批次号
            long ordercode = QueryString.LongIntSafeQ("code");//订单号
            int pageindex = QueryString.IntSafeQ("pageindex", 1);
            if (string.IsNullOrEmpty(bactchno))
            {
                bactchno = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.PayRebackBatchNo);
                RouteValueDictionary _dic = new RouteValueDictionary();
                _dic.Add("bno", bactchno);
                _dic.Add("code", ordercode);
                return RedirectToAction("DoRefundManage", _dic);
            }
            ViewBag.RebackBatchNo = bactchno;
            ViewBag.OrderCode = ordercode;
            //判断是否是修改
            int pagesize = CommonKey.PageSizeFinance;
            int recordcount = 0; 
            IList<VWFinanceRefundEntity> _orderentity = FinanceRefundBLL.Instance.GetVWFinanceRefundList(pagesize, pageindex, ref recordcount, ordercode);
            ViewBag.List = _orderentity;
            IList<string> batchnolist = PayRebackBLL.Instance.GetBatchTodayWait();
            ViewBag.BatchList = batchnolist;
            string _url = "/Finance/RefundManage?bno=" + bactchno + "&code=" + ordercode;
            string _pageStr = HTMLPage.SetOrderListPage(recordcount, pagesize, pageindex, _url);
            ViewBag.PageStr = _pageStr;
            return View();
        }
        public ActionResult RefundManage()
        {
            string bactchno = QueryString.SafeQ("bno");//退款批次号
            long ordercode = QueryString.LongIntSafeQ("code");//订单号
            int pageindex = QueryString.IntSafeQ("pageindex", 1);
            if (string.IsNullOrEmpty(bactchno))
            {
                bactchno = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.PayRebackBatchNo);
                RouteValueDictionary _dic = new RouteValueDictionary();
                _dic.Add("bno", bactchno);
                _dic.Add("code", ordercode);
                return RedirectToAction("RefundManage", _dic);
            }
            ViewBag.RebackBatchNo = bactchno;
            ViewBag.OrderCode = ordercode;
            //判断是否是修改
            int pagesize = CommonKey.PageSizeFinance;
            int recordcount = 0;
            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderCode, CompareValue = ordercode });
            IList<VWPayAliResultEntity> _orderentity = PayAliResultBLL.Instance.GetVWPayAliResultList(pagesize, pageindex, ref recordcount, wherelist);
            ViewBag.List = _orderentity;
            IList<string> batchnolist = PayRebackBLL.Instance.GetBatchTodayWait();
            ViewBag.BatchList = batchnolist;
            string _url = "/Finance/RefundManage?bno=" + bactchno + "&code=" + ordercode;
            string _pageStr = HTMLPage.SetOrderListPage(recordcount, pagesize, pageindex, _url);
            ViewBag.PageStr = _pageStr;
            return View();
        }
        public ActionResult RefundDetail()
        {
            string bactchno = QueryString.SafeQ("bno");
            int pageindex = QueryString.IntSafeQ("pageindex", 1);
            if (!string.IsNullOrEmpty(bactchno))
            {
                ViewBag.RebackBatchNo = bactchno;
                //判断是否是修改
                int pagesize = CommonKey.PageSizeFinance;
                int recordcount = 0;
                IList<ConditionUnit> wherelist = new List<ConditionUnit>();
                wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.RebackBatchNo, CompareValue = bactchno });
                IList<PayRebackEntity> _list = PayRebackBLL.Instance.GetPayRebackList(pagesize, pageindex, ref recordcount, wherelist);
                ViewBag.List = _list;
                string _url = "/Finance/RefundDetail?bno=" + bactchno;
                string _pageStr = HTMLPage.SetOrderListPage(recordcount, pagesize, pageindex, _url);
                ViewBag.PageStr = _pageStr;
            }
            return View();
        }

        public ActionResult RebackFundPage()
        {
            string batchno = FormString.SafeQ("bno");
            int paytype = FormString.IntSafeQ("paytype");
            if (paytype == 0) paytype = (int)PayType.AliPay;
            IList<PayRebackEntity> _list = PayRebackBLL.Instance.GetPayRebackAll(batchno);
            int num = 0;
            string redata = "";
            if (_list != null && _list.Count > 0)
            {
                foreach (PayRebackEntity entity in _list)
                {
                    num++;
                    redata += entity.TradeNoLog + "^" + entity.RebackFee.ToString() + "^" + entity.Remack + "#";
                }
                redata = redata.TrimEnd('#');
            }
            PayEBankEntity _bank = new PayEBankEntity();
            PayTradeEntity _trade = new PayTradeEntity();
            _trade.Refund_Data = redata;
            _trade.Batch_No = batchno;
            _trade.Batch_Num = num;
            //PaymentRequest.Instance(((PayType)paytype).ToString(),  _trade).ReturnRequest();
            //支付宝退款
            return View();
        }


        /// <summary>
        /// 添加退款信息
        /// </summary>
        /// <returns></returns>
        public string AddPayReback()
        {
            ResultObj obj = new ResultObj();
            long _batchno = FormString.LongIntSafeQ("batchno");
            int _refundid = FormString.IntSafeQ("refundid");
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            string _tradecode = FormString.SafeQ("tradecode");
            decimal _rebackfee = FormString.DecimalSafeQ("rebackfee");
            string _returnreason = FormString.SafeQ("returnreason",1000);
            OrderEntity orderentity=  OrderBLL.Instance.GetOrderByCode(_ordercode);
            FinanceRefundEntity Refundentity = FinanceRefundBLL.Instance.GetFinanceRefund(_refundid);
            if (orderentity.ActPrice - orderentity.ReBackFee >= _rebackfee&& Refundentity.RebackFee>= _rebackfee)
            {
                PayRebackEntity entity = new PayRebackEntity();
                entity.BatchNo = _batchno;
                entity.OrderCode = _ordercode;
                entity.TradeNoLog = _tradecode;
                entity.RebackFee = _rebackfee;
                entity.Remack = _returnreason;
                entity.RebackTime = DateTime.Now;
                entity.CreateTime = DateTime.Now;
                entity.FinanceRefundId = _refundid;

                int _result = PayRebackBLL.Instance.AddPayReback(entity);
                if(_result>0)
                {
                    obj.Status = (int)CommonStatus.Success;
                }
                else
                {
                    obj.Status = _result;
                }
            }
            else
            {
                obj.Status = (int)CommonStatus.AddPayRebackBig;
            }
            //int _result = OrderBLL.Instance.UpdateOrderRebackFee(_ordercode,_rebackfee);
             
            return JsonJC.ObjectToJson(obj);
        }

        public string AddPayRebackIntegral()
        {
            ResultObj obj = new ResultObj();
            long _batchno = FormString.LongIntSafeQ("batchno");
            int _refundid = FormString.IntSafeQ("refundid");
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            string _tradecode = FormString.SafeQ("tradecode");
            int _returnintegral = FormString.IntSafeQ("returnintegral");
            string _returnreason = FormString.SafeQ("returnreason", 1000);
            OrderEntity orderentity = OrderBLL.Instance.GetOrderByCode(_ordercode);
            FinanceRefundEntity Refundentity = FinanceRefundBLL.Instance.GetFinanceRefund(_refundid);
            //if (Refundentity.IntegralStatus == (int)ReBackIntegralStatus.WaitReback)
            //{
                if (orderentity.Integral - orderentity.ReBackFeeIntegral >= _returnintegral && Refundentity.ReBackIntegral - Refundentity.ActReBackIntegral >= _returnintegral)
                {
                    int _result = OrderBLL.Instance.RebackIntegral(_ordercode, orderentity.TimeStampCode, _refundid, _returnintegral, orderentity.MemId);
                    if (_result > 0)
                    {
                        obj.Status = (int)CommonStatus.Success;
                    }
                    else
                    {
                        obj.Status = _result;
                    }
                }
                else
                {
                    obj.Status = (int)CommonStatus.RebackIntegralBig;
                }
            //}
            //else
            //{
            //    obj.Status = (int)CommonStatus.RebackIntegralBig;

            //}

            //int _result = OrderBLL.Instance.UpdateOrderRebackFee(_ordercode,_rebackfee);

            return JsonJC.ObjectToJson(obj);
        }
        /// <summary>
        /// 个人增值税发票管理
        /// </summary>
        /// <returns></returns>
        public ActionResult MemBillVATManage()
        {

            string _companyName = QueryString.SafeQ("companyName",100);
            int _status = QueryString.IntSafeQ("status",-1);
            int _billType = QueryString.IntSafeQ("billType",0);

            int _pageSize = CommonKey.PageSizeMemBillVAT;
            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _recordCount = 0;

            IList<ConditionUnit> _whereList = new List<ConditionUnit>();
            _whereList.Add(new ConditionUnit { FieldName = SearchFieldName.CompanyName, CompareValue = _companyName.ToString() });
            _whereList.Add(new ConditionUnit { FieldName = SearchFieldName.MemBillVATStatus, CompareValue = _status.ToString() });
            _whereList.Add(new ConditionUnit { FieldName = SearchFieldName.BillType, CompareValue = _billType.ToString() });

            IList<MemBillVATEntity> _entityList = MemBillVATBLL.Instance.GetMemBillVATList(_pageSize, _pageIndex, ref _recordCount,_whereList);

            string _url = "/Finance/MemBillVATManage/?companyName=" + _companyName + "&status=" + _status + "&billType=" + _billType;
            string _pageStr= HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.EntityList = _entityList;
            return View();

        }


        /// <summary>
        /// 增值税发票审核
        /// </summary>
        /// <returns></returns>
        public int MemBillAudit()
        {
            int _id = FormString.IntSafeQ("id");
            int _status = FormString.IntSafeQ("status");
            if (_status>3||_status<0)
            {
                return 0;
            }
            int _result = MemBillVATBLL.Instance.UpdateMemBillVATStatus(_id,_status);
            return _result;
        }



    }
}
