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
    public class CommonController : Controller
    {
        public string GetProvinceJson()
        {
            IList<GYProvinceEntity> list = new List<GYProvinceEntity>();
            list = GYProvinceBLL.Instance.GetListGYProvinceAll();
            var listfilter = list.Select(
                    p => new
                    {
                        Code = p.Id,
                        Name = p.Name
                    });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        public string GetCityJson()
        {
            string pcode = FormString.SafeQ("pcode");
            IList<GYCityEntity> list = new List<GYCityEntity>();
            list = GYCityBLL.Instance.GetGYCityAllByPid(pcode);
            var listfilter = list.Select(
                   p => new
                   {
                       Code = p.Code,
                       Name = p.Name
                   });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        /// <summary>
        /// 根据分类Id 和级别获取子分类
        /// </summary>
        /// <returns></returns>
        public string GetClassFoundLevel()
        {
            int _level = FormString.IntSafeQ("level");
            int _pid = FormString.IntSafeQ("pid");
            int siteid = FormString.IntSafeQ("siteid"); 
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassListByLevel(_pid, _level, siteid, -1);
            var listfilter = list.Select(
            p => new
            {
                Id = p.Id,
                IsEnd = p.IsEnd,
                Name = p.Name
            });
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }
        /// <summary>
        /// 根据分类Id  获取对应分类的品牌列表
        /// </summary>
        /// <returns></returns>
        public string GetBrandByClass()
        {
            int _classid = FormString.IntSafeQ("classid");
            //int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级 
            IList<BrandEntity> list = new List<BrandEntity>();
            list = ClassBrandBLL.Instance.GetBrandByClass(_classid, 0);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        /// <summary>
        /// 根据分类得到对应属性
        /// </summary>
        /// <returns></returns>
        public string GetPropertyBySiteId()
        {
            int _siteid = FormString.IntSafeQ("siteid");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<BasicSitePropertiesEntity> list = new List<BasicSitePropertiesEntity>();
            list = BasicSitePropertiesBLL.Instance.GetListByClassId(_siteid, _pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Name = p.Name,
                         IsSpec = p.IsSpec,
                         Sort = p.Sort
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        /// <summary>
        /// 根据分类得到对应属性
        /// </summary>
        /// <returns></returns>
        public string GetPropertyValueList()
        {
            int _propertyId = FormString.IntSafeQ("propertyId");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<ClassProDetailsEntity> list = new List<ClassProDetailsEntity>();
            list = ClassProDetailsBLL.Instance.GetListByPropertyId(_propertyId, _pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Code = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        /// <summary>
        /// 获取订单详情
        /// </summary>
        /// <returns></returns>
        public string GetContentCmsByStyleId()
        {
            int _styleId = FormString.IntSafeQ("styleId");
            ProductStyleExtEntity _entity = new ProductStyleExtEntity();
            _entity = ProductStyleExtBLL.Instance.GetStyleExtByStyleId(_styleId);
            string liststr = JsonJC.ObjectToJson(_entity);
            return liststr;
        }
        /// <summary>
        /// 根据分类得到对应规格
        /// </summary>
        /// <returns></returns>
        public string GetSpecByClass()
        {
            int _classid = FormString.IntSafeQ("classid");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<VWClassSpecEntity> list = new List<VWClassSpecEntity>();
            //list = ClassSpecBLL.Instance.GetListByClass(_classid, _pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.SpecId,
                         Name = p.Name,
                         IsEnd = p.IsEnd
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        /// <summary>
        /// 根据分类得到对应规格
        /// </summary>
        /// <returns></returns>
        public string GetSpecValueByStyle()
        {
            int _styleid = FormString.IntSafeQ("styleid");
            int _specid = FormString.IntSafeQ("specid");//上级品牌Id，0代表第一级
            IList<VWProductStyleSpecEntity> list = new List<VWProductStyleSpecEntity>();
            list = ProductStyleSpecBLL.Instance.GetListByStyle(_styleid, _specid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.SpecId,
                         Name = p.SpecDetailName
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        /// <summary>
        /// 获取车的品牌列表
        /// </summary>
        /// <returns></returns>
        public string GetCarTypeList()
        {
            int _pid = FormString.IntSafeQ("pid");
            IList<CarTypeEntity> list = new List<CarTypeEntity>();
            list = CarTypeBLL.Instance.GetListByParent(_pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Code = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        /// <summary>
        /// 获取款式图片
        /// </summary>
        /// <returns></returns>
        public string GetPicList()
        {
            int _styleid = FormString.IntSafeQ("styleid");//上级品牌Id，0代表第一级
            IList<ProductStylePicsEntity> list = new List<ProductStylePicsEntity>();
            list = ProductStylePicsBLL.Instance.GetListByStyleId(_styleid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         StyleId = p.StyleId,
                         PicUrl = p.PicUrl,
                         PicSuffix = p.PicSuffix,
                         HasCompress = p.HasCompress
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        /// <summary>
        /// 获取产品信息
        /// </summary>
        /// <returns></returns>
        public string GetProductDetails()
        {
            int _productid = FormString.IntSafeQ("productid");
            VWProductStyleEntity _entity = ProductStyleBLL.Instance.GetProduct(_productid);
            string liststr = JsonJC.ObjectToJson(_entity);
            return liststr;

        }

        ///// <summary>
        ///// 获取品牌列表
        ///// </summary>
        ///// <returns></returns>
        //public string GetBrandList()
        //{
        //    IList<BrandEntity> entitylist = BrandBLL.Instance.GetBrandAllSyn();
        //    var listfilter = entitylist.Select(s => new { Id = s.Id, Name = s.Name }).ToList();
        //    string liststr = JsonJC.ObjectToJson(listfilter);
        //    return liststr;
        //}


        /// <summary>
        /// 获取帮助表的列表
        /// </summary>
        /// <returns></returns>
        public string GetClassLevelList()
        {
            int _parentid = FormString.IntSafeQ("parentid", 0);
            IList<IssueClassEntity> entitylist = IssueClassBLL.Instance.GetIssueClassByParentId(_parentid);
            var listfilter = entitylist.Select(s => new { Id = s.Id, Name = s.ClassName }).ToList();
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

    }
}
