using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.LoginWebControllers
{
    public class HomeController : BaseCommonController
    { 
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            if (Globals.IsMobileDevice()&& Request.Url.AbsoluteUri.Contains(ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl))
            {
                return Redirect(Request.Url.AbsoluteUri.Replace(ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl, ConfigCore.Instance.ConfigCommonEntity.LoginMobileWebUrl));
            }
            string returnurl = StringUtils.GetDbString(Request.QueryString["returnurl"]);
            string unionid = "";
            string code = QueryString.SafeQ("wechatcode"); //微信自动登录
            if (!string.IsNullOrEmpty(code))
            {
                MemWeChatMsgEntity shortmsg = WeiXinJsSdk.Instance.GetWeChatShortInfo(code);
                if (!string.IsNullOrEmpty(shortmsg.OpenId) && !string.IsNullOrEmpty(shortmsg.UnionId))
                {
                    unionid = shortmsg.UnionId;
                    System.Web.HttpContext.Current.Session[CommonKey.WeChatUnionId] = unionid;
                    MemWeChatMsgEntity entity = new MemWeChatMsgEntity();
                    entity.AppId = WeiXinConfig.GetAppId();
                    entity.Status = (int)WeChatStatus.ShortInfo;
                    entity.UnionId = shortmsg.UnionId;
                    entity.OpenId = shortmsg.OpenId;
                    MemWeChatMsgBLL.Instance.WeChatLogin(entity);
                    ResultObj _result = MemberLoginBLL.Instance.Login(unionid, "", "", (int)ClientTypeEnum.PC, IPAddress.IP);
                    if (_result != null && _result.Obj != null)
                    { 
                        if (_result.Status == (int)CommonStatus.Success)
                        {
                            MemberLoginEntity _loginmodel = (MemberLoginEntity)_result.Obj;
                            if (_loginmodel.Status == (int)MemberStatus.Active)
                            {
                                CookieBLL.SetLoginCookie(_loginmodel, false);
                                CookieBLL.ComBineCart(1);
                                CookieBLL.ComBineCartXuQiu(1);
                                if (string.IsNullOrEmpty(returnurl))
                                    returnurl = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MainWebUrl;
                                return Redirect(returnurl);
                            }
                        }
                    }
                }
            } 
            string tempcode =StringUtils.SafeCodeSmall( QueryString.SafeQNo("tempcode"));//临时自动登录
            if (!string.IsNullOrEmpty(tempcode))
            {
                ResultObj _resulttemp = MemberLoginBLL.Instance.LoginByTempCode(tempcode, unionid, (int)ClientTypeEnum.PC, IPAddress.IP);
                if (_resulttemp != null && _resulttemp.Obj != null)
                {
                    if (_resulttemp.Status == (int)CommonStatus.Success)
                    {
                        MemberLoginEntity _loginmodel = (MemberLoginEntity)_resulttemp.Obj;
                        if (_loginmodel.Status == (int)MemberStatus.Active)
                        {
                            CookieBLL.SetLoginCookie(_loginmodel, false);
                            CookieBLL.ComBineCart(1);
                            CookieBLL.ComBineCartXuQiu(1);
                            if (string.IsNullOrEmpty(returnurl))
                                returnurl = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.MainWebUrl;
                            return Redirect(returnurl);
                        }
                    }
                }
            }
          
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            string returnurl = StringUtils.GetDbString(Request.QueryString["returnurl"]);

            string[] cookieName = System.Web.HttpContext.Current.Request.Cookies.AllKeys;
            for (var i = 0; i < cookieName.Length; i++)
            {
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName[i]];
            
                if (!string.IsNullOrEmpty(ConfigCore.Instance.ConfigCommonEntity.CookieDomain))
                { 
                    cookie.Domain = ConfigCore.Instance.ConfigCommonEntity.CookieDomain;
                }
               
                cookie.Expires = DateTime.Now.AddDays(-30);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            return Redirect("/Home/Login?returnurl="+ HttpUtility.UrlEncode(returnurl, System.Text.Encoding.GetEncoding(936) ));
        }

        public ActionResult HasNoAuth()
        {
            string returnurl = StringUtils.GetDbString(Request.QueryString["returnurl"]);
            string loginurl = "/Home/Login?returnurl=" + System.Web.HttpContext.Current.Server.UrlEncode(returnurl);
            ViewBag.LoginUrl = System.Web.HttpContext.Current.Server.UrlEncode(returnurl);
            return View();
        }
        #endregion



        #region 会员注册 
        public ActionResult Reg()
        {
            if (Globals.IsMobileDevice() && Request.Url.AbsoluteUri.Contains(ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl))
            {
                return Redirect(Request.Url.AbsoluteUri.Replace(ConfigCore.Instance.ConfigCommonEntity.LoginWebUrl, ConfigCore.Instance.ConfigCommonEntity.LoginMobileWebUrl));
            } 
            return View();
        }
        /// <summary>
        /// 上传证件
        /// </summary>
        /// <returns></returns>
        public ActionResult Reg2()
        {
            int _memid = QueryString.IntSafeQ("memid");
            string _stamp = QueryString.SafeQNo("stamp");
            string returnurl = QueryString.SafeQNo("returnurl");
            if (_memid == 0) _memid = CookieBLL.GetRegisterCookie();
            if (_memid > 0)
            {
                VWMemberEntity member = MemberBLL.Instance.GetVWMember(_memid);
                if(member!=null&& member.MemId>0&& member.Status==(int)MemberStatus.Register1 && member.TimeStampTab== _stamp)
                {
                    ViewBag.VWMember = member;
                    return View();
                } 
            }

            return RedirectToAction("Reg", new { returnurl = returnurl }); 
            
        }
        public ActionResult Reg3()
        {
            int _memid = QueryString.IntSafeQ("memid");
            string _stamp = QueryString.SafeQNo("stamp");
            string returnurl = QueryString.SafeQNo("returnurl");
            if (_memid == 0) _memid = CookieBLL.GetRegisterCookie();
            if (_memid > 0)
            {
                VWMemberEntity member = MemberBLL.Instance.GetVWMember(_memid);
                if (member != null && member.MemId > 0 && member.Status == (int)MemberStatus.Register2 && member.TimeStampTab == _stamp)
                {
                    ViewBag.VWMember = member;
                    return View();
                }
            }

            return RedirectToAction("Reg", new { returnurl = returnurl });
             
           
        }
        #region 信息提交
        public string  RegSubmit()
        {
            ResultObj _obj = new ResultObj(); 
            string password = FormString.SafeQ("password");
            string mobile = FormString.SafeQ("mobile");
            string mobilecode = FormString.SafeQ("mobilecode"); 
            
            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null)
            {
                _obj.Status = (int)CommonStatus.RegisterVerCodeHasExDay;
            }
            else if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister].ToString() != mobile || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() != mobilecode)
            {
                _obj.Status = (int)CommonStatus.RegisterErrorVerCode;
            }
            else
            {
                MemberEntity _mem = new MemberEntity();
                _mem.MobilePhone = mobile;
                _mem.PassWord = password;
                //_mem.IsSupplier = issupplier;
                //_mem.IsStore = isstore;
                string code = CookieBLL.GetWeiXinWebCode();
                if (!string.IsNullOrEmpty(code))
                {
                    MemWeChatMsgEntity shortmsg = WeiXinJsSdk.Instance.GetWeChatShortInfo(code);
                    if (!string.IsNullOrEmpty(shortmsg.OpenId) && !string.IsNullOrEmpty(shortmsg.UnionId))
                    {
                        _mem.WeChat = shortmsg.UnionId; 
                    }
                } 
                _mem.CreateClientType = Globals.IsMobileDevice()? (int)ClientTypeEnum.Mobile:(int)ClientTypeEnum.PC;
                _mem.CreateIp = IPAddress.IP; 
                _mem.Status = (int)MemberStatus.Active; 
                _mem.LoginNum = 0; 
                ResultObj _loginentity = MemberLoginBLL.Instance.Register1(_mem);
                if (_loginentity.Status == (int)CommonStatus.Success)
                {
                    MemberEntity member = (MemberEntity)_loginentity.Obj;
                    CookieBLL.SetRegisterCookie(StringUtils.GetDbInt(member.Id));
                }
                _obj = _loginentity; 
            }
            return JsonJC.ObjectToJson(_obj);
        }
        public string Reg2Submit()
        {
            ResultObj _obj = new ResultObj();
            string useraccount = FormString.SafeQ("useraccount"); 
            string mobile = FormString.SafeQ("mobile");
            string stamp = FormString.SafeQ("stamp");
            string companyname = FormString.SafeQ("companyname");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            int comtype = FormString.IntSafeQ("comtype");
            string contractmanname = FormString.SafeQ("contractmanname"); 
            string address = FormString.SafeQ("address", 300);
            MemberEntity mem = MemberBLL.Instance.GetMemByMethod(mobile,LoginMethodEnum.MobilePhone);
            if(mem.Status!=(int)MemberStatus.Register1)
            {
                _obj.Status = (int)CommonStatus.RegisterNoModify;

                return JsonJC.ObjectToJson(_obj);
            }
            if(!(mem.TimeStampTab== stamp && CookieBLL.GetRegisterCookie()== mem.Id))
            {
                _obj.Status = (int)CommonStatus.RegisterFail;
            } 
            else
            {
                mem.MemCode = useraccount;
                MemberInfoEntity _meminfo = MemberInfoBLL.Instance.GetMemberInfoByMemId(mem.Id);
                _meminfo.MemId = mem.Id;
                _meminfo.Nickname = contractmanname;
                _meminfo.MemName = contractmanname;
                _meminfo.MobilePhone = mobile;
                MemberInfoBLL.Instance.AddMemberInfo(_meminfo);
                MemStoreEntity _store = new MemStoreEntity();
                _store.Address = address;
                _store.CityId = city;
                _store.CompanyName = companyname;
                _store.ContactsManName = contractmanname;
                _store.CreateTime = DateTime.Now ;
                _store.ProvinceId = province;
                _store.Status = (int)MemberStatus.WaitCheck;
                _store.StoreType = mem.StoreType;
                _store.MobilePhone = mobile;
                _store.MemId = mem.Id;  
                ResultObj _loginentity = MemberLoginBLL.Instance.Register2(mem, _meminfo, _store);
                //if (_loginentity.Status == (int)CommonStatus.Success)
                //{
                //    CookieBLL.SetRegisterCookie(StringUtils.GetDbInt(_loginentity.Obj));
                //}
                _obj = _loginentity; 
            }
            return JsonJC.ObjectToJson(_obj);
        }

        public string Reg3Submit()
        {

            ResultObj _obj = new ResultObj();
            string stamp = FormString.SafeQ("stamp");
            int memid = FormString.IntSafeQ("memid");
            string path = FormString.SafeQ("licensepath"); 
            MemberEntity mem = MemberBLL.Instance.GetMemByMethod(memid.ToString(), LoginMethodEnum.MemId);
            if (mem != null && mem.Id > 0 && mem.Status == (int)MemberStatus.Register2 && mem.TimeStampTab == stamp)
            {
                if (MemberBLL.Instance.RegisterCompanyLicense(memid, path) > 0)
                {
                    _obj.Status = (int)CommonStatus.Success;
                    _obj.Obj = MemberBLL.Instance.GetMember(memid);
                    return JsonJC.ObjectToJson(_obj);
                }
                _obj.Status = (int)CommonStatus.Fail;
                return JsonJC.ObjectToJson(_obj);
            }
            else
            { 
                _obj.Status = (int)CommonStatus.RegisterNoModify;
                return JsonJC.ObjectToJson(_obj);
            } 

        }
        #endregion



        /// <summary>
        /// 企业会员注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register2()
        {
            VWMemberEntity _en = new VWMemberEntity(); 
            ViewBag.MemberEntity = _en;
            return View();
        }
        public ActionResult Register3()
        {
            int memid = CookieBLL.GetRegisterCookie();
            if (memid > 0)
            {
                MemStoreEntity _en = StoreBLL.Instance.GetStoreByMemId(memid);
                if (_en != null)
                {
                    MemberEntity _mementity = MemberBLL.Instance.GetMember(memid);
                    if (_mementity.Status == (int)MemberStatus.Active)
                    {
                        //Response.Write("您已注册通过，请直接登录"); 
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.LicensePath = _en.LicensePath;
                    }
                }
            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        public ActionResult Register4()
        {
            MemBillVATEntity _en = new MemBillVATEntity();
            int memid = CookieBLL.GetRegisterCookie();

            if (memid > 0)
            {
                _en = MemBillVATBLL.Instance.GetMemBillVATByMemId(memid);
            }
            else
            {
                return RedirectToAction("Login");
            }
            ViewBag.BillEntity = _en;
            return View();
        }

        /// <summary>
        /// 检查注册手机唯一性
        /// </summary>
        /// <returns></returns>
        public string CheckPhoneIsExist()
        {
            ResultObj result = new ResultObj();
            string _mobilePhone = FormString.SafeQ("mobilePhone");

            if (MemberBLL.Instance.RegisterPhoneExist(_mobilePhone))
            {
                result.Status = (int)CommonStatus.Success;
            }
            else
            {
                result.Status = (int)CommonStatus.PhoneNotExist;
            }
            return JsonJC.ObjectToJson(result);
        }
        public string CheckPhoneIsNotExist()
        {
            ResultObj result = new ResultObj();
            string _mobilePhone = FormString.SafeQ("mobilePhone");

            if (MemberBLL.Instance.RegisterPhoneExist(_mobilePhone))
            {
                result.Status = (int)CommonStatus.PhoneExist;
            }
            else
            {
                result.Status = (int)CommonStatus.Success;
            }
            return JsonJC.ObjectToJson(result);
        }

        public string CheckHasMemPhone()
        {
            ResultObj result = new ResultObj();
            string _mobilePhone = FormString.SafeQ("mobilePhone");

            if (MemberBLL.Instance.MemPhoneExist(_mobilePhone))
            {
                result.Status = (int)CommonStatus.PhoneExist;
            }
            else
            {
                result.Status = (int)CommonStatus.Success;
            }

            return JsonJC.ObjectToJson(result);
        }


        #endregion

        #region 登录
        public string UserLogin()
        {
            ResultObj _result = new ResultObj();

            _result.Status = (int)CommonStatus.Fail;
            string useraccount = StringUtils.SafeStr(Request.Form["useraccount"]);
            string password = StringUtils.SafeStr(Request.Form["password"]);
            string VerCode = StringUtils.SafeStr(FormString.SafeQ("verCode"));
            int autologin = FormString.IntSafeQ("autologin");

            //if (VerCode.Length == 4)
            //{
            //    //string svercode = StringUtils.GetDbString(System.Web.HttpContext.Current.Session["verify_login"]);

            //    HttpCookie cookie = Request.Cookies["verify_login"];
            //    string svercode = "";
            //    if (cookie != null)
            //    {
            //        svercode = CryptDES.DESDecrypt(cookie.Value);
            //    }

            //    if (svercode != "")
            //    {
            //        if (VerCode.ToLower() != svercode.ToLower())
            //        {
            //            return "验证码输入错误";
            //        }
            //    }
            //}
            MemberLoginEntity _loginmodel = new MemberLoginEntity();
            string unionid = "";
            if(System.Web.HttpContext.Current.Session[CommonKey.WeChatUnionId]!=null)
            {
                unionid = System.Web.HttpContext.Current.Session[CommonKey.WeChatUnionId].ToString();
            }
            //string code = CookieBLL.GetWeiXinWebCode();
            //LogUtil.Log("微信传值：", code);
            //if (!string.IsNullOrEmpty(code))
            //{
            //    MemWeChatMsgEntity shortmsg = WeiXinJsSdk.Instance.GetWeChatShortInfo(code);
            //    LogUtil.Log("微信传值2：", shortmsg.UnionId+","+ shortmsg.OpenId);
            //    if (!string.IsNullOrEmpty(shortmsg.OpenId) && !string.IsNullOrEmpty(shortmsg.UnionId))
            //    {
            //        unionid = shortmsg.UnionId;
            //    }
            //}
            _result = MemberLoginBLL.Instance.Login(unionid,useraccount, password, (int)ClientTypeEnum.PC, IPAddress.IP);

            if (_result != null && _result.Obj != null)
            {
                _loginmodel = (MemberLoginEntity)_result.Obj;
                try
                {
                    if (_result.Status == (int)CommonStatus.Success)
                    { 
                        if (_loginmodel.Status == (int)MemberStatus.Register1|| _loginmodel.Status == (int)MemberStatus.Register2||_loginmodel.Status == (int)MemberStatus.WaitCheck)
                        {
                            CookieBLL.SetRegisterCookie(_loginmodel.MemId);
                        }
                        else
                        {
                            CookieBLL.SetLoginCookie(_loginmodel, autologin == 1);
                            CookieBLL.ComBineCart(1);
                            CookieBLL.ComBineCartXuQiu(1);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogUtil.Log(ex);
                    _result.Status = (int)CommonStatus.ServerError;
                }
            }
            return JsonJC.ObjectToJson(_result);

        }

        public string CheckLogin()
        {
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null)
            {
                if (!string.IsNullOrEmpty(member.MobilePhone))
                    return member.MobilePhone;
            }
            return "0";
        }
        public string CheckMemberHasLogin()
        {
            MemberLoginEntity member = CookieBLL.GetLoginCookie();

            if (member != null)
            {
                if (!string.IsNullOrEmpty(member.MobilePhone))
                    return "1";
            }
            return "0";
        }
        #endregion
         
        #region  公司注册
        public ActionResult Register()
        {
            var _errorcode = QueryString.IntSafeQ("error", (int)CommonStatus.Success);
            if (_errorcode != (int)CommonStatus.Success)
            {
                ViewBag.ErrorTitle = EnumEntityShow.Instance.GetEnumDes((CommonStatus)_errorcode);
            }
            return View();
        }
        public ActionResult RegMsg()
        {
            string _mobile = QueryString.SafeQ("mobile");
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

        public ActionResult RegisterAgree()
        {
            return View();
        }

        public string CompanyRegister()
        {
            ResultObj _obj = new ResultObj();
            string useraccount = FormString.SafeQ("useraccount");
            string password = FormString.SafeQ("password");
            string mobile = FormString.SafeQ("mobile");
            string mobilecode = FormString.SafeQ("mobilecode");
            string companyname = FormString.SafeQ("companyname");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            int comtype = FormString.IntSafeQ("comtype");
            string contractmanname = FormString.SafeQ("contractmanname");
            string email = FormString.SafeQ("email");
            string address = FormString.SafeQ("address", 300);
            string recommend = FormString.SafeQ("recommend");
            string lisenceurl = FormString.SafeQ("lisencepath", 500);
            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null)
            {
                _obj.Status = (int)CommonStatus.RegisterVerCodeHasExDay;
            }
            else if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister].ToString() != mobile || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() != mobilecode)
            {
                _obj.Status = (int)CommonStatus.RegisterErrorVerCode;
            }
            else
            {
                VWMemberEntity _mem = new VWMemberEntity();
                _mem.MemCode = useraccount;
                _mem.PassWord = password;
                _mem.ContactsMobile = mobile;
                _mem.CompanyName = companyname;
                _mem.CompanyProvinceId = province;
                _mem.CompanyCityId = city;
                _mem.CompanyAddress = address;
                _mem.ContactsManName = contractmanname;
                _mem.ContactsEmail = email;
                _mem.CompanyType = comtype;
                _mem.RecommendCode = recommend;
                _mem.CreateClientType = (int)ClientTypeEnum.PC;
                _mem.CreateIp = IPAddress.IP;
                _mem.IsStore = 1;
                _mem.LicensePath = lisenceurl;
                _mem.IsSupplier = 0;
                ResultObj _loginentity = MemberLoginBLL.Instance.RegisterStore(_mem);
                if (_loginentity.Status == (int)CommonStatus.Success)
                {
                    CookieBLL.SetRegisterCookie(StringUtils.GetDbInt(_loginentity.Obj));
                }
                _obj = _loginentity;

            }
            return JsonJC.ObjectToJson(_obj);
        }

        public string UploadLicense()
        { 
            int memid = CookieBLL.GetRegisterCookie();
            string memcodes = "";
            if (memid <= 0)
            {
                if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null)
                {
                    memcodes = System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister].ToString();
                }
            }
            else
            {
                memcodes = memid.ToString();
                ViewBag.MemId = memid;
            }
            if (!string.IsNullOrEmpty(memcodes))
            { 
                HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
                int aa = files.Count;
                 int _pictype = FormString.IntSafeQ("pictype");
                HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
                CertificateType _certype = (CertificateType)_pictype;
                  if (file != null)
                {
                    byte[] bytes = null;
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        bytes = binaryReader.ReadBytes(file.ContentLength);
                    }
                     FtpUtil _ftp = new FtpUtil();
                    Random _rd = new Random();
                      string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, ImagesSysPathCode.Certifacate.ToString(), memcodes, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                    string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));
                    string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                     _ftp.UploadFile(certifapath, bytes, true);
                   //return dicpathfull;
                    return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
                }
            }
            return "";
        }

        public string SaveLicense()
        {
            ResultObj _loginentity = new ResultObj();
            int memid = CookieBLL.GetRegisterCookie();
            string path = FormString.SafeQ("licensepath");
            if (memid > 0)
            {
                MemberEntity _mem = new MemberEntity();
                _mem = MemberBLL.Instance.GetMember(memid);
                if (_mem != null && _mem.Status == (int)MemberStatus.WaitCheck)
                {
                    MemberBLL.Instance.RegisterCompanyLicense(memid, path);
                    _loginentity.Status = (int)CommonStatus.Success;
                }
                else
                {
                    _loginentity.Status = (int)CommonStatus.Fail;
                }
            }
            else
            {
                _loginentity.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(_loginentity);
        }

        public string CompanyBillVATRegister()
        {
            ResultObj _loginentity = new ResultObj();
            int memid = CookieBLL.GetRegisterCookie();
            if (memid > 0)
            {
                MemberEntity _mem = MemberBLL.Instance.GetMember(memid);
                if (_mem != null)
                {
                    if (_mem.Status == (int)MemberStatus.Active)
                    {
                        _loginentity.Status = (int)CommonStatus.HasAccount;
                    }
                    else
                    {
                        string companyname = FormString.SafeQ("companyname");
                        string companycode = FormString.SafeQ("companycode");
                        string companyaddress = FormString.SafeQ("companyaddress");
                        string companyphone = FormString.SafeQ("companyphone");
                        string companybank = FormString.SafeQ("companybank");
                        string bankaccount = FormString.SafeQ("bankaccount");
                        string receivername = FormString.SafeQ("receivername");
                        int province = FormString.IntSafeQ("province");
                        int city = FormString.IntSafeQ("city");
                        string receiverphone = FormString.SafeQ("receiverphone");
                        string receiveraddress = FormString.SafeQ("receiveraddress");

                        MemBillVATEntity _bill = new MemBillVATEntity();
                        _bill.BankAccount = bankaccount;
                        _bill.BillType = (int)BillType.VAT;
                        _bill.CompanyAddress = companyaddress;
                        _bill.CompanyBank = companybank;
                        _bill.CompanyCode = companycode;
                        _bill.CompanyName = companyname;
                        _bill.CompanyPhone = companyphone;
                        _bill.CreateTime = DateTime.Now;
                        _bill.MemId = memid;
                        _bill.ReceiverAddress = receiveraddress;
                        _bill.ReceiverCity = city;
                        _bill.ReceiverName = receivername;
                        _bill.ReceiverPhone = receiverphone;
                        _bill.ReceiverProvince = province;
                        _bill.UpdateTime = DateTime.Now;

                        int billid = MemBillVATBLL.Instance.AddMemBillVAT(_bill);
                        if (billid > 0)
                        {

                            _loginentity.Status = (int)CommonStatus.Success;
                        }
                        else
                        {
                            _loginentity.Status = (int)CommonStatus.Fail;
                        }
                    }

                }
                else
                {
                    _loginentity.Status = (int)CommonStatus.Fail;
                }
            }
            else
            {
                _loginentity.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(_loginentity);
        }
        #endregion


      

        #region 检查Email是否存在
        /// <summary>
        /// 检查Email是否存在
        /// </summary>
        /// <returns></returns>
        public string CheckMemberCode()
        {
            string email = QueryString.SafeQ("useraccount");
            int exid = QueryString.IntSafeQ("userid");
            if (email.ToString() != "")
            {
                MemberEntity _mem = MemberBLL.Instance.GetMemberByCode(email);
                if (_mem != null && _mem.Id>0 && _mem.Id != exid)
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
         
        #region 清除缓存
        public void ClearMemCache()
        {
            string _cachekey = QueryString.SafeQ("id");
            MemCache.RemoveAllCache();
        }

        #endregion


        #region 发送手机验证码
        public string SendMobileMessage()
        {
            ResultObj _loginentity = new ResultObj();
            string mobilestr = QueryString.SafeQ("mobile");
            long ip = IPAddress.DenaryIp;
            MobileMessageEntity _entity = new MobileMessageEntity();
            _entity.IpInt = ip;
            _entity.MobilePhone = mobilestr;
            _entity.CreateTime = DateTime.Now;
            if (MobileMessageBLL.Instance.SendNum(ip) > 20)
            {
                _loginentity.Status = (int)CommonStatus.ExceedDay;
            }

            DateTime? latedate = MobileMessageBLL.Instance.GetLatelySend(ip);
            bool sendphone = true;
            if (latedate != null)
            {
                DateTime startTime = Convert.ToDateTime(latedate);
                DateTime endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                TimeSpan ts = endTime - startTime;
                if (ts.TotalSeconds <= 60)  //当日次数未起过20次，间隔时间不超过1分钟
                {
                    _loginentity.Status = (int)CommonStatus.LatelyTime;
                    sendphone = false;
                }
            }
            if (sendphone && StringUtils.IsHandset(mobilestr))
            {
                if (StringUtils.GetDbBool(System.Web.HttpContext.Current.Session[CommonKey.MobileSending]))
                {
                    _loginentity.Status = (int)CommonStatus.PhoneSendGap;
                    sendphone = false;
                    if (System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] == null)
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] = DateTime.Now;
                    }
                    else if (StringUtils.GetDbDateTime(System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime]) < DateTime.Now.AddSeconds(-10))
                    {
                        sendphone = true;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                    }
                }
                if (sendphone)
                {
                    try
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = true;
                        Random _r = new Random();
                        string mycode = "111111";// _r.Next(100000, 999999).ToString();
                        string datenowstr = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string _regMsgBody = ConfigCore.Instance.ConfigCommonEntity.MobileRegMsgBody;
                        string msgbody = string.Format(_regMsgBody, mycode);
                        _entity.MessageContent = msgbody;
                        //MobileMessageEntity smsresult = ConfigSmsProviderBLL.Instance.SendSms(_entity);
                        MobileMessageEntity smsresult =  _entity;

                        System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] = DateTime.Now;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] = mycode;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileNo] = mobilestr;
                        MobileMessageBLL.Instance.AddMobileMessage(smsresult);
                        _loginentity.Status = (int)CommonStatus.Success;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                        return JsonJC.ObjectToJson(_loginentity);

                    }
                    catch (Exception ex)
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                        LogUtil.Log("注册验证码生成失败", ex.Message);
                        _loginentity.Status = (int)CommonStatus.Fail;
                    }
                }
            }
            return JsonJC.ObjectToJson(_loginentity);
        }

        /// <summary>
        /// 注册发送手机验证码
        /// </summary>
        /// <returns></returns>
        public string SendRegisterMobileMessage()
        {
            ResultObj _loginentity = new ResultObj();
            string mobilestr = QueryString.SafeQ("mobile");
            long ip = IPAddress.DenaryIp;
            MobileMessageEntity _entity = new MobileMessageEntity();
            _entity.IpInt = ip;
            _entity.MobilePhone = mobilestr;
            _entity.CreateTime = DateTime.Now;
            if (MobileMessageBLL.Instance.SendNum(ip) > 30)//当前IP最多一天只能发送30条注册验证短信
            {
                _loginentity.Status = (int)CommonStatus.ExceedDay;
            }

            DateTime? latedate = MobileMessageBLL.Instance.GetLatelySend(ip);
            bool sendphone = true;
            if (latedate != null)
            {
                DateTime startTime = Convert.ToDateTime(latedate);
                DateTime endTime = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                TimeSpan ts = endTime - startTime;
                if (ts.TotalSeconds <= 60)  //当日次数未起过20次，间隔时间不超过1分钟
                {
                    _loginentity.Status = (int)CommonStatus.LatelyTime;
                    sendphone = false;
                }
            }
            if (sendphone && StringUtils.IsHandset(mobilestr))
            {
                if (StringUtils.GetDbBool(System.Web.HttpContext.Current.Session[CommonKey.MobileSending]))
                {
                    _loginentity.Status = (int)CommonStatus.PhoneSendGap;
                    sendphone = false;
                    if (System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] == null)
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] = DateTime.Now;
                    }
                    else if (StringUtils.GetDbDateTime(System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime]) < DateTime.Now.AddSeconds(-10))
                    {
                        sendphone = true;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                    }
                }
                if (sendphone)
                {
                    try
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = true;
                        Random _r = new Random();
                        string mycode = _r.Next(100000, 999999).ToString();
                        string datenowstr = DateTime.Now.ToString("yyyyMMddHHmmss");
                        string _regmsgbody = ConfigCore.Instance.ConfigCommonEntity.MobileRegMsgBody;
                        string msgbody = string.Format(_regmsgbody, mycode);
                        _entity.MessageContent = msgbody;
                        MobileMessageEntity smsresult = ConfigSmsProviderBLL.Instance.SendSms(_entity);
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] = DateTime.Now;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] = mycode;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] = mobilestr;

                        MobileMessageBLL.Instance.AddMobileMessage(smsresult);
                        _loginentity.Status = (int)CommonStatus.Success;
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                        return JsonJC.ObjectToJson(_loginentity);

                    }
                    catch (Exception ex)
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSending] = false;
                        LogUtil.Log("注册验证码生成失败", ex.Message);
                        _loginentity.Status = (int)CommonStatus.Fail;
                    }
                }
            }
            return JsonJC.ObjectToJson(_loginentity);
        }


        #endregion


        #region 身份证图片上传

        /// <summary>
        /// 身份证图片上传
        /// </summary>
        /// <returns></returns>
        public int IDImagesSubmit()
        {
            int _memId = FormString.IntSafeQ("MemId");
            string _IDPre = FormString.SafeQ("IDPre");
            string _IDBeh = FormString.SafeQ("IDBeh");

            MemberInfoEntity _entity = new MemberInfoEntity();
            _entity.MemId = _memId;
            _entity.IdentityPre = _IDPre;
            _entity.IdentityBeh = _IDBeh;

            int _result = 0;

            _result = MemberInfoBLL.Instance.AddMemberInfo(_entity);

            return _result;

        }

        #endregion

        #region  找回密码

        ///// <summary>
        /////  找回密码
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult FindPwd()
        //{
        //    return View();
        //}


        /// <summary>
        /// 确认个人信息,忘记密码
        /// </summary>
        /// <returns></returns>
        public string ConfirmPersonalInfo()
        {
            ResultObj result = new ResultObj();
            string _mobile = FormString.SafeQ("mobile");
            string _vCode = FormString.SafeQ("vCode");

            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNo] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileNo].ToString() == _mobile && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() == _vCode)
            {

                MemberEntity _entity = MemberBLL.Instance.GetMemByMobile(_mobile);
                if (_entity.Id > 0)
                { 
                    if (MemberBLL.Instance.UpdateMember(_entity) > 0)
                    {
                        MemberEntity _upbehinden = MemberBLL.Instance.GetMember(_entity.Id);
                        System.Web.HttpContext.Current.Session.Add(CommonKey.FindPwdID, _entity.Id);
                        result.Status = (int)CommonStatus.Success;
                        result.Obj = _upbehinden.TimeStampTab;
                    }
                    else
                    {
                        result.Status = (int)CommonStatus.Fail;
                    }
                }
                else
                {
                    result.Status = (int)CommonStatus.PhoneNotExist;
                }
            }
            else
            {
                result.Status = (int)CommonStatus.VerCodeIsError;
            }
            return JsonJC.ObjectToJson(result);
        }


        ///// <summary>
        ///// 修改密码
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult ModifyPwd()
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
        //            return RedirectToAction("FindPwd");
        //        }
        //    }

        //    return RedirectToAction("FindPwd");

        //}


        /// <summary>
        /// 提交修改密码信息
        /// </summary>
        /// <returns></returns>
        public int SubmitPwdInfo()
        {
            string _password = FormString.SafeQ("newpassword");
            if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
            {
                int _id = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
                if (_id > 0)
                {
                    MemberEntity _entity = MemberBLL.Instance.GetMember(_id);
                    _entity.PassWord = CryptMD5.Encrypt(_password);
                    return MemberBLL.Instance.UpdateMember(_entity);
                }

                return 0;
            }
            return 0;
        }


        ///// <summary>
        ///// 找到密码
        ///// </summary>
        ///// <returns></returns>
        //public ActionResult FoundPwd()
        //{
        //    long _key = QueryString.LongIntSafeQ("k");
        //    if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
        //    {
        //        int _memid = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
        //        if (_memid > 0)
        //        {
        //            MemberEntity _entity = MemberBLL.Instance.GetMember(_memid);
        //            if (_entity.TimeStampCode == _key)
        //            {
        //                return View();
        //            }
        //            return RedirectToAction("FindPwd", new { returnurl = returnurl });
        //        }
        //    }

        //    return RedirectToAction("FindPwd", new { returnurl = returnurl });

        //}

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
        public ActionResult ForgetPwdReset()
        {
            string _key = QueryString.SafeQ("key");
            string returnurl = QueryString.SafeQNo("returnurl");
            if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
            {
                int _memid = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
                if (_memid > 0)
                {
                    MemberEntity _entity = MemberBLL.Instance.GetMember(_memid);
                    if (_entity.TimeStampTab== _key)
                    {
                        ViewBag.key = _key;
                        return View();
                    }
                    return RedirectToAction("ForgetPwd", new { returnurl = returnurl });
                }
            }
            return RedirectToAction("ForgetPwd", new { returnurl = returnurl });
        }
        public ActionResult ForgetPwdSuccess()
        {
            return View();
        }

        #endregion




    }
}
