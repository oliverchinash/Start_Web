////////////////////////////////////////////////////////////////
//功能描述:
//          Cookie管理通用功能
//创    建:
//创建时间:
//变    更:
//          Simon Cheng
//变更时间:
//          2011.10.19
//备    注:
////////////////////////////////////////////////////////////////

using System;
using System.Web;


namespace SuperMarket.Core.Cookie
{
    public class CookieUtil
    {
        /// <summary>
        /// 获取Cookie值
        /// </summary>
        /// <param name="cookieName">Cookie名称</param>
        /// <returns></returns>
        public static string GetCookieValue(string cookieName)
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                return cookie.Value;
            }
            else
            {
                return null;
            }
        }

        /// <summary> 
        /// 设置Cookie 
        /// </summary> 
        /// <param name="CookieName">Cookie名称</param> 
        /// <param name="CookieValue">Cookie值</param> 
        public static void SetCookie(string CookieName, string CookieValue)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            myCookie.Value = CookieValue;
            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
        }


        /// <summary> 
        /// 设置Cookie 
        /// </summary> 
        /// <param name="CookieName">Cookie名称</param> 
        /// <param name="CookieValue">Cookie值</param> 
        /// <param name="CookieTime">Cookie过期时间(分),0为关闭页面失效</param> 
        public static void SetCookie(string CookieName, string CookieValue, double CookieTime)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);          
            myCookie.Value = CookieValue;
            if (CookieTime != 0)
            {
                myCookie.Expires = DateTime.Now.AddMinutes(CookieTime);
                if (System.Web.HttpContext.Current.Response.Cookies[CookieName] != null)
                    System.Web.HttpContext.Current.Response.Cookies.Remove(CookieName);
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            else
            {
                if (System.Web.HttpContext.Current.Response.Cookies[CookieName] != null)
                    System.Web.HttpContext.Current.Response.Cookies.Remove(CookieName);
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }



        /// <summary> 
        /// 设置Cookie 
        /// </summary> 
        /// <param name="CookieName">Cookie名称</param> 
        /// <param name="CookieValue">Cookie值</param> 
        /// <param name="CookieTime">Cookie过期时间(分),0为关闭页面失效</param> 
        public static void SetCookie(string CookieDomain, string CookieName, string CookieValue, double CookieTime)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);      
            myCookie.Value = CookieValue;
            if (CookieTime != 0)
            {
                myCookie.Expires = DateTime.Now.AddMinutes(CookieTime);
                if (!string.IsNullOrEmpty(CookieDomain))
                {
                    myCookie.Domain = CookieDomain;
                }
                if (System.Web.HttpContext.Current.Response.Cookies[CookieName] != null)
                    System.Web.HttpContext.Current.Response.Cookies.Remove(CookieName);
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
            }
            else
            {
                if (System.Web.HttpContext.Current.Response.Cookies[CookieName] != null)
                    System.Web.HttpContext.Current.Response.Cookies.Remove(CookieName);
                System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
            }
        }

        /// <summary> 
        /// 清除CookieValue 
        /// </summary> 
        /// <param name="CookieName">Cookie名称</param> 
        public static void ClearCookie(string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);       
            myCookie.Expires = DateTime.Now.AddYears(-2);
            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
        }


        /// <summary> 
        /// 清除CookieValue 
        /// </summary> 
        /// <param name="CookieName">Cookie域</param> 
        /// <param name="CookieName">Cookie名称</param> 
        public static void ClearCookie(string CookieDomain, string CookieName)
        {
            HttpCookie myCookie = new HttpCookie(CookieName);
            DateTime now = DateTime.Now;
            myCookie.Expires = now.AddYears(-2);
            myCookie.Domain = CookieDomain;
            System.Web.HttpContext.Current.Response.Cookies.Add(myCookie);
        }
        
    }
}
