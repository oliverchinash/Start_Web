using SuperMarket.BLL;
using SuperMarket.Core;
using SuperMarket.Core.BaiduMap;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.IO;
using System.Web;
using SuperMarket.Model.Basic.VW.Shopping;
using SuperMarket.Core.Linq;
using System.Linq;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.HelpDB; 
using SuperMarket.BLL.CGOrderDB; 
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.JcOrderInquiry;
using SuperMarket.Web.CommonControllers;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.WeiXin;

namespace SuperMarket.Web.Controllers
{
    public class MemberController : BaseMemberController
    {
        public ActionResult Home()
        {
            VWMemberEntity _memberentity = MemberInfoBLL.Instance.GetVWMemberInfoByMemId(memid);
            VWOrderMsgEntity _msgentity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            ViewBag.VWOrderMsg = _msgentity;
            ViewBag.Member = _memberentity;
            int FavoritNum = MemFavoritesBLL.Instance.GetMemFavoritesNum(memid);
            ViewBag.FavoritesNum = FavoritNum;
            int CouponsNum = MemCouponsBLL.Instance.GetCanUseCouponsNum(memid);
            ViewBag.CouponsNum = CouponsNum;
            ViewBag.PageMenu = "4";
            return View();
        }

        public ActionResult Favorites()
        {
            int _pagesize = CommonKey.PageSizeFavoritsMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;
            int active = 1;//是否有效
            IList<VWMemFavoritesEntity> list = MemFavoritesBLL.Instance.GetVWMemFavoritesList(_pageindex, _pagesize, ref _recordCount, memid, active);
            if (list != null && list.Count > 0)
            {
                foreach (VWMemFavoritesEntity entity in list)
                {
                    entity.ProductEntity.ActualPrice = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductEntity.TradePrice, entity.ProductEntity.Price, entity.ProductEntity.IsBP, entity.ProductEntity.DealerPrice);
                }
            }
            ViewBag.List = list;
            ViewBag.TotalNum = _recordCount;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }

