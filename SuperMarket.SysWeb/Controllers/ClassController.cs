using SuperMarket.BLL;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.Core;
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
    public class ClassController : BaseMemAdminController
    {
        /// <summary>
        /// 分类管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassManage()
        {
            
            int _level = QueryString.IntSafeQ("level",0);
            int _isactive = QueryString.IntSafeQ("isactive", -1);
            int _parentid = QueryString.IntSafeQ("parentid", -1);
            int _classtype = QueryString.IntSafeQ("classtype", -1);
            int _siteid = QueryString.IntSafeQ("siteid", (int)SiteIdEnum.BathRoom);
            ViewBag.ClassType = _classtype;
            ViewBag.SiteId = _siteid;
            ViewBag.ParentId = _parentid;
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
            wherelist.Add(new ConditionUnit { FieldName = "SiteId", CompareValue = _siteid.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = "ClassMenuType", CompareValue = ((int)ClassMenuTypeEnum.Default).ToString() });
            IList<ClassesFoundEntity> entitylist = ClassesFoundBLL.Instance.GetClassesFoundList(_pagesize, _pageindex, ref _recordCount, wherelist);
            string _url = "/Class/ClassManage?level=" + _level+ "&parentid=" + _parentid+ "&name"+_name+"&isactive="+_isactive+ "&_classtype="+ _classtype;
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
            string _op = QueryString.SafeQ("op");
            int _id = QueryString.IntSafeQ("id");
            int _parentid = QueryString.IntSafeQ("parentid");
            ClassesFoundEntity entity = new ClassesFoundEntity();

            if (_op == "add")
            {  
                entity.ParentId = _parentid;
            }
            else 
            {


                entity = ClassesFoundBLL.Instance.GetClassesFound(_id, false);
            }
            ViewBag.op = _op;
            ViewBag.entity = entity;
            return View();
        }


        /// <summary>
        /// 分类信息添加
        /// </summary>
        /// <returns></returns>
        public int ClassInfoAdd()
        { 
            int _pid = FormString.IntSafeQ("pid"); 
            int _adid = FormString.IntSafeQ("adid");
            int _isend = FormString.IntSafeQ("isend");
            int _isactive = FormString.IntSafeQ("isactive");
            int _sort = FormString.IntSafeQ("sort");
            int _hasproduct = FormString.IntSafeQ("hasproduct");
            int _classtype = FormString.IntSafeQ("classtype"); 
            int _hasproperty = FormString.IntSafeQ("hasproperty");
            int _haspropertyclassid = FormString.IntSafeQ("haspropertyclassid");
            string _code = FormString.SafeQ("code");
            string _name = FormString.SafeQ("name");
            string _fullname = FormString.SafeQ("fullname");
            string _pyshort = FormString.SafeQ("pyshort");
            string _pyfull = FormString.SafeQ("pyfull");

            ClassesFoundEntity entity = new ClassesFoundEntity();

            entity.AdId = _adid;
            entity.IsEnd = _isend;
            entity.IsActive = _isactive;
            entity.Sort = _sort;
            entity.HasProduct = _hasproduct;
            entity.ClassType = _classtype; 
            entity.HasProperties = _hasproperty;
            entity.PropertiesClassId = _haspropertyclassid;
            entity.Code = _code;
            entity.Name = _name;
            entity.FullName = _fullname;
            entity.PYShort = _pyshort;
            entity.PYFull = _pyfull;

            entity.UpdateTime = entity.CreateTime = DateTime.Now;

            if (_pid == 0)
            {
                entity.ClassLevel = 1;
                entity.ParentId = 0;
                entity.IsEnd = 0;
            }
            else if (_pid>0)
            {
                ClassesFoundEntity temp = ClassesFoundBLL.Instance.GetClassesFound(_pid, false);
                entity.ClassLevel = temp.ClassLevel + 1;
                entity.ParentId = temp.Id;
                //if (entity.ClassLevel == 4)
                //{
                //    entity.IsEnd = 1;
                //}
                //else
                //{
                //    entity.IsEnd = 0;
                //}
            }

            int _result = ClassesFoundBLL.Instance.AddClassesFound(entity);
            return _result;

        }


        /// <summary>
        /// 分类信息修改
        /// </summary>
        /// <returns></returns>
        public int ClassInfoUpdate()
        {

            int _id = FormString.IntSafeQ("id");

            int _adid = FormString.IntSafeQ("adid");
            int _ishot = FormString.IntSafeQ("ishot");
            int _isactive = FormString.IntSafeQ("isactive");
            int _sort = FormString.IntSafeQ("sort");
            int _hasproduct = FormString.IntSafeQ("hasproduct");
            int _productnum = FormString.IntSafeQ("productnum");
            int _hasproperty = FormString.IntSafeQ("hasproperty");
            int _haspropertyclassid = FormString.IntSafeQ("haspropertyclassid");
            string _code = FormString.SafeQ("code");
            string _name = FormString.SafeQ("name");
            int _classtype = FormString.IntSafeQ("classtype");
            string _fullname = FormString.SafeQ("fullname");
            string _pyshort = FormString.SafeQ("pyshort");
            string _pyfull = FormString.SafeQ("pyfull");

            ClassesFoundEntity entity = ClassesFoundBLL.Instance.GetClassesFound(_id, false);

            entity.AdId = _adid;
            entity.IsHot = _ishot;
            entity.IsActive = _isactive;
            entity.Sort = _sort;
            entity.HasProduct = _hasproduct;
            entity.ClassType = _productnum;
            entity.HasProperties = _hasproperty;
            entity.PropertiesClassId = _haspropertyclassid;
            entity.Code = _code;
            entity.Name = _name;
            entity.FullName = _fullname;
            entity.ClassType = _classtype;
            entity.PYShort = _pyshort;
            entity.PYFull = _pyfull;

            entity.UpdateTime = DateTime.Now;

            int _result = ClassesFoundBLL.Instance.UpdateClassesFound(entity);
            return _result;

        }

        /// <summary>
        /// 分类信息删除
        /// </summary>
        /// <returns></returns>
        public int ClassDelete()
        {
            int _id = FormString.IntSafeQ("id");
            ClassesFoundEntity entity = ClassesFoundBLL.Instance.GetClassesFound(_id,false);
            int _result = 0;

            if (entity.ClassLevel == 3)//叶子节点直接删除
            {
                _result = ClassesFoundBLL.Instance.DeleteClassesFoundByKey(_id);//删除三级节点
            }
            else if (entity.ClassLevel == 2)//非叶子节点级联删除
            {
                _result = ClassesFoundBLL.Instance.DeleteClassesFoundByKey(_id);//删除二级节点
                if (_result > 0)
                {
                    _result = ClassesFoundBLL.Instance.DeleteClassesFoundByParentId(_id);//删除三级节点
                }
            }
            else if (entity.ClassLevel == 1)//非叶子节点级联删除
            {
                _result = ClassesFoundBLL.Instance.DeleteClassesFoundByKey(_id);//删除一级节点
                if (_result > 0)
                {
                    IList<ClassesFoundEntity> entitylist = ClassesFoundBLL.Instance.GetClassesAllByPId(_id, false,-1 );
                    if (entitylist!=null&&entitylist.Count>0)
                    {
                        foreach (ClassesFoundEntity item in entitylist)
                        {
                            _result = ClassesFoundBLL.Instance.DeleteClassesFoundByKey(item.Id);//删除二级节点
                            _result = ClassesFoundBLL.Instance.DeleteClassesFoundByParentId(item.Id);//删除三级节点
                        }
                    }
                } 
            }
            return _result;
        }

        /// <summary>
        /// 失效
        /// </summary>
        public int ClassDisable()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = ClassesFoundBLL.Instance.DisableClassesFoundByIds(_id.ToString());
            return _result;
        }

        /// <summary>
        /// 生效
        /// </summary>
        public int ClassEnable()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = ClassesFoundBLL.Instance.EnableClassesFoundByIds(_id.ToString());
            return _result;
        }

        /// <summary>
        /// 分类品牌管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassBrandManage()
        {
            int _classid = QueryString.IntSafeQ("classid", 0);
            ClassesFoundEntity entity = ClassesFoundBLL.Instance.GetClassesFound(_classid, false);

            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeBrand;
            int _recordCount = 0;

            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = "ClassId", CompareValue = _classid.ToString() });

            IList<ClassBrandEntity> entitylist = ClassBrandBLL.Instance.GetClassBrandList(_pagesize, _pageindex, ref _recordCount, wherelist);

            ViewBag.entitylist = entitylist;
            string _url = "/Class/ClassBrandManage?classid=" + _classid;

            string PageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = PageStr;
            ViewBag.entity = entity;
            return View();
        }


        /// <summary>
        /// 分类品牌信息
        /// </summary>
        /// <returns></returns>
        public ActionResult ClassBrandInfo()
        {
            int _classid = QueryString.IntSafeQ("classid",0);
            int _id = QueryString.IntSafeQ("id",0);
            ClassBrandEntity entity = ClassBrandBLL.Instance.GetClassBrand(_id);
            string _BrandName = BrandBLL.Instance.GetBrand(entity.BrandId,false).Name;

            ViewBag.entity = entity;
            ViewBag.classid = _classid;
            ViewBag.BrandName = _BrandName;
            return View();
        }


        /// <summary>
        /// 分类品牌信息添加
        /// </summary>
        /// <returns></returns>
        public int ClassBrandInfoAdd()
        {
            int _classid = FormString.IntSafeQ("classid");
            int _brandid = FormString.IntSafeQ("brandid");
            int _sort = FormString.IntSafeQ("sort");

            ClassBrandEntity entity = new ClassBrandEntity();
            entity.ClassId = _classid;
            entity.BrandId = _brandid;
            entity.Sort = _sort;

            int _result = ClassBrandBLL.Instance.AddClassBrand(entity) ;
            return _result;

        }

        /// <summary>
        /// 分类品牌信息修改
        /// </summary>
        /// <returns></returns>
        public int ClassBrandInfoUpdate()
        {
            int _id = FormString.IntSafeQ("id");
            int _classid = FormString.IntSafeQ("classid");
            int _brandid = FormString.IntSafeQ("brandid");
            int _sort = FormString.IntSafeQ("sort");

            ClassBrandEntity entity = ClassBrandBLL.Instance.GetClassBrand(_id);
            entity.ClassId = _classid;
            entity.BrandId = _brandid;
            entity.Sort = _sort;

            int _result = ClassBrandBLL.Instance.UpdateClassBrand(entity);
            return _result;
        }


        /// <summary>
        /// 分类品牌信息删除
        /// </summary>
        /// <returns></returns>
        public int ClassBrandInfoDelete()
        {
            int _id = FormString.IntSafeQ("id");
            int _result = ClassBrandBLL.Instance.DeleteClassBrandByKey(_id);
            return _id;
        }

        public ActionResult PropertiseManage()
        {  
            int _classid = QueryString.IntSafeQ("classid", -1);    
            IList<ClassPropertiesEntity> entitylist = ClassPropertiesBLL.Instance.GetPropertiesByClassId(_classid, false);
            ViewBag.entitylist = entitylist;
            ViewBag.ClassId = _classid;
            return View();
        }
        public ActionResult PropertiseInfo()
        { 
            int _classid = QueryString.IntSafeQ("classid", -1);
            int _propertyid = QueryString.IntSafeQ("propertyid", -1);
            ClassPropertiesEntity entity = new ClassPropertiesEntity();
            if (_propertyid>0)
            {
                entity = ClassPropertiesBLL.Instance.GetClassProperties(_propertyid,false);
                ViewBag.ClassId = entity.ClassId; 
            }
            else
            {
                ViewBag.ClassId = _classid;
                ViewBag.ClassName = ClassesFoundBLL.Instance.GetClassesFound(_classid).Name;
            }
            ViewBag.entity = entity; 
            return View();
        }
        public int   ClassPropertiesEdit()
        {
            int _propid = FormString.IntSafeQ("propid");
            int _classid = FormString.IntSafeQ("classid");
            string _name = FormString.SafeQ("name");
            int _isactive = FormString.IntSafeQ("isactive");
            int _sort = FormString.IntSafeQ("sort");

            ClassPropertiesEntity entity = new ClassPropertiesEntity();
            if (_propid > 0)
            {
                entity = ClassPropertiesBLL.Instance.GetClassProperties(_propid, false);
            }
            
            entity.Name = _name;
            entity.Sort = _sort;
            entity.IsActive = _isactive;
            if(_propid==0)
            {
                entity.CanInput = 1;
                entity.Code = "";
                entity.ComPropertyId = 0;
                entity.IsSpec = 0;
                entity.ParentId = 0;
                entity.RootPropertyId = 0; 
            }
          
            int _result = ClassPropertiesBLL.Instance.AddClassProperties(entity);
            return _result;

        }

        public ActionResult PropertiseDetailManage()
        {
            int _propid = QueryString.IntSafeQ("propid", -1); 
            IList<ClassProDetailsEntity> entitylist = ClassProDetailsBLL.Instance.GetListByPropertyId(_propid, 0,false);
            ViewBag.entitylist = entitylist;
            ViewBag.PropertyId = _propid;
            return View();
        }
        public ActionResult PropertiseDetailInfo()
        { 
            int _propid = QueryString.IntSafeQ("propid", -1);
            int _pdid = QueryString.IntSafeQ("pdid", -1);
            ClassProDetailsEntity entity = new ClassProDetailsEntity();
            if (_pdid > 0)
            { 
                entity = ClassProDetailsBLL.Instance.GetClassProDetails(_pdid,false);
                ViewBag.PropertyId = entity.PropertyId;
                ClassPropertiesEntity prp= ClassPropertiesBLL.Instance.GetClassProperties(entity.PropertyId);
                ViewBag.PropertyName = prp.Name; 

            }
            else
            {
                ViewBag.PropertyId = _propid; 
            }
            ViewBag.entity = entity;
            return View();
        }
        public int ClassPropDetailEdit()
        {
            int _pdid = FormString.IntSafeQ("pdid");
            int _propid = FormString.IntSafeQ("propid");
            string _name = FormString.SafeQ("name");
            int _isactive = FormString.IntSafeQ("isactive");
            int _sort = FormString.IntSafeQ("sort");
            ClassProDetailsEntity entity = new ClassProDetailsEntity();
            if (_pdid > 0) entity = ClassProDetailsBLL.Instance.GetClassProDetails(_pdid);
             
            entity.PropertyId = _propid;
            entity.Name = _name;
            entity.Sort = _sort;
            entity.Active = _isactive; 
            if (_pdid == 0)
            {
                entity.Status = 1;
                entity.Code = "";
                entity.ParentId = 0;
                entity.PicUrl = "";
              
            } 
            int _result = ClassProDetailsBLL.Instance.AddClassProDetails(entity);
            return _result;

        }
    }
}