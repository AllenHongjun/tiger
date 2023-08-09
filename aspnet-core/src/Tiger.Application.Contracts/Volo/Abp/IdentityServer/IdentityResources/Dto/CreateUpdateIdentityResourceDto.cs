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

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        public virtual string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 必要
        /// </summary>
        public bool Required { get; set; }

        /// <summary>
        /// 强调
        /// </summary>
        public bool Emphasize { get; set; }

        /// <summary>
        /// 在发现文档中显示
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; }

        /// <summary>
        /// 用户声明
        /// </summary>
        public List<IdentityResourceClaimDto> UserClaims { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<IdentityResoucePropertyDto> Properties { get; set; }
    }
}
