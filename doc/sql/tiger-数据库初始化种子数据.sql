USE [Tiger]
GO
INSERT [dbo].[IdentityServerApiResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Properties]) VALUES (N'34b8efba-7358-272e-a4fd-39fe8a317cfe', N'{}', N'648d019323db4b16b0e646d742dfdc59', CAST(N'2021-08-24 19:56:15.5343581' AS DateTime2), NULL, CAST(N'2021-08-24 19:56:15.8769204' AS DateTime2), NULL, 0, NULL, NULL, N'Tiger', N'Tiger API', NULL, 1, N'{}')
GO
INSERT [dbo].[IdentityServerApiScopes] ([ApiResourceId], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument]) VALUES (N'34b8efba-7358-272e-a4fd-39fe8a317cfe', N'Tiger', N'Tiger API', NULL, 0, 0, 1)
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'email', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'email_verified', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'name', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'phone_number', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'phone_number_verified', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'role', N'34b8efba-7358-272e-a4fd-39fe8a317cfe')
GO
INSERT [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'{}', N'd94b09f4ed664024bb419f8f727faa5d', CAST(N'2021-08-24 19:56:15.8767227' AS DateTime2), NULL, CAST(N'2021-08-24 19:56:15.9756303' AS DateTime2), NULL, 0, NULL, NULL, N'Tiger_Web', N'Tiger_Web', N'Tiger_Web', NULL, NULL, 1, N'oidc', 1, 0, 1, 1, 0, 0, 0, N'https://localhost:44358/Account/FrontChannelLogout', 1, NULL, 1, 1, 300, 31536000, 300, NULL, 31536000, 1296000, 1, 0, 1, 0, 1, 0, 0, N'client_', NULL, NULL, NULL, 300)
GO
INSERT [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'{}', N'2e121bc7f5534a14864f9977016b34f6', CAST(N'2021-08-24 19:56:15.9755054' AS DateTime2), NULL, CAST(N'2021-08-24 19:56:16.0468675' AS DateTime2), NULL, 0, NULL, NULL, N'Tiger_App', N'Tiger_App', N'Tiger_App', NULL, NULL, 1, N'oidc', 0, 0, 1, 1, 0, 0, 0, NULL, 1, NULL, 1, 1, 300, 31536000, 300, NULL, 31536000, 1296000, 1, 0, 1, 0, 1, 0, 0, N'client_', NULL, NULL, NULL, 300)
GO
INSERT [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'{}', N'd01bef4c033f45b293b7b54ed6c87b50', CAST(N'2021-08-24 19:56:16.0467063' AS DateTime2), NULL, CAST(N'2021-08-24 19:56:16.6581989' AS DateTime2), NULL, 0, NULL, NULL, N'Tiger_Blazor', N'Tiger_Blazor', N'Tiger_Blazor', NULL, NULL, 1, N'oidc', 0, 0, 1, 1, 0, 0, 0, NULL, 1, NULL, 1, 1, 300, 31536000, 300, NULL, 31536000, 1296000, 1, 0, 1, 0, 1, 0, 0, N'client_', NULL, NULL, NULL, 300)
GO
INSERT [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'hybrid')
GO
INSERT [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'authorization_code')
GO
INSERT [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'client_credentials')
GO
INSERT [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'password')
GO
INSERT [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'authorization_code')
GO
INSERT [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'https://localhost:44358/signout-callback-oidc')
GO
INSERT [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'http://localhost:9528')
GO
INSERT [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'https://localhost:44307/authentication/logout-callback')
GO
INSERT [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'https://localhost:44358/signin-oidc')
GO
INSERT [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'http://localhost:9528')
GO
INSERT [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'https://localhost:44307/authentication/login-callback')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'address')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'email')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'openid')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'phone')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'profile')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'role')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'53f48534-6377-72c2-0a55-39fe8a317e5b', N'Tiger')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'address')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'email')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'openid')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'phone')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'profile')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'role')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', N'Tiger')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'address')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'email')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'openid')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'phone')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'profile')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'role')
GO
INSERT [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'486319d8-5816-af39-767f-39fe8a317f2d', N'Tiger')
GO
INSERT [dbo].[IdentityServerClientSecrets] ([Type], [Value], [ClientId], [Description], [Expiration]) VALUES (N'SharedSecret', N'E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=', N'53f48534-6377-72c2-0a55-39fe8a317e5b', NULL, NULL)
GO
INSERT [dbo].[IdentityServerClientSecrets] ([Type], [Value], [ClientId], [Description], [Expiration]) VALUES (N'SharedSecret', N'E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=', N'5ff65cd9-3d0f-1091-5f45-39fe8a317ed8', NULL, NULL)
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'f0310e05-66ba-4f35-b042-39fe8a317b36', N'{}', N'836556945afa425d8f4ba2f531ffc141', CAST(N'2021-08-24 19:56:15.5339893' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'openid', N'Your user identifier', NULL, 1, 1, 0, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'addf1e65-fc4f-347b-fad9-39fe8a317c25', N'{}', N'75d4b639103341098861b53009b39df5', CAST(N'2021-08-24 19:56:15.5341980' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'profile', N'User profile', N'Your user profile information (first name, last name, etc.)', 1, 0, 1, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'5e865c3f-aa7f-4d81-6c94-39fe8a317c55', N'{}', N'8635850832fc4e5eaef8a70f1679186d', CAST(N'2021-08-24 19:56:15.5342784' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'email', N'Your email address', NULL, 1, 0, 1, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'ae56f1b0-eafa-2381-e1b0-39fe8a317c73', N'{}', N'be678b5556484d5196650504c0ea2923', CAST(N'2021-08-24 19:56:15.5342862' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'address', N'Your postal address', NULL, 1, 0, 1, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'e131c053-574c-4ae4-f6ab-39fe8a317ca4', N'{}', N'bb4b4a826709456a807eeadecbf63d65', CAST(N'2021-08-24 19:56:15.5342887' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'phone', N'Your phone number', NULL, 1, 0, 1, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'29332cb4-75ee-dd57-939a-39fe8a317cbd', N'{}', N'8dc28a8b7a144025b0e681cb672ea52a', CAST(N'2021-08-24 19:56:15.5342957' AS DateTime2), NULL, NULL, NULL, 0, NULL, NULL, N'role', N'Roles of the user', NULL, 1, 0, 0, 1, N'{}')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'sub', N'f0310e05-66ba-4f35-b042-39fe8a317b36')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'birthdate', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'family_name', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'gender', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'given_name', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'locale', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'middle_name', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'name', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'nickname', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'picture', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'preferred_username', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'profile', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'updated_at', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'website', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'zoneinfo', N'addf1e65-fc4f-347b-fad9-39fe8a317c25')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'email', N'5e865c3f-aa7f-4d81-6c94-39fe8a317c55')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'email_verified', N'5e865c3f-aa7f-4d81-6c94-39fe8a317c55')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'address', N'ae56f1b0-eafa-2381-e1b0-39fe8a317c73')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'phone_number', N'e131c053-574c-4ae4-f6ab-39fe8a317ca4')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'phone_number_verified', N'e131c053-574c-4ae4-f6ab-39fe8a317ca4')
GO
INSERT [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'role', N'29332cb4-75ee-dd57-939a-39fe8a317cbd')
GO

