using System;

namespace Tiger.Volo.Abp.Identity.Roles.Dto
{
    public class IdentityRoleClaimDto
    {
        public Guid RoleId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
