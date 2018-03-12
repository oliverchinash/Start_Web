using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuperMarket.BLL.Cookie
{
    public class CookieBLL
    {
        #region 修理厂用户（阿哈马平台）cookie
        public static SuperMarket.Model.Account.MemberLoginEntity GetLoginCookie()
        {
            try
            {
                SuperMarket.Model.Account.MemberLoginEntity MemberCookie;
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MemberCookieName];
                if (cookie!=null&&!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
                {
                    cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
                  } 
                if (cookie != null)
                {
                    MemberCookie = JsonJC.JsonToObject<SuperMarket.Model.Account.MemberLoginEntity>(CryptDES.DESDecrypt(cookie.Value));
                      if (MemberCookie == null)
                    {
                        return null;
                    }
                    else if (MemberCookie  == null || MemberCookie.MemId == 0)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                        return null;
                    }
                    else
                    {
                        return MemberCookie;
                    }
                }
                else
                { 

                    return null;
                }
            }
            catch(Exception ex)
            {
                LogUtil.Log("cookie域", ex.Message);
                return null;
            }
        }

        public static void SetLoginCookie(SuperMarket.Model.Account.MemberLoginEntity member,bool autologin=false)
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MemberCookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MemberCookieName);
               
            }
            if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
            {
                cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
            }
            cookie.Value = CryptDES.DESEncrypt(JsonJC.ObjectToJson(member));
            if(autologin)
            {
                cookie.Expires = DateTime.Now.AddDays(30); 
            }
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            //if (member != null && member.MemId > 0)
            //{
            //    System.Web.HttpContext.Current.Response.Cookies[SysCookieName.DefaultLoginId].Value = member.MemId.ToString();
            //    System.Web.HttpContext.Current.Response.Cookies[SysCookieName.DefaultLoginId].Expires = DateTime.Now.AddDays(30);
            //    System.Web.HttpContext.Current.Response.Cookies[SysCookieName.DefaultCusName].Value = (member == null || string.IsNullOrEmpty(member.NickName)) ? member.MemCode : member.NickName;
            //    System.Web.HttpContext.Current.Response.Cookies[SysCookieName.DefaultCusName].Expires = DateTime.Now.AddDays(30);

            //}

        }

        public static  int GetRegisterCookie()
        {
            try
            {
                int memid = 0;
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.RegisterMemCookieName];
                if (cookie != null)
                {
                    if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
                    {
                        cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
                    }
                    memid = StringUtils.GetDbInt( CryptDES.DESDecrypt(cookie.Value));
                    if (memid == 0)
                    {
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
                        return 0;
                    }
                    else
                    {
                        return memid;
                    }
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static void SetRegisterCookie(int  member)
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.RegisterMemCookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.RegisterMemCookieName);
            }
            if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
            {
                cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
            }
            cookie.Value = CryptDES.DESEncrypt(JsonJC.ObjectToJson(member));
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie); 
        }

        #endregion
 
        public static void ComBineCart(int val)
        {
            System.Web.HttpContext.Current.Response.Cookies[SysCookieName.ComBineCart].Value = val.ToString();
            System.Web.HttpContext.Current.Response.Cookies[SysCookieName.ComBineCart].Expires = DateTime.Now.AddDays(30);
        }


        public static void ComBineCartXuQiu(int val)
        {
            System.Web.HttpContext.Current.Response.Cookies[SysCookieName.ComBineXuQiu].Value = val.ToString();
            System.Web.HttpContext.Current.Response.Cookies[SysCookieName.ComBineXuQiu].Expires = DateTime.Now.AddDays(30);
        }

        /// <summary>
        /// 获取选中的车型COOKIE
        /// </summary>
        /// <param name="level">层级目前有1，2，3，4</param>
        /// <returns></returns>
        //public static int GetCarTypeIdCookie(int level)
        //{
        //    try
        //    {
        //        int cid = 0;
        //        System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.CarTypeId+ level.ToString()];
        //        if (cookie != null)
        //        {
        //            cid = StringUtils.GetDbInt(System.Web.HttpContext.Current.Server.UrlDecode(cookie.Value));  

        //            if (cid == 0)
        //            {
        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //                return 0;
        //            }
        //            else
        //            {
        //                return cid;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public static void SetCarTypeIdCookie(int level, int cartypeid)
        //{
        //    try
        //    {
        //        System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.CarTypeId + level.ToString()];
        //        if (cookie == null)
        //        {
        //            cookie = new HttpCookie(SysCookieName.CarTypeId + level.ToString());
        //        }
        //        cookie.Value = cartypeid.ToString();
        //        System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //    }
        //    catch
        //    {
        //    }
        //}
        //public static void ClearCarTypeIdCookie()
        //{
        //    try
        //    { 
        //        for (int i = 1; i <= 4; i++)
        //        {
        //            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.CarTypeId + i.ToString()];
        //            if (cookie != null)
        //            {

        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);

        //            }
        //        }
              
               
        //    }
        //    catch
        //    {
                
        //    }
        //}
        //public static int GetClassTypeCookie()
        //{
        //    try
        //    {
        //        int cid = 0;
        //        System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.ClassType];
        //        if (cookie != null)
        //        {
        //            cid = StringUtils.GetDbInt(System.Web.HttpContext.Current.Server.UrlDecode(cookie.Value));

        //            if (cid == 0)
        //            {
        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //                return 0;
        //            }
        //            else
        //            {
        //                return cid;
        //            }
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}


        public static string GetMemBrowseLogCookie()
        {   string BrowseLogStr="";
            try
            {
             
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.MemBrowseLog];
                if (cookie != null)
                {
                    BrowseLogStr =   CryptDES.DESDecrypt(cookie.Value) ;   
                }
                return BrowseLogStr;
            }
            catch
            {
                return BrowseLogStr;
            }
        }
        public static void AddMemBrowseLogCookie(int pdid)
        {
            string BrowseLogStr = "";
            try
            { 
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.MemBrowseLog];
                if (cookie != null)
                {
                    BrowseLogStr =  CryptDES.DESDecrypt(cookie.Value);
                }
                else
                {
                    cookie = new HttpCookie(SysCookieName.MemBrowseLog); 
                }
                string[] arr = BrowseLogStr.Split(',');
                if (!arr.Contains(pdid.ToString()))
                {
                    if (string.IsNullOrEmpty(BrowseLogStr))
                    {
                        BrowseLogStr = pdid.ToString();
                    }
                    else
                    {
                        BrowseLogStr += "," + pdid.ToString();
                    }
                }
              
                cookie.Value = CryptDES.DESEncrypt(BrowseLogStr); 
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch
            {
               
            }
        }


        public static void ClearBrowseLogCookie()
        {
            try
            {
                    System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.MemBrowseLog];
                    if (cookie != null)
                    { 
                        cookie.Expires = DateTime.Now.AddDays(-1);
                        System.Web.HttpContext.Current.Response.Cookies.Add(cookie); 
                    } 
            }
            catch
            {

            }
        }
        /// <summary>
        /// 获取微信入口权限字符串
        /// </summary>
        /// <returns></returns>
        public static string GetWeixinAccessCookie( )
        {
            string resultstr = "";
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeixinAccessCookie];
                if (cookie != null)
                {
                    resultstr =  cookie.Value ;
                } 
            }
            catch
            {
            }
            return resultstr;
        }
        public static void SetWeixinAccessCookie(string accesskey)
        {
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeixinAccessCookie];
                if (cookie == null)
                {
                    cookie = new HttpCookie(SysCookieName.WeixinAccessCookie);
                }
                cookie.Value = accesskey;
                cookie.Expires = DateTime.Now.AddHours(1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch
            {
            }
        }
        /// <summary>
        /// 获取微信入口权限字符串
        /// </summary>
        /// <returns></returns>
        public static string GetWeixinTicketCookie()
        {
            string resultstr = "";
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeixinAccessTicket];
                if (cookie != null)
                {
                    resultstr = cookie.Value;
                }
            }
            catch
            {
            }
            return resultstr;
        }
        public static void SetWeixinTicketCookie(string ticket)
        {
            try
            {
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeixinAccessTicket];
                if (cookie == null)
                {
                    cookie = new HttpCookie(SysCookieName.WeixinAccessCookie);
                }
                cookie.Value = ticket;
                cookie.Expires = DateTime.Now.AddHours(1);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            catch
            {
            }
        }


        public static  string GetWeiXinWebCode()
        {
            try
            { 
                System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeCharWebCode];
                if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
                {
                    cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
                }
                if (cookie != null)
                {
                    string result =  CryptDES.DESDecrypt(cookie.Value) ;
                    if (result == null)
                    {
                        return null;
                    } 
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }

        public static void SetWeiXinWebCode(string code)
        {
            System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.WeCharWebCode];
            if (cookie == null)
            {
                cookie = new HttpCookie(SysCookieName.WeCharWebCode); 
            }
            if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
            {
                cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
            }
            cookie.Value = CryptDES.DESEncrypt(code);
           
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie); 
        }

    }
}
