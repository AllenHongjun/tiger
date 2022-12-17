using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 添加角色关联多个组织
    /// </summary>
    public class IdentityRoleOrgCreateDto : IdentityRoleCreateDto
    {
        public IdentityRoleOrgCreateDto()
        {

        }

        public Guid? OrgId { get; set; }
    }
}
