using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Model.Const
{
    public static class EnumDicTionary
    {  
        /// <summary>
        /// 角色管理
        /// </summary> 
        public const string RoleManageCode = "MenuRoleManage";
        /// <summary>
        /// 权限管理编码
        /// </summary>
        public const string  AuthorityManageCode = "MenuAuthorityManage";
         
        /// <summary>
        /// 登陆用户IDCookie
        /// </summary>
        public const string MemberCookieID = "MemberLoginID";
        /// <summary>
        /// 登陆用户名Cookie
        /// </summary>
        public const string MemberCookieCode = "MemberLoginCode";
        /// <summary>
        /// 登陆用户Cookie名
        /// </summary>
        public const string MemberCookieName = "MemberLoginName";
        /// <summary>
        /// 登陆用户Cookie实体
        /// </summary>
        public const string MemberCookie = "MemberLoginEntity";
        /// <summary>
        /// 默认密码
        /// </summary>
        public const string DefaultPass = "000000";

        /// <summary>
        /// 保存语言种类
        /// </summary>
        public const string LanageCookieid = "LanageCookieid";

    }

    public static class EnumSysDic
    {
        /// <summary>
        /// 连接字符串名称
        /// </summary>
        public const string ConnectionString = "ConnectionString";

        public const string SysDB = "ConnectionString";
    }
}
