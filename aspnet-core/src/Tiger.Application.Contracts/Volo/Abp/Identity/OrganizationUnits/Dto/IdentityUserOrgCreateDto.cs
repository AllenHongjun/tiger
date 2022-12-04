using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class IdentityUserOrgCreateDto : IdentityUserCreateDto
    {
        public List<Guid> OrgIds { get; set; }
    }
}
