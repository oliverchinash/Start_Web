using SuperMarket.Core;
using SuperMarket.Core.WebService;
using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SuperMarket.Model;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.MemberDB;
using SuperMarket.Core.Json;
using SuperMarket.BLL.SysDB;
using SuperMarket.Model.Common;
using SuperMarket.BLL.JcOrderInquiry;

namespace SuperMarket.BLL.WeiXin
{
    /// <summary>
    /// 调客服接口类
    /// </summary>
    public class WeiXinCustomerBLL
    {
        #region 实例化
        public static WeiXinCustomerBLL Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        class Nested
        {
            static Nested()
            {
            }
            internal static readonly WeiXinCustomerBLL instance = new WeiXinCustomerBLL();
        }
        
        public bool SendCGUrlToManager(string oldurl,int memid)
        {
            bool returnresult = false;
            string _redirecturl = oldurl;
            VWMemberEntity _memen = MemberBLL.Instance.GetVWMember(memid);
            WeChatNavigationEntity _entity = WeChatNavigationBLL.Instance.GetNavigationByUrl(_redirecturl);
            if (_entity == null || _entity.Id == 0)
            {
                _entity.RedirectUrl = _redirecturl;
                //_entity.Id = WeChatNavigationBLL.Instance.AddWeChatNavigation(_entity);
            }
            _entity.WeChatUrlType =(int) WeChatUrTypeEnum.Temp;
            _entity.Remark = "供应商临时登录报价";
            //_entity.WeChatUrl = string.Format(WeiXinConfig.URL_FORMAT_KHRedirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.WeChatWebUrl), _entity.Id);
            _entity.WeChatUrl = string.Format(WeiXinConfig.URL_WeiXin_Redirect, WeiXinConfig.GetAppId(), System.Web.HttpContext.Current.Server.UrlEncode(_redirecturl), _entity.Id);

            int _result = WeChatNavigationBLL.Instance.UpdateWeChatNavigation(_entity);

            IList<VWMemberEntity> memlist = MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderQuote);

            if (memlist != null && memlist.Count > 0)
            {
                foreach (VWMemberEntity mem in memlist)
                {
                    if (!string.IsNullOrEmpty(mem.WeChat))
                    {
                        
                        MemWeChatMsgEntity wecharmsg = MemWeChatMsgBLL.Instance.GetMsgByAppUnionId(WeiXinConfig.GetAppId(), mem.WeChat);
                        if (wecharmsg != null && !string.IsNullOrEmpty(wecharmsg.OpenId))
                        {
                            WeiXinCustomerEntity customer = new WeiXinCustomerEntity();
                            customer.touser = wecharmsg.OpenId;
                            customer.msgtype = WeiXinCustomerMsgtypeEnum.text;
                            WeiXinCustomerTextEntity text = new WeiXinCustomerTextEntity();
                            text.content = "亲爱的" + _memen.CompanyName + "\n易店心来新的询价单，赶紧点击网址抢单额\n抢单网址:" + _entity.WeChatUrl;
                            customer.text = text; 
                            returnresult = WeiXinJsSdk.Instance.SendWeiXinCustomerNote(customer);
                           
                        }
                    } 
                }
            }
            return returnresult;
        }
        #endregion
    }
}
