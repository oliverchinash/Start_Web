using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Web.CommonControllers;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class HomeController : BaseMemAdminController
    {
      
       
        public ActionResult Index()
        {
            return View();
        }
           
    }
}
