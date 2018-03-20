using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
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

namespace SuperMarket.Web.MemberControllers
{
    public class MobilePayController : BaseMemberController
    {

        #region 支付状态

        /// <summary>
        /// 支付成功
        /// </summary>
        /// <returns></returns>
        public ActionResult PaySuccess()
        {
            return View();
        }


        /// <summary>
        /// 支付失败
        /// </summary>
        /// <returns></returns>
        public ActionResult PayFail()
        {
            return View();
        }


        /// <summary>
        /// 支付出错
        /// </summary>
        /// <returns></returns>
        public ActionResult PayError()
        {
            return View();
        }

        #endregion

    }
}
