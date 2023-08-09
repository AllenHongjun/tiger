using AutoMapper;
using System.Linq;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Tiger.Volo.Abp.IdentityServer.ApiScopes;
using Tiger.Volo.Abp.IdentityServer.ApiScopes.Dto;
using Tiger.Volo.Abp.IdentityServer.Clients;
using Tiger.Volo.Abp.IdentityServer.Clients.Dto;
using Tiger.Volo.Abp.IdentityServer.Grants;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Tiger.Volo.Abp.IdentityServer.IdentityResources.Dto;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.Grants;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    public class AbpIdentityServerModuleAutoMapperProfile : Profile
    {
        public AbpIdentityServerModuleAutoMapperProfile()
        {
            CreateMap<ClientSecret, ClientSecretDto>();
            CreateMap<ClientScope, ClientScopeDto>();
            CreateMap<ClientGrantType, ClientGrantTypeDto>();
            CreateMap<ClientCorsOrigin, ClientCorsOriginDto>();
            CreateMap<ClientRedirectUri, ClientRedirectUriDto>();
            CreateMap<ClientPostLogoutRedirectUri, ClientPostLogoutRedirectUriDto>();
            CreateMap<ClientIdPRestriction, ClientIdPRestrictionDto>();
            CreateMap<ClientClaim, ClientClaimDto>();
            CreateMap<ClientProperty, ClientPropertyDto>();
            CreateMap<Client, ClientDto>()
            .ForMember(dto => dto.AllowedCorsOrigins,
                map => map.MapFrom(client => client.AllowedCorsOrigins.Select(origin => origin.Origin).ToList()))
            .ForMember(dto => dto.AllowedGrantTypes, 
                map => map.MapFrom(client => client.AllowedGrantTypes.Select(grantType => grantType.GrantType).ToList()))
            .ForMember(dto => dto.AllowedScopes, 
                map => map.MapFrom(client => client.AllowedScopes.Select(scope => scope.Scope).ToList()))
            .ForMember(dto => dto.IdentityProviderRestrictions, 
                map => map.MapFrom(client => client.IdentityProviderRestrictions.Select(provider => provider.Provider).ToList()))
            .ForMember(dto => dto.PostLogoutRedirectUris, 
                map => map.MapFrom(client => client.PostLogoutRedirectUris.Select(uri => uri.PostLogoutRedirectUri).ToList()))
            .ForMember(dto => dto.RedirectUris, map => 
                map.MapFrom(client => client.RedirectUris.Select(uri => uri.RedirectUri).ToList()));

            // CreateMap<ApiSecret, ApiResourceSecretDto>();
            CreateMap<ApiScopeClaim, ApiScopeClaimDto>();
            //CreateMap<ApiScopeProperty, ApiScopePropertyDto>();

            //CreateMap<ApiResourceProperty, ApiResourcePropertyDto>();
            CreateMap<ApiSecret, ApiResourceSecretDto>();
            CreateMap<ApiScope, ApiResourceScopeDto>();
            CreateMap<ApiResourceClaim, ApiResourceClaimDto>();
            CreateMap<ApiResource, ApiResourceDto>()
                .MapExtraProperties();

            CreateMap<IdentityClaim, IdentityResourceClaimDto>();
            //CreateMap<IdentityResourceProperty, IdentityResourcePropertyDto>();
            CreateMap<IdentityResource, IdentityResourceDto>()
                .MapExtraProperties();

            //CreateMap<PersistedGrant, PersistedGrantDto>()
            //    .MapExtraProperties();
        }
    }
}
