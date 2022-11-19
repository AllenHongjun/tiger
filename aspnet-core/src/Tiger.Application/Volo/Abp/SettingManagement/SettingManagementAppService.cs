using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.BackgroundJob;
using Tiger.Volo.Abp.SettingManagementAppService;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Identity;
using Volo.Abp.ObjectMapping;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.TenantManagement;

namespace Tiger.Volo.Abp.SettingManagement
{   
    /// <summary>
    /// 系统设置
    /// </summary>
    [RemoteService(false)]
    public class SettingManagementAppService: ApplicationService, ISettingManagementAppService
    {
        protected ISettingManager _settingManager { get; set; }

        protected ISettingRepository _settingRepository { get; }

        private readonly ISettingProvider _settingProvider;

        private readonly IBackgroundJobManager _backgroundJobManager;

        public SettingManagementAppService(ISettingManager settingManager, ISettingRepository settingRepository, ISettingProvider settingProvider, IBackgroundJobManager backgroundJobManager)
        {
            _settingManager = settingManager;
            _settingRepository = settingRepository;
            _settingProvider=settingProvider;
            _backgroundJobManager=backgroundJobManager;
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
            var setting =  await _settingRepository.FindAsync(name, providerName, providerKey);
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
            var list = await _settingRepository.GetListAsync(providerName, providerKey);
            return ObjectMapper.Map<List<Setting>, List<SettingManagementDto>>(list);
        }

        /// <summary>
        /// 获取所有设置
        /// </summary>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="fallback"></param>
        /// <returns></returns>
        public async Task<List<SettingValue>> GetAllAsync(string providerName, string providerKey, bool fallback = true)
        {
            return await _settingManager.GetAllAsync(providerName, providerKey, fallback);
        }


        /// <summary>
        /// 更新设置
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="providerName"></param>
        /// <param name="providerKey"></param>
        /// <param name="forceToSet"></param>
        /// <returns></returns>
        public virtual async Task SetAsync(string name, string value, string providerName, string providerKey, bool forceToSet = false)
        {
            await _settingManager.SetAsync(name, value, providerName, providerKey, forceToSet);
        }






        #region 测试读取设置的值

        /// <summary>
        /// 从缓存中读取配置信息
        /// </summary>
        /// <returns></returns>
        public async Task TestGetSettingValueAsync()
        {
            //Get a value as string.
            string userName = await _settingProvider.GetOrNullAsync("Smtp.UserName");

            //Get a bool value and fallback to the default value (false) if not set.
            bool enableSsl = await _settingProvider.GetAsync<bool>("Smtp.EnableSsl");

            //Get a bool value and fallback to the provided default value (true) if not set.
            bool enableSsl1 = await _settingProvider.GetAsync<bool>(
                "Smtp.EnableSsl", defaultValue: true);


            var host = await _settingProvider.GetOrNullAsync("Smtp.Host");

            var allSetts = await _settingProvider.GetAllAsync();

            //Get a bool value with the IsTrueAsync shortcut extension method
            bool enableSsl2 = await _settingProvider.IsTrueAsync("Smtp.EnableSsl");

            //Get an int value or the default value (0) if not set
            int port = (await _settingProvider.GetAsync<int>("Smtp.Port"));

            //Get an int value or null if not provided
            int? port1 = (await _settingProvider.GetOrNullAsync("Smtp.Port"))?.To<int>();
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

            await _settingManager.SetGlobalAsync("Qiniu.Oss.AccessKey", "1pWN6jy9PFgv4xf9tJuhcBQ0qzAEztQblkXS323");
            await _settingManager.SetGlobalAsync("Qiniu.Oss.SecretKey", "su40oxnCEV6DGSJ9mNBM3jgZ84DMolEk3232");
            await _settingManager.SetGlobalAsync("Qiniu.Oss.Bucket", "blog-hongjy");



            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.Host", "127.0.0.1");
            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.Port", "25");
            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.UserName", "test123");
            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.Password", "1q2w3E*");
            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.Domain", "https://www.baidu.com");
            await _settingManager.SetForCurrentUserAsync("Abp.Mailing.Smtp.EnableSsl", "true");


            await _settingManager.SetForCurrentUserAsync("App.UI.LayoutType", "LeftMenu");
            await _settingManager.SetForUserAsync(user1Id, "App.UI.LayoutType", "LeftMenu");

            string layoutType1 =
                await _settingManager.GetOrNullForCurrentUserAsync("App.UI.LayoutType");
            string layoutType2 =
                await _settingManager.GetOrNullForUserAsync("App.UI.LayoutType", user1Id);

            //Get/set a setting value for the current tenant or the specified tenant

            

            await _settingManager.SetForCurrentTenantAsync("App.UI.LayoutType", "LeftMenu");
            await _settingManager.SetForTenantAsync(tenant1Id, "App.UI.LayoutType", "LeftMenu");

            string layoutType3 =
                await _settingManager.GetOrNullForCurrentTenantAsync("App.UI.LayoutType");
            string layoutType4 =
                await _settingManager.GetOrNullForTenantAsync("App.UI.LayoutType", tenant1Id);

            //Get/set a global and default setting value

            string layoutType5 =
                await _settingManager.GetOrNullGlobalAsync("App.UI.LayoutType");
            string layoutType6 =
                await _settingManager.GetOrNullDefaultAsync("App.UI.LayoutType");

            await _settingManager.SetGlobalAsync("App.UI.LayoutType", "TopMenu");
        }
        #endregion


        #region 添加一个后台作业到队列中
        public async Task RegisterAsync(string userName, string emailAddress, string password)
        {
            //TODO: 创建一个新用户到数据库中...

            await _backgroundJobManager.EnqueueAsync(
                new EmailSendingArgs
                {
                    EmailAddress = emailAddress,
                    Subject = "You've successfully registered!",
                    Body = "..."
                }
            );
        } 
        #endregion
    }
}
