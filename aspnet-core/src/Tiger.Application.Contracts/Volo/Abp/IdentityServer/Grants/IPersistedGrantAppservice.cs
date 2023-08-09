using System;
using Tiger.Volo.Abp.IdentityServer.Grants.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.Grants
{
    public interface IPersistedGrantAppService :
        IReadOnlyAppService<PersistedGrantDto, Guid, GetPersistedGrantInput>,
        IDeleteAppService<Guid>
    {

    }
}
