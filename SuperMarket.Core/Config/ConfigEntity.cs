using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core
{
    [Serializable()]
    public class ConfigEntity
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        public string WebName
        {
            get;
            set;
        }
        
        /// <summary>
        /// 站点Id(备用)
        /// </summary>
        public int SiteId
        {
            get;
            set;
        }
        /// <summary>
        /// 是否生成静态页
        /// </summary>
        public int CreateHtml
        {
            get;
            set;
        }
      
        /// <summary>
        /// 是否开放及时送菜单
        /// </summary>
        public int JiShiSong
        {
            get;
            set;
        }
        
        public List<HTMLPageEntity> HTMLPages;
        public List<PicConPressEntity> PicConPress;
    }

    public class CommonConfigEntity
    {
        public string ApiPath;
    }
    public class JsVersionEntity
    {
        public string GlobalCarts;
    }
    public class CssVersionEntity
    {
        public string MemberStyle;
    }
    [Serializable()]
    public class HTMLPageEntity
    {
        public string Name;
        public string Version;
    }
    public class MobileMessageConfig
    {
        public string Name;
        public string AppId;
        public string UserCode;
        public string PassWord;
        public string Url;
        public string MsgBody;
    }

    public class FtpImagesSysPath
    {
        public int Code;
        public string Name;
        public string Path;

    }

    public class ImagesSysPathCode
    {
        /// <summary>
        /// 默认的未知图片路径
        /// </summary>
        public const int Default = 1000;
        /// <summary>
        /// 产品图
        /// </summary>
        public const int Product = 1001;
        /// <summary>
        /// 产品详情图
        /// </summary>
        public const int ProductDescription = 1002;
        /// <summary>
        /// 证件相关
        /// </summary>
        public const int Certifacate = 1003;
        /// <summary>
        /// 退换货相关
        /// </summary>
        public const int ProductReturn = 1004;
        /// <summary>
        /// 证件相关
        /// </summary>
        public const int CertifacateCG = 1005;
        /// <summary>
        /// 询价产品
        /// </summary>
        public const int InquiryImg = 1006;

    }
    public class ImagesSysPathCodeCG
    {
        /// <summary>
        /// 默认的未知图片路径
        /// </summary>
        public const int Default = 2000;
        /// <summary>
        /// 产品图
        /// </summary>
        public const int Product = 2001;
        /// <summary>
        /// 产品详情图
        /// </summary>
        public const int ProductDescription = 2002;
        /// <summary>
        /// 证件相关
        /// </summary>
        public const int Certifacate = 2003;
        /// <summary>
        /// 退换货相关
        /// </summary>
        public const int ProductReturn = 2004;

    }

    public class PicConPressEntity  
    {
      public int Width;
      public int Height;
}
}
