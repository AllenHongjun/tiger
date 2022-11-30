using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace Tiger.Volo.Abp.IdentityServer.Clients
{
    public class ClientDto:FullAuditedEntityDto<Guid>
    {
        public virtual string ClientId { get; set; }

        public virtual string ClientName { get; set; }

        public virtual string Description { get; set; }

        public virtual string ClientUri { get; set; }

        public virtual string LogoUri { get; set; }

        public virtual bool Enabled { get; set; } = true;

        public virtual string ProtocolType { get; set; }

        public virtual bool RequireClientSecret { get; set; }

        public virtual bool RequireConsent { get; set; }

        public virtual bool AllowRememberConsent { get; set; }

        public virtual bool AlwaysIncludeUserClaimsInIdToken { get; set; }

        public virtual bool RequirePkce { get; set; }

        public virtual bool AllowPlainTextPkce { get; set; }

        public virtual bool AllowAccessTokensViaBrowser { get; set; }

        public virtual string FrontChannelLogoutUri { get; set; }

        public virtual bool FrontChannelLogoutSessionRequired { get; set; }

        public virtual string BackChannelLogoutUri { get; set; }

        public virtual bool BackChannelLogoutSessionRequired { get; set; }

        public virtual bool AllowOfflineAccess { get; set; }

        public virtual int IdentityTokenLifetime { get; set; }

        public virtual int AccessTokenLifetime { get; set; }

        public virtual int AuthorizationCodeLifetime { get; set; }

        public virtual int? ConsentLifetime { get; set; }

        public virtual int AbsoluteRefreshTokenLifetime { get; set; }

        public virtual int SlidingRefreshTokenLifetime { get; set; }

        public virtual int RefreshTokenUsage { get; set; }

        public virtual bool UpdateAccessTokenClaimsOnRefresh { get; set; }

        public virtual int RefreshTokenExpiration { get; set; }

        public virtual int AccessTokenType { get; set; }

        public virtual bool EnableLocalLogin { get; set; }

        public virtual bool IncludeJwtId { get; set; }

        public virtual bool AlwaysSendClientClaims { get; set; }

        public virtual string ClientClaimsPrefix { get; set; }

        public virtual string PairWiseSubjectSalt { get; set; }

        public virtual int? UserSsoLifetime { get; set; }

        public virtual string UserCodeType { get; set; }

        public virtual int DeviceCodeLifetime { get; set; } = 300;
    }
}
