/**
 * 类    名：EmailSettingProvider   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 17:54:13       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Settings;

namespace Tiger.Books.Demo
{   
    /// <summary>
    /// ABP设置系统使用 demo
    /// </summary>
    public class EmailSettingProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            /*
                Abp.Mailing.DefaultFromAddress: 当你在发送电子邮件时未指定发件人时,用作发件人的电子邮件地址（就像上面的示例一样）.
                Abp.Mailing.DefaultFromDisplayName: 当你在发送电子邮件时未指定发件人时,用作发件人的显示名称（就像在上面的示例中一样）.
                Abp.Mailing.Smtp.Host: SMTP 服务器的 IP/域（默认值：127.0.0.1）。
                Abp.Mailing.Smtp.Port: SMTP 服务器的端口（默认值：25）.
                Abp.Mailing.Smtp.UserName: 用户名，如果 SMTP 服务器需要身份验证需要。
                Abp.Mailing.Smtp.Password: 密码，如果 SMTP 服务器需要身份验证需要。 此值已加密（请参阅下面的部分）.
                Abp.Mailing.Smtp.Domain: 账号域，如果 SMTP 服务器需要身份验证需要.
                Abp.Mailing.Smtp.EnableSsl: 指示 SMTP 服务器是否使用 SSL 的值（“true”或“false”。默认值：“false”）.
                Abp.Mailing.Smtp.UseDefaultCredentials:如果为 true，则使用默认凭据,而不是提供的用户名和密码（“true”或“false”。默认值：“true”）。.
             
             */
            //context.Add(
            //    new SettingDefinition("Abp.Mailing.DefaultFromAddress", "hongjy2000@outlook.com"),
            //    new SettingDefinition("Abp.Mailing.DefaultFromDisplayName", "AllenHong"),
            //    new SettingDefinition("Abp.Mailing.Smtp.Host", "smtp.office365.com"),
            //    new SettingDefinition("Abp.Mailing.Smtp.Port", "587"),
            //    // 直接使用保存的密码需要加密 https://www.cnblogs.com/zinan/p/15309515.html
            //    new SettingDefinition("Abp.Mailing.Smtp.UserName", "hongjy2000@outlook.com"),
            //    new SettingDefinition("Abp.Mailing.Smtp.Password", "OCttA8wx583UxEFnASt42w=="),
            //    new SettingDefinition("Abp.Mailing.Smtp.Domain", "smtp.office365.com"),
            //    new SettingDefinition("Abp.Mailing.Smtp.EnableSsl", "true"),
            //    new SettingDefinition("Abp.Mailing.Smtp.UseDefaultCredentials", "false")
            //);
        }
    }
}
