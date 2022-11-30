using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.Clients
{
    public interface IClientAppService:
        ICrudAppService<
            ClientDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateClientDto>
    {
    }
}
