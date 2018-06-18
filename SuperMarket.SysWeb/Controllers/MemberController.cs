using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMarket.Model;
using SuperMarket.BLL;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Json;
using SuperMarket.Model.Common;
using SuperMarket.Model.Const;
using System.Security.Cryptography;
using SuperMarket.Core;
using SuperMarket.Model.Basic.VW.Member;
using System.Collections;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.Web.CommonControllers;

namespace SuperMarket.SysWeb.Controllers
{
    public class MemberController : BaseCommonController
    {
         public ActionResult Index()
        {
            return View();
        }
    }
}