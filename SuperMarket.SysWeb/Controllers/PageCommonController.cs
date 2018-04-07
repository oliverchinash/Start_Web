using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.HelpDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class PageCommonController : Controller
    {
        public ActionResult SiteSelect()
        {
            return View();
        }
    }
}
