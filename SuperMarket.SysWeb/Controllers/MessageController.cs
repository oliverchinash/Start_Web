using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
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

namespace SuperMarket.SysWeb.Controllers
{
    public class MessageController : BaseMemAdminController
    {

        /// <summary>
        /// 待发送短信队列
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMsgList()
        {
            int _pagesize = CommonKey.PageSizeOrder;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            if (_pageindex == 0) _pageindex = 1;
            int _recordCount = 0;

            int _systemType = QueryString.IntSafeQ("systemType", 0);
            int _status = QueryString.IntSafeQ("status", -1);

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SystemType, CompareValue = _systemType.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.SmsNoticeStatus, CompareValue = _status.ToString() });
            IList<SMSNoticeEntity> _entitylist = SMSNoticeBLL.Instance.GetSMSNoticeList(_pagesize, _pageindex, ref _recordCount, _wherelist);

            string _url = "/Message/SendMsgList/?systemType=" + _systemType + "&status=" + _status;
            string _pageStr = HTMLPage.SetOrderListPage(_recordCount, _pagesize, _pageindex, _url);

            ViewBag.PageStr = _pageStr;
            ViewBag.entitylist = _entitylist;
            return View();
        }

        /// <summary>
        /// 编辑待发送短信
        /// </summary>
        /// <returns></returns>
        public ActionResult SendMsgEdit()
        {
            int memid = QueryString.IntSafeQ("memid", 0);
            int _id = QueryString.IntSafeQ("id", 0);
            string _SMSContent = QueryString.SafeQ("SMSContent",0,2000);
            VWMemberEntity _entity = MemberBLL.Instance.GetVWMember(memid);
            SMSNoticeEntity entity = SMSNoticeBLL.Instance.GetSMSNotice(_id);

            ViewBag.entity = entity;
            ViewBag.MobilePhone = _entity.MobilePhone;
            ViewBag.SMSContent = _SMSContent;
            return View();
        }


        /// <summary>
        /// 添加待发送短信
        /// </summary>
        /// <returns></returns>
        public int AddSendMsg()
        {
            string _mobilePhone = FormString.SafeQ("mobilePhone");
            string _SMSContent = FormString.SafeQ("SMSContent");
            int _sendStatus = FormString.IntSafeQ("sendStatus");
            int _systemType = FormString.IntSafeQ("systemType");

            SMSNoticeEntity entity = new SMSNoticeEntity();
            entity.MobilePhone = _mobilePhone;
            entity.SMSContent = _SMSContent;
            entity.Status = _sendStatus;
            entity.SystemType = _systemType;
            entity.SendTime = DateTime.Now.AddDays(10);
            entity.CreateTime = DateTime.Now;

            return SMSNoticeBLL.Instance.AddSMSNotice(entity);

        }


        /// <summary>
        /// 修改待发送短信
        /// </summary>
        /// <returns></returns>
        public int UpdateSendMsg()
        {
            int _id = FormString.IntSafeQ("id");
            string _mobilePhone = FormString.SafeQ("mobilePhone");
            string _SMSContent = FormString.SafeQ("SMSContent");
            int _sendStatus = FormString.IntSafeQ("sendStatus");
            int _systemType = FormString.IntSafeQ("systemType");

            SMSNoticeEntity entity = SMSNoticeBLL.Instance.GetSMSNotice(_id);
            entity.MobilePhone = _mobilePhone;
            entity.SMSContent = _SMSContent;
            entity.Status = _sendStatus;
            entity.SystemType = _systemType;
            entity.CreateTime = DateTime.Now;
            entity.SendTime = DateTime.Now.AddDays(30); 

            return SMSNoticeBLL.Instance.UpdateSMSNotice(entity);

        }



        /// <summary>
        /// 删除待发送短信
        /// </summary>
        /// <returns></returns>
        public int DelSendMsg()
        {
            int _id = FormString.IntSafeQ("id");
            return SMSNoticeBLL.Instance.DeleteSMSNoticeByKey(_id);
        }

    }
}
