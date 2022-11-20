/**
 * 类    名：EmailService   
 * 作    者：花生了什么树       
 * 创建时间：2021/8/16 17:09:11       
 * 说    明: 
 * 
 */

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Tiger.Books.Demo
{
    [ApiExplorerSettings(GroupName = "api")]
    [RemoteService(false)]
    public class EmailService : ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public EmailService(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }



        /*
         Provides IEmailSender service that is used to send emails.
        Defines settings to configure email sending.
        Integrates to the background job system to send emails via background jobs.
        Provides MailKit integration package.
        1. 定义发送邮件的同一接口
        2. 集成后台定时发送消息的任务
        3. 集成第三方发送邮件的服务
         
         */
        public async Task DoItAsync()
        {
            await _emailSender.SendAsync(
                "652971723@163.com",     // target email address
                "Email subject",         // subject
                "This is email body..."  // email body
            );
        }
    }
}
