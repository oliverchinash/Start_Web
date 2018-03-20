using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.LoginWebControllers
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
        /// <summary>
        /// 根据分类Id 和级别获取子分类
        /// </summary>
        /// <returns></returns>
        public string GetClassFoundLevel()
        {
            int _level = FormString.IntSafeQ("level",-1);
            int _pid = FormString.IntSafeQ("pid",-1);
            int siteid = FormString.IntSafeQ("siteid",-1);
            int _classmenutype = FormString.IntSafeQ("menutype",-1);
            IList<ClassesFoundEntity> list = new List<ClassesFoundEntity>();
            list = ClassesFoundBLL.Instance.GetClassListByLevel(_pid, _level, siteid, _classmenutype);
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
                         Code = p.Id,
                         Name = p.Name
                     });
            string liststr = JsonJC.ObjectToJson(listfilter);
            return liststr;
        }

        /// <summary>
        /// 根据分类得到对应属性
        /// </summary>
        /// <returns></returns>
        public string GetPropertyByClass()
        {
            int _classid = FormString.IntSafeQ("classid");
            int _parentid = FormString.IntSafeQ("pid");//上级 ，0代表第一级
            IList<ClassPropertiesEntity> list = new List<ClassPropertiesEntity>();
            list = ClassPropertiesBLL.Instance.GetListByClassId(_classid, _parentid);
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
        public string GetPropertyValueList()
        {
            int _propertyId = FormString.IntSafeQ("propertyId");
            int _pid = FormString.IntSafeQ("pid");//上级品牌Id，0代表第一级
            IList<ComPropertyDetailsEntity> list = new List<ComPropertyDetailsEntity>();
            list = ComPropertyDetailsBLL.Instance.GetListByPropertyId(_propertyId, _pid);
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

        public string GetSpecsShowByStyle()
        {
            int _styleid = FormString.IntSafeQ("styleid");
            //IList<VWClassPropertiesEntity> _list = ClassPropertiesBLL.Instance.GetSpecsByClass(_styleid, _pid);
            IList<VWClassPropertiesEntity> _list = null;// ProductStyleProBLL.Instance.GetSpecsByStyle(_styleid);
            string liststr = JsonJC.ObjectToJson(_list);
            return liststr;
        }
        public string GetSpecValues()
        {
            int _hasproduct = FormString.IntSafeQ("hasproduct");
            int _classid = FormString.IntSafeQ("classid");
            int _styleid = FormString.IntSafeQ("styleid");
            int _brandid = FormString.IntSafeQ("brandid");
            IList<ClassProDetailsEntity> _list = null;
            int _propertyid = FormString.IntSafeQ("propertyid");
            if (_hasproduct == 1)
            {
                //_list= ProductSpecBLL.Instance.GetSpecValsByStyle(_styleid, _propertyid);
            }
            else
            {
                //_list= ProductStyleProBLL.Instance.GetSpecValsByBrand(_brandid, _classid, _propertyid);
            }
            //if (_hasproduct == 1)
            //{
            //    _productids = FormString.SafeQ("productids");
            //    if (!string.IsNullOrEmpty(_productids))
            //    {
            //        ids = ids + "," + _productids;
            //    }
            //    _list = ProductSpecBLL.Instance.GetSpecValsByProducts(ids, _propertyid);
            //}
            //else
            //{
            //    _styleids = FormString.SafeQ("styleids");
            //    if (!string.IsNullOrEmpty(_styleids))
            //    {
            //        ids = ids + "," + _styleids;
            //    }
            //    _list = ProductStyleProBLL.Instance.GetSpecValsByStyles(ids, _propertyid);
            //} 
            string liststr = JsonJC.ObjectToJson(_list);
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
            if (_brandid > 0)
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
                //VWProductNomalParamEntity vwpa = ProductExtBLL.Instance.GetProductNormalParamById(vwp.ProductId);
                MemStoreEntity store = StoreBLL.Instance.GetStoreByMemId(member.MemId);
                _obj.Product = vwp;
                //_obj.ProductExt = vwpa;
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
        #endregion
    }
}
