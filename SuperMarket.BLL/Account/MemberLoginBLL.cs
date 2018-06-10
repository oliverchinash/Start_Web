
using SuperMarket.BLL.MemberDB;
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
     public  class MemberLoginBLL
    {
        #region 实例化
        public static MemberLoginBLL Instance
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
            internal static readonly MemberLoginBLL instance = new MemberLoginBLL();
        }
        #endregion

        public ResultObj Login(string unionid,string code, string password,int clienttype ,string ipaddress)
        {
            ResultObj _returnentity = new ResultObj();
            MemberLoginEntity _entity =null;
            bool isweixin = true; 
            if (!string.IsNullOrEmpty(unionid))
            {
                _entity = MemberBLL.Instance.GetLoginMemByMethod(unionid, LoginMethodEnum.WeChat);
            }
            if ((_entity==null|| _entity.MemId==0)&& (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(password)))
            {
                _returnentity.Status =  (int)CommonStatus.LoginEmpty ; 
                return _returnentity;
            }
            if (_entity == null || _entity.MemId == 0)
            {
                isweixin = false;
                _entity = MemberBLL.Instance.GetLoginMemByMethod(code, LoginMethodEnum.Code);
            }
            if (_entity == null || _entity.MemId == 0)
            {
                isweixin = false;
                _entity = MemberBLL.Instance.GetLoginMemByMethod(code, LoginMethodEnum.MobilePhone);

            }
            if (_entity == null || _entity.MemId == 0)
                {
                    _returnentity.Status = (int)CommonStatus.LoginNoMemCode;
                    return _returnentity;
                }
            if (!isweixin)
            {
                string passmd5 = CryptMD5.Encrypt(password);
                if (_entity.PassWord != passmd5)
                {
                    _returnentity.Status = (int)CommonStatus.LoginError;
                    return _returnentity;
                }
            }
            if (_entity.Status == (int)MemberStatus.IsLock)
            {
                _returnentity.Status =  (int)CommonStatus.LoginStatusLock ; 
                return _returnentity;
            }
            if (_entity.Status == (int)MemberStatus.WaitCheck)
            {
                _returnentity.Status = (int)CommonStatus.Success;
                _returnentity.Obj = _entity;
                return _returnentity;
            }
            _returnentity.Obj = _entity; 
            _returnentity.Status =  (int)CommonStatus.Success ; 
            if(_entity.MemId>0&&string.IsNullOrEmpty(_entity.WeChat)&& !string.IsNullOrEmpty(unionid))
            {
                MemberBLL.Instance.BindMemWeChat(_entity.MemId, unionid, _entity.TimeStampTab);
              }
          
            //此处可以异步
            AddLoginLog(_entity.MemId, clienttype, ipaddress);
            _returnentity.Obj = MemberBLL.Instance.GetLoginMemByMethod(_entity.MemId.ToString(), LoginMethodEnum.MemId);
            return _returnentity;
        }

        public ResultObj LoginByTempCode(string tempcode,string unionid, int clienttype, string ipaddress)
        {
            ResultObj _returnentity = new ResultObj();
            MemberLoginEntity _entity = null;
            _entity = MemberBLL.Instance.GetLoginMemByMethod(tempcode, LoginMethodEnum.TempCode);
            if (_entity.Status == (int)MemberStatus.IsLock)
            {
                _returnentity.Status = (int)CommonStatus.LoginStatusLock;
                return _returnentity;
            }
            if (_entity.Status == (int)MemberStatus.WaitCheck)
            {
                _returnentity.Status = (int)CommonStatus.Success;
                _returnentity.Obj = _entity;
                return _returnentity;
            }
            _returnentity.Obj = _entity;
            _returnentity.Status = (int)CommonStatus.Success;
            
            if (_entity.MemId > 0 && string.IsNullOrEmpty(_entity.WeChat) && !string.IsNullOrEmpty(unionid))
            {
                MemberBLL.Instance.BindMemWeChat(_entity.MemId, unionid, _entity.TimeStampTab);
            } 
            //此处可以异步
            AddLoginLog(_entity.MemId, 0, "");
            return _returnentity;
        }

        public void AddLoginLog(int memberid,int clienttype,string ipaddress)
        {
            MemLoginLogEntity _loginlog = new MemLoginLogEntity();
            _loginlog.ClientType = clienttype;
            _loginlog.MemId = memberid;
            _loginlog.LoginTime = DateTime.Now;
            _loginlog.LoginIP = ipaddress;
            MemberBLL.Instance.UpdateLastLoginDate(_loginlog); 
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
        public ResultObj Register(string code, string password,string mobile, int clienttype, string ipaddress,string recommendcode,int isstore=0)
        {
            ResultObj _returnentity = new ResultObj();
            MemberEntity _entity = new MemberEntity();
            _entity.MemCode = code;
            _entity.PassWord = password ; 
            _entity.LastLoginTime = DateTime.Now;
            _entity.CreateTime = DateTime.Now;
            _entity.CreateClientType = clienttype;
            _entity.MobilePhone = mobile;
            _entity.CreateIp = ipaddress;
            _entity.MemGrade = (int)MemberGrade.Normal;
            //_entity.Status = (int)MemberStatus.Registing;
            _entity.IsStore = isstore;
            _entity.Status = (int)MemberStatus.WaitCheck;

            if (string.IsNullOrEmpty(code) || string.IsNullOrEmpty(password))
            {
                _returnentity.Status =  (int)CommonStatus.RegisterEmpty ; 
                return _returnentity;
            }

            if (MemberBLL.Instance.IsExist(_entity))
            {
                _returnentity.Status = (int)CommonStatus.RegisterHasMember ; 
                return _returnentity;
            }

            _entity.RecommendCode = recommendcode;
              
            _entity.Id= MemberBLL.Instance.AddMember(_entity);
           
            if (_entity.Id>0)
            {
                _returnentity.Status = (int)CommonStatus.Success ; 
                _returnentity.Obj = _entity.Id;
                return _returnentity;
            }
           
            //此处可以异步
            AddLoginLog(_entity.Id, clienttype, ipaddress);

            return _returnentity;
        }
        public ResultObj RegisterStore(VWMemberEntity _mem)
        {
            ResultObj _returnentity = new ResultObj();
            MemberEntity _entity = new MemberEntity();
            _entity.MemCode = _mem.MemCode;
            _entity.PassWord = _mem.PassWord;     
            if (string.IsNullOrEmpty(_mem.MemCode) || string.IsNullOrEmpty(_mem.PassWord))
            {
                _returnentity.Status =  (int)CommonStatus.RegisterEmpty ; 
                return _returnentity;
            }
            MemberEntity mem1 = MemberBLL.Instance.GetMemberByCode(_entity.MemCode);
            if(mem1!=null&&!string.IsNullOrEmpty( mem1.MobilePhone)&& mem1.MobilePhone!= _mem.ContactsMobile)
            {
                _returnentity.Status =  (int)CommonStatus.RegisterHasMember ;
                return _returnentity;
            }
            MemStoreEntity mem2 = StoreBLL.Instance.GetStoreByNameAdd(_mem.CompanyName, _mem.CompanyAddress);
            if (mem2 != null && !string.IsNullOrEmpty(mem2.MobilePhone) && mem2.MobilePhone != _mem.ContactsMobile)
            {
                _returnentity.Status = (int)CommonStatus.RegisterHasMember;
                return _returnentity;
            }
            _entity.Id = MemberBLL.Instance.RegisterCompanyProc(_mem);

            if (_entity.Id > 0)
            {
                _returnentity.Status =  (int)CommonStatus.Success;
                _returnentity.Obj = _entity.Id;
            }
            else
            {
                _returnentity.Status = (int)CommonStatus.Fail;
            }

            //此处可以异步
            AddLoginLog(_entity.Id, _mem.CreateClientType, _mem.CreateIp);
            return _returnentity;
        }

        /// <summary>
        /// 注册第一步
        /// </summary>
        /// <param name="_mem"></param>
        /// <returns></returns>
        public ResultObj Register1(MemberEntity _mem)
        {
            ResultObj _returnentity = new ResultObj(); 
            if (  string.IsNullOrEmpty(_mem.PassWord))
            {
                _returnentity.Status = (int)CommonStatus.RegisterEmpty;
                return _returnentity;
            }
            MemberEntity mem1 = MemberBLL.Instance.GetLoginMemByMobile(_mem.MobilePhone);
            if (mem1 != null && !string.IsNullOrEmpty(mem1.MobilePhone)&& mem1.Id>0)
            {
                _returnentity.Status = (int)CommonStatus.RegisterHasMember;
                return _returnentity;
            }
            _mem.Id = MemberBLL.Instance.AddMember(_mem);

            if (_mem.Id > 0)
            {
                _returnentity.Status = (int)CommonStatus.Success;
                _returnentity.Obj = MemberBLL.Instance.GetMember(_mem.Id);
            }
            else
            {
                _returnentity.Status = (int)CommonStatus.Fail;
            }
            
            return _returnentity;
        }
        /// <summary>
        /// 注册第一步
        /// </summary>
        /// <param name="_mem"></param>
        /// <returns></returns>
        public ResultObj Register2(MemberEntity _mem,MemberInfoEntity _meminfo,MemStoreEntity _store)
        {
            ResultObj _returnentity = new ResultObj();
            if (_mem.Status == (int)MemberStatus.Register1)
            {
                int result = MemberBLL.Instance.RegisterProc(_mem, _meminfo, _store);
                if (result > 0)
                {
                    _returnentity.Status = (int)CommonStatus.Success;
                    _returnentity.Obj = MemberBLL.Instance.GetMember(_mem.Id);
                    return _returnentity;
                }
            }
            _returnentity.Status = (int)CommonStatus.Fail;
            
            return _returnentity;
        }

        #region COOKIE

        //public   MemberLoginEntity GetLoginCookie()
        //{
        //    try
        //    {
        //        MemberLoginEntity MemberCookie;
        //        HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ConfigCore.Instance.ConfigCommonEntity.MemberCookieName];
        //        if (cookie != null)
        //        {

        //            MemberCookie = JsonJC.JsonToObject<MemberLoginEntity>(CryptDES.DESDecrypt(cookie.Value));
        //            if (MemberCookie == null)
        //            {
        //                return null;
        //            }
        //            else if (MemberCookie.Member == null || string.IsNullOrEmpty(MemberCookie.Member.MemCode))
        //            {
        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //                return null;
        //            }
        //            else
        //            {
        //                return MemberCookie;
        //            }
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //}
        //public   void SetLoginCookie(MemberLoginEntity member)
        //{
        //    HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[ConfigCore.Instance.ConfigCommonEntity.MemberCookieName];
        //    if (cookie == null)
        //    {
        //        cookie = new HttpCookie(ConfigCore.Instance.ConfigCommonEntity.MemberCookieName);
        //    }
        //    cookie.Value = CryptDES.DESEncrypt(JsonJC.ObjectToJson(member));
        //    if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
        //    {
        //        cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
        //    }
        //    System.Web.HttpContext.Current.Response.Cookies.Add(cookie);


        //}
        //public   void ComBineCart(int val)
        //{
        //    if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
        //    {
        //       System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
        //    }
        //       System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Value = val.ToString();
        //       System.Web.HttpContext.Current.Response.Cookies[ConfigCore.Instance.ConfigCommonEntity.ComBineCart].Expires = DateTime.Now.AddDays(30);
        //}
        #endregion

    }
}
