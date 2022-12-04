using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class IdentityRoleOrgCreateDto : IdentityRoleCreateDto
    {
        public IdentityRoleOrgCreateDto()
        {

        }

        public Guid? OrgId { get; set; }
    }
}
