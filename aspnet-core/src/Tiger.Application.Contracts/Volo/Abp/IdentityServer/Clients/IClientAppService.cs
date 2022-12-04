using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.Clients.Dto;
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

        Task<ClientDto> CloneAsync(Guid id, ClientCloneDto input);

        Task<ListResultDto<string>> GetAssignableApiResourceAsync();

        Task<ListResultDto<string>> GetAssignableIdentityResourceAsync();

        Task<ListResultDto<string>> GetAllDistinctAllowedCorsOriginsAsync();

    }
}
