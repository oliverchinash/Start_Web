using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Web.CommonControllers;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SuperMarket.Admin.Controllers
{
    public class HomeController : BaseCommonController
    {
      
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }

        #region 登录
        public string UserLogin()
        {
            string email = StringUtils.SafeStr(Request.Form["Email"]);
            string password = StringUtils.SafeStr(Request.Form["PassWord"]);
            string VerCode = StringUtils.SafeStr(FormString.SafeQ("VerCode"));
            

            AdminLoginEntity _loginmodel = AdminLoginBLL.Instance.Login(email, password);
            if (_loginmodel != null)
            {
                try
                {
                    AdminLoginBLL.Instance.SetLoginCookie(_loginmodel);
                    AdminLoginBLL.Instance.ComBineCart(1);
                    return _loginmodel.ResultCode;
                }
                catch (Exception ex)
                {
                    LogUtil.Log(ex);
                    return ((int)CommonStatus.ServerError).ToString();
                }
            }
            return ((int)CommonStatus.Fail).ToString();
        }

        public string CheckLogin()
        {
            MemberLoginEntity member = CookieBLL.GetLoginCookie();

            if (member != null  )
            {
                if (!string.IsNullOrEmpty(member.MemCode))
                    return member.MemCode;
            }
            return "0";
        }
     
        #endregion
        #region
        public string UserRegister()
        { 

            string email = StringUtils.SafeStr(FormString.SafeQ("Email"));
            string password = StringUtils.SafeStr(FormString.SafeQ("PassWord"));

            AdminLoginEntity _loginentity = AdminLoginBLL.Instance.Register(email, password);
            AdminLoginBLL.Instance.SetLoginCookie(_loginentity);
            AdminLoginBLL.Instance.ComBineCart(1); 
            return _loginentity.ResultCode;
        }
        #endregion
        #region 检查Email是否存在
        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        /// <returns></returns>
        public string CheckMemberCode()
        {
            string useraccount = QueryString.SafeQ("useraccount");
            if (useraccount.ToString() != "")
            {
                if (MemberBLL.Instance.MemberCodeExist(useraccount))
                {
                    return "1";
                }
                else
                {
                    return "0";
                }
            }
            else
            {
                return "0";
            }

        }
        #endregion
    }
}
