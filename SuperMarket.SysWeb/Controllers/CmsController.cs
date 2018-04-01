using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.BaiduMap;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Picture;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class CmsController : BaseMemAdminController
    {

        #region CMS模板内容绑定
        /// <summary>
        /// CMS模板内容绑定
        /// </summary>
        /// <returns></returns>
        public ActionResult CmsProductBind()
        {
            int _cmsid = QueryString.IntSafeQ("cmsid");
            CmsContentEntity _entity = new CmsContentEntity();
            IList<VWProductEntity> _cmsstyle = new List<VWProductEntity>();
            IList<CmsTempletEntity> _cmstempletlist = new List<CmsTempletEntity>();
            int stylenum = 0;
            if (_cmsid > 0)
            {
                _entity = CmsContentBLL.Instance.GetCmsContent(_cmsid);
                _cmstempletlist = CmsTempletBLL.Instance.GetCmsTempletListByType(_entity.CMSType);
                _cmsstyle = CmsTemProductBLL.Instance.GetCmsTemProductByCmsid(_cmsid);

                for (int i = 0; i < _cmsstyle.Count; i++)
                {
                    _cmsstyle[i] = ProductBLL.Instance.GetProVWByDetailId(_cmsstyle[i].ProductDetailId);
                }

                if (_cmsstyle != null && _cmsstyle.Count > 0) stylenum = _cmsstyle.Count;
            }
            ViewBag.ContentEntity = _entity;
            ViewBag.ContentStyles = _cmsstyle;
            ViewBag.CmsTemplets = _cmstempletlist;
            ViewBag.StyleNum = stylenum;
            return View();
        }

        /// <summary>
        /// 获取CMS模板标题
        /// </summary>
        /// <returns></returns>
        public string GetCmsTempletTitles()
        {
            int _cmstype = FormString.IntSafeQ("cmstype");
            IList<CmsTempletEntity> _entitylist = CmsTempletBLL.Instance.GetCmsTempletListByType(_cmstype);
            string liststr = JsonJC.ObjectToJson(_entitylist);
            return liststr;
        }

        /// <summary>
        /// 获取CMS模板
        /// </summary>
        /// <returns></returns>
        public string GetCmsTemplet()
        {
            int _cmstempletid = FormString.IntSafeQ("cmstempletid");
            CmsTempletEntity entity = CmsTempletBLL.Instance.GetCmsTemplet(_cmstempletid);
            string str = JsonJC.ObjectToJson(entity);
            return str;
        }

        /// <summary>
        /// CMS模板内容添加
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public string CmsComtentAdd()
        {

            string _str = FormString.SafeQ("str", 1000000);
            string title = FormString.SafeQ("title", 200);
            int cmstyleid = FormString.IntSafeQ("cmstyleid");
            int cmsid = FormString.IntSafeQ("cmsid",0);
            string cmscontent = FormString.SafeQNo("cmscontent", 1000000);
            int cmstempletid = FormString.IntSafeQ("cmstempletid");
            CmsContentEntity _cmsentity = CmsContentBLL.Instance.GetCmsContent(cmsid);

            _cmsentity.Id = cmsid;
            _cmsentity.CmsContent = cmscontent;
            _cmsentity.CMSType = cmstyleid;
            _cmsentity.CreateTime = DateTime.Now;
            _cmsentity.IsActive = 1;
            _cmsentity.Title = title;
            _cmsentity.CmsTempletId = cmstempletid;
            int cmsidresult = CmsContentBLL.Instance.AddCmsContent(_cmsentity);
            if (cmsidresult > 0)
            {
                if (!string.IsNullOrEmpty(_str))
                {
                    int cmsproducts = CmsTemProductBLL.Instance.AddCmsProc(_str, cmsidresult);

                }
                return ((int)CommonStatus.Success).ToString();
            }


            return ((int)CommonStatus.Fail).ToString();
        }


        #endregion

        #region CMS模板管理

        /// <summary>
        /// CMS模板添加
        /// </summary>
        /// <returns></returns>
        public ActionResult CmsAdd()
        {
            int _cmsid = QueryString.IntSafeQ("cmsid");
            CmsTempletEntity _entity = CmsTempletBLL.Instance.GetCmsTemplet(_cmsid);
            ViewBag.CmsEntity = _entity;
            return View();
        }

        /// <summary>
        /// CMS模板管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CmsManage()
        {
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeList;
            int recordcount = 0;

            string _searchkey = QueryString.SafeQ("k");
            int _cmstype = QueryString.IntSafeQ("t", 0);
            int _isactive = QueryString.IntSafeQ("s", 0);
            string _principle = QueryString.SafeQ("p");

            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SeachDefault, CompareValue = _searchkey });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.CmsType, CompareValue = _cmstype.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.IsActive, CompareValue = _isactive.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SortKeyWord, CompareValue = _principle });
            IList<CmsTempletEntity> _entitylist = CmsTempletBLL.Instance.GetCmsTempletList(_pagesize, _pageindex, ref recordcount, wherelist);

            ViewBag.EntityList = _entitylist;

            string url = "/Cms/CmsManage?k=" + _searchkey + "&t=" + _cmstype + "&s=" + _isactive;
            string pagelist = HTMLPage.SetProductListPage(recordcount, _pagesize, _pageindex, url);
            ViewBag.PageStr = pagelist;
            return View();
        }

        /// <summary>
        /// CMS模板信息添加
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public string CmsAddSubmit()
        {
            string _title = FormString.SafeQ("title");
            string _templetcontent = FormString.SafeQNo("templetcontent", 1000000);
            int _productnum = FormString.IntSafeQ("productnum");
            int _cmstype = FormString.IntSafeQ("cmstype");
            int _isactive = FormString.IntSafeQ("isactive");

            CmsTempletEntity _entity = new CmsTempletEntity();
            _entity.Title = _title;
            _entity.TempletContent = _templetcontent;
            _entity.ProductNum = _productnum;
            _entity.CMSType = _cmstype;
            _entity.CreateDate = DateTime.Now;
            _entity.IsActive = _isactive;

            int _count = CmsTempletBLL.Instance.AddCmsTemplet(_entity);

            if (_count > 0)
            {
                return ((int)CommonStatus.Success).ToString();
            }

            return ((int)CommonStatus.Fail).ToString();
        }

        /// <summary>
        /// CMS模板信息修改
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public string CmsUpdateSubmit()
        {
            int _cmsid = FormString.IntSafeQ("cmsid");
            string _title = FormString.SafeQ("title");
            string _templetcontent = FormString.SafeQNo("templetcontent", 1000000);
            int _productnum = FormString.IntSafeQ("productnum");
            int _cmstype = FormString.IntSafeQ("cmstype");
            int _isactive = FormString.IntSafeQ("isactive");

            CmsTempletEntity _entity = new CmsTempletEntity();
            _entity.Id = _cmsid;
            _entity.Title = _title;
            _entity.TempletContent = _templetcontent;
            _entity.ProductNum = _productnum;
            _entity.CMSType = _cmstype;
            _entity.CreateDate = DateTime.Now;
            _entity.IsActive = _isactive;

            int _count = CmsTempletBLL.Instance.UpdateCmsTemplet(_entity);

            if (_count > 0)
            {
                return ((int)CommonStatus.Success).ToString();
            }

            return ((int)CommonStatus.Fail).ToString();
        }


        /// <summary>
        /// 对CMS模板做失效处理
        /// </summary>
        /// <returns></returns>
        public int CmsTempletDisable()
        {
            int _templetId = FormString.IntSafeQ("templetId");
            CmsTempletEntity _entity = CmsTempletBLL.Instance.GetCmsTemplet(_templetId);
            _entity.IsActive = 0;
            return CmsTempletBLL.Instance.UpdateCmsTemplet(_entity);
        }

        /// <summary>
        /// 对CMS模板做生效处理
        /// </summary>
        /// <returns></returns>
        public int CmsTempletEnable()
        {
            int _templetId = FormString.IntSafeQ("templetId");
            CmsTempletEntity _entity = CmsTempletBLL.Instance.GetCmsTemplet(_templetId);
            _entity.IsActive = 1;
            return CmsTempletBLL.Instance.UpdateCmsTemplet(_entity);
        }

        #endregion

        #region CMS模板+内容管理

        /// <summary>
        /// CMS内容管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CMSContentManage()
        {
            int pageindex = QueryString.IntSafeQ("pageindex");
            if (pageindex == 0) pageindex = 1;
            int pagesize = CommonKey.PageSizeList;
            int recordcount = 0;

            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            int contentid = QueryString.IntSafeQ("cid");
            string contenttitle = QueryString.SafeQ("t");
            int ctype = QueryString.IntSafeQ("ct");
            int status = QueryString.IntSafeQ("s", 1);
            wherelist.Add(new ConditionUnit { FieldName = "CId", CompareValue = contentid.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "Title", CompareValue = contenttitle });
            wherelist.Add(new ConditionUnit { FieldName = "CType", CompareValue = ctype.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "Status", CompareValue = status.ToString() });
            IList<CmsContentEntity> list = CmsContentBLL.Instance.GetCmsContentList(pagesize, pageindex, ref recordcount, wherelist);
            ViewBag.ContentList = list;
            string url = "/Cms/CMSContentManage?cid=" + contentid + "&t=" + contenttitle + "&ct=" + ctype + "&s=" + status;
            string pagelist = HTMLPage.SetProductListPage(recordcount, pagesize, pageindex, url);
            ViewBag.PageStr = pagelist;
            return View();
        }

        #endregion


        /// <summary>
        /// 公告管理
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeManage()
        {
            int _pageIndex = QueryString.IntSafeQ("pageindex");
            int _systemtype = QueryString.IntSafeQ("st");
            string  _key = QueryString.SafeQ("k");
            int  _status = QueryString.IntSafeQ("s",-1);
            int _noticetype = QueryString.IntSafeQ("nt",-1);
         
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.B2BNoticePageSize;
            int _recordCount = 0; 
            if (_systemtype == 0) _systemtype = -1;
            IList<B2BNoticeEntity> _entityList = B2BNoticeBLL.Instance.GetB2BNoticeList(_pageSize, _pageIndex, ref _recordCount, _key, _status, _noticetype, _systemtype);
            ViewBag.EntityList = _entityList;
            string _url = "/Cms/NoticeManage?k="+ _key+"&st="+ _systemtype+"&s="+ _status + "&nt="+ _noticetype;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
 
            return View();
        }

        /// <summary>
        /// 公告编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult NoticeEdit()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            B2BNoticeEntity _entity = B2BNoticeBLL.Instance.GetB2BNotice(_id);
            ViewBag.Entity = _entity;
            return View();
        }



        [ValidateInput(false)]
        /// <summary>
        /// 公告提交
        /// </summary>
        /// <returns></returns>
        public int NoticeSubmit()
        {

            int _id = FormString.IntSafeQ("_id");
            string _title = FormString.SafeQ("_title");
            string _content = FormString.SafeQNo("_content", 1000000);
            int _sort = FormString.IntSafeQ("_sort");
            int _isactive = FormString.IntSafeQ("_isactive");
            int _systemtype = FormString.IntSafeQ("_systemtype");
            int _noticetype = FormString.IntSafeQ("_noticetype");
              
            B2BNoticeEntity _entity = B2BNoticeBLL.Instance.GetB2BNotice(_id);
            _entity.Id = _id;
            _entity.Title = _title;
            _entity.NoticeContent = _content;
            _entity.Sort = _sort;
            _entity.IsActive = _isactive;
            _entity.CreateTime = StringUtils.GetDbDateTime(FormString.SafeQ("_creatime"));
            _entity.ShowTime = StringUtils.GetDbDateTime(FormString.SafeQ("_showtime"));
            _entity.EndTime = StringUtils.GetDbDateTime(FormString.SafeQ("_endtime"));
            _entity.SystemType = _systemtype;
            _entity.NoticeType = _noticetype; 

            int _result = B2BNoticeBLL.Instance.AddB2BNotice(_entity);
            return _result;
        }
        public ActionResult WeiXinQNewsManage()
        {
            int _pageIndex = QueryString.IntSafeQ("pageindex");   
            string _key = QueryString.SafeQ("k");    
            int _nt = QueryString.IntSafeQ("nt");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.B2BNoticePageSize;
            int _recordCount = 0; 
            IList<WeiXinQNewsEntity> _entityList = WeiXinQNewsBLL.Instance.GetWeiXinQNewsList(_pageSize, _pageIndex, ref _recordCount, _nt, _key);
            ViewBag.EntityList = _entityList;
            string _url = "/Cms/WeiXinQNewsManage?k="+ _key+"&nt="+ _nt;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr; 
            return View();
        }



        /// <summary>
        /// 对CMS模板做失效处理
        /// </summary>
        /// <returns></returns>
        public int WeiXinQNewsPublish()
        {
            int _templetId = FormString.IntSafeQ("templetId"); 
            return WeiXinQNewsBLL.Instance.WeiXinQNewsPublish(_templetId);
        }
        public ActionResult WeiXinQNewsEdit()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            WeiXinQNewsEntity _entity = WeiXinQNewsBLL.Instance.GetWeiXinQNews(_id);
            ViewBag.Entity = _entity;
            return View();
        }

        [ValidateInput(false)]
        public int WeiXinQNewsSubmit()
        {

            int _id = FormString.IntSafeQ("_id");
            string _title = FormString.SafeQ("_title",200);
            string _content = FormString.SafeQNo("_content", 1000000);
            int _sort = FormString.IntSafeQ("_sort");
            int _isactive = FormString.IntSafeQ("_isactive");
            int _systemtype = FormString.IntSafeQ("_systemtype");
            int _noticetype = FormString.IntSafeQ("_noticetype");
            string _FromUrl = FormString.SafeQ("FromUrl",200); 
            WeiXinQNewsEntity _entity = WeiXinQNewsBLL.Instance.GetWeiXinQNews(_id);
            _entity.Id = _id;
            _entity.Title = _title;
            _entity.NewsContent = _content; 
            _entity.CreateTime = StringUtils.GetDbDateTime(FormString.SafeQ("_creatime"));  
            _entity.NewsType = _noticetype;
            _entity.FromUrl = _FromUrl; 
            int _result = WeiXinQNewsBLL.Instance.AddWeiXinQNews(_entity);
            return _result;
        }


        public ActionResult WeiXinUrlEdit()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            WeChatNavigationEntity _entity = WeChatNavigationBLL.Instance.GetWeChatNavigation(_id);
            ViewBag.Entity = _entity;
            return View();
        }

        public ActionResult WeiXinUrlManage()
        {
            int _pageIndex = QueryString.IntSafeQ("pageindex");
            string _key = QueryString.SafeQ("k"); 
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.B2BNoticePageSize;
            int _recordCount = 0;
            IList<WeChatNavigationEntity> _entityList = WeChatNavigationBLL.Instance.GetWeChatNavigationList(_pageSize, _pageIndex, ref _recordCount, _key);
            ViewBag.EntityList = _entityList;
            string _url = "/Cms/WeiXinUrlManage?k=" + _key  ;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            return View();
        }
        public int WeChatUrlAdd()
        { 
            string _redirecturl = StringUtils.SafeCodeSmall(FormString.SafeQNo("_url", 200));
            int _urltype = FormString.IntSafeQ("_urltype");
            string _remark = FormString.SafeQ("_remark", 200);

            WeChatNavigationEntity _entity = WeChatNavigationBLL.Instance.GetNavigationByUrl(_redirecturl);
            if (_entity == null || _entity.Id == 0)
            {
                _entity.RedirectUrl = _redirecturl; 
                _entity.Id = WeChatNavigationBLL.Instance.AddWeChatNavigation(_entity);
            }
            _entity.WeChatUrlType = _urltype;
            _entity.Remark = _remark;
            //_entity.WeChatUrl = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.WeChatWebUrl), _entity.Id);
           _entity.WeChatUrl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(_redirecturl), _entity.Id);

            int _result = WeChatNavigationBLL.Instance.UpdateWeChatNavigation(_entity);
            return _result;
        }

    }
}

