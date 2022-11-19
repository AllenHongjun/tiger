using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.TenantManagement
{
    public class TenantDto : ExtensibleEntityDto<Guid>
    {
        public virtual string Name { get; set; }
    }
}
