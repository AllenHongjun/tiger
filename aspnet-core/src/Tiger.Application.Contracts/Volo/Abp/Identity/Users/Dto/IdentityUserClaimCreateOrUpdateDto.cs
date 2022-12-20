using System.ComponentModel.DataAnnotations;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Identity.Users.Dto
{
    public abstract class IdentityUserClaimCreateOrUpdateDto
    {
        [Required]
        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimTypeLength))]
        public string ClaimType { get; set; }

        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimValueLength))]
        public string ClaimValue { get; set; }
    }
}
