using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public interface IIdentityResourceAppService:
        ICrudAppService<
            IdentityResourceDto,
            Guid,
            GetIdentityResourceDto,
            CreateUpdateIdentityResourceDto>
    {
    }
}
