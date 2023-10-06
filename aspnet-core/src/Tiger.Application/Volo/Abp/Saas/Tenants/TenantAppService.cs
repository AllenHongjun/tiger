using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tiger.Volo.Abp.Identity.Users.Dto;
using Tiger.Volo.Abp.Sass.MultiTenancy;
using Tiger.Volo.Abp.Sass.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using IdentityUser = Volo.Abp.Identity.IdentityUser;

namespace Tiger.Volo.Abp.Sass.Tenants;

/// <summary>
/// 租户管理
/// </summary>
[RemoteService(isEnabled:false)]
public class TenantAppService : AbpSaasAppServiceBase, ITenantAppService
{
    #region 构造函数
    protected IDistributedEventBus EventBus { get; }
    protected ITenantRepository TenantRepository { get; }
    protected ITenantManager TenantManager { get; }
    protected IdentityUserManager IdentityUserManager { get; }
    protected IdentityRoleManager IdentityRoleManager { get; }

    public TenantAppService(
        ITenantRepository tenantRepository,
        ITenantManager tenantManager,
        IDistributedEventBus eventBus,
        IdentityUserManager identityUserManager,
        IdentityRoleManager identityRoleManager)
    {
        EventBus = eventBus;
        TenantRepository = tenantRepository;
        TenantManager = tenantManager;
        IdentityUserManager=identityUserManager;
        IdentityRoleManager=identityRoleManager;
    } 
    #endregion

    #region 租户
    /// <summary>
    /// 更具id查询租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    [Authorize(AbpSaasPermissions.Tenants.Default)]
    public async virtual Task<TenantDto> GetAsync(Guid id)
    {
        var tenant = await TenantRepository.FindAsync(id);
        if (tenant == null)
        {
            throw new UserFriendlyException(L["TenantNotFoundById", id]);
        }

        return ObjectMapper.Map<Tenant, TenantDto>(tenant);
    }

    /// <summary>
    /// 根据租户名称查询租户信息
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    /// <exception cref="UserFriendlyException"></exception>
    [Authorize(AbpSaasPermissions.Tenants.Default)]
    public async virtual Task<TenantDto> GetAsync(string name)
    {
        var tenant = await TenantRepository.FindByNameAsync(name);
        if (tenant == null)
        {
            throw new UserFriendlyException(L["TenantNotFoundByName", name]);
        }
        return ObjectMapper.Map<Tenant, TenantDto>(tenant);
    }

    /// <summary>
    /// 分页查询租户列表
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize]
    public async virtual Task<PagedResultDto<TenantDto>> GetListAsync(TenantGetListInput input)
    {
        var count = await TenantRepository.GetCountAsync(input.Filter);
        var list = await TenantRepository.GetListAsync(
            input.Sorting,
            input.MaxResultCount,
            input.SkipCount,
            input.Filter,
            input.EditionId,
            input.DisableBeginTime,
            input.DisableEndTime,
            input.IsActive
        );

        return new PagedResultDto<TenantDto>(
            count,
            ObjectMapper.Map<List<Tenant>, List<TenantDto>>(list)
        );
    }

    /// <summary>
    /// 创建租户
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.Create)]
    public async virtual Task<TenantDto> CreateAsync(TenantCreateDto input)
    {
        var tenant = await TenantManager.CreateAsync(input.Name);
        tenant.IsActive = input.IsActive;
        tenant.EditionId = input.EditionId;
        tenant.SetEnableTime(input.EnableTime);
        tenant.SetDisableTime(input.DisableTime);
        input.MapExtraPropertiesTo(tenant);

        if (!input.UseSharedDatabase && !input.DefaultConnectionString.IsNullOrWhiteSpace())
        {
            tenant.SetDefaultConnectionString(input.DefaultConnectionString);
        }

        await TenantRepository.InsertAsync(tenant);
        
        CurrentUnitOfWork.OnCompleted(async () =>
        {
            var createEventData = new CreateEventData
            {
                Id = tenant.Id,
                Name = tenant.Name,
                AdminUserId = GuidGenerator.Create(),
                AdminEmailAddress = input.AdminEmailAddress,
                AdminPassword = input.AdminPassword
            };
            // 如何 因为项目各自独立，租户增加时添加管理用户必须通过事件总线
            // 而 TenantEto 对象没有包含所需的用户名密码，需要独立发布事件
            await EventBus.PublishAsync(createEventData);

            // 要保存当前租户的用户需要 ChangeTenant 参考abp的源码
            using (CurrentTenant.Change(tenant.Id, tenant.Name))
            {
                /*
                 * 通过事件订阅
                 1. 发送租户创建成功邮件通知
                 2. 创建租户的种子数据
                 3. 创建租户的账号和密码
                 4，给租户的管理员授予所有租户端的权限
                 */
                await SeedTenantAdminAsync(createEventData);
            }
        });

        await CurrentUnitOfWork.SaveChangesAsync();

        return ObjectMapper.Map<Tenant, TenantDto>(tenant);
    }
    

    /// <summary>
    /// 创建租户管理员的账号
    /// </summary>
    /// <param name="eventData"></param>
    /// <returns></returns>
    /// <remarks>
    /// TODO: 如果分开多个独立项目使用事件总线订阅方式创建
    /// </remarks>
    private async Task SeedTenantAdminAsync(CreateEventData eventData)
    {
        // 创建租户的默认管理员账号密码 默认 admin
        const string tenantAdminUserName = "admin";
        const string tenantAdminRoleName = "admin";
        var tenantAdminRoleId = Guid.Empty; ;

        if (!await IdentityRoleManager.RoleExistsAsync(tenantAdminRoleName))
        {
            tenantAdminRoleId = GuidGenerator.Create();
            var tenantAdminRole = new IdentityRole(tenantAdminRoleId, tenantAdminRoleName, eventData.Id)
            {
                IsStatic = true,
                IsPublic = true
            };
            (await IdentityRoleManager.CreateAsync(tenantAdminRole)).CheckErrors();
        }
        else
        {
            var tenantAdminRole = await IdentityRoleManager.FindByNameAsync(tenantAdminRoleName);
            tenantAdminRoleId = tenantAdminRole.Id;
        }

        var tenantAdminUser = await IdentityUserManager.FindByNameAsync(eventData.AdminEmailAddress);
        if (tenantAdminUser == null)
        {
            tenantAdminUser = new IdentityUser(
                eventData.AdminUserId,
                tenantAdminUserName,
                eventData.AdminEmailAddress,
                eventData.Id);

            tenantAdminUser.AddRole(tenantAdminRoleId);
            // 创建租户管理用户
            var userReuslt = await IdentityUserManager.CreateAsync(tenantAdminUser, eventData.AdminPassword);
        }
    }


    /// <summary>
    /// 更新租户信息
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.Update)]
    public async virtual Task<TenantDto> UpdateAsync(Guid id, TenantUpdateDto input)
    {
        var tenant = await TenantRepository.GetAsync(id);

        if (!string.Equals(tenant.Name, input.Name))
        {
            await TenantManager.ChangeNameAsync(tenant, input.Name);
        }

        tenant.IsActive = input.IsActive;
        tenant.EditionId = input.EditionId;
        tenant.SetEnableTime(input.EnableTime);
        tenant.SetDisableTime(input.DisableTime);
        //tenant.SetConcurrencyStampIfNotNull(input.ConcurrencyStamp);
        input.MapExtraPropertiesTo(tenant);
        await TenantRepository.UpdateAsync(tenant);

        await CurrentUnitOfWork.SaveChangesAsync();

        //CurrentUnitOfWork.OnCompleted(async () =>
        //{
        //    var tenantEto = new TenantEto();
        //    tenantEto.Name = tenant.Name;
        //    tenantEto.Id = tenant.Id;
        //    var createEventData = new EntityDeletedEto<TenantEto>(tenantEto);
        //    await EventBus.PublishAsync(createEventData);
        //});

        return ObjectMapper.Map<Tenant, TenantDto>(tenant);
    }

    /// <summary>
    /// 删除租户
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.Delete)]
    public async virtual Task DeleteAsync(Guid id)
    {
        var tenant = await TenantRepository.FindAsync(id);
        if (tenant == null)
        {
            return;
        }
        await TenantRepository.DeleteAsync(tenant);

        await CurrentUnitOfWork.SaveChangesAsync();
    }


    /// <summary>
    /// 修改租户用户密码
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    /// <exception cref="BusinessException"></exception>
    [Authorize(AbpSaasPermissions.Tenants.ChangeUserPassword)]
    public async Task ChangePasswordAsync(string userName, IdentityUserSetPasswordInput input)
    {
        var user =  await IdentityUserManager.FindByNameAsync(userName);

        if (user.IsExternal)
        {
            throw new BusinessException(code:Identity.IdentityErrorCodes.ExternalUserPasswordChange);
        }

        if (user.PasswordHash == null)
        {
            (await IdentityUserManager.AddPasswordAsync(user, input.Password)).CheckErrors();
        }
        else
        {
            var token = await IdentityUserManager.GeneratePasswordResetTokenAsync(user);

            (await IdentityUserManager.ResetPasswordAsync(user, token, input.Password)).CheckErrors();
        }

        await CurrentUnitOfWork.SaveChangesAsync();

    }
    #endregion

    #region 数据库连接字符串

    /// <summary>
    /// 根据租户id和名称查询连接字符串
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
    public async virtual Task<TenantConnectionStringDto> GetConnectionStringAsync(Guid id, string name)
    {
        var tenant = await TenantRepository.GetAsync(id,true);

        var tenantConnectionString = tenant.FindConnectionString(name);

        return new TenantConnectionStringDto
        {
            Name = name,
            Value = tenantConnectionString
        };
    }

    /// <summary>
    /// 查询连接字符串
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <remarks>
    /// 需要multi-tendey 模块的代码  事件代码 缓存代码
    /// </remarks>
    [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
    public async virtual Task<ListResultDto<TenantConnectionStringDto>> GetConnectionStringAsync(Guid id)
    {
        var tenant = await TenantRepository.GetAsync(id, true);

        return new ListResultDto<TenantConnectionStringDto>(
            ObjectMapper.Map<List<TenantConnectionString>, List<TenantConnectionStringDto>>(tenant.ConnectionStrings.ToList()));
    }

    /// <summary>
    /// 设置连接字符串
    /// </summary>
    /// <param name="id"></param>
    /// <param name="input"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
    public async virtual Task<TenantConnectionStringDto> SetConnectionStringAsync(Guid id, TenantConnectionStringCreateOrUpdate input)
    {
        var tenant = await TenantRepository.GetAsync(id);
        if (tenant.FindConnectionString(input.Name) == null)
        {
            CurrentUnitOfWork.OnCompleted(async () =>
            {
                var eventData = new ConnectionStringCreatedEventData
                {
                    TenantId = tenant.Id,
                    TenantName = tenant.Name,
                    Name = input.Name
                };

                await EventBus.PublishAsync(eventData);
            });
        }
        tenant.SetConnectionString(input.Name, input.Value);

        await TenantRepository.UpdateAsync(tenant);

        await CurrentUnitOfWork.SaveChangesAsync();

        return new TenantConnectionStringDto
        {
            Name = input.Name,
            Value = input.Value
        };
    }

    /// <summary>
    /// 删除租户链接字符串
    /// </summary>
    /// <param name="id"></param>
    /// <param name="name"></param>
    /// <returns></returns>
    [Authorize(AbpSaasPermissions.Tenants.ManageConnectionStrings)]
    public async virtual Task DeleteConnectionStringAsync(Guid id, string name)
    {
        var tenant = await TenantRepository.GetAsync(id);

        tenant.RemoveConnectionString(name);

        CurrentUnitOfWork.OnCompleted(async () =>
        {
            var eventData = new ConnectionStringDeletedEventData
            {
                TenantId = tenant.Id,
                TenantName = tenant.Name,
                Name = name
            };

            await EventBus.PublishAsync(eventData);
        });

        await TenantRepository.UpdateAsync(tenant);

        await CurrentUnitOfWork.SaveChangesAsync();
    } 
    #endregion
}
