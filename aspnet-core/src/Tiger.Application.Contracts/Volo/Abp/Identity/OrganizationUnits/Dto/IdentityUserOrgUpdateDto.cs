using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 添加用户关联多个组织
    /// </summary>
    public class IdentityUserOrgUpdateDto : IdentityUserUpdateDto
    {
        /// <summary>
        /// 组织id列表
        /// </summary>
        [Required]
        public List<Guid> OrgIds { get; set; }
    }
}
