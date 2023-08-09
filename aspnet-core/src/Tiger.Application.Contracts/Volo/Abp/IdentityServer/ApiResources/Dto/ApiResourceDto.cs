using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{   
    /// <summary>
    /// Api资源
    /// </summary>
    public class ApiResourceDto : ExtensibleAuditedEntityDto<Guid>
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
        /// 允许访问令牌签名算法
        /// </summary>
        public string AllowedAccessTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// 在发现文档中显示
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public List<ApiResourceSecretDto> Secrets { get; set; }

        /// <summary>
        /// 作用域
        /// </summary>
        public List<ApiResourceScopeDto> Scopes { get; set; }

        /// <summary>
        /// 用户声明
        /// </summary>
        public List<ApiResourceClaimDto> UserClaims { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public List<ApiResourcePropertyDto> Properties { get; set; }

        public ApiResourceDto()
        {
            UserClaims = new List<ApiResourceClaimDto>();
            Scopes = new List<ApiResourceScopeDto>();
            Secrets = new List<ApiResourceSecretDto>();
            Properties = new List<ApiResourcePropertyDto>();
        }
    }
}
