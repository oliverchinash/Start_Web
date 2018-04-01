using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Linq;
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
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class  MemberController : BaseMemberController
    { 
        #region 展示

        public ActionResult Home()
        {
            VWMemberEntity _memberentity = MemberInfoBLL.Instance.GetVWMemberInfoByMemId(memid);
            VWOrderMsgEntity _msgentity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid); 
            ViewBag.VWOrderMsg = _msgentity;
            ViewBag.Member = _memberentity;
            int FavoritNum=    MemFavoritesBLL.Instance.GetMemFavoritesNum(memid);
            ViewBag.FavoritesNum= FavoritNum;
            int CouponsNum = MemCouponsBLL.Instance.GetCanUseCouponsNum(memid);
            ViewBag.CouponsNum = CouponsNum;
            ViewBag.PageMenu = "4";
            return View();
        }
        public ActionResult Address()
        {
            return View();
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
            IList<MemCouponsEntity> _list = MemCouponsBLL.Instance.GetMemCouponsList(_pageindex, _pagesize, ref recordCount, _status, memid);
            ViewBag.CouponsList = _list; 
            return View();
        }
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult OrderList()
        {
            int _pagesize = CommonKey.PageSizeOrderMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            int _status = QueryString.IntSafeQ("s", 0);
            int _term = QueryString.IntSafeQ("t", 0);
            int orderstyle = QueryString.IntSafeQ("os", 0);
            if(orderstyle==0) orderstyle=(int) OrderStyleEnum.Normal;
            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = _status });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = orderstyle });

            IList<VWOrderEntity> _Orderlist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            VWOrderMsgEntity _msgentity = OrderMsgMemBLL.Instance.GetVWOrderMsgByMemId(memid);
            ViewBag.SearchOrderStatus = _status;
            ViewBag.SearchOrderTerm = _term; 
            string _url = "/MobileMember/OrderList?os="+ orderstyle + "&s=" + _status + "&t=" + _term + "&k=" + _keyword;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pageStr;
            ViewBag.Orderlist = _Orderlist;
            ViewBag.VWOrderMsg = _msgentity;
            ViewBag.Status = _status; 
            ViewBag.Key = _keyword;
            ViewBag.Term = _term;

            ViewBag.TotalNum = _recordCount;
            ViewBag.OrderStyle = orderstyle;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;

            return View();
        }
 
        public string GetOrderListJson()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeOrderMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            string _keyword = QueryString.SafeQ("k");
            int _status = QueryString.IntSafeQ("s", 0);
            int _term = QueryString.IntSafeQ("t", 0);
            int orderstyle = QueryString.IntSafeQ("os", 0);
            if (orderstyle == 0) orderstyle = (int)OrderStyleEnum.Normal;


            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();

            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _keyword });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStatus, CompareValue = _status });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderTerm, CompareValue = _term });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderStyle, CompareValue = orderstyle });


            IList<VWOrderEntity> _Orderlist = OrderBLL.Instance.GetVWOrderList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            result.Total = _recordCount;
            result.List = _Orderlist;

            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
           
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
            string _url = "/MobileMember/Integral?1=1";
            string _pageStr = HTMLPage.SetOrderListPage(recordCount, _pagesize, _pageindex, _url);

            ViewBag.PageStr = _pageStr; 
            return View();
        }
        public ActionResult IntegralDetails()
        {
            int _pagesize = CommonKey.PageSizeIntegralChange;
            int _pageindex = QueryString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;
            int recordCount = 0; 
            IList<IntegralChangeEntity> _changelist = IntegralChangeBLL.Instance.GetIntegralChangeList(_pagesize, _pageindex, ref recordCount, memid);
            ViewBag.ChangeList = _changelist;  
            return View();
        }
        public string GetIntegralDetails()
        {
            ListObj result = new ListObj();
            int _pagesize = CommonKey.PageSizeIntegralChange;
            int _pageindex = QueryString.IntSafeQ("pageindex");
            if (_pageindex == 0) _pageindex = 1;
            int recordCount = 0;
            IList<IntegralChangeEntity> _changelist = IntegralChangeBLL.Instance.GetIntegralChangeList(_pagesize, _pageindex, ref recordCount, memid);
            result.Total = recordCount;
            result.List = _changelist;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public ActionResult Favorites()
        {
            int _pagesize = CommonKey.PageSizeFavoritsMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;
            int active = 1;//是否有效
            IList<VWMemFavoritesEntity> list = MemFavoritesBLL.Instance.GetVWMemFavoritesList(_pageindex, _pagesize,  ref _recordCount, memid, active);
            if(list!=null&&list.Count>0)
            {
                foreach(VWMemFavoritesEntity entity in list)
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
            IList<VWMemBrowseLogEntity> list = MemBrowseLogBLL.Instance.GetVWMemBrowseLogList(_pageindex, _pagesize, ref _recordCount, memid );
            if (list != null && list.Count > 0)
            {
                foreach (VWMemBrowseLogEntity entity in list)
                {
                    entity.ProductEntity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, entity.ProductEntity.TradePrice, entity.ProductEntity.Price, entity.ProductEntity.IsBP, entity.ProductEntity.DealerPrice);
                }
            }
            ViewBag.List = list;
            ViewBag.TotalNum = _recordCount;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }

        public ActionResult Invoice()
        {
            return View();
        }
        #region 评价
        /// <summary>
        /// 待评价
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluateWait()
        {
            int _pagesize = CommonKey.PageSizeCommentShowMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;
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
            IList<VWOrderCommentEntity> _orderlist = OrderDetailBLL.Instance.GetOrderCommentList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            ViewBag.OrderCommentList = _orderlist;

            ViewBag.TotalNum = _recordCount;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }
        /// <summary>
        /// 已评价
        /// </summary>
        /// <returns></returns>
        public ActionResult Evaluated()
        {
            int _pagesize = CommonKey.PageSizeCommentShowMobile;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemId, CompareValue = memid });
            IList<CommentEntity> _commentlist = CommentBLL.Instance.GetCommentList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            ViewBag.CommentList = _commentlist; 
            ViewBag.TotalNum = _recordCount;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }
        /// <summary>
        /// 评价详情，只读
        /// </summary>
        /// <returns></returns>
        public ActionResult EvaluateRead()
        {
            int _odid = QueryString.IntSafeQ("odid");
            IList<CommentEntity> _list = CommentBLL.Instance.GetCommentByODId(_odid, memid);
            if (_list != null && _list.Count > 0)
            {
                CommentEntity centity = _list[0];
                int productid = centity.ProductId;
                VWOrderDetailEntity _detailentity = OrderDetailBLL.Instance.GetVWOrderDetail(_odid, memid);
                IList<CommentPicEntity> commentpics = CommentPicBLL.Instance.GetCommentPicAll(centity.Id);
                //ViewBag.CommentList = _list;
                ViewBag.CommentEntity = centity;
                ViewBag.ProductEntity = _detailentity;
                ViewBag.CommentPicList = commentpics;
            }
            return View();
        }
        /// <summary>
        /// 评价详情
        /// </summary>
        /// <returns></returns>
        public ActionResult Evaluate()
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

        #endregion
        #region 意见反馈
        public ActionResult Suggest()
        {
            return View();
        }
        #endregion
        #region 个人信息
        public ActionResult PersonalMsg()
        {
            VWMemberEntity mem = MemberBLL.Instance.GetVWMember(memid);
            ViewBag.VWMember = mem;
            return View();
        }

        #endregion
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


        #endregion

    }
}
