using SuperMarket.BLL.Cookie;
using SuperMarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
namespace SuperMarket.BLL.MemberDB
{
    public class SessionUtil
    {
        #region 配送地址Session
        //public static void SetAddrSession(Model.KH_CusAddressEntity addressEntity)
        //{
        //    HttpContext.Current.Session["OrderAddress"] = addressEntity;
        //}

        //public static Model.KH_CusAddressEntity GetAddrSession()
        //{
        //    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[Core.SysCookieName.ComBineCart];
        //    string result = "0";
        //    if (cookie != null)
        //    {
        //        result = cookie.Value;
        //    }
        //    if (result == "1")  
        //    {
        //        RemoveAddrSession();
        //        return null;
        //    }
        //    Mola.Model.KH_CusAddressEntity addressEntity = HttpContext.Current.Session["OrderAddress"] as Mola.Model.KH_CusAddressEntity;
        //    if (addressEntity != null)
        //    {  
        //        if (addressEntity.Address.Trim() == "" || addressEntity.AccepterName.Trim() == "" || addressEntity.CountryCode.Trim() == "")
        //        {
        //            Mola.BLL.KH_CusAddress.Instance.DeleteMyAddressByCondition(addressEntity.Id.ToString());
        //            SessionUtil.RemoveAddrSession();
        //            addressEntity = null;
        //        }
        //    }

        //    return addressEntity;
        //}

        //public static void RemoveAddrSession()
        //{
        //    HttpContext.Current.Session.Remove("OrderAddress");
        //}

        #endregion 


        #region 购物车Session
        public static void SetCartSession(VWShoppingCartInfo shoppingcart)
        { 
            //HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies["ShoppingCartKey"];
            //string ShoppingCartKey = "";
            //if (cookie != null)
            //{
            //    ShoppingCartKey = cookie.Value;
            //}
            //if (ShoppingCartKey == "")
            //{
            //    ShoppingCartKey = Guid.NewGuid().ToString();
            //    if (Core.CookieUtil.Domain != "")
            //    {
            //        System.Web.HttpContext.Current.Response.Cookies["ShoppingCartKey"].Domain = Core.CookieUtil.Domain;
            //    }
            //    System.Web.HttpContext.Current.Response.Cookies["ShoppingCartKey"].Value = Guid.NewGuid().ToString();
            //    System.Web.HttpContext.Current.Response.Cookies["ShoppingCartKey"].Expires = DateTime.Now.AddYears(30);
            //} 
            //Core.MemCache.AddCache(ShoppingCartKey, shoppingcart);

             
            //Cache版本
            //HttpContext.Current.Session["ShoppingCart"] = shoppingcart; 
        }

        public static VWShoppingCartInfo GetCartSession()
        {
            #region 重新获取购物车SESSSION
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[Core.SysCookieName.ComBineCart];
            string result = "0";
            if (cookie != null)
            {
                result = cookie.Value;
            }
            if (result == "1")
            { 
                RemoveCartSession();
                ShoppingCartProcessor.WriteShoppingCartCookieByLogin();
                CookieBLL.ComBineCart(0);
            }
            #endregion  
            VWShoppingCartInfo shoppingCart =  ShoppingCartProcessor.GetShoppingCart();
            return shoppingCart;
        }

        public static void RemoveCartSession()
        {
            System.Web.HttpContext.Current.Session.Remove("ShoppingCart");
        }

        #endregion

        #region 需求清单
        public static VWShoppingCartInfo GetXuQiuSession()
        {
            #region 重新获取购物车SESSSION
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[Core.SysCookieName.ComBineXuQiu];
            string result = "0";
            if (cookie != null)
            {
                result = cookie.Value;
            }
            if (result == "1")
            {
                RemoveXuQiuSession();
                ShoppingXuQiuProcessor.WriteShoppingXuQiuCookieByLogin();
                CookieBLL.ComBineCartXuQiu(0);
            }
            #endregion  
            VWShoppingCartInfo shoppingCart = ShoppingXuQiuProcessor.GetShoppingXuQiu();
            return shoppingCart;
        }
        public static void RemoveXuQiuSession()
        {
            System.Web.HttpContext.Current.Session.Remove("ShoppingXuQiu");
        }
        #endregion

    }
}
