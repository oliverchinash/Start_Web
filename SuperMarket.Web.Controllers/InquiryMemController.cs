using SuperMarket.BLL;
using SuperMarket.Core;
using SuperMarket.Core.BaiduMap;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Web;
using SuperMarket.Model.Basic.VW.Shopping;
using SuperMarket.Core.Linq;
using System.Linq;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.HelpDB;
using SuperMarket.Data.CGOrderDB;
using SuperMarket.BLL.CGOrderDB;
using System.Activities.Expressions;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.JcOrderInquiry;
using SuperMarket.Web.CommonControllers;
using SuperMarket.BLL.SysDB;

namespace SuperMarket.Web.MemberControllers
{
    public class InquiryMemController : BaseMemberController
    {

        #region 询价订单
        public ActionResult InquirySelect()
        {
            string _ordercode = QueryString.SafeQ("code");
            VWInquiryOrderEntity order = InquiryOrderBLL.Instance.GetVWInquiryOrderByCode(_ordercode);
            if (order != null && order.Status == (int)OrderInquiryStatusEnum.WaitAccept && order.MemId == memid)
            {
                VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(_ordercode);
                if (vworder.MemId == memid)
                {
                    IList<InquiryOrderPicsEntity> orderpics = vworder.OrderPics;
                    IList<InquiryProductEntity> products = vworder.OrderProducts;
                    IList<CGQuotedPriceEntity> productquoteds = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(_ordercode, memid, true, true);
                    Dictionary<int, IList<CGQuotedPriceEntity>> productquotedic = new Dictionary<int, IList<CGQuotedPriceEntity>>();
                    if (productquoteds != null && productquoteds.Count > 0)
                    {
                        foreach (CGQuotedPriceEntity proquotee in productquoteds)
                        {
                            if (!productquotedic.ContainsKey(proquotee.InquiryProductId))
                            {
                                productquotedic.Add(proquotee.InquiryProductId, new List<CGQuotedPriceEntity>());
                            }
                            productquotedic[proquotee.InquiryProductId].Add(proquotee);
                        }
                    }
                    ViewBag.VWOrder = vworder;
                    ViewBag.ProductList = products;
                    ViewBag.ProductQuoteDic = productquotedic;
                }
            }
            else
            {
                return RedirectToAction("InquiryOrderDetail", new { code = _ordercode });
            }

            return View();
        }

        /// <summary>
        /// 用户选择产品
        /// </summary>
        /// <returns></returns>
        public string OrderProductSelect()
        {
            ResultObj result = new ResultObj();
            string _ordercode = FormString.SafeQ("code");
            string _producselect = FormString.SafeQ("producselect");
            VWInquiryOrderEntity order = InquiryOrderBLL.Instance.GetVWInquiryOrderByCode(_ordercode);
            if (order != null && (order.Status == (int)OrderInquiryStatusEnum.WaitAccept )&& order.MemId == memid)
            {
                Random rd = new Random();
                string confirmcode = XTCodeBLL.Instance.GetCodeFromProc(XTCodeType.OrderDayNo) + rd.Next(100, 999).ToString();

                int resulrow = CGQuotedPriceBLL.Instance.UserSelectQuote(_ordercode, confirmcode, _producselect);
                if (resulrow > 0)
                {
                    result.Status = (int)CommonStatus.Success;
                    result.Obj = confirmcode;
                    return JsonJC.ObjectToJson(result);
                }
            }
            else
            {
                result.Status = (int)CommonStatus.Fail;
                result.Obj = _ordercode;
            }
            result.Status = (int)CommonStatus.Fail;
            result.Obj = _ordercode;
            return JsonJC.ObjectToJson(result);
        }
        public ActionResult InquiryOrderList()
        {
            int _status = QueryString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeCGOrderMobile;
            int pageindex = QueryString.IntSafeQ("px");
            if (pageindex == 0) pageindex = 1;
            string key = QueryString.SafeQ("k");
            int record = 0;
            IList<VWInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetInquiryOrderList(pagesize, pageindex, ref record, memid, _status, -1, key);
            ViewBag.InquiryOrderList = list;
            ViewBag.Status = _status;
            ViewBag.KeyWord = key;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageNum = maxpage;
            return View();
        }
        public string InquiryOrderListJeson()
        {
            ListObj result = new ListObj();
            int _status = FormString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeCGOrderMobile;
            int pageindex = FormString.IntSafeQ("px");
            if (pageindex == 0) pageindex = 1;
            string key = FormString.SafeQ("k");
            if (pageindex == 0) pageindex = 1;
            int record = 0;
            IList<VWInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetInquiryOrderList(pagesize, pageindex, ref record, memid, _status, -1, key);
            result.Total = record;
            result.List = list;
            ViewBag.Status = _status;
            return JsonJC.ObjectToJson(result);
        }
        public ActionResult InquiryOrderDetail()
        {

            string _code = QueryString.SafeQ("code");
            VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(_code);
            ViewBag.VWInquiryOrder = vworder;

            return View();
        }

      
        public ActionResult QuoteDetailRead()
        {
            return View();
        }
        #endregion

    }

}
