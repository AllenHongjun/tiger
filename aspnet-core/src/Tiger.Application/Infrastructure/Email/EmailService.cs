using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;
using Volo.Abp.Settings;
using System.IO;
using IdentityServer4.Models;
using Volo.Abp.TextTemplating;
using Volo.Abp.Emailing.Templates;
using Volo.Abp.Security.Encryption;
using System.Linq;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;

namespace Tiger.Books.Demo
{
    [ApiExplorerSettings(GroupName = "admin")]
    [RemoteService(true)]
    public class EmailService :TigerAppService, ITransientDependency
    {
        private readonly IEmailSender _emailSender;
        // 文本模板集成 https://docs.abp.io/zh-Hans/abp/latest/Text-Templating
        private readonly ITemplateRenderer _templateRenderer;

        private readonly ISettingEncryptionService _settingEncryptionService;
        private readonly ISettingDefinitionManager _settingDefinitionManager;
        protected ISettingManager SettingManager { get; }


        public EmailService(IEmailSender emailSender, ISettingEncryptionService settingEncryptionService, ISettingDefinitionManager settingDefinitionManager, ITemplateRenderer templateRenderer, ISettingManager settingManager)
        {
            _emailSender = emailSender;
            _settingEncryptionService=settingEncryptionService;
            _settingDefinitionManager=settingDefinitionManager;
            _templateRenderer=templateRenderer;
            SettingManager=settingManager;
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

        /*
                 1. 邮件配置管理开发
                 3. 邮件模板发送测试
                 
                 */

        /// <summary>
        /// 邮件发送测试
        /// </summary>
        /// <returns></returns>
        public async Task TestEmailSendAsync()
        {   
            // 读取邮件设置
            var settingsDto = new EmailSettingsDto
            {
                SmtpHost = await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.Host),
                SmtpPort = Convert.ToInt32(await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.Port)),
                SmtpUserName = await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.UserName),
                SmtpPassword = await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.Password),
                SmtpDomain = await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.Domain),
                SmtpEnableSsl = Convert.ToBoolean(await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.EnableSsl)),
                SmtpUseDefaultCredentials = Convert.ToBoolean(await SettingProvider.GetOrNullAsync(EmailSettingNames.Smtp.UseDefaultCredentials)),
                DefaultFromAddress = await SettingProvider.GetOrNullAsync(EmailSettingNames.DefaultFromAddress),
                DefaultFromDisplayName = await SettingProvider.GetOrNullAsync(EmailSettingNames.DefaultFromDisplayName),
            };

            if (CurrentTenant.IsAvailable)
            {
                settingsDto.SmtpHost = await SettingManager.GetOrNullForTenantAsync(EmailSettingNames.Smtp.Host, CurrentTenant.GetId(), false);
                settingsDto.SmtpUserName = await SettingManager.GetOrNullForTenantAsync(EmailSettingNames.Smtp.UserName, CurrentTenant.GetId(), false);
                settingsDto.SmtpPassword = await SettingManager.GetOrNullForTenantAsync(EmailSettingNames.Smtp.Password, CurrentTenant.GetId(), false);
                settingsDto.SmtpDomain = await SettingManager.GetOrNullForTenantAsync(EmailSettingNames.Smtp.Domain, CurrentTenant.GetId(), false);
            }


            try
            {
                // .NET 框架默认不支持465端口发送邮件 ssl通信
                var allsettings = _settingDefinitionManager.GetAll().Where(x => x.Name.Contains("Abp.Mailing")).ToList();
                // 普通邮箱有外发限制。
                await _emailSender.SendAsync(
                    "652971723@qq.com",     // target email address
                    "Email subject",         // subject
                    "This is email body..."  // email body
                );

                /*
                 1. 邮件配置管理开发
                 2. 邮件附件发送测试
                 3. 邮件模板发送测试
                 4. 免费好用的邮箱
                 
                 */  
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// 通过队列发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task TestEmailQueueAsync()
        {
            try
            {
                await _emailSender.QueueAsync(
                    "hongjy1991@gmail.com",     // target email address
                    "通过队列作业发送邮件",         // subject
                    "这是一封通过队列发送的邮件"  // email body
                );
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// 发送邮件附件
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 附件发送邮件 https://learn.microsoft.com/zh-cn/dotnet/api/system.net.mail.mailmessage?view=net-7.0
        /// </remarks>
        public async Task TestEmailSendWithAttachmentAsync()
        {
            // Specify the file to be attached and sent.
            // This example assumes that a file named Data.xls exists in the
            // current working directory.
            // Create a message and set up the recipients.
            string file = Directory.GetCurrentDirectory() + "/Files/data.xlsx";    //根目录
                                                                                   // Create a message and set up the recipients.
            MailMessage mailMessage = new MailMessage(
                "hongjy2000@outlook.com",
                "806815027@qq.com",
                "Quarterly data report.",
                "See the attached spreadsheet.");

            // Create  the file attachment for this email message.
            Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
            // Add time stamp information for the file.
            ContentDisposition disposition = data.ContentDisposition;
            disposition.CreationDate = System.IO.File.GetCreationTime(file);
            disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
            disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
            // Add the file attachment to this email message.
            mailMessage.Attachments.Add(data);



            try
            {
                // 普通邮箱有外发限制。
                await _emailSender.SendAsync(mailMessage, true);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateMessageWithAttachment(): {0}",
                    ex.ToString());
            }


            // Display the values in the ContentDisposition for the attachment.
            ContentDisposition cd = data.ContentDisposition;
            Console.WriteLine("Content disposition");
            Console.WriteLine(cd.ToString());
            Console.WriteLine("File {0}", cd.FileName);
            Console.WriteLine("Size {0}", cd.Size);
            Console.WriteLine("Creation {0}", cd.CreationDate);
            Console.WriteLine("Modification {0}", cd.ModificationDate);
            Console.WriteLine("Read {0}", cd.ReadDate);
            Console.WriteLine("Inline {0}", cd.Inline);
            Console.WriteLine("Parameters: {0}", cd.Parameters.Count);
            foreach (DictionaryEntry d in cd.Parameters)
            {
                Console.WriteLine("{0} = {1}", d.Key, d.Value);
            }
            data.Dispose();
        }



        /// <summary>
        /// 测试使用文本模板发送邮件
        /// </summary>
        /// <returns></returns>
        public async Task TestEmailSendWithTemplateRenderer()
        {   
            // 使用邮件模板 发送邮件
            var body = await _templateRenderer.RenderAsync(
                StandardEmailTemplates.Message,  // 这是模板名称
                new
                {
                    message = "这是一封使用模板渲染发送的邮件"
                }
            );

            await _emailSender.QueueAsync(
                "806815027@qq.com",
                "Email subject",
                body
            );
        }


        /// <summary>
        /// 替换覆盖文件模板
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// 虚拟文件系统 https://docs.abp.io/zh-Hans/abp/latest/Virtual-File-System
        /// </remarks>
        public async Task TestEmailSendWithCustomTemplateRenderer()
        {
            var body = await _templateRenderer.RenderAsync(
                StandardEmailTemplates.Message,
                new
                {
                    message = "这是一封使用自定义模板渲染发送的邮件"
                }
            );

            await _emailSender.QueueAsync(
                "806815027@qq.com",
                "Email subject",
                body
            );
        }



        /// <summary>
        /// 加密邮箱密码
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public  Task EncryptPwd()
        {

            var setting = _settingDefinitionManager.Get(EmailSettingNames.Smtp.Password);
            var psd = _settingEncryptionService.Encrypt(setting, "HONGjunyu123");
            throw new Exception($"密码是:{psd}");
        }


    }
}
