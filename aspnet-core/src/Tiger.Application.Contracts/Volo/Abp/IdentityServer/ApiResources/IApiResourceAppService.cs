using System;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{
    public interface IApiResourceAppService :
        ICrudAppService<
            ApiResourceDto,
            Guid,
            ApiResourceGetByPagedInputDto,
            ApiResourceCreateDto,
            ApiResourceUpdateDto>
    {
    }
}
