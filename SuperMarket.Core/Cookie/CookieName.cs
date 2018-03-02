using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core 
{    
    /// <summary>
    /// Cookie名称		
    /// </summary>
    public struct SysCookieName
    {
        /// <summary>
        /// 后台管理用户登录Cookie		
        /// </summary>
        public static readonly string SysUserCookieName = "SysUserCookieName";
        public static readonly string WeCharWebCode = "WeCharWebCode";

        
        ///// <summary>
        ///// 默认登录名		
        ///// </summary>
        //public static readonly string DefaultCusName = "DefaultCusName";
        ///// <summary>
        ///// 登录Cookie		
        ///// </summary>
        //public static readonly string MemberCookieName = "MemberCookieName";
        ///// <summary>
        ///// 登录Cookie		
        ///// </summary>
        //public static readonly string RegisterMemId = "RegisterMemId";

        /// <summary>
        /// 购物车Cookie	
        /// </summary>
        public static readonly string ShoppingCart_Data = "shoppingCart";
        /// <summary>
        /// 需求清单Cookie	
        /// </summary>
        public static readonly string ShoppingXuQiu_Data = "shoppingXuQiu";
        /// <summary>
        /// 购物车JSON数据
        /// </summary>
        public static readonly string ShoppingCartJSON_Data = "shoppingCartJSON";
        /// <summary>
        /// 购物车国家代码
        /// </summary>
        public static readonly string ShoppingCartJSON_CountryCode = "cartCountryCode";
        /// <summary>
        /// 购物车Cookie	
        /// </summary>
        public static readonly string ShoppingCartCount_Data = "shoppingCartCount";      
        /// <summary>
/// 需求清单Cookie	
/// </summary>
        public static readonly string ShoppingXuQiuCount_Data = "shoppingXuQiuCount";
 
        /// <summary>
        /// 是否重新获取购物车Cookie		
        /// </summary>
        public static readonly string ComBineCart = "ComBineCart";
        /// <summary>
        /// 是否重新获取需求清单Cookie		
        /// </summary>
        public static readonly string ComBineXuQiu = "ComBineXuQiu";
        /// <summary>
        /// 拆扣Cookie	
        /// </summary>
        public static readonly string DisCount_Data = "DisCount";
        
        public static readonly string CarTypeId = "CarTypeId";
        public static readonly string SysTemType = "SystemType";
        public static readonly string ClassType = "ClassType";

        public static readonly string MemBrowseLog = "MemBrowseLog";
        public static readonly string WeixinAccessCookie = "WeixinAccessCookie";
        public static readonly string WeixinAccessTicket = "WeixinAccessTicket";
        

    }
    
}
