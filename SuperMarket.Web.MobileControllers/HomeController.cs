using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.HelpDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
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
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WeChatJsSdk.SdkCore;

namespace SuperMarket.Web.MemberControllers
{
    public class  HomeController : BaseCommonController
    {

        public ActionResult Search()
        {
            string key = QueryString.SafeQ("key");//精选类型
            int jishi = QueryString.IntSafeQ("js", -1);
            if (jishi == -1) jishi = (int)JiShiSongEnum.Normal;
            ViewBag.JiShiSong = jishi;
            ViewBag.SearchKey = key;
            return View();
        }
        #region 首页
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            int _pagesize = 10;
            int total =  0;
            IList<VWProductMenuEntity> listbrand = ProductMenuBLL.Instance.GetProductMenuAll((int)ProductMenuType.BPBrand, 1);
                ViewBag.ListBrand = listbrand;
                //爆品-限量乐购
                IList<VWProductBaoPinEntity> listbpproduct = ProductBaoPinBLL.Instance.GetProductBaoPinShowList();



                //获取精选的第一页
                int _finetype = FormString.IntSafeQ("ft", (int)ProductFineTypeEnum.MobileHome);//精选类型

                 IList<VWProductFineEntity> listjx = ProductFineBLL.Instance.GetProductFineList(1, _pagesize, ref total, _finetype);

                MemberLoginEntity member = CookieBLL.GetLoginCookie();
                if (member != null && member.MemId > 0)
                {
                    ViewBag.MemId = member.MemId;
                    ViewBag.MemStatus = member.Status;
                    if (listbpproduct != null && listbpproduct.Count > 0)
                    {
                        foreach (VWProductBaoPinEntity entity in listbpproduct)
                        {
                            if (entity.ProductDetail != null && entity.ProductDetail.ProductId > 0)
                                entity.Price = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                        }
                    }
                    if (listjx != null && listjx.Count > 0)
                    {
                        foreach (VWProductFineEntity entity in listjx)
                        {
                            if (entity.ProductDetail != null && entity.ProductDetail.ProductId > 0)
                                entity.Price = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                        }
                    }
                }

                ViewBag.ListProduct = listbpproduct;

                ViewBag.ListJinXuan = listjx;
          
            ViewBag.PageMenu = "1";

           
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            string returnurl = StringUtils.GetDbString(Request.QueryString["returnurl"]);
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        public ActionResult StartUp()
        {
            return View(); 
        }
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            string[] cookieName = System.Web.HttpContext.Current.Request.Cookies.AllKeys;
            for (var i = 0; i < cookieName.Length; i++)
            { 
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName[i]];
                cookie.Expires = DateTime.Now.AddDays(-30);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            } 
            return Redirect("/");
        }

        public ActionResult Register()
        {
            var  _errorcode = QueryString.IntSafeQ("error", (int)CommonStatus.Success);
            if(_errorcode!= (int)CommonStatus.Success)
            {
                ViewBag.ErrorTitle= EnumEntityShow.Instance.GetEnumDes((CommonStatus)_errorcode);
            }
            return View();
        }
        public ActionResult RegMsg()
        {
            string _mobile =QueryString.SafeQ("mobile");
            string _vCode = QueryString.SafeQ("vCode");

            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister].ToString() == _mobile && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() == _vCode)
            {
                ViewBag.MobileNo = _mobile;
                ViewBag.MobileNoYZCode = _vCode;
                VWMemberEntity mem = MemberBLL.Instance.GetVWMemberByPhone(_mobile);
                ViewBag.VWMember = mem;
                return View();
            }
            else
            {
                return RedirectToAction("Register", new { error = ((int)CommonStatus.RegisterErrorVerCode).ToString() });
            }
        }
         
        public string weixintoken()
        {
            WinXinConfig config = new WinXinConfig();
            string appId = System.Configuration.ConfigurationManager.AppSettings["WeChatAppId"];
            string appSecret = System.Configuration.ConfigurationManager.AppSettings["WeChatAppSecret"];
            bool debug = System.Configuration.ConfigurationManager.AppSettings["WeChatAppDebug"].ToLower() == "true";
            JSSDK sdk = new JSSDK(appId, appSecret, debug);

            config = sdk.GetConfig();
            return config.accesstoken;
        }
        #endregion
     
  
        #region  找回密码

        /// <summary>
        ///  找回密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ForgetPwd()
        {
            return View();
        }
        //public ActionResult ForgetPwdReset()
        //{
        //    long _key = QueryString.LongIntSafeQ("key");
        //    if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
        //    {
        //        int _memid = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
        //        if (_memid > 0)
        //        {
        //            MemberEntity _entity = MemberBLL.Instance.GetMember(_memid);
        //            if (_entity.TimeStampCode == _key)
        //            {
        //                ViewBag.key = _key;
        //                return View();
        //            }
        //            return RedirectToAction("ForgetPwd");
        //        }
        //    } 
        //    return RedirectToAction("ForgetPwd");
        //}
        public ActionResult ForgetPwdSuccess()
        {
            return View();
        }

        #endregion

        public ActionResult ContactUs()
        {
            return View();
        }
        public ActionResult HelpContent()
        {
            IssueContentEntity entity = new IssueContentEntity();
            int contentid = QueryString.IntSafeQ("id");
            if(contentid>0)
            {
                entity = IssueContentBLL.Instance.GetIssueContent(contentid);
            }
            else
            {
                string title= QueryString.SafeQ("title");
                entity = IssueContentBLL.Instance.GetIssueContentByName(title);
            }
            ViewBag.Content= entity;
            return View();
        }
        public void GetWeiXinMsg()
        {
            WinXinConfig config = new WinXinConfig();
            string appId = System.Configuration.ConfigurationManager.AppSettings["WeChatAppId"];
            string appSecret = System.Configuration.ConfigurationManager.AppSettings["WeChatAppSecret"];
            bool debug = System.Configuration.ConfigurationManager.AppSettings["WeChatAppDebug"].ToLower() == "true";
            JSSDK sdk = new JSSDK(appId, appSecret, debug);

            config = sdk.GetConfig();
            if (config != null)
            {
                LogUtil.Log("获取微信Config出错", "appid:" + appId + ",appSecret:" + appSecret + ",debug:" + debug + ",token:");

                ViewBag.WeiXinConfig = config;
            }
            else
            {
                ViewBag.WeiXinConfig = new WinXinConfig();
            }
        }
    }
}
