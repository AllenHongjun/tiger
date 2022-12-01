using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.SettingManagementAppService
{
    public interface IEmailSettingsAppService:IApplicationService
    {
        /// <summary>
        /// 获取邮件配置
        /// </summary>
        /// <returns></returns>
        Task<EmailSettingsDto> GetAsync();

        /// <summary>
        /// 更新邮件配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateEmailSettingsDto input);

        /// <summary>
        /// 发送测试邮件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task SendTestEmailAsync(SendTestEmailInput input);
    }
}
