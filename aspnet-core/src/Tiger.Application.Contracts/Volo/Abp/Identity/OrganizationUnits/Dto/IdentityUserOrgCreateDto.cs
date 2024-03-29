﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class IdentityUserOrgCreateDto : IdentityUserCreateDto
    {
        [Required]
        public List<Guid> OrgIds { get; set; }
    }
}
