using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.HelpDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
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
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Admin.Controllers
{
    public class HelpController : BaseMemAdminController
    {
        /// <summary>
        /// 购物指南
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingDirectory()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            IssueContentEntity _entity = IssueContentBLL.Instance.GetIssueContent(_id);
            IssueClassEntity _classentity = IssueClassBLL.Instance.GetIssueClass(_entity.ClassId);
            ViewBag.entity = _entity;
            ViewBag.classentity = _classentity;
            return View();
        }

        [ValidateInput(false)]
        /// <summary>
        /// 购物指南添加
        /// </summary>
        /// <returns></returns>
        public int ShoppingDirectoryAdd()
        {
            string _issuetitle = FormString.SafeQ("issuetitle");
            string _issuecontent = FormString.SafeQNo("issuecontent", 1000000); 
            int _isactive = FormString.IntSafeQ("isactive");
            int _classid = FormString.IntSafeQ("classid");
            IssueContentEntity entity = new IssueContentEntity();
            entity.IssueTitle = _issuetitle;
            entity.IssueContent = _issuecontent;
            entity.IsActive = _isactive;
            entity.ClassId = _classid;

            return IssueContentBLL.Instance.AddIssueContent(entity);
        }

        [ValidateInput(false)]
        /// <summary>
        /// 购物指南修改
        /// </summary>
        /// <returns></returns>
        public int ShoppingDirectoryUpdate()
        {
            int _id = FormString.IntSafeQ("id");
            string _issuetitle = FormString.SafeQ("issuetitle");
            string _issuecontent = FormString.SafeQNo("issuecontent", 1000000);
            int _isactive = FormString.IntSafeQ("isactive");
            int _classid = FormString.IntSafeQ("classid");
            IssueContentEntity entity = new IssueContentEntity();
            entity.Id = _id; 
            entity.IssueTitle = _issuetitle;
            entity.IssueContent = _issuecontent;
            entity.IsActive = _isactive;
            entity.ClassId = _classid;

            return IssueContentBLL.Instance.UpdateIssueContent(entity);
        }

        /// <summary>
        /// 购物指南管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ShoppingDirectoryManage()
        {
            int _pagesize = CommonKey.PageShoppingDirectory;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            int _id = QueryString.IntSafeQ("id", 0);
            string _issuetitle = QueryString.SafeQ("issuetitle");
            int _isactive = QueryString.IntSafeQ("isactive",-1);
            int _systype = QueryString.IntSafeQ("systype",-1);

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.Id, CompareValue = _id.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.IssueTitle, CompareValue = _issuetitle });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.IsActive, CompareValue = _isactive.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SystemType, CompareValue = _systype.ToString() });

            IList<IssueContentEntity> _entitylist = IssueContentBLL.Instance.GetIssueContentList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Help/ShoppingDirectoryManage?id=" + _id + "&issuetitle=" + _issuetitle + "&isactive=" + _isactive+ "&systype="+ _systype;
            string _PageStr = HTMLPage.SetIssueContentPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.entitylist = _entitylist;
            ViewBag.PageStr = _PageStr;
            return View();
        }

        /// <summary>
        /// 删除购物指南
        /// </summary>
        /// <returns></returns>
        public int ShoppingDirectoryDelete()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = 0;
            if (_id > 0)
            {
                _result = IssueContentBLL.Instance.DeleteIssueContentByKey(_id);
            }
            return _id;
        }

    }
}