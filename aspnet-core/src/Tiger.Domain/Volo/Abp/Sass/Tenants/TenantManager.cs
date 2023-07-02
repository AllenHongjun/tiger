﻿using System;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Tiger.Volo.Abp.Sass.Tenants;

public class TenantManager : DomainService, ITenantManager
{
    protected ITenantRepository TenantRepository { get; }

    public TenantManager(ITenantRepository tenantRepository)
    {
        TenantRepository = tenantRepository;

    }

    public async virtual Task<Tenant> CreateAsync(string name)
    {
        Check.NotNull(name, nameof(name));

        await ValidateNameAsync(name);
        return new Tenant(GuidGenerator.Create(), name);
    }

    public async virtual Task ChangeNameAsync(Tenant tenant, string name)
    {
        Check.NotNull(tenant, nameof(tenant));
        Check.NotNull(name, nameof(name));

        await ValidateNameAsync(name, tenant.Id);
        tenant.SetName(name);
    }

    protected async virtual Task ValidateNameAsync(string name, Guid? expectedId = null)
    {
        var tenant = await TenantRepository.FindByNameAsync(name);
        if (tenant != null && tenant.Id != expectedId)
        {
            throw new BusinessException(AbpSaasErrorCodes.DuplicateTenantName)
                .WithData(nameof(Tenant.Name), name);
        }
    }
}
