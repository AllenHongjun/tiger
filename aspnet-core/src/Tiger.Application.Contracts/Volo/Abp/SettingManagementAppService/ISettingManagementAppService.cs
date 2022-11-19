using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tiger.Volo.Abp.SettingManagementAppService
{
    public interface ISettingManagementAppService
    {

        /// <summary>
        /// 查找单个设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task<SettingManagementDto> FindAsync(string name, string providerName, string providerKey);




        Task TestGetSettingValueAsync();


        Task TestSetManager();
    }
}
