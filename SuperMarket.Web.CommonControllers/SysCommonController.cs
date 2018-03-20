using SuperMarket.BLL;
using SuperMarket.BLL.Account;
using SuperMarket.BLL.CatograyDB;
using SuperMarket.BLL.CommentDB;
using SuperMarket.BLL.Common;
using SuperMarket.BLL.Cookie;
using SuperMarket.BLL.JcOrderInquiry;
using SuperMarket.BLL.MemberDB;
using SuperMarket.BLL.MessageDB;
using SuperMarket.BLL.ProductDB;
using SuperMarket.BLL.SysDB;
using SuperMarket.BLL.WeiXin;
using SuperMarket.Core;
using SuperMarket.Core.Config;
using SuperMarket.Core.Ftp;
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SuperMarket.Web.CommonControllers
{
    public class SysCommonController : BaseMemberController
    {
        public string GetMemberJson()
        {
            string contactname = FormString.SafeQ("contactname");
            string companyname = FormString.SafeQ("companyname");
            string contactphone = FormString.SafeQ("contactphone");

            IList<VWMemAutoTemplete> list = MemberBLL.Instance.GetVWMemAutoTemplete(contactname, contactphone, companyname);
            return  JsonJC.ObjectToJson(list);
            
        }
        public string GetCGMemListJson()
        {
            ResultObj result = new ResultObj();
            string contactname = FormString.SafeQ("contactname");
            string contactphone = FormString.SafeQ("contactphone");
            string companyname = FormString.SafeQ("companyname");
            string status = FormString.SafeQ("s",-1);
            int pageindex = FormString.IntSafeQ("pageindex",1);
            int pagesize = CommonKey.PageSizeCheck;
            int record = 0;
            ListObj _listobj = new ListObj();
            IList<VWMemAutoTemplete> list = MemberBLL.Instance.GetMemBasicInfoList(pagesize, pageindex,ref record,contactname, contactphone, companyname,-1,1,-1);
            result.Status = (int)CommonStatus.Success;
            _listobj.Total = record;
            _listobj.List = list;
            result.Obj = _listobj;
            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 查询价订单指派的供应商列表
        /// </summary>
        /// <returns></returns>
        public string GetInquiryCGMemListJson()
        {
            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            int status = FormString.IntSafeQ("s", -1);
            int hasread = FormString.IntSafeQ("r", -1);
            int hasquote = FormString.IntSafeQ("q", -1);
            int cgmemid = FormString.IntSafeQ("cgmemid", -1);
            int pageindex = FormString.IntSafeQ("pageindex", 1);
            int pagesize = CommonKey.PageSizeCheck;
            int record = 0;
            ListObj _listobj = new ListObj();
            IList<VWCGMemQuotedEntity> list = CGMemQuotedBLL.Instance.GetInquiryCGMemQuotedList(pagesize, pageindex, ref record, ordercode, hasread, hasquote,status, cgmemid);
            result.Status = (int)CommonStatus.Success;
            _listobj.Total = record;
            _listobj.List = list;
            result.Obj = _listobj;
            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 询价订单派遣给供应商
        /// </summary>
        public string InquiryToCGMemQuote()
        {

            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            string memids = FormString.SafeQ("memids");
            memids= memids.Trim(',');
            int resultrowi = CGMemQuotedBLL.Instance.AddInquiryToCGMemQuoted(ordercode, memids);
            if(resultrowi>0)
            {
                result.Status = (int)CommonStatus.Success;
                result.Obj = resultrowi;

            }
            else
            {

                result.Status = (int)CommonStatus.Fail;
            }
            return JsonJC.ObjectToJson(result);
        }
        /// <summary>
        /// 询价订单供应商发送微信提醒
        /// </summary>
        /// <returns></returns>
        public string WeChatInquiryOrderSend()
        {

            ResultObj result = new ResultObj();
            string ordercode = FormString.SafeQ("ordercode");
            string memids = FormString.SafeQ("memids");
            memids = memids.Trim(',');

            string[] memidattr = memids.Split(',');

            InquiryOrderEntity orderentity = InquiryOrderBLL.Instance.GetInquiryOrderByCode(ordercode);
            if (orderentity != null && orderentity.Status == (int)OrderInquiryStatusEnum.Quoting)
            {
                string url = string.Format(WeiXinConfig.URL_FORMAT_SendMsg, WeiXinJsSdk.Instance.GetAccessToken());
                string resulturl = SuperMarketWebUrl.InquiryOrderSendWeChat(ordercode);
                string redirecturl = SuperMarketWebUrl.InquiryOrderUrl( ordercode);
                result.Obj = resulturl;
                ////获取链接导航Id
                //int navid = WeChatNavigationBLL.Instance.GetIdByUrl(redirecturl);
                foreach (string memidstr in memidattr)
                {
                    int memid = StringUtils.GetDbInt(memidstr);
                    MemberEntity memen = MemberBLL.Instance.GetMember(memid);
                    if (!string.IsNullOrEmpty(memen.WeChat))
                    {
                        MemWeChatMsgEntity wecharmsg = MemWeChatMsgBLL.Instance.GetMsgByAppUnionId(WeiXinConfig.GetAppId(), memen.WeChat);
                        if (wecharmsg != null && !string.IsNullOrEmpty(wecharmsg.OpenId))
                        {
                            WeiXinSendMsgEntity send = new WeiXinSendMsgEntity();
                            send.touser = wecharmsg.OpenId;
                            send.template_id = WeiXinTemplet.InquiryQuoteSend;
                            send.url = resulturl;
                            WeiXinInquiryEntity inq = new WeiXinInquiryEntity();
                            inq.first = new WeiXinUnitEntity() { value = "您有订单需要报价啦，赶紧抢单,订单编号：" + orderentity.Code };
                            inq.tradeDateTime = new WeiXinUnitEntity() { value = orderentity.CreateTime.ToString("yyyy-MM-dd HH:mm:ss") };
                            inq.orderType = new WeiXinUnitEntity() { value = "询价订单" };
                            inq.customerInfo = new WeiXinUnitEntity() { value = "易店心" };
                            //inq.orderItemName = new WeiXinUnitEntity() { value = "随机名称" };
                            //inq.orderItemData = new WeiXinUnitEntity() { value = "随机数据" };
                            inq.remark = new WeiXinUnitEntity() { value = orderentity.Remark };
                            send.data = inq;
                            string json = JsonJC.ObjectToJson(send);
                            WeChatMsgEntity msg = new WeChatMsgEntity();
                            msg.ParamStr = json;
                            msg.WeChatOpenId = wecharmsg.OpenId;
                            msg.RedirectUrl = redirecturl;
                            msg.WeChatUrl = url;
                            msg.TemplateIid = WeiXinTemplet.InquiryQuoteSend;
                            msg.Id = WeChatMsgBLL.Instance.AddWeChatMsg(msg);
                            string resultrowi = WebServiceClient.QueryPostWebServiceJson(url, json);
                            WeiXinFailEntity resulten = JsonJC.JsonToObject<WeiXinFailEntity>(resultrowi);
                            if (resulten.errmsg.ToLower() == "ok")
                            {
                                CGMemQuotedBLL.Instance.CGQuotedSend(memid, ordercode); 
                            }
                            msg.Result = resultrowi;
                            WeChatMsgBLL.Instance.UpdateWeChatMsg(msg);
                        }
                    }
                }
            
                
            }
            else
            {
                CGMemQuotedBLL.Instance.QuotedCloseByCode(ordercode);
            }
            result.Status = (int)CommonStatus.Success;
            return JsonJC.ObjectToJson(result);
        }
 
        #region 上传图片
        public string UploadImageFile()
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
                Random _rd = new Random();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, _certype.ToString(), memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                string dicpathfull = dicpath + file.FileName.Substring(file.FileName.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
            }

            return "";
        }

        public string UploadImageFileForBit()
        {
            int _pictype = QueryString.IntSafeQ("pictype");
            string filename = QueryString.SafeQ("name");
            using (Stream filesStream = System.Web.HttpContext.Current.Request.InputStream)
            { 
                CertificateType _certype = (CertificateType)_pictype; 
                byte[] bytes = new byte[filesStream.Length];
                filesStream.Read(bytes, 0, bytes.Length);
                // 设置当前流的位置为流的开始
                filesStream.Seek(0, SeekOrigin.Begin);
                FtpUtil _ftp = new FtpUtil();
                Random _rd = new Random();
                string dicpath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesSystemName, _certype.ToString(), memid.ToString(), DateTime.Now.ToString("yyyy"), DateTime.Now.ToString("MM"), DateTime.Now.ToString("dd"), _rd.Next(100000, 999999).ToString());
                string dicpathfull = dicpath + filename.Substring(filename.LastIndexOf("."));
                string certifapath = FilePath.PathCombine(ConfigCore.Instance.ConfigCommonEntity.FtpImagesRootPath, dicpathfull);
                _ftp.UploadFile(certifapath, bytes, true);
                return "{\"jsonrpc\" : \"2.0\", \"result\" : null, \"pic_raw\" : \"" + dicpathfull + "\"}";
            }

            return "";
        }

        #endregion
    }
}
