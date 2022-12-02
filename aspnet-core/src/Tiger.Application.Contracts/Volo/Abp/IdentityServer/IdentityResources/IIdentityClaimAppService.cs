using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public interface IIdentityClaimAppService:
        ICrudAppService<
            IdentityClaimDto,
            Guid,
            PagedAndSortedResultRequestDto,
            CreateUpdateIdentityClaimDto>
    {
        
    }
}
