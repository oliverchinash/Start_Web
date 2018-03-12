﻿using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Net;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Xml;
using SuperMarket.Core;
using SuperMarket.Model;
using SuperMarket.Core.Json;
using SuperMarket.Core.Cookie;
using SuperMarket.BLL.Cookie;
using SuperMarket.Model.Account;
using SuperMarket.Core.Util;
using System.Threading; 
namespace SuperMarket.BLL.MemberDB
{
    public class ShoppingXuQiuProcessor
    {
        /// <summary>
        /// 获取购物车数据
        /// </summary>
        /// <returns></returns>
        public static VWShoppingCartInfo GetShoppingXuQiu()
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];

            if (cookie == null || cookie.Value == "" || cookie.Value == "[]")
            {
                return GetShoppingXuQiuByData();
            }
            string cookieValue = HttpUtility.UrlDecode(cookie.Value, Encoding.GetEncoding("UTF-8"));
            if (cookieValue.Contains("ProductName") || cookieValue.Contains("ProductDetailId"))
            {
                if (cookie != null)
                {
                    cookie.Expires = DateTime.Now.AddDays(-1);
                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                }
                SetShoppingXuQiuCount(0);
                cookieValue = "[]";
            }
            IList<ShoppCookie> itemsCart = JsonJC.JsonToObject<List<ShoppCookie>>(cookieValue); 
            return GetShoppingXuQiu(itemsCart);
        }

        /// <summary>
        /// 根据数据库获取购物车
        /// </summary>
        /// <returns></returns>
        public static VWShoppingCartInfo GetShoppingXuQiuByData()
        {
            VWShoppingCartInfo _info = new VWShoppingCartInfo();
            SuperMarket.Model.Account.MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                if (SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.XuQiuCookie == 1)
                {
                    IList<ShoppCookie> listcookie = new List<ShoppCookie>();

                    IList<ShoppCarEntity> list = new List<ShoppCarEntity>();
                    string cookieval = WZShopCartBLL.Instance.GetXuQiuCookie(member.MemId);
                    if (cookieval != "")
                    {
                        string cookieValue = HttpUtility.UrlDecode(cookieval, Encoding.GetEncoding("UTF-8"));
                        //string cookieValue = cookieval;
                        if (cookieValue.Contains("ProductName")|| cookieValue.Contains("ProductDetailId"))
                        {
                            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
                            if (cookie != null)
                            {
                                cookie.Expires = DateTime.Now.AddDays(-1);
                                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                            }
                            SetShoppingXuQiuCount(0);
                        }
                        else
                        {

                            listcookie = JsonJC.JsonToObject<List<ShoppCookie>>(cookieValue);
                        }

                    }
                    if (listcookie != null && listcookie.Count > 0)
                    {
                        _info.CartItems = listcookie;
                        WriteShoppingXuQiuCookie(SuperMarket.Core.Json.JsonJC.ObjectToJson(listcookie), _info.CartCount);
                        return _info;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 获取购物车
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        public static VWShoppingCartInfo GetShoppingXuQiu(IList<ShoppCookie> shoppingCart)
        {
            VWShoppingCartInfo _info = new VWShoppingCartInfo();
            if (shoppingCart.Count > 0)
            {
                SuperMarket.Model.Account.MemberLoginEntity member = CookieBLL.GetLoginCookie();
                VWShoppingCartInfo shoppingXuQiuInfo = new VWShoppingCartInfo();

                shoppingXuQiuInfo.CartItems = shoppingCart;
                return shoppingXuQiuInfo;
            }
            return null;
        }

        /// <summary>
        /// 添加购物车单品
        /// </summary>
        /// <param name="shoppingcart">购物车对象</param>
        /// <param name="wareCode">产品编号</param>
        /// <param name="Qty">数量</param>
        /// <param name="webSiteId">站点编号</param>
        public static VWShoppingCartInfo BuyContinue(VWShoppingCartInfo shoppingcart, IList<ShoppCookie> _entitycart)
        {
            int boo_i = 0;
            if (shoppingcart != null && shoppingcart.CartItems.Count > 0)
            {
          
                foreach (ShoppCookie item in shoppingcart.CartItems)
                {
                    item.C = 0;
                    foreach (ShoppCookie additem in _entitycart)
                    {
                        if (item.ProDId == additem.ProDId)
                        {
                            item.C  = 1;
                            item.Num = additem.Num;
                            item.S = boo_i;
                            boo_i += 1;
                            _entitycart.Remove(additem);
                            break;
                        }
                    }
                }
                if (shoppingcart.CartItems.Count+ _entitycart.Count > CommonKey.CartMaxNum  )
                {
                    for (int i = 0; i < shoppingcart.CartItems.Count+ _entitycart.Count - CommonKey.CartMaxNum  ; i++)
                    {
                        shoppingcart.CartItems.RemoveAt(0);
                    }
                }
                if (_entitycart.Count > 0)
                {
                    foreach (ShoppCookie _additem in _entitycart)
                    {
                        _additem.C  = 1;
                        _additem.S = boo_i;
                        boo_i += 1;
                        shoppingcart.CartItems.Add(_additem);
                    }
                }
            }
            else
            {
                if (shoppingcart == null)
                {
                    shoppingcart = new VWShoppingCartInfo();
                }
                if (shoppingcart.CartItems == null)
                {
                    shoppingcart.CartItems = new List<ShoppCookie>();
                }
                foreach (ShoppCookie _additem in _entitycart)
                {
                    _additem.C = 1;
                    _additem.S = boo_i;
                    boo_i += 1; 
                }
                shoppingcart.CartItems = _entitycart;
            } 
            ShoppingXuQiuProcessor.WriteShoppingXuQiuCookie(shoppingcart);
            return shoppingcart;
        }
        public static void AddToXuQiu(VWShoppingCartInfo shoppingcart, ShoppCookie _entitycart)
        {
            bool boo_i = true;
            int sort_i = 0;
            if (shoppingcart != null && shoppingcart.CartItems.Count > 0)
            {
                foreach (ShoppCookie item in shoppingcart.CartItems)
                { 
                    if (item.ProDId == _entitycart.ProDId)
                    {
                        item.C  = 1; 
                        item.Num += _entitycart.Num;
                        boo_i = false;
                        break;
                    }
                    item.S= sort_i++;
                }
            }
            if (boo_i)
            {
                if (shoppingcart == null)
                {
                    shoppingcart = new VWShoppingCartInfo();
                }
                if (shoppingcart.CartItems == null)
                {
                    shoppingcart.CartItems = new List<ShoppCookie>();
                }
                if (shoppingcart.CartItems.Count >= CommonKey.CartMaxNum)
                {
                    for (int i = 0; i <= shoppingcart.CartItems.Count   - CommonKey.CartMaxNum; i++)
                    {
                        shoppingcart.CartItems.RemoveAt(0);
                    }
                }

                _entitycart.S = sort_i;
                shoppingcart.CartItems.Add(_entitycart);
            } 
            ShoppingXuQiuProcessor.WriteShoppingXuQiuCookie(shoppingcart);
        }

        public static void BuyImmediately(VWShoppingCartInfo shoppingcart, ShoppCookie _entitycart)
        {
            bool boo_i = true;
            int sort_i = 0;
            if (shoppingcart != null && shoppingcart.CartItems.Count > 0)
            {
                foreach (ShoppCookie item in shoppingcart.CartItems)
                {
                    item.C  = 0;
                    if (item.ProDId == _entitycart.ProDId)
                    {
                        item.C = 1;
                        item.Num = _entitycart.Num;
                        boo_i = false;
                        break;
                    }
                    item.S = sort_i++;
                }
            }
            if (boo_i)
            {
                if (shoppingcart == null)
                {
                    shoppingcart = new VWShoppingCartInfo();
                }
                if (shoppingcart.CartItems == null)
                {
                    shoppingcart.CartItems = new List<ShoppCookie>();
                }
                _entitycart.C = 1;
                _entitycart.S = sort_i++;
                shoppingcart.CartItems.Add(_entitycart);
            }
            ShoppingXuQiuProcessor.WriteShoppingXuQiuCookie(shoppingcart);
        }
        /// <summary>
        /// 修改购物车商品数量
        /// </summary>
        /// <param name="wareCode"></param>
        /// <param name="qty"></param>
        public static string UpdateCarItemQty(VWShoppingCartInfo shoppingcart, int productdetailid, int qty)
        {
            string strMsg = null;
            //修改购物车对象
            for (int i = shoppingcart.CartItems.Count - 1; i >= 0; i--)
            {
                if (shoppingcart.CartItems[i].ProDId == productdetailid)
                {
                    shoppingcart.CartItems[i].Num = qty;
                    break;
                }
            }
            WriteShoppingXuQiuCookie(shoppingcart);
            return strMsg;
        }

        public static string UpdateCarIteChecked(VWShoppingCartInfo shoppingcart, int productdetailid, int check)
        {
            string strMsg = null;
            //修改购物车对象
            for (int i = shoppingcart.CartItems.Count - 1; i >= 0; i--)
            {
                if (shoppingcart.CartItems[i].ProDId == productdetailid)
                {
                    shoppingcart.CartItems[i].C  = check;
                    break;
                }
            }
            WriteShoppingXuQiuCookie(shoppingcart);
            return strMsg;
        }
        /// <summary>
        /// 删除购物车数据
        /// </summary>
        /// <param name="wareCode"></param>               
        public static void RemoveXuQiuItem(VWShoppingCartInfo shoppingcart, int prodetailid)
        {
            if (shoppingcart == null)
            {
                return;
            }
            for (int i = shoppingcart.CartItems.Count - 1; i >= 0; i--)
            {
                if (shoppingcart.CartItems[i].ProDId == prodetailid)
                {
                    shoppingcart.CartItems.RemoveAt(i);
                    break;
                }
            } 
            WriteShoppingXuQiuCookie(shoppingcart);
        }
        public static void RemoveCartXuQiuItems(VWShoppingCartInfo shoppingcart, List<int> pdids)
        {
            if (shoppingcart == null)
            {
                return;
            }
            foreach (int entityid in pdids)
            {
                for (int i = shoppingcart.CartItems.Count - 1; i >= 0; i--)
                {
                    if (shoppingcart.CartItems[i].ProDId == entityid)
                    {
                        shoppingcart.CartItems.RemoveAt(i);
                        break;
                    }
                }
            } 
            WriteShoppingXuQiuCookie(shoppingcart);
        }
        /// <summary>
        /// 生成购物车cookie
        /// </summary>
        /// <param name="shoppingCart"></param>
        public static void WriteShoppingXuQiuCookie(VWShoppingCartInfo shoppingcart)
        {
            if (shoppingcart == null)
            {
                return;
            }
            string jsonRst = SuperMarket.Core.Json.JsonJC.ObjectToJson(shoppingcart.CartItems);

            //保存购物车cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
            if (cookie == null)
            {
                cookie = new System.Web.HttpCookie(SysCookieName.ShoppingXuQiu_Data);
            }
            cookie.Value = HttpUtility.UrlEncode(jsonRst, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = DateTime.Now.AddDays(15);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //修改购物车数量cookie
            SetShoppingXuQiuCount(shoppingcart.CartCount);


            //如果登录保存COOKIE至数据库

            if (SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.XuQiuCookie == 1)
            {
                if (shoppingcart != null)
                {
                    MemberLoginEntity member = CookieBLL.GetLoginCookie();
                    if (member != null && member.MemId > 0 && jsonRst != "")
                    {
                        WZShopCartEntity model = new WZShopCartEntity();
                        model.AddDate = DateTime.Now;
                        model.BuyDate = DateTime.Now.Date;
                        model.EndDate = DateTime.Now.Date.AddDays(15);
                        model.MemId = member.MemId;
                        model.CookieValueXuQiu = jsonRst;
                        WZShopCartBLL.Instance.AddWZShopCartXuQiu(model);
                    }
                }
            }

        }

        /// <summary>
        /// 根据数据库生成购物车cookie
        /// </summary>
        /// <param name="shoppingCart"></param>
        public static void WriteShoppingXuQiuCookie(string jsonRst, int XuQiuCount)
        {
            //保存购物车cookie
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
            if (cookie == null)
            {
                cookie = new System.Web.HttpCookie(SysCookieName.ShoppingXuQiu_Data);
            }
            cookie.Value = HttpUtility.UrlEncode(jsonRst, Encoding.GetEncoding("UTF-8"));
            cookie.Expires = DateTime.Now.AddDays(15);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //修改购物车数量cookie
            SetShoppingXuQiuCount(XuQiuCount);
        }

        /// <summary>
        /// 修改购物车数量cookie
        /// </summary>
        /// <param name="count"></param>
        private static void SetShoppingXuQiuCount(int count)
        {
            System.Web.HttpCookie cookieCount = System.Web.HttpContext.Current.Response.Cookies[SysCookieName.ShoppingXuQiuCount_Data];
            if (cookieCount == null)
            {
                cookieCount = new System.Web.HttpCookie(SysCookieName.ShoppingXuQiuCount_Data);
            }
            cookieCount.Value = count.ToString();
            cookieCount.Expires = DateTime.Now.AddDays(15);
            System.Web.HttpContext.Current.Response.Cookies.Add(cookieCount);
        }
        /// <summary>
        /// 获取购物车数量
        /// </summary>
        /// <returns></returns>
        public static int GetShoppingXuQiuCount()
        {
            System.Web.HttpCookie cookieCount = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiuCount_Data];
            if (cookieCount == null)
            {
                return 0;
            }
            return StringUtils.GetDbInt(cookieCount.Value);
        }
        /// <summary>
        /// 清除购物车
        /// </summary>
        public static void ClearShoppingXuQiu()
        {
            if (SessionUtil.GetXuQiuSession() != null)
            {
                SessionUtil.RemoveXuQiuSession();
            }
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
            if (cookie != null)
            {
                cookie.Expires = DateTime.Now.AddDays(-1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            SetShoppingXuQiuCount(0);
        }

        /// <summary>
        /// 登录时合并购物车数据
        /// </summary>
        public static void WriteShoppingXuQiuCookieByLogin()
        {
            if (SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.XuQiuCookie == 0)
            {
                return;
            }
            List<ShoppCookie> shoppingCart1 = new List<ShoppCookie>();
            List<ShoppCookie> shoppingCart2 = new List<ShoppCookie>();
            List<ShoppCookie> shoppingCartnew = new List<ShoppCookie>();
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
            if (cookie != null && cookie.Value != "" && cookie.Value != "[]")
            {
                string cookieValue = HttpUtility.UrlDecode(cookie.Value, Encoding.GetEncoding("UTF-8"));
                if (cookieValue.Contains("ProductName") || cookieValue.Contains("ProductDetailId"))
                {
                    if (cookie != null)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                    }
                    SetShoppingXuQiuCount(0);
                    cookieValue = "[]";
                }
                shoppingCart1 = JsonJC.JsonToObject<List<ShoppCookie>>(cookieValue);
                shoppingCartnew = JsonJC.JsonToObject<List<ShoppCookie>>(cookieValue);
                if (shoppingCart1 != null && shoppingCart1.Count > 0)
                {
                    MemberLoginEntity member = CookieBLL.GetLoginCookie();
                    if (member != null && member.MemId > 0)
                    {
                        string cookieval = WZShopCartBLL.Instance.GetXuQiuCookie(member.MemId);
                        if (cookieval != "")
                        {
                            if (cookieval.Contains("ProductName") || cookieval.Contains("ProductDetailId"))
                            {
                                if (cookie != null)
                                {
                                    cookie.Expires = DateTime.Now.AddDays(-1);
                                    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                                }
                                SetShoppingXuQiuCount(0);
                                cookieval = "[]";
                            }
                            cookieValue = HttpUtility.UrlDecode(cookieval, Encoding.GetEncoding("UTF-8"));
                            //cookieValue =  cookieval; 
                            shoppingCart2 = JsonJC.JsonToObject<List<ShoppCookie>>(cookieValue);

                            bool exit = false;
                            if (shoppingCart2 != null && shoppingCart2.Count > 0)
                            {
                                foreach (ShoppCookie i in shoppingCart2)
                                {
                                    exit = false;
                                    foreach (ShoppCookie j in shoppingCart1)
                                    {
                                        if (j.ProDId == i.ProDId)
                                        {
                                            exit = true;
                                            break;
                                        }
                                    }
                                    if (!exit)
                                    {
                                        shoppingCartnew.Add(i);
                                    }
                                }
                                VWShoppingCartInfo shoppingcart = GetShoppingXuQiu(shoppingCartnew); 
                                WriteShoppingXuQiuCookie(shoppingcart);
                            }
                        }
                    }

                }
            }
            else
            {
                GetShoppingXuQiuByData();
            }
        }


        /// <summary>
        /// 检查购物车是否删除成功
        /// </summary>
        public static bool CheckTheXuQiuIsNull()
        {

            object _shoppingXuQiuSession = System.Web.HttpContext.Current.Session[SysCookieName.ShoppingXuQiu_Data];
            if (_shoppingXuQiuSession != null)
            {
                return false;
            }

            object _shoppingXuQiuCookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiu_Data];
            if (_shoppingXuQiuCookie != null)
            {
                return false;
            }

            HttpCookie _shoppingXuQiuCountCookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ShoppingXuQiuCount_Data];
            if (_shoppingXuQiuCountCookie != null && _shoppingXuQiuCountCookie.Value != 0.ToString())
            {
                return false;
            }

            return true;

        }

       
    }
}
