using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto
{   
    /// <summary>
    /// 身份资源Dto
    /// </summary>
    public class CreateUpdateIdentityResourceDto : FullAuditedEntityDto<Guid>
    {
        public CreateUpdateIdentityResourceDto()
        {
            UserClaims= new List<IdentityResourceClaimDto>();
            Properties= new List<IdentityResoucePropertyDto>();
        }

        [Required]
        public virtual string Name { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Description { get; set; }

        public virtual bool Enabled { get; set; }

        public virtual bool Required { get; set; }

        public virtual bool Emphasize { get; set; }

        public virtual bool ShowInDiscoveryDocument { get; set; }

        public List<IdentityResourceClaimDto> UserClaims { get; set; }

        public List<IdentityResoucePropertyDto> Properties { get; set; }
    }
}
