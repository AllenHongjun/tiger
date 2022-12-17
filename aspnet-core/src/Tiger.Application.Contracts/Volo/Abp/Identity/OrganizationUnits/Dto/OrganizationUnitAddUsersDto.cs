using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 组织机构关联多个用户
    /// </summary>
    public class OrganizationUnitAddUsersDto
    {   
        /// <summary>
        /// 用户id数组
        /// </summary>
        public List<Guid> UserIds { get; set; }
    }
}
