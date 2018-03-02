using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SuperMarket.Core.Mail
{
    public class MailTemplate
    {
        /// <summary>
        /// 邮件模板名称
        /// </summary>
        public string TemplateName
        {
            get;
            set;
        }
        /// <summary>
        /// 邮件标题
        /// </summary>
        public string Subject
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件正文
        /// </summary>
        public string Body
        {
            get;
            set;
        }

        /// <summary>
        /// 邮件服务器
        /// </summary>
        public string Server
        {
            get;
            set;
        }
        /// <summary>
        /// 帐户
        /// </summary>
        public string Account
        {
            get;
            set;
        }
        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        /// <summary>
        /// 发送者地址
        /// </summary>
        public string FromAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 接收地址
        /// </summary>
        public string ToAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 抄送地址
        /// </summary>
        public string CCAddr
        {
            get;
            set;
        }
        /// <summary>
        /// 密送地址
        /// </summary>
        public string BCCAddr
        {
            get;
            set;
        }
    }
}
