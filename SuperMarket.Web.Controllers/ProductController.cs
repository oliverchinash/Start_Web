using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc; 
using SuperMarket.Model;
using SuperMarket.Web.Controllers;
using SuperMarket.BLL;
using SuperMarket.Core.Safe;
using SuperMarket.Model.Common;
using SuperMarket.Core.Util;
using SuperMarket.Core;
using SuperMarket.BLL.Common;
using SuperMarket.Model.Account;
using SuperMarket.BLL.Cookie;
using SuperMarket.Core.Linq;
using SuperMarket.Core.Json;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.ProductDB; 
using SuperMarket.Web.CommonControllers;

namespace SuperMarket.Web.Controllers
{
    public class ProductController : BaseCommonController
    {
        public ActionResult Detail()
        {
            string title = "易店心,";
            int _productdetailid = QueryString.IntSafeQ("pd");
            int _producttype = (int)ProductType.Normal;
            if ( _productdetailid == 0) {
                 _productdetailid = StringUtils.GetDbInt( RouteData.Values["pd"]);
            if(  _productdetailid == 0)
                return Redirect("/"); }
            VWProductEntity _vwentity = new VWProductEntity();
          
            _vwentity = ProductBLL.Instance.GetProVWByDetailId(_productdetailid);
            int  _productid = _vwentity.ProductId;
             
            _producttype = _vwentity.ProductType;

          //if(_vwentity.ProductType== (int)ProductType.SpecialPrice)
          //{
          //    return RedirectToAction(  "SProductDetail", new { pd = _productdetailid });
          //}
            title += _vwentity.AdTitle;
            ViewBag.Title = title;
            IList<ProductStylePicsEntity> productpiclist = null;

            ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(_vwentity.ClassId, false);
            BrandEntity _brandrntity = BrandBLL.Instance.GetBrand(_vwentity.BrandId, false);
            ViewBag.RavClassList = ListRavStr(_classentity);
            ViewBag.Brand = _brandrntity;
            productpiclist = ProductStylePicsBLL.Instance.GetListPicsByProductId(  _productid );
            _vwentity.ProductPics = productpiclist;

            IList<ProductPropertyEntity> productpropertylist = ProductPropertyBLL.Instance.GetListByProductId(_productid);
            _vwentity.ProductPropertys = productpropertylist;

            if (!string.IsNullOrEmpty(_vwentity.Spec1))
            {
                ViewBag.Spec1Name = _vwentity.Spec1;
                IList<VWProductEntity> plist = ProductBLL.Instance.GetListSpecsByStyleId(_vwentity.SiteId, _vwentity.ProductType, _vwentity.CGMemId);
                //ViewBag.SpecList = plist;
                var listfilter = (from c in plist
                                  select new { c.Spec1, c.Spec2, c.PicUrlLittle, c.ProductId,c.ProductDetailId });
                string liststr = JsonJC.ObjectToJson(listfilter);
                ViewBag.SpecListJson = liststr;
                IList<string> OBJSpec1 = (from c in plist
                                          where !string.IsNullOrEmpty(c.Spec1)
                                          select c.Spec1).Distinct().OrderBy(p => p).ToList();
                ViewBag.Spec1List = OBJSpec1;

                if (!string.IsNullOrEmpty(_vwentity.Spec2))
                {
                    ViewBag.Spec2Name = _vwentity.Spec2;
                    IList<string> OBJSpec2 = (from c in plist
                                              where !string.IsNullOrEmpty(c.Spec2)
                                              select c.Spec2).Distinct().OrderBy(p => p).ToList<string>();
                    ViewBag.Spec2List = OBJSpec2;
                }
            }

            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                ViewBag.Member = member;
                _vwentity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, _vwentity.TradePrice, _vwentity.Price, _vwentity.IsBP, _vwentity.DealerPrice);
            }
            ViewBag.VWProductEntity = _vwentity;
            return View();
        }

        /// <summary>
        /// 常规商品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            string title =   "易店心,";
            string _key = QueryString.SafeQ("key");
            int classid= QueryString.IntSafeQ("cid");
            int brandid= QueryString.IntSafeQ("bd");
            int order_i= QueryString.IntSafeQ("px");//排序类型
            //if (classid == 0) { return Redirect("/");  } 
            int siteid = QueryString.IntSafeQ("s");
            if (siteid<=0) siteid = (int)SiteIdEnum.BathRoom;
            ViewBag.SiteId = siteid;
            ///获取已选择的分类属性
            string propertiesstr = QueryString.SafeQ("pp");
            //if (classid == 0) RedirectToAction("Home", "Index");
            ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, false);
            if(_classentity!=null&& _classentity.Id>0)
            { 
                title += "类别：" + _classentity.Name + ",";
            }
            ViewBag.ClassId = classid;
            ViewBag.SelectClassName = _classentity.Name;
            ViewBag.SiteId = _classentity.SiteId;
            ViewBag.OrderBy = order_i;
            ViewBag.ClassEntity = _classentity;
            ViewBag.BrandId = brandid;
            int rediclassid = classid;//实际指向分类
            if (rediclassid == -1) rediclassid = 0;
            if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
              siteid = _classentity.SiteId;
            //if(siteid == 0&& rediclassid>0)
            //{
            //    ClassesFoundEntity _classredirectentity = ClassesFoundBLL.Instance.GetClassesFound(rediclassid, false);
            //}  
            if (brandid > 0&& classid==0)
            {
                //获取子集分类
                IList<ClassesFoundEntity> classes = ClassesFoundBLL.Instance.GetClassesAllByBrandId(siteid, brandid, false);
                ViewBag.ClassList = classes; 
            }
            else
            {
                //分类导航显示
                if (_classentity.IsEnd == 0)
                {
                    //获取子集分类
                    IList<ClassesFoundEntity> classes = ClassesFoundBLL.Instance.GetClassesAllByPId(rediclassid, false, siteid);
                    ViewBag.ClassList = classes;
                }
            } 
            if (classid > 0)
            {
                //ViewBag.RavClassList = ListRavStr(_classentity); 
                if (brandid == 0)
                {
                    //获取分类对应品牌
                    IList<BrandEntity> brands = ClassBrandBLL.Instance.GetBrandByClass(rediclassid );
                    ViewBag.BrandList = brands;
                } 
                ////获取分类属性
                //IList<ClassPropertiesEntity> _ClassPropertieslist = ClassPropertiesBLL.Instance.GetAllPropertyByClassId(classid);
                //ViewBag.PropertiesList = _ClassPropertieslist; 
            }
            if (brandid> 0)
            {
                BrandEntity brand = BrandBLL.Instance.GetBrand(brandid, false);
                ViewBag.SelectBrandName = brand.Name;
                ViewBag.BrandSelect = brand;
                title += "品牌：" + brand.Name+",";
            }
            //ViewBag.PropertiesStr = propertiesstr;
     
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeList;
            int record = 0;
            if (pageindex == 0) pageindex = 1; 
            string classidstr = "";
            if (classid > 0)
            {
                IList<int> classintlist = new List<int>();
                if (_classentity.RedirectClassId > 0)
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(_classentity.RedirectClassId);
                }
                else
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(classid); 
                }
                if (classintlist != null && classintlist.Count > 100)
                    classintlist = classintlist.Take(100).ToList<int>();
                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
                else if(_classentity.RedirectClassId > 0)
                {
                    classidstr = StringUtils.GetDbString(_classentity.RedirectClassId);
                }
                else
                {
                    classidstr = StringUtils.GetDbString(classid); 
                }
            }
            else
            {
               
                    IList<int> classintlist = new List<int>();
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndListBySite(siteid );
                if (classintlist != null && classintlist.Count > 100)
                    classintlist = classintlist.Take(100).ToList<int>();

                if (classintlist != null && classintlist.Count > 0)
                    {
                        classidstr = string.Join("_", classintlist);
                    }
                    else if (_classentity.RedirectClassId > 0)
                    {
                        classidstr = StringUtils.GetDbString(_classentity.RedirectClassId);
                    }
                    else
                    {
                        classidstr = StringUtils.GetDbString(classid);
                    } 
            }
            IList<VWProductEntity> _productlist = new List<VWProductEntity>();// ProductBLL.Instance.GetProductListProc(pagesize, pageindex, ref record, classidstr, brandid, propertiesstr, order_i,     _key  );
           _productlist = ProductBLL.Instance.GetProductListProc(pagesize, pageindex, ref record, classidstr, brandid, propertiesstr, order_i, _key);

            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                foreach (VWProductEntity _entity in _productlist)
                { 
                    _entity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, _entity.TradePrice, _entity.Price, _entity.IsBP, _entity.DealerPrice);
                }
            }
            else
            {
                foreach (VWProductEntity _entity in _productlist)
                { 
                    _entity.ActualPrice = 0;
                } 
            } 
            ViewBag.Title = title;
            ViewBag.ProductList = _productlist;
            ViewBag.TotalNum = record; 
            //ViewBag.ListShowMethod = (int)ListShowMethodEnum.Normal;
            string url = "/Product/list.html?px=" + order_i + "&cid=" + classid + "&bd=" + brandid + "&pp=" + propertiesstr+ "&s=" + siteid +"&ct=0&key=" + _key;
            ViewBag.SiteId = siteid;
            string pagehtml = HTMLPage.SetOrderListPage(record, pagesize, pageindex, url);
            ViewBag.PageHtml = pagehtml;
            int maxpage = record / pagesize;
            if (record % pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }

        public ActionResult Categray()
        { 
            int siteid = QueryString.IntSafeQ("s");
            if (siteid == 0) siteid = (int)SiteIdEnum.BathRoom;  
            ViewBag.SiteId = siteid; 
            return View();
        }

        public string GetJsonList()
        {
            ListObj result = new ListObj();
            string _key = QueryString.SafeQ("key");
            int classid = QueryString.IntSafeQ("cid");
            int brandid = QueryString.IntSafeQ("bd");   
            int siteid = QueryString.IntSafeQ("s");
            if (siteid <= 0) siteid = (int)SiteIdEnum.BathRoom;
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeList;
            int record = 0;
            int order_i = 0;//默认排序
            if (pageindex == 0) pageindex = 1;
            ViewBag.SelectClassId = classid;
            ViewBag.SelectBrandId = brandid; 
            int rediclassid = classid;
            string classidstr = "";//分类子集

            

            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, false);
                if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                IList<int> classintlist = new List<int>();
                if (_classentity.RedirectClassId > 0)
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(_classentity.RedirectClassId);
                }
                else
                {
                    classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(classid);
                }
                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
                else if (_classentity.RedirectClassId > 0)
                {
                    classidstr = StringUtils.GetDbString(_classentity.RedirectClassId);
                }
                else
                {
                    classidstr = StringUtils.GetDbString(classid);
                }
            }
            else
            {

                IList<int> classintlist = new List<int>();
                classintlist = ClassesFoundBLL.Instance.GetSubClassEndListBySite(siteid);

                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
            }


            IList<VWProductEntity> _productlist = new List<VWProductEntity>();// ProductBLL.Instance.GetProductListProcCYC(pagesize, pageindex, ref record, classidstr, brandid, "", order_i, producttype,  cartype ,jishi);
            _productlist = ProductBLL.Instance.GetProductListProc(pagesize, pageindex, ref record, classidstr, brandid, "", order_i, _key);

            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, _entity.TradePrice, _entity.Price, _entity.IsBP, _entity.DealerPrice);
                }
            }
            else
            {
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = 0;
                }

            }
            result.Total = record;
            result.List = _productlist;

            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }

        public ActionResult Search()
        { 
            int classid = QueryString.IntSafeQ("cl");
            int brandid = QueryString.IntSafeQ("bd");
            int siteid = QueryString.IntSafeQ("s"); 
            int pageindex = QueryString.IntSafeQ("pageindex");
            if (pageindex < 1) pageindex = 1;
            int pagesize = CommonKey.PageSizeList;
            string  querykey = QueryString.SafeQ("key"); 
            //SearchIndex search = new SearchIndex();
            //GyLuceneEntity luceneentity = search.LuceneSearch(querykey, siteid, brandid, classid, jishi, pagesize, pageindex);
            //IList<VWProductEntity> _productlist = new List<VWProductEntity>();
            //List <ProductLuceneEntity> list= luceneentity.ProductList;
            //IList<ClassesFoundEntity> listclass = new List<ClassesFoundEntity>(); 
            //if (luceneentity.ClassList != null && luceneentity.ClassList.Count > 0)
            //{
            //    foreach (int cid in luceneentity.ClassList)
            //    {
            //        ClassesFoundEntity entity = ClassesFoundBLL.Instance.GetClassesFound(cid, true);
            //        listclass.Add(entity);
            //    }
            //}
            //IList<BrandEntity> listbrand = new List<BrandEntity>(); 
            //if (luceneentity.BrandList != null && luceneentity.BrandList.Count > 0)
            //{
            //    foreach (int cid in luceneentity.BrandList)
            //    {
            //        BrandEntity entity = BrandBLL.Instance.GetBrand(cid, true);
            //        listbrand.Add(entity);
            //    }
            //}
            //if (brandid > 0)
            //{
            //    BrandEntity brandentity = BrandBLL.Instance.GetBrand(brandid, true);
            //    ViewBag.BrandSelect = brandentity; 
            //}
            //if (classid > 0)
            //{
            //    ClassesFoundEntity classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
            //    ViewBag.ClassSelect = classentity;
            //}
            //int listmethod = (int)SuperMarket.Model.ListShowMethodEnum.Default;
            //if (list != null && list.Count > 0)
            //{
            //    bool haslogin = false;
            //    MemberLoginEntity member = CookieBLL.GetLoginCookie();
            //    if (member != null && member.MemId > 0)
            //    {
            //        ViewBag.MemId = member.MemId;
            //        ViewBag.MemStatus = member.Status;
            //        haslogin = true;
            //    }
            //    foreach (ProductLuceneEntity _productentity in list)
            //    {
            //        VWProductEntity productentity = ProductBLL.Instance.GetProVWByDetailId(_productentity.ProductDetailId);
            //        if (productentity.ListShowMethod == (int)SuperMarket.Model.ListShowMethodEnum.Normal)
            //        {
            //            listmethod = productentity.ListShowMethod;
            //        }
            //        if (haslogin)
            //        {
            //            productentity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, productentity.TradePrice, productentity.Price, productentity.IsBP, productentity.DealerPrice);

            //        }
            //        else
            //        {
            //            productentity.ActualPrice = 0;
            //        }
            //        _productlist.Add(productentity);
            //    } 
            //}
            //int record = luceneentity.Count;
            string url = "/Product/Search.html?s="+siteid+"&cl="+classid+"&bd="+brandid+"&key=" + querykey; 
            //string pagehtml = HTMLPage.SetOrderListPage(record, pagesize, pageindex, url);
            //ViewBag.PageHtml = pagehtml;
            //ViewBag.ListShowMethod = listmethod;
            ViewBag.BrandId = brandid;
            ViewBag.ClassId = classid;
            ViewBag.SiteId = siteid;
            //ViewBag.SearchKey = querykey;
            //ViewBag.ProductList = _productlist;
            //ViewBag.ClassList = listclass;
            //ViewBag.BrandList = listbrand; 
            return View();
        }

        public ActionResult Limited()
        {
            string title = "易店心,限量乐购";
            string _key = "";
            int classid = 0;
            int brandid = 0;
            int order_i = 0;//排序类型 
            int producttype = (int)ProductType.SpecialPrice;
  
            int _classtype = -1; 
            _classtype = QueryString.IntSafeQ("clt"); 
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeList;
            int record = 0;
            if (pageindex == 0) pageindex = 1;
            string propertiesstr = "";
            string classidstr = "";
            IList<VWProductEntity> _productlist = ProductBLL.Instance.GetProductListProc(pagesize, pageindex, ref record, classidstr, brandid, propertiesstr, order_i,  _key );

            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade,   _entity.TradePrice, _entity.Price, _entity.IsBP, _entity.DealerPrice);
                }
            }
            else
            {
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = 0;
                }

            }
             
            ViewBag.Title = title;
            ViewBag.ProductList = _productlist;
            ViewBag.TotalNum = record;
            string url = "/Product/Limited.html?";

            string pagehtml = HTMLPage.SetOrderListPage(record, pagesize, pageindex, url);
            ViewBag.PageHtml = pagehtml;
            return View();
        }
        public ActionResult FourMatch()
        {
            string title = "易店心,四配套专区";
            string _key = ""; 
            int brandid = 0;
            int order_i = 0;//排序类型  
             
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeList;
            int record = 0;
            if (pageindex == 0) pageindex = 1;
            string propertiesstr = "";
            string classidstr = "846";
            IList<VWProductEntity> _productlist = ProductBLL.Instance.GetProductListProc(pagesize, pageindex, ref record, classidstr, brandid, propertiesstr, order_i,   _key );

            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, _entity.TradePrice, _entity.Price, _entity.IsBP, _entity.DealerPrice);
                }
            }
            else
            {
                foreach (VWProductEntity _entity in _productlist)
                {
                    _entity.ActualPrice = 0;
                } 
            } 
            ViewBag.Title = title;
            ViewBag.ProductList = _productlist;
            ViewBag.TotalNum = record;
            string url = "/Product/FourMatch.html?";

            string pagehtml = HTMLPage.SetOrderListPage(record, pagesize, pageindex, url);
            ViewBag.PageHtml = pagehtml;
            return View();
        }

        public ActionResult Special()
        {
            string title = "小店专区-";
            int _specialid = QueryString.IntSafeQ("sid");
            string _specialcode = QueryString.SafeQ("code");
            int pageindex = QueryString.IntSafeQ("pageindex");
            if( pageindex==0) pageindex = 1;
            int _pagesize = CommonKey.PageSizeProductMobile;
            int isactive = 1;
            int _recordCount = 0;
            ProductSpecialEntity sentity = new ProductSpecialEntity();
            if (_specialid > 0)
            {
                sentity = ProductSpecialBLL.Instance.GetProductSpecial(_specialid);
            }
            else if (!string.IsNullOrEmpty(_specialcode))
            {
                sentity = ProductSpecialBLL.Instance.GetProductSpecialByCode(_specialcode);
            }
            title = sentity.FullName;
            IList<VWProductSpecialDetailsEntity> sdlist = ProductSpecialDetailsBLL.Instance.GetProductSpecialDetailsList(pageindex, _pagesize, ref _recordCount, sentity.Id, isactive);
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                if (sdlist != null && sdlist.Count > 0)
                {
                    foreach (VWProductSpecialDetailsEntity entity in sdlist)
                    {
                        if (entity.ProductDetail != null && entity.ProductDetail.ProductId > 0)
                            entity.Price = Calculate.GetPrice(member.Status, member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                    }
                }
            }
            ViewBag.ProductSpecialEntity = sentity;
            ViewBag.SpecialDetailList = sdlist;
            string url = "/Product/Special?sid=" + _specialid + "&code=" + _specialcode;
            string pagehtml = HTMLPage.SetOrderListPage(_recordCount, _pagesize, pageindex, url);
            ViewBag.PageHtml = pagehtml;
            ViewBag.Title = title;
            return View();
        }

        public ActionResult NoProduct()
        {
            return View();
        }
        /// <summary>
        /// 特价商品列表
        /// </summary>
        /// <returns></returns>
      
        private IList<ClassesFoundEntity> ListRavStr(ClassesFoundEntity _classentity)
        {
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list.Add(_classentity);
            if (_classentity != null)
            {
                if (_classentity.ParentId > 0)
                {
                    ClassesFoundEntity _classentity2 = ClassesFoundBLL.Instance.GetClassesFound(_classentity.ParentId, false);
                    if (_classentity.Name != _classentity2.Name) list.Add(_classentity2);
                    if (_classentity2.ParentId > 0)
                    {
                        ClassesFoundEntity _classentity3 = ClassesFoundBLL.Instance.GetClassesFound(_classentity2.ParentId, false);
                        if(_classentity2.Name!= _classentity3.Name)
                        list.Add(_classentity3);
                        if (_classentity3.ParentId > 0)
                        {
                            ClassesFoundEntity _classentity4 = ClassesFoundBLL.Instance.GetClassesFound(_classentity3.ParentId, false);
                            if (_classentity3.Name != _classentity4.Name)
                                list.Add(_classentity4);
                        }
                    }
                }
            }
            return list;
        }

        public ActionResult ProductEvaluation()
        {
            return View();
        }
        public ActionResult Categroy()
        { 
            int ct = QueryString.IntSafeQ("ct");
            if (ct == 0) ct = 1;
            ViewBag.ClassType = ct;
            return View();
        }
      

        public ActionResult WholeParts()
        {
            //int cartypeid = QueryString.IntSafeQ("ct");
            //int cartypelevel = 0;
            //int ctid1 = 0;
            //int ctid2 = 0;
            //int ctid3 = 0;
            //int ctid4 = 0;
            //int _classtype = -1;
            //_classtype = QueryString.IntSafeQ("clt");
            //if (cartypeid == -1)
            //{
            //    CookieBLL.ClearCarTypeIdCookie();
            //}
            //if (cartypeid >= 0)
            //{
            //    ctid1 = CookieBLL.GetCarTypeIdCookie(1);
            //    ctid2 = CookieBLL.GetCarTypeIdCookie(2);
            //    ctid3 = CookieBLL.GetCarTypeIdCookie(3);
            //    ctid4 = CookieBLL.GetCarTypeIdCookie(4);
            //    //if (cartypeid > 0)
            //    //{
            //    CarTypeEntity cartypeentity = CarTypeBLL.Instance.GetCarType(cartypeid);
            //    cartypelevel = cartypeentity.CarTypeLevel;
            //    //清除原先的不一致的车型选择
            //    if (cartypelevel == 1 && ctid1 != cartypeid)
            //    {
            //        ctid2 = 0;
            //        ctid3 = 0;
            //        ctid4 = 0;
            //    }
            //    else if (cartypelevel == 2 && ctid2 != cartypeid)
            //    {
            //        ctid3 = 0;
            //        ctid4 = 0;
            //    }
            //    else if (cartypelevel == 3 && ctid3 != cartypeid)
            //    {
            //        ctid4 = 0;
            //    }
            //    //获取新的车型选择项
            //    if (cartypelevel == 4)
            //    {
            //        ctid4 = cartypeid;
            //        cartypeentity = CarTypeBLL.Instance.GetCarType(cartypeentity.ParentId);
            //        CookieBLL.SetCarTypeIdCookie(4, ctid4);
            //    }
            //    if (cartypeentity.CarTypeLevel == 3)
            //    {
            //        ctid3 = cartypeentity.Id;
            //        cartypeentity = CarTypeBLL.Instance.GetCarType(cartypeentity.ParentId);
            //        CookieBLL.SetCarTypeIdCookie(3, ctid3);

            //    }
            //    if (cartypeentity.CarTypeLevel == 2)
            //    {

            //        ctid2 = cartypeentity.Id;
            //        cartypeentity = CarTypeBLL.Instance.GetCarType(cartypeentity.ParentId);
            //        CookieBLL.SetCarTypeIdCookie(2, ctid2);
            //    }
            //    if (cartypeentity.CarTypeLevel == 1)
            //    {
            //        ctid1 = cartypeentity.Id;
            //        CookieBLL.SetCarTypeIdCookie(1, ctid1);
            //    } 
            //}
            //ViewBag.CarTypeId1 = ctid1;
            //ViewBag.CarTypeId2 = ctid2;
            //ViewBag.CarTypeId3 = ctid3;
            //ViewBag.CarTypeId4 = ctid4;
            return View(); 
        }

        public string GetProductStockNum()
        {
            string result = "";
            int _pdid = FormString.IntSafeQ("pdid");//上级品牌Id，0代表第一级
            if (_pdid > 0)
            {
                VWProductDetailEntity entity = ProductDetailBLL.Instance.GetProductDetail(_pdid);
                if(entity!=null&& entity.Id>0)
                {
                    result = entity.StockNum.ToString();
                }
            }
            return result;
        }
    }
}
