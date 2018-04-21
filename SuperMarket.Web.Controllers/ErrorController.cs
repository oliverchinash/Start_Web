using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
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
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class ErrorController : BaseCommonController
    {
        public ActionResult Noproduct()
        {
            return View();
        }
    }
}
