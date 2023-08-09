using System;
using System.Collections.Generic;
using Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.IdentityResources
{
    public class IdentityResourceDto : ExtensibleAuditedEntityDto<Guid>
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        
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
        public List<IdentityResourcePropertyDto> Properties { get; set; }

        public IdentityResourceDto()
        {
            UserClaims = new List<IdentityResourceClaimDto>();
            Properties = new List<IdentityResourcePropertyDto>();
        }
    }
}