        public ActionResult BrowseLog()
        {
            int _pagesize = CommonKey.PageSizeFavoritsMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;
            IList<VWMemBrowseLogEntity> list = MemBrowseLogBLL.Instance.GetVWMemBrowseLogList(_pageindex, _pagesize, ref _recordCount, memid);
            if (list != null && list.Count > 0)
            {
                foreach (VWMemBrowseLogEntity entity in list)
                {
                    entity.ProductEntity.ActualPrice = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductEntity.TradePrice, entity.ProductEntity.Price, entity.ProductEntity.IsBP, entity.ProductEntity.DealerPrice);
                }
            }
            ViewBag.List = list;
            ViewBag.TotalNum = _recordCount;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }


        /// <summary>
        /// 跟微信同
        /// </summary>
        /// <returns></returns>
        public ActionResult WeChatCode()
        {
            string code = QueryString.SafeQ("wechatcode");
            VWMemberEntity mem = new VWMemberEntity();
            mem = MemberBLL.Instance.GetVWMember(memid);
            if (!string.IsNullOrEmpty(code) && string.IsNullOrEmpty(mem.WeChat))
            {
                MemWeChatMsgEntity shortmsg = WeiXinJsSdk.Instance.GetWeChatShortInfo(code);
                if (!string.IsNullOrEmpty(shortmsg.OpenId) && !string.IsNullOrEmpty(shortmsg.UnionId))
                {
                    MemWeChatMsgEntity entity = new MemWeChatMsgEntity();
                    entity.AppId = WeiXinConfig.GetAppId();
                    entity.Status = (int)WeChatStatus.ShortInfo;
                    entity.UnionId = shortmsg.UnionId;
                    entity.OpenId = shortmsg.OpenId;
                    MemWeChatMsgBLL.Instance.WeChatLogin(entity);
                    MemberBLL.Instance.BindMemWeChat(memid, shortmsg.UnionId, mem.TimeStampTab);
                    mem = MemberBLL.Instance.GetVWMember(memid);
                }
            }
            ViewBag.VWMember = mem;

            return View();
        }

        public ActionResult PersonalMsg()
        {
            VWMemberEntity mem = MemberBLL.Instance.GetVWMember(memid);
            ViewBag.VWMember = mem;
            return View();
        }
        public ActionResult Address()
        {
            return View();
        }
        public ActionResult Promote()
        {
            string url = "http://www.baidu.com";
          ViewBag.CodeUrl=  System.Web.HttpUtility.UrlDecode(url);
            return View();
        }
        

        #region  个人信息页

        /// <summary>
        /// 个人订单信息页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            VWMemberEntity _memberentity = MemberInfoBLL.Instance.GetVWMemberInfoByMemId(memid);
            //AssetEntity _assetentity = AssetBLL.Instance.GetAssetByMemId(memid);
            //int _noreadmsg = SysMsgBLL.Instance.GetNoReadNumByMemId(memid);
            VWOrderMsgEntity _ordermsg = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            IList<VWOrderEntity> _orderlist = OrderBLL.Instance.GetOrdersByMemIdTop3(memid);


            ViewBag.Member = _memberentity;
            //ViewBag.Asset = _assetentity;
            //ViewBag.SysMsgNum = _noreadmsg;

            ViewBag.Order = _ordermsg;
            ViewBag.OrderList = _orderlist;
            return View();
        }

        /// <summary>
        /// 地址维护
        /// </summary>
        /// <returns></returns>
        public ActionResult ManageAddress()
        {
            ViewBag.MemId = memid;
            return View();
        }

        #endregion

        #region 个人订单
        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderList()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            int _status = QueryString.IntSafeQ("s", 0);
            int _term = QueryString.IntSafeQ("t", 0);


            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = _status });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = ((int)OrderStyleEnum.Normal).ToString() });

            IList<VWOrderEntity> _Orderlist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            VWOrderMsgEntity _msgentity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            ViewBag.SearchOrderStatus = _status;
            ViewBag.SearchOrderTerm = _term;

            string _url = "/Member/OrderList?s=" + _status + "&t=" + _term + "&k=" + _keyword;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.Orderlist = _Orderlist;
            ViewBag.VWOrderMsg = _msgentity;
            ViewBag.Status = _status;

            return View();
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <returns></returns>
        public ActionResult XuQiuList()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            int _status = QueryString.IntSafeQ("s", 0);
            int _term = QueryString.IntSafeQ("t", 0);
            int _vcode = QueryString.IntSafeQ("vcode", 0); 


            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = _status });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = (int)OrderStyleEnum.XuQiu });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.VisualOrderCode, CompareValue = _vcode  });

            IList<VWOrderEntity> _Orderlist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            VWOrderMsgEntity _msgentity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            ViewBag.SearchOrderStatus = _status;
            ViewBag.SearchOrderTerm = _term;

            string _url = "/Member/OrderList?s=" + _status + "&t=" + _term + "&k=" + _keyword;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.Orderlist = _Orderlist;
            ViewBag.VWOrderMsg = _msgentity;
            ViewBag.Status = _status;

            return View();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetailsCancel()
        {
            long _ordercode = QueryString.LongIntSafeQ("code");
            VWOrderEntity _entity = OrderBLL.Instance.GetOrderByMemId(_ordercode, memid);
            if (_entity != null && _entity.Id > 0)
            {
                if (_entity.Status == (int)OrderStatus.Cancel)
                {
                    ViewBag.OrderEntity = _entity;
                }
            }

            return View();
        }


        /// <summary>
        /// 获取普通订单
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetailsNormal()
        {
            long _ordercode = QueryString.LongIntSafeQ("code");
            VWOrderEntity _entity = OrderBLL.Instance.GetOrderByMemId(_ordercode, memid);
            if (_entity != null && _entity.Id > 0 && _entity.Status != (int)OrderStatus.Cancel)
            {
                WLPsOrderEntity _wlmsg = WLPsOrderBLL.Instance.GetWLPsOrderByCode(_ordercode);

                ViewBag.OrderEntity = _entity;
                ViewBag.OrderWLEntity = _wlmsg;
            }
            else if (_entity.Status == (int)OrderStatus.Cancel)
            {
                return RedirectToAction("OrderDetailsCancel", new { code = _ordercode });//new {}匿名类型
            }
            return View();
        }

        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderDetails()
        {
            long _ordercode = QueryString.LongIntSafeQ("ordercode");
            int _orderDetailId = QueryString.IntSafeQ("orderDetailId");
            VWOrderEntity _entity = OrderBLL.Instance.GetVWOrderByCode(_ordercode);
            OrderDetailEntity _detailEntity = OrderDetailBLL.Instance.GetOrderDetail(_orderDetailId);
            string _status = EnumShow.Instance.GetEnumDes((OrderStatus)_entity.Status);
            ViewBag.entity = _entity;
            ViewBag.detailEntity = _detailEntity;
            ViewBag.status = _status;
            return View();
        }

        /// <summary>                                                                                                                      
        /// 获取订单统计信息，订单数相关信息
        /// </summary>
        /// <returns></returns>
        public string GetOrderMsg()
        {
            VWOrderMsgEntity _mementity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            return JsonJC.ObjectToJson(_mementity);
        }

        /// <summary>
        /// 获取订单产品列表
        /// </summary>
        /// <returns></returns>
        public string GetOrderDetails()
        {
            long _ordercode = FormString.LongIntSafeQ("ordercode");
            IList<OrderDetailEntity> _listproduct = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, _ordercode);
            return JsonJC.ObjectToJson(_listproduct);
        }


        /// <summary>
        /// 销售单打印
        /// </summary>
        /// <returns></returns>
        public ActionResult SalesSlip()
        {
            long _orderCode = QueryString.LongIntSafeQ("orderCode");
            OrderEntity entity = OrderBLL.Instance.GetOrderByCode(_orderCode);
            IList<OrderDetailEntity> entityList = OrderDetailBLL.Instance.GetOrderDetailAllByOrder(memid, _orderCode , true);
            ViewBag.Store = StoreBLL.Instance.GetStoreByMemId(memid);
            ViewBag.OrderAddress = OrderAddressBLL.Instance.GetOrderAddressByOrderCode(_orderCode);
            ViewBag.EntityList = entityList;
            ViewBag.Entity = entity;
            return View();
        }


        #endregion

        #region 发票

        /// <summary>
        /// 我的发票
        /// </summary>
        /// <returns></returns>
        public ActionResult MyBill()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = (int)OrderStatus.Finished });

            IList<VWOrderEntity> _Orderlist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            string _url = "/Member/MyBill?k=" + _keyword;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.Orderlist = _Orderlist;

            return View();
        }

        /// <summary>
        /// 发票详情
        /// </summary>
        /// <returns></returns>
        public ActionResult MyBillDetail()
        {
            return View();
        }
        #endregion

        #region 个人信息

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult PersonalInfo()
        {
            VWMemberEntity _memberentity = MemberInfoBLL.Instance.GetVWMemberInfoByMemId(memid);
            ViewBag.Member = _memberentity;
            return View();
        }

        /// <summary>
        /// 个人资产
        /// </summary>
        /// <returns></returns>
        public ActionResult AssetBalance()
        {
            int _pageindex = QueryString.IntSafeQ("p");
            int _pagesize = CommonKey.PageSizeAssetLog;
            int recordtotal = 0;
            IList<ConditionUnit> _listwhere = new List<ConditionUnit>();
            _listwhere.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            IList<AssetChangeEntity> list = AssetChangeBLL.Instance.GetAssetChangeList(_pagesize, _pageindex, ref recordtotal, _listwhere);
            AssetEntity _asst = AssetBLL.Instance.GetAsset(memid);
            ViewBag.Asset = _asst;
            ViewBag.AssetChangeList = list;
            return View();
        }

        /// <summary>
        /// 显示头部
        /// </summary>
        /// <returns></returns>
        public ActionResult ShowHead()
        {
            return View();
        }

        #endregion


        #region 修改密码(失效的)
        /// <summary>
        /// 账号信息，密码修改
        /// </summary>
        /// <returns></returns>
        public string ModifyPassWord()
        {

            string _pwd = FormString.SafeQ("pwd");
            if (string.IsNullOrEmpty(_pwd))
            {
                MemPWDModLogEntity _modentity = new MemPWDModLogEntity();
                _modentity.MemId = memid;
                _modentity.IPAddress = Globals.GetIp();
                _modentity.ClientType = (int)ClientTypeEnum.PC;
                _modentity.PassWord = MemberBLL.Instance.GetMember(memid).PassWord;
                _modentity.NewPassWord = _pwd;
                _modentity.ModifyTime = DateTime.Now;
                if (MemPWDModLogBLL.Instance.AddMemPWDModLog(_modentity) == memid)
                {
                    return ((int)CommonStatus.Success).ToString();
                }
            }
            else
            {
                return ((int)CommonStatus.PassWordEmpty).ToString();
            }
            return ((int)CommonStatus.Fail).ToString();
        }

        #endregion


        #region 个人信息提交

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <returns></returns>
        public string PersonalInfoSubmit()
        {
            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                string _email = FormString.SafeQ("email");
                string _nickname = FormString.SafeQ("nickname");
                string _memname = FormString.SafeQ("memname");
                string _identityno = FormString.SafeQ("identityno");
                MemberInfoEntity _meminforntity = MemberInfoBLL.Instance.GetMemberInfoByMemId(memid);

                _meminforntity.MemId = memid;
                _meminforntity.Email = _email;
                _meminforntity.Nickname = _nickname;
                _meminforntity.MemName = _memname;
                _meminforntity.IdentityNo = _identityno;
                if (_meminforntity.Id > 0)
                {
                    if (MemberInfoBLL.Instance.UpdateMemberInfo(_meminforntity) > 0)
                    {
                        return ((int)CommonStatus.Success).ToString();
                    }
                }
                else
                {
                    if (MemberInfoBLL.Instance.AddMemberInfo(_meminforntity) > 0)
                    {
                        return ((int)CommonStatus.Success).ToString();
                    }

                }
            }
            return ((int)CommonStatus.Fail).ToString();

        }


        #endregion

        #region 收货地址
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

        #region  发票信息

        /// <summary>
        /// 获取发票列表
        /// </summary>
        /// <returns></returns>
        public string GetBillComList()
        {
            int _pageindex = FormString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;
            int _pagesize = (int)CommonKey.PageSizeBill;
            int recordcount = 0;
            ListObj tobj = new ListObj();//定义实际需要的对象
            IList<MemBillVATEntity> _list = MemBillVATBLL.Instance.GetMemBillVATList(_pagesize, _pageindex, ref recordcount, memid,-1);
            tobj.Total = recordcount;
            tobj.List = _list;
            return JsonJC.ObjectToJson(tobj);
        }

        /// <summary>
        /// 设置默认地址值
        /// </summary>
        /// <returns></returns>
        public int SetBillComDefault()
        {
            int _billid = FormString.IntSafeQ("billid");
            if (_billid != 0)
            {
                return MemBillVATBLL.Instance.SetDefault(_billid, memid);
            }

            return 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public int DelBillCom()
        {
            int _billid = FormString.IntSafeQ("billid");
            if (_billid != 0)
            {
                return MemBillVATBLL.Instance.DisableMemBillVATById(_billid, memid);
            }

            return 0;
        }

        public int UpdateBillCom()
        {
            int _billid = FormString.IntSafeQ("billid");
            string _billname = FormString.SafeQ("title");
            if (_billid == 0)
            {
                MemBillVATEntity entity = MemBillVATBLL.Instance.GetMemBillVATByTitle(memid, _billid, _billname,(int)BillType.Normal);
                if (entity.Id == 0)
                {
                    return MemBillVATBLL.Instance.AddComName(_billid, _billname, memid);
                }
                else
                {
                    return entity.Id;
                }
            }
            else
            { 
                return MemBillVATBLL.Instance.UpdateComName(_billid, _billname, memid);
            }
        }

        public string GetBillVAT()
        {
            MemBillVATEntity _entity = MemBillVATBLL.Instance.GetMemBillVAT(memid);
            return JsonJC.ObjectToJson(_entity);
        }

        public int UpdateMemBillVATMsg()
        {
            int _billid = FormString.IntSafeQ("billid");
            string _name = FormString.SafeQ("name");
            string _code = FormString.SafeQ("code");
            string _address = FormString.SafeQ("address");
            string _phone = FormString.SafeQ("phone");
            string _bank = FormString.SafeQ("bank");
            string _account = FormString.SafeQ("account");

            MemBillVATEntity _emtity = new MemBillVATEntity();
            _emtity.Id = _billid;
            _emtity.CompanyName = _name;
            _emtity.CompanyCode = _code;
            _emtity.CompanyAddress = _address;
            _emtity.CompanyPhone = _phone;
            _emtity.CompanyBank = _bank;
            _emtity.BankAccount = _account;
            _emtity.MemId = memid;
            if (_billid > 0)
            {
                return MemBillVATBLL.Instance.UpdateMemBillVATMsg(_emtity);
            }
            else
            {
                return MemBillVATBLL.Instance.AddMemBillVATMsg(_emtity);
            }
        }

        public int UpdateMemBillVATReciever()
        {


            string _rename = FormString.SafeQ("rename");
            string _rephone = FormString.SafeQ("rephone");
            int _reprovince = FormString.IntSafeQ("reprovince");
            int _recity = FormString.IntSafeQ("recity");
            string _readdress = FormString.SafeQ("readdress", 200);
            string _companyName = FormString.SafeQ("companyName");
            string _companyCode = FormString.SafeQ("companyCode");
            string _companyAddress = FormString.SafeQ("companyAddress");
            string _companyPhone = FormString.SafeQ("companyPhone");
            string _companyBank = FormString.SafeQ("companyBank");
            string _bankAccount = FormString.SafeQ("bankAccount");


            MemBillVATEntity _emtity = MemBillVATBLL.Instance.GetMemBillVATByMemId(memid);
            _emtity.MemId = memid;
            _emtity.ReceiverAddress = _readdress;
            _emtity.ReceiverCity = _recity;
            _emtity.ReceiverProvince = _reprovince;
            _emtity.ReceiverPhone = _rephone;
            _emtity.ReceiverName = _rename;
            _emtity.BillType = (int)BillType.VAT;

            _emtity.CompanyName = _companyName;
            _emtity.CompanyCode = _companyCode;
            _emtity.CompanyAddress = _companyAddress;
            _emtity.CompanyPhone = _companyPhone;
            _emtity.CompanyBank = _companyBank;
            _emtity.BankAccount = _bankAccount;

            if (_emtity == null || _emtity.Id == 0)
            {
                return MemBillVATBLL.Instance.AddMemBillVAT(_emtity);
            }
            else if (_emtity.Status == 0)
            {
                return MemBillVATBLL.Instance.UpdateMemBillVAT(_emtity);
            }
            else
            {
                return _emtity.Id;
            }
        }

        public string UpdateBillEditSubmit()
        {
            ResultObj result = new ResultObj();
            int _billid = FormString.IntSafeQ("billid");
            int _billtype = FormString.IntSafeQ("billtype");  
            string _companyName = FormString.SafeQ("companyName");
            if(string.IsNullOrEmpty(_companyName))
            {
                result.Status = (int)CommonStatus.BillTitleIsEmpty;
            }
            string _companyCode = FormString.SafeQ("companyCode");
            string _companyAddress = FormString.SafeQ("companyAddress");
            string _companyPhone = FormString.SafeQ("companyPhone");
            string _companyBank = FormString.SafeQ("companyBank");
            string _bankAccount = FormString.SafeQ("bankAccount");
            int _isdefault = FormString.IntSafeQ("isdefault");
            MemBillVATEntity entity = MemBillVATBLL.Instance.GetMemBillVATByTitle(memid, _billid, _companyName,_billtype);
            if(_billtype==(int)BillType.Normal)
            { 
                if (entity.Id == 0)
                {
                    entity.BillType = _billtype;
                    entity.CompanyName = _companyName; 
                    entity.MemId = memid;
                    entity.Id = MemBillVATBLL.Instance.AddMemBillVAT(entity); 
                }
                else
                {
                    entity.BillType = _billtype;
                    entity.CompanyName = _companyName;
                    entity.MemId = memid;
                    entity.Id = MemBillVATBLL.Instance.UpdateMemBillVAT(entity); 
                }
            }
            else if (_billtype == (int)BillType.VAT && entity.Status==0)
            {
                entity.BillType = _billtype;
                entity.CompanyName = _companyName;
                entity.CompanyCode = _companyCode;
                entity.CompanyAddress = _companyAddress;
                entity.CompanyPhone = _companyPhone;
                entity.CompanyBank = _companyBank;
                entity.BankAccount = _bankAccount;
                entity.MemId = memid;
                if(entity.Id == 0)
                {
                    entity.Id = MemBillVATBLL.Instance.AddMemBillVAT(entity);
                }
                else
                {
                    entity.Id = MemBillVATBLL.Instance.UpdateMemBillVAT(entity);
                }
            }
            if(entity.Id>0&&_isdefault==1)//如果选择默认
            {
                  MemBillVATBLL.Instance.SetDefault(entity.Id, memid); 
            }
            result.Status = (int)CommonStatus.Success; 
            result.Obj = entity;
            return JsonJC.ObjectToJson(result);
        }
        #endregion


        #region 动态获取订单列表

        /// <summary>
        /// 根据查询条件动态获取订单列表
        /// </summary>
        /// <returns></returns>
        public string PageListChangeLoad()
        {
            int _pagesize = FormString.IntSafeQ("pagesize");
            int _pageindex = FormString.IntSafeQ("pageindex");
            int _memid = FormString.IntSafeQ("memid");
            int _recordCount = 0;
            IList<MemPostAddressEntity> _entitylist = PostAddressBLL.Instance.NewGetPostAddressList(_pagesize, _pageindex, ref _recordCount, _memid);
            return JsonJC.ObjectToJson(_entitylist);
        }

        #endregion

        #region 商家信息管理

        /// <summary>
        /// 商家信息管理
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreInfo()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            return View();
        }
        /// <summary>
        /// 商家申请
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreInfoApply()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            return View();
        }
        /// <summary>
        /// 商家信息管理 
        /// </summary>
        /// <returns></returns>
        public ActionResult StoreInfoManage()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            if (_meminforntity.Latitude > 0 && _meminforntity.Longitude > 0)
            {
                ViewBag.Latitude = _meminforntity.Latitude;
                ViewBag.Longitude = _meminforntity.Longitude;
            }
            else
            {
                string theIP = "114.97.195.112";// YMethod.GetHostAddress();
                string msg = MapCore.GetInfoByUrl(MapCore.GetMapIPUrl(theIP));
                msg = System.Text.RegularExpressions.Regex.Unescape(msg);
                BaiduApiStatus info = JsonConvert.DeserializeObject<BaiduApiStatus>(msg);
                if (info.status.Equals("0"))
                {
                    BaiduApiLocation baiduApiLocation = JsonConvert.DeserializeObject<BaiduApiLocation>(msg);
                    ViewBag.Longitude = baiduApiLocation.content.point.x;
                    ViewBag.Latitude = baiduApiLocation.content.point.y;
                }
                else if (info.status.Equals("1"))
                {
                    ViewBag.Latitude = "0";
                    ViewBag.Longitude = "0";
                }
            }
            return View();
        }
        #region 个人信息修改
        public ActionResult InfoMsgManage()
        {
            VWMemberEntity _mementity = MemberBLL.Instance.GetVWMember(memid);
            ViewBag.MemberEntity = _mementity;
            return View();
        }

        /// <summary>
        /// 个人信息修改
        /// </summary>
        /// <returns></returns>
        public ActionResult InfoMsgModify()
        {
            VWMemberEntity _mementity = MemberBLL.Instance.GetVWMember(memid);
            ViewBag.MemberEntity = _mementity;
            return View();
        }


        public string CompanyMsgUpdate()
        {
            ResultObj _obj = new ResultObj();
            string mobile = FormString.SafeQ("mobile");
            string mobilecode = FormString.SafeQ("mobilecode");
            string companyname = FormString.SafeQ("companyname");
            int province = FormString.IntSafeQ("province");
            int city = FormString.IntSafeQ("city");
            int comtype = FormString.IntSafeQ("comtype");
            string contractmanname = FormString.SafeQ("contractmanname");
            string email = FormString.SafeQ("email");
            string address = FormString.SafeQ("address", 300);
            ///开放验证码
            //if (System.Web.HttpContext.Current.Session[CommonKey.MobileNo] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileNo].ToString() != mobile || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() != mobilecode)
            //{
            //    _obj.Status = (int)CommonStatus.RegisterErrorVerCode;
            //}
            //else
            //{
            VWMemberEntity _mem = new VWMemberEntity();
            _mem.MemId = memid;
            _mem.ContactsMobile = mobile;
            _mem.CompanyName = companyname;
            _mem.CompanyProvinceId = province;
            _mem.CompanyCityId = city;
            _mem.CompanyAddress = address;
            _mem.ContactsManName = contractmanname;
            _mem.ContactsEmail = email;
            _mem.CompanyType = comtype;
            int rei = MemberBLL.Instance.StoreBasicMsgUpdate(_mem);
            if (rei > 0)
            {
                _obj.Status = (int)CommonStatus.Success;
            }
            else
            {
                _obj.Status = (int)CommonStatus.Fail;
            }

            return JsonJC.ObjectToJson(_obj);
        }

        public string MemberMsgUpdate()
        {
            ResultObj _obj = new ResultObj();
            string mobile = FormString.SafeQ("mobile");
            string mobilecode = FormString.SafeQ("mobilecode");
            string identityno = FormString.SafeQ("identityno");
            string memname = FormString.SafeQ("memname");
            string email = FormString.SafeQ("email");

            //if (System.Web.HttpContext.Current.Session[CommonKey.MobileNo] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] == null || System.Web.HttpContext.Current.Session[CommonKey.MobileNo].ToString() != mobile || System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() != mobilecode)
            //{
            //    _obj.Status = (int)CommonStatus.RegisterErrorVerCode;
            //}
            //else
            //{
            VWMemberEntity _mem = new VWMemberEntity();
            _mem.MemId = memid;
            _mem.ContactsMobile = mobile;
            _mem.IdentityNo = identityno;
            _mem.ContactsManName = memname;
            _mem.ContactsEmail = email;
            int rei = MemberBLL.Instance.MemberBasicMsgUpdate(_mem);
            if (rei > 0)
            {
                _obj.Status = (int)CommonStatus.Success;
            }
            else
            {
                _obj.Status = (int)CommonStatus.Fail;
            }

            //}
            return JsonJC.ObjectToJson(_obj);
        }

        public string UpdateNickName()
        {
            ResultObj _obj = new ResultObj();
            string nickname = FormString.SafeQ("nickname");

            VWMemberEntity _mem = new VWMemberEntity();
            _mem.MemId = memid;
            _mem.MemNikeName = nickname;
            int rei = MemberBLL.Instance.BasicMsgUpdate(_mem);
            if (rei > 0)
            {
                _obj.Status = (int)CommonStatus.Success;
            }
            else
            {
                _obj.Status = (int)CommonStatus.Fail;
            }


            return JsonJC.ObjectToJson(_obj);
        }
        #endregion
        /// <summary>
        /// 商家申请提交
        /// </summary>
        /// <returns></returns>
        public string StoreInfoApplySubmit()
        {

            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                string _companyname = FormString.SafeQ("companyname");
                string _licenseno = FormString.SafeQ("licenseno");
                string _licensepath = FormString.SafeQ("licensepath", 200);
                string _legalname = FormString.SafeQ("legalname");
                string _legalmobilephone = FormString.SafeQ("legalmobilephone");
                string _legalidentityno = FormString.SafeQ("legalidentityno");
                string _legalidentitypre = FormString.SafeQ("legalidentitypre", 200);
                string _legalidentitybeh = FormString.SafeQ("legalidentitybeh", 200);

                MemStoreEntity _supplierrntity = StoreBLL.Instance.GetStoreByMemId(memid);
                if (_supplierrntity.Id > 0)
                {
                    if (_supplierrntity.Status == (int)StoreStatus.Active)
                        return ((int)CommonStatus.Applyed).ToString();
                    if (_supplierrntity.Status == (int)StoreStatus.OnChecking)
                        return ((int)CommonStatus.Checking).ToString();
                }
                _supplierrntity.MemId = memid;
                _supplierrntity.CompanyName = _companyname;
                _supplierrntity.LicensePath = _licensepath;
                _supplierrntity.LicenseNo = _licenseno;
                _supplierrntity.LegalName = _legalname;
                _supplierrntity.LegalIdentityNo = _legalidentityno;
                _supplierrntity.LegalIdentityPre = _legalidentitypre;
                _supplierrntity.LegalIdentityBeh = _legalidentitybeh;
                _supplierrntity.LegalMobilePhone = _legalmobilephone;
                if (_supplierrntity.Id > 0)
                {
                    if (StoreBLL.Instance.UpdateStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }
                else
                {
                    _supplierrntity.CreateTime = DateTime.Now;
                    _supplierrntity.Status = (int)StoreStatus.WaitCheck;
                    if (StoreBLL.Instance.AddStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }

            }
            return ((int)CommonStatus.Fail).ToString();

        }
        /// <summary>
        /// 店铺信息维护
        /// </summary>
        /// <returns></returns>
        public string StoreInfoManageSubmit()
        {

            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                int _provincecode = FormString.IntSafeQ("provinceid");
                int _citycode = FormString.IntSafeQ("cityid");
                string _storename = FormString.SafeQ("storename", 200);
                string _mobilephone = FormString.SafeQ("mobilephone");
                string _address = FormString.SafeQ("address", 200);
                string _latitude = FormString.SafeQ("latitude");
                string _longitude = FormString.SafeQ("longitude");
                MemStoreEntity _supplierrntity = StoreBLL.Instance.GetStoreByMemId(memid);

                _supplierrntity.MemId = memid;
                _supplierrntity.ProvinceId = _provincecode;
                _supplierrntity.CityId = _citycode;
                _supplierrntity.StoreName = _storename;
                _supplierrntity.MobilePhone = _mobilephone;
                _supplierrntity.Address = _address;
                _supplierrntity.Latitude = StringUtils.GetDbDecimal(_latitude);
                _supplierrntity.Longitude = StringUtils.GetDbDecimal(_longitude);
                if (_supplierrntity.Id > 0)
                {
                    if (StoreBLL.Instance.UpdateStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }
            }
            return ((int)CommonStatus.Fail).ToString();

        }

        /// <summary>
        /// 证件图片上传
        /// </summary>
        /// <returns></returns>
        public string UploadFile()
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
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName,  ImagesSysPathCode.Certifacate.ToString(), memid.ToString(), _certype.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
                //return dicpathfull;
            }
            return "";
        }

        /// <summary>
        /// 退换货
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnOrExchangeGoods()
        {
            int _OrderDetailId = QueryString.IntSafeQ("OrderDetailId");
            OrderDetailEntity _entity = OrderDetailBLL.Instance.GetOrderDetail(_OrderDetailId);
            ViewBag.entity = _entity;
            return View();
        }

        /// <summary>
        /// 退货
        /// </summary>
        /// <returns></returns>
        public int ReturnGood()
        {
            int _productid = FormString.IntSafeQ("productid");
            Int64 _returnordercode = FormString.LongIntSafeQ("returnordercode");
            int _returnorderdetailid = FormString.IntSafeQ("returnorderdetailid");
            string _productname = FormString.SafeQ("productname");
            int _num = FormString.IntSafeQ("num");
            int _price = FormString.IntSafeQ("price");
            string _returnreason = FormString.SafeQ("returnreason");
            string _returndescription = FormString.SafeQ("returndescription");

            OrderDetailEntity _orderdetailentity = OrderDetailBLL.Instance.GetOrderDetail(_returnorderdetailid);
            _orderdetailentity.HasReturn = 1;
            _orderdetailentity.ReturnNum = _num;
            OrderDetailBLL.Instance.UpdateOrderDetail(_orderdetailentity);

            ReturnXSEntity _entity = new ReturnXSEntity();
            _entity.ProductId = _productid;
            _entity.ReturnOrderCode = _returnordercode;
            _entity.ReturnOrderDetailId = _returnorderdetailid;
            _entity.ProductName = _productname;
            _entity.Num = _num;
            _entity.Price = _price;
            _entity.ReturnReason = _returnreason;
            _entity.ReturnDescription = _returndescription;
            _entity.CreateTime = DateTime.Now;
            _entity.Status = 0;
            _entity.MemId = memid;
            _entity.ReturnType = 1;

            return ReturnXSBLL.Instance.AddReturnXS(_entity);

        }

        /// <summary>
        /// 换货
        /// </summary>
        /// <returns></returns>
        public int ExchangeGood()
        {
            int _productid = FormString.IntSafeQ("productid");
            Int64 _returnordercode = FormString.LongIntSafeQ("returnordercode");
            int _returnorderdetailid = FormString.IntSafeQ("returnorderdetailid");
            string _productname = FormString.SafeQ("productname");
            int _num = FormString.IntSafeQ("num");
            int _price = FormString.IntSafeQ("price");
            string _rejectreason = FormString.SafeQ("rejectreason");
            string _returndescription = FormString.SafeQ("returndescription");
            string _newordercode = FormString.SafeQ("newordercode");

            ReturnXSEntity _entity = new ReturnXSEntity();
            _entity.ProductId = _productid;
            _entity.ReturnOrderCode = _returnordercode;
            _entity.ReturnOrderDetailId = _returnorderdetailid;
            _entity.ProductName = _productname;
            _entity.Num = _num;
            _entity.Price = _price;
            _entity.RejectReason = _rejectreason;
            _entity.ReturnDescription = _returndescription;
            _entity.NewOrderCode = _newordercode;
            _entity.CreateTime = DateTime.Now;
            _entity.Status = 0;
            _entity.MemId = memid;
            _entity.ReturnType = 2;

            return ReturnXSBLL.Instance.AddReturnXS(_entity);
        }

        #endregion

        #region  我的退换货

        /// <summary>
        /// 我的退换货
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnsApp()
        {
            //获取订单
            int _pagesize = CommonKey.PageSizeReturns;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            int _term = QueryString.IntSafeQ("t", 0);
            long _ordercode = QueryString.LongIntSafeQ("ordercode");

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderCode, CompareValue = _ordercode.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = (-1).ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.CanReturn, CompareValue = (1).ToString() });
            IList<VWOrderEntity> _entitylist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            //分页
            string _url = "/Member/ReturnsApp?k=" + _keyword + "&t=" + _term;
            string _pageStr = HTMLPage.SetReturnListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;

            ViewBag.entitylist = _entitylist;


            return View();
        }

        /// <summary>
        /// 退换货详情
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnDetail()
        {
            int _orderdetailid = QueryString.IntSafeQ("orderdetailid", 0);
            VWOrderDetailEntity orderdetailentity = OrderDetailBLL.Instance.GetVWOrderDetail(_orderdetailid, memid);
            ViewBag.OrderDetailEntity = orderdetailentity;
            return View();

        }

        /// <summary>
        /// 退换货信息提交
        /// </summary>
        /// <returns></returns>
        public string ReturnInfoSubmit()
        {
            ResultObj _obj = new ResultObj();
            _obj.Status = (int)CommonStatus.Fail;

            int _productid = FormString.IntSafeQ("productid");
            int _returnorderdetailid = FormString.IntSafeQ("returnorderdetailid");
            int _returntype = FormString.IntSafeQ("returntype");
            int _receiveType = FormString.IntSafeQ("receiveType");
            string _returnreason = FormString.SafeQ("returnreason");
            long _returnordercode = FormString.LongIntSafeQ("returnordercode");
            string _acceptname = FormString.SafeQ("acceptname");
            string _acceptphone = FormString.SafeQ("acceptphone");
            int _provinceid = FormString.IntSafeQ("provinceid");
            int _cityid = FormString.IntSafeQ("cityid");
            string _getaddress = FormString.SafeQ("getaddress");
            int _num = FormString.IntSafeQ("num");

            int _retult = ReturnXSBLL.Instance.ReturnGoods(memid, _returnordercode, _returnorderdetailid, _num, _returntype, _returnreason, _acceptname, _acceptphone, _provinceid, _cityid, _getaddress, _receiveType);
            if (_retult > 0)
                _obj.Status = (int)CommonStatus.Success;

            return JsonJC.ObjectToJson(_obj);

        }

        /// <summary>
        /// 退换货记录
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnRecords()
        {
            int _pagesize = CommonKey.PageSizeReturns;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");//关键字
            int _status = QueryString.IntSafeQ("s", 0);//状态 默认申请中
            int _term = QueryString.IntSafeQ("t", 0);//时间段 默认三个月内
            int _returntype = QueryString.IntSafeQ("r", 0);//退or换货 不默认 全部显示

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword.ToString() });//关键字
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ReturnOrderStatus, CompareValue = _status.ToString() });//状态
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ReturnOrderTerm, CompareValue = _term.ToString() });//时间段
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ReturnType, CompareValue = _returntype.ToString() });//退货or换货
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid.ToString() });//用户ID
            IList<ReturnXSEntity> _entitylist = ReturnXSBLL.Instance.GetReturnXSList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Member/ReturnRecords?k=" + _keyword + "&s=" + _status + "&t=" + _term + "&r=" + _returntype;
            string _pageStr = HTMLPage.SetReturnListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;

            ViewBag.entitylist = _entitylist;
            return View();
        }

        /// <summary>
        /// 退换货进展
        /// </summary>
        /// <returns></returns>
        public ActionResult ReturnProgress()
        {

            int _id = QueryString.IntSafeQ("returnid", 34);
            ReturnXSEntity entity = ReturnXSBLL.Instance.GetReturnXS(_id, memid);
            if (entity.Status == (int)ReturnOrderStatus.Reject)
            {
                Response.Write("<h3 style='text-align:left;width:100%'>" + entity.RejectReason + "</h1>");
                return null;
            }
            else
            {
                ViewBag.entity = entity;
                ViewBag.EntityList = CGReturnBLL.Instance.GetCGReturnById(_id);
                return View();
            }
             

        }

        #endregion

        #region 修改密码

        /// <summary>
        /// 修改密码第一步
        /// </summary>
        /// <returns></returns>
        public ActionResult MPWStepOne()
        {
            MemberEntity entity = MemberBLL.Instance.GetMember(memid);
            if (entity.IsStore == 1)
            {
                ViewBag.MobilePhone = StoreBLL.Instance.GetStoreByMemId(entity.Id).MobilePhone;
            }
            else  ViewBag.MobilePhone = entity.MobilePhone;
            return View();
        }

        /// <summary>
        /// 验证身份(手机 验证码)
        /// </summary>
        /// <returns></returns>
        public string VerifyIdentity()
        {
            ResultObj reult = new ResultObj();
            string _mobilephone = StringUtils.GetDbString(FormString.SafeQ("mobilephone"));
            string _checkcode = StringUtils.GetDbString(FormString.SafeQ("checkcode"));
  MemberEntity entity = MemberBLL.Instance.GetMember(memid);

            if (System.Web.HttpContext.Current.Session[CommonKey.MobileNo] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode] != null && System.Web.HttpContext.Current.Session[CommonKey.MobileNo].ToString() == _mobilephone && System.Web.HttpContext.Current.Session[CommonKey.MobileYZCode].ToString() == _checkcode && entity.MobilePhone == _mobilephone)
            {
                reult.Status = (int)CommonStatus.Success;
                reult.Obj = entity.TimeStampTab;
                return entity.TimeStampTab;
            }
            else
            {
                reult.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(reult);

        }

        /// <summary>
        /// 修改密码第二步
        /// </summary>
        /// <returns></returns>
        public ActionResult MPWStepTwo()
        {
            string _key = QueryString.SafeQ("k");
            MemberEntity entity = MemberBLL.Instance.GetMember(memid);
            if (entity.TimeStampTab == _key)
            {
                ViewBag.key = entity.TimeStampTab;
                return View();
            }
            else
            {
                return RedirectToAction("MPWStepOne");
            }

        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public string ModifyPassword2()
        {
            ResultObj result = new ResultObj();
            string _newpassword = StringUtils.GetDbString(FormString.SafeQ("newpassword"));
            string _timestramp =  FormString.SafeQ("keystramp") ;
            if (!string.IsNullOrEmpty(_newpassword))
            {
                MemberEntity entity = MemberBLL.Instance.GetMember(memid);
                if (_timestramp == entity.TimeStampTab)
                {
                    entity.PassWord = CryptMD5.Encrypt(_newpassword);
                    if (MemberBLL.Instance.UpdateMember(entity) > 0)
                    {
                        MemberEntity entity2 = MemberBLL.Instance.GetMember(memid);
                        result.Status =(int) CommonStatus.Success;
                        result.Obj = entity2.TimeStampTab;
                        return JsonJC.ObjectToJson(result);
                    }
                }
              
            }
            result.Status = (int)CommonStatus.Fail; 

            return JsonJC.ObjectToJson(result);
        }

        /// <summary>
        /// 修改密码第三步
        /// </summary>
        /// <returns></returns>
        public ActionResult MPWStepThree()
        {
            string _k = QueryString.SafeQ("k");
            MemberEntity entity = MemberBLL.Instance.GetMember(memid);
            if (_k == entity.TimeStampTab)
            {
                return View();
            }
            else
            {
                return RedirectToAction("MPWStepOne");
            }
        }

        ///// <summary>
        ///// 清除cookie and session
        ///// </summary>
        ///// <returns></returns>
        //public int ClearCookieAndSession()
        //{
        //    try
        //    {
        //        string[] cookiestr = { SysCookieName.DefaultCusName, SysCookieName.DefaultLoginId, SysCookieName.ComBineCart, SysCookieName.MemberCookieName };
        //        for (var i = 0; i < cookiestr.Length; i++)
        //        {
        //            HttpCookie cookie = System.Web.HttpContext.Current.Request.Cookies.Get(cookiestr[i]);
        //            if (cookie != null)
        //            {
        //                cookie.Expires = DateTime.Now.AddDays(-1);
        //                System.Web.HttpContext.Current.Response.Cookies.Add(cookie);
        //            }
        //        }

        //        System.Web.HttpContext.Current.Session.Clear();

        //        return 1;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }


        //}

        #endregion

        #region 增值税发票
        /// <summary>
        /// 增值税发票申请
        /// </summary>
        /// <returns></returns>
        public ActionResult VATInvoiceApplication()
        {
            MemBillVATEntity entity = MemBillVATBLL.Instance.GetMemBillVATByMemId(memid);
            if (entity != null && entity.BillType == (int)BillType.VAT)
            {
                return RedirectToAction("VATInvoiceModification");
            }
            return View();
        }


        /// <summary>
        /// 增值税发票修改
        /// </summary>
        /// <returns></returns>
        public ActionResult VATInvoiceModification()
        {
            MemBillVATEntity entity = MemBillVATBLL.Instance.GetMemBillVATByMemId(memid);
            ViewBag.entity = entity;
            return View();
        }


        /// <summary>
        /// 增值税发票信息
        /// </summary>
        /// <returns></returns>
        public int SubmitVATInfo()
        {
            int _isadd = FormString.IntSafeQ("isadd");//等于1添加 等于0修改

            string _companyname = FormString.SafeQ("companyname");
            string _companycode = FormString.SafeQ("companycode");
            string _companyaddress = FormString.SafeQ("companyaddress");
            string _companyphone = FormString.SafeQ("companyphone");
            string _companybank = FormString.SafeQ("companybank");
            string _bankaccount = FormString.SafeQ("bankaccount");
            string _receivername = string.Empty;
            string _receiverphone = string.Empty;
            int _pid = 0;
            int _cid = 0;
            string _address = string.Empty;

            if (_isadd == 0)
            {
                _receivername = FormString.SafeQ("receivername");
                _receiverphone = FormString.SafeQ("receiverphone");
                _pid = FormString.IntSafeQ("pid");
                _cid = FormString.IntSafeQ("cid");
                _address = FormString.SafeQ("address");
            }

            MemBillVATEntity entity = MemBillVATBLL.Instance.GetMemBillVATByMemId(memid);
            entity.MemId = memid;
            entity.CompanyName = _companyname;
            entity.CompanyCode = _companycode;
            entity.CompanyAddress = _companyaddress;
            entity.CompanyPhone = _companyphone;
            entity.CompanyBank = _companybank;
            entity.BankAccount = _bankaccount;
            entity.BillType = (int)BillType.VAT;
            entity.CheckTime = DateTime.Now;

            if (_isadd == 0)
            {
                entity.ReceiverName = _receivername;
                entity.ReceiverPhone = _receiverphone;
                entity.ReceiverProvince = _pid;
                entity.ReceiverCity = _cid;
                entity.ReceiverAddress = _address;
            }

            if (_isadd == 1)
            {
                return MemBillVATBLL.Instance.AddMemBillVAT(entity);
            }
            else if (_isadd == 0)
            {
                return MemBillVATBLL.Instance.UpdateMemBillVAT(entity);
            }
            else
            {
                return 0;
            }

        }


        /// <summary>
        /// 增值税发票信息删除
        /// </summary>
        /// <returns></returns>
        public int DeleteVATInfo()
        {
            int _id = FormString.IntSafeQ("id");
            return MemBillVATBLL.Instance.DeleteMemBillVATByKey(_id);
        }


        /// <summary>
        /// 购物指南
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingDirectory()
        {
            int _id = QueryString.IntSafeQ("id", 17);
            IssueContentEntity _entity = IssueContentBLL.Instance.GetIssueContent(_id);
            ViewBag.entity = _entity;
            return View();
        }

        #endregion

        #region 清除缓存
        public ActionResult ClearMemCache()
        {
            string _cachekey = QueryString.SafeQ("id");
            MemCache.RemoveAllCache();
            return View();
        }

        #endregion

        #region  我的资产
        public string GetAsset()
        {
            AssetEntity _entity = AssetBLL.Instance.GetAssetByMemId(memid);
            return JsonJC.ObjectToJson(_entity);
        }

        #endregion

        #region 头部图片
        /// <summary>
        /// 显示头部图片
        /// </summary>
        /// <returns></returns>
        public string SaveShowHeadPic()
        {
            ResultObj _loginentity = new ResultObj();
            string path = FormString.SafeQ("picpath", 500);
            if (memid > 0)
            {
                MemberEntity _mem = new MemberEntity();
                _mem = MemberBLL.Instance.GetMember(memid);
                if (_mem != null && _mem.Status == 1)
                {
                    if (MemberInfoBLL.Instance.UpdateShowHead(memid, path) > 0)

                        _loginentity.Status = (int)CommonStatus.Success;
                    else
                        _loginentity.Status = (int)CommonStatus.Fail;
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
        public string SaveMemberInfoMsg()
        {
            ResultObj _loginentity = new ResultObj();
            string path = FormString.SafeQ("picpath", 500);
            string nickname = FormString.SafeQ("nickname", 50);
            if (memid > 0)
            {
                MemberEntity _mem = new MemberEntity();
                _mem = MemberBLL.Instance.GetMember(memid);
                if (_mem != null && _mem.Status == 1)
                {
                    MemberInfoEntity meminfo = MemberInfoBLL.Instance.GetMemberInfoByMemId(memid);
                    if (!string.IsNullOrEmpty(nickname))
                    { 
                        meminfo.Nickname = nickname;
                    }
                    if(!string.IsNullOrEmpty(path))
                    {

                        meminfo.HeadPicUrl = path;
                    }
                    if (MemberInfoBLL.Instance.UpdateMemberInfo(meminfo) > 0)

                        _loginentity.Status = (int)CommonStatus.Success;
                    else
                        _loginentity.Status = (int)CommonStatus.Fail;
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

        #region 我的积分

        public string GetIntegral()
        {
            IntegralEntity _entity = IntegralBLL.Instance.GetIntegralByMemId(memid);
            return JsonJC.ObjectToJson(_entity);
        }

        public ActionResult Integral()
        {
            int _pagesize = CommonKey.PageSizeIntegralChange;
            int _pageindex = QueryString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;
            int recordCount = 0;
            IntegralEntity _integral = IntegralBLL.Instance.GetIntegralByMemId(memid);
            IList<IntegralChangeEntity> _changelist = IntegralChangeBLL.Instance.GetIntegralChangeList(_pagesize, _pageindex, ref recordCount, memid);
            ViewBag.ChangeList = _changelist;
            ViewBag.Integral = _integral;
            string _url = "/Member/Integral?1=1";
            string _pageStr = HTMLPage.SetOrderListPage(recordCount, _pagesize, _pageindex, _url);

            ViewBag.PageStr = _pageStr;
            return View();
        }

        #endregion

        #region 我的优惠券
        public string GetUseCoupons()
        {
            IList<MemCouponsEntity> _list = new List<MemCouponsEntity>();
            _list= MemCouponsBLL.Instance.GetCanUseMemCoupons(memid);
            return JsonJC.ObjectToJson(_list);
        }
        public string GetUseCouponsByMem()
        {
            IList<DicCouponsEntity> _list = new List<DicCouponsEntity>();
            _list = MemCouponsBLL.Instance.GetCouponsByMem(memid);
            return JsonJC.ObjectToJson(_list);
        }
        public string GetUseCoupon()
        {
            ResultObj result = new ResultObj();
            int couponid = FormString.IntSafeQ("couponid");
            MemCouponsEntity entity = MemCouponsBLL.Instance.GetByCouponIdAndMem(couponid,memid);
            if(entity!=null&& entity.Id>0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = entity;
            } 
            else
            { 
                result.Status = (int)CommonStatus.CouponsError;
            }
            return JsonJC.ObjectToJson(result);

            
        }
    public ActionResult Coupons()
        { 
            int _status = QueryString.IntSafeQ("s");
            int _pagesize = CommonKey.PageSizeIntegralChange;
            if (_status == 0) _status = 1;
            ViewBag.Status = _status;
            int _pageindex = QueryString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;
            int recordCount = 0;
            IList<MemCouponsEntity> _list = MemCouponsBLL.Instance.GetMemCouponsList(_pageindex, _pagesize,ref recordCount, _status,memid);
            ViewBag.CouponsList = _list; 
            //string _url = "/Member/Coupons?";
            //string _pageStr = HTMLPage.SetOrderListPage(recordCount, _pagesize, _pageindex, _url);

            //ViewBag.PageStr = _pageStr;
            return View(); 
        }
        #endregion

        #region 我的评价

        /// <summary>
        /// 评价晒单
        /// </summary>
        /// <returns></returns>
        public ActionResult BeEvaluated()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int recordCount = 0;


            string _keyword = QueryString.SafeQ("keyword");
            int _term = QueryString.IntSafeQ("t", -1);
            int _hascomment = QueryString.IntSafeQ("h", 0);

            int orderstyle = (int)OrderStyleEnum.Normal;//默认仅显示易店心自营
            if (SuperMarket.Core.ConfigCore.Instance.ConfigEntity.JiShiSong == 1)
            {
                orderstyle = -1;//显示所有的评价
            }
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.HasComment, CompareValue = _hascomment });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = orderstyle });

            IList<VWOrderDetailEntity> _vworderdetaillist = OrderDetailBLL.Instance.GetVWOrderDetailList(_pagesize, _pageindex, ref recordCount, _wherelist);
            ViewBag.VWOrderDetailList = _vworderdetaillist;

            string _url = "/Member/BeEvaluated/?memid=" + memid + "&keyword=" + _keyword + "&t" + _term + "&h=" + _hascomment;
            string _PageStr = HTMLPage.SetEvaluationListPage(recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _PageStr;

            return View();
        }


        /// <summary>
        /// 查看评价
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluateR()
        {
            int _odid = QueryString.IntSafeQ("odid");
            IList<CommentEntity> _list = CommentBLL.Instance.GetCommentByODId(_odid, memid);
            if (_list != null && _list.Count > 0)
            {
                int productid = _list[0].ProductId;
                VWOrderDetailEntity _detailentity = OrderDetailBLL.Instance.GetVWOrderDetail(_odid, memid);
                ViewBag.CommentList = _list;
                ViewBag.ProductEntity = _detailentity;
            }
            return View();
        }

        /// <summary>
        /// 等待评价的
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluateWait()
        {

            int _pagesize = CommonKey.PageSizeCommentShow;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int recordCount = 0;
            int orderstyle = (int)OrderStyleEnum.Normal;
            if (SuperMarket.Core.ConfigCore.Instance.ConfigEntity.JiShiSong == 1)
            {
                orderstyle = -1;
            }
                IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = (int)OrderStatus.Finished });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.HasComment, CompareValue = 0 });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = orderstyle });
            IList<VWOrderDetailEntity> _orderlist = OrderDetailBLL.Instance.GetVWOrderDetailList(_pagesize, _pageindex, ref recordCount, _wherelist);

            if (_orderlist != null && _orderlist.Count > 0)
            {
                IList<VWOrderDetailEntity> objcodelist = _orderlist.DistinctBy(p => p.OrderCode).ToList();
                ViewBag.OrderDetailList = _orderlist;
                ViewBag.OrderCodeList = objcodelist; 
            }
            ViewBag.WaitNum = recordCount;
            ViewBag.EvaluatedNum = OrderDetailBLL.Instance.GetWaitEvaluateNum(memid, 1, orderstyle);

            string _url = "/Member/EvaluateWait?";
            string _pageStr = HTMLPage.SetOrderListPage(recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            return View();
        }

        /// <summary>
        /// 已评价过的
        /// </summary>
        /// <returns></returns>
        public ActionResult Evaluated()
        {
            int _pagesize = CommonKey.PageSizeCommentShow;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int recordCount = 0;
            int orderstyle = (int)OrderStyleEnum.Normal;
            if (SuperMarket.Core.ConfigCore.Instance.ConfigEntity.JiShiSong == 1)
            {
                orderstyle = -1;
            }
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            IList<CommentEntity> _commentlist = CommentBLL.Instance.GetCommentList(_pagesize, _pageindex, ref recordCount, _wherelist);


            ViewBag.CommentList = _commentlist;

            ViewBag.WaitNum = OrderDetailBLL.Instance.GetWaitEvaluateNum(memid, 0, orderstyle);
            ViewBag.EvaluatedNum = recordCount;
            string _url = "/Member/Evaluated?";
            string _pageStr = HTMLPage.SetOrderListPage(recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            return View();
        }
        public ActionResult Evaluates()
        {
            int _orderdetailid = QueryString.IntSafeQ("odid");
            VWOrderDetailEntity _orderdetail = OrderDetailBLL.Instance.GetVWOrderDetail(_orderdetailid, memid);
            if (_orderdetail != null && _orderdetail.Id > 0)
            {
                WLPsOrderDetailEntity _ps = WLPsOrderDetailBLL.Instance.GetWLByDetailId(_orderdetailid);
                ViewBag.WLDetail = _ps;
            }
            ViewBag.OrderDetail = _orderdetail;
            return View();
        }

        public string EvaluateSubmit()
        {
            ResultObj _result = new ResultObj();
            int odid = FormString.IntSafeQ("odid");
            int PackStar = FormString.IntSafeQ("packstar");
            int ProductStar = FormString.IntSafeQ("productstar");
            int SeviceStar = FormString.IntSafeQ("sevicestar");
            int TrafficStar = FormString.IntSafeQ("trafficstar");
            string CommentContent = FormString.SafeQ("commentcontent", 500);
            string PicContent = FormString.SafeQ("piccontent", 5000);

            CommentEntity _entity = new CommentEntity();
            VWOrderDetailEntity _order = OrderDetailBLL.Instance.GetVWOrderDetail(odid, memid);
            if (_order != null && _order.Id > 0)
            {
                if (_order.Status != (int)OrderStatus.Finished)
                {
                    _result.Status = (int)CommonStatus.StatusNoEvaluate;
                    return JsonJC.ObjectToJson(_result);
                }
                else if (_order.HasComment == 1)
                {
                    _result.Status = (int)CommonStatus.HasEvaluated;
                    return JsonJC.ObjectToJson(_result);
                }
                _entity.OrderDetailId = odid;
                _entity.OrderCode = _order.OrderCode;
                _entity.ProductId = _order.ProductId;
                _entity.ProductDetailId = _order.ProductDetailId;
                _entity.ProductName = _order.ProductName;
                _entity.OrderDate = _order.CreateTime;
                _entity.PicUrl = _order.ProductPic;
                _entity.PicSuffix = _order.ProductPicSuffix;
                _entity.ProductStar = ProductStar;
                _entity.PackStar = PackStar;
                _entity.TrafficStar = TrafficStar;
                _entity.SeviceStar = SeviceStar;
                _entity.MemId = memid; 
                _entity.PicUrlContent = PicContent;
                _entity.MemLevelName = EnumShow.Instance.GetEnumDes((MemberGrade)member.MemGrade);
                _entity.CreateTime = DateTime.Now;
                _entity.Status = 0;
                _entity.CommentContent = CommentContent;
                int c_id = CommentBLL.Instance.CreateCommentProc(_entity);
                if (c_id > 0)
                {
                    OrderDetailBLL.Instance.CommentHasEvaluate(odid);
                    _result.Status = (int)CommonStatus.Success;
                    return JsonJC.ObjectToJson(_result);
                }
                else if (c_id == (int)CommonStatus.HasEvaluated)
                {
                    _result.Status = (int)CommonStatus.HasEvaluated;
                    return JsonJC.ObjectToJson(_result);
                }
            }
            _result.Status = (int)CommonStatus.Fail;
            return JsonJC.ObjectToJson(_result);

        }


        public string GetEvaluateWaitJson()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeCommentShowMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int recordCount = 0;
            int orderstyle = (int)OrderStyleEnum.Normal;
            if (SuperMarket.Core.ConfigCore.Instance.ConfigEntity.JiShiSong == 1)
            {
                orderstyle = -1;
            }
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = (int)OrderStatus.Finished });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.HasComment, CompareValue = 0 });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = orderstyle });
 
            IList<VWOrderCommentEntity> _orderlist = OrderDetailBLL.Instance.GetOrderCommentList(_pagesize, _pageindex, ref recordCount, _wherelist);
            result.Total = recordCount;
            result.List = _orderlist; 
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }

        public string GetEvaluatedJson()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeCommentShowMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int recordCount = 0;

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            IList<CommentEntity> _commentlist = CommentBLL.Instance.GetCommentList(_pagesize, _pageindex, ref recordCount, _wherelist);
            result.Total = recordCount;
            result.List = _commentlist;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public ActionResult EvaluateSuccess()
        {
            int _productid = QueryString.IntSafeQ("productid");
            ProductEntity entity = ProductBLL.Instance.GetProduct(_productid);
            ViewBag.Entity = entity;
            return View();
        }
        #endregion

        #region 我的收藏
        public string HasAddToFavorite()
        {
            ResultObj result = new ResultObj();
            int _productdetailid = FormString.IntSafeQ("pdid");
            result.Status= (int)CommonStatus.Success;
            result.Obj = MemFavoritesBLL.Instance.ExistNum(_productdetailid, memid);
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public string FavoriteAdd()
        {
            ResultObj result = new ResultObj();
            int _productdetailid = FormString.IntSafeQ("pdid");
            int _systemtype = FormString.IntSafeQ("sysid");
            MemFavoritesEntity entity = new MemFavoritesEntity(); 
            entity.MemId = memid;
            entity.ProductDetailId = _productdetailid;
            entity.SystemId = _systemtype; 
            entity.IsActive = 1;
            MemFavoritesEntity entityresult =   MemFavoritesBLL.Instance.AddMemFavorites(entity);
            if(entityresult!=null&& entityresult.Id>0)
            {
  result.Status= (int)CommonStatus.Success;
  result.Obj= entityresult;
            }
          else
            {
                result.Status = (int)CommonStatus.Fail; 
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        /// <summary>
        /// 删除收藏
        /// </summary>
        /// <returns></returns>
        public string FavoriteCancel()
        {
            ResultObj result = new ResultObj();
            string _productdetailid = FormString.SafeQ("pdids",8000);
            int _systemtype = FormString.IntSafeQ("sysid"); 
            int resultnum= MemFavoritesBLL.Instance.DisableMemFavoritesByIds(_productdetailid,memid, _systemtype);
            if (resultnum > 0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = resultnum;
            }
            else
            {
                result.Status = (int)CommonStatus.Fail;
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        /// <summary>
        /// 删除所有收藏夹
        /// </summary>
        /// <returns></returns>
        public string FavoriteCancelAll()
        {
            ResultObj result = new ResultObj(); 
            int resultnum = MemFavoritesBLL.Instance.DisableMemFavoritesAll(  memid );
            if (resultnum > 0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = resultnum;
            }
            else
            {
                result.Status = (int)CommonStatus.Fail;
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        
        public string GetFavoritesJson()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeFavoritsMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;
            int active = 1;//是否有效
            IList<VWMemFavoritesEntity> list = MemFavoritesBLL.Instance.GetVWMemFavoritesList(_pageindex, _pagesize, ref _recordCount, memid, active);
            if (list != null && list.Count > 0)
            {
                foreach (VWMemFavoritesEntity entity in list)
                {
                    entity.ProductEntity.ActualPrice = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductEntity.TradePrice, entity.ProductEntity.Price, entity.ProductEntity.IsBP, entity.ProductEntity.DealerPrice);
                }
            }
            result.Total = _recordCount;
            result.List = list;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public string GetBrowseLogJson()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeFavoritsMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0; 
            IList<VWMemBrowseLogEntity> list = MemBrowseLogBLL.Instance.GetVWMemBrowseLogList(_pageindex, _pagesize, ref _recordCount, memid);
            if (list != null && list.Count > 0)
            {
                foreach (VWMemBrowseLogEntity entity in list)
                {
                    entity.ProductEntity.ActualPrice = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductEntity.TradePrice, entity.ProductEntity.Price, entity.ProductEntity.IsBP, entity.ProductEntity.DealerPrice);
                }
            }
            result.Total = _recordCount;
            result.List = list;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }


        #endregion

        #region  我的建议
        public string AddSuggest()
        {
            ResultObj result = new ResultObj();
            SuggestRecordEntity entity = new SuggestRecordEntity();
            entity.MemId = memid;
            entity.KFService = FormString.SafeQ("kf");
            entity.ProductNum = FormString.SafeQ("pn");
            entity.ProductPrice = FormString.SafeQ("pp");
            entity.WLService = FormString.SafeQ("wl");
            entity.SuggestDetail = FormString.SafeQ("sd",500); 
            entity.Id= SuggestRecordBLL.Instance.AddSuggestRecord(entity);
            if(entity.Id>0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = entity;
            }
            else
            {
                result.Status = (int)CommonStatus.Fail; 
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        #endregion

        #region  微信绑定相关
        public string DelWeChatBind()
        {
            ResultObj result = new ResultObj();
            int updaterowi = MemberBLL.Instance.BindMemWeChatRelease(memid);
            if(updaterowi>0)
            {
                result.Status = (int)CommonStatus.Success; 
            }
            else
            { 
                result.Status = (int)CommonStatus.Fail;
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public string WeChatBind()
        {
            ResultObj result = new ResultObj(); 
            string redirecturl = ConfigCore.Instance.ConfigCommonEntity.MainMobileWebUrl + "/Member/WeChatCode";
            //int navid = WeChatNavigationBLL.Instance.GetIdByUrl(redirecturl);
           //string url = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, WeiXinConfig.GetAppId(), SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.WeChatWebUrl, navid);
            string url = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(redirecturl), "0");
            result.Status = (int)CommonStatus.Success;
            result.Obj = url;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        #endregion

        #region 询价单相关
      public ActionResult OrderInquiryList()
        {
            int _status = QueryString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeOrderInquiry;
            int pageindex = QueryString.IntSafeQ("px");
            if (pageindex == 0) pageindex = 1;
            string key = QueryString.SafeQ("k");
            int record = 0;
            IList<VWInquiryOrderEntity> list = InquiryOrderBLL.Instance.GetInquiryOrderList(pagesize, pageindex, ref record, memid, _status, -1, key);
            IList<VWInquiryOrderEntity> listreturn = new List<VWInquiryOrderEntity>();

            if (list != null && list.Count > 0)
            {
                foreach (VWInquiryOrderEntity entity in list)
                {
                    VWInquiryOrderEntity entity2 = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(entity.InquiryOrderCode);
                    listreturn.Add(entity2);
                }
            }
            ViewBag.VWInquiryOrderList = listreturn;
            ViewBag.Status = _status;
            ViewBag.KeyWord = key;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageNum = maxpage;
            return View();
        }
        public ActionResult OrderInquiryDetail()
        {
            string _code = QueryString.SafeQ("code");
            VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(_code);
            IList<InquiryProductSubEntity> prosub = vworder.OrderProductSubs;
            Dictionary<int, IList<InquiryProductSubEntity>> prosubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
            if (prosub != null && prosub.Count > 0)
            {
                foreach (InquiryProductSubEntity prosuben in prosub)
                {
                    if (!prosubdic.ContainsKey(prosuben.InquiryProductId))
                    {
                        prosubdic.Add(prosuben.InquiryProductId, new List<InquiryProductSubEntity>());
                    }
                    prosubdic[prosuben.InquiryProductId].Add(prosuben);
                }
            }
            CGMemQuotedEntity quote = CGMemQuotedBLL.Instance.GetCGMemHasCheckedByCode(_code);
            Dictionary<int, CGQuotedPriceEntity> pricedic = new Dictionary<int, CGQuotedPriceEntity>();
            if (quote != null && quote.CGMemId > 0)
            {

                IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(_code, quote.CGMemId, true, true, true);
                if (pricelist != null && pricelist.Count > 0)
                {
                    foreach (CGQuotedPriceEntity en in pricelist)
                    {
                        if (!pricedic.ContainsKey(en.InquiryProductSubId))
                        {
                            pricedic.Add(en.InquiryProductSubId, en);
                        }
                    }
                }
            }
            ViewBag.PriceListDic = pricedic;
            ViewBag.VWInquiryOrder = vworder;
            ViewBag.ProductSubDic = prosubdic;

            return View();
        }
        public ActionResult OrderInquiryPrint()
        {
            string _code = QueryString.SafeQ("code");
            VWInquiryOrderEntity vworder = InquiryOrderBLL.Instance.GetInquiryOrderDataByCode(_code);
            IList<InquiryProductSubEntity> prosub = vworder.OrderProductSubs;
            Dictionary<int, IList<InquiryProductSubEntity>> prosubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
            if (prosub != null && prosub.Count > 0)
            {
                foreach (InquiryProductSubEntity prosuben in prosub)
                {
                    if (!prosubdic.ContainsKey(prosuben.InquiryProductId))
                    {
                        prosubdic.Add(prosuben.InquiryProductId, new List<InquiryProductSubEntity>());
                    }
                    prosubdic[prosuben.InquiryProductId].Add(prosuben);
                }
            }
            CGMemQuotedEntity quote = CGMemQuotedBLL.Instance.GetCGMemHasCheckedByCode(_code);
            Dictionary<int, CGQuotedPriceEntity> pricedic = new Dictionary<int, CGQuotedPriceEntity>();
            if (quote != null && quote.CGMemId > 0)
            {

                IList<CGQuotedPriceEntity> pricelist = CGQuotedPriceBLL.Instance.GetCGQuotedPriceAll(_code, quote.CGMemId, true, true, true);
                if (pricelist != null && pricelist.Count > 0)
                {
                    foreach (CGQuotedPriceEntity en in pricelist)
                    {
                        if (!pricedic.ContainsKey(en.InquiryProductSubId))
                        {
                            pricedic.Add(en.InquiryProductSubId, en);
                        }
                    }
                }
            }
            ViewBag.PriceListDic = pricedic;
            ViewBag.VWInquiryOrder = vworder;
            ViewBag.ProductSubDic = prosubdic;

            return View();
        }

        #endregion

        #region 询价确认订单相关
        public ActionResult OrderConfirmList()
        {
            int _status = QueryString.IntSafeQ("s", -1);
            int pagesize = CommonKey.PageSizeOrderInquiry;
            int pageindex = QueryString.IntSafeQ("px");
            if (pageindex == 0) pageindex = 1;
            int cgmemid = QueryString.IntSafeQ("cgmemid",-1);
            string inquirycode = QueryString.SafeQ("inquirycode");
            string key = QueryString.SafeQ("k");
            int record = 0;
            IList<VWConfirmOrderEntity> list = ConfirmOrderBLL.Instance.GetConfirmOrderList(pagesize, pageindex, ref record, memid, cgmemid, _status,  key, inquirycode);
             if(list!=null&& list.Count>0)
            {
                foreach(VWConfirmOrderEntity order in list)
                {
                    order.OrderProducts = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(order.ConfirmOrderCode,-1,false);
                }
            }
            ViewBag.VWOrderList = list;
            ViewBag.Status = _status;
            ViewBag.KeyWord = key;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageNum = maxpage;
            return View();
        }
        public ActionResult OrderConfirmDetail()
        {
            string _code = QueryString.SafeQ("code");
            VWConfirmOrderEntity confirmorder = ConfirmOrderBLL.Instance.GetVWConfirmOrderByCode(_code);
            if (confirmorder != null && confirmorder.ConfirmOrderId > 0&& confirmorder.MemId==memid)
            {
                IList<ConfirmOrderProductEntity> productlist = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(_code,-1, false);
                confirmorder.OrderProducts = productlist;
                confirmorder.OrderPics = InquiryOrderPicsBLL.Instance.GetInquiryOrderPicsAllByCode(confirmorder.InquiryOrderCode);
                ViewBag.VWOrder = confirmorder;
            }
            else
            {
                RedirectToAction("OrderConfirmList");
            }
            return View();
        }
        public ActionResult OrderConfirmPrint()
        {
            string _code = QueryString.SafeQ("code");
            VWConfirmOrderEntity confirmorder = ConfirmOrderBLL.Instance.GetVWConfirmOrderByCode(_code);
            if (confirmorder != null && confirmorder.ConfirmOrderId > 0 && confirmorder.MemId == memid)
            {
                IList<ConfirmOrderProductEntity> productlist = ConfirmOrderProductBLL.Instance.GetConfirmProductAllByCode(_code, -1,false);
                confirmorder.OrderProducts = productlist;
                confirmorder.OrderPics = InquiryOrderPicsBLL.Instance.GetInquiryOrderPicsAllByCode(confirmorder.InquiryOrderCode);
                
                ViewBag.VWOrder = confirmorder;
            }
            else
            {
                RedirectToAction("OrderConfirmList");
            }
            return View();
        }

        #endregion
    }

}
