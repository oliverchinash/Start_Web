using SuperMarket.BLL.Account;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class SysMemHomeController: BaseMemAdminController
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberList()
        {
             string companyname = QueryString.SafeQ("companyName");
            string memname = QueryString.SafeQ("memname");
            string _memcode = QueryString.SafeQ("memcode");
            string mobilephone = QueryString.SafeQ("mobilephone");
            string supplierurl = QueryString.SafeQNo("supplierurl");
            string brandname = QueryString.SafeQ("brandname");
            string _contractname = QueryString.SafeQ("contractname");
            int _status = QueryString.IntSafeQ("status", -1);
            int memtype = QueryString.IntSafeQ("memtype", -1); 
            int issupplier = QueryString.IntSafeQ("issupplier", -1);
            int isstore = QueryString.IntSafeQ("isstore", -1); 
            int issys = QueryString.IntSafeQ("issys", -1);
            int _pagesize = CommonKey.PageSizeCheck;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);

            int _recordCount = 0;

           

            IList<VWMemberEntity> _entitylist = MemberBLL.Instance.GetVWMemberList(_pagesize, _pageindex, ref _recordCount, _memcode, _status, companyname, mobilephone, issupplier, isstore, issys,memname, brandname);


            ViewBag.entitylist = _entitylist;

            string _url = "/SysMemHome/MemberList?memcode=" + _memcode + "&mobilephone=" + mobilephone + "&companyName=" + companyname + "&status=" + _status + "&memtype=" + memtype + "&issupplier=" + issupplier + "&isstore=" + isstore + "&issys=" + issys;  
            string _pagestr = HTMLPage.GetSysListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pagestr;
            ViewBag.MemType = memtype;
            ViewBag.Status = _status;
            ViewBag.MemCode = _memcode;
            ViewBag.MemName = memname;
            ViewBag.CompanyName = companyname;
            ViewBag.MobilePhone = mobilephone;
            ViewBag.BrandName = brandname; 
            
            return View();
        }
        /// <summary>
        /// 发送给虚拟报价员用户访问的网址，待转发
        /// </summary>
        /// <returns></returns>
        public string SendQuoteUrl()
        {
            ResultObj _obj = new ResultObj();
            int memid = FormString.IntSafeQ("memid");
            string oldurl = StringUtils.SafeCodeSmall(FormString.SafeQNo("oldurl",8000));
            if (!string.IsNullOrEmpty(oldurl))
            {
                VWMemberEntity member = MemberBLL.Instance.GetVWMember(memid);
                string tempcode = Server.UrlEncode(CryptDES.DESEncrypt(member.TimeStampTab));
                string baseurl;
                Dictionary<string,string> nvc = new Dictionary<string, string>();
                StringUtils.ParseUrl(oldurl, out baseurl, out nvc);
                nvc.Remove("wechatcode");
                nvc.Add("tempcode",  tempcode);
                string url = baseurl + "?";
                foreach (string key in nvc.Keys)
                {
                    url += "&" + key + "=" + nvc[key];
                }
                bool result = WeiXinCustomerBLL.Instance.SendCGUrlToManager(url, memid);
                if (result)
                {
                    _obj.Status = (int)CommonStatus.Success;
                }
                else
                {
                    _obj.Status = (int)CommonStatus.Fail; 
                } 
            }
            else
            { 
                _obj.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(_obj);
        }

        public ActionResult MemberDetail()
        {

            int memid = QueryString.IntSafeQ("memid");
            MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(memid);
            MemberEntity member =MemberBLL.Instance.GetMember(memid);
            ViewBag.Member = member;
            ViewBag.Store = store;
            ViewBag.TempCode = Server.UrlEncode(CryptDES.DESEncrypt(member.TimeStampTab));
            return View();
        }
        public ActionResult MemberSimpleAdd()
        {
            SuperMarket.Model.VWMemberEntity _member = new VWMemberEntity();
            ViewBag.VWMember= _member;
            return View();
        }
       public string MemSimpleAddSubmit()
        {
            ResultObj _obj = new ResultObj();
            string useraccount = FormString.SafeQ("useraccount");
            string mobile = FormString.SafeQ("mobile"); 
            string companyname = FormString.SafeQ("companyname");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            int issysuser = FormString.IntSafeQ("issysuser");
            int issupplier = FormString.IntSafeQ("issupplier");
            int isstore = FormString.IntSafeQ("isstore");
            int comtype = FormString.IntSafeQ("comtype");
            string contractmanname = FormString.SafeQ("contractmanname");
            string address = FormString.SafeQ("address", 300);
            MemberEntity mem = MemberBLL.Instance.GetMemByMethod(mobile, LoginMethodEnum.MobilePhone);
            if(mem.Id==0)
            {
                mem.MemCode = "xl" + mobile;
                mem.MobilePhone = mobile;
                mem.IsSysUser = issysuser;
                mem.IsSupplier = issupplier;
                mem.IsStore = isstore;
                mem.Status = (int)MemberStatus.Active;
                mem.CreateTime = DateTime.Now;
                mem.PassWord = DateTime.Now.ToShortDateString();
                mem.Id= MemberBLL.Instance.AddMember(mem); 
            }
            else
            {
                mem.MobilePhone = mobile;
                mem.IsSysUser = issysuser;
                mem.IsSupplier = issupplier;
                mem.IsStore = isstore;  
                MemberBLL.Instance.UpdateMember(mem); 
            }
            if (mem.Id > 0)
            {

                MemberInfoEntity _meminfo = MemberInfoBLL.Instance.GetMemberInfoByMemId(mem.Id);
                if (_meminfo.Id == 0)
                {
                    _meminfo.MemId = mem.Id;
                    _meminfo.Nickname = contractmanname;
                    _meminfo.MemName = contractmanname;
                    _meminfo.MobilePhone = mobile;
                    MemberInfoBLL.Instance.AddMemberInfo(_meminfo);
                }
                MemStoreEntity _store = StoreBLL.Instance.GetStoreByMemId(mem.Id);

                _store.Address = address;
                _store.CityId = city;
                _store.CompanyName = companyname;
                _store.ContactsManName = contractmanname;
                _store.CreateTime = DateTime.Now;
                _store.ProvinceId = province;
                _store.StoreType = mem.StoreType;
                _store.MobilePhone = mobile;
                _store.MemId = mem.Id;
                if (_store.Id == 0)
                {
                    _store.Status = (int)MemberStatus.Active;
                    StoreBLL.Instance.AddStore(_store);
                }
                else
                {
                    StoreBLL.Instance.UpdateStore(_store);
                }
                _obj.Status = (int)CommonStatus.Success;
                _obj.Obj = mem.MobilePhone;
            }
            return JsonJC.ObjectToJson(_obj);
        }

    }
}
