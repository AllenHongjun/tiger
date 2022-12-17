using System;
using System.Collections.Generic;
using System.Text;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 组织机构拖拽移动
    /// </summary>
    public class OrganizationUnitMoveDto
    {
        public Guid? ParentId { get; set; }
    }
}
