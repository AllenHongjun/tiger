using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Setting
{
    //[RemoteService(Name = SettingManagementRemoteServiceConsts.RemoteServiceName)]
    //[Area(SettingManagementRemoteServiceConsts.ModuleName)]
    [Area("setting")]
    [ControllerName("Setting")]
    [Route("api/setting/emailing")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class EmailSettingsController : AbpController, IEmailSettingsAppService
    {
        private readonly IEmailSettingsAppService _emailSettingsAppService;

        public EmailSettingsController(IEmailSettingsAppService emailSettingsAppService)
        {
            _emailSettingsAppService = emailSettingsAppService;
        }

        /// <summary>
        /// 获取邮件配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Task<EmailSettingsDto> GetAsync()
        {
            return _emailSettingsAppService.GetAsync();
        }

        /// <summary>
        /// 更新邮件配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task UpdateAsync(UpdateEmailSettingsDto input)
        {
            return _emailSettingsAppService.UpdateAsync(input);
        }

        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("send-test-email")]
        public Task SendTestEmailAsync(SendTestEmailInput input)
        {
            return _emailSettingsAppService.SendTestEmailAsync(input);
        }
    }
}
