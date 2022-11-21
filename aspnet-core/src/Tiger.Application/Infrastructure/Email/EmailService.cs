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
using Volo.Abp.Settings;

namespace Tiger.Books.Demo
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(true)]
    public class EmailService :TigerAppService, ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        private readonly ISettingEncryptionService _settingEncryptionService;
        private readonly ISettingDefinitionManager _settingDefinitionManager;


        public EmailService(IEmailSender emailSender, ISettingEncryptionService settingEncryptionService, ISettingDefinitionManager settingDefinitionManager)
        {
            _emailSender = emailSender;
            _settingEncryptionService=settingEncryptionService;
            _settingDefinitionManager=settingDefinitionManager;
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
        public async Task EmailSimpleTestAsync()
        {
            try
            {
                await _emailSender.SendAsync(
                    "hongjy1991@gmail.com",     // target email address
                    "Email subject",         // subject
                    "This is email body..."  // email body
                );

                await _emailSender.SendAsync(
                    "806815027@qq.com",     // target email address
                    "Email subject",         // subject
                    "This is email body..."  // email body
                );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public async Task EncryptPwd()
        {

            var setting = _settingDefinitionManager.Get(EmailSettingNames.Smtp.Password);
            var psd = _settingEncryptionService.Encrypt(setting, "HONGjunyu123");
            throw new Exception($"密码是:{psd}");
        }


    }
}
