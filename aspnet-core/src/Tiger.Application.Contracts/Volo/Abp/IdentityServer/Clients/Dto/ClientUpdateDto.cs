using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.Validation;

namespace Tiger.Volo.Abp.IdentityServer.Clients
{
    public class ClientUpdateDto : ClientCreateOrUpdateDto
    {
        /// <summary>
        /// 客户端 Uri
        /// </summary>
        [MaxLength(ClientConsts.ClientUriMaxLength)]
        public string ClientUri { get; set; }

        /// <summary>
        /// 徽标 Uri
        /// </summary>
        [MaxLength(ClientConsts.LogoUriMaxLength)]
        public string LogoUri { get; set; }

        /// <summary>
        /// 启用客户端
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 协议类型
        /// </summary>
        [MaxLength(ClientConsts.ProtocolTypeMaxLength)]
        public string ProtocolType { get; set; }

        
        public bool RequireClientSecret { get; set; }

        /// <summary>
        /// 允许身份令牌签名算法
        /// </summary>
        public string AllowedIdentityTokenSigningAlgorithms { get; set; }

        /// <summary>
        /// 需要同意
        /// </summary>
        public bool RequireConsent { get; set; }

        /// <summary>
        /// 允许记住同意
        /// </summary>
        public bool AllowRememberConsent { get; set; }

        /// <summary>
        /// 需要请求对象
        /// </summary>
        public bool RequireRequestObject { get; set; }

        

        /// <summary>
        /// 始终在标识令牌中包含用户声明
        /// </summary>
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        /// <summary>
        /// 需要 Pkce
        /// </summary>
        public bool RequirePkce { get; set; }

        /// <summary>
        /// 允许纯文本 Pkce
        /// </summary>
        public bool AllowPlainTextPkce { get; set; }

        /// <summary>
        /// 允许通过浏览器访问令牌
        /// </summary>
        public bool AllowAccessTokensViaBrowser { get; set; }

        /// <summary>
        /// 前端通道注销 Uri
        /// </summary>
        [MaxLength(ClientConsts.FrontChannelLogoutUriMaxLength)]
        public string FrontChannelLogoutUri { get; set; }

        /// <summary>
        /// 需要前端通道注销会话
        /// </summary>
        public bool FrontChannelLogoutSessionRequired { get; set; }

        /// <summary>
        /// 后端通道退出 Uri
        /// </summary>
        [MaxLength(ClientConsts.BackChannelLogoutUriMaxLength)]
        public string BackChannelLogoutUri { get; set; }

        /// <summary>
        /// 需要后端通道注销会话
        /// </summary>
        public bool BackChannelLogoutSessionRequired { get; set; }

        /// <summary>
        /// 允许离线访问
        /// </summary>
        public bool AllowOfflineAccess { get; set; }

        /// <summary>
        /// 身份令牌有效期(s)
        /// </summary>
        public int IdentityTokenLifetime { get; set; }

        /// <summary>
        /// 访问令牌有效期(s)
        /// </summary>
        public int AccessTokenLifetime { get; set; }

        /// <summary>
        /// 授权码有效期(s)
        /// </summary>
        public int AuthorizationCodeLifetime { get; set; }

        public int? ConsentLifetime { get; set; }

        /// <summary>
        /// 绝对刷新令牌有效期(s)
        /// </summary>
        public int AbsoluteRefreshTokenLifetime { get; set; }

        /// <summary>
        /// 滚动刷新令牌有效期(s)
        /// </summary>
        public int SlidingRefreshTokenLifetime { get; set; }

        /// <summary>
        /// 刷新令牌使用情况
        /// </summary>
        public int RefreshTokenUsage { get; set; }

        /// <summary>
        /// 刷新时更新访问令牌声明
        /// </summary>
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        /// <summary>
        /// 刷新令牌过期方式
        /// </summary>
        public int RefreshTokenExpiration { get; set; }

        /// <summary>
        /// 访问令牌类型
        /// </summary>
        public int AccessTokenType { get; set; }

        /// <summary>
        /// 启用本地登录
        /// </summary>
        public bool EnableLocalLogin { get; set; }

        /// <summary>
        /// 包括 Jwt 标识
        /// </summary>
        public bool IncludeJwtId { get; set; }

        /// <summary>
        /// 始终发送客户端声明
        /// </summary>
        public bool AlwaysSendClientClaims { get; set; }

        /// <summary>
        /// 客户端声明前缀
        /// </summary>
        [MaxLength(ClientConsts.ClientClaimsPrefixMaxLength)]
        public string ClientClaimsPrefix { get; set; }

        /// <summary>
        /// 配对主体盐
        /// </summary>
        [MaxLength(ClientConsts.PairWiseSubjectSaltMaxLength)]
        public string PairWiseSubjectSalt { get; set; }

        /// <summary>
        /// 用户 SSO 生命周期
        /// </summary>
        public int? UserSsoLifetime { get; set; }

        /// <summary>
        /// 用户代码类型
        /// </summary>
        [MaxLength(ClientConsts.UserCodeTypeMaxLength)]
        public string UserCodeType { get; set; }

        /// <summary>
        /// 设备授权码有效期(s)
        /// </summary>
        public int DeviceCodeLifetime { get; set; }

        /// <summary>
        /// 允许的作用域
        /// </summary>
        public List<ClientScopeDto> AllowedScopes { get; set; }

        /// <summary>
        /// 客户端密钥
        /// </summary>
        public List<ClientSecretDto> ClientSecrets { get; set; }


        /// <summary>
        /// 允许跨域来源
        /// </summary>
        public List<ClientCorsOriginDto> AllowedCorsOrigins { get; set; }

        /// <summary>
        /// 重定向 Uri
        /// </summary>
        public List<ClientRedirectUriDto> RedirectUris { get; set; }

        /// <summary>
        /// 注销重定向 Uri
        /// </summary>
        public List<ClientPostLogoutRedirectUriDto> PostLogoutRedirectUris { get; set; }

        /// <summary>
        /// 身份提供程序限制
        /// </summary>
        public List<ClientIdPRestrictionDto> IdentityProviderRestrictions { get; set; }

        /// <summary>
        /// 客户端声明
        /// </summary>
        public List<ClientClaimDto> Claims { get; set; }

        /// <summary>
        /// 客户端属性
        /// </summary>
        public List<ClientPropertyDto> Properties { get; set; }

        public ClientUpdateDto()
        {
            Enabled = true;
            DeviceCodeLifetime = 300;
            AllowedScopes = new List<ClientScopeDto>();
            RedirectUris = new List<ClientRedirectUriDto>();
            AllowedCorsOrigins = new List<ClientCorsOriginDto>();
            PostLogoutRedirectUris = new List<ClientPostLogoutRedirectUriDto>();
            IdentityProviderRestrictions = new List<ClientIdPRestrictionDto>();
            Properties = new List<ClientPropertyDto>();
            ClientSecrets = new List<ClientSecretDto>();
            Claims = new List<ClientClaimDto>();
        }
    }
}
