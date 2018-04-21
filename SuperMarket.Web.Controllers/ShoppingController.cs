using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class ShoppingController: BaseMemberController
    {
        public ActionResult XuQiu()
        {
            XuQiuCartMethod();
            return View(); 
        }

        public void XuQiuCartMethod()
        {
            Dictionary<int, IList<MemShoppCarEntity>> shopdic = new Dictionary<int, IList<MemShoppCarEntity>>();

            int itemproductnum = 0;//产品数量
            decimal productamountall = 0;//产品总金额
            string pids = QueryString.SafeQ("pids", 500);
            string nums = QueryString.SafeQ("nums", 500);
            VWShoppingCartInfo shoppingCart = SessionUtil.GetXuQiuSession();
            //待项目完善后此段判断可删除
            if (shoppingCart != null && shoppingCart.CartItems != null && shoppingCart.CartItems.Count > 0)
            {
                shoppingCart.CartItemsL = new List<MemShoppCarEntity>();

                foreach (ShoppCookie entity in shoppingCart.CartItems)
                {
                    if (entity.ProDId <= 0)
                    {
                        ShoppingXuQiuProcessor.RemoveXuQiuItem(shoppingCart, entity.ProDId);
                    }
                    else
                    {
                        VWProductEntity vwpentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProDId);

                        MemShoppCarEntity shopcarentity = new MemShoppCarEntity();
                        shopcarentity.PicUrl = vwpentity.PicUrl;
                        shopcarentity.PicSuffix = vwpentity.PicSuffix;
                        shopcarentity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice);
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.Check = entity.C == 1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        shopcarentity.CGMemId = vwpentity.CGMemId;
                        shopcarentity.CGMemNickName = vwpentity.CGMemNickName;
                        shopcarentity.IsAhmTake = vwpentity.IsAhmTake;
                        if (!shopdic.Keys.Contains(vwpentity.CGMemId))
                        {
                            shopdic[vwpentity.CGMemId] = new List<MemShoppCarEntity>();
                        }
                        shopdic[vwpentity.CGMemId].Add(shopcarentity);
                        itemproductnum = itemproductnum + entity.Num;
                        productamountall += shopcarentity.TotalPrice;
                    }
                }
            }
            if (!string.IsNullOrEmpty(pids))
            {
                string[] pidattr = pids.Split('_');
                string[] numattr = nums.Split('_');
                IList<ShoppCookie> list = new List<ShoppCookie>();
                IList<MemShoppCarEntity> listL = new List<MemShoppCarEntity>();
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
                shoppingCart = ShoppingXuQiuProcessor.BuyContinue(shoppingCart, list);
                shoppingCart.CartItemsL = new List<MemShoppCarEntity>();

                foreach (ShoppCookie entity in shoppingCart.CartItems)
                {
                    if (entity.ProDId <= 0)
                    {
                        ShoppingXuQiuProcessor.RemoveXuQiuItem(shoppingCart, entity.ProDId);
                    }
                    else
                    {
                        VWProductEntity vwpentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProDId);
                        MemShoppCarEntity shopcarentity = new MemShoppCarEntity();
                        shopcarentity.PicUrl = vwpentity.PicUrl;
                        shopcarentity.PicSuffix = vwpentity.PicSuffix;
                        shopcarentity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice);
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.Check = entity.C == 1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        shopcarentity.CGMemId = vwpentity.CGMemId;
                        shopcarentity.CGMemNickName = vwpentity.CGMemNickName;
                        shopcarentity.IsAhmTake = vwpentity.IsAhmTake;
                        if (!shopdic.Keys.Contains(vwpentity.CGMemId))
                        {
                            shopdic[vwpentity.CGMemId] = new List<MemShoppCarEntity>();
                        }
                        shopdic[vwpentity.CGMemId].Add(shopcarentity);
                        itemproductnum = itemproductnum + entity.Num;
                        productamountall += shopcarentity.TotalPrice;
                    }
                }
            }
            ViewBag.ShoppingCartXuQiu = shopdic;
            ViewBag.ItemNum = itemproductnum;
            ViewBag.AmountAll = productamountall;
        }
        public ActionResult Cart()
        {
            string pids = QueryString.SafeQ("pids",500);
            string nums = QueryString.SafeQ("nums",500);
            VWShoppingCartInfo shoppingCart = SessionUtil.GetCartSession();
            //待项目完善后此段判断可删除
            if(shoppingCart!=null&&shoppingCart.CartItems!=null&& shoppingCart.CartItems.Count>0)
            {
                shoppingCart.CartItemsL = new List<MemShoppCarEntity>();

                foreach (ShoppCookie entity in shoppingCart.CartItems)
                {
                    if(entity.ProDId <= 0)
                    {
                        ShoppingCartProcessor.RemoveCartItem(shoppingCart,entity.ProDId);
                    }
                    else
                    { 
                        VWProductEntity vwpentity = ProductBLL.Instance.GetProVWByDetailId(entity.ProDId);
                        MemShoppCarEntity shopcarentity = new MemShoppCarEntity();
                        shopcarentity.PicUrl = vwpentity.PicUrl;
                        shopcarentity.PicSuffix = vwpentity.PicSuffix;
                        shopcarentity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice); 
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.Check = entity.C==1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        shopcarentity.CGMemId = vwpentity.CGMemId;
                        shopcarentity.CGMemNickName = vwpentity.CGMemNickName;
                        shopcarentity.IsAhmTake = vwpentity.IsAhmTake;
                        shoppingCart.CartItemsL.Add(shopcarentity); 
                    }
                }
            }
            if (!string.IsNullOrEmpty(pids))
            {
                string[] pidattr = pids.Split('_');
                string[] numattr = nums.Split('_');
                IList<ShoppCookie> list = new List<ShoppCookie>();
                IList<MemShoppCarEntity> listL = new List<MemShoppCarEntity>();
                for (int i=0;i< pidattr.Length; i++)
                {
                    VWProductEntity _productentity = ProductBLL.Instance.GetProVWByDetailId(StringUtils.GetDbInt(pidattr[i]));
                    if(_productentity.ProductDetailId>0)
                    {
                        ShoppCookie cookentity = new ShoppCookie();
                        cookentity.C = 1;
                        cookentity.Num = StringUtils.GetDbInt(numattr[i]);
                        cookentity.ProDId = _productentity.ProductDetailId;
                        list.Add(cookentity); 
                    }
                   
                } 
                shoppingCart= ShoppingCartProcessor.BuyContinue(shoppingCart, list);
                shoppingCart.CartItemsL = new List<MemShoppCarEntity>();

                foreach (ShoppCookie entity in shoppingCart.CartItems)
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
                        shopcarentity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, vwpentity.TradePrice, vwpentity.Price, vwpentity.IsBP, vwpentity.DealerPrice);
                        shopcarentity.ProductName = vwpentity.AdTitle;
                        shopcarentity.Spec1 = vwpentity.Spec1;
                        shopcarentity.Spec2 = vwpentity.Spec2;
                        shopcarentity.Spec3 = vwpentity.Spec3;
                        shopcarentity.TotalPrice = shopcarentity.Price * entity.Num;
                        shopcarentity.Num = entity.Num;
                        shopcarentity.ProductType = vwpentity.ProductType;
                        shopcarentity.ProductId = vwpentity.ProductId;
                        shopcarentity.Check = entity.C==1;
                        shopcarentity.ProductDetailId = entity.ProDId;
                        shopcarentity.CGMemId = vwpentity.CGMemId;
                        shopcarentity.CGMemNickName = vwpentity.CGMemNickName;
                        shopcarentity.IsAhmTake = vwpentity.IsAhmTake;
                        shoppingCart.CartItemsL.Add(shopcarentity);
                    }
                }
            }
         
         
            //int memid = 0; int issupplier = 0;
            //MemberLoginEntity member = CookieBLL.GetLoginCookie(); 
            //if (member != null && member.MemId > 0)
            //{
            //    memid = member.MemId ;
            //    issupplier = member.IsStore;
            //} 
            ViewBag.ShoppingCart = shoppingCart;
            ViewBag.MemId = memid; 
            return View();
        }

        public ActionResult AddToCart()
        { 
            int _pdid = QueryString.IntSafeQ("pdid");
            int _num = QueryString.IntSafeQ("num");
            VWProductEntity entity = new VWProductEntity(); 
            entity = ProductBLL.Instance.GetProVWByDetailId(_pdid);
            ViewBag.IsAhaTake = entity.IsAhmTake;
            ViewBag.Num = _num;
            ViewBag.Entity = entity;
            return View();
        }

    }
}
