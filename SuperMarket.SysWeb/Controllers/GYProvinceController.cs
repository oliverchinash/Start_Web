using SuperMarket.BLL;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class GYProvinceController : Controller
    {
        public ActionResult GYProvinceList()
        {
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = 20;
            int _recordCount = 0;
            string name = QueryString.SafeQ("key" );
            IList<GYProvinceEntity> entitylist = GYProvinceBLL.Instance.GetGYProvinceList(_pagesize, _pageindex, ref _recordCount, name);

            ViewBag.entitylist = entitylist;
            string _url = "/GYProvince/GYProvinceList?key=" + name;

            string PageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = PageStr;
            return View();
        }
        public ActionResult GYProvinceEdit()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            GYProvinceEntity entity = GYProvinceBLL.Instance.GetGYProvince(_id );
            ViewBag.entity = entity;
            return View();
        }
        /// <summary>
        ///  信息修改
        /// </summary>
        /// <returns></returns>
        public string GYProvinceEditSubmit()
        {
            ResultObj _obj = new ResultObj();
            int enid = FormString.IntSafeQ("enid");
            string name = FormString.SafeQ("name");
            int sort = FormString.IntSafeQ("sort");
            int isactive = FormString.IntSafeQ("isactive"); 
            string fullname = FormString.SafeQ("fullname");

            GYProvinceEntity entity = new GYProvinceEntity();
            if (enid > 0)
            {
                entity = GYProvinceBLL.Instance.GetGYProvince(enid);
            }
            entity.Name = name;
            entity.Sort = sort;
            entity.IsActive =  isactive; 
            entity.FullName = fullname; 
            if (enid > 0)
            {
                int _result = GYProvinceBLL.Instance.UpdateGYProvince(entity);
                if (_result > 0)
                {
                    _obj.Status = (int)CommonStatus.Success;
                    _obj.Obj = entity;
                }
                else
                {
                    _obj.Status = (int)CommonStatus.Fail;
                }
            }
            else
            {
                entity.Id = GYProvinceBLL.Instance.AddGYProvince(entity);
                if (entity.Id > 0)
                {
                    _obj.Status = (int)CommonStatus.Success;
                    _obj.Obj = entity;
                }
                else
                {
                    _obj.Status = (int)CommonStatus.Fail;
                }
            }
            return  JsonJC.ObjectToJson(_obj);
        }

        public string GYProvinceDeleteSubmit()
        {
            ResultObj _obj = new ResultObj();
            int _id = FormString.IntSafeQ("id");
            int _result = GYProvinceBLL.Instance.DeleteGYProvinceByKey(_id); 
            if (_result > 0)
            {
                _obj.Status = (int)CommonStatus.Success;
                _obj.Obj = _id;
            }
            else
            {
                _obj.Status = (int)CommonStatus.Fail;
                _obj.Obj = _id;
            }
            return JsonJC.ObjectToJson(_obj);
        }
    }
}