using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.Identity.Roles.Dto
{
    public class IdentityRoleClaimUpdateDto : IdentityRoleClaimCreateDto
    {
        [Required]
        [DynamicMaxLength(typeof(IdentityRoleClaimConsts), nameof(IdentityRoleClaimConsts.MaxClaimValueLength))]
        public string NewClaimValue { get; set; }
    }
}
