using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.Identity.OrganizationUnits.Dto
{   
    /// <summary>
    /// 组织单元获取子节点
    /// </summary>
    public class GetOrganizationUnitChildrenDto:IEntityDto<Guid>
    {

        public Guid Id { get; set; }

        public bool Recursive { get; set; }
    }
}
