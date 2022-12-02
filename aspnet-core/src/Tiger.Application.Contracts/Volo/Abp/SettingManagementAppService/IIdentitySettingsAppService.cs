using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Volo.Abp.SettingManagementAppService
{   
    /// <summary>
    /// 身份标识管理
    /// </summary>
    public interface IIdentitySettingsAppService
    {
        /// <summary>
        /// 获取身份标识配置
        /// </summary>
        /// <returns></returns>
        Task<IdentitySettingsDto> GetAsync();

        /// <summary>
        /// 更新身份标识配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task UpdateAsync(UpdateIdentitySettingsDto input);
    }
}
