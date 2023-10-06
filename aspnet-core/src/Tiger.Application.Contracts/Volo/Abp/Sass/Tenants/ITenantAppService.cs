using Microsoft.AspNetCore.Authorization;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Tiger.Volo.Abp.Sass.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.Sass.Tenants;

public interface ITenantAppService :
        ICrudAppService<
            TenantDto,
            Guid,
            TenantGetListInput,
            TenantCreateDto,
            TenantUpdateDto>
{
    Task<TenantDto> GetAsync(
        [Required] string name);

    Task<TenantConnectionStringDto> GetConnectionStringAsync(
        Guid id,
        [Required] string connectionName);

    Task<ListResultDto<TenantConnectionStringDto>> GetConnectionStringAsync(
        Guid id);

    Task<TenantConnectionStringDto> SetConnectionStringAsync(
        Guid id, 
        TenantConnectionStringCreateOrUpdate input);

    Task DeleteConnectionStringAsync(
        Guid id,
        [Required] string connectionName);

    /// <summary>
    /// 修改租户用户密码
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    Task ChangePasswordAsync(string userName, IdentityUserSetPasswordInput input);
}
