using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Configuration.Provider;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp.Application.Services;
using Volo.Abp.Emailing;
using Volo.Abp.Features;
using Volo.Abp.Identity.Settings;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Tiger.Volo.Abp.SettingManagement
{
    /// <summary>
    /// 邮件设置服务
    /// </summary>
    //[Authorize(SettingManagementPermissions.Emailing)]
    public class EmailSettingsAppService : ApplicationService, IEmailSettingsAppService
    {
        protected ISettingManager SettingManager { get; }

        protected IEmailSender EmailSender { get; }

        public EmailSettingsAppService(ISettingManager settingManager, IEmailSender emailSender)
        {
            SettingManager=settingManager;
            EmailSender=emailSender;
        }

        
        /// <summary>
        /// 获取邮箱设置
        /// </summary>
        /// <returns></returns>
        public async Task<EmailSettingsDto> GetAsync()
        {
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

            return settingsDto;
        }

        /// <summary>
        /// 更新邮箱设置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task UpdateAsync(UpdateEmailSettingsDto input)
        {
            //await CheckFeatureAsync();
            if (CurrentTenant.IsAvailable)
            {
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Port, input.SmtpPort.ToString());
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString());
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.UseDefaultCredentials,input.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            }
            else
            {
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.Port, input.SmtpPort.ToString());
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString());
                await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
                await SettingManager.SetGlobalAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                await SettingManager.SetGlobalAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            }
        }

        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        //[Authorize(SettingManagementPermissions.EmailingTest)]
        public virtual async Task SendTestEmailAsync(SendTestEmailInput input)
        {
            //await CheckFeatureAsync();

            await EmailSender.SendAsync(input.SenderEmailAddress, input.TargetEmailAddress, input.Subject, input.Body);
        }

        //protected virtual async Task CheckFeatureAsync()
        //{
        //    await FeatureChecker.CheckEnabledAsync(SettingManagementFeatures.Enable);
        //    if (CurrentTenant.IsAvailable)
        //    {
        //        await FeatureChecker.CheckEnabledAsync(SettingManagementFeatures.AllowChangingEmailSettings);
        //    }
        //}


    }
}
