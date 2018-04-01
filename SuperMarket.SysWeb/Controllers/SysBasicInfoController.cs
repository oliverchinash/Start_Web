using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.Core;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Model;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SuperMarket.SysWeb.Controllers
{
    public class SysBasicInfoController : BaseMemAdminController
    {

        public ActionResult Index()
        {
            return View();
        }
        #region 品牌
        public ActionResult BrandList()
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
        public ActionResult BrandEdit()
        {
            int _id = QueryString.IntSafeQ("id", 0);
            BrandEntity entity = BrandBLL.Instance.GetBrand(_id, false);
            ViewBag.entity = entity;
            return View();
        }

        /// <summary>
        /// 品牌信息修改
        /// </summary>
        /// <returns></returns>
        public string BrandEditSubmit()
        {
            ResultObj _obj = new ResultObj();
            int _enid = FormString.IntSafeQ("enid"); 
            string _name = FormString.SafeQ("name"); 
            int _sort = FormString.IntSafeQ("sort");
            int _isactive = FormString.IntSafeQ("isactive");
            int _ishot = FormString.IntSafeQ("ishot");
            string _manu = FormString.SafeQ("manu");
            string _picurl = FormString.SafeQ("picurl");

            BrandEntity entity =new BrandEntity();
            if(_enid>0)
            {
                entity = BrandBLL.Instance.GetBrand(_enid);
            } 
            entity.Name = _name;  
            entity.Sort = _sort;
            entity.IsActive = _isactive;
            entity.IsHot = _ishot;
            entity.Manufacturer = _manu;
            entity.PicUrl = _picurl;
            if (_enid > 0)
            {
                int _result = BrandBLL.Instance.UpdateBrand(entity);
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
                entity.Id = BrandBLL.Instance.AddBrand(entity);
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
            return JsonJC.ObjectToJson(_obj);
        }

        /// <summary>
        /// 品牌信息删除
        /// </summary>
        /// <returns></returns>
        public string BrandDeleteSubmit()
        {
            ResultObj _obj = new ResultObj();
            int _id = FormString.IntSafeQ("id");
            int _result = BrandBLL.Instance.DeleteBrandByKey(_id);
            _result = ClassBrandBLL.Instance.DeleteClassBrandByBrandId(_id);
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
        #endregion

        #region 分类
        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassList()
        {

            int _level = QueryString.IntSafeQ("level", 1);
            int _isactive = QueryString.IntSafeQ("isactive", -1);
            int _parentid = QueryString.IntSafeQ("parentid", -1);
            int _classtype = QueryString.IntSafeQ("classtype", -1);
            ViewBag.ClassType = _classtype;
            string _name = QueryString.SafeQ("name");

            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeClass;
            int _recordCount = 0;

            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = "Level", CompareValue = _level.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "IsActive", CompareValue = _isactive.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "ParentId", CompareValue = _parentid.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "Name", CompareValue = _name.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "ClassType", CompareValue = _classtype.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "ClassMenuType", CompareValue = ((int)ClassMenuTypeEnum.Default).ToString() });
            IList<ClassesFoundEntity> entitylist = ClassesFoundBLL.Instance.GetClassesFoundList(_pagesize, _pageindex, ref _recordCount, wherelist);
            string _url = "/SysBasicInfo/ClassList?level=" + _level + "&parentid=" + _parentid + "&name" + _name + "&isactive=" + _isactive + "&_classtype=" + _classtype;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _PageStr;
            ViewBag.entitylist = entitylist;
            return View();
        }


        /// <summary>
        /// 分类编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassEdit()
        { 
            int _pid = QueryString.IntSafeQ("pid");
            int _id = QueryString.IntSafeQ("id");
            ClassesFoundEntity entity = new ClassesFoundEntity();
            ClassesFoundEntity pentity = new ClassesFoundEntity();
            if (_id > 0)
            {
                entity= ClassesFoundBLL.Instance.GetClassesFound(_id, false);
            }
            if (entity.ParentId > 0)
            {
                pentity = ClassesFoundBLL.Instance.GetClassesFound(entity.ParentId, false);
            } 
            else if(_pid>0)
            {
                pentity = ClassesFoundBLL.Instance.GetClassesFound(_pid, false); 
            }
            ViewBag.Entity = entity;
            ViewBag.ParentEntity = entity;
            return View();
        }

        #endregion

        #region 产品
        public ActionResult ProductList()
        { 
            int _styleId = QueryString.IntSafeQ("styleId");
            int pid1 = QueryString.IntSafeQ("pid1",-1); 
            int pid2 = QueryString.IntSafeQ("pid2",-1); 
            int pid3 = QueryString.IntSafeQ("pid3",-1);
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeList;
            int _recordCount = 0;
            string _productName = QueryString.SafeQ("key");
            int pid = -1;
            if (pid1 >= 0) { pid = pid1; }
            if (pid2 >= 0) { pid  = pid2; }
            if (pid3 >= 0) { pid  = pid3; }
            string classidstr = "";
            IList<int> classintlist = new List<int>(); 
            classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(pid);  
            foreach(int classid in classintlist)
            {
                classidstr += classid + ",";
            }
            classidstr.TrimEnd(',');
            IList<ProductEntity> entitylist = ProductBLL.Instance.GetProductList(_pagesize, _pageindex, ref _recordCount, _productName, classidstr, _styleId);
            ViewBag.entitylist = entitylist; 
            string _url = "/SysBasicInfo/ProductList?pid1=" + pid1+ "&pid2=" + pid2 + "&pid3=" + pid3 + "&key=" + _productName;
            string pagestr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = pagestr;
            ViewBag.StyleId = _styleId;
            ViewBag.ProductName = _productName; 
            return View(); 
        }
        #endregion
    }
}
