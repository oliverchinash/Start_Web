using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core
{
    [Serializable()]
    public class ConfigCommonEntity
    {
     
        /// <summary>
        /// 列表页默认显示记录数
        /// </summary>
        public int PageSize
        {
            get;
            set;
        }
        /// <summary>
        /// 图片服务器url
        /// </summary>
        public string PictureUrl
        {
            get;
            set;
        }
        /// <summary>
        ///  静态HTML服务器
        /// </summary>
        public string HtmlService
        {
            get;
            set;
        }
        /// <summary>
        ///  静态HTML 路径
        /// </summary>
        public string Site_HtmlPath
        {
            get;
            set;
        }
        /// <summary>
        ///  图片服务器IIS访问路径
        /// </summary>
        public string ImagesServerUrl
        {
            get;
            set;
        }
        /// <summary>
        /// FTP服务器
        /// </summary>
        public string FtpServer
        {
            get;
            set;
        }
        /// <summary>
        ///  FTP访问用户名
        /// </summary>
        public string FtpUserName
        {
            get;
            set;
        }
        /// <summary>
        ///  FTP访问密码
        /// </summary>
        public string FtpPassWord
        {
            get;
            set;
        }
        /// <summary>
        ///  FTP保存图片文件的根目录(与图片服务器网址匹配）
        /// </summary>
        public string FtpImagesRootPath
        {
            get;
            set;
        }

        /// <summary>
        ///  图片服务器访问根路径
        /// </summary>
        public string FtpImagesPath
        {
            get;
            set;
        }
        public string FtpImagesSystemName
        {
            get;
            set;
        }
       
        /// <summary>
        /// 用户cookie
        /// </summary>
        public string MemberCookieName
        {
            get;
            set;
        }
        public string RegisterMemCookieName
        {
            get;
            set;
        }
        /// <summary>
        /// 购物车Cookie
        /// </summary>
        public string ComBineCart
        {
            get;
            set;
        }
        /// <summary>
        /// Cookie域
        /// </summary>
        public string CookieDomain
        {
            get;
            set;
        }
        /// <summary>
        /// 主域名
        /// </summary>
        public string MainWebUrl
        {
            get;
            set;
        }    
        /// <summary>
             /// 主域名
             /// </summary>
        public string MainMobileWebUrl
        {
            get;
            set;
        }
        public string SysWebUrl
        {
            get;
            set;
        }
        public string SysMobileWebUrl
        {
            get;
            set;
        }
        public string MemMobileWebUrl
        {
            get;
            set;
        }
        
        /// <summary>
        /// 帮助中心主域名
        /// </summary>
        public string HelpWebUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 静态站点
        /// </summary>
        public string StaticWebUrl
        {
            get;
            set;
        }
        
        public string HelpMobileWebUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 登录域名
        /// </summary>
        public string LoginWebUrl
        {
            get;
            set;
        }
        public string LoginMobileWebUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 
        /// </summary>
        public string WeChatWebUrl
        {
            get;
            set;
        }
        /// <summary>
        /// 询价网页链接
        /// </summary>
        public string InquiryWebUrl
        {
            get;
            set;
        }
        public string InquiryMobileWebUrl
        {
            get;
            set;
        }
        public string MemberWebUrl
        {
            get;
            set;
        }
        public string MemberMobileWebUrl
        {
            get;
            set;
        }
        public int CartCookie
        {
            get;
            set;
        }
        public int XuQiuCookie
        {
            get;
            set;
        }
        /// <summary>
        /// 购物车网址
        /// </summary>
        public string DomainShopping
        {
            get;
            set;
        } 
        public string SendMailManager
        {
            get;
            set;
        }
        public JsVersionEntity JsVersion;
        public CssVersionEntity CssVersion;
        public string MobileRegMsgBody; 
    } 
}
