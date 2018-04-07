using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{
    /// <summary>
    /// 为防止出现二义性，特只赋值负数 
    /// </summary>
    public enum CommonStatus
    {
        [Description("成功")]
        Success = -1,
        [Description("失败")]
        Fail = -2,
        [Description("异常")]
        Exception = -3,
        [Description("服务器错误")]
        ServerError = -4,
        #region B2B平台
        [Description("申请成功，请等待管理员审核,我们会在一个工作日内联系您")]//商家申请
        SuccessApply = -11,
        [Description("审核通过，如需要修改请联系客服")]
        Applyed = -12,
        [Description("审核中，如需要修改请联系客服")]
        Checking = -13,
        [Description("添加成功")]
        ADD_Success = -1001,
        [Description("添加失败")]
        ADD_Fail = -1002,
        [Description("添加失败,记录已存在,请检查编号和名称")]
        ADD_Fail_Exist = -1003,
        [Description("添加失败,缺少父级或是空记录")]
        ADD_Fail_Empty = -1004,
        [Description("添加失败,缺少分类或品牌")]
        ADD_Fail_EmptyClass = -1005,
        [Description("编辑成功")]
        Update_Success = -1011,
        [Description("编辑失败")]
        Update_Fail = -1012,
        [Description("编辑失败,记录已存在,请检查编号和名称")]
        Update_Fail_Exist = -1013,
        [Description("编辑失败,缺少父级或是空记录")]
        Update_Fail_Empty = -1014,
        [Description("登录成功")]
        LoginSuccess = -1101,
        [Description("登录失败")]
        LoginFail = -1102,
        [Description("没有这个账号")]
        LoginNoMemCode = -1103,
        [Description("账号密码不正确")]
        LoginError = -1104,
        [Description("账号密码不能为空")]
        LoginEmpty = -1105,
        [Description("账号待审核")]
        LoginStatusWait = -1106,
        [Description("账号已锁定")]
        LoginStatusLock = -1107,
        [Description("注册成功")]
        RegisterSuccess = -1201,
        [Description("注册失败")]
        RegisterFail = -1202,
        [Description("已有这个账号")]
        RegisterHasMember = -1203,
        [Description("注册验证码不正确")]
        RegisterErrorVerCode = -1204,
        [Description("密码或用户名为空")]
        RegisterEmpty = -1205,
        [Description("间隔时间不超过1分钟")]
        LatelyTime = -1206,
        [Description("一天登录次数超过20次")]
        ExceedDay = -1207,
        [Description("您已注册成功了，请直接登录")]
        HasAccount = -1208,
        [Description("手机号码已存在，如有疑问，请联系客服")]
        PhoneExist = -1209,
        [Description("验证码发送中，请稍候")]
        PhoneSendGap = -1210, 
        [Description("注册验证码已过期")]
        RegisterVerCodeHasExDay = -1211, 
        [Description("验证码不正确")]
        VerCodeIsError = -1212,
        [Description("手机号码不存在")]
        PhoneNotExist = -1213,
        [Description("需要注册")]
        NeedLogin = -1214,
        [Description("当前用户注册信息不能修改")]
        RegisterNoModify = -1215,

        [Description("密码不能为空")]
        PassWordEmpty = -1301,
        [Description("重复的名称")]
        DBRepetName = -1401,
        [Description("重复的公司名称")]
        DBRepetComName = -1402,
        [Description("重复的店铺名称")]
        DBRepetSuppName = -1403,
        [Description("收货地址数量太多了")]
        AddressNumSuper = -1404,

        [Description("亲，您的优惠券已过期了")]
        CouponsError = -1405,
        [Description("亲,您的增票未审核，请联系客服")]
        BillVATNoCheck = -1501,
        [Description("亲,您需要的产品已下架或库存不足，请从购物车重新下单")]
        ProductLess = -1502,
        [Description("亲,发票抬头必须填写")]
        BillTitleIsEmpty = -1503,

        [Description("亲,您的订单目前不能取消，请联系客服处理")]
        OrderNoCancel = -1601,
        [Description("亲,您的订单已取消，无需重复取消")]
        OrderHasCancel = -1602,
        [Description("订单不存在")]
        OrderNotExist = -1603,
        [Description("当前订单不能进行此操作")]
        OrderCanNotDo = -1604,
        [Description("亲,当前订单不能评论额")]
        StatusNoEvaluate = -1701,
        [Description("亲,当前订单已评论了")]
        HasEvaluated = -1702,
        [Description("亲，退换货数量超过该物品可退换货数量")]
        ReturnNumExceed = -1703,
        [Description("亲，该订单已支付成功")]
        HasPayed = -1704,

        [Description("亲，产品对应的车型已存在")]
        HasCarType = -1801,

        [Description("亲，您没有权限执行此操作")]
        HasNoAuth = -1802,
        #endregion
        #region 采购平台
        [Description("亲，您选择的纵向件已存在产品列表中")]
        HasProductsL = -2101,
        [Description("亲，您选择的横向件已存在产品列表中")]
        HasProductsT = -2102,
        [Description("亲，对应订单状态不能维护发货信息")]
        CanNotDelevery = -2201,
        [Description("亲，订单不是您的额！")]
        CGOrderIsOrther = -2202,
        [Description("亲，凭证已上传，如果要重新上传，请联系客服更改")]
        CGOrderHasUpload = -2203,
        [Description("亲，您已对该订单报过价了")]
        CGOrderHasQuote = -2204,
        [Description("亲，产品报价超过限价啦")]
        CGOrderQuotePriceExt = -2205,
        #endregion

        #region 后台系统
        [Description("亲，退款金额超限了")]
        AddPayRebackBig = -9001,
        [Description("亲，退回积分超限了")]
        RebackIntegralBig = -9002,
        [Description("亲，积分已退回")]
        HasRebackIntegral = -9003,
        [Description("亲，您选择的订单已添加到线下财务确认列表中")]
        HasAddToLineConfirm = -9101,
        [Description("亲，该退换货数量与分配的供应商退货数量不一致")]
        RebackNumNoValid = -9102,
        [Description("亲，新的订单号不正确或状态不正确")]
        NewOrderIsError = -9103,
        #endregion
         
    }


    /// <summary>
    /// 客户端类型
    /// </summary>
    public enum ClientTypeEnum
    {
        [Description("电脑端")]
        PC = 1,
        [Description("安卓APP")]
        android = 2,
        [Description("苹果APP")]
        IOS = 3,
        [Description("手机网站")]
        Mobile = 4,
    }

    /// <summary>
    /// 用户等级
    /// </summary>
    public enum MemberGrade
    {
        [Description("普通客户")]
        Normal = 1,
        [Description("铜牌客户")]
        Copper = 2,
        [Description("银牌客户")]
        Silver = 3,
        [Description("金牌客户")]
        Gold = 4,
        [Description("钻石客户")]
        Diamonds = 5,
        [Description("普通商家")]
        Store = 10,
    }
    /// <summary>
    /// 商家级别
    /// </summary>
    public enum StoreGrade
    {
        [Description("普通商家")]
        Normal = 1,
        [Description("铜牌商家")]
        Copper = 2,
        [Description("银牌商家")]
        Silver = 3,
        [Description("金牌商家")]
        Gold = 4,
        [Description("钻石商家")]
        Diamonds = 5,

    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum MemberType
    {
        [Description("普通用户")]
        Store = 1,
        [Description("供应商")]
        Supplier = 2,
        [Description("系统用户")]
        Sys = 3,
    }

    /// <summary>
    /// 用户状态
    /// </summary>
    public enum MemberStatus
    {
        [Description("注册中")]
        Registing = 0,
        [Description("活跃用户")]
        Active = 1,
        [Description("审核中")]
        OnChecking = 2,
        [Description("锁定用户")]
        IsLock = 3,
        [Description("审核拒绝")]
        Rejected = 4,
        [Description("修改原资料后再次要求审核")]
        WaitChecked = 5,
        [Description("待审核")]
        WaitCheck = 10,
        [Description("注册第一步")]
        Register1 = 11,
        [Description("注册第二步")]
        Register2 = 12,
    }
    public enum StoreStatus
    {
        [Description("待审核")]
        WaitCheck = 0,
        [Description("活跃用户")]
        Active = 1,
        [Description("审核中")]
        OnChecking = 2,
        [Description("锁定用户")]
        IsLock = 3,
        [Description("审核拒绝")]
        Rejected = 4,
        [Description("修改原资料后再次要求审核")]
        WaitChecked = 5
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CertificateType
    {
        [Description("身份证正面")]
        IdentityPositive = 1,
        [Description("身份证反面")]
        IdentityOpposite = 2,
        [Description("营业执照")]
        BusinessLicense = 3,
        [Description("用户头像")]
        HearPic = 4,
        [Description("评论图片")]
        EvaluatePic = 5,
        [Description("供应商资质")]
        OfferLicense = 6,
        [Description("询价提交产品相关图")]
        InquiryImg = 7,
        [Description("询价提交Vin码图片")]
        InquiryVin = 8,
        [Description("询价提交发动机号图片")]
        InquiryEngine = 9,
        [Description("品牌商标")]
        BrandTag = 11,
    }


    public enum OrderStatus
    {
        [Description("全部状态")]
        AllStatus = 0,
        [Description("等待付款")]
        WaitPay = 1,
        [Description("等待确认")]
        WaitDeal = 2,
        [Description("等待发货")]
        WaitDeliver = 3,
        [Description("等待收货")]
        WaitRecive = 4,
        [Description("完成")]
        Finished = 5,
        [Description("取消订单")]
        Cancel = 7,
        /// <summary>
        /// 财务确认后发货前取消订单
        /// </summary>
        [Description("取消订单申请")]
        CancelApp = 8,
        [Description("备货中")] 
        ChoicingCargo = 10,
        [Description("发货中")]
        Delivering = 11,
        [Description("已退货")]
        HasReturned = 21,
        [Description("需求提交")]
        XuQiuSubmit = 30, 
        [Description("已关闭")]
        Close = 100,
    }

    public enum FinancialStatus
    {
        [Description("默认值")]
        Default = 0,
        [Description("待财务确认")]
        WaitChecked = 1,
        [Description("财务已确认")]
        Checked = 2,
        [Description("财务拒绝")]
        NoRecived = 3,
        [Description("待退款")]
        WaitReback = 4,
        [Description("已退款")]
        HasReback = 5,
    }
    public enum OrderTerm
    {
        [Description("近三十天内订单")]
        OneMonth = -1,
        [Description("近三个月订单")]
        Default = 0,
        [Description("今年内订单")]
        YearThis = 1,
        [Description("去年订单")]
        YearLast = 2,
        [Description("前年订单")]
        YearPrevious = 3
    }
    public enum PayLineConfirm
    {
        [Description("待审核")]
        Default = 0,
        [Description("审核通过")]
        Pass = 1,
        [Description("审核拒绝")]
        Reject = 2,
    }
    public enum UpdateError
    {
        [Description("名称重复")]
        NameRepet = -1,
        [Description("公司名称重复")]
        CompanyNameRapet = -2,
        [Description("店铺名称重复")]
        YearLast = 2,
        [Description("前年订单")]
        YearPrevious = 3
    }
   
    /// <summary>
    /// 分类属性类型
    /// </summary>
    public enum PropertyType
    {
        [Description("普通属性")]
        Normal = 0,
        [Description("颜色")]
        Color = 1,
        [Description("尺码")]
        Size = 2
    }
    /// <summary>
    /// 排序类型
    /// </summary>
    public enum OrderByType
    {
        [Description("默认排序")]
        Default = 0,
        [Description("销量降序")]
        SaleNum = 1,
        [Description("价格升序")]
        PriceAsc = 2,
        [Description("价格降序")]
        PriceDesc = 3,
        [Description("id倒序")]
        IdDesc = 4,
        [Description("名称升序")]
        NameAsc = 5,
        [Description("上架时间")]
        CreateTime = 6
    }
    ///// <summary>
    ///// 邮费种类
    ///// </summary>
    //public enum OrderPostFeeType
    //{
    //    [Description("固定邮费")]
    //    Default = 0,
    //    [Description("常规商品")]
    //    Free = 1,
    //    [Description("大件商品")]
    //    Big = 2,
    //    [Description("特殊商品")]
    //    Special = 3,
    //    [Description("运费到付")]
    //    PayBehind = 4
    //}

    public enum TransFeeTypeEnum
    {
        [Description("运费到付")]
        PayBehind = 1,
        [Description("免运费")]
        Free = 2

    }

    public enum OrderType
    {
        [Description("电话")]
        Default = 0,
        [Description("网上")]
        OnLine = 1,
        [Description("线下")]
        OffLine = 2,
        [Description("换货")]
        ExchangeGood = 3
    }
    public enum OrderStyleEnum
    {
        [Description("普通订单")]
        Normal = 1,
        [Description("需求订单")]
        XuQiu = 2,
    }
    public enum ReturnOrderStatus
    {
        [Description("申请中")]
        Applying = 0,
        [Description("审核通过")]
        Adopt = 1,
        [Description("审核拒绝")]
        Reject = 2,
        [Description("退货中")]
        Returning = 3,
        [Description("退换货完成")]
        Complete = 4,
        [Description("待退款")]
        WaitForRefund = 21,
        [Description("已退款")]
        HasRebackPay = 22,
        [Description("待发货")]
        WaitForDelivery = 13,
        [Description("已发货")]
        HadDeliveried = 14
    }


    public enum ReturnOrderTerm
    {
        [Description("三个月内")]
        ThreeMonth = 0,
        [Description("半年内")]
        HalfYear = 1,
        [Description("今年")]
        ThisYear = 2,
        [Description("去年")]
        LastYear = 3,
        [Description("前年")]
        PreviousYear = 4
    }
    public enum ReturnXSWLStatus
    {
        [Description("供应商未收货")]
        Default = 0,
        [Description("供应商已收货")]
        HasRecived = 1,
    }
    public enum ReturnType
    {
        [Description("退货")]
        ReturnGoods = 1,
        [Description("换货")]
        ExchangeGoods = 2,
    }

    public enum PayType
    {
        [Description("支付宝")]
        AliPay = 1,
        [Description("微信")]
        WeChat = 2,
        [Description("银联")]
        UnionPay = 3,
        [Description("线下支付")]
        OutLine = 4,
        [Description("支付宝手机端网站")]
        AliPayMobile = 5,
        [Description("货到付款")]
        Delivery = 6,
    }
    public enum ProductPicShowType
    {
        [Description("产品图片与款式图片合并显示")]
        Combine = 1,
        [Description("只显示产品图片")]
        Product = 2,
        [Description("只显示款式图片")]
        Style = 3
    }
    /// <summary>
    /// 发票类型
    /// </summary>
    public enum BillType
    {
        [Description("无需发票")]
        NoNeed = 0,
        [Description("普票")]
        Normal = 1,
        [Description("增值税发票")]
        VAT = 2
    }
    public enum CompanyType
    {
        [Description("4S店")]
        Shop4S = 1,
        [Description("维修厂")]
        Maintenance = 2,
        [Description("经销商")]
        Agency = 3,
        [Description("厂家")]
        Manufactor = 4
    }
    
    /// <summary>
    /// 评论状态
    /// </summary>
    public enum CommentStatus
    {
        [Description("未审核")]
        NotAudited = 0,
        [Description("审核发表")]
        AuditPublished = 1,
        [Description("审核不发表")]
        AuditNotPublished = 2
    }

    public enum ProductType
    {
        [Description("普通商品")]
        Normal = 1,
        [Description("特价商品")]
        SpecialPrice = 2,
        [Description("爆品")]
        BaoPin = 3,
        [Description("每日一抢")]
        DailyGrab =4,
    }

    public enum MemBillStatus
    {
        [Description("待审核")]
        WaitToAudit = 0,
        [Description("审核通过")]
        AuditPass = 1,
        [Description("已删除")]
        Deleted = 2,
        [Description("审核拒绝")]
        AuditReject = 3

    }
    /// <summary>
    /// 平台类型
    /// </summary>
    public enum SystemType
    {
        [Description("B2B电商平台")]
        B2B = 1,
        [Description("B2B电商平台后台")]
        B2BBehind = 11,
        [Description("采购平台")]
        Purchase = 2,
        [Description("采购平台后台")]
        PurchaseBehind = 3,
        [Description("B2B电商手机网站")]
        B2BMobile = 4,
    }
     public enum SearchMethod
    {
        [Description("拼音简称")]
        PY = 1,
        [Description("拼音全称")]
        PingYing = 2,
        [Description("名称")]
        Name = 3,
    }
    public enum CGOrderOfferStatus
    {
        [Description("待接单")]
        OrderWait = 0,
        [Description("已接单")]
        OrderTaken = 1,
        [Description("错过订单")]
        OrderMissed = 2
    }
    public enum RefundStatus
    {
        [Description("待退款")]
        Wait = 0,
        [Description("已退款")]
        HasRefund = 1,
        [Description("拒绝")]
        Reject = 2
    }
    public enum SyncCGStatus
    {
        [Description("待同步")]
        WaitSync = 0,
        [Description("已同步")]
        Synced = 1,
        [Description("同步出错")]
        SyncError = 2,
        [Description("同步中")]
        Syncing = 11
    }
    /// <summary>
    /// 采购产品申请状态
    /// </summary>
    public enum CGProductAppStatus
    {
        [Description("未申请")]
        Default = 0,
        [Description("申请中")]
        Apping = 1,
        [Description("申请通过")]
        Accept = 2,
        [Description("拒绝")]
        Reject = 3,
        [Description("用户取消")]
        MemCancel = 4
    }
    public enum SMSNoticeStatus
    {
        [Description("待发送")]
        WaitSend = 0,
        [Description("已发送")]
        HasSend = 1,
        [Description("发送失败")]
        SendError = 2,
    }
    /// <summary>
    /// 0待打印发货单，1待发货，2已发货，3已收货，4待收款，5已收款，100已关闭
    /// </summary>
    public enum CGOrderTake
    {
        [Description("待打印发货单")]
        WaitPrint = 0,
        [Description("待发货")]
        WaitSend = 1,
        [Description("已发货")]
        HasSend = 2,
        [Description("待审核收货凭证")]
        WaitCheckCer = 21,
        [Description("已收货")]
        HasRecived = 3,
        [Description("待收款")]
        WaitPay = 4,
        [Description("已收款")]
        HasPay = 5,
        [Description("已取消")]
        Cancel = 6,
        [Description("已关闭")]
        Closed = 100,
    }
    /// <summary>
    /// 状态：0待报价：4进入线下客服处理流程，11已派单,12供应商已打印发货单，13供应商已发货，14供应商上传客户收货凭证，15送货完成，21财务批准打款，22款已支付，
    /// </summary>
    public enum CGOrderStatus
    {
        [Description("待报价")]
        WaitBJ = 0,
        [Description("待接收")]
        WaitRecived = 1,
        [Description("进入线下客服处理流程")]
        Line = 4,
        [Description("已派单")]
        HasTake = 11,
        [Description("供应商已发货")]
        HasDelivery = 13,
        [Description("供应商上传客户收货凭证")]
        UploadRecivered = 14,
        [Description("送货完成")]
        Finished = 15,
        [Description("财务批准打款")]
        WaitPay = 21,
        [Description("款已支付")]
        HasPay = 22,
        [Description("订单取消")]
        Cancel = 31,
        [Description("已退货")]
        HasReback = 32,
        [Description("订单关闭")]
        Closed = 99,
    }
    /// <summary>
    /// 0待申请，1申请通过，2拒绝，3已收货，4已寄出，100结束
    /// </summary>
    public enum CGReturnStatus
    {
        [Description("待申请")]
        Apply = 0,
        [Description("申请通过")]
        Accept = 1,
        [Description("拒绝")]
        Reject = 2,
        [Description("已收货")]
        HasRecived = 3,
        [Description("新订单已寄出")]
        HasDelivery = 4,
        [Description("结束")]
        Closed = 100,
    }
    public enum ClassType
    {
        [Description("汽车用品")]
        Automobile = 1,
        [Description("乘用车配件")]
        PJCYC = 7,
        [Description("商用车配件")]
        PJSYC = 6,
        [Description("工程机械")]
        PJGCJX = 5,
        [Description("辅材")]
        FC = 3,
        [Description("汽修工具")]
        QXGJ = 4,
        [Description("通用件")]
        TYJ = 2,
    }
    /// <summary>
    /// 分类列表种类
    /// </summary>
    public enum ClassMenuTypeEnum
    {
        [Description("默认基础分类")]
        Default = 1,
        [Description("通用分类")]
        Normal = 2,
        [Description("PC端分类")]
        NormalPC = 3,
        [Description("及时达通用分类")]
        JiShiDa = 4,

    }
    /// <summary>
    /// 列表方式
    /// </summary>
    public enum ListShowMethodEnum
    {
        [Description("默认列表")]
        Default = 1,
        [Description("横向列表")]
        Normal = 2  
    }
    //所属站点
    public enum SiteEnum
    {
        [Description("乘用车")]
        Default = 1,
        [Description("商用车")]
        SYC = 2,
    }
    public enum ReBackIntegralStatus 
    {   [Description("待退")]
        WaitReback = 0,
            [Description("已退")]
        HasReback = 1, 
    }
    public enum WLComPany
    {
        [Description("普通配送")]
        Normal = 0,
        [Description("顺丰")]
        SF = 1,
        [Description("圆通")]
        YT = 2,
        [Description("韵达")]
        YD = 3,
        [Description("中通")]
        ZT = 4,
        [Description("申通")]
        ShengTong = 5, 
    }
    /// <summary>
    /// 类型：1爆品产品，11爆品品牌，2限量乐购，
    /// </summary>
    public enum ProductMenuType
    {
        [Description("爆品")]
        BPProduct =1,
        [Description("爆品品牌")]
        BPBrand = 11,
        [Description("限量乐购")]
        XLLG = 2,
    }
    /// <summary>
    /// 优惠券类型：1金额优惠券，2折扣券
    /// </summary>
    public enum CouponTypeEnum
    {
        [Description("1金额优惠券")]
        Money = 1,
        [Description("2折扣券")]
        DisCount = 2,
    }
    /// <summary>
    /// 询价状态
    /// </summary>
    public enum InquiryStatus
    {
        [Description("1待处理")]
        WaitDeal = 1,
        [Description("2已处理")]
        HasDeal = 2,
        [Description("3已作废")]
        Cancel = 3,
    }
    public enum JiShiSongEnum
    {
        [Description("及时送")]
        JiShi = 1,
        [Description("非及时送产品")]
        Normal = 2, 
    }
    #region 手机网站

    /// <summary>
    /// 产品精选类型
    /// </summary>
    public enum ProductFineTypeEnum
    {
        [Description("手机首页产品精选")]
        MobileHome = 1,
        [Description("产品页产品精选")]
        ProductDetail = 2, 
        [Description("及时送手机首页产品精选")]
        MobileHomeJiShiSong = 3,
    }
    /// <summary>
    /// 导航类型标记
    /// </summary>
    public enum NavMenuTypeEnum
    {
        [Description("手机首页分类导航")]
        MobileHome = 11,
    }

    public enum DicCouponsEnum
    {
        [Description("10元现金券,1000元启用")]
        Cash10_1000 = 1,
        [Description("50元现金券,5000元启用")]
        Cash50_5000 = 2,
        [Description("5元优惠券,6元启用")]
        Cash5_6  = 3,
        [Description("10元优惠券,11元启用")]
        Cash10_11 = 4,
    }
    /// <summary>
    /// 首页分类的广告显示属性，1代表品牌显示，2代表产品显示
    /// </summary>
    public enum ClassesAdShowType
    {
        [Description("品牌")]
        Brand = 1,
        [Description("产品")]
        Product = 2,
    }
    /// <summary>
    /// 产品专区类型
    /// </summary>
    public enum ProductSpecialType
    {
        [Description("通用")]
        Normal = 1,
        [Description("隐藏")]
        Hide = 2,
        [Description("PC首页显示")]
        PCWeb = 3,
    }

    public enum WeiXinNewsTypeEnum
    {
        [Description("行业资讯")]
        HYZX = 1,
        [Description("维修案例")]
        WXAL = 2,
        [Description("店铺管理")]
        DPGL = 3,
        [Description("轻松一笑")]
        QSYX = 4,
    }
    #endregion

  

    public enum LoginMethodEnum
    {
        [Description("账号登录")]
        Code = 1,
        [Description("手机登录")]
        MobilePhone = 2,
        [Description("微信登录")]
        WeChat = 3,
        [Description("根据Id获取")]
        MemId = 4,
        [Description("根据临时编号获取")]
        TempCode = 5,
    }

    public enum OrderInquiryStatusEnum
    {
        [Description("订单编辑")]
        Edit = 0,
        [Description("订单提交")]
        Submit = 1,
        [Description("订单接收")]
        Checked = 2,
        [Description("供应商报价中")]
        Quoting = 3,
        [Description("待客户确认价格")]
        WaitAccept = 4,    
        [Description("已确认订单")]
        HasAccept = 13,
        [Description("报价超时")]
        QuotingOvertime = 14,
        [Description("取消订单")]
        Cancel = 99,
        [Description("已完成")]
        Finished = 100,
    }
    public enum InquiryStatusForMemEnum
    {
        [Description("订单编辑")]
        Edit = 0,
        [Description("订单提交")]
        Submit = 1,
        [Description("报价中")]
        Quoting = 2,
        [Description("待确认")]
        WaitAccept = 3, 
        [Description("报价超时")]
        QuotingOvertime = 6,
        [Description("完成")]
        Finished = 100,
        [Description("已取消")]
        Cancel = 99,
        [Description("结束")]
        Closed = 98,
    }

    public enum OrderConfirmStatusEnum
    { 
        [Description("订单提交")]
        Submit = 1,
        [Description("待付款")]
        WaitPay = 2,
        [Description("待选择供应商")]
        WaitSelectCGMem = 3,
        [Description("待选择物流通道")]
        WaitSelectDeliveryMan = 4,
        [Description("待发货")]
        WaitDelivery = 5,
        [Description("发货中")]
        Delivering = 6,
        [Description("取消订单")]
        Cancel = 99,
        [Description("已完成")]
        Finished = 100,
    }
    public enum ConfirmStatusForMemEnum
    {
        [Description("待提交")]
        WaitSubmit = 0,
        [Description("待支付")]
        WaitPay = 1,
        [Description("待发货")]
        WaitDelivery = 2,
        [Description("发货中")]
        Delivering = 3, 
        [Description("完成")]
        Finished = 100,
        [Description("已取消")]
        Cancel = 99,
    }

    public enum  InquiryProductTypeEnum
    {
        [Description("原厂")]
        YuanChang = 1,
        [Description("下线")]
        Submit = 2,
        [Description("精品")]
        Checked = 3,
        [Description("品牌")]
        Quoting = 4,
        [Description("普通")]
        WaitAccept = 5,
        [Description("拆车")]
        Chop = 6,
        [Description("配套")]
        Complement = 7,
    }
    public enum InquiryOrderFeedBackEnum
    {
        [Description("很好，我很满意")]
        Good = 1,
        [Description("期待价格更便宜")]
        PriceTooHigh = 2,
        [Description("其他渠道价格更便宜")]
        HasOtherChannel = 3,
        [Description("产品质量有待提高")]
        QualityLow = 4,
        [Description("其他原因")]
        OtherReason = 5,
    }

    /// <summary>
    /// 询价订单发送给供应商报价状态
    /// </summary>
    public enum QuoteStatusEnum
    {
        [Description("待发送给供应商")]
        WaitSend = 0,
        [Description("已发送给供应商")]
        HasSend = 1,
        [Description("发送失败")]
        SendFail = 2,
        [Description("供应商报价已审核")]
        HasQuote = 3,
        [Description("发送中")]
        Sendding = 99,
    }
    public enum QuoteInStock
    {
        [Description("有现货")]
        HasStock =1,
        [Description("需订货")]
        NeedReserve = 2,
    }
    public enum ScopeTypeEnum
    {
        [Description("车型件")]
        Car = 1,
        [Description("通用件")]
        Normal = 2,
    }
    #region  报表相关
    /// <summary>
    /// 询价单报表类型
    /// </summary>
    public enum ReportInquiryTypeEnum
    {
        [Description("日报表")] 
        CreateDate = 1, 
        [Description("用户日报表")]
        Mem = 2,
        [Description("月报表")]
        CreateMonth = 3,
        [Description("用户月报表")]
        MemMonth = 4,
    }
    /// <summary>
    /// 询价单报表排序类型
    /// </summary>
    public enum ReportInquiryOrderEnum
    {
        [Description("订单总数")]
        TotalNum = 1,
        [Description("确认订单总数")]
        ConfirmNum = 2,
        [Description("转化率")]
        ConfirmRate = 3,
    }
    /// <summary>
    /// 供应商询价单报价统计报表类型
    /// </summary>
    public enum ReportInquiryCGTypeEnum
    {
        //[Description("日报表")]
        //CreateDate = 1,
        [Description("供应商日报表")]
        Mem = 2,
        //[Description("月报表")]
        //CreateMonth = 3,
        [Description("供应商月报表")]
        MemMonth = 4,
    }
    /// <summary>
    /// 供应商询价单报表排序类型
    /// </summary>
    public enum ReportInquiryCGOrderEnum
    {
        [Description("订单总数")]
        TotalNum = 1,
        [Description("报价数")]
        QuoteNum = 2,
        [Description("得单数")]
        CheckedNum = 3,
        [Description("报价率")]
        QuoteRate = 4,
        [Description("得单率")]
        CheckedRate = 5,
    }
    public enum ReportConfirmOrderEnum
    {
        [Description("订单数")]
        TotalNum = 1,
        [Description("采购金额")]
        CGTotalPrice = 2,
        [Description("销售金额")]
        TotalPrice = 3,
        [Description("利润")]
        Profit = 4,
    }
    #endregion


   
}
