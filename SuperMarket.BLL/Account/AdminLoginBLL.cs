using SuperMarket.BLL.SysDB;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SuperMarket.BLL.Account
{
    public class AdminLoginBLL
    {
        #region 实例化
        public static AdminLoginBLL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly AdminLoginBLL instance = new AdminLoginBLL();
        }
        #endregion

         public AdminLoginEntity Login(string code, string password )
        {
            AdminLoginEntity _returnentity = new AdminLoginEntity();
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(password))
            {
                _returnentity.ResultCode = ((int)CommonStatus.LoginEmpty).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.LoginEmpty);
                return _returnentity;
            }
            SysUserEntity _entity = SysUserBLL.Instance.GetUserByUserCode(code);
            if (_entity == null)
            {
                _returnentity.ResultCode = ((int)CommonStatus.LoginNoMemCode).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.LoginNoMemCode);
                return _returnentity;
            }
            string passmd5 = CryptMD5.Encrypt(password);
            if (_entity.PassWord != passmd5)
            {
                _returnentity.ResultCode = ((int)CommonStatus.LoginError).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.LoginError);
                return _returnentity;
            }            
            _returnentity.Member = _entity;
            _returnentity.ResultCode = ((int)CommonStatus.Success).ToString();
            _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.Success);
   
            return _returnentity;
        }
         
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">账号</param>
        /// <param name="password">密码</param>
        /// <param name="clienttype">客户端种类</param>
        /// <param name="ipaddress">ip地址</param>
        /// <param name="recommendcode">推荐码</param>
        /// <returns></returns>
        public AdminLoginEntity Register(string code, string password)
        {
            AdminLoginEntity _returnentity = new AdminLoginEntity();
            SysUserEntity _entity = new SysUserEntity();
            _entity.UserCode = code;
            _entity.PassWord = password; 
            _entity.CreateTime = DateTime.Now; 
            _entity.UserLevel  = 1;
            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(password))
            {
                _returnentity.ResultCode = ((int)CommonStatus.RegisterEmpty).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.RegisterEmpty);
                return _returnentity;
            }
            if (SysUserBLL.Instance.IsExist(_entity))
            {
                _returnentity.ResultCode = ((int)CommonStatus.RegisterHasMember).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.RegisterHasMember);
                return _returnentity;
            } 
            _entity.Id = SysUserBLL.Instance.AddSysUser(_entity);

            if (_entity.Id > 0)
            {
                _returnentity.ResultCode = ((int)CommonStatus.Success).ToString();
                _returnentity.ResultMsg = EnumShow.Instance.GetEnumDes(CommonStatus.Success);
                _returnentity.Member = _entity;
                return _returnentity;
            }
             
            return _returnentity;
        }


        #region

        public AdminLoginEntity GetLoginCookie()
        {
            try
            {
                AdminLoginEntity MemberCookie;
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.SysUserCookieName];
                if (cookie != null)
                {

                    MemberCookie = JsonJC.JsonToObject<AdminLoginEntity>(CryptDES.DESDecrypt(cookie.Value));
                    if (MemberCookie == null)
                    {
                        return null;
                    }
                    else if (MemberCookie.Member == null || string.IsNullOrEmpty(MemberCookie.Member.UserCode))
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
            catch
            {
                return null;
            }
        }
        public void SetLoginCookie(AdminLoginEntity member)
        {
            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.SysUserCookieName];
            if (cookie == null)
            {
                cookie = new HttpCookie(SysCookieName.SysUserCookieName);
            }
            cookie.Value = CryptDES.DESEncrypt(JsonJC.ObjectToJson(member));
            if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
            {
                cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
            }
          
            System.Web.HttpContext.Current.Response.Cookies.Add(cookie);


        }
        public void ComBineCart(int val)
        {
            if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
            {
                System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
            }
            System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Value = val.ToString();
            System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Expires = DateTime.Now.AddDays(30);
        }
        #endregion

    }
}
