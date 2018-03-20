using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Cookie;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.MemberControllers
{
    public class DiscussController: BaseCommonController
    {

        public string GetStyleIdByProperties()
        {
            Hashtable _hashresult = new Hashtable();
            int _productid = QueryString.IntSafeQ("p");
            int _pageindex = QueryString.IntSafeQ("pi");
            if (_pageindex == 0) _pageindex = 1;
            int pagesize = CommonKey.PageSizeCommentShow;
            int _record = 0;
            IList<SuperMarket.Model.CommentEntity> _list = CommentBLL.Instance.GetCommentListByProductId(pagesize, 1, ref _record, _productid);
            var listfilter = _list.Select(
                  p => new
                  {
                      Days = ((TimeSpan)(p.CreateTime - p.OrderDate)).Days,
                      CreateTime = p.CreateTime,
                      Title = p.ProductName,
                      CommentContent = p.CommentContent,
                      PicUrlContent = p.PicUrlContent,
                      MemCode= p.MemCode,
                      MemLevelName = p.MemLevelName,
                  });
            _hashresult.Add("ListObj", listfilter);
            _hashresult.Add("PageStr", SuperMarket.Core.HTMLPage.GetDiscusssListDivPage(_record, SuperMarket.Model.CommonKey.PageSizeCommentShow, _pageindex, "/DisCuss/GetStyleIdByProperties?p" + _productid));
            string liststr = JsonJC.ObjectToJson(_hashresult);
            return liststr;
        }
         
    }
}
