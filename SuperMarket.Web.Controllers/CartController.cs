using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class CartController : BaseMemberController
    {
        #region 展示

        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult ShopCart()
        {
            string pids = QueryString.SafeQ("pids", 500);
            string nums = QueryString.SafeQ("nums", 500);  
            VWShoppingCartInfo shoppingCart = new VWShoppingCartInfo();

            shoppingCart = SessionUtil.GetCartSession();


            IList<MemShoppCarEntity> clist = new List<MemShoppCarEntity>();

            if (shoppingCart != null && shoppingCart.CartItems != null && shoppingCart.CartItems.Count > 0)
            {
                IList<ShoppCookie> shopcoolist = shoppingCart.CartItems.OrderByDescending(c => c.C).ThenByDescending(c => c.S).ToList();
                //shoppingCart.CartItemsL = new List<ShoppCarEntity>();
                foreach (ShoppCookie entity in shopcoolist)
                {
                    if (entity.ProDId <= 0)
                    {

                        ShoppingCartProcessor.RemoveCartItem(shoppingCart, entity.ProDId);

                    }
                    else
                    {
                        VWProductEntity vwpentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProDId);
                        MemShoppCarEntity shopcarentity = new MemShoppCarEntity();
                        shopcarentity.PicUrl = vwpentity.PicUrl;
                        shopcarentity.PicSuffix = vwpentity.PicSuffix;
                        shopcarentity.Price = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice);
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.StockNum = vwpentity.StockNum;
                        shopcarentity.Check = entity.C == 1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        clist.Add(shopcarentity);
                    }
                }
                shoppingCart.CartItemsL = clist;
            }
            if (!string.IsNullOrEmpty(pids))
            {
                string[] pidattr = pids.Split('_');
                string[] numattr = nums.Split('_');
                IList<ShoppCookie> list = new List<ShoppCookie>();
                for (int i = 0; i < pidattr.Length; i++)
                {
                    VWProductEntity _productentity = ProductBLL.Instance.GetProVWByDetailId(StringUtils.GetDbInt(pidattr[i]));
                    if (_productentity.ProductDetailId > 0)
                    {
                        ShoppCookie cookentity = new ShoppCookie();
                        cookentity.C = 1;
                        cookentity.Num = StringUtils.GetDbInt(numattr[i]);
                        cookentity.ProDId = _productentity.ProductDetailId;
                        list.Add(cookentity);
                    }

                }

                shoppingCart = ShoppingCartProcessor.BuyContinue(shoppingCart, list);


                clist = new List<MemShoppCarEntity>();
                IList<ShoppCookie> shopcoolist = shoppingCart.CartItems.OrderByDescending(c => c.C).ThenByDescending(c => c.S).ToList();

                foreach (ShoppCookie entity in shopcoolist)
                {
                    if (entity.ProDId <= 0)
                    {

                        ShoppingCartProcessor.RemoveCartItem(shoppingCart, entity.ProDId);

                    }
                    else
                    {
                        VWProductEntity vwpentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProDId);
                        MemShoppCarEntity shopcarentity = new MemShoppCarEntity();
                        shopcarentity.PicUrl = vwpentity.PicUrl;
                        shopcarentity.PicSuffix = vwpentity.PicSuffix;
                        shopcarentity.Price = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice);
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.StockNum = vwpentity.StockNum;
                        shopcarentity.Check = entity.C == 1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        clist.Add(shopcarentity);
                    }
                }
                shoppingCart.CartItemsL = clist;
            }
            ViewBag.ShoppingCart = shoppingCart;
            ViewBag.MemId = memid;

            ViewBag.PageMenu = "3";
            return View();
        }
        //public ActionResult PreOrder()
        //{
        //    //PostAddressEntity address= PostAddressBLL.Instance.GetDefaultPostAddress(memid);
        //    long _preordercode = QueryString.LongIntSafeQ("code");
        //    int _jishi = QueryString.IntSafeQ("js");
        //    if (_jishi == (int)JiShiSongEnum.JiShi)
        //    {

        //    }
        //    else
        //    {
        //        ///可正常销售的产品
        //        IList<OrderDetailPreTempEntity> _listproduct = OrderDetailPreTempBLL.Instance.GetOrderPreTempByCode(_preordercode, 1);

        //        //不能正常销售的产品
        //        IList<OrderDetailPreTempEntity> _listproductless = OrderDetailPreTempBLL.Instance.GetOrderPreTempByCode(_preordercode, 0);
        //        ViewBag.ProductList = _listproduct;
        //        ViewBag.ProductListLess = _listproductless;
        //        ViewBag.MemId = member.MemId;
        //        ViewBag.PreOrderCode = _preordercode;
        //        VWOrderEntity _order = OrderCommonBLL.Instance.GetTransFeeDisCount(_listproduct);
        //        ViewBag.Order = _order;

        //        IntegralEntity _entity = IntegralBLL.Instance.GetIntegralByMemId(memid);
        //        ViewBag.Integral = _entity;
        //    }

        //    return View();
        //}
        #endregion

    }
}
