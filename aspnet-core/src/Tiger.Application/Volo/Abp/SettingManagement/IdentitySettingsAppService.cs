using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Emailing;
using Volo.Abp.SettingManagement;

namespace Tiger.Volo.Abp.SettingManagement
{
    [RemoteService(false)]
    //[RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
    //[Area(SettingManagementRemoteServiceConsts.ModuleName)]
    //[Area("setting")]
    //[Route("api/setting/identity")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class IdentitySettingsAppService : ApplicationService, IIdentitySettingsAppService
    {
        protected ISettingManager SettingManager { get; }

        public IdentitySettingsAppService(ISettingManager settingManager)
        {
            SettingManager=settingManager;
        }

        

        public async Task<IdentitySettingsDto> GetAsync()
        {
            var settingsDto = new IdentitySettingsDto
            {   
                password = new Password
                {
                    requiredLength = Convert.ToInt32( await SettingProvider.GetOrNullAsync(IdentitySettingsConsts.Password.RequiredLength))
                }
            };

            return settingsDto;
        }

        public async Task UpdateAsync(UpdateIdentitySettingsDto input)
        {
            ////await CheckFeatureAsync();
            if (CurrentTenant.IsAvailable)
            {
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Host, input.SmtpHost);
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Port, input.SmtpPort.ToString());
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.UserName, input.SmtpUserName);
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Password, input.SmtpPassword);
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.Domain, input.SmtpDomain);
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.EnableSsl, input.SmtpEnableSsl.ToString());
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.SmtpUseDefaultCredentials.ToString().ToLowerInvariant());
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                //await SettingManager.SetForCurrentTenantAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            }
            else
            {
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequiredLength, input.password.requiredLength.ToString());
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequiredUniqueChars, input.password.ToString());
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequireNonAlphanumeric, input.password.requireNonAlphanumeric.ToString());
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequireLowercase, input.password.requireLowercase.ToString());
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequireUppercase, input.password.requireUppercase.ToString());
                await SettingManager.SetGlobalAsync(IdentitySettingsConsts.Password.RequireDigit, input.password.requireDigit.ToString());

                //await SettingManager.SetGlobalAsync(EmailSettingNames.Smtp.UseDefaultCredentials, input.password);
                //await SettingManager.SetGlobalAsync(EmailSettingNames.DefaultFromAddress, input.DefaultFromAddress);
                //await SettingManager.SetGlobalAsync(EmailSettingNames.DefaultFromDisplayName, input.DefaultFromDisplayName);
            }
        }
    }
}
