using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model.Common
{
    public class MemberAuthEnum
    { 
        /// <summary>
        /// 系统配置
        /// </summary>
        public const string SysConfigure = "SysConfigure";
        /// <summary>
        /// 基础信息编辑 
        /// </summary>
        public const string BasicInfoManager = "BasicInfoManager";
        /// <summary>
        /// 产品信息编辑 
        /// </summary>
        public const string ProductInfoManager = "ProductInfoManager";
        #region 询价系统权限
        /// <summary>
        /// 询价单列表查询
        /// </summary>
        public const string InquiryOrderListManager = "InquiryOrderListManager";
        /// <summary>
        /// 询价单报价审核
        /// </summary>
        public const string InquiryOrderQuoteCheck = "InquiryOrderQuoteCheck"; 
        /// <summary>
        /// 询价单虚拟报价
        /// </summary>
        public const string InquiryOrderQuote = "InquiryOrderQuote";
        /// <summary>
        /// 询价订单送货
        /// </summary>
        public const string InquiryOrderDelivery = "InquiryOrderDelivery";
        #endregion
    }
    public class MemberRoleEnum
    {
        /// <summary>
        /// 系统管理员
        /// </summary>
       public const string SysManage = "SysManage";
        /// <summary>
        /// 报价管理员
        /// </summary>
        public const string BaoJiaManage = "BaoJiaManage";
        /// <summary>
        /// 送货员
        /// </summary>
        public const string SongHuoMan = "SongHuoMan";
        /// <summary>
        /// 超级询价管理员
        /// </summary>
        public const string XJSuperManager = "XJSuperManager";

    }
}
