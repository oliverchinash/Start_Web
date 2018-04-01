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
    public class SysOrderInquiryController : BaseMemAdminController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult OrderList()
        {
            int _status = QueryString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeCheck;
            int pageindex = QueryString.IntSafeQ("pageindex");
            if (pageindex == 0) pageindex = 1;
            string key = QueryString.SafeQ("key");
            int record = 0;
            IList<VWInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetInquiryOrderList(pagesize, pageindex, ref record, -1, _status,-1, key);
            ViewBag.InquiryOrderList = list;
            ViewBag.Status = _status;
            ViewBag.KeyWord = key;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageNum = maxpage;
            string _url = "/SysOrderInquiry/OrderList?s=" + _status + "&k=" + key  ;
            string _pagestr = HTMLPage.GetSysListPage(record, pagesize, pageindex, _url);
            ViewBag.PageStr = _pagestr;

            return View(); 
        }
        public ActionResult OrderDetail()
        {
            string _ordercode = QueryString.SafeQ("code");
            InquiryOrderEntity order = InquiryOrderBLL.Instance.GetInquiryOrderByCode(_ordercode);
            if (order != null  )
            { 
                VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(_ordercode);
                IList<InquiryOrderFeedBackEntity> feedbacklist = InquiryOrderFeedBackBLL.Instance.GetOrderFeedBackAllByCode(_ordercode);
                IList<InquiryOrderPicsEntity> orderpics = vworder.OrderPics;
                IList<InquiryProductEntity> productlist = vworder.OrderProducts;
                IList<InquiryProductSubEntity> productsublist = vworder.OrderProductSubs;
                IList<VWCGMemQuotedEntity> quotelist = CGMemQuotedBLL.Instance.GetVWCGMemQuotedAllByCode(_ordercode);
                IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(_ordercode, -1, true);
                Dictionary<string, CGQuotedPriceEntity> pricedic = new Dictionary<string, CGQuotedPriceEntity>();
                IList<InquiryProductSubEntity> productsubs = vworder.OrderProductSubs;
                Dictionary<int, IList<InquiryProductSubEntity>> productsubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
                foreach (CGQuotedPriceEntity price in pricelist)
                {
                    if (!pricedic.ContainsKey(price.InquiryProductSubId + "_" + price.CGMemId))
                    {
                        pricedic.Add(price.InquiryProductSubId + "_" + price.CGMemId, price);
                    }
                }
                if (productsubs != null && productsubs.Count > 0)
                {
                    foreach (InquiryProductSubEntity prod in productsubs)
                    {
                        if (!productsubdic.ContainsKey(prod.InquiryProductId))
                        {
                            productsubdic.Add(prod.InquiryProductId, new List<InquiryProductSubEntity>());
                        }
                        productsubdic[prod.InquiryProductId].Add(prod);
                    }
                }
                CGMemQuotedEntity quote = CGMemQuotedBLL.Instance.GetCGMemHasCheckedByCode(_ordercode);
                MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(quote.CGMemId); 
                ViewBag.QuotedCGMemList = quotelist;
                ViewBag.ProductSubDIc = productsubdic;
                ViewBag.PriceDic = pricedic;  
                ViewBag.VWOrder = vworder;
                ViewBag.ProductSubDic = productsubdic;
                ViewBag.CGMemStore = store;
                ViewBag.CGMemQuote = quote; 
                ViewBag.FeedBackList = feedbacklist;
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
            VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(code);
            IList<VWCGMemQuotedEntity> quotelist = CGMemQuotedBLL.Instance.GetVWCGMemQuotedAllByCode(code);
            IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(code, -1, true);
            Dictionary<string, CGQuotedPriceEntity> pricedic = new Dictionary<string, CGQuotedPriceEntity>();
            IList<InquiryProductSubEntity> productsubs = vworder.OrderProductSubs;
            Dictionary<int, IList<InquiryProductSubEntity>> productsubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();

            foreach (CGQuotedPriceEntity price in  pricelist)
            {
                if(!pricedic.ContainsKey(price.InquiryProductSubId + "_"+ price.CGMemId))
                {
                    pricedic.Add(price.InquiryProductSubId + "_" + price.CGMemId, price);
                }
            }
            if (productsubs != null && productsubs.Count > 0)
            {
                foreach (InquiryProductSubEntity prod in productsubs)
                {
                    if (!productsubdic.ContainsKey(prod.InquiryProductId))
                    {
                        productsubdic.Add(prod.InquiryProductId, new List<InquiryProductSubEntity>());
                    }
                    productsubdic[prod.InquiryProductId].Add(prod);
                }
            }
            ViewBag.VWOrder = vworder ;
            ViewBag.QuotedCGMemList = quotelist;
            ViewBag.ProductSubDIc = productsubdic;
            ViewBag.PriceDic = pricedic;
            ViewBag.Code = code;
            return View();
        }
        /// <summary>
        /// 选择供应商
        /// </summary>
        /// <returns></returns>
        public string SelectCGMemSubmit()
        {
            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            int memid = FormString.IntSafeQ("memid");
            InquiryOrderEntity order = InquiryOrderBLL.Instance.GetInquiryOrderByCode(ordercode);
            if (order.Status == (int)OrderInquiryStatusEnum.Quoting || order.Status == (int)OrderInquiryStatusEnum.WaitAccept || order.Status == (int)OrderInquiryStatusEnum.HasAccept  )
            {
                int resultrowi = CGMemQuotedBLL.Instance.SelectInquiryOrderCGMem(ordercode, memid);
                if (resultrowi > 0)
                {
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

        public string SelectCGMemPriceSubmit()
        {
            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            string memprices = FormString.SafeQ("memprices",8000);
            memprices = memprices.Trim('|');
            InquiryOrderEntity order = InquiryOrderBLL.Instance.GetInquiryOrderByCode(ordercode);
            if (order.Status == (int)OrderInquiryStatusEnum.Quoting || order.Status == (int)OrderInquiryStatusEnum.WaitAccept || order.Status == (int)OrderInquiryStatusEnum.HasAccept  )
            { 
                int resultrowi = CGQuotedPriceBLL.Instance.InquirySelectQuote(ordercode, memprices);
                if (resultrowi > 0)
                {
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
        /// 设定价格页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductPriceSet()
        {
            string code = QueryString.SafeQ("code");
            VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(code);
            //CGMemQuotedEntity  quote  = CGMemQuotedBLL.Instance.GetHasCheckedCGMem(code);
            //VWMemberEntity vwmember = null;
            //if (quote != null&& quote.Id>0)
            //{
            //      vwmember = MemberBLL.Instance.GetVWMember(quote.CGMemId);
            //}
            IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(code,-1, true,false,true);
            Dictionary<int, SuperMarket.Model.CGQuotedPriceEntity> pricedic = new Dictionary<int, SuperMarket.Model.CGQuotedPriceEntity>();

            foreach (CGQuotedPriceEntity price in pricelist)
            {
                if (!pricedic.ContainsKey(price.InquiryProductSubId  ))
                {
                    pricedic.Add(price.InquiryProductSubId  , price );
                }
            }
            IList<InquiryProductSubEntity> productsubs = vworder.OrderProductSubs;
            Dictionary<int, IList<InquiryProductSubEntity>> productsubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
            if (productsubs != null && productsubs.Count > 0)
            {
                foreach (InquiryProductSubEntity prod in productsubs)
                {
                    if (!productsubdic.ContainsKey(prod.InquiryProductId))
                    {
                        productsubdic.Add(prod.InquiryProductId, new List<InquiryProductSubEntity>());
                    }
                    productsubdic[prod.InquiryProductId].Add(prod);
                }
            }
            ViewBag.VWOrder = vworder ;
            //ViewBag.CGMember  = vwmember;
            ViewBag.ProductSubDic = productsubdic;
            ViewBag.PriceDic = pricedic;
            ViewBag.Code = code;
            return View();
        }
         
        /// <summary>
        /// 设定价格提交
        /// </summary>
        /// <returns></returns>
        public string QuotePriceSetSubmit()
        {
            ResultObj result = new ResultObj();
            string _code = FormString.SafeQ("code");
            string _pricestr = FormString.SafeQ("prices", 8000);
            InquiryOrderEntity orderentity = InquiryOrderBLL.Instance.GetInquiryOrderByCode(_code);
            if (orderentity != null&& orderentity.Id>0  )
            {
                int status = (int)orderentity.Status;//此状态后台记录，如果超过报价，不改变此状态
                int statusformem = (int)InquiryStatusForMemEnum.WaitAccept;
                if (orderentity.Status == (int)OrderInquiryStatusEnum.Quoting)
                {
                    status = (int)OrderInquiryStatusEnum.WaitAccept;
                }
                int rowi = InquiryOrderBLL.Instance.QuotePriceSet(_code, _pricestr, status, statusformem);
                if (rowi > 0)
                {
                    //rowi = InquiryOrderBLL.Instance.QuotePriceCheck(_code);
                    //if (rowi > 0)
                    //{
                        if (orderentity.Status == (int)OrderInquiryStatusEnum.Quoting)
                        {
                            WeiXinInQuiryOrderBLL.Instance.NoteToUserAccept(_code, orderentity.MemId);
                        }
                        else
                        {
                        ///重复通知接收者
                            WeiXinInQuiryOrderBLL.Instance.NoteToUserAcceptAgain(_code, orderentity.MemId);
                        }
                    //}
                    result.Status = (int)CommonStatus.Success;
                    result.Obj = _code;
                    return JsonJC.ObjectToJson(result);
                }
            }
            result.Status = (int)CommonStatus.Fail;
            result.Obj = _code;
            return JsonJC.ObjectToJson(result);
        }

        #region 报表查询
        /// <summary>
        /// 询价单报表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportOrderList()
        {
            string startdate = QueryString.SafeQ("startdate");
            string enddate = QueryString.SafeQ("enddate");
            int status = QueryString.IntSafeQ("status",-1);
            int reporttype = QueryString.IntSafeQ("reporttype");
            int orderby = QueryString.IntSafeQ("orderby");
            if (startdate == "") startdate = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            if (enddate == "") enddate = DateTime.Now.AddMonths(1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");

            IList<ReportInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetReportDaily(startdate, enddate, status, reporttype, orderby);
            ViewBag.ReportList = list;
            ViewBag.StartDate = startdate;
            ViewBag.EndDate = enddate;
            ViewBag.Status = status;
            ViewBag.ReportType = reporttype;
            ViewBag.OrderBy = orderby;
            return View();
        }
        /// <summary>
        /// 询价单供应商报表
        /// </summary>
        /// <returns></returns>
        public ActionResult ReportCGOrderList()
        {
            string startdate = QueryString.SafeQ("startdate");
            string enddate = QueryString.SafeQ("enddate");
            int status = QueryString.IntSafeQ("status", -1);
            int reporttype = QueryString.IntSafeQ("reporttype");
            int orderby = QueryString.IntSafeQ("orderby");
            if (startdate == "") startdate = DateTime.Now.AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");
            if (enddate == "") enddate = DateTime.Now.AddMonths(1).AddDays(1 - DateTime.Now.Day).ToString("yyyy-MM-dd");

            IList<ReportInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetCGReportDaily(startdate, enddate, status, reporttype, orderby);
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
