using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Tiger.Volo.Abp.SettingManagement
{   
    /// <summary>
    /// 系统设置
    /// </summary>
    [RemoteService(false)]
    public class SettingManagementAppService
    {
        protected ISettingManager _settingManager { get; set; }

        protected ISettingRepository _settingRepository { get; }

        public SettingManagementAppService(ISettingManager settingManager, ISettingRepository settingRepository)
        {
            _settingManager = settingManager;
            _settingRepository = settingRepository;
        }

        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public async Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey, bool fallback = true)
        {
            return await _settingManager.GetAllAsync(providerName, providerKey, fallback);
        }





    }
}
