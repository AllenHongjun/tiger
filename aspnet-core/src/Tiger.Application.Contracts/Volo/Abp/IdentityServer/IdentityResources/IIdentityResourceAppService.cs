using System;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public interface IIdentityResourceAppService : 
        ICrudAppService<
            IdentityResourceDto,
            Guid,
            IdentityResourceGetByPagedDto,
            IdentityResourceCreateOrUpdateDto,
            IdentityResourceCreateOrUpdateDto
            >
    {
    }
}
