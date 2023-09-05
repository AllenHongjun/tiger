using System;
using Tiger.Volo.Abp.IdentityServer.Grants.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.Grants
{
    public interface IPersistedGrantAppService :
        ICrudAppService<PersistedGrantDto, Guid, GetPersistedGrantInput, CreateUpdatePersistedGrantDto>,
        IDeleteAppService<Guid>
    {

    }
}
