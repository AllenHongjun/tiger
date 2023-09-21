using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;

namespace Tiger.Volo.Abp.SettingManagement
{
    /// <summary>
    /// 系统设置
    /// </summary>
    [RemoteService(false)]
    
    public class SettingManagementAppService: ApplicationService, ISettingManagementAppService
    {
        protected ISettingManager SettingManager { get; set; }

        private readonly ISettingManagementStore _settingManagerStore;

        protected ISettingRepository SettingRepository { get; }
        

        public SettingManagementAppService(
            ISettingManager settingManager,
            ISettingManagementStore settingManagerStore,
            ISettingRepository settingRepository
            )
        {
            SettingManager = settingManager;
            _settingManagerStore = settingManagerStore;
            SettingRepository = settingRepository;
        }




        /// <summary>
        /// 查找单个设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<SettingManagementDto> FindAsync(string name, string providerName, string providerKey)
        {
            var setting =  await SettingRepository.FindAsync(name, providerName, providerKey);
            return ObjectMapper.Map<Setting, SettingManagementDto>(setting);
            
        }


        /// <summary>
        /// 获取设置列表
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public virtual async Task<List<SettingManagementDto>> GetListAsync(string providerName, string providerKey)
        {
            var list = await SettingRepository.GetListAsync(providerName, providerKey);
            return ObjectMapper.Map<List<Setting>, List<SettingManagementDto>>(list);
        }

        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public async Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey)
        {
            var settingValue =  await _settingManagerStore.GetListAsync(providerName, providerKey);
            return settingValue;
            //return await _settingManager.GetAllAsync(providerName, providerKey, fallback);
        }


        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <returns></returns>
        public virtual async Task SetAsync(string name, string value, string providerName, string providerKey)
        {
            await _settingManagerStore.SetAsync(name, value, providerName, providerKey);
            await CurrentUnitOfWork.SaveChangesAsync();
            //await _settingManager.SetAsync(name, value, providerName, providerKey, forceToSet);
        }






        #region 测试读取设置的值

        /// <summary>
        /// 从缓存中读取配置信息
        /// </summary>
        /// <returns></returns>
        public async Task TestGetSettingValueAsync()
        {
            //Get a value as string.
            //return Task.CompletedTask;
            
        }


        /// <summary>
        /// 测试将设置保存到数据库中
        /// </summary>
        /// <returns></returns>
        public async Task TestSetManager()
        {
            Guid user1Id = CurrentTenant.Id ?? Guid.NewGuid();
            Guid tenant1Id = CurrentUser.Id ?? Guid.NewGuid();

            //Get/set a setting value for the current user or the specified user

            /*
             1. 需要在代码里面先增加这个配置
             2. _settingManager 才能修改这个配置数据
             
             */

            await SettingManager.SetGlobalAsync("Qiniu.Oss.AccessKey", "1pWN6jy9PFgv4xf9tJuhcBQ0qzAEztQblkXS323");
            await SettingManager.SetGlobalAsync("Qiniu.Oss.SecretKey", "su40oxnCEV6DGSJ9mNBM3jgZ84DMolEk3232");
            await SettingManager.SetGlobalAsync("Qiniu.Oss.Bucket", "blog-hongjy");


            

            await SettingManager.SetForCurrentUserAsync("App.UI.LayoutType", "LeftMenu");
            await SettingManager.SetForUserAsync(user1Id, "App.UI.LayoutType", "LeftMenu");
            

            await SettingManager.SetForCurrentTenantAsync("App.UI.LayoutType", "LeftMenu");
            await SettingManager.SetForTenantAsync(tenant1Id, "App.UI.LayoutType", "LeftMenu");


            await SettingManager.SetGlobalAsync("App.UI.LayoutType", "TopMenu");
        }

        public Task RegisterAsync(string userName, string emailAddress, string password)
        {
            throw new NotImplementedException();
        }
        #endregion



    }
}
