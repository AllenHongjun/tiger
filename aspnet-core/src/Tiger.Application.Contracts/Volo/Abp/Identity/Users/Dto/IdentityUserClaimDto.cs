using System;

namespace Tiger.Volo.Abp.Identity.Users.Dto
{
    public class IdentityUserClaimDto
    {
        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
