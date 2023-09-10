using System.Collections.Generic;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{
    public class ApiResourceCreateOrUpdateDto
    {
        /// <summary>
        /// 显示名称
        /// </summary>
        //[DynamicStringLength(typeof(ApiResourceConsts), nameof(ApiResourceConsts.DisplayNameMaxLength))]
        public string DisplayName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        //[DynamicStringLength(typeof(ApiResourceConsts), nameof(ApiResourceConsts.DescriptionMaxLength))]
        public string Description { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 允许访问令牌签名算法
        /// </summary>
        public bool AllowedAccessTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// 在发现文档中显示
        /// </summary>
        public bool ShowInDiscoveryDocument { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public List<ApiResourceSecretCreateOrUpdateDto> Secrets { get; set; }

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
        public Dictionary<string, string>  Properties { get; set; }

        protected ApiResourceCreateOrUpdateDto()
        {
            UserClaims = new List<ApiResourceClaimDto>();
            Scopes = new List<ApiResourceScopeDto>();
            Secrets = new List<ApiResourceSecretCreateOrUpdateDto>();
            Properties = new Dictionary<string, string>();
        }
    }
}
