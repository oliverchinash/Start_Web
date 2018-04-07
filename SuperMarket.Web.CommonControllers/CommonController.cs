using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.HelpDB;
using SuperMarket.BLL.JcOrderInquiry;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Core.WebService;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ThoughtWorks.QRCode.Codec;

namespace SuperMarket.Web.CommonControllers
{
    public class CommonController : Controller
    {
        public string GetProvinceJson()
        {
            return GYProvinceBLL.Instance.GetListGYProvinceJson();

        }
        public string GetCityJson()
        {
            string pcode = FormString.SafeQ("pcode");
            return GYCityBLL.Instance.GetGYCityJsonByPid(pcode);

        }
        public string GetClassFoundBasic()
        {
            int _level = -1;
            int _pid = FormString.IntSafeQ("pid", 0);
            int siteid = -1;
            int _classmenutype = (int)ClassMenuTypeEnum.Default;
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassListByLevel(_pid, _level, siteid, _classmenutype);
            foreach(ClassesFoundEntity entity in list)
            {
                entity.Code = entity.Id.ToString() ;
            }
            string liststr = JsonJC.ObjectToJson(list);
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
            int siteid = FormString.IntSafeQ("siteid",-1);
            int _classmenutype = FormString.IntSafeQ("menutype",-1);
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassListByLevel(_pid, _level, siteid, _classmenutype);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }
        /// <summary>
        /// 根据分类Id 和级别获取子分类
        /// </summary>
        /// <returns></returns>
        public string GetClassFoundByParent()
        { 
            int _pid = FormString.IntSafeQ("pid", -1); 
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassesAllByPId(_pid, true, -1, -1);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }
        ///<summary>
        /// 根据styleid获取图片路径列表
        /// <summary>
        /// <returns></returns>
        public string GetStylePicsById()
        {
            int _styleid = FormString.IntSafeQ("styleid");
            IList<ProductStylePicsEntity> list = new List<ProductStylePicsEntity>();
            list = ProductStylePicsBLL.Instance.GetListByStyleId(_styleid);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }
        ///<summary> 
        /// 根据分类Id  获取对应分类的品牌列表
        /// </summary>
        /// <returns></returns>
        public string GetBrandByClass()
        {
            int _classid = FormString.IntSafeQ("classid");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<BrandEntity> list = new List<BrandEntity>();
            list = ClassBrandBLL.Instance.GetBrandByClass(_classid, _pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Code = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        ///<summary> 
        /// 根据分类Id  获取对应分类的品牌列表
        /// </summary>
        /// <returns></returns>
        public string GetBrandByKey()
        {
            int _classid = FormString.IntSafeQ("classid");
            int _pid = FormString.IntSafeQ("pid");
            string _key = FormString.SafeQ("key");
            IList<BrandEntity> list = new List<BrandEntity>();


            list = BrandBLL.Instance.GetBrandByKey(_classid, _pid, _key);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }
        public string GetDicListByParent()
        {
            int _pid = FormString.IntSafeQ("pid");
            int _classid = FormString.IntSafeQ("classid");
            IList<DicInquiryOrderEntity> list = new List<DicInquiryOrderEntity>();
            list = DicInquiryOrderBLL.Instance.GetDicInquiryOrderShow(_pid, _classid);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }
        public string GetDicProductSubDetail()
        {
            int _producttypeid = FormString.IntSafeQ("producttypeid"); 
            IList<DicInquiryOrderEntity> list = new List<DicInquiryOrderEntity>();
            list = DicInquiryOrderBLL.Instance.GetDicFromCode(_producttypeid.ToString(), InquiryDicMenuCode.InquiryProductTypeEnum,false);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;
        }

        
        /// <summary>
        /// 获取分类
        /// </summary>
        /// <returns></returns>
        public string GetClassBySiteId()
        {
            int _siteid = FormString.IntSafeQ("siteid");
            int _parentid = FormString.IntSafeQ("pid");//上级 ，0代表第一级
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassesAllByPId( _parentid,false, _siteid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Name = p.Name, 
                         IsEnd = p.IsEnd,
                         Sort = p.Sort
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
            int _parentid = FormString.IntSafeQ("pid");//上级 ，0代表第一级
            IList<BasicSitePropertiesEntity> list = new List<BasicSitePropertiesEntity>();
            list = BasicSitePropertiesBLL.Instance.GetListBySiteId(_siteid, _parentid);
            var listfilter = list.Select(
                     p => new
                     {
                         Id = p.Id,
                         Code = p.Code,
                         Name = p.Name, 
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
            IList<BasicSiteProDetailsEntity> list = new List<BasicSiteProDetailsEntity>();
            list = BasicSiteProDetailsBLL.Instance.GetListByPropertyId(_propertyId, _pid);
            //list = ComPropertyDetailsBLL.Instance.GetListByPropertyId(_propertyId, _pid);
            var listfilter = list.Select(
                     p => new
                     {
                         Code = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        ///// <summary>
        ///// 根据分类得到对应规格
        ///// </summary>
        ///// <returns></returns>
        //public string GetSpecByClass()
        //{
        //    int _classid = FormString.IntSafeQ("classid");
        //    int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
        //    IList<VWClassSpecEntity> list = new List<VWClassSpecEntity>();
        //    list = ClassSpecBLL.Instance.GetListByClass(_classid, _pid);
        //    var listfilter = list.Select(
        //             p => new
        //             {
        //                 Id = p.SpecId,
        //                 Name = p.Name,
        //                 IsEnd = p.IsEnd
        //             });
        //    string liststr = JsonJC.ObjectToJson(listfilter);
        //    return liststr;
        //}
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
        public string GetBrandCarList()
        {
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
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

        public string GetProsNeedShow()
        {
            //int _styleid = FormString.IntSafeQ("styleid");//上级品牌Id，0代表第一级
            //List<Prod>
            return "";
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
        public string GetSpecsShowByStyle()
        {
            int _styleid = FormString.IntSafeQ("styleid");
            //IList<VWBasicSitePropertiesEntity> _list = BasicSitePropertiesBLL.Instance.GetSpecsByClass(_styleid, _pid);
            IList<VWBasicSitePropertiesEntity> _list = null;// ProductStyleProBLL.Instance.GetSpecsByStyle(_styleid);
            string liststr = JsonJC.ObjectToJson(_list);
            return liststr;
        }
       
      
        /// <summary>
        /// 获取产品详情
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
        /// 获取产品详情
        /// </summary>
        /// <returns></returns>
        public string GetCommentByStyle()
        {
            int _styleId = FormString.IntSafeQ("styleid");
            int _pageindex = FormString.IntSafeQ("pageindex");
            int _pagesize = CommonKey.PageSizeCommentShow;
            int _recordcount = 0;
            IList<CommentEntity> _list = new List<CommentEntity>();
            IList<ConditionUnit> where = new List<ConditionUnit>();
            where.Add(new ConditionUnit { FieldName = "StyleId", CompareValue = _styleId.ToString() });
            _list = CommentBLL.Instance.GetCommentList(_pagesize, _pageindex, ref _recordcount, where);
            ListObj listobj = new ListObj();
            listobj.Total = _recordcount;
            listobj.List = _list;
            string liststr = JsonJC.ObjectToJson(listobj);
            return liststr;
        }
        public string GetHotProductByClass()
        {
            int _classid = FormString.IntSafeQ("classid");
            IList<VWProductEntity> _list = new List<VWProductEntity>();

            _list = ProductSimilarHotBLL.Instance.GetHotProducts(_classid);
            if (_list == null || _list.Count == 0) _list = ProductSimilarHotBLL.Instance.GetHotProducts(0);
            string liststr = JsonJC.ObjectToJson(_list);
            return liststr;
        }
        public string GetUnitNameList()
        {
            IList<DicUnitEnumEntity> list = DicUnitEnumBLL.Instance.GetDicUnitEnumAll();
            return JsonJC.ObjectToJson(list);
            
        }
        public string GetMemId()
        {
            string callback = QueryString.SafeQ("callback");//jsonp回调函数 
            string result = "";
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                result = member.MemId.ToString();
            }
            return result;
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
                         Name = p.Name,
                         Year = p.Year,
                         IsHot = p.IsHot,
                         FirstPY = p.FirstPY,
                         ClassType = p.ClassType,
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }


        #region 精选产品
        public string GetProductFine()
        {
            int _finetype = FormString.IntSafeQ("ft", (int)ProductFineTypeEnum.MobileHome);//精选类型
            int _pageindex = FormString.IntSafeQ("pageindex");
            int _pagesize = CommonKey.PageSizeList;
            int total = 0;
            IList<VWProductFineEntity> list = ProductFineBLL.Instance.GetProductFineList(_pagesize, _pagesize, ref total, _finetype);
            ListObj listobj = new ListObj();
            listobj.Total = total;
            listobj.List = list;
            string liststr = JsonJC.ObjectToJson(listobj);
            return liststr;
        }
        #endregion

        #region 获取车型数据
        /// <summary>
        /// 获取车系
        /// </summary>
        /// <returns></returns>
        public string GetSeriesByBrandId()
        {
            int _brandid = FormString.IntSafeQ("brandid");
            int _subbrandid = FormString.IntSafeQ("subbrandid");
        if (_subbrandid > 0)
            {
                IList<VWTreeCTSeriesEntity> list = CarTypeSeriesBLL.Instance.GetTreeCTSeriesBySubBrand(_subbrandid);
                string liststr = JsonJC.ObjectToJson(list);
                return liststr;
            }
         else   if (_brandid > 0)
            {
                IList<VWTreeCTSeriesEntity> list = CarTypeSeriesBLL.Instance.GetTreeCTSeries(_brandid);
                string liststr = JsonJC.ObjectToJson(list);
                return liststr;
            }
            
                return "";
        }
        /// <summary>
        ///获取车型，按照车系
        /// </summary>
        /// <returns></returns>
        public string GetModelBySeriesId()
        {
            int _seriesid = FormString.IntSafeQ("seriesid");
            if (_seriesid > 0)
            {
                VWTreeCTModelEntity list = CarTypeModelBLL.Instance.GetTreeCTModel(_seriesid);
                string liststr = JsonJC.ObjectToJson(list);
                return liststr;
            }
            return "";
        }
        public string GetNextStore()
        {
            int _preid = FormString.IntSafeQ("preid");
            VWShowStoreEntity list = StoreBLL.Instance.GetNextStore(_preid);
            string liststr = JsonJC.ObjectToJson(list);
            return liststr;

        }
        public string GetMsg()
        {
            string msg = System.Web.HttpContext.Current.Request.UserAgent.ToLower();
            return msg;
        }
        public string GetCJAmount()
        {
            ResultObj result = new ResultObj();
            StatisticTradeEntity entity = StatisticTradeBLL.Instance.GetStatisticTradeToday();
            if (entity != null && entity.Id > 0)
            {
                result.Obj = entity;
            }
            result.Status = (int)CommonStatus.Success;
            return JsonJC.ObjectToJson(result);
        }
        //public string GetWeiXinAccessToken()
        //{ 
        //    return WeiXinBLL.GetWeiXinAccessToken(); 
        //}
        //public string GetWeiXinJsticket()
        //{ 
        //    return WeiXinBLL.GetWeiXinJsticket(); 
        //}
        //public string GetWeiXinGetSignature()
        //{
        //    string ticket = FormString.SafeQ("ticket", 200);
        //    string noncestr = FormString.SafeQ("noncestr", 200);
        //    long times = FormString.LongIntSafeQ("timestamp");
        //    string url = FormString.SafeQ("url",200 );
        //    return WeiXinBLL.GetSignature(ticket,   noncestr, times,   url);
        //}

        //public string GetWeiXinAPPId()
        //{
        //    return WeiXinBLL.APPId;
        //}

        #endregion


        #region 询价
        public string GetXunJiaMsg()
        {
            ResultObj result = new ResultObj();
            int _pdid = FormString.IntSafeQ("pdid");
            MemberLoginEntity member = CookieBLL.GetLoginCookie();
            if (member != null && member.MemId > 0)
            {
                VWXunJiaObj _obj = new VWXunJiaObj();
                VWProductEntity vwp = ProductBLL.Instance.GetProVWByDetailId(_pdid);
                VWProductNomalParamEntity vwpa = ProductExtBLL.Instance.GetProductNormalParamById(vwp.ProductId);
                MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(member.MemId);
                _obj.Product = vwp;
                _obj.ProductExt = vwpa;
                _obj.MobilePhone = store.MobilePhone;
                _obj.MemId = store.MemId;
                result.Status = (int)CommonStatus.Success;
                result.Obj = _obj;
            }
            else
            {
                result.Status = (int)CommonStatus.NeedLogin;
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }
        public string CreateInquiry()
        {
            ResultObj result = new ResultObj();
            int _pdid = FormString.IntSafeQ("pdid");
            string memphone = FormString.SafeQ("memphone");
            string remark = FormString.SafeQ("remark",500);
            InquiryRecordsEntity inquiry = new InquiryRecordsEntity();
            inquiry.MobilePhone = memphone;
            inquiry.Remark = remark;
            inquiry.ProductDetailId = _pdid;
            inquiry.CreatTime = DateTime.Now;  
            inquiry.Status = (int)InquiryStatus.WaitDeal; 
            int inid=  InquiryRecordsBLL.Instance.AddInquiryRecords(inquiry);
            if(inid>0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = inid;
            }
            else
            {
                result.Status = (int)CommonStatus.Fail; 
            }
            string liststr = JsonJC.ObjectToJson(result);
            return liststr;
        }

        /// <summary>
        /// 根据询价订单产品明细查找
        /// </summary>
        /// <returns></returns>
        public string GetProductAndSub()
        {
            ResultObj result = new ResultObj();
            string _code = FormString.SafeQ("code");
            int _orderproid = FormString.IntSafeQ("orderproid");
            VWInquiryProductEntity vwproduct = InquiryProductBLL.Instance.GetVWInquiryProduct(_orderproid, false);
            if (vwproduct != null && vwproduct.Id > 0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = vwproduct;
            }
            else
            {
                result.Status = (int)CommonStatus.Fail;
                result.Obj = _orderproid;
            }
            return JsonJC.ObjectToJson(result);
        }

        /// <summary>
        /// 根据拼音获取产品数据
        /// </summary>
        /// <returns></returns>
        public string GetProductByPY()
        {
            ResultObj result = new ResultObj();
            string _py = FormString.SafeQ("py");
            int _scopetype = FormString.IntSafeQ("scopetype");
            int _scopeid = FormString.IntSafeQ("scopeid");
            IList<PartsProductFoundEntity> list = PartsProductFoundBLL.Instance.GetPartsProductFoundShow(_py, _scopetype, _scopeid ,true);

            result.Status = (int)CommonStatus.Success;
            result.Obj = list;
            return JsonJC.ObjectToJson(result);

        }

        public void GetQRCodeImage()
        {
            if (!string.IsNullOrEmpty(Request.QueryString["data"]))
            {
                string str = Request.QueryString["data"];

                //初始化二维码生成工具
                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                qrCodeEncoder.QRCodeVersion = 0;
                qrCodeEncoder.QRCodeScale = 4;

                //将字符串生成二维码图片
                Bitmap image = qrCodeEncoder.Encode(str, Encoding.Default);

                //保存为PNG到内存流  
                //MemoryStream ms = new MemoryStream();
                image.Save(Response.OutputStream, ImageFormat.Png);

                //输出二维码图片
                //Response.BinaryWrite(ms.GetBuffer());
                Response.End();
            } 
        }
        #endregion
    }
}
