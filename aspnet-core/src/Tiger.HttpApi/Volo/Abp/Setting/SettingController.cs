using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;
using Tiger.Volo.Abp.SettingManagementAppService;
using System.Threading.Tasks;
using Volo.Abp.Settings;
using Tiger.Volo.Abp.SettingUi;

namespace Tiger.Volo.Abp.Setting
{

    /// <summary>
    /// 系统设置
    /// </summary>
    [RemoteService(Name = SettingUiRemoteServiceConsts.RemoteServiceName)]
    [Area(SettingUiRemoteServiceConsts.ModuleName)]
    [ControllerName("Setting")]
    [Route("api/setting")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class SettingController : AbpController, ISettingManagementAppService, ISettingUiAppService
    {
        private readonly ISettingManagementAppService _settingManagementAppService;

        private readonly ISettingUiAppService _settingUiAppService;

        public SettingController(ISettingManagementAppService settingManagementAppService, ISettingUiAppService settingUiAppService)
        {
            _settingManagementAppService=settingManagementAppService;
            _settingUiAppService=settingUiAppService;
        }


        [HttpGet]
        [Route("group-setting-definitions")]
        public virtual Task<List<SettingGroup>> GroupSettingDefinitionsAsync()
        {
            return _settingUiAppService.GroupSettingDefinitionsAsync();
        }

        [HttpPut]
        [Route("set-setting-values")]
        public virtual Task SetSettingValuesAsync(Dictionary<string, string> settingValues)
        {
            return _settingUiAppService.SetSettingValuesAsync(settingValues);
        }

        [HttpPut]
        [Route("reset-setting-values")]
        public virtual Task ResetSettingValuesAsync(List<string> settingNames)
        {
            return _settingUiAppService.ResetSettingValuesAsync(settingNames);
        }


        


        /// <summary>
        /// 查询单个设置值
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("find")]
        public async Task<SettingManagementDto> FindAsync(string name, string providerName, string providerKey)
        {
            return await _settingManagementAppService.FindAsync(name, providerName, providerKey);
        }

        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("get-list")]
        public async Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey)
        {
            return await _settingManagementAppService.GetAllAsync(providerName, providerKey);
        }

        /// <summary>
        /// 增加设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        [HttpPost]
        public Task SetAsync(string name, string value, string providerName, string providerKey)
        {
            return _settingManagementAppService.SetAsync(name, value, providerName, providerKey);
        }


        [HttpGet]
        [Route("test-register-task")]
        [RemoteService(false)]
        public Task RegisterAsync(string userName="admin", string emailAddress="admin@gmai.com", string password="1q2w3E*")
        {
            return _settingManagementAppService.RegisterAsync(userName, emailAddress, password);
        }

        

        [HttpGet]
        [Route("test-get-setting-value")]
        [RemoteService(false)]
        public Task TestGetSettingValueAsync()
        {
            return _settingManagementAppService.TestGetSettingValueAsync();
        }

        [HttpGet]
        [Route("test-set-manager")]
        [RemoteService(false)]
        public Task TestSetManager()
        {
            return _settingManagementAppService.TestSetManager();
        }
    }
    
}
