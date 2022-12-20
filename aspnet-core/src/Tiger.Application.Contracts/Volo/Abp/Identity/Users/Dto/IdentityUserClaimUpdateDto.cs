using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Identity.Users.Dto
{
    public class IdentityUserClaimUpdateDto : IdentityUserClaimCreateOrUpdateDto
    {
        [DynamicMaxLength(typeof(IdentityUserClaimConsts), nameof(IdentityUserClaimConsts.MaxClaimValueLength))]
        public string NewClaimValue { get; set; }
    }
}
