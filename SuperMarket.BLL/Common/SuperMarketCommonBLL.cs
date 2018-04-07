
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using SuperMarket.Core;
using SuperMarket.BLL.WeiXin;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model;
using SuperMarket.BLL.Cookie;
using System.Collections.Generic;
using SuperMarket.Model.Account;
using SuperMarket.BLL.MemberDB;

namespace SuperMarket.BLL.Common
{
    /// <summary>
    /// IPAddress 的摘要说明
    /// </summary>
    public class SuperMarketCommonBLL : System.Web.UI.Page
    {

        private static Dictionary<string, string> AuthDic;
        /// <summary>
        /// 根据cookie获取后台权限
        /// </summary>
        /// <param name="ordercode"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetMenuByCookie()
        {
            if (AuthDic == null || AuthDic.Keys.Count == 0)
            {
                 MemberLoginEntity member = CookieBLL.GetLoginCookie();
                int memid = member.MemId;

                AuthDic = MemberBLL.Instance.GetAuthByMemId(memid);
                 
            } 
            return AuthDic;
        }
        
    }
}