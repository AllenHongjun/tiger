using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Tiger.Volo.Abp.Sass.Permissions;
using Tiger.Volo.Abp.Sass.Tenants;
using Tiger.Volo.Abp.Sass.Tenants.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Sass
{
    /// <summary>
    /// 租户
    /// </summary>
    [Controller]
    [RemoteService(Name = AbpSaasRemoteServiceConsts.RemoteServiceName)] // 服务名称
    [Area(AbpSaasRemoteServiceConsts.ModuleName)] // 域名称
    [Route("api/saas/tenants")] // 跟路由
    public class TenantController: ITenantAppService
    {
        protected ITenantAppService TenantAppService { get;}

        public TenantController(ITenantAppService tenantAppService)
        {
            TenantAppService=tenantAppService;
        }


        #region Tenant
        [HttpGet]
        [Route("{id}")]
        public virtual async Task<TenantDto> GetAsync(Guid id)
        {
            return await TenantAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("by-name/{name}")]
        public virtual async Task<TenantDto> GetAsync(string name)
        {
            return await TenantAppService.GetAsync(name);
        }

        [HttpGet]
        public virtual async Task<PagedResultDto<TenantDto>> GetListAsync(TenantGetListInput input)
        {
            return await TenantAppService.GetListAsync(input);
        }

        [HttpPost]
        public virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
        {
            return TenantAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
        {
            return TenantAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return TenantAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 修改租户用户的密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("change-user-password")]
        public async Task ChangePasswordAsync(TenantChangePasswordInput input)
        {
            await TenantAppService.ChangePasswordAsync(input);
        }

        
        #endregion

        #region ConnectionString
        [HttpGet]
        [Route("{id}/connection-string/{name}")]
        [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task<TenantConnectionStringDto> GetConnectionStringAsync(Guid id, string name)
        {
            return TenantAppService.GetConnectionStringAsync(id, name);
        }

        [HttpGet]
        [Route("{id}/connection-string")]
        [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task<ListResultDto<TenantConnectionStringDto>> GetConnectionStringAsync(Guid id)
        {
            return TenantAppService.GetConnectionStringAsync(id);
        }

        [HttpPut]
        [Route("{id}/connection-string")]
        [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task<TenantConnectionStringDto> SetConnectionStringAsync(Guid id, TenantConnectionStringCreateOrUpdate input)
        {
            return TenantAppService.SetConnectionStringAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}/connection-string/{name}")]
        [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
        public virtual Task DeleteConnectionStringAsync(Guid id, string name)
        {
            return TenantAppService.DeleteConnectionStringAsync(id, name);
        }

        
        #endregion


    }
}
