using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SuperMarket.Model;
using SuperMarket.BLL;
using SuperMarket.Core.Safe;
using SuperMarket.Core.Json;
using SuperMarket.Model.Common;
using SuperMarket.Model.Const;
using System.Security.Cryptography;
using SuperMarket.Core;
using SuperMarket.Model.Basic.VW.Member;
using System.Collections;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.Web.CommonControllers;

namespace SuperMarket.SysWeb.Controllers
{
    public class CheckController : BaseMemAdminController
    {

        #region  企业用户审核
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 企业审核
        /// </summary>
        /// <returns></returns>                 
        public ActionResult CheckSupplier()
        {
            string _legalName = QueryString.SafeQ("legalName");
            string _companyName = QueryString.SafeQ("companyName");
            string _memcode = QueryString.SafeQ("memcode");
            string _contractphone = QueryString.SafeQ("contractphone");
            string _contractname = QueryString.SafeQ("contractname");
            int _checkStatus = QueryString.IntSafeQ("checkStatus", -1);

            int _pagesize = CommonKey.PageSizeCheck;
            int _pageindex = QueryString.IntSafeQ("pageindex", 1);
            int _recordCount = 0;

            IList<ConditionUnit> _wherelist = new List<ConditionUnit>();
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.LegalName, CompareValue = _legalName });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.CompanyName, CompareValue = _companyName });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.CheckStatus, CompareValue = _checkStatus.ToString() });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.MemCode, CompareValue = _memcode });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ContractName, CompareValue = _contractname });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.ContractPhone, CompareValue = _contractphone });
            _wherelist.Add(new ConditionUnit { FieldName = SearchFieldName.IsSupplier, CompareValue =0 });

            IList<MemStoreEntity> _entitylist = StoreBLL.Instance.GetStoreList(_pagesize, _pageindex, ref _recordCount, _wherelist);
            ViewBag.entitylist = _entitylist;

            string _url = "/Check/CheckSupplier?memcode="+ _memcode + "&contractname="+ _contractname + "&contractphone=" + _contractphone + "&legalName=" + _legalName + "&companyName=" + _companyName + "&checkStatus=" + _checkStatus;
            string _pagestr = HTMLPage.SetProductListPage(_recordCount, _pagesize, _pageindex, _url);
            ViewBag.PageStr = _pagestr;

            return View();
        }

        /// <summary>
        /// 企业申请信息详情
        /// </summary>
        /// <returns></returns>
        public ActionResult SupplierDetail()
        {
            int _memid = QueryString.IntSafeQ("MemId", 0);
            MemberEntity _mementity = MemberBLL.Instance.GetMember(_memid);
            MemberInfoEntity _meminfoentity = MemberInfoBLL.Instance.GetMemberInfoByMemId(_memid);
            MemBillVATEntity _membill = MemBillVATBLL.Instance.GetMemBillVATByMemId(_memid);
            MemStoreEntity _storeentity = StoreBLL.Instance.GetStoreByMemId(_memid);

            string _provinceName = GYProvinceBLL.Instance.GetGYProvince(_storeentity.ProvinceId ).Name;
            string _cityName = GYCityBLL.Instance.GetGYCityByCode(_storeentity.CityId.ToString()).Name;
            ViewBag.AddressofStore = _provinceName + _cityName;

            _provinceName = GYProvinceBLL.Instance.GetGYProvince(_membill.ReceiverProvince ).Name;
            _cityName = GYCityBLL.Instance.GetGYCityByCode(_membill.ReceiverCity.ToString()).Name;
            ViewBag.AddressofBill = _provinceName + _cityName;

            ViewBag.mementity = _mementity;
            ViewBag.meninfoentity = _meminfoentity;
            ViewBag.membill = _membill;
            ViewBag.storentity = _storeentity;
            return View();

        }

        /// <summary>
        ///审核通过
        /// </summary>
        /// <returns></returns>
        public int AcceptStore()
        {
            int _StoreId = FormString.IntSafeQ("StoreId");
            int _MemberId = FormString.IntSafeQ("MemberId");
            int _billId = FormString.IntSafeQ("billId");

            //StoreEntity _supplierEntity = StoreBLL.Instance.GetStore(_StoreId);
            //_supplierEntity.Status = (int)StoreStatus.Active;
            //int _result = StoreBLL.Instance.UpdateStore(_supplierEntity);

            //MemberEntity _memberEntity = MemberBLL.Instance.GetMember(_MemberId);
            //_memberEntity.Status = (int)MemberStatus.Active;
            //MemberBLL.Instance.UpdateMember(_memberEntity);

            //_result = MemBillVATBLL.Instance.UpdateMemBillVATStatus(_billId, (int)MemBillStatus.AuditPass);

            int _result = MemberBLL.Instance.CheckSupplier(_MemberId, (int)MemberStatus.Active,_StoreId,(int)StoreStatus.Active,_billId,(int)MemBillStatus.AuditPass,memid);
            if(_result>0)
            {
                string msgbodyt = SMSTempletBLL.Instance.GetSMSContentByCode(SMSCodeCollection.RegisterCompanyCheck);
                if(!string.IsNullOrEmpty(msgbodyt))
                {
                    MemberEntity mementity = MemberBLL.Instance.GetMember(_MemberId);
                    SMSNoticeEntity notice = new SMSNoticeEntity();
                    notice.MobilePhone = mementity.MobilePhone; 
                    notice.SMSContent = msgbodyt;
                    notice.SystemType = (int)SystemType.B2B;
                    SMSNoticeBLL.Instance.AddSMSNotice(notice);

                }
            }
            return _result;

        }

        /// <summary>
        /// 审核拒绝
        /// </summary>
        /// <returns></returns>
        public int RefuseStore()
        {
            int _StoreId = FormString.IntSafeQ("StoreId");
            int _MemberId = FormString.IntSafeQ("MemberId");
            int _billId = FormString.IntSafeQ("billId");

            //StoreEntity _supplierEntity = StoreBLL.Instance.GetStore(_StoreId);
            //_supplierEntity.Status = (int)StoreStatus.Rejected;
            //int _result = StoreBLL.Instance.UpdateStore(_supplierEntity);

            //MemberEntity _memberEntity = MemberBLL.Instance.GetMember(_MemberId);
            //_memberEntity.Status = (int)MemberStatus.Rejected;
            //MemberBLL.Instance.UpdateMember(_memberEntity);

            //_result = MemBillVATBLL.Instance.UpdateMemBillVATStatus(_billId, (int)MemBillStatus.AuditReject);
            int _result = MemberBLL.Instance.CheckSupplier(_MemberId, (int)MemberStatus.Rejected, _StoreId, (int)StoreStatus.Rejected, _billId, (int)MemBillStatus.AuditReject,memid);
            if (_result > 0)
            {
                string msgbodyt = SMSTempletBLL.Instance.GetSMSContentByCode(SMSCodeCollection.RegisterCompanyCheckR);
                if (!string.IsNullOrEmpty(msgbodyt))
                {
                    MemberEntity mementity = MemberBLL.Instance.GetMember(_MemberId);
                    SMSNoticeEntity notice = new SMSNoticeEntity();
                    notice.MobilePhone = mementity.MobilePhone;
                    notice.SMSContent = msgbodyt;
                    notice.SystemType = (int)SystemType.B2B;
                    SMSNoticeBLL.Instance.AddSMSNotice(notice); 
                }
            }
            return _result;
        }

        #endregion

        #region 个人信息审核


        /// <summary>
        /// 个人信息审核
        /// </summary>
        /// <returns></returns>
        public ActionResult CheckPersonalInfo()
        {
            string _memCode = QueryString.SafeQ("memCode");//用户名
            int _status = QueryString.IntSafeQ("status");

            int _pageIndex = QueryString.IntSafeQ("pageindex");
            if (_pageIndex == 0) _pageIndex = 1;
            int _pageSize = CommonKey.PageSizeCheckPersonalInfo;
            int _recordCount = 0;

            IList<ConditionUnit> _whereList = new List<ConditionUnit>();
            _whereList.Add(new ConditionUnit { FieldName = SearchFieldName.MemCode, CompareValue = _memCode.ToString() });
            _whereList.Add(new ConditionUnit { FieldName = SearchFieldName.CheckStatus, CompareValue = _status.ToString() });
            IList<VWMemberEntity2> _entityList = MemberInfoBLL.Instance.GetVWMemberInfo(_pageIndex, _pageSize, ref _recordCount, _whereList);
            ViewBag.EntityList = _entityList;

            string _url = "/Check/CheckPersonalInfo/?memCode=" + _memCode + "&status=" + _status;
            string _PageStr = HTMLPage.SetProductListPage(_recordCount, _pageSize, _pageIndex, _url);
            ViewBag.PageStr = _PageStr;
            return View();

        }


        /// <summary>
        /// 审核申请
        /// </summary>
        /// <returns></returns>
        public int CheckTheApply()
        {
            int _id = FormString.IntSafeQ("id");
            int _status = FormString.IntSafeQ("status");
            int _result = MemberBLL.Instance.UpdateMemberSatus(_id, _status);
            if (_result > 0)
            {
                string msgbodyt = "";
                if(_status==(int)MemberStatus.Active)
                {
                     msgbodyt = SMSTempletBLL.Instance.GetSMSContentByCode(SMSCodeCollection.RegisterPersonalCheck);
                }
                if (_status == (int)MemberStatus.Rejected)
                {
                    msgbodyt = SMSTempletBLL.Instance.GetSMSContentByCode(SMSCodeCollection.RegisterPersonalCheckR);
                }
                if (!string.IsNullOrEmpty(msgbodyt))
                {
                    MemberEntity mementity = MemberBLL.Instance.GetMember(_id);
                    SMSNoticeEntity notice = new SMSNoticeEntity();
                    notice.MobilePhone = mementity.MobilePhone;
                    notice.SMSContent = msgbodyt;
                    notice.SystemType = (int)SystemType.B2B;
                    SMSNoticeBLL.Instance.AddSMSNotice(notice); 
                }
            }
            return _result;

        }


        #endregion

    }
}