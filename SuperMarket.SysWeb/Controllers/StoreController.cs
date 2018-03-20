using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
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
using SuperMarket.Model.Basic.VW.Product;
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

namespace SuperMarket.Admin.Controllers
{
    public class StoreController : BaseMemAdminController
    {

        #region 测试
        public ActionResult Test()
        {
            return View();
        }
        #endregion

        #region 默认
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 产品类别
        public ActionResult PClass()
        {
            ViewBag.TypeId = QueryString.IntSafeQ("type");
            return View();
        }
        #endregion

        #region 产品发布(0)

        public ActionResult PRelease()
        {
            //判断是否是修改
            int styleid = QueryString.IntSafeQ("styleid");
            int classid = 0;
            VWProductStyleEntity _styleentity = new VWProductStyleEntity();
            if (styleid > 0)
            {
                _styleentity = ProductStyleBLL.Instance.GetProductStyle(styleid);
                _styleentity.StylePics = ProductStylePicsBLL.Instance.GetListByStyleId(styleid);//获取样式图片列表
                classid = _styleentity.ClassId;
            }
            else
            {
                classid = QueryString.IntSafeQ("classid");
            }

            if (classid == 0) RedirectToAction("PClass");
            ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
            if (_classentity.IsEnd == 0) RedirectToAction("PClass");

            string className = _classentity.Name;
            int _pid = _classentity.ParentId;
            for (int i = 1; i < _classentity.ClassLevel && _pid > 0; i++)
            {
                ClassesFoundEntity _pentity = ClassesFoundBLL.Instance.GetClassesFound(_pid, true);
                _pid = _pentity.ParentId;
                className = _pentity.Name + ">" + className;
            }
            ViewBag.ClassNamePath = className;
            ViewBag.ClassId = classid;
            ViewBag.ProductStyle = _styleentity;
            ViewBag.MemId = memid;

            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            return View();
        }

        public ActionResult PPublished()
        {
            int _classid = QueryString.IntSafeQ("classid");
            IList<ProductStyleEntity> _entitylist = ProductStyleBLL.Instance.GetProductStyleByClassFoundId(_classid);
            ViewBag.EntityList = _entitylist;
            return View();
        }
        #endregion

        #region 清除缓存
        public ActionResult ClearMemCache()
        {
            string _cachekey = QueryString.SafeQ("id");
            MemCache.RemoveAllCache();
            return View();
        }

        #endregion

        #region 商家信息管理
        public ActionResult StoreInfo()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            return View();
        }
        #endregion

