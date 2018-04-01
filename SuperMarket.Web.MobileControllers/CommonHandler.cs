using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using SuperMarket.Core.Safe;
using SuperMarket.Core;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Model;
using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.Model.Account;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.Cookie;
using SuperMarket.Core.Json;
using SuperMarket.Core.Util;
using SuperMarket.Web.CommonControllers;

namespace SuperMarket.Web.MemberControllers
{
    public class CommonHandlerController : BaseCommonController
    {
        public ActionResult CommonHandler()
        {
            //在此写入您的处理程序实现。

            Response.AddHeader("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            Response.CacheControl = "no-cache";
            Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
            Response.Expires = 0;

            try
            {
                string action = QueryString.SafeQ("action").ToLower();
                switch (action)
                {
                    case "getdefaultcusname":
                        return GetDefaultCusName();
                    case "getshoppingcartcount":
                        return GetShoppingCartCount();
                    case "getshoppingcartxuqiucount":
                        return GetShoppingCartXuQiuCount();
                    case "addtocart":
                        return ProcessAddToCart();
                    case "addtoxuqiu":
                        return ProcessAddToXuQiu();
                    case "delfromcart":
                        return ProcessDelFromCart();
                    case "delfromcartxuqiu":
                        return ProcessDelFromCartXuQiu();
                    case "updatecartnum":
                        return ProcessUpdateCartNum();
                    case "updatecartxuqiunum":
                        return ProcessUpdateCartXuQiuNum();
                    case "updatecartcheck":
                        return ProcessUpdateCartChecked();
                    case "delallfromcart":
                        return ProcessDeleteAllFromCart();
                    case "delallfromcartxuqiu":
                        return ProcessDeleteAllFromCartXuQiu(); 
                    case "delpdsfromcart":
                        return ProcessDeletePdsFromCart();
                    case "delpdsfromcartxuqiu":
                        return ProcessDeletePdsFromCartXuQiu();
                    case "getloginuser":
                        return GetMemberLogin();
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return Content(string.Empty);

        }
        private ActionResult GetDefaultCusName()
        {
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
                                                            //System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.DefaultCusName];
                                                            //string result = "";
                                                            //if (cookie != null)
                                                            //{
                                                            //    result = Server.UrlDecode(cookie.Value);
                                                            //}
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            string result = "";

            if (member != null)
            {
                if (string.IsNullOrEmpty(member.NickName)) { result = member.MobilePhone; } else { result = member.NickName; }
            }
            if (result != "")
            {
                result = Server.UrlDecode(result);
            }
            return Content(callback + "('" + result + "')");
        }


        private ActionResult GetShoppingCartCount()
        {
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingCartCount_Data];
            string result = "0";
            if (cookie != null)
            {
                result = cookie.Value;
            }

            return Content(callback + "('" + result + "')");
        }

        private ActionResult GetShoppingCartXuQiuCount()
        {
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiuCount_Data];
            string result = "0";
            if (cookie != null)
            {
                result = cookie.Value;
            }

            return Content(callback + "('" + result + "')");
        }

        /// <summary>
        /// 添加单品
        /// </summary>
        /// <param name="context"></param>
        private ActionResult ProcessAddToCart()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            int prodetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num"); 
            if (prodetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择商品\"});");
            } 
            if (ProductDetailBLL.Instance.GetProductDetail(prodetailid).StockNum < num)
            {
                return Content(callback + "({\"Status\":\"库存数量不足\"})");
            } 
            else
            {
                ShoppCookie _entity = new ShoppCookie();
                _entity.C  = 1;
                _entity.Num = num;
                _entity.ProDId = prodetailid; 
                ShoppingCartProcessor.AddToCart(shoppingCart, _entity); 
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessAddToXuQiu()
        {
            #region 需求清单
            VWShoppingCartInfo shoppingCart = SessionUtil.GetXuQiuSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            int prodetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num");
            if (prodetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择商品\"});");
            }
            if (ProductDetailBLL.Instance.GetProductDetail(prodetailid).StockNum < num)
            {
                return Content(callback + "({\"Status\":\"库存数量不足\"})");
            }
            else
            {
                ShoppCookie _entity = new ShoppCookie();
                _entity.C = 1;
                _entity.Num = num;
                _entity.ProDId = prodetailid;
                ShoppingXuQiuProcessor.AddToXuQiu(shoppingCart, _entity);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }

        private ActionResult ProcessDelFromCart()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数  
            int prodetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num");

            if (prodetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                ShoppingCartProcessor.RemoveCartItem(shoppingCart, prodetailid);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessDelFromCartXuQiu()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetXuQiuSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数  
            int prodetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num");

            if (prodetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                ShoppingXuQiuProcessor.RemoveXuQiuItem(shoppingCart, prodetailid);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessDeletePdsFromCart()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数  
            string prodetailids = QueryString.SafeQ("pdids",8000);
            int num = QueryString.IntSafeQ("Num");

            if (string.IsNullOrEmpty(prodetailids) )
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                List<int> pdidslist = new List<int>();

                string[] pdisattr = prodetailids.Split(',');
                foreach(string str in pdisattr)
                {
                    pdidslist.Add(StringUtils.GetDbInt(str));
                }
                ShoppingCartProcessor.RemoveCartItems(shoppingCart, pdidslist);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessDeletePdsFromCartXuQiu()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetXuQiuSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数  
            string prodetailids = QueryString.SafeQ("pdids", 8000);
            int num = QueryString.IntSafeQ("Num");

            if (string.IsNullOrEmpty(prodetailids))
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                List<int> pdidslist = new List<int>();

                string[] pdisattr = prodetailids.Split(',');
                foreach (string str in pdisattr)
                {
                    pdidslist.Add(StringUtils.GetDbInt(str));
                }
                ShoppingXuQiuProcessor.RemoveCartXuQiuItems(shoppingCart, pdidslist);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessUpdateCartNum()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            int productdetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num");

            if (productdetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                ShoppingCartProcessor.UpdateCarItemQty(shoppingCart, productdetailid, num);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        private ActionResult ProcessUpdateCartXuQiuNum()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetXuQiuSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            int productdetailid = QueryString.IntSafeQ("prodetailid");
            int num = QueryString.IntSafeQ("Num");

            if (productdetailid == 0)
            {
                return Content(callback + "({\"Status\":\"请选择需要删除的商品\"});");
            }
            else
            {
                ShoppingXuQiuProcessor.UpdateCarItemQty(shoppingCart, productdetailid, num);
                return Content(callback + "({\"Status\":\"OK\"});");
            }
        }
        
        private ActionResult ProcessUpdateCartChecked()
        {
            #region 购物车
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            #endregion

            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            int productdetailid = QueryString.IntSafeQ("prodetailid");
            int check = QueryString.IntSafeQ("checkstatus") ;
            if (productdetailid > 0)
            {
                ShoppingCartProcessor.UpdateCarIteChecked(shoppingCart, productdetailid, check);
            }
            return Content(callback + "({\"Status\":\"OK\"});");
        }


        /// <summary>
        /// 删除全部产品
        /// </summary>
        /// <returns></returns>
        private ActionResult ProcessDeleteAllFromCart()
        {
            ShoppingCartProcessor.ClearShoppingCart();
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
            return Content(callback + "({\"Status\":\"OK\"})");

        }
        /// <summary>
        /// 删除全部产品
        /// </summary>
        /// <returns></returns>
        private ActionResult ProcessDeleteAllFromCartXuQiu()
        {
            ShoppingXuQiuProcessor.ClearShoppingXuQiu();
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
            return Content(callback + "({\"Status\":\"OK\"})");

        }
        
        public ActionResult GetMemberLogin()
        {
            string result = "";
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if(member!=null&&  member.MemId > 0)
            {
                VWMemberEntity _mementity = MemberBLL.Instance.GetVWMember(member.MemId);
                result= JsonJC.ObjectToJson(_mementity);
            }
            string callback = QueryString.SafeQ("callback");//jsonp回调函数
            return Content(callback + "(" + result + ")");
        }
    }
}
