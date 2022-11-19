using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TigerAdmin.Volo.Abp.TenantManagement;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;
using Volo.Abp;
using Tiger.Volo.Abp.SettingManagementAppService;
using System.Threading.Tasks;

namespace Tiger.Volo.Abp.Setting
{

    /// <summary>
    /// 系统设置
    /// </summary>
    [RemoteService(Name = IdentityRemoteServiceConsts.RemoteServiceName)]
    [Area("setting")]
    [ControllerName("Setting")]
    [Route("api/setting")]
    [ApiExplorerSettings(GroupName = "admin")]
    public class SettingController : AbpController, ISettingManagementAppService
    {
        private readonly ISettingManagementAppService _settingManagementAppService;

        public SettingController(ISettingManagementAppService settingManagementAppService)
        {
            _settingManagementAppService=settingManagementAppService;
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


        [HttpGet]
        [Route("test-get-setting-value")]
        public Task TestGetSettingValueAsync()
        {
            return _settingManagementAppService.TestGetSettingValueAsync();
        }

        [HttpGet]
        [Route("test-set-manager")]
        public Task TestSetManager()
        {
            return _settingManagementAppService.TestSetManager();
        }
    }
    
}
