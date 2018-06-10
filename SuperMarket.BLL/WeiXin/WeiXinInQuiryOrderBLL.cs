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
using SuperMarket.BLL.CatograyDB;

namespace SuperMarket.BLL.WeiXin
{
    public class WeiXinInQuiryOrderBLL
    {
        #region 实例化
        public static WeiXinInQuiryOrderBLL Instance
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
            internal static readonly WeiXinInQuiryOrderBLL instance = new WeiXinInQuiryOrderBLL();
        }
        /// <summary>
        /// 发送给用户确认询价订单
        /// </summary>
        /// <param name="code"></param>
        /// <param name="templeteid"></param>
        /// <returns></returns>
        public bool NoteToUserAccept(string code, int memid)
        {
            bool hassend = false;
            VWMemberEntity memen = MemberBLL.Instance.GetVWMember(memid);
            if (!string.IsNullOrEmpty(memen.WeChat))
            {
                string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryWebUrl + string.Format(WebUrlEnum.InquiryMemConfirm, code);
                Hashtable hashentity = new Hashtable();
                hashentity.Add("first", new WeiXinUnitEntity() { value = "请确认您的订单,订单编号：" + code });
                hashentity.Add("keyword1", new WeiXinUnitEntity() { value = memen.ContactsManName });
                hashentity.Add("keyword2", new WeiXinUnitEntity() { value = DateTime.Now.ToShortDateString() });
                hashentity.Add("remark", new WeiXinUnitEntity() { value = "" });
                hassend= WeiXinJsSdk.Instance.SendWeiXinMsgNote(memen.WeChat, redirecturl, WeiXinTemplet.OrderConfirmNote, hashentity);
            }
            return hassend;
        }
        public bool NoteToUserAcceptAgain(string code, int memid)
        {
            bool hassend = false;
            VWMemberEntity memen = MemberBLL.Instance.GetVWMember(memid);
            if (!string.IsNullOrEmpty(memen.WeChat))
            {
                string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryWebUrl + string.Format(WebUrlEnum.InquiryMemConfirm, code);
                Hashtable hashentity = new Hashtable();
                hashentity.Add("first", new WeiXinUnitEntity() { value = "您的询价产品价格已更新，请及时查看,订单编号：" + code });
                hashentity.Add("keyword1", new WeiXinUnitEntity() { value = memen.ContactsManName });
                hashentity.Add("keyword2", new WeiXinUnitEntity() { value = DateTime.Now.ToShortDateString() });
                hashentity.Add("remark", new WeiXinUnitEntity() { value = "报价调整" });
                hassend = WeiXinJsSdk.Instance.SendWeiXinMsgNote(memen.WeChat, redirecturl, WeiXinTemplet.OrderConfirmNote, hashentity);
            }
            return hassend;
        }
        /// <summary>
        /// 供应商报价后提交报价收到通知到报价审核员
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool NoteToSysCheckPrice(string code)
        {
            bool result = false;
            IList<VWMemberEntity> memlist= MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderQuoteCheck);
            if(memlist!=null&& memlist.Count>0)
            {
                int needquoterelease = CGMemQuotedBLL.Instance.GetCGMemNotQuoted(code);
                string strneedquote = "";
                if (needquoterelease > 0)
                {
                    strneedquote = "还有" + needquoterelease + "位供应商未报价";
                }
                else
                {
                    strneedquote = "所有通知供应商都报价完成";
                }
                foreach (VWMemberEntity mem in memlist)
                {
                    if (!string.IsNullOrEmpty(mem.WeChat))
                    {
                        string redirecturl = ConfigCore.Instance.ConfigCommonEntity.SysMobileWebUrl + string.Format(WebUrlEnum.InquirySysMemCheck, code);

                        Hashtable hashentity = new Hashtable();
                        hashentity.Add("first", new WeiXinUnitEntity() { value = "您收到一个供应商的报价啦,订单编号：" + code });
                        hashentity.Add("keyword1", new WeiXinUnitEntity() { value = DateTime.Now.ToShortDateString()  });
                        hashentity.Add("keyword2", new WeiXinUnitEntity() { value = "供应商" + mem.MemId + "_" + mem.MemGrade });
                        //hashentity.Add("keyword3", new WeiXinUnitEntity() { value = "查看详情" });
                        hashentity.Add("remark", new WeiXinUnitEntity() { value = strneedquote });
                        if (!result)
                        {
                            result = WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.RecivedQuoteNote, hashentity);
                        }
                        else
                        {
                            WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.RecivedQuoteNote, hashentity);
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 供应商备货通知
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool CGMemStockNote(string code)
        {
            bool result = false; 
            ConfirmOrderEntity order = ConfirmOrderBLL.Instance.GetConfirmOrderByCode(code);
            IList<VWConfirmOrderCGMemEntity> memberlist=ConfirmOrderProductBLL.Instance.GetConfirmCGMemsByCode(code);
            if (order != null && order.Id > 0 && memberlist != null && memberlist.Count > 0)
            {
                foreach (VWConfirmOrderCGMemEntity member in memberlist)
                {
                    //if (order.ScopeType==(int)SuperMarket.Model.ScopeTypeEnum.Normal)
                    //{
                    //    ClassCGScopeEntity cgscope = new ClassCGScopeEntity();
                    //    cgscope.ScopeClassName = order.CarBrandName;
                    //    cgscope.ClassId = 0;
                    //    cgscope.IsActive = 1;
                    //    cgscope.Sort = 0;
                    //    cgscope.IsRoot = 0;
                    //    cgscope.ParentId = 0;
                    //    ClassCGScopeBLL.Instance.AddClassCGScope(cgscope);
                    //}
                    MemCGScopeEntity scope = new MemCGScopeEntity();
                    scope.BrandId = order.CarBrandId;
                    scope.BrandName = order.CarBrandName;
                    scope.ClassId = 0;
                    scope.ClassName = "";
                    scope.CGMemId = member.CGMemId;
                    scope.CreateTime = DateTime.Now;
                    scope.IsActive =1; 
                    scope.ScopeType = (int)ScopeTypeEnum.Car;
                    MemCGScopeBLL.Instance.AddMemCGScope(scope);
                   VWMemberEntity _cgmementity = MemberBLL.Instance.GetVWMember(member.CGMemId);

                    if (!string.IsNullOrEmpty(_cgmementity.WeChat))
                    {
                        string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryMobileWebUrl + string.Format(WebUrlEnum.ConfirmCGStockNote, code);
                        Hashtable hashentity = new Hashtable();
                        hashentity.Add("first", new WeiXinUnitEntity() { value = "订单已确认，准备发货啦" });
                        hashentity.Add("keyword1", new WeiXinUnitEntity() { value = code });
                        hashentity.Add("keyword2", new WeiXinUnitEntity() { value = member.CGTotalPrice.ToString("0.00") });
                        hashentity.Add("keyword3", new WeiXinUnitEntity() { value = "易店心" });
                        hashentity.Add("keyword4", new WeiXinUnitEntity() { value = "待发货" });
                        hashentity.Add("remark", new WeiXinUnitEntity() { value = "客户已确认，尽快备货吧" });
                        WeiXinJsSdk.Instance.SendWeiXinMsgNote(_cgmementity.WeChat, redirecturl, WeiXinTemplet.OrderStockNote, hashentity);

                    }
                }
            }
              
           
            return result;
        }
        /// <summary>
        /// 通知送货员微信
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool NoteToDeliveryman(string code)
        {
            bool result = false;
            IList<VWMemberEntity> memlist = MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderDelivery);
            if (memlist != null && memlist.Count > 0)
            {
                ConfirmOrderEntity order = ConfirmOrderBLL.Instance.GetConfirmOrderByCode(code);
                //CGMemQuotedEntity orderquote = CGMemQuotedBLL.Instance.GetCGMemHasCheckedByCode(order.InquiryOrderCode);
                VWMemberEntity  _mementity = MemberBLL.Instance.GetVWMember(order.MemId);
                foreach (VWMemberEntity mem in memlist)
                {
                    if (!string.IsNullOrEmpty(mem.WeChat))
                    {
                        string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryMobileWebUrl + string.Format(WebUrlEnum.InquiryDeliveryNote, code);
                        Hashtable hashentity = new Hashtable();
                        hashentity.Add("first", new WeiXinUnitEntity() { value = "又来订单啦，赶紧发货走起,订单编号：" + code });
                        hashentity.Add("keyword1", new WeiXinUnitEntity() { value = "易店心" });
                        hashentity.Add("keyword2", new WeiXinUnitEntity() { value = "易店心" });
                        hashentity.Add("keyword3", new WeiXinUnitEntity() { value = _mementity.CompanyAddress });
                        hashentity.Add("keyword4", new WeiXinUnitEntity() { value = _mementity.CompanyName });
                        hashentity.Add("keyword5", new WeiXinUnitEntity() { value = order.CreateTime.ToString() });
                        hashentity.Add("remark", new WeiXinUnitEntity() { value = "" });
                        if (!result)
                        {
                            result = WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.OrderDeliveryNote, hashentity);
                        }
                        else
                        {
                            WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.OrderDeliveryNote, hashentity);
                        }
                    }
                }
            }
            return result;
        }

       
        /// <summary>
        /// 通知供应商
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool NoteToCGMemForQuote(string code)
        {
            bool result = false;

            string emailnote = "供应商发送微信情况<br/>";
            emailnote += "订单号：" + code + "<br/>";
             InquiryOrderEntity order = InquiryOrderBLL.Instance.GetInquiryOrderByCode(code);
            string memidsold = CGMemQuotedBLL.Instance.GetQuotedCGMemByCode(order.Code);
            string memidsnew = MemCGScopeBLL.Instance.GetCGMemIdsByCarBrand(order.ScopeType, order.CarBrandName, memidsold);
            //获取虚拟报价员
            IList<VWMemberEntity> memlist = MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderQuote);

            string[] memidsoldattr = memidsold.Split(',');
            string[] memidnewattr = memidsnew.Split(',');
            bool hascg = false; 
            if(!string.IsNullOrEmpty(memidsnew))
            {
                hascg = true;
            }
            if (memlist!=null&& memlist.Count>0)
            {
                foreach(VWMemberEntity cgmemsup in memlist)
                {
                    bool oldhas = false;
                    if(memidsoldattr!=null&& memidsoldattr.Length>0)
                    {
                        foreach(string memidoleen in memidsoldattr)
                        {
                            if(cgmemsup.MemId.ToString()==memidoleen)
                            {
                                oldhas = true;
                                break;
                            }
                        }
                        if (!oldhas)
                        {
                            foreach (string memidnewen in memidnewattr)
                            {
                                if (cgmemsup.MemId.ToString() == memidnewen)
                                {
                                    oldhas = true;
                                    break;
                                }
                            }
                        }
                    }
                    if(!oldhas)
                    {
                        memidsnew += "," + cgmemsup.MemId.ToString();
                    }
                }
            }
            int resultrowi = CGMemQuotedBLL.Instance.AddInquiryToCGMemQuoted(order.Code, memidsnew);
            //获取要发送的供应商，并发送通知
             //InquiryOrderBLL.Instance.UpdateQuoteStatus(order.Code, (int)QuoteStatusEnum.HasSend);
            IList<CGMemQuotedEntity> _sendlist = CGMemQuotedBLL.Instance.GetCGMemQuotedNeedSend(order.Code);
            string redirecturl = ConfigCore.Instance.ConfigCommonEntity.InquiryWebUrl + string.Format(WebUrlEnum.InquiryCGMemNote, code);
            //int navid = WeChatNavigationBLL.Instance.GetIdByUrl(redirecturl);
            //string url = string.Format(WeiXinConfig.URL_FORMAT_SendMsg, WeiXinJsSdk.Instance.GetAccessToken(false));
            if (!hascg)
            {
                emailnote += "没有找到供应商<br/>";
            }
            if (_sendlist != null && _sendlist.Count > 0)
            {
                foreach (CGMemQuotedEntity entity in _sendlist)
                {
                    if (!hascg)
                    {
                        string title = "您的询价单没有找到供应商额,订单编号：" + order.Code; 
                        string ordercode = entity.InquiryOrderCode;
                        try
                        {
                            //获取链接导航Id
                            int memid = entity.CGMemId;
                            VWMemberEntity memen = MemberBLL.Instance.GetVWMember(memid); 
                            emailnote += memen.CompanyName + memen.MobilePhone + memen.CompanyAddress + "     ";
                            if (!string.IsNullOrEmpty(memen.WeChat))
                            {
                                Hashtable hashentity = new Hashtable();
                                hashentity.Add("first", new WeiXinUnitEntity() { value = title });
                                hashentity.Add("keyword1", new WeiXinUnitEntity() { value = "无" });
                                hashentity.Add("keyword2", new WeiXinUnitEntity() { value = order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") });
                                hashentity.Add("keyword3", new WeiXinUnitEntity() { value = order.CarBrandName });
                                hashentity.Add("keyword4", new WeiXinUnitEntity() { value ="备注"+ order.Remark });
                                hashentity.Add("remark", new WeiXinUnitEntity() { value = "" });
                                bool sendsuccess = WeiXinJsSdk.Instance.SendWeiXinMsgNote(memen.WeChat, redirecturl, WeiXinTemplet.InquiryNeedNote, hashentity);
                                if (sendsuccess)
                                {
                                    result = sendsuccess;
                                    //发送微信成功备份
                                    CGMemQuotedBLL.Instance.CGQuotedSend(memid, ordercode);
                                    emailnote += "发送成功<br/>";
                                } 
                            }
                            else
                            {
                                emailnote+= "没有绑定微信<br/>";
                                ////需发邮件提醒 
                                //EmailSendEntity email = new EmailSendEntity();
                                //email.CreateTime = DateTime.Now;
                                //email.Body = "询价单编号：" + order.Code + "<br/> 供应商：" + memen.MemCode + "(" + memen.MobilePhone + ")" + memen.MemGradeName + "，<br/>网址：" + redirecturl;
                                //var emailsendstr = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.SendMailManager;
                                //if (emailsendstr == null || string.IsNullOrEmpty(emailsendstr.ToString()))
                                //{
                                //    email.Email = "20718505@qq.com";
                                //}
                                //else
                                //{
                                //    email.Email = emailsendstr.ToString();
                                //}
                                //email.Title = "询价单发送微信错误供应商未登记微信";
                                //email.Status = 0;
                                //EmailSendBLL.Instance.AddEmailSend(email);
                            }
                        }
                        catch (Exception ex)
                        {
                            emailnote += "发送错误" + ex.Message + "<br/>";
                            LogUtil.Log("微信发送询价出错：订单号：" + ordercode, ex.Message);
                        }
                    }
                    else
                    {


                        string title = "您有订单需要报价啦，赶紧抢单,订单编号：" + order.Code;
                        if (entity.HasSend == 1)
                        {
                            title = "客户修改了询价单信息，请完善报价,订单编号：" + order.Code;
                        }
                        string ordercode = entity.InquiryOrderCode;
                        try
                        {
                            //获取链接导航Id
                            int memid = entity.CGMemId;
                            VWMemberEntity memen = MemberBLL.Instance.GetVWMember(memid);

                            emailnote += memen.CompanyName + memen.MobilePhone + memen.CompanyAddress + "     ";
                            if (!string.IsNullOrEmpty(memen.WeChat))
                            {
                                Hashtable hashentity = new Hashtable();
                                hashentity.Add("first", new WeiXinUnitEntity() { value = title });
                                hashentity.Add("tradeDateTime", new WeiXinUnitEntity() { value = order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") });
                                hashentity.Add("orderType", new WeiXinUnitEntity() { value = "询价订单" });
                                hashentity.Add("customerInfo", new WeiXinUnitEntity() { value = "易店心" });
                                hashentity.Add("orderItemName", new WeiXinUnitEntity() { value = "备注" });
                                hashentity.Add("orderItemData", new WeiXinUnitEntity() { value = order.Remark });
                                hashentity.Add("remark", new WeiXinUnitEntity() { value = "" });
                                bool sendsuccess = WeiXinJsSdk.Instance.SendWeiXinMsgNote(memen.WeChat, redirecturl, WeiXinTemplet.InquiryQuoteSend, hashentity);
                                if (sendsuccess)
                                {
                                    result = sendsuccess;
                                    //发送微信成功备份
                                    CGMemQuotedBLL.Instance.CGQuotedSend(memid, ordercode);
                                    emailnote += "发送成功<br/>";
                                }
                                else
                                {
                                    emailnote += "发送失败<br/>";
                                }
                                //else
                                //{
                                //    //需发邮件提醒 
                                //    EmailSendEntity email = new EmailSendEntity();
                                //    email.CreateTime = DateTime.Now;
                                //    email.Body = "询价单编号：" + order.Code + "<br/> 供应商：" + memen.MemCode + "(" + memen.MobilePhone + ")" + memen.MemGradeName + "，<br/>网址：" + redirecturl;
                                //    var emailsendstr = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.SendMailManager;
                                //    if (emailsendstr == null || string.IsNullOrEmpty(emailsendstr.ToString()))
                                //    {
                                //        email.Email = "20718505@qq.com";
                                //    }
                                //    else
                                //    {
                                //        email.Email = emailsendstr.ToString();
                                //    }
                                //    email.Title = "询价单发送微信通知失败";
                                //    email.Status = 0;
                                //    EmailSendBLL.Instance.AddEmailSend(email); 
                                //}


                            }
                            else
                            {
                                emailnote += "没有绑定微信<br/>";
                                ////需发邮件提醒 
                                //EmailSendEntity email = new EmailSendEntity();
                                //email.CreateTime = DateTime.Now;
                                //email.Body = "询价单编号：" + order.Code + "<br/> 供应商：" + memen.MemCode + "(" + memen.MobilePhone + ")" + memen.MemGradeName + "，<br/>网址：" + redirecturl;
                                //var emailsendstr = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.SendMailManager;
                                //if (emailsendstr == null || string.IsNullOrEmpty(emailsendstr.ToString()))
                                //{
                                //    email.Email = "20718505@qq.com";
                                //}
                                //else
                                //{
                                //    email.Email = emailsendstr.ToString();
                                //}
                                //email.Title = "询价单发送微信错误供应商未登记微信";
                                //email.Status = 0;
                                //EmailSendBLL.Instance.AddEmailSend(email);
                            }
                        }
                        catch (Exception ex)
                        {
                            emailnote += "发送错误" + ex.Message +"<br/>";

                            LogUtil.Log("微信发送询价出错：订单号：" + ordercode, ex.Message);
                        }
                    }
                }
            }
            else
            { 
                InquiryOrderBLL.Instance.UpdateQuoteStatus(order.Code, (int)QuoteStatusEnum.SendFail);
            }

            string notemsg = emailnote  + "报价网址" + redirecturl+ "<br/>";
              notemsg += "转移到线下网址：" +SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.InquiryMobileWebUrl+ "/Check/copyinquiry?code="+ order.Code + "<br/>";
            //notemsg+="订单信息:订单号：" + order.Code+"<br/>";
            //notemsg+="备注" + order.Remark+ "<br/>";
            //notemsg+="图片信息" + "<br/>";
            //if(!string.IsNullOrEmpty(order.VinPic))
            //{
            //    notemsg += "<img src='" + SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.ImagesServerUrl + order.VinPic + "' />";
            //}
            //if (!string.IsNullOrEmpty(order.EngineModelPic))
            //{
            //    notemsg += "<img src='" + SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.ImagesServerUrl + order.EngineModelPic + "' />";
            //}
            //if (order.OrderPics!=null&& order.OrderPics.Count>0)
            //{
            //    foreach(InquiryOrderPicsEntity pic in order.OrderPics)
            //    {
            //              notemsg += "<img src='" + SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.ImagesServerUrl + pic.PicUrl + "' />";
            //    }
            //}
            //notemsg +="<br/>";
            //notemsg += "产品信息" + "<br/>";
            //if (order.OrderProducts != null && order.OrderProducts.Count > 0 && order.OrderProductSubs!=null && order.OrderProductSubs.Count>0)
            //{
            //    Dictionary<int, IList<InquiryProductSubEntity>> prosubdic = new Dictionary<int, IList<InquiryProductSubEntity>>();
            //    foreach (InquiryProductSubEntity prosub in order.OrderProductSubs)
            //    {
            //        if(!prosubdic.ContainsKey(prosub.InquiryProductId))
            //        {
            //            prosubdic.Add(prosub.InquiryProductId, new List<InquiryProductSubEntity>());
            //        }
            //        prosubdic[prosub.InquiryProductId].Add(prosub); 
            //    }
            //    foreach(InquiryProductEntity pro in order.OrderProducts)
            //    {
            //        if(prosubdic.ContainsKey(pro.Id))
            //        {
            //            IList <InquiryProductSubEntity> prosublist = prosubdic[pro.Id];
            //            if(prosublist!=null&& prosublist.Count>0)
            //            {
            //                foreach(InquiryProductSubEntity prosub in prosublist)
            //                {
            //                    notemsg += pro.ProductName + " " + prosub.InquiryProductTypeName + "<br/>";
            //                }
            //            }
            //        } 
            //    }
            //}
            if (memlist != null && memlist.Count > 0)
            {
                foreach (VWMemberEntity cgmemsup in memlist)
                {
                    MemWeChatMsgEntity wecharmsg = MemWeChatMsgBLL.Instance.GetMsgByAppUnionId(WeiXinConfig.GetAppId(), cgmemsup.WeChat);
                    WeiXinCustomerEntity customer = new WeiXinCustomerEntity();
                    customer.touser = wecharmsg.OpenId;
                    customer.msgtype = WeiXinCustomerMsgtypeEnum.text;
                    WeiXinCustomerTextEntity text = new WeiXinCustomerTextEntity();
                    text.content = notemsg.Replace("<br/>", "\n");
                    customer.text = text; 
                    WeiXinJsSdk.Instance.SendWeiXinCustomerNote(customer); 
                }
            }
            //需发邮件提醒 
            EmailSendEntity email = new EmailSendEntity();
            email.CreateTime = DateTime.Now;
            email.Body = notemsg;
            var emailsendstr = SuperMarket.Core.ConfigCore.Instance.ConfigCommonEntity.SendMailManager;
            if (emailsendstr == null || string.IsNullOrEmpty(emailsendstr.ToString()))
            {
                email.Email = "20718505@qq.com";
            }
            else
            {
                email.Email = emailsendstr.ToString();
            }
            email.Title = "询价单没有找到供应商进行报价";
            email.Status = 0;
            EmailSendBLL.Instance.AddEmailSend(email);
            return result;
        }

     

        /// <summary>
        /// 用户确认后通知报价审核员再次选择供应商
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool NoteToSysSelectCGMem(string code,int memid)
        {
            bool result = false;
            IList<VWMemberEntity> memlist = MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderQuoteCheck);
            if (memlist != null && memlist.Count > 0)
            {
                VWMemberEntity member = MemberBLL.Instance.GetVWMember(memid);
                foreach (VWMemberEntity mem in memlist)
                {
                    if (!string.IsNullOrEmpty(mem.WeChat))
                    {
                        string redirecturl = ConfigCore.Instance.ConfigCommonEntity.SysMobileWebUrl + string.Format(WebUrlEnum.ConfirmSysMemCheck, code);
                        Hashtable hashentity = new Hashtable();
                        hashentity.Add("first", new WeiXinUnitEntity() { value = "用户的订单已确认，请选择采购供应商,订单编号：" + code });
                        hashentity.Add("keyword1", new WeiXinUnitEntity() { value = member.CompanyName+"("+ member.ContactsManName + ")" });
                        hashentity.Add("keyword2", new WeiXinUnitEntity() { value = DateTime.Now.ToShortDateString() });
                        hashentity.Add("remark", new WeiXinUnitEntity() { value = "" });
                         if (!result)
                        {
                            result = WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.OrderConfirmNote, hashentity);
                        }
                        else
                        {
                            WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.OrderConfirmNote, hashentity);
                        }
                    }
                }
            }
            return result;
        }

        public bool NoteToSysFeedBack(string code, InquiryOrderFeedBackEntity  feedback)
        { 

            bool result = false;
            IList<VWMemberEntity> memlist = MemberBLL.Instance.GetMemByAuthCode(MemberAuthEnum.InquiryOrderQuoteCheck);
            if (memlist != null && memlist.Count > 0)
            {
                VWMemberEntity member = MemberBLL.Instance.GetVWMember(feedback.MemId);
                foreach (VWMemberEntity mem in memlist)
                {
                    if (!string.IsNullOrEmpty(mem.WeChat))
                    {
                        string redirecturl = ConfigCore.Instance.ConfigCommonEntity.SysMobileWebUrl + string.Format(WebUrlEnum.InquiryOrderReadForSys, code);
                        Hashtable hashentity = new Hashtable();
                        hashentity.Add("first", new WeiXinUnitEntity() { value = "用户提出了反馈需求，请及时处理,订单编号：" + code });
                        hashentity.Add("keyword1", new WeiXinUnitEntity() { value = feedback.FeedBackReasonName });
                        hashentity.Add("keyword2", new WeiXinUnitEntity() { value = feedback.FeedBackTime.ToShortDateString() });
                        hashentity.Add("remark", new WeiXinUnitEntity() { value = "反馈人：" + member.CompanyName });
                        if (!result)
                        {
                            result = WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.InquiryOrderFeedBackNote, hashentity);
                        }
                        else
                        {
                            WeiXinJsSdk.Instance.SendWeiXinMsgNote(mem.WeChat, redirecturl, WeiXinTemplet.InquiryOrderFeedBackNote, hashentity);
                        }
                    }
                }
            }  
            return result;
        }
        #endregion
    }
}
