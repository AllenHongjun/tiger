using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.Grants
{
    public interface IPersistedGrantAppService:
        ICrudAppService<
            PersistedGrantDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdatePersistedGrantDto>
    {
    }
}
