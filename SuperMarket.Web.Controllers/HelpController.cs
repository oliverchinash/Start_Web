using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.HelpDB;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class HelpController: BaseCommonController 
    {

        /// <summary>
        /// 帮助中心列表
        /// </summary>
        /// <returns></returns>
        public ActionResult HelpL()
        { 
            int _pageindex = QueryString.IntSafeQ("pageindex",1);
            int _pagesize = CommonKey.PageSizeHelp;
            int _recordCount = 0;

            int _classid = QueryString.IntSafeQ("classid");
            if(_classid==0)
            _classid = StringUtils.GetDbInt(RouteData.Values["classid"]);
            if (_classid == 0) _classid = 5;
            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName= SearchFieldName.ClassId,CompareValue=_classid.ToString()});
            wherelist.Add(new ConditionUnit { FieldName= SearchFieldName.SystemType,CompareValue= ((int)SystemType.B2B).ToString()});
            IList<IssueContentEntity> entitylist2 = IssueContentBLL.Instance.GetIssueContentList(_pagesize,_pageindex,ref _recordCount,wherelist);

            var _parentId = IssueClassBLL.Instance.GetIssueClass(_classid).ParentId;
            ViewBag.SecondLevelName = IssueClassBLL.Instance.GetIssueClass(_classid).ClassName;
            ViewBag.FirstLevelName = IssueClassBLL.Instance.GetIssueClass(_parentId).ClassName; 
            ViewBag.entitylist2 = entitylist2;
            return View();
        }

        /// <summary>
        /// 帮助中心详情
        /// </summary>
        /// <returns></returns>
        public ActionResult HelpC()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            if (_id == 0)
                _id = StringUtils.GetDbInt(RouteData.Values["id"]);

            IssueContentEntity entity = IssueContentBLL.Instance.GetIssueContent(_id);
            var _classid = entity.ClassId;
            var _parentId = IssueClassBLL.Instance.GetIssueClass(_classid).ParentId;
            ViewBag.SecondLevelName = IssueClassBLL.Instance.GetIssueClass(_classid).ClassName;
            ViewBag.FirstLevelName = IssueClassBLL.Instance.GetIssueClass(_parentId).ClassName;
            ViewBag.entity = entity; 
            return View();
        }
    }
}
