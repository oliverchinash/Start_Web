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
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
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
    public class MemberController : BaseMemberController
    {
        #region 个人信息
        public ActionResult PersonalMsg()
        {
            VWMemberEntity mem = MemberBLL.Instance.GetVWMember(memid);
            ViewBag.VWMember = mem;
            return View();
        }
        public string SaveBasicMsg()
        {
            ResultObj _loginentity = new ResultObj();
            int memid = CookieBLL.GetRegisterCookie();
            string mobile = FormString.SafeQ("mobile");
            string nikename = FormString.SafeQ("nikename");
            if (memid > 0)
            {
                VWMemberEntity _mem = new VWMemberEntity();
                _mem.MemId = memid;
                _mem.MemNikeName = nikename;
                int rei = MemberBLL.Instance.BasicMsgUpdate(_mem);
                if (rei > 0)
                {
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
        #endregion

        #region 收货地址
        public ActionResult Address()
        {
            return View();
        }
        /// <summary>
        /// 收货地址
        /// </summary>
        /// <returns></returns>
        public string GetReceiptAddress()
        {
            int _id = FormString.IntSafeQ("addressid");

            MemPostAddressEntity _entity = new MemPostAddressEntity();
            _entity = PostAddressBLL.Instance.GetPostAddress(_id);
            if (_entity.MemId == memid)
                return JsonJC.ObjectToJson(_entity);
            else return JsonJC.ObjectToJson(new MemPostAddressEntity());
        }

        /// <summary>
        /// 添加收货地址
        /// </summary>
        /// <returns></returns>
        public string AddPostAddress()
        {
            ResultObj _result = new ResultObj();
            int _id = FormString.IntSafeQ("addressid");
            int _province = FormString.IntSafeQ("province");
            string _acceptername = FormString.SafeQ("acceptername");
            int _city = FormString.IntSafeQ("city");
            string _countryname = FormString.SafeQ("countryname", 200);
            int _cttype = FormString.IntSafeQ("cttype");
            string _mobilephone = FormString.SafeQ("mobilephone");
            string _address = FormString.SafeQ("address");
            string _telephone = FormString.SafeQ("telephone", 200);
            string _email = FormString.SafeQ("email", 200);
            int _isdefault = FormString.IntSafeQ("isdefault");

            MemPostAddressEntity _entity = new MemPostAddressEntity();
            if (_id > 0)//如果是更新
            {
                _entity = PostAddressBLL.Instance.GetPostAddress(_id);//获取地址实体
                if (_entity.MemId != memid)
                {
                    _result.Status = (int)CommonStatus.Fail;
                    return JsonJC.ObjectToJson(_result);
                }
            }

            _entity.Id = _id;
            _entity.AccepterName = _acceptername;
            _entity.CountryName = _countryname;
            _entity.Address = _address;
            _entity.ProvinceId = _province;
            _entity.CityId = _city;
            _entity.CtType = _cttype;
            _entity.MemId = memid;
            _entity.MobilePhone = _mobilephone;
            _entity.Telephone = _telephone;
            _entity.Email = _email;
            _entity.IsDefault = _isdefault;

            if (_entity.Id > 0)//address已经存在 表示更新
            {
                if (PostAddressBLL.Instance.UpdatePostAddress(_entity) > 0)
                {
                    _result.Status = (int)CommonStatus.Success;
                    _result.Obj = PostAddressBLL.Instance.GetPostAddress(_entity.Id);
                    return JsonJC.ObjectToJson(_result);
                }
            }
            else if (PostAddressBLL.Instance.GetNumForAddress(memid) < 10) //address不存在 表示添加
            {
                _entity.IsDefault = 0;
                _entity.Sort = 0;
                int _Id = PostAddressBLL.Instance.AddPostAddress(_entity);
                if (_Id > 0)
                {
                    _result.Status = (int)CommonStatus.Success;
                    _result.Obj = PostAddressBLL.Instance.GetPostAddress(_Id);
                    return JsonJC.ObjectToJson(_result);
                }
            }
            else
            {
                _result.Status = (int)CommonStatus.AddressNumSuper;
                return JsonJC.ObjectToJson(_result);

            }
            _result.Status = (int)CommonStatus.Fail;
            return JsonJC.ObjectToJson(_result);
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <returns></returns>
        public string GetPostAddressList()
        {
            int _pageindex = FormString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;//获得当前页的页数 
            int _pagesize = (int)CommonKey.PageSizeAddress;
            int recordcount = 0;
            ListObj tobj = new ListObj();//定义实际需要的对象
            IList<ConditionUnit> _seachwhere = new List<ConditionUnit>();
            _seachwhere.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            IList<MemPostAddressEntity> _list = PostAddressBLL.Instance.GetPostAddressList(_pagesize, _pageindex, ref recordcount, _seachwhere);
            tobj.Total = recordcount;
            tobj.List = _list;
            //tobj.Listjson = JsonJC.ObjectToJson(_list); 
            return JsonJC.ObjectToJson(tobj);
        }

        /// <summary>
        /// 设置默认地址值
        /// </summary>
        /// <returns></returns>
        public int SetDefaultAddress()
        {
            int _addressid = FormString.IntSafeQ("addressid");
            if (_addressid != 0)
            {
                return PostAddressBLL.Instance.SetDefault(_addressid, memid);
            }

            return 0;
        }

        public int DelAddress()
        {
            int _addressid = FormString.IntSafeQ("addressid");
            if (_addressid != 0)
            {
                return PostAddressBLL.Instance.DeletePostAddressByKey(_addressid, memid);
            }

            return 0;
        }

        #endregion

    }
}