USE [Tiger]
GO
INSERT [dbo].[AbpOrganizationUnits] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [TenantId], [ParentId], [Code], [DisplayName]) VALUES (N'18152f38-8684-8b5b-1a67-39fe8a55d8f7', N'{}', N'aa6a5643ceed4eb8842057157ad57370', CAST(N'2021-08-24 20:35:58.3943961' AS DateTime2), N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', NULL, NULL, 1, N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', CAST(N'2021-08-24 20:36:01.4565932' AS DateTime2), NULL, NULL, N'00001', N'浙江分公司')
GO
INSERT [dbo].[AbpOrganizationUnits] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [TenantId], [ParentId], [Code], [DisplayName]) VALUES (N'9d3a8f32-f51a-034f-0bda-39fe8a55fe6a', N'{}', N'380879f2b5e043fca6fb0256ff9731c4', CAST(N'2021-08-24 20:36:07.9243563' AS DateTime2), N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', NULL, NULL, 0, NULL, NULL, NULL, NULL, N'00001', N'上海分公司')
GO
INSERT [dbo].[AbpOrganizationUnits] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [TenantId], [ParentId], [Code], [DisplayName]) VALUES (N'96eded4b-66e2-a668-00ed-39fe8a563198', N'{}', N'b932cdfff5174b75ba8fbba0140e8c62', CAST(N'2021-08-24 20:36:21.0282489' AS DateTime2), N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', CAST(N'2021-08-24 20:36:24.5805142' AS DateTime2), N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', 0, NULL, NULL, NULL, NULL, N'00002', N'北京分公司2')
GO
INSERT [dbo].[AbpRoles] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [Name], [NormalizedName], [IsDefault], [IsStatic], [IsPublic]) VALUES (N'7245d1cc-46a4-832d-b6a4-39fe8a3172a9', N'{}', N'f835d4ab-76d2-43ed-bc12-39422084838c', NULL, N'admin', N'ADMIN', 0, 1, 1)
GO
INSERT [dbo].[AbpRoles] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [Name], [NormalizedName], [IsDefault], [IsStatic], [IsPublic]) VALUES (N'562c3fce-ca6c-3631-f3fb-39fe8a55641b', N'{}', N'66a399ca-a987-432e-9f51-db64b17f6493', NULL, N'财务', N'财务', 1, 0, 1)
GO
INSERT [dbo].[AbpUsers] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [TenantId], [UserName], [NormalizedUserName], [Name], [Surname], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [IsExternal], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', N'{}', N'6234bac0886a4137838406331cc725e4', CAST(N'2021-08-24 19:56:12.6161360' AS DateTime2), NULL, CAST(N'2021-08-25 08:26:30.0321418' AS DateTime2), NULL, 0, NULL, NULL, NULL, N'admin', N'ADMIN', N'admin', NULL, N'admin@abp.io', N'ADMIN@ABP.IO', 0, N'AQAAAAEAACcQAAAAEMM0FgSdNTZr7T4H2Pw0O+JxLdUHjT3uRXSRLKpwqoWyAJgEvwTn6/5Zb4ihF2WJiQ==', N'3CN6IEXWGCKGZGK2IXZF2DRFX37NMLAD', 0, NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AbpUserRoles] ([UserId], [RoleId], [TenantId]) VALUES (N'f4f78acc-4b5e-3ca9-ba3f-39fe8a3170e4', N'7245d1cc-46a4-832d-b6a4-39fe8a3172a9', NULL)
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'd70ec25c-5609-d637-4793-39fe8a317585', NULL, N'AbpIdentity.Roles', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'f321779a-e3d5-21fd-23dc-39fe8a317597', NULL, N'AbpIdentity.Roles.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'2994860f-7f07-cb3b-8fca-39fe8a3175a6', NULL, N'AbpIdentity.Roles.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'a578410b-1eef-3d6d-5268-39fe8a3175b3', NULL, N'AbpIdentity.Roles.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'cb21ec57-8868-8a8f-19ca-39fe8a3175c1', NULL, N'AbpIdentity.Roles.ManagePermissions', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'84ddc373-d281-8c7e-747a-39fe8a3175ce', NULL, N'AbpIdentity.Users', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'f1f4f6aa-cca4-812b-006a-39fe8a3175dc', NULL, N'AbpIdentity.Users.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'3ea0f109-04ba-d243-583f-39fe8a3175eb', NULL, N'AbpIdentity.Users.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'18daf933-7f9e-7180-3f8e-39fe8a3175f8', NULL, N'AbpIdentity.Users.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'1286c1a9-b8ee-0354-03f6-39fe8a317604', NULL, N'AbpIdentity.Users.ManagePermissions', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'65112cfc-613d-5f2a-a28c-39fe8a317614', NULL, N'AbpIdentity.OrganitaionUnits', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'd16cc692-fca2-5cef-9810-39fe8a317621', NULL, N'AbpIdentity.OrganitaionUnits.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'4b30b637-7e58-23fd-1907-39fe8a317630', NULL, N'AbpIdentity.OrganitaionUnits.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'c4af4390-4776-0934-e4c4-39fe8a31764a', NULL, N'AbpIdentity.OrganitaionUnits.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'4e00dda9-0ae2-0649-d289-39fe8a317657', NULL, N'FeatureManagement.ManageHostFeatures', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'55eff808-234f-733f-8a96-39fe8a317664', NULL, N'AbpTenantManagement.Tenants', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'e44bbbbb-094f-4f81-f5c1-39fe8a317674', NULL, N'AbpTenantManagement.Tenants.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'77493cd5-cd0e-28bf-0b90-39fe8a317685', NULL, N'AbpTenantManagement.Tenants.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'ce33b050-d9a6-146d-bea7-39fe8a317697', NULL, N'AbpTenantManagement.Tenants.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'd8bebf83-e679-e42b-bd2a-39fe8a3176a8', NULL, N'AbpTenantManagement.Tenants.ManageFeatures', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'c503220d-bff4-b468-bf04-39fe8a3176b6', NULL, N'AbpTenantManagement.Tenants.ManageConnectionStrings', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'bfb227be-fc30-4328-cb5c-39fe8a3176c2', NULL, N'BookStore.Authors', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'3c3519b4-87bc-914f-02df-39fe8a3176cf', NULL, N'BookStore.Authors.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'e2af4da4-df8a-7ee1-1c64-39fe8a3176db', NULL, N'BookStore.Authors.Edit', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'c99a0df1-fdbd-5e6d-6a49-39fe8a3176ed', NULL, N'BookStore.Authors.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'36916428-472a-1273-7688-39fe8a3176fe', NULL, N'Basic.Products', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'28dda757-b9e3-6519-17bf-39fe8a31770a', NULL, N'Basic.Products.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'6b455c5c-62c6-473b-8a4a-39fe8a317719', NULL, N'Basic.Products.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'd597fb59-eadf-a6d0-abbd-39fe8a317728', NULL, N'Basic.Products.Delete', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'94e6598a-1396-0efa-941d-39fe8a317735', NULL, N'AbpAuditLogging.Default', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'aca34ad8-0fdf-c3ed-11dc-39fe8a317741', NULL, N'AbpAuditLogging.Default.Create', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'4851b93b-7088-2d40-8a61-39fe8a31774f', NULL, N'AbpAuditLogging.Default.Update', N'R', N'admin')
GO
INSERT [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'c20ec90b-e5c3-893f-fca0-39fe8a31775e', NULL, N'AbpAuditLogging.Default.Delete', N'R', N'admin')
GO
