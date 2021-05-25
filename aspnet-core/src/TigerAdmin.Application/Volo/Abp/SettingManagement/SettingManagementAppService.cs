using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.SettingManagement;

namespace TigerAdmin.Volo.Abp.SettingManagement
{
    public class SettingManagementAppService
    {
        protected ISettingManager settingManager { get; set; }

        protected ISettingRepository SettingRepository { get; }
    }
}
