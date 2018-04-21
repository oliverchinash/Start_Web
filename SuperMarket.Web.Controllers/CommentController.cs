using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.ShoppingDB;
using SuperMarket.Core;
using SuperMarket.Core.Enums;
using SuperMarket.Core.Json;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Util;
using SuperMarket.Model;
using SuperMarket.Model.Account;
using SuperMarket.Model.Common;
using SuperMarket.Web.CommonControllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.Controllers
{
    public class CommentController:BaseMemberController
    {
        public ActionResult ProductEvaluation()
        {
            int _ordercode = QueryString.IntSafeQ("ordercode");
            int _productStyleId = QueryString.IntSafeQ("productstyleid");
            int _orderdetailid = QueryString.IntSafeQ("orderdetailid");
            VWProductStyleEntity _entity = ProductStyleBLL.Instance.GetProductStyle(_productStyleId);
            ViewBag.entity = _entity;
            ViewBag.OrderCode = _ordercode;
            ViewBag.MemId = memid;
            ViewBag.orderdetailid = _orderdetailid;
            return View();
        }

        public int SubmitEvaluation()
        {
            Int64 _ordercode = FormString.LongIntSafeQ("ordercode");
            int _orderdetailid = FormString.IntSafeQ("orderdetailid"); 
            string _title = FormString.SafeQ("title");
            int _servicestar = FormString.IntSafeQ("servicestar");
            string _commentContent = FormString.SafeQ("commentContent");
            int _memberId = FormString.IntSafeQ("memberId");
            DateTime DateTimeNow = DateTime.Now;
            CommentEntity _entity = new CommentEntity();        
            _entity.ProductId = 1; 
            _entity.OrderCode = _ordercode;
            _entity.OrderDetailId = _orderdetailid;
            _entity.ProductName = _title;
            _entity.ProductStar = 5;
            _entity.SeviceStar = _servicestar;
            _entity.TrafficStar = 5;
            _entity.CommentContent = _commentContent;
            _entity.ClientType = 1;
            _entity.HasPic = 0;
            _entity.IpAddress = "127.0.0.1";
            _entity.MemId = _memberId;
            _entity.CreateTime = DateTimeNow;
            _entity.Status = 0;
            _entity.CheckManId = 1;
            _entity.CheckTime = DateTimeNow;
            _entity.CheckManId = 5;
            _entity.ReplyContent = "";

            OrderDetailEntity _orderentity = OrderDetailBLL.Instance.GetOrderDetail(_orderdetailid);
            _orderentity.HasComment = 1;
            OrderDetailBLL.Instance.UpdateOrderDetail(_orderentity);
          
            int status = CommentBLL.Instance.AddComment(_entity);


            //CommonStatus cs = (CommonStatus)status;
            //return SuperMarket.Core.Enums.EnumShow.Instance.GetEnumDes(cs);
            return status;
        }



    }
}
