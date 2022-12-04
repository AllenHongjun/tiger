using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources.Dto
{
    public class ApiResourceDto : FullAuditedEntityDto<Guid>
    {
        // 

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public bool Enabled { get; set; }


        //public virtual Dictionary<string, string> Properties { get; protected set; }
    }
}
