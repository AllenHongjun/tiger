using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.SettingUi
{
    public interface ISettingUiAppService:IApplicationService
    {
        Task<List<SettingGroup>> GroupSettingDefinitionsAsync();
        Task SetSettingValuesAsync(Dictionary<string, string> settingValues);
        Task ResetSettingValuesAsync(List<string> settingNames);
    }
}
