using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    public class CommonKey
    {

        /// <summary>
        /// 默认列表取所有值的页码
        /// </summary>
        public const decimal MaxPrice = 1000000;
        /// <summary>
        /// 默认列表取所有值的页码
        /// </summary>
        public const int CartMaxNum = 50;
        /// <summary>
        /// 默认列表取所有值的页码
        /// </summary>
        public const int PageMaxNum = 999;
        /// <summary>
        /// 默认树级根节点上级
        /// </summary>
        public const int RootParent = 0;
        /// <summary>
        /// 默认添加节点的Id=遇到Id为这个值得认为是添加
        /// </summary>
        public const int AddId = 0;
        /// <summary>
        /// 手机端采购订单显示记录数
        /// </summary>
        public const int PageSizeCGOrderMobile =10;
        /// <summary>
        /// 询价订单显示记录数
        /// </summary>
        public const int PageSizeOrderInquiry = 5;
        /// <summary>
        /// 订单显示记录数
        /// </summary>
        public const int PageSizeOrder = 15;
        /// <summary>
        /// 订单显示记录数
        /// </summary>
        public const int PageSizeProductMobile = 10;
        /// <summary>
        /// 手机端订单显示记录数
        /// </summary>
        public const int PageSizeOrderMobile =3;
        /// <summary>
        /// 手机端订单显示记录数
        /// </summary>
        public const int PageSizeFavoritsMobile = 5;
        /// <summary>
        /// 积分变动显示记录数
        /// </summary>
        public const int PageSizeIntegralChange =15;
        /// <summary>
        /// 收货地址显示记录数
        /// </summary>
        public const int PageSizeAddress = 10;
        /// <summary>
        /// 普通发票地址显示记录数
        /// </summary>
        public const int PageSizeBill = 5;
        /// <summary>
        /// 资金变动情况显示记录数
        /// </summary>
        public const int PageSizeAssetLog = 10;
        /// <summary>
        /// 评论显示记录数
        /// </summary>
        public const int PageSizeCommentShow = 10;
        public const int PageSizeCommentShowMobile = 5;
        /// <summary>
        /// 列表页显示记录数
        /// </summary>
        public const int PageSizeList = 20;
        /// <summary>
        /// 热门款式显示的记录数
        /// </summary>
        public const int HotStyleShowNum = 10;
        /// <summary>
        /// 供应商资格审核的记录数
        /// </summary>
        public const int PageSizeCheck = 15;
        /// <summary>
        /// 退换货的记录数
        /// </summary>
        public const int PageSizeReturns = 15;
        /// <summary>
        /// 购物指南的记录数
        /// </summary>
        public const int PageShoppingDirectory = 5;
        /// <summary>
        /// 手机验证码键值
        /// </summary>
        public const string  MobileYZCode = "MobileYZCode";
        public const string  MobileSending = "MobileSending";
        public const string  MobileSendPreTime = "MobileSendPreTime";
        public const string  MobileNo= "MobileNo";
        /// <summary>
        /// 注册的手机号码标记
        /// </summary>
        public const string  MobileNoRegister = "MobileNoRegister";
        public const string  MobileNoFogotPwd = "MobileNoFogotPwd";
        public const string  FindPwdID= "FindPwdID";
        /// <summary>
        /// 微信唯一吗键值
        /// </summary>
        public const string WeChatUnionId = "WeChatUnionId";

        /// <summary>
        /// 品牌列表记录数
        /// </summary>
        public const int PageSizeBrand= 20;

        /// <summary>
        /// 分类列表记录数
        /// </summary>
        public const int PageSizeClass = 10;

        /// <summary>
        /// 分类列表记录数
        /// </summary>
        public const int PageSizeHelp = 30;


        /// <summary>
        /// 分类列表记录数
        /// </summary>
        public const int PageSizeFinance = 20;

        /// <summary>
        ///  个人增值税发票记录数
        /// </summary>
        public const int PageSizeMemBillVAT = 5;


        /// <summary>
        ///  个人增值税发票记录数
        /// </summary>
        public const int PageSizeCheckPersonalInfo = 2;

        /// <summary>
        /// 常规产品页大小
        /// </summary>
        public const int ConventionalPListPageSize = 5;

        /// <summary>
        /// 特价产品页大小
        /// </summary>
        public const int SpecialPListPageSize = 5;

        /// <summary>
        /// 特价产品页大小
        /// </summary>
        public const int B2BNoticePageSize = 5;

        /// <summary>
        /// 特价产品页大小
        /// </summary>
        public const int PCarTypeManagePageSize = 5;

        /// <summary>
        /// 采购订单提交的纵向横向列表显示记录数
        /// </summary>
        public const int CGProductLTPageSize = 10;

        /// <summary>
        /// 订单显示记录数
        /// </summary>
        public const int CGPageSizeOrder = 10;

        /// <summary>
        /// 采购订单处理记录数
        /// </summary>
        public const int CGOrderHandlePageSize = 10;

        /// <summary>
        /// 采购供应商记录数
        /// </summary>
        public  const  int CGSuppliersPageSize = 10;  

    }


    /// <summary>
    /// 短信通知枚举
    /// </summary>
    public class SMSCodeCollection
    {
        /// <summary>
        /// 公司注册审核通过
        /// </summary>
        public const string RegisterCompanyCheck = "RegisterCompanyCheck";
        /// <summary>
        /// 公司注册审核拒绝
        /// </summary>
        public const string RegisterCompanyCheckR = "RegisterCompanyCheckR";
        /// <summary>
        /// 个人注册审核通过
        /// </summary>
        public const string RegisterPersonalCheck = "RegisterPersonalCheck";
        /// <summary>
        /// 个人注册审核拒绝
        /// </summary>
        public const string RegisterPersonalCheckR = "RegisterPersonalCheckR";
        /// <summary>
        /// 采购系统用户注册审核通过
        /// </summary>
        public const string CGRegisterCheck = "CGRegisterCheck";
        /// <summary>
        /// 采购系统用户注册审核拒绝
        /// </summary>
        public const string CGRegisterCheckR = "CGRegisterCheckR";
        /// <summary>
        /// 采购竞价通知短信
        /// </summary>
        public const string CGNoteBiddingOrder = "CGNoteBiddingOrder";
        /// <summary>
        /// 采购订单推送短信通知
        /// </summary>
        public const string CGNotePushOrder = "CGNotePushOrder";
        /// <summary>
        /// 退换货通知短信
        /// </summary>
        public const string NoteReback = "NoteReback";
        /// <summary>
        /// 退换货收货通知短信
        /// </summary>
        public const string NoteRebackRecived = "NoteRebackRecived";
    }

    public class SMSProviders
    {
        /// <summary>
        /// 助通科技
        /// </summary>
        public const string ZhuTongKeJi = "ZhuTongKeJi";
        /// <summary>
        /// 创世漫道
        /// </summary>
        public const string ChuangShiManDao = "ChuangShiManDao";
    }
}
