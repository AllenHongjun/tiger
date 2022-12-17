using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 组织机构关联多个角色
    /// </summary>
    public class OrganizationUnitAddRolesDto
    {

        [Required]
        public List<Guid> RoleIds { get; set; }
    }
}
