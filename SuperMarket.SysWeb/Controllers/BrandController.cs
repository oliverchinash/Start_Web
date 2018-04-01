using SuperMarket.BLL;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class BrandController : BaseMemAdminController
    {
        /// <summary>
        /// 品牌管理
        /// </summary>
        /// <returns></returns>
        public ActionResult BrandManage()
        {
            int _ishot = QueryString.IntSafeQ("ishot", -1);
            string _name = QueryString.SafeQ("name"); 
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeBrand;
            int _recordCount = 0; 
            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = "IsHot", CompareValue = _ishot.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "Name", CompareValue = _name.ToString() });

            IList<BrandEntity> entitylist = BrandBLL.Instance.GetBrandList(_pagesize, _pageindex, ref _recordCount, wherelist);

            ViewBag.entitylist = entitylist;
            string _url = "/Brand/BrandManage?ishot=" + _ishot + "&name" + _name;

            string PageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = PageStr;
            return View();
        }

        /// <summary>
        /// 品牌信息
        /// </summary>
        /// <returns></returns>
        public ActionResult BrandInfo()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            BrandEntity entity = BrandBLL.Instance.GetBrand(_id, false);
            ViewBag.entity = entity;
            return View();
        }


        /// <summary>
        /// 品牌信息添加
        /// </summary>
        /// <returns></returns>
        public int BrandInfoAdd()
        {
            string _code = FormString.SafeQ("code");
            string _name = FormString.SafeQ("name");
            string _title = FormString.SafeQ("title");
            string _pyshort = FormString.SafeQ("pyshort");
            string _pyfull = FormString.SafeQ("pyfull");
            int _sort = FormString.IntSafeQ("sort");
            int _isactive = FormString.IntSafeQ("isactive");
            int _ishot = FormString.IntSafeQ("ishot");
            string _manu = FormString.SafeQ("manu");

            BrandEntity entity = new BrandEntity();
            entity.Code = _code;
            entity.Name = _name;
            entity.Title = _title;
            entity.PYShort = _pyshort;
            entity.PYFull = _pyfull;
            entity.Sort = _sort;
            entity.IsActive = _isactive;
            entity.IsHot = _ishot;
            entity.Manufacturer = _manu;

            int _result = BrandBLL.Instance.AddBrand(entity);
            return _result;

        }

        /// <summary>
        /// 品牌信息修改
        /// </summary>
        /// <returns></returns>
        public int BrandInfoUpdate()
        {
            int _id = FormString.IntSafeQ("id");
            string _code = FormString.SafeQ("code");
            string _name = FormString.SafeQ("name");
            string _title = FormString.SafeQ("title");
            string _pyshort = FormString.SafeQ("pyshort");
            string _pyfull = FormString.SafeQ("pyfull");
            int _sort = FormString.IntSafeQ("sort");
            int _isactive = FormString.IntSafeQ("isactive");
            int _ishot = FormString.IntSafeQ("ishot");
            string _manu = FormString.SafeQ("manu");

            BrandEntity entity = BrandBLL.Instance.GetBrand(_id, false);
            entity.Code = _code;
            entity.Name = _name;
            entity.Title = _title;
            entity.PYShort = _pyshort;
            entity.PYFull = _pyfull;
            entity.Sort = _sort;
            entity.IsActive = _isactive;
            entity.IsHot = _ishot;
            entity.Manufacturer = _manu;

            int _result = BrandBLL.Instance.UpdateBrand(entity);
            return _result;
        }

        /// <summary>
        /// 品牌信息删除
        /// </summary>
        /// <returns></returns>
        public int BrandInfoDelete()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = BrandBLL.Instance.DeleteBrandByKey(_id);
            _result = ClassBrandBLL.Instance.DeleteClassBrandByBrandId(_id);
            return _result;
        }

        /// <summary>
        /// 获取品牌列表(模糊查询)
        /// </summary>
        /// <returns></returns>
        public string GetBrandListByBrand()
        {
            string _brand = FormString.SafeQ("brand");
            IList<BrandEntity> entitylist = BrandBLL.Instance.GetBrandListByBrand(_brand);
            var listfilter = entitylist.Select(s => new { Id = s.Id, Name = s.Name }).ToList();
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
    }
}