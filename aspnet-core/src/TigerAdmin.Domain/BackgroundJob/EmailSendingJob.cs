﻿/**
 * 类    名：EmailSendingJob   
 * 作    者：hongjy       
 * 创建时间：2021/8/12 8:01:37       
 * 说    明: 
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;

namespace TigerAdmin.BackgroundJob
{
    public class EmailSendingJob : BackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        //private readonly IEmailSender _emailSender;

        public EmailSendingJob()
        {
            //IEmailSender emailSender
            //_emailSender = emailSender;
        }

        public override void Execute(EmailSendingArgs args)
        {
            Console.WriteLine("后台定时任务测试执行");
            //_emailSender.Send(
            //    args.EmailAddress,
            //    args.Subject,
            //    args.Body
            //);
        }
    }
}
