using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.PayDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Pay.WeiXin;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class SysFinanceController : BaseMemAdminController
    {

        public ActionResult Index()
        {
            return View();
        }
        #region 品牌
        public ActionResult PayOrderList()
        {
            string _paycode = QueryString.SafeQ("paycode"); 
            string _syscode = QueryString.SafeQ("syscode");
            int _paymethod = QueryString.IntSafeQ("paymethod",-1);
            int _systype = QueryString.IntSafeQ("systype", -1);
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeBrand;
            int _recordCount = 0;
           
            IList<VWPayOrderEntity> entitylist = PayOrderBLL.Instance.GetVWPayOrderList(_pagesize, _pageindex, ref _recordCount, _paycode, _syscode, _paymethod, _systype);

            ViewBag.entitylist = entitylist;
            string _url = "/SysFinance/PayOrderList?systype=" + _systype + "&paymethod" + _paymethod+ "&paycode"+ _paycode + "&syscode" + _syscode;

            string PageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = PageStr; 
            return View();
        }

        public ActionResult PayReback()
        { 
            string _paycode = QueryString.SafeQ("paycode");
            VWPayOrderEntity enrity = PayOrderBLL.Instance.GetVWPayOrderByPayCode(_paycode);
            ViewBag.Entity = enrity;
            return View();
        }
        public string PayRebackSubmit()
        {

            ResultObj _obj = new ResultObj();
            string paycode = FormString.SafeQ("paycode");
            string rebackremark = FormString.SafeQ("rebackremark");
            decimal rebackprice = FormString.DecimalSafeQ("rebackprice");
            VWPayOrderEntity order = PayOrderBLL.Instance.GetVWPayOrderByPayCode(paycode);
            IList<PayOrderRebackEntity> rebacklist = PayOrderRebackBLL.Instance.GetRebackByPayCode(paycode);
            decimal hasreback = 0;
            if(rebacklist!=null&& rebacklist.Count>0)
            {
                foreach(PayOrderRebackEntity reback in rebacklist)
                {
                    hasreback += reback.RebackPrice;
                }
            } 
            if(order.PayPrice- hasreback>= rebackprice)//金额满足退款条件（退款金额<=付款金额)
            {
                PayOrderRebackEntity reback = new PayOrderRebackEntity();
                reback.ExternalCode = order.ExternalCode;
                reback.PayMethod = order.PayMethod;
                reback.PayOrderCode = order.PayOrderCode;
                reback.TotalPrice = order.PayPrice;  
                reback.RebackPrice = rebackprice; 
                reback.RebackRemark = rebackremark;
                reback.Status = 0;
                reback.SysOrderCode = order.SysOrderCode;
                reback.SysType = order.SysType;
                PayOrderRebackEntity rebackadd= PayOrderRebackBLL.Instance.AddPayOrderReback(reback);
                if (reback.PayMethod == (int)PayType.WeChat)
                {
                    string result = Refund.Run(reback);
                    _obj.Status = (int)CommonStatus.Success;
                }
                else if (reback.PayMethod == (int)PayType.AliPay|| reback.PayMethod == (int)PayType.AliPayMobile)
                {

                    _obj.Status = (int)CommonStatus.Success;
                }

            }
            return JsonJC.ObjectToJson(_obj);
        }
        #endregion


    }
}
