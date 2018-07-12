using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
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

namespace SuperMarket.Web.Controllers
{
    public class HomeController : BaseCommonController
    {

        #region 首页
        public ActionResult CategroyCms()
        {

            int _id = QueryString.IntSafeQ("m");
            ViewBag.Method = _id;
            return View();
        }
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            if(Globals.IsMobileDevice())
            {
              return   Redirect("http://m.aahama.com");
            }
            int siteid=QueryString.IntSafeQ("s");
            if (siteid == 0) siteid = 1;
            ViewBag.SiteId =siteid;


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

        /// <summary>
        /// 公告动态
        /// </summary>
        /// <returns></returns>
        public ActionResult Notice()
        {
            int _id = QueryString.IntSafeQ("id");
            B2BNoticeEntity _entity = B2BNoticeBLL.Instance.GetB2BNotice(_id);
            ViewBag.Entity = _entity;
            return View();
        }

        #endregion

        #region 会员注册 
        public ActionResult Reg()
        {
            return View();
        }
        /// <summary>
        /// 上传证件
        /// </summary>
        /// <returns></returns>
        public ActionResult Reg2()
        {
            int _memId = QueryString.IntSafeQ("memId");
            if(_memId==0) _memId = CookieBLL.GetRegisterCookie();
             if(_memId>0)
            {
                MemberInfoEntity enti = MemberInfoBLL.Instance.GetMemberInfoByMemId(_memId);
                if(enti!=null&& enti.Id>0)
                {
                    ViewBag.LegalIdentityPre = enti.IdentityPre;
                    ViewBag.LegalIdentityBeh = enti.IdentityBeh;
                }ViewBag.MemId = _memId;
            }
            else
            {
                RedirectToAction("Reg");
            }
                
            return View();
        }
        /// <summary>
        /// 企业会员注册
        /// </summary>
        /// <returns></returns>
        public ActionResult Register2()
        {
            VWMemberEntity _en = new VWMemberEntity();
            ////注册后直接登录
            //int memid = CookieBLL.GetRegisterCookie();
            //if (memid > 0)
            //{
            //    _en = MemberBLL.Instance.GetVWMember(memid);
            //    if (_en != null)
            //    {
            //        if (_en.Status == 1)
            //        {
            //            //Response.Write("您已注册通过，请直接登录"); 
            //            return RedirectToAction("Login");
            //        }
            //    }
            //}
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

            if(MemberBLL.Instance.RegisterPhoneExist(_mobilePhone))
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
            _result = MemberLoginBLL.Instance.Login("",useraccount, password, (int)ClientTypeEnum.PC, IPAddress.IP);

            if (_result != null && _result.Obj != null)
            {
                _loginmodel = (MemberLoginEntity)_result.Obj;
                try
                {
                    if (_result.Status == (int)CommonStatus.Success)
                    {
                        if (_loginmodel.Status == (int)MemberStatus.WaitCheck)
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

        #region 注册事件

        public string UserRegister()
        {

            //string VerCode = StringUtils.SafeStr(FormString.SafeQ("VerCode"));
            //if (VerCode.Length == 4)
            //{
            //    HttpCookie cookie = Request.Cookies["verify_register"];
            //    string svercode = "";
            //    if (cookie != null)
            //    {
            //        svercode = Core.DES.DESDecrypt(cookie.Value);
            //    }

            //    if (svercode != "")
            //    {
            //        if (VerCode.ToLower() != svercode.ToLower())
            //        {
            //            return "011110";
            //        }
            //    }
            //}
            //else
            //{
            //    return "011110";
            //}}
            ResultObj _result = new ResultObj();
            string useraccount = StringUtils.SafeStr(FormString.SafeQ("useraccount"));
            string password = StringUtils.SafeStr(FormString.SafeQ("PassWord"));
            string mobile = StringUtils.SafeStr(FormString.SafeQ("mobile"));
            string mobilecode = StringUtils.SafeStr(FormString.SafeQ("mobilecode"));
            ///验证码
            //if (System.Web.HttpContext.Current.Session[CommonKey.MobileNo] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileNo].ToString() == mobile && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() == mobilecode)
            //{
            _result = MemberLoginBLL.Instance.Register(useraccount, password, mobile, (int)ClientTypeEnum.PC, IPAddress.IP, "");
            //CookieBLL.SetLoginCookie(_loginentity);
            //CookieBLL.ComBineCart(1);
            if (_result.Status == (int)CommonStatus.Success)
            {
                CookieBLL.SetRegisterCookie(StringUtils.GetDbInt(_result.Obj));
            }
                //if (StringUtils.IsEmail(useraccount))
                //{
                //    //此处可以异步
                //    string contenturl = SuperMarket.Core.Globals.WebUrl + "email/RegisterSuccess.aspx?MemCode=" + email + "&Id=" + _loginentity.Member.Id;
                //    try
                //    {
                //        SendEmailSmsEntity sendemail = new SendEmailSmsEntity();
                //        sendemail.Title = "注册成功通知";
                //        sendemail.SendTo = email;
                //        sendemail.CreateTime = DateTime.Now;
                //        sendemail.CreateBy = "网站";
                //        sendemail.Status = 0;
                //        sendemail.MemCode = email;
                //        sendemail.EmailContent = StringUtils.GetContent(contenturl);
                //        sendemail.PriorIndex = 100;
                //        if (sendemail.EmailContent != "")
                //        {
                //            SendEmailSmsBLL.Instance.AddSendEmailSms(sendemail);
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        LogUtil.Log(ex);
                //    }
                //}  
            //}
            //else
            //{
            //    _result.Status = (int)CommonStatus.RegisterErrorVerCode;
            //}
            return JsonJC.ObjectToJson(_result);
        }

        #endregion


        #region  公司注册
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
            string lisenceurl = FormString.SafeQ("lisencepath",500);
            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null )
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
                if (System.Web.HttpContext.Current.Session[CommonKey.MobileNoRegister] != null &&   System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null  )
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
                     string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName,  ImagesSysPathCode.Certifacate.ToString(), memcodes, DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
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


        #region 退出登录
        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        public ActionResult LoginOut()
        {

            string[] cookieName = System.Web.HttpContext.Current.Request.Cookies.AllKeys;
            for (var i = 0; i < cookieName.Length; i++)
            {
                //if (cookieName[i] == SysCookieName.ShoppingCartCount_Data || cookieName[i] == SysCookieName.ShoppingCart_Data)
                //    continue;
                HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[cookieName[i]];
                cookie.Expires = DateTime.Now.AddDays(-30);
                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
            }
            //System.Web.HttpContext.Current.Session.Clear();

            //System.Web.HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies[SysCookieName.MemberCookieName];
            //if (cookie != null)
            //{
            //    cookie.Expires = DateTime.Now.AddDays(-1);
            //    Response.Cookies.Add(cookie);
            //}

            //System.Web.HttpContext.Current.Response.Cookies[SysCookieName.DefaultCusName].Expires = DateTime.Now.AddDays(-1);

            //string url = StringUtils.GetDbString(Request.UrlReferrer);
            //if (url == "")
            //{
            //    url = "/Home/Login";
            //}
            return Redirect("/Home/Login");
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
                if (_mem!=null&& _mem.Id!= exid)
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


        #region 获取cms
        public static string GetCms(int Model)
        {
            return GetCms(Model, "");
        }

        public static string GetCms(int Model, string ImgType)
        {
            string html = "";
            CmsContentEntity _entity = CmsContentBLL.Instance.GetCmsContent(Model);
            if (_entity != null && !string.IsNullOrEmpty(_entity.CmsContent))
            {
                html = _entity.CmsContent;
            }
            return html;
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
                    if(System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime]==null)
                    {
                        System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime] = DateTime.Now;
                    }
                    else if(StringUtils.GetDbDateTime(System.Web.HttpContext.Current.Session[CommonKey.MobileSendPreTime])< DateTime.Now.AddSeconds(-10))
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
                        string _regMsgBody = ConfigCore.Instance.ConfigCommonEntity.MobileRegMsgBody;
                        string msgbody = string.Format(_regMsgBody, mycode); 
                        _entity.MessageContent = msgbody;
                        MobileMessageEntity smsresult = ConfigSmsProviderBLL.Instance.SendSms(_entity);




                        //Hashtable _Pars = new Hashtable();
                        //_Pars.Add("username", _config.UserCode);
                        //_Pars.Add("password", CryptMD5.Encrypt(CryptMD5.Encrypt(_config.PassWord.Trim()).ToLower() + datenowstr).ToLower());
                        //_Pars.Add("mobile", mobilestr);
                        //_Pars.Add("content", msgbody);
                        //_Pars.Add("tkey", datenowstr);
                        //_Pars.Add("productid", _config.AppId);
                        //_Pars.Add("xh", "");

                        //WebServiceClient.QueryPostWebService(_config.Url, _Pars);
                        //System.Web.HttpContext.Current.Response.Cookies.Add(new HttpCookie(CommonKey.MobileYZCode, mycode));
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
                        string _regMsgBody = ConfigCore.Instance.ConfigCommonEntity.MobileRegMsgBody;
                        string msgbody = string.Format(_regMsgBody, mycode); 
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

        /// <summary>
        ///  找回密码
        /// </summary>
        /// <returns></returns>
        public ActionResult FindPwd()
        {
            return View();
        }


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
                        MemberEntity _memnew = MemberBLL.Instance.GetMember(_entity.Id);
                        System.Web.HttpContext.Current.Session.Add(CommonKey.FindPwdID, _entity.Id);
                        result.Status = (int)CommonStatus.Success;
                        result.Obj = _memnew.TimeStampTab;
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


        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult ModifyPwd()
        {
            string _key = QueryString.SafeQ("key");
            if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
            {
                int _memid = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
                if (_memid > 0)
                {
                    MemberEntity _entity = MemberBLL.Instance.GetMember(_memid);
                    if (_entity.TimeStampTab == _key)
                    {
                        ViewBag.key = _key;
                        return View();
                    }
                    return RedirectToAction("FindPwd");
                }
            }

            return RedirectToAction("FindPwd");

        }


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
        //    string _key = QueryString.SafeQ("k");
        //    if (System.Web.HttpContext.Current.Session[CommonKey.FindPwdID] != null)
        //    {
        //        int _memid = Convert.ToInt32(System.Web.HttpContext.Current.Session[CommonKey.FindPwdID]);
        //        if (_memid > 0)
        //        {
        //            MemberEntity _entity = MemberBLL.Instance.GetMember(_memid);
        //            if (_entity.TimeStampTab == _key)
        //            {
        //                return View();
        //            }
        //            return RedirectToAction("FindPwd");
        //        }
        //    }

        //    return RedirectToAction("FindPwd");

        //}

        #endregion


        #region 浏览记录登记

        public string MsgProductClickAdd()
        { 
            int _pdId = FormString.IntSafeQ("pdid");
            if (_pdId > 0)
            {
                int _sysId = FormString.IntSafeQ("sysid");
                string _ip = IPAddress.IP;
                int memid = 0;
                MemberLoginEntity member = CookieBLL.GetLoginCookie();
                if (member != null)
                {
                    memid = member.MemId;
                    string cookievalue = CookieBLL.GetMemBrowseLogCookie();
                    if (string.IsNullOrEmpty(cookievalue))
                    {
                        MemBrowseLogEntity brouselog = new MemBrowseLogEntity();
                        brouselog.MemId = memid;
                        brouselog.ProductDetailId = _pdId;
                        brouselog.SystemId = _sysId;
                        MemBrowseLogBLL.Instance.AddMemBrowseLog(brouselog);
                    }
                    else
                    { 
                        CookieBLL.AddMemBrowseLogCookie(_pdId);
                        cookievalue = CookieBLL.GetMemBrowseLogCookie();
                        MemBrowseLogBLL.Instance.AddMemBrowseLogStr(cookievalue,memid, _sysId);
                        CookieBLL.ClearBrowseLogCookie();
                    }
                }
                else
                {
                    CookieBLL.AddMemBrowseLogCookie(_pdId);
                }
                MemProductClickEntity entity = new MemProductClickEntity();
                entity.MemId = memid;
                entity.ProductDetailId = _pdId;
                entity.ClickIp = _ip;
                entity.SystemId = _sysId;
                MemProductClickBLL.Instance.AddMemProductClick(entity);
            }
            return "";
        }
        #endregion





    }
}
