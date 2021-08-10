using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace TigerAdmin.Volo.Abp.SettingManagement
{
    public class SettingManagementAppService
    {
        public SettingManagementAppService(ISettingManager settingManager, ISettingRepository settingRepository)
        {
            _settingManager = settingManager;
            _settingRepository = settingRepository;
        }

        protected ISettingManager _settingManager { get; set; }

        protected ISettingRepository _settingRepository { get; }


        public async Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey, bool fallback = true)
        {
            return await _settingManager.GetAllAsync(providerName, providerKey, fallback);
        }
    }
}
