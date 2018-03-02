 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Xml;
namespace SuperMarket.Core.Mail
{
    public class Mail
    {
        /// <summary>
        /// 后台批处理邮件
        /// </summary>
        /// <param name="title"></param>
        /// <param name="content"></param>
        /// <param name="toemail"></param>
        /// <param name="mailtype"></param>
        /// <returns></returns>
        public static int SendMail(string title, string content, string toemail,string mailtype)
        {
            try
            {
                SuperMarket.Core.Mail.MailTemplate mailTemplate = SuperMarket.Core.Mail.Mail.GetMailTemplate(mailtype);
                  
                string[] mails = toemail.Replace('，', ',').Replace(';', ',').Replace('；', ',').Split(',');
                if (mails.Length > 0)
                {
                    int i = 0;
                    MailAddress from = null;
                    if (mailTemplate != null && mailTemplate.FromAddr != "")
                    {
                        from = new MailAddress(mailTemplate.FromAddr);
                    }
                    else
                    {
                        from = new MailAddress("luogz@liveflow.com.cn");
                    }
                    //MailAddress to = new MailAddress(toemail);
                    MailMessage message = new MailMessage();
                    message.From = from;
                    foreach (string mail in mails)
                    {
                        if (IsValidEmail(mail))
                        {
                            message.To.Add(new MailAddress(mail));
                            i++;
                        }
                    }
                    //MailMessage message = new MailMessage(from, to);
                    message.Subject = title;//设置邮件主题
                    message.IsBodyHtml = true;//设置邮件正文为html格式
                    message.Body = content;//设置邮件内容

                    //设置发送邮件身份验证方式
                    SmtpClient client = null;
                    if (mailTemplate != null && mailTemplate.Server != "")
                    {
                        client = new SmtpClient(mailTemplate.Server);
                    }
                    else
                    {
                       client = new SmtpClient("smtp.liveflow.com.cn");
                     }
                    if (mailTemplate != null && mailTemplate.FromAddr != "")
                    {
                        client.Credentials = new NetworkCredential(mailTemplate.FromAddr, mailTemplate.Password);
                      }
                    else
                    {
                        client.Credentials = new NetworkCredential("luogz@liveflow.com.cn", "luogz123");
                    }
                    if (i > 0)
                    {
                        client.Send(message);
                        return 1;
                    }
                }
            }
            catch
            {
                return 0;
            }
            return 0;
        }
        public static bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
      
        /// <summary>
        /// 获取邮件模板
        /// </summary>
        /// <param name="mailType">模板类型</param>
        /// <returns>模板</returns>
        public static MailTemplate GetMailTemplate(string templateName)
        {
            MailTemplate mailTemp = new MailTemplate();

            XmlDocument doc = new XmlDocument();
            doc.Load(ConfigurationManager.AppSettings["MailTemplate"].ToString() + "MailTemplate.xml");

            XmlNode pnode = doc.SelectSingleNode("MailTemplate/Template[@name='" + templateName + "']");
            XmlNodeList childnodes = pnode.ChildNodes; 
            mailTemp.TemplateName = pnode["TemplateName"].InnerText;
            mailTemp.Subject = pnode["Subject"].InnerText;
            mailTemp.Body = pnode["Body"].InnerText;
            mailTemp.Server = pnode["Server"].InnerText;
            mailTemp.FromAddr = pnode["FromAddr"].InnerText;
            mailTemp.Password = pnode["Password"].InnerText; 
            
            return mailTemp;
        }

        /// <summary>
        /// 获取邮件模板
        /// </summary>
        /// <param name="mailType">模板类型</param>
        /// <returns>模板</returns>
        public static MailTemplate GetWebMailTemplate(string templateName)
        {
            MailTemplate mailTemp = new MailTemplate();

            XmlDocument doc = new XmlDocument();
            string uploadPath = System.Web.HttpContext.Current.Request.MapPath(ConfigurationManager.AppSettings["MailTemplateBack"].ToString());
            doc.Load(uploadPath + "MailTemplate.xml");

            XmlNode pnode = doc.SelectSingleNode("MailTemplate/Template[@name='" + templateName + "']");
            XmlNodeList childnodes = pnode.ChildNodes;
            mailTemp.TemplateName = pnode["TemplateName"].InnerText;
            mailTemp.Subject = pnode["Subject"].InnerText;
            mailTemp.Body = pnode["Body"].InnerText;
            mailTemp.Server = pnode["Server"].InnerText;
            mailTemp.FromAddr = pnode["FromAddr"].InnerText;
            mailTemp.Password = pnode["Password"].InnerText;

            return mailTemp;
        }
    }
}
