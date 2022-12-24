using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Settings;

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


        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey);


        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        Task SetAsync(string name, string value, string providerName, string providerKey);


        Task TestGetSettingValueAsync();


        Task TestSetManager();


        Task RegisterAsync(string userName, string emailAddress, string password);
    }
}
