using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
//using SuperMarket.BLL.Search;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class  ProductController : BaseCommonController
    {

        #region 展示

        public ActionResult Categray()
        {
            ViewBag.PageMenu = "2";
            int siteid = QueryString.IntSafeQ("s");
            if (siteid == 0) siteid = (int)SiteEnum.Default;
            int _classmenutype = (int)ClassMenuTypeEnum.Normal;//分类列表选择类型
            int jishi = QueryString.IntSafeQ("js");
            if (jishi == 0) jishi = (int)JiShiSongEnum.Normal;
            if (jishi == (int)JiShiSongEnum.JiShi) _classmenutype = (int)ClassMenuTypeEnum.JiShiDa;
            ViewBag.SiteId = siteid;
            ViewBag.ClassMenuType = _classmenutype;
            return View();
        }
        /// <summary>
        /// 网站首页
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            int classid = QueryString.IntSafeQ("cl");
            int brandid = QueryString.IntSafeQ("bd"); 
            int cartypemodelid = QueryString.IntSafeQ("ct");//车型 
            int siteid = QueryString.IntSafeQ("s");
            if (siteid == 0) siteid = (int)SiteEnum.Default; 
            int jishi = QueryString.IntSafeQ("js");//是否及时送
            if (jishi == 0) jishi = (int)JiShiSongEnum.Normal;
             
            int producttype = QueryString.IntSafeQ("pt");
            if(producttype==0)  producttype = (int)ProductType.Normal;
            int pageindex = QueryString.IntSafeQ("pageindex");
            int _classmenutype = (int)ClassMenuTypeEnum.Normal;//分类列表选择类型
            if (jishi == (int)JiShiSongEnum.JiShi) _classmenutype = (int)ClassMenuTypeEnum.JiShiDa;
            int _pagesize = CommonKey.PageSizeList;
            int _recordCount = 0;
            int order_i = 0;//默认排序
            int _classtype = -1;
            _classtype = QueryString.IntSafeQ("clt");
            ViewBag.ClassType = _classtype;
            if (pageindex == 0) pageindex = 1;
            ViewBag.SelectClassId = classid;
            ViewBag.SelectBrandId = brandid; 
            ViewBag.CarTypeModelId = cartypemodelid;
            ViewBag.ProductType = producttype;
            int rediclassid = classid;
            string classidstr = "";//分类子集
            //获取选择的品牌
            if (brandid > 0)
            {
                BrandEntity brand = BrandBLL.Instance.GetBrand(brandid, true);
                ViewBag.SelectBrandName = brand.Name; 
            }
            //获取选择的车型
            if(cartypemodelid>0)
            {
                CarTypeModelEntity cartypemodel = CarTypeModelBLL.Instance.GetCarTypeModel(cartypemodelid);
                ViewBag.SelectCarTypeName = cartypemodel.ModelName; 
            } 
            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
                siteid = _classentity.SiteId;
                if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                ViewBag.SelectClassName = _classentity.Name;
                //获取分类子集
                IList<int> classintlist = new List<int>();
                classintlist = ClassesFoundBLL.Instance.GetSubClassEndList(rediclassid);

                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
                else
                {
                    classidstr = StringUtils.GetDbString(rediclassid);
                }
            }
            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
                if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                ViewBag.SelectClassName = _classentity.Name;
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
                classintlist = ClassesFoundBLL.Instance.GetSubClassEndListBySite(siteid, _classmenutype);

                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
            }
            IList<VWProductEntity> _productlist = ProductBLL.Instance.GetProductListProcCYC(_pagesize, pageindex, ref _recordCount, classidstr, brandid, "", order_i, producttype,  cartypemodelid,jishi);

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

            ViewBag.ProductList = _productlist;
            ViewBag.SiteId = siteid;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            ViewBag.JiShiSong = jishi;  
            ViewBag.ClassmMenuType = _classmenutype; 
            return View();
        }
        public ActionResult Search()
        {
            int classid = QueryString.IntSafeQ("cl");
            int brandid = QueryString.IntSafeQ("bd");
            int siteid = QueryString.IntSafeQ("s");
            int pageindex = QueryString.IntSafeQ("pi");
            int jishi = QueryString.IntSafeQ("js",-1);
            if (jishi == -1) jishi = (int)JiShiSongEnum.Normal;
            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
                ViewBag.SelectClassName = _classentity.Name;
                ViewBag.SelectClassId = classid;
            }
            if (brandid > 0)
            {
                BrandEntity brand = BrandBLL.Instance.GetBrand(brandid, true);
                ViewBag.SelectBrandName = brand.Name;
                ViewBag.SelectBrandId = brandid;
            }

            if (pageindex < 1) pageindex = 1;
            int pagesize = CommonKey.PageSizeList;
            string querykey = QueryString.SafeQ("key");
            //SearchIndex search = new SearchIndex();
            //GyLuceneEntity luceneentity = search.LuceneSearch(querykey, siteid, brandid, classid,jishi, pagesize, pageindex);
            IList<VWProductEntity> _productlist = new List<VWProductEntity>();
            //List<ProductLuceneEntity> list = luceneentity.ProductList;
            IList<ClassesFoundEntity> listclass = new List<ClassesFoundEntity>();
            //if (luceneentity.ClassList != null && luceneentity.ClassList.Count > 0)
            //{
            //    foreach (int cid in luceneentity.ClassList)
            //    {
            //        ClassesFoundEntity entity = ClassesFoundBLL.Instance.GetClassesFound(cid, true);
            //        listclass.Add(entity);
            //    }
            //}
            IList<BrandEntity> listbrand = new List<BrandEntity>();
            //if (luceneentity.BrandList != null && luceneentity.BrandList.Count > 0)
            //{
            //    foreach (int cid in luceneentity.BrandList)
            //    {
            //        BrandEntity entity = BrandBLL.Instance.GetBrand(cid, true);
            //        listbrand.Add(entity);
            //    }
            //}
            if (brandid > 0)
            {
                BrandEntity brandentity = BrandBLL.Instance.GetBrand(brandid, true);
                ViewBag.BrandSelect = brandentity;
            }
            if (classid > 0)
            {
                ClassesFoundEntity classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
                ViewBag.ClassSelect = classentity;
            }
            int listmethod = (int)SuperMarket.Model.ListShowMethodEnum.Default;
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
            //    //else
            //    //{
            //    //    foreach (ProductLuceneEntity product in list)
            //    //    {
            //    //        if (product.ListShowMethod == (int)SuperMarket.Model.ListShowMethodEnum.Normal)
            //    //        {
            //    //            listmethod = product.ListShowMethod;
            //    //            break;
            //    //        }
            //    //    }
            //    //}

            //}
            //int record = luceneentity.Count;
            //string url = "/Product/Search.html?s=" + siteid + "&cl=" + classid + "&bd=" + brandid + "&key=" + querykey;
            //string pagehtml = HTMLPage.SetOrderListPage(record, pagesize, pageindex, url);
            //ViewBag.MaxPageIndex = record;
            //ViewBag.PageHtml = pagehtml;
            ViewBag.ListShowMethod = listmethod;
            ViewBag.BrandId = brandid;
            ViewBag.ClassId = classid;
            ViewBag.SiteId = siteid;
            ViewBag.SearchKey = querykey;
            ViewBag.ProductList = _productlist;
            ViewBag.ClassList = listclass;
            ViewBag.BrandList = listbrand;
            ViewBag.JiShiSong = jishi;
            return View();
        }
         public string GetJsonList()
        {
            ListObj result = new ListObj();
            int classid = QueryString.IntSafeQ("cl");
            int brandid = QueryString.IntSafeQ("bd"); 
            int cartype  = QueryString.IntSafeQ("ct"); 
            int producttype = QueryString.IntSafeQ("pt");
            int jishi = QueryString.IntSafeQ("js");
            if (jishi == 0) jishi = (int)JiShiSongEnum.Normal;
            if (producttype==0)  producttype = (int)ProductType.Normal;
            int siteid = QueryString.IntSafeQ("s");
            if (siteid <= 0) siteid = (int)SiteEnum.Default;
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeList;
            int record = 0;
            int order_i = 0;//默认排序
            int _classtype = -1;
            _classtype = QueryString.IntSafeQ("clt");
            ViewBag.ClassType = _classtype;
            if (pageindex == 0) pageindex = 1;
            ViewBag.SelectClassId = classid;
            ViewBag.SelectBrandId = brandid; 
            ViewBag.CarType  = cartype;
            int _classmenutype = (int)ClassMenuTypeEnum.Normal;//分类列表选择类型
            int rediclassid = classid;
            string classidstr = "";//分类子集

            //获取选择的车型
            if (cartype  > 0)
            {
                CarTypeModelEntity cartypemodel = CarTypeModelBLL.Instance.GetCarTypeModel(cartype);
                ViewBag.SelectCarTypeName = cartypemodel.ModelName;
            }
             
            if (classid > 0)
            {
                ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
                if (_classentity.RedirectClassId > 0) rediclassid = _classentity.RedirectClassId;
                ViewBag.SelectClassName = _classentity.Name;
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
                classintlist = ClassesFoundBLL.Instance.GetSubClassEndListBySite(siteid, _classmenutype);

                if (classintlist != null && classintlist.Count > 0)
                {
                    classidstr = string.Join("_", classintlist);
                }
            }


            IList<VWProductEntity> _productlist = ProductBLL.Instance.GetProductListProcCYC(pagesize, pageindex, ref record, classidstr, brandid, "", order_i, producttype,  cartype ,jishi);

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
            result.Total = record;
            result.List = _productlist;

            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public ActionResult Detail()
        {
            int _productdetailid = QueryString.IntSafeQ("pd");
            VWProductEntity _vwentity = new VWProductEntity(); 
            _vwentity = ProductBLL.Instance.GetProVWByDetailId(_productdetailid);
            IList<ProductStylePicsEntity> productpiclist = ProductStylePicsBLL.Instance.GetListPics(_vwentity.StyleId, _vwentity.ProductId, _vwentity.ShowPicType);
            _vwentity.ProductPics = productpiclist;
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                _vwentity.ActualPrice = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, _vwentity.TradePrice, _vwentity.Price, _vwentity.IsBP, _vwentity.DealerPrice);
            }
            VWProductNomalParamEntity paramentity = ProductExtBLL.Instance.GetProductNormalParamById(_vwentity.ProductId);
            ViewBag.VWProductEntity = _vwentity;
            ViewBag.ProductParamEntity = paramentity;  
            return View();
        }
        /// <summary>
        /// 爆品页面
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductBp()
        {
            int pageindex = QueryString.IntSafeQ("pageindex");
            if (pageindex == 0) pageindex = 1;
            int _pagesize = CommonKey.PageSizeProductMobile;
            int isactive = 1;
            int showproduct = -1;//代表所有
            int _recordCount = 0;

            IList<VWProductBaoPinEntity> listproduct = ProductBaoPinBLL.Instance.GetProductBaoPinList(pageindex, _pagesize, ref _recordCount, isactive, showproduct);
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                if (listproduct != null && listproduct.Count > 0)
                {
                    foreach (VWProductBaoPinEntity entity in listproduct)
                    {
                        if (entity.ProductDetail != null && entity.ProductDetail.ProductId > 0)
                            entity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                    }
                }
            }
            ViewBag.ListProduct = listproduct;
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1; 
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }

 
        public string GetProductBpList()
        {
            ListObj result = new ListObj();
            
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeProductMobile;
            int isactive = 1;
            int showproduct = -1;//代表所有
            int recordcount = 0;

            IList<VWProductBaoPinEntity> listproduct = ProductBaoPinBLL.Instance.GetProductBaoPinList(pageindex, pagesize, ref recordcount, isactive, showproduct);
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                ViewBag.MemId = member.MemId;
                ViewBag.MemStatus = member.Status;
                if (listproduct != null && listproduct.Count > 0)
                {
                    foreach (VWProductBaoPinEntity entity in listproduct)
                    {
                        if (entity.ProductDetail != null && entity.ProductDetail.ProductId > 0)
                            entity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                    }
                }
            }
            result.Total = recordcount;
            result.List = listproduct;

            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        /// <summary>
        /// 专区产品显示
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductS()
        {
            int _specialid = QueryString.IntSafeQ("sid");
            string _specialcode = QueryString.SafeQ("code");
            int pageindex = 1;
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
                            entity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                    }
                }
            }
            ViewBag.ProductSpecialEntity = sentity;
            ViewBag.SpecialDetailList = sdlist; 
            int maxpage = _recordCount / _pagesize;
            if (_recordCount % _pagesize > 0) maxpage = maxpage + 1;
            ViewBag.MaxPageIndex = maxpage;
            return View();
        }
         
        public string GetProductSList()
        {
            ListObj result = new ListObj();

            int sid = QueryString.IntSafeQ("sid");
            int pageindex = QueryString.IntSafeQ("pageindex");
            int pagesize = CommonKey.PageSizeProductMobile;
            int isactive = 1; 
            int recordcount = 0;

            IList<VWProductSpecialDetailsEntity> sdlist = ProductSpecialDetailsBLL.Instance.GetProductSpecialDetailsList(pageindex, pagesize, ref recordcount, sid, isactive);
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
                            entity.Price = Calculate.GetPrice(member.Status,member.IsStore, member.StoreType, member.MemGrade, entity.ProductDetail.TradePrice, entity.ProductDetail.Price, entity.ProductDetail.IsBP, entity.ProductDetail.DealerPrice);
                    }
                }
            }
            result.Total = recordcount;
            result.List = sdlist;
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }

        public ActionResult BrandShow()
        {
            return View();
        }
        public ActionResult DailyGrab()
        {
            int isactive = 1;
            IList<VWProductDailyGrabEntity> vwplist = ProductDailyGrabBLL.Instance.GetVWProductDailyGrabAll(isactive);
            ViewBag.DailyGrabList = vwplist;
            return View();
        }
        #endregion

    }
}