        #region 商家申请
        public ActionResult StoreInfoApply()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            return View();
        }
        #endregion

        #region 商家信息管理    
        public ActionResult StoreInfoManage()
        {
            VWStoreEntity _meminforntity = StoreBLL.Instance.GetVWStoreByMemId(memid);
            ViewBag.Store = _meminforntity;
            if (_meminforntity.Latitude > 0 && _meminforntity.Longitude > 0)
            {
                ViewBag.Latitude = _meminforntity.Latitude;
                ViewBag.Longitude = _meminforntity.Longitude;
            }
            else
            {
                string theIP = "114.97.195.112";// YMethod.GetHostAddress();
                string msg = MapCore.GetInfoByUrl(MapCore.GetMapIPUrl(theIP));
                msg = System.Text.RegularExpressions.Regex.Unescape(msg);
                BaiduApiStatus info = JsonConvert.DeserializeObject<BaiduApiStatus>(msg);
                if (info.status.Equals("0"))
                {
                    BaiduApiLocation baiduApiLocation = JsonConvert.DeserializeObject<BaiduApiLocation>(msg);
                    ViewBag.Longitude = baiduApiLocation.content.point.x;
                    ViewBag.Latitude = baiduApiLocation.content.point.y;
                }
                else if (info.status.Equals("1"))
                {
                    ViewBag.Latitude = "0";
                    ViewBag.Longitude = "0";
                }
            }
            return View();
        }
        #endregion

        #region 商家申请提交
        public string StoreInfoApplySubmit()
        {

            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                string _companyname = FormString.SafeQ("companyname");
                string _licenseno = FormString.SafeQ("licenseno");
                string _licensepath = FormString.SafeQ("licensepath", 200);
                string _legalname = FormString.SafeQ("legalname");
                string _legalmobilephone = FormString.SafeQ("legalmobilephone");
                string _legalidentityno = FormString.SafeQ("legalidentityno");
                string _legalidentitypre = FormString.SafeQ("legalidentitypre", 200);
                string _legalidentitybeh = FormString.SafeQ("legalidentitybeh", 200);

                MemStoreEntity _supplierrntity = StoreBLL.Instance.GetStoreByMemId(memid);
                if (_supplierrntity.Id > 0)
                {
                    if (_supplierrntity.Status == (int)StoreStatus.Active)
                        return ((int)CommonStatus.Applyed).ToString();
                    if (_supplierrntity.Status == (int)StoreStatus.OnChecking)
                        return ((int)CommonStatus.Checking).ToString();
                }
                _supplierrntity.MemId = memid;
                _supplierrntity.CompanyName = _companyname;
                _supplierrntity.LicensePath = _licensepath;
                _supplierrntity.LicenseNo = _licenseno;
                _supplierrntity.LegalName = _legalname;
                _supplierrntity.LegalIdentityNo = _legalidentityno;
                _supplierrntity.LegalIdentityPre = _legalidentitypre;
                _supplierrntity.LegalIdentityBeh = _legalidentitybeh;
                _supplierrntity.LegalMobilePhone = _legalmobilephone;
                if (_supplierrntity.Id > 0)
                {
                    if (StoreBLL.Instance.UpdateStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }
                else
                {
                    _supplierrntity.CreateTime = DateTime.Now;
                    _supplierrntity.Status = (int)StoreStatus.WaitCheck;
                    if (StoreBLL.Instance.AddStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }

            }
            return ((int)CommonStatus.Fail).ToString();

        }
        #endregion 

        #region 店铺信息维护
        public string StoreInfoManageSubmit()
        {

            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                int _provincecode = FormString.IntSafeQ("provincecode");
                int _citycode = FormString.IntSafeQ("citycode");
                string _storename = FormString.SafeQ("storename", 200);
                string _mobilephone = FormString.SafeQ("mobilephone");
                string _address = FormString.SafeQ("address", 200);
                string _latitude = FormString.SafeQ("latitude");
                string _longitude = FormString.SafeQ("longitude");
                MemStoreEntity _supplierrntity = StoreBLL.Instance.GetStoreByMemId(memid);

                _supplierrntity.MemId = memid;
                _supplierrntity.ProvinceId = _provincecode;
                _supplierrntity.CityId = _citycode;
                _supplierrntity.StoreName = _storename;
                _supplierrntity.MobilePhone = _mobilephone;
                _supplierrntity.Address = _address;
                _supplierrntity.Latitude = StringUtils.GetDbDecimal(_latitude);
                _supplierrntity.Longitude = StringUtils.GetDbDecimal(_longitude);
                if (_supplierrntity.Id > 0)
                {
                    if (StoreBLL.Instance.UpdateStore(_supplierrntity) > 0)
                    {
                        return ((int)CommonStatus.SuccessApply).ToString();
                    }
                }
            }
            return ((int)CommonStatus.Fail).ToString();

        }
        #endregion

        #region 证件图片上传
        public string UploadFile()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            int aa = files.Count;

            int _pictype = FormString.IntSafeQ("pictype");
            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            CertificateType _certype = (CertificateType)_pictype;
            if (file != null)
            {
                byte[] bytes = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    bytes = binaryReader.ReadBytes(file.ContentLength);
                }
                FtpUtil _ftp = new FtpUtil();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName,  ImagesSysPathCode.Certifacate.ToString(), "behind_" + memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), Guid.NewGuid().ToString());
                string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return dicpathfull;
            }
            return "";
        }
        #endregion

        #region 产品图片上传
        public string UploadProductImages()
        {
            HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            int aa = files.Count;

            HttpPostedFile file = System.Web.HttpContext.Current.Request.Files[0];
            if (file != null)
            {
                byte[] bytes = null;
                using (var binaryReader = new BinaryReader(file.InputStream))
                {
                    bytes = binaryReader.ReadBytes(file.ContentLength);
                }
                FtpUtil _ftp = new FtpUtil();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName,ImagesSysPathCode.Product.ToString(), "Behind-" + memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), Guid.NewGuid().ToString());//生成图片路径
                string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));//指明图片的扩展名
                string picpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);//指明上传路径

                _ftp.UploadFile(picpath, bytes, true);
                return dicpathfull;
            }
            return "";
        }
        #endregion

        #region 获取规格值
        public string GetSpecValueList()
        {
            int _specid = FormString.IntSafeQ("specid");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<ClassProDetailsEntity> list = new List<ClassProDetailsEntity>();
            //list = ComSpecDetailsBLL.Instance.GetListBySpecId(_specid, _pid, memid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        #endregion

        #region 发布产品
        [ValidateInput(false)]
        public string ProductSubmit()
        {
            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                int _productId = FormString.IntSafeQ("productId");
                int _styleId = FormString.IntSafeQ("styleId");
                int _classId = FormString.IntSafeQ("classId");
                string _adTitle = FormString.SafeQ("adTitle", 200);
                string _title = FormString.SafeQ("title", 200);
                string _code = FormString.SafeQ("Code");
                string _oECode = FormString.SafeQ("oECode");
                string _placeorigin = FormString.SafeQ("placeorigin");
                string _name = FormString.SafeQ("name");
                string _spec1 = FormString.SafeQ("spec1");
                string _spec2 = FormString.SafeQ("spec2");
                decimal _grossweight = StringUtils.GetDbDecimal(FormString.SafeQ("grossweight"));
                string _brandId = FormString.SafeQ("brandId");
                int _cartyperelated = FormString.IntSafeQ("cartyperelated");
                string _productPropertysstr = FormString.SafeQ("productPropertysstr", 4000);
                string _CarTypeStr = FormString.SafeQ("CarTypeStr", 4000);
                string _productpicsstr = FormString.SafeQ("productpicsstr", 8000);
                int _num = FormString.IntSafeQ("num");
                decimal _price = StringUtils.GetDbDecimal(FormString.SafeQ("price", 200));
                decimal _tradePrice = StringUtils.GetDbDecimal(FormString.SafeQ("tradePrice", 200));
                decimal _marketPrice = StringUtils.GetDbDecimal(FormString.SafeQ("marketPrice", 200));
                int _isoem = FormString.IntSafeQ("isoem");
                int _transfeetype = FormString.IntSafeQ("transfeetype");
                decimal _transfee = StringUtils.GetDbDecimal(FormString.SafeQ("transfee"));
                string _contentcms = FormString.SafeQNo("contentcms", 8000);
                string _picUrl = FormString.SafeQ("picUrl", 500);
                int _unit = FormString.IntSafeQ("unit");
                ProcProductEntity _entity = new ProcProductEntity();

                _entity.StyleId = _styleId;
                _entity.ClassId = _classId;
                _entity.Code = _code;
                _entity.AdTitle = _adTitle;
                _entity.Title = _title;
                _entity.CarTypesStr = _CarTypeStr;
                _entity.BrandId = _brandId;
                _entity.ContentCms = _contentcms;
                _entity.IsOEM = _isoem;
                _entity.ProductId = _productId;
                _entity.TransFee = _transfee ;
                _entity.HasHtml = 0;
                _entity.TransFeeType = _transfeetype;
                _entity.MarketPrice = _marketPrice;
                _entity.Num = _num;
                _entity.CarTypeRelated = _cartyperelated;
                _entity.OECode = _oECode;
                _entity.GrossWeight = _grossweight;
                _entity.Name = _name;
                _entity.Spec1 = _spec1;
                _entity.Spec2 = _spec2;
                _entity.Name = _name;
                _entity.Unit = _unit;
                _entity.PlaceOrigin = _placeorigin;

                _entity.Price = _price;
                _entity.StyleId = _styleId;
                _entity.ProductPicsStr = _productpicsstr;
                _entity.PicUrl = _picUrl;
                _entity.ProductPropertysStr = _productPropertysstr;
                _entity.TradePrice = _tradePrice;
                _entity.CreateManId = memid;
                _styleId = ProductBLL.Instance.AddProductProc(_entity);

                if (_styleId > 0)
                {
                    return ((int)CommonStatus.Success).ToString();
                }

            }
            return ((int)CommonStatus.Fail).ToString();
        }
        #endregion

        #region 创建样式
        public string CreateStyle()
        {
            string str = "wqweqdasdadq121312";
            byte[] bytes = System.Text.Encoding.Default.GetBytes(str);
            FtpUtil _ftp = new FtpUtil();
            string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, ImagesSysPathCode.Certifacate.ToString(), "behind_" + memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), Guid.NewGuid().ToString());
            string dicpathfull = dicpath + ".html";
            string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
            _ftp.UploadFile(certifapath, bytes, true);
            return dicpathfull;

        }
        #endregion

        #region  样式管理


        /// <summary>
        /// 样式列表
        /// </summary>
        /// <returns></returns>
        public ActionResult PStyleList()
        {
            int _classid = QueryString.IntSafeQ("classid");
            int _brandid = QueryString.IntSafeQ("brandid");
            int pageindex = QueryString.IntSafeQ("pageindex");
            int orderby = QueryString.IntSafeQ("orderby");
            int type = QueryString.IntSafeQ("type");
            string stylename = QueryString.SafeQ("stylename");

            if (pageindex == 0) pageindex = 1;
            int _pagesize = CommonKey.PageSizeList;
            int record = 0;

            IList<ConditionUnit> wherelist = new List<ConditionUnit>();
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ClassId, CompareValue = _classid.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.BrandId, CompareValue = _brandid.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.OrderByType, CompareValue = orderby.ToString() });
            wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.StyleName, CompareValue = stylename });

            IList<ProductStyleEntity> _entitylist = ProductStyleBLL.Instance.GetProductStyleList(_pagesize, pageindex, ref record, wherelist);
            //type==2 表示选取的是产品，本例：发布产品的时候是唯一款式自动到对应款式的添加产品项
            if (type == 2 && _entitylist.Count == 1)
            {
                return RedirectToAction("ProductEdit", new { styleid = _entitylist[0].Id });
            }
            ViewBag.EntityList = _entitylist;
            string url = "/Store/PStyleList?class=" + _classid + "&brandid=" + _brandid + "&orderby=" + orderby;
            ViewBag.PageStr = HTMLPage.SetProductListPage(record, _pagesize, pageindex, url);
            return View();
        }

        /// <summary>
        /// 样式编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult PStyleEdit()
        {
            //判断是否是修改
            int styleid = QueryString.IntSafeQ("styleid");
            int classid = QueryString.IntSafeQ("classid");
            int brandid = QueryString.IntSafeQ("brandid");
            VWProductStyleEntity _styleentity = new VWProductStyleEntity();
            if (styleid > 0)
            {
                _styleentity = ProductStyleBLL.Instance.GetProductStyle(styleid);
                _styleentity.StylePics = ProductStylePicsBLL.Instance.GetListByStyleId(styleid);//获取样式图片列表
                //_styleentity.StylePropertys = ProductStyleProBLL.Instance.GetListByStyleId(styleid);//获取样式属性列表
                //_styleentity.ProductStyleCars = ProductStyleCarBLL.Instance.GetListByStyleId(styleid);//获取样式车型列表
                classid = _styleentity.ClassId;
                brandid = _styleentity.BrandId;
            }


            if (classid == 0 || brandid == 0) RedirectToAction("PClass");
            BrandEntity _brandentity = BrandBLL.Instance.GetBrand(brandid, true);
            ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
            if (_classentity.IsEnd == 0) RedirectToAction("PClass");
            string className = _classentity.Name;
            int _pid = _classentity.ParentId;
            for (int i = 1; i < _classentity.ClassLevel && _pid > 0; i++)
            {
                ClassesFoundEntity _pentity = ClassesFoundBLL.Instance.GetClassesFound(_pid, true);
                _pid = _pentity.ParentId;
                className = _pentity.Name + ">" + className;
            }
            ViewBag.ClassNamePath = className;
            ViewBag.ClassId = classid;
            ViewBag.BrandId = _brandentity.Id;
            ViewBag.BrandName = _brandentity.Name;
            ViewBag.ProductStyle = _styleentity;
            ViewBag.MemId = memid;

            return View();
        }


        /// <summary>
        /// 样式信息添加或修改
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public string StyleSubmit()
        {
            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                int _styleId = FormString.IntSafeQ("styleId");
                int _classId = FormString.IntSafeQ("classId");
                string _adTitle = FormString.SafeQ("adTitle", 200);
                string _title = FormString.SafeQ("title", 200);
                //string _oECode = FormString.SafeQ("oECode");
                int _brandId = FormString.IntSafeQ("brandId");
                //string _stylePropertys = FormString.SafeQ("stylePropertys", 4000);
                //string _brandCar = FormString.SafeQ("brandCar", 4000);
                string _stylePics = FormString.SafeQ("stylePics", 8000);
                //int _num = FormString.IntSafeQ("num");
                //decimal _price = StringUtils.GetDbDecimal(FormString.SafeQ("price", 200));
                //decimal _tradePrice = StringUtils.GetDbDecimal(FormString.SafeQ("tradePrice", 200));
                //decimal _marketPrice = StringUtils.GetDbDecimal(FormString.SafeQ("marketPrice", 200));
                //decimal _cost = StringUtils.GetDbDecimal(FormString.SafeQ("cost"));
                int _transfeetype = FormString.IntSafeQ("transfeetype");
                decimal _transfee = StringUtils.GetDbDecimal(FormString.SafeQ("transfee"));
                string _contentcms = FormString.SafeQNo("contentcms", 8000);
                string _picUrl = FormString.SafeQ("picUrl", 500);
                VWProductStyleEntity _entity = new VWProductStyleEntity();

                _entity.StyleId = _styleId;
                _entity.ClassId = _classId;
                _entity.AdTitle = _adTitle;
                _entity.Title = _title;
                //_entity.BrandCars = _brandCar;
                _entity.BrandId = _brandId;
                _entity.ContentCms = _contentcms;
                //_entity.Cost = _cost;
                _entity.DefaultProductId = 0;
                _entity.TransFee = _transfee;
                _entity.HasHtml = 0;
                _entity.TransFeeType = _transfeetype;
                //_entity.MarketPrice = _marketPrice;
                //_entity.Num = _num;
                //_entity.OECode = _oECode;

                //_entity.Price = _price;
                //_entity.SellerId = memid; 
                _entity.StylePicsStr = _stylePics;
                _entity.PicUrl = _picUrl;
                //_entity.StylePropertys = _stylePropertys;
                //_entity.TradePrice = _tradePrice;

                _styleId = ProductStyleBLL.Instance.AddProductStyleProc(_entity);
                if (_styleId > 0)
                {
                    return ((int)CommonStatus.Success).ToString();
                }

            }
            return ((int)CommonStatus.Fail).ToString();
        }



        #endregion

        #region 产品管理

        /// <summary>
        /// 产品展示
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductList()
        {
            int _flag = QueryString.IntSafeQ("flag");
            int _styleId = QueryString.IntSafeQ("styleId");
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _pagesize = CommonKey.PageSizeList;
            int _recordCount = 0;
            string _productName = QueryString.SafeQ("productName");
             
            IList<ProductEntity> entitylist = ProductBLL.Instance.GetProductList(_pagesize, _pageindex, ref _recordCount, _productName,"", _styleId);
            ViewBag.entitylist = entitylist;

            string _url = "/Store/ProductList?stylename=" + _productName;
            string pagestr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = pagestr;
            ViewBag.StyleId = _styleId;
            ViewBag.Flag = _flag;
            return View();
        }

        /// <summary>
        /// 产品编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductEdit()
        {
            //判断是否是修改
            int productid = QueryString.IntSafeQ("productid");
            int styleid = QueryString.IntSafeQ("styleid");
            int classid = 0;
            int brandid = 0;
            VWProductEntity _vwproductentity = new VWProductEntity();
            if (productid > 0)
            {
                _vwproductentity = ProductBLL.Instance.GetProductVW(productid);
                _vwproductentity.ProductPics = ProductStylePicsBLL.Instance.GetListPicsByProductId(productid);//获取样式图片列表
                _vwproductentity.ProductPropertys = ProductPropertyBLL.Instance.GetListByProductId(productid);//获取样式属性列表 
                _vwproductentity.ProductCarTypes = ProductCarTypeBLL.Instance.GetListByProductId(productid);//获取样式车型列表
                ProductBLL.Instance.Assignment(ref _vwproductentity, productid);


                classid = _vwproductentity.ClassId;
                brandid = _vwproductentity.BrandId;
                styleid = _vwproductentity.StyleId;
            }
            else if (styleid > 0)
            {
                VWProductStyleEntity _styleentity = ProductStyleBLL.Instance.GetProductStyle(styleid);
                classid = _styleentity.ClassId;
                brandid = _styleentity.BrandId;
            }
            else
            {
                classid = QueryString.IntSafeQ("classid");
                brandid = QueryString.IntSafeQ("brandid");
            }

            if (classid == 0 || brandid == 0) RedirectToAction("PClass");
            ClassesFoundEntity _classentity = ClassesFoundBLL.Instance.GetClassesFound(classid, true);
            BrandEntity _brandntity = BrandBLL.Instance.GetBrand(brandid, true);

            if (_classentity.IsEnd == 0) RedirectToAction("PClass");
            string classfullName = _classentity.FullName;

            ViewBag.ClassNamePath = classfullName;
            ViewBag.ClassId = classid;
            ViewBag.PropertiesClassId = _classentity.PropertiesClassId;
            ViewBag.BrandId = brandid;
            ViewBag.BrandName = _brandntity.Name;
            ViewBag.Product = _vwproductentity;
            ViewBag.MemId = memid;
            ViewBag.StyleId = styleid;
            return View();
        }


        /// <summary>
        /// 产品图片添加
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductPicsEdit()
        {
            int _productid = QueryString.IntSafeQ("productid");
            int _styleid = QueryString.IntSafeQ("styleid");
            ProductEntity _entity = ProductBLL.Instance.GetProduct(_productid);
            IList<ProductStylePicsEntity> _entityList = ProductStylePicsBLL.Instance.GetListPicsByProductId(_productid);
            ViewBag.ProductId = _productid;
            ViewBag.StyleId = _styleid;
            ViewBag.Entity = _entity;
            ViewBag.EntityList = _entityList;
            return View();

        }


        /// <summary>
        /// 产品图片上传
        /// </summary>
        /// <returns></returns>
        public int ProductPicSubmit()
        {
            int _styleId = FormString.IntSafeQ("styleId");
            int _productId = FormString.IntSafeQ("productId");
            string _productpicsstr = FormString.SafeQ("productpicsstr", 999999);
            return ProductStylePicsBLL.Instance.AddProcStylePics(_styleId, _productId, _productpicsstr, "");
        }

        /// <summary>
        /// 产品信息提交
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public string ProductEditSubmit()
        {
            int _memid = FormString.IntSafeQ("memid");
            if (_memid == memid)
            {
                int _productId = FormString.IntSafeQ("productId");
                int _styleId = FormString.IntSafeQ("styleId");
                int _classId = FormString.IntSafeQ("classId");
                string _adTitle = FormString.SafeQ("adTitle", 200);
                string _title = FormString.SafeQ("title", 200);
                string _oECode = FormString.SafeQ("oECode");
                string _brandId = FormString.SafeQ("brandId");
                int _cartyperelated = FormString.IntSafeQ("cartyperelated");
                string _productPropertysstr = FormString.SafeQ("productPropertysstr", 4000);
                string _CarTypeStr = FormString.SafeQ("CarTypeStr", 4000);
                string _productpicsstr = FormString.SafeQ("productpicsstr", 8000);
                int _num = FormString.IntSafeQ("num");
                decimal _price = StringUtils.GetDbDecimal(FormString.SafeQ("price", 200));
                decimal _tradePrice = StringUtils.GetDbDecimal(FormString.SafeQ("tradePrice", 200));
                decimal _marketPrice = StringUtils.GetDbDecimal(FormString.SafeQ("marketPrice", 200));
                decimal _cost = StringUtils.GetDbDecimal(FormString.SafeQ("cost"));
                int _transfeetype = FormString.IntSafeQ("transfeetype");
                decimal _transfee = StringUtils.GetDbDecimal(FormString.SafeQ("transfee"));
                string _contentcms = FormString.SafeQNo("contentcms", 8000);
                string _picUrl = FormString.SafeQ("picUrl", 500);
                ProcProductEntity _entity = new ProcProductEntity();

                _entity.StyleId = _styleId;
                _entity.ClassId = _classId;
                _entity.AdTitle = _adTitle;
                _entity.Title = _title;
                _entity.CarTypesStr = _CarTypeStr;
                _entity.BrandId = _brandId;
                _entity.ContentCms = _contentcms;
                _entity.Cost = _cost;
                _entity.ProductId = 0;
                _entity.TransFee = _transfee;
                _entity.HasHtml = 0;
                _entity.TransFeeType = _transfeetype;
                _entity.MarketPrice = _marketPrice;
                _entity.Num = _num;
                _entity.CarTypeRelated = _cartyperelated;
                _entity.OECode = _oECode;

                _entity.Price = _price;
                _entity.StyleId = _styleId;
                _entity.ProductPicsStr = _productpicsstr;
                _entity.PicUrl = _picUrl;
                _entity.ProductPropertysStr = _productPropertysstr;
                _entity.TradePrice = _tradePrice;

                _styleId = ProductBLL.Instance.AddProductProc(_entity);

                if (_styleId > 0)
                {
                    return ((int)CommonStatus.Success).ToString();
                }

            }
            return ((int)CommonStatus.Fail).ToString();
        }


        /// <summary>
        /// 上架或者下架产品
        /// </summary>
        /// <returns></returns>
        public int ChangeProductStatus()
        {
            int _id = FormString.IntSafeQ("id");
            int _status = FormString.IntSafeQ("status");
            if (_status > 1 || _status < 0 || _id < 1)
            {
                return 0;
            }

            return ProductBLL.Instance.ChangeProductStatus(_id, _status);
        }


        #endregion

        #region 改版产品发布


        /// <summary>
        /// 常规产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult ConventionalPList()
        {
            int _productType = QueryString.IntSafeQ("productType", 0);
            string _productName = QueryString.SafeQ("productName");

            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.ConventionalPListPageSize;
            int _recordCount = 0;

            IList<ConditionUnit> whereList = new List<ConditionUnit>();
            whereList.Add(new ConditionUnit { FieldName = SearchFieldName.ProductType, CompareValue = _productType.ToString() });
            whereList.Add(new ConditionUnit { FieldName = SearchFieldName.ProductName, CompareValue = _productName.ToString() });

            IList<VWConProductEntity> entityList = ProductDetailBLL.Instance.GetConProductList(_pageSize, _pageIndex, ref _recordCount, whereList);

            string _url = "/Store/ConventionalPList?productType=0&productName=" + _productName;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            ViewBag.EntityList = entityList;
            return View();
        }

        /// <summary>
        /// 特价产品列表
        /// </summary>
        /// <returns></returns>
        public ActionResult SpecialPList()
        {

            int _productType = QueryString.IntSafeQ("productType", 0);
            string _productName = QueryString.SafeQ("productName");

            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.SpecialPListPageSize;
            int _recordCount = 0;

            IList<ConditionUnit> whereList = new List<ConditionUnit>();
            whereList.Add(new ConditionUnit { FieldName = SearchFieldName.ProductType, CompareValue = _productType.ToString() });
            whereList.Add(new ConditionUnit { FieldName = SearchFieldName.ProductName, CompareValue = _productName.ToString() });

            IList<VWSpeProductEntity> entityList = ProductDetailBLL.Instance.GetSpeProductList(_pageSize, _pageIndex, ref _recordCount, whereList);

            string _url = "/Store/SpecialPList?productType=1&productName=" + _productName;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            ViewBag.EntityList = entityList;
            return View();
        }


        /// <summary>
        /// 发布常规或者特价产品
        /// </summary>
        /// <returns></returns>
        public ActionResult ReleaseProduct()
        {
            int _ptype = QueryString.IntSafeQ("ptype");
            int _pdid = QueryString.IntSafeQ("pdid");
            int _pid = QueryString.IntSafeQ("pid");
            ProductDetailEntity detailEntity = new ProductDetailEntity();
            if (_pdid > 0)
            {
                detailEntity = ProductDetailBLL.Instance.GetProductDetail(_pdid);
                ViewBag.ProductId = detailEntity.ProductId;
            }
            else if (_pid > 0)
            {
                ViewBag.ProductId = _pid;
                detailEntity = ProductDetailBLL.Instance.GetProductDetailByPId(_pid, _ptype);
            }
            ViewBag.DetailEntity = detailEntity;
            ViewBag.ProductType = _ptype;
            return View();
        }

        /// <summary>
        /// 产品信息提交
        /// </summary>
        /// <returns></returns>
        public int ProductDetailEdit()
        {

            int _productId = FormString.IntSafeQ("productId");
            int _productDetailId = FormString.IntSafeQ("productDetailId");
            int _productType = FormString.IntSafeQ("productType");
            int _status = FormString.IntSafeQ("status");
            int _saleNum = FormString.IntSafeQ("saleNum");
            decimal _tradePrice = FormString.DecimalSafeQ("tradePrice");

            int _stockNum = FormString.IntSafeQ("stockNum");

            ProductDetailEntity entity = new ProductDetailEntity();

            entity.ProductId = _productId;
            entity.Id = _productDetailId;
            entity.ProductType = _productType;
            entity.Status = _status;
            entity.SaleNum = _saleNum;
            entity.TradePrice = _tradePrice;

            entity.StockNum = _stockNum;
            //if (_productType == (int)ProductType.SpecialPrice)
            //{
            //    entity.StockNum = _stockNum;
            //}
            //else if (_productType == (int)ProductType.Normal)
            //{
            //    entity.StockNum = 999999;
            //}
            int _result = ProductDetailBLL.Instance.AddProductDetail(entity);

            return _result;

        }


        /// <summary>
        /// 上架或者下架产品
        /// </summary>
        /// <returns></returns>
        public int OnorOffShelf()
        {
            int _id = FormString.IntSafeQ("id");
            int _status = FormString.IntSafeQ("status");
            if (_id < 1 || _status > 1 || _status < 0)
            {
                return 0;
            }

            int _result = ProductDetailBLL.Instance.UpdatePDetailStatus(_id, _status);

            return _result;
        }



        /// <summary>
        /// 车型管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductCarTypeManage()
        {
            int _pid = QueryString.IntSafeQ("pid");
            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.PCarTypeManagePageSize;
            int _recordCount = 0;
            IList<ConditionUnit> _whereList = new List<ConditionUnit>();
            _whereList.Add(new ConditionUnit() { FieldName = SearchFieldName.ProductId, CompareValue = _pid.ToString() });

            IList<ProductCarTypeEntity> _entityList = ProductCarTypeBLL.Instance.GetProductCarTypeList(_pageSize, _pageIndex, ref _recordCount, _whereList);
            string _url = "/Store/ProductCarTypeManage?pid=" + _pid;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            ViewBag.EntityList = _entityList;
            ViewBag.PId = _pid;
            ViewBag.ProductName = ProductBLL.Instance.GetProduct(_pid).Name;

            return View();
        }
        /// <summary>
        /// 车型管理，更新后
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductCarTypeModel()
        {
            int _pid = QueryString.IntSafeQ("pid");
            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.PCarTypeManagePageSize;
            int _recordCount = 0; 

            IList<ProductCarTypeModelEntity> _entityList = ProductCarTypeModelBLL.Instance.GetProductCarTypeModelList(_pageSize, _pageIndex, ref _recordCount, _pid);
            string _url = "/Store/ProductCarTypeManage?pid=" + _pid;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            ViewBag.EntityList = _entityList;
            ViewBag.PId = _pid;
            ViewBag.ProductName = ProductBLL.Instance.GetProduct(_pid).Name;
            return View();
        }
        /// <summary>
        /// 车型添加
        /// </summary>
        /// <returns></returns>
        public string ProductCarTypeAdd()
        {

            int _pid = FormString.IntSafeQ("_pid");
            int _cartype1 = FormString.IntSafeQ("_cartype1");
            int _cartype2 = FormString.IntSafeQ("_cartype2");
            int _cartype3 = FormString.IntSafeQ("_cartype3");
            int _cartype4 = FormString.IntSafeQ("_cartype4");

            ProductCarTypeEntity _entity = new ProductCarTypeEntity();
            _entity.ProductId = _pid;
            _entity.CarType1 = _cartype1;
            _entity.CarType2 = _cartype2;
            _entity.CarType3 = _cartype3;
            _entity.CarType4 = _cartype4;

            int _result = ProductCarTypeBLL.Instance.ExecProcProductCarTypeAdd(_entity);
            _entity = ProductCarTypeBLL.Instance.GetProductCarType(_result);
            return JsonJC.ObjectToJson(_entity);

        }


        /// <summary>
        /// 删除车型
        /// </summary>
        /// <returns></returns>
        public int DeletePCarType()
        {
            int _id = FormString.IntSafeQ("id");
            return ProductCarTypeBLL.Instance.DeleteProductCarTypeByKey(_id);
        }


        #endregion


        /// <summary>
        /// 产品详情图片编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult ProductDetailPicsEdit()
        {
            int _pid = QueryString.IntSafeQ("productid", 0);
            ProductExtEntity entity = ProductExtBLL.Instance.GetProductExtByProductId(_pid);
            ProductEntity productEntity = ProductBLL.Instance.GetProduct(_pid);
            ViewBag.Entity = entity;
            ViewBag.ProductEntity = productEntity;
            return View();
        }


        [ValidateInput(false)]
        /// <summary>
        /// 产品详情图片添加
        /// </summary>
        /// <returns></returns>
        public int ProductDetailPicsAdd()
        {
            int _productExtId = FormString.IntSafeQ("productExtId");
            int _pid = FormString.IntSafeQ("pid");
            string _detailDes = FormString.SafeQNo("detailDes",1000000); 
             ProductExtEntity entity = new ProductExtEntity();
            entity.Id = _productExtId;
            entity.ProductId = _pid;
            entity.DetailDescrip = _detailDes;
            return ProductExtBLL.Instance.AddProductExt(entity); 
        }


    }
}
