﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{
    public class IdentityUserOrgUpdateDto : IdentityUserUpdateDto
    {
        /// <summary>
        /// 组织id列表
        /// </summary>
        public List<Guid> OrgIds { get; set; }
    }
}