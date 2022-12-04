using System;

namespace Tiger.Volo.Abp.Identity.ClaimTypes.Dto
{
    public class IdentityUserClaimDto
    {
        public Guid UserId { get; set; }

        public string ClaimType { get; set; }

        public string ClaimValue { get; set; }
    }
}
