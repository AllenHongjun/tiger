using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Books;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{
    public interface IApiResourceAppService:
        ICrudAppService<
            ApiResourceDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateApiResourceDto>
    {
    }
}
