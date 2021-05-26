using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Identity
{
    public interface ITigerIdentityRoleAppService : IApplicationService
    {
        Task AddToOrganizationUnitAsync(Guid roleId, Guid ouId);

        Task<IdentityRoleDto> CreateAsync(IdentityRoleOrgCreateDto input);
    }
}
