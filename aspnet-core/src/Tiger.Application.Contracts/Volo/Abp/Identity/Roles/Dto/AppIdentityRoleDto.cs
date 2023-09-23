using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.Roles.Dto
{
    public class AppIdentityRoleDto: IdentityRoleDto
    {
        public int UserCount { get;set; }
    }
}
