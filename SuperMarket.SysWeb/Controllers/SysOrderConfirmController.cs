using SuperMarket.BLL.JcOrderInquiry;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class SysOrderConfirmController : BaseMemAdminController
    {
      
        public ActionResult OrderList()
        {
            int _status = QueryString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeCheck;
            int pageindex = QueryString.IntSafeQ("px");
            if (pageindex == 0) pageindex = 1;
            string key = QueryString.SafeQ("k");
            int _cgmemid = QueryString.IntSafeQ("cgmemid",-1);
            int _memid = QueryString.IntSafeQ("memid",-1);
            string inquirycode = QueryString.SafeQ("inquirycode");
            int record = 0;
            IList<VWConfirmOrderEntity> list = ConfirmOrderBLL.Instance.GetConfirmOrderList(pagesize, pageindex, ref record, _memid, _cgmemid, _status,  key, inquirycode);
            ViewBag.OrderList = list;
            ViewBag.Status = _status;
            ViewBag.KeyWord = key;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageNum = maxpage;
            string _url = "/SysOrderConfirm/OrderList?s=" + _status + "&k=" + key  ;
            string _pagestr = HTMLPage.GetSysListPage(record, pagesize, pageindex, _url);
            ViewBag.PageStr = _pagestr;

            return View(); 
        }
        public ActionResult OrderDetail()
        {
            string _ordercode = QueryString.SafeQ("code");

            VWConfirmOrderEntity confirmorder = ConfirmOrderBLL.Instance.GetVWConfirmOrderByCode(_ordercode);
            if (confirmorder != null&& confirmorder.ConfirmOrderId>0)
            {
                Dictionary<int, VWMemberEntity> memberdic = new Dictionary<int, VWMemberEntity>();
                IList<ConfirmOrderProductEntity> productlist = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(_ordercode, -1,false);
                confirmorder.OrderProducts = productlist;
                confirmorder.OrderPics = InquiryOrderPicsBLL.Instance.GetInquiryOrderPicsAllByCode(confirmorder.InquiryOrderCode);
                foreach(ConfirmOrderProductEntity pro in productlist)
                {
                    if(pro.CGMemId>0&& !memberdic.ContainsKey(pro.CGMemId))
                    {
                        memberdic[pro.CGMemId] = MemberBLL.Instance.GetVWMember(pro.CGMemId);
                    }
                }
                //CGMemQuotedEntity quote = CGMemQuotedBLL.Instance.GetCGMemHasCheckedByCode(confirmorder.InquiryOrderCode);
                //ViewBag.CGMemQuote = quote;
                ViewBag.CGMemberDic = memberdic; 
            } 
            ViewBag.VWOrder = confirmorder;
            return View();

        }

        public ActionResult OrderConfirmPrint()
        {
            string _code = QueryString.SafeQ("code");
            VWConfirmOrderEntity confirmorder = ConfirmOrderBLL.Instance.GetVWConfirmOrderByCode(_code);
            if (confirmorder != null && confirmorder.ConfirmOrderId > 0  )
            {
                IList<ConfirmOrderProductEntity> productlist = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(_code, -1, false);
                confirmorder.OrderProducts = productlist;
                confirmorder.OrderPics = InquiryOrderPicsBLL.Instance.GetInquiryOrderPicsAllByCode(confirmorder.InquiryOrderCode);

                ViewBag.VWOrder = confirmorder;
            }
            else
            {
                RedirectToAction("OrderConfirmList");
            }
            return View();
        }
        /// <summary>
        /// 选择供应商页面
        /// </summary>
        /// <returns></returns>
        public ActionResult CGMemSelect()
        {
            string code = QueryString.SafeQ("code");
            VWConfirmOrderEntity conor = ConfirmOrderBLL.Instance.GetVWConfirmOrderByCode(code);
            if (conor.Status == (int)OrderConfirmStatusEnum.WaitSelectCGMem)
            {
                conor.OrderProducts = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(code, -1, false);
                //VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(conor.InquiryOrderCode);
                IList<VWCGMemQuotedEntity> quotelist = CGMemQuotedBLL.Instance.GetVWCGMemQuotedAllByCode(conor.InquiryOrderCode);
                IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(conor.InquiryOrderCode, -1, true);
                Dictionary<string, CGQuotedPriceEntity> pricedic = new Dictionary<string, CGQuotedPriceEntity>();
                foreach (CGQuotedPriceEntity price in pricelist)
                {
                    if (!pricedic.ContainsKey(price.InquiryProductSubId + "_" + price.CGMemId))
                    {
                        pricedic.Add(price.InquiryProductId+"_"+ price.InquiryProductType + "_" + price.CGMemId, price);
                    }
                }
                //IList<InquiryProductSubEntity> productsubs = vworder.OrderProductSubs;
                //Dictionary<int, IList<InquiryProductSubEntity>> productsubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
                //if (productsubs != null && productsubs.Count > 0)
                //{
                //    foreach (InquiryProductSubEntity prod in productsubs)
                //    {
                //        if (!productsubdic.ContainsKey(prod.InquiryProductId))
                //        {
                //            productsubdic.Add(prod.InquiryProductId, new List<InquiryProductSubEntity>());
                //        }
                //        productsubdic[prod.InquiryProductId].Add(prod);
                //    }
                //}
                ViewBag.VWOrder = conor;
                ViewBag.QuotedCGMemList = quotelist;
                //ViewBag.ProductSubDIc = productsubdic;
                ViewBag.PriceDic = pricedic;
                ViewBag.Code = code;
            }
            else
            {
                return RedirectToAction("OrderDetail", new { code = code });
            }
            return View();
        }
        /// <summary>
        /// 为订单选择供应商.只有一个供应商，备用
        /// </summary>
        /// <returns></returns>
        public string SelectCGMemForConfirmOrder()
        {
            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            int memid = FormString.IntSafeQ("memid");
            ConfirmOrderEntity order = ConfirmOrderBLL.Instance.GetConfirmOrderByCode(ordercode);
            if (order.Status == (int)OrderConfirmStatusEnum.WaitSelectCGMem)
            {
                int resultrowi = CGMemQuotedBLL.Instance.SelectConfirmOrderCGMem(order.InquiryOrderCode,ordercode,   memid);
                if (resultrowi > 0)
                {
                    //选择后直接到支付（暂时省），通知送货员或调用物流接口
                    WeiXinInQuiryOrderBLL.Instance.NoteToDeliveryman(ordercode); 
                    ////通知供应商备货
                    WeiXinInQuiryOrderBLL.Instance.CGMemStockNote(ordercode);
                    result.Status = (int)CommonStatus.Success;
                    result.Obj = resultrowi;
                }
                else
                {
                    result.Status = (int)CommonStatus.Fail;
                }
            }
            else
            {
                result.Status = (int)CommonStatus.OrderCanNotDo;
            }

            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 选择供应的产品，新版选择供应商
        /// </summary>
        /// <returns></returns>
        public string SelCGMemForConfirm()
        {
            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            string memprices = FormString.SafeQ("memprices",8000);
            ConfirmOrderEntity order = ConfirmOrderBLL.Instance.GetConfirmOrderByCode(ordercode);
            if (order.Status == (int)OrderConfirmStatusEnum.WaitSelectCGMem)
            {
                int resultrowi = CGMemQuotedBLL.Instance.SelConfirmOrderCGMem(order.InquiryOrderCode, ordercode, memprices);
                if (resultrowi > 0)
                {
                    //选择后直接到支付（暂时省），通知送货员或调用物流接口
                    WeiXinInQuiryOrderBLL.Instance.NoteToDeliveryman(ordercode);
                    ////通知供应商备货
                    WeiXinInQuiryOrderBLL.Instance.CGMemStockNote(ordercode);
                    result.Status = (int)CommonStatus.Success;
                    result.Obj = resultrowi;
                }
                else
                {
                    result.Status = (int)CommonStatus.Fail;
                }
            }
            else
            {
                result.Status = (int)CommonStatus.OrderCanNotDo;
            }

            return JsonJC.ObjectToJson(result);
        }

        #region
        public ActionResult ReportOrderList()
        {
        
            string startdate = QueryString.SafeQ("startdate");
            string enddate = QueryString.SafeQ("enddate");
            int status = QueryString.IntSafeQ("status", -1);
            int reporttype = QueryString.IntSafeQ("reporttype");
            int orderby = QueryString.IntSafeQ("orderby");
            if (startdate == "") startdate = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            if (enddate == "") enddate = DateTime.Now.AddMonths(1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");

            IList<ReportInquiryOrderEntity> list = ConfirmOrderBLL.Instance.GetReportDaily(startdate, enddate, status, reporttype, orderby);
            ViewBag.ReportList = list;
            ViewBag.StartDate = startdate;
            ViewBag.EndDate = enddate;
            ViewBag.Status = status;
            ViewBag.ReportType = reporttype;
            ViewBag.OrderBy = orderby;
            return View();
     
    }

        public ActionResult ReportCGOrderList()
        {
            string startdate = QueryString.SafeQ("startdate");
            string enddate = QueryString.SafeQ("enddate");
            int status = QueryString.IntSafeQ("status", -1);
            int reporttype = QueryString.IntSafeQ("reporttype");
            int orderby = QueryString.IntSafeQ("orderby");
            if (startdate == "") startdate = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            if (enddate == "") enddate = DateTime.Now.AddMonths(1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");

            IList<ReportInquiryOrderEntity> list = ConfirmOrderBLL.Instance.GetCGReportDaily(startdate, enddate, status, reporttype, orderby);
            ViewBag.ReportList = list;
            ViewBag.StartDate = startdate;
            ViewBag.EndDate = enddate;
            ViewBag.Status = status;
            ViewBag.ReportType = reporttype;
            ViewBag.OrderBy = orderby;
            return View();
        }

        #endregion

    }
}
