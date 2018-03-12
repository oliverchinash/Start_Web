using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Model
{


    /// <summary>
    /// 网址枚举
    /// </summary>
    public class WebUrlEnum
    { 
        /// <summary>
        /// 询价订单
        /// </summary>
        public const string InquiryCGMemNote = "/MemSupplier/QuoteDetailPreEdit?code={0}";
        /// <summary>
        /// 订单确认通知
        /// </summary>
        public const string InquiryMemConfirm =   "/InquiryMem/InquirySelect?code={0}";
        /// <summary>
        /// 系统确认审核供应商报价,选择供应商，前置系统管理网址
        /// </summary>
        public const string InquirySysMemCheck = "/SysOrderInquiry/CGMemSelect?code={0}";
        /// <summary>
        /// 确认采购供应商
        /// </summary>
        public const string ConfirmSysMemCheck = "/SysOrderConfirm/CGMemSelect?code={0}";
        /// <summary>
        /// 后台浏览询价单
        /// </summary>
        public const string InquiryOrderReadForSys = "/SysOrderInquiry/OrderDetail?code={0}";
        /// <summary>
        /// 供应商推出  前置询价系统网址
        /// </summary>
        public const string InquiryDeliveryNote = "/Check/InquiryWL?code={0}";
        /// <summary>
        /// 供应商询价查看页，前置询价系统网址
        /// </summary>
        public const string InquiryCGStockNote = "/MemSupplier/QuoteDetailRead?code={0}";
        /// <summary>
        /// 供应商备货查看页，前置询价系统网址
        /// </summary>
        public const string ConfirmCGStockNote = "/MemSupplier/ConfirmDetailRead?code={0}";

    }

}
