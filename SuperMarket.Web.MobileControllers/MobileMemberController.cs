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
    public class MobileMemberController : BaseMemberController
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
       

        #endregion

    }
}
