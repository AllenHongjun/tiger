/*
Navicat SQL Server Data Transfer

Source Server         : localhost
Source Server Version : 120000
Source Host           : 127.0.0.1:1433
Source Database       : Tiger
Source Schema         : dbo

Target Server Type    : SQL Server
Target Server Version : 120000
File Encoding         : 65001

Date: 2021-08-22 23:41:50
*/


-- ----------------------------
-- Table structure for __EFMigrationsHistory
-- ----------------------------
DROP TABLE [dbo].[__EFMigrationsHistory]
GO
CREATE TABLE [dbo].[__EFMigrationsHistory] (
[MigrationId] nvarchar(150) NOT NULL ,
[ProductVersion] nvarchar(32) NOT NULL 
)


GO

-- ----------------------------
-- Records of __EFMigrationsHistory
-- ----------------------------
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20200810022238_Initial', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20201022120224_Create_Book_Entity', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20201022120717_Create_Book_Entity1', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210812063550_Created_Book_Entity', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210813171820_test1', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210816171436_Create-Author', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210817013613_Added-autherId-to-book', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210819234659_AddProduct', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210820114332_Product-change', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210820114904_change', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210820122031_add-Product-', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210820123152_Orders', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210821030326_Category', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210821040629_change-product', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210821163510_ProductAttribute', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210822071817_Update-Product', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210822073432_Update-Basic', N'3.1.6')
GO
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210822152946_ProductForeignKey', N'3.1.6')
GO
GO

-- ----------------------------
-- Table structure for AbpAuditLogActions
-- ----------------------------
DROP TABLE [dbo].[AbpAuditLogActions]
GO
CREATE TABLE [dbo].[AbpAuditLogActions] (
[Id] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[AuditLogId] uniqueidentifier NOT NULL ,
[ServiceName] nvarchar(256) NULL ,
[MethodName] nvarchar(128) NULL ,
[Parameters] nvarchar(2000) NULL ,
[ExecutionTime] datetime2(7) NOT NULL ,
[ExecutionDuration] int NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AbpAuditLogActions
-- ----------------------------
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'D6CC8724-5130-8A6E-4F3B-39FE7BC37472', null, N'A5CF689B-4C67-4237-BEDB-39FE7BC37471', N'Tiger.Basic.ProductAttributeAppService', N'CreateAsync', N'{"input":{"name":"颜色","selectType":0,"inputType":0,"inputList":"黑,红,白","sort":0,"filterType":0,"searchType":0,"relatedStatus":0,"handAddStatus":0,"type":0}}', N'2021-08-22 00:41:23.2674428', N'13', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'7DDC8062-DE05-86DC-3F5C-39FE7BC6247E', null, N'D67F814C-D79A-89CA-7307-39FE7BC6247C', N'Tiger.Basic.ProductAttributeAppService', N'CreateAsync', N'{"input":{"name":"颜色","selectType":0,"inputType":0,"inputList":"string","sort":0,"filterType":0,"searchType":0,"relatedStatus":0,"handAddStatus":0,"type":0}}', N'2021-08-22 00:44:19.0549022', N'370', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'0BAF5591-E595-0B45-15C4-39FE7BC6E354', null, N'15B610EA-280C-0FE4-43EC-39FE7BC6E353', N'Tiger.Basic.ProductAttributeAppService', N'CreateAsync', N'{"input":{"name":"容量","selectType":0,"inputType":0,"inputList":"16G,64G,128G,256G","sort":0,"filterType":0,"searchType":0,"relatedStatus":0,"handAddStatus":0,"type":0}}', N'2021-08-22 00:45:08.2812452', N'24', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'E9EA00A4-CF21-218B-3D21-39FE7BC76173', null, N'3D8CDB0B-27FB-207B-3B22-39FE7BC76173', N'Tiger.Basic.ProductAttributeAppService', N'CreateAsync', N'{"input":{"name":"体积","selectType":0,"inputType":0,"inputList":"500ml,330ml,1.5L,","sort":0,"filterType":0,"searchType":0,"relatedStatus":0,"handAddStatus":0,"type":0}}', N'2021-08-22 00:45:40.5724203', N'22', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'E7F79BD4-01F3-E140-2E57-39FE7BC80D8B', null, N'C4A8081C-E432-C0C8-3197-39FE7BC80D8A', N'Tiger.Basic.ProductAttributeAppService', N'CreateAsync', N'{"input":{"name":"屏幕尺寸","selectType":0,"inputType":0,"inputList":"3.7寸,4.7寸,6.7寸","sort":0,"filterType":0,"searchType":0,"relatedStatus":0,"handAddStatus":0,"type":0}}', N'2021-08-22 00:46:24.6274788', N'21', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'6DEED91C-9A67-85F8-B498-39FE7D7F4C53', null, N'FBA376A9-8108-4405-64E2-39FE7D7F4C52', N'Volo.Abp.PermissionManagement.PermissionsController', N'GetAsync', N'{}', N'2021-08-22 08:46:10.9479600', N'22', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'A27DA08B-9A73-A84C-5B0C-39FE7D7FA1C1', null, N'D6514197-40B4-A344-CA34-39FE7D7FA1C1', N'Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController', N'Login', N'{"login":{"userNameOrEmailAddress":"admin","rememberMe":true}}', N'2021-08-22 08:46:32.7320532', N'159', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'64C8B540-D57A-FF68-2C8C-39FE7D7FB6B1', null, N'811822D5-75F7-24AA-6690-39FE7D7FB6B1', N'Volo.Abp.PermissionManagement.PermissionsController', N'GetAsync', N'{"providerName":"R"}', N'2021-08-22 08:46:38.2459406', N'9', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'2F465BC1-5B49-8A8E-3D20-39FE7D801B08', null, N'B6BFF31D-B461-FC87-79DF-39FE7D801B08', N'Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController', N'Login', N'{"login":{"userNameOrEmailAddress":"admin","rememberMe":true}}', N'2021-08-22 08:47:03.9182127', N'25', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'01272911-1F62-CEF7-18AE-39FE7D871161', null, N'BB444A4F-4F0A-758A-1156-39FE7D871161', N'Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController', N'Login', N'{"login":{"userNameOrEmailAddress":"admin","rememberMe":true}}', N'2021-08-22 08:54:39.6483622', N'576', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'D1CC461C-ADC0-D194-4067-39FE7D872B69', null, N'21304E01-03CD-30CA-6B1D-39FE7D872B69', N'Volo.Abp.PermissionManagement.PermissionsController', N'GetAsync', N'{"providerName":"R"}', N'2021-08-22 08:54:46.8623044', N'22', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'C2B59B29-193B-70E6-7758-39FE7D872B69', null, N'21304E01-03CD-30CA-6B1D-39FE7D872B69', N'Tiger.Volo.Abp.PermissionManagement.TigerPermissionAppService', N'GetAsync', N'{"providerName":"R","providerKey":null}', N'2021-08-22 08:54:46.8660406', N'18', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'DD89A215-AC0E-1FAC-27DA-39FE7D87403E', null, N'6D8F7279-CD35-D4CD-B585-39FE7D87403E', N'Volo.Abp.PermissionManagement.PermissionsController', N'GetAsync', N'{}', N'2021-08-22 08:54:52.2139523', N'6', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'1F91AE9D-5C74-A261-6F8C-39FE7D87403E', null, N'6D8F7279-CD35-D4CD-B585-39FE7D87403E', N'Tiger.Volo.Abp.PermissionManagement.TigerPermissionAppService', N'GetAsync', N'{"providerName":null,"providerKey":null}', N'2021-08-22 08:54:52.2150827', N'4', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'35953596-82F1-80BC-16A0-39FE7D87F28B', null, N'C7D16758-A431-6289-3ADB-39FE7D87F28B', N'Volo.Abp.PermissionManagement.PermissionsController', N'GetAsync', N'{"providerName":"R","providerKey":"admin"}', N'2021-08-22 08:55:37.8576779', N'6', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'4CA3B1FF-2849-696B-3499-39FE7D87F28B', null, N'C7D16758-A431-6289-3ADB-39FE7D87F28B', N'Tiger.Volo.Abp.PermissionManagement.TigerPermissionAppService', N'GetAsync', N'{"providerName":"R","providerKey":"admin"}', N'2021-08-22 08:55:37.8607456', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'05283492-73A3-2DCE-B9D4-39FE7EA2202D', null, N'A98229E7-C72D-449F-8A98-39FE7EA2202C', N'Tiger.Basic.ProductAppService', N'CreateAsync', N'{"input":{"name":"海南哈密瓜","picture":"<string>","productSn":"fdsfds","publishStatus":1,"sort":1,"price":12.3,"subTitle":"<string>","unit":"克","detailDesc":"海南哈密瓜很好次"}}', N'2021-08-22 14:03:50.1498154', N'510', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'8FC4A2FF-D352-C0B0-99D9-39FE7EA2BF89', null, N'7D6F7DCF-496E-6F89-4631-39FE7EA2BF89', N'Tiger.Basic.ProductAppService', N'GetListAsync', N'{"input":{"filter":null,"brandId":0,"productCategoryId":0,"publishStatus":0,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 14:04:31.4471530', N'3', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'077A5C67-5D1F-C0D4-FBAE-39FE7EA31190', null, N'7BFFC665-58EF-57F0-959A-39FE7EA31190', N'Tiger.Basic.ProductAppService', N'GetListAsync', N'{"input":{"filter":null,"brandId":1,"productCategoryId":1,"publishStatus":0,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 14:04:52.4924409', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'CA3E687B-9F28-DF66-E763-39FE7EE8A543', null, N'64A8849A-7217-543B-1A6D-39FE7EE8A541', N'Tiger.Basic.ProductAppService', N'CreateAsync', N'{"input":{"productCategoryId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","productAttributeTypeId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","name":"大鸡腿","productSn":"326262323","publishStatus":1,"newStatus":0,"recommandStatus":0,"verifyStatus":0,"sort":0,"sale":0,"note":"大鸡腿备注","picture":"string","albumPics":"string","detailTitle":"string","subTitle":"string","unit":"string","keywords":"string","detailDesc":"string","price":0.0,"oriPrice":0.0,"purchasePrice":0.0,"promotionPrice":0.0,"giftGrowth":0,"giftIntegration":0,"stock":0,"lowStock":0,"weight":0.0,"previewStatus":0,"promotionType":0,"promotionStartTime":"2021-08-22T07:19:22.479Z","promotionEndTime":"2021-08-22T07:19:22.479Z","promotionPerLimit":0,"categoryName":"肉禽"}}', N'2021-08-22 15:20:51.9095227', N'345', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'A0DC981F-004D-AB02-9DF8-39FE7F04D223', null, N'EA53988F-1A63-FF9B-DB01-39FE7F04D221', N'Volo.Abp.Identity.IdentityUserController', N'GetListAsync', N'{"input":{"filter":null,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 15:51:38.7630876', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'A8F2345E-6ACB-C084-0FE9-39FE7F05435C', null, N'9360BDF1-F046-E1F0-7C9E-39FE7F05435C', N'Volo.Abp.Identity.IdentityUserController', N'GetListAsync', N'{"input":{"filter":null,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 15:52:07.7701134', N'0', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'A8630D78-F53F-7EFE-3186-39FE804B9889', null, N'8DC6A965-C175-6151-6C05-39FE804B9888', N'Volo.Abp.Identity.IdentityUserController', N'GetListAsync', N'{"input":{"filter":null,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 21:48:34.2847997', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'F7B892CA-B622-3B1C-9080-39FE804BBEE6', null, N'421F4EDE-A370-BF48-3633-39FE804BBEE6', N'Volo.Abp.Identity.IdentityUserController', N'GetListAsync', N'{"input":{"filter":null,"sorting":null,"skipCount":0,"maxResultCount":10}}', N'2021-08-22 21:48:44.1328126', N'0', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'9D86962E-B243-7D02-B2A3-39FE80673C68', null, N'55BEF81A-4780-E0FF-F9E3-39FE80673C66', N'Tiger.Controllers.Admin.Basics.ProductController', N'CreateAsync', N'{"input":{"productCategoryId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","productAttributeTypeId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","name":"string","productSn":"","publishStatus":0,"newStatus":0,"recommandStatus":0,"sort":0,"note":"string","picture":"string","albumPics":"string","detailTitle":"string","subTitle":"string","unit":"string","keywords":"string","detailDesc":"string","price":0.0,"oriPrice":0.0,"purchasePrice":0.0,"promotionPrice":0.0,"giftGrowth":0,"giftIntegration":0,"stock":0,"lowStock":0,"weight":0.0,"previewStatus":0,"promotionType":0,"promotionStartTime":"2021-08-22T14:18:34.713Z","promotionEndTime":"2021-08-22T14:18:34.713Z","promotionPerLimit":0,"categoryName":"string"}}', N'2021-08-22 22:18:45.6735271', N'7', N'{}')
GO
GO
INSERT INTO [dbo].[AbpAuditLogActions] ([Id], [TenantId], [AuditLogId], [ServiceName], [MethodName], [Parameters], [ExecutionTime], [ExecutionDuration], [ExtraProperties]) VALUES (N'6B53253B-A6BE-AC85-664C-39FE80677EB0', null, N'C009AB9B-98F4-018C-02F2-39FE80677EAF', N'Tiger.Controllers.Admin.Basics.ProductController', N'CreateAsync', N'{"input":{"productCategoryId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","productAttributeTypeId":"3fa85f64-5717-4562-b3fc-2c963f66afa6","name":"","productSn":"","publishStatus":0,"newStatus":0,"recommandStatus":0,"sort":0,"note":"string","picture":"string","albumPics":"string","detailTitle":"string","subTitle":"string","unit":"string","keywords":"string","detailDesc":"string","price":0.0,"oriPrice":0.0,"purchasePrice":0.0,"promotionPrice":0.0,"giftGrowth":0,"giftIntegration":0,"stock":0,"lowStock":0,"weight":0.0,"previewStatus":0,"promotionType":0,"promotionStartTime":"2021-08-22T14:18:34.713Z","promotionEndTime":"2021-08-22T14:18:34.713Z","promotionPerLimit":0,"categoryName":"string"}}', N'2021-08-22 22:19:02.7012871', N'0', N'{}')
GO
GO

-- ----------------------------
-- Table structure for AbpAuditLogs
-- ----------------------------
DROP TABLE [dbo].[AbpAuditLogs]
GO
CREATE TABLE [dbo].[AbpAuditLogs] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[ApplicationName] nvarchar(96) NULL ,
[UserId] uniqueidentifier NULL ,
[UserName] nvarchar(256) NULL ,
[TenantId] uniqueidentifier NULL ,
[TenantName] nvarchar(MAX) NULL ,
[ImpersonatorUserId] uniqueidentifier NULL ,
[ImpersonatorTenantId] uniqueidentifier NULL ,
[ExecutionTime] datetime2(7) NOT NULL ,
[ExecutionDuration] int NOT NULL ,
[ClientIpAddress] nvarchar(64) NULL ,
[ClientName] nvarchar(128) NULL ,
[ClientId] nvarchar(64) NULL ,
[CorrelationId] nvarchar(64) NULL ,
[BrowserInfo] nvarchar(512) NULL ,
[HttpMethod] nvarchar(16) NULL ,
[Url] nvarchar(256) NULL ,
[Exceptions] nvarchar(4000) NULL ,
[Comments] nvarchar(256) NULL ,
[HttpStatusCode] int NULL 
)


GO

-- ----------------------------
-- Records of AbpAuditLogs
-- ----------------------------
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'A5CF689B-4C67-4237-BEDB-39FE7BC37471', N'{}', N'14e40d8620534a4ab0a61f6070f3b004', null, null, null, null, null, null, null, N'2021-08-22 00:41:23.2424393', N'62', N'::1', null, null, N'd586676e524f495b9d735d7bb18a2bbb', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/app/productAttribute', N'AutoMapper.AutoMapperMappingException: Missing type map configuration or unsupported mapping.

Mapping types:
Object -> ProductAttribute
System.Object -> Tiger.Basic.ProductAttribute
   at lambda_method(Closure , Object , ProductAttribute , ResolutionContext )
   at Volo.Abp.AutoMapper.AutoMapperAutoObjectMappingProvider.Map[TSource,TDestination](Object source)
   at Volo.Abp.ObjectMapping.DefaultObjectMapper.AutoMap[TSource,TDestination](Object source)
   at Volo.Abp.ObjectMapping.DefaultObjectMapper.Map[TSource,TDestination](TSource source)
   at Volo.Abp.Application.Services.AbstractKeyCrudAppService`7.MapToEntity(TCreateInput createInput)
   at Castle.Proxies.ProductAttributeAppServiceProxy.MapToEntity_callback_1(CreateUpdateProductAttributeDto createInput)
   at Castle.Proxies.Invocations.AbstractKeyCrudAppService`7_MapToEntity_8.InvokeMethodOnTarget()
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.DynamicProxy.AbstractInvocation.ProceedInfo.Invoke()
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedSynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
--- End of stack trace from previous location where exception was thrown ---
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Auditing.AuditingInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.Rethrow(Exception exception)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.RethrowInnerIfAggregate(Exception exception)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.RethrowIfFaulted(Task task)
   at Castle.DynamicProxy.AsyncInterceptorBase.InterceptSynchronousResult[TResult](AsyncInterceptorBase me, IInvocation invocation)
   at Castle.DynamicProxy.AsyncInterceptorBase.Castle.DynamicProxy.IAsyncInterceptor.InterceptSynchronous(IInvocation invocation)
   at Castle.DynamicProxy.AsyncDeterminationInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.DynamicProxy.AbstractInvocation.ProceedInfo.Invoke()
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedSynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
--- End of stack trace from previous location where exception was thrown ---
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.Rethrow(Exception exception)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.RethrowInnerIfAggregate(Exception exception)
   at Castle.DynamicProxy.NoCoverage.RethrowHelper.RethrowIfFaulted(Task task)
   at Castle.DynamicProxy.AsyncInterceptorBase.InterceptSynchronousResult[TResult](AsyncInterceptorBase me, IInvocation invocation)
   at Castle.DynamicProxy.AsyncInterceptorBase.Castle.DynamicProxy.IAsyncInterceptor.InterceptSynchronous(IInvocation invocation)
   at Castle.DynamicProxy.AsyncDeterminationInterceptor.Intercept(IInvocation invocation)
   at Castle.DynamicProxy.AbstractInvocation.Proceed()
   at Castle.DynamicProxy.AbstractInvocation.ProceedInfo.Invoke()
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedSynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
--- End of stack trace from previous location where exception was thrown ---
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.Inter', N'', N'500')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'D67F814C-D79A-89CA-7307-39FE7BC6247C', N'{}', N'ead7fc81b59f4029b50e21c89a067363', null, null, null, null, null, null, null, N'2021-08-22 00:44:18.8708468', N'572', N'::1', null, null, N'03ac450d71ab454a99cc9049b93e5588', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/app/productAttribute', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'15B610EA-280C-0FE4-43EC-39FE7BC6E353', N'{}', N'f6256036bf9845dc846ea50ac8306f9e', null, null, null, null, null, null, null, N'2021-08-22 00:45:08.2752268', N'32', N'::1', null, null, N'e5f687c11a814f4091b629787210255d', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/app/productAttribute', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'3D8CDB0B-27FB-207B-3B22-39FE7BC76173', N'{}', N'11db2e31617c492e8f2174c47dfbc855', null, null, null, null, null, null, null, N'2021-08-22 00:45:40.5666303', N'28', N'::1', null, null, N'218289e0fda54c8aa8951c6f135774d6', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/app/productAttribute', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'C4A8081C-E432-C0C8-3197-39FE7BC80D8A', N'{}', N'65d4d6ccb64d4cb9be3789eb92f4a700', null, null, null, null, null, null, null, N'2021-08-22 00:46:24.6096802', N'41', N'::1', null, null, N'65dc772aebbb4edab886c541fed0b2c8', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/app/productAttribute', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'FBA376A9-8108-4405-64E2-39FE7D7F4C52', N'{}', N'a55b4bf84c8d47a186569af4eeb29fa8', null, null, null, null, null, null, null, N'2021-08-22 08:46:10.7829089', N'231', N'::1', null, null, N'bdc6271ce5ee49e2a5afe822d149a95d', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/permission-management/permissions', N'Volo.Abp.Authorization.AbpAuthorizationException: Authorization failed! Given policy has not granted.
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.CheckAsync(IAuthorizationService authorizationService, AuthorizationPolicy policy)
   at Volo.Abp.Authorization.MethodInvocationAuthorizationService.CheckAsync(MethodInvocationAuthorizationContext context)
   at Volo.Abp.Authorization.AuthorizationInterceptor.AuthorizeAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Authorization.AuthorizationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at lambda_method(Closure , Object )
   at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'401')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'D6514197-40B4-A344-CA34-39FE7D7FA1C1', N'{}', N'3fb7f70699a44cb99d8f77daedee451c', null, null, null, null, null, null, null, N'2021-08-22 08:46:32.2646852', N'633', N'::1', null, null, N'f495e03efe04496f87a9d77d21fef70f', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/account/login', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'811822D5-75F7-24AA-6690-39FE7D7FB6B1', N'{}', N'abe2f3c441c24f2d86db31fc1f2ce6fa', null, null, null, null, null, null, null, N'2021-08-22 08:46:38.2407197', N'16', N'::1', null, null, N'd6995f9511af4403ba1812d753b8fb0c', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/permission-management/permissions', N'Volo.Abp.Authorization.AbpAuthorizationException: Authorization failed! Given policy has not granted.
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.CheckAsync(IAuthorizationService authorizationService, AuthorizationPolicy policy)
   at Volo.Abp.Authorization.MethodInvocationAuthorizationService.CheckAsync(MethodInvocationAuthorizationContext context)
   at Volo.Abp.Authorization.AuthorizationInterceptor.AuthorizeAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Authorization.AuthorizationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at lambda_method(Closure , Object )
   at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'401')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'B6BFF31D-B461-FC87-79DF-39FE7D801B08', N'{}', N'54a454afeace44219b7fb98e14dc6b50', null, null, null, null, null, null, null, N'2021-08-22 08:47:03.8784961', N'66', N'::1', null, null, N'd1538115ddca45fd919474dcf532dd78', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/account/login', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'BB444A4F-4F0A-758A-1156-39FE7D871161', N'{}', N'59c1da73201e47108690d62238d43463', null, null, null, null, null, null, null, N'2021-08-22 08:54:39.6231968', N'602', N'::1', null, null, N'830dff95d3404a79a38438c87f9ddabe', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/account/login', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'21304E01-03CD-30CA-6B1D-39FE7D872B69', N'{}', N'd4d01271c50a41bdbc1e967223884508', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 08:54:46.8575133', N'31', N'::1', null, null, N'96d9e3549e4b4555b2dfa0e3d725fa15', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/permission-management/permissions', N'Volo.Abp.AbpException: Duplicate permission name: Basic.Products
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.AddPermissionToDictionaryRecursively(Dictionary`2 permissions, PermissionDefinition permission)
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.CreatePermissionDefinitions()
   at System.Lazy`1.ViaFactory(LazyThreadSafetyMode mode)
   at System.Lazy`1.ExecutionAndPublication(LazyHelper executionAndPublication, Boolean useDefaultConstructor)
   at System.Lazy`1.CreateValue()
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.get_PermissionDefinitions()
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.GetOrNull(String name)
   at Volo.Abp.Authorization.AbpAuthorizationPolicyProvider.GetPolicyAsync(String policyName)
   at Microsoft.AspNetCore.Authorization.DefaultAuthorizationService.AuthorizeAsync(ClaimsPrincipal user, Object resource, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.AuthorizeAsync(IAuthorizationService authorizationService, Object resource, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.AuthorizeAsync(IAuthorizationService authorizationService, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.IsGrantedAsync(IAuthorizationService authorizationService, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.CheckAsync(IAuthorizationService authorizationService, String policyName)
   at Volo.Abp.PermissionManagement.PermissionAppService.CheckProviderPolicy(String providerName)
   at Volo.Abp.PermissionManagement.PermissionAppService.GetAsync(String providerName, String providerKey)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Auditing.AuditingInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Authorization.AuthorizationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at lambda_method(Closure , Object )
   at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult()
   at Microsoft.AspNetCore.Mvc.Infrastructure.Actio', N'', N'500')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'6D8F7279-CD35-D4CD-B585-39FE7D87403E', N'{}', N'6dd1f8a65f654ef09bbdbc5162e91889', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 08:54:52.2086204', N'13', N'::1', null, null, N'26752434dc124dde852a7022febe2a12', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/permission-management/permissions', N'System.ArgumentNullException: Value cannot be null. (Parameter ''key'')
   at System.Collections.Generic.Dictionary`2.FindEntry(TKey key)
   at System.Collections.Generic.Dictionary`2.TryGetValue(TKey key, TValue& value)
   at System.Collections.Generic.AbpDictionaryExtensions.GetOrDefault[TKey,TValue](Dictionary`2 dictionary, TKey key)
   at Volo.Abp.PermissionManagement.PermissionAppService.CheckProviderPolicy(String providerName)
   at Volo.Abp.PermissionManagement.PermissionAppService.GetAsync(String providerName, String providerKey)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Auditing.AuditingInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Authorization.AuthorizationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at lambda_method(Closure , Object )
   at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
System.ArgumentNullException: Value cannot be null. (', N'', N'500')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'C7D16758-A431-6289-3ADB-39FE7D87F28B', N'{}', N'b7bd7256f0df44bc89cd299a84d3d652', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 08:55:37.8521951', N'14', N'::1', null, null, N'76f7d204a8a0408d91ad0f746c3f5e50', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/permission-management/permissions', N'Volo.Abp.AbpException: Duplicate permission name: Basic.Products
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.AddPermissionToDictionaryRecursively(Dictionary`2 permissions, PermissionDefinition permission)
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.CreatePermissionDefinitions()
   at System.Lazy`1.ViaFactory(LazyThreadSafetyMode mode)
--- End of stack trace from previous location where exception was thrown ---
   at System.Lazy`1.CreateValue()
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.get_PermissionDefinitions()
   at Volo.Abp.Authorization.Permissions.PermissionDefinitionManager.GetOrNull(String name)
   at Volo.Abp.Authorization.AbpAuthorizationPolicyProvider.GetPolicyAsync(String policyName)
   at Microsoft.AspNetCore.Authorization.DefaultAuthorizationService.AuthorizeAsync(ClaimsPrincipal user, Object resource, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.AuthorizeAsync(IAuthorizationService authorizationService, Object resource, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.AuthorizeAsync(IAuthorizationService authorizationService, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.IsGrantedAsync(IAuthorizationService authorizationService, String policyName)
   at Microsoft.AspNetCore.Authorization.AbpAuthorizationServiceExtensions.CheckAsync(IAuthorizationService authorizationService, String policyName)
   at Volo.Abp.PermissionManagement.PermissionAppService.CheckProviderPolicy(String providerName)
   at Volo.Abp.PermissionManagement.PermissionAppService.GetAsync(String providerName, String providerKey)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Auditing.AuditingInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Authorization.AuthorizationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Validation.ValidationInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at Castle.DynamicProxy.AsyncInterceptorBase.ProceedAsynchronous[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo)
   at Volo.Abp.Castle.DynamicProxy.CastleAbpMethodInvocationAdapterWithReturnValue`1.ProceedAsync()
   at Volo.Abp.Uow.UnitOfWorkInterceptor.InterceptAsync(IAbpMethodInvocation invocation)
   at Volo.Abp.Castle.DynamicProxy.CastleAsyncAbpInterceptorAdapter`1.InterceptAsync[TResult](IInvocation invocation, IInvocationProceedInfo proceedInfo, Func`3 proceed)
   at lambda_method(Closure , Object )
   at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult()
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectRes', N'', N'500')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'A98229E7-C72D-449F-8A98-39FE7EA2202C', N'{}', N'12f8da8e9d3a4364adc963c75666b506', null, null, null, null, null, null, null, N'2021-08-22 14:03:49.8474348', N'846', N'::1', null, null, N'd1c8b097a8cd4188ade6aa84d42016c4', N'PostmanRuntime/7.26.8', N'POST', N'/api/app/product', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'7D6F7DCF-496E-6F89-4631-39FE7EA2BF89', N'{}', N'dc6efe73ce114a35ba267ee338df2a09', null, null, null, null, null, null, null, N'2021-08-22 14:04:31.2458024', N'251', N'::1', null, null, N'1fea77ab6e724609b4980a6a0cae5316', N'PostmanRuntime/7.26.8', N'GET', N'/api/app/product', N'Volo.Abp.Validation.AbpValidationException: ModelState is not valid! See ValidationErrors for details.
   at Volo.Abp.AspNetCore.Mvc.Validation.ModelStateValidator.Validate(ModelStateDictionary modelState)
   at Volo.Abp.AspNetCore.Mvc.Validation.AbpValidationActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'400')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'7BFFC665-58EF-57F0-959A-39FE7EA31190', N'{}', N'80118502cd0f477d8d816c6d0a285e13', null, null, null, null, null, null, null, N'2021-08-22 14:04:52.4744868', N'21', N'::1', null, null, N'63cf8be0488e4078a700d6a77ba44d11', N'PostmanRuntime/7.26.8', N'GET', N'/api/app/product', N'Volo.Abp.Validation.AbpValidationException: ModelState is not valid! See ValidationErrors for details.
   at Volo.Abp.AspNetCore.Mvc.Validation.ModelStateValidator.Validate(ModelStateDictionary modelState)
   at Volo.Abp.AspNetCore.Mvc.Validation.AbpValidationActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'400')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'64A8849A-7217-543B-1A6D-39FE7EE8A541', N'{}', N'7b36a1dcc7984ee6b3e245666fea5aea', null, null, null, null, null, null, null, N'2021-08-22 15:20:51.6855370', N'594', N'::1', null, null, N'7066d5a0b6374bec8d568ceb97503669', N'PostmanRuntime/7.26.8', N'POST', N'/api/app/product', N'', N'', N'200')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'EA53988F-1A63-FF9B-DB01-39FE7F04D221', N'{}', N'66cf18b58e4149e9adf3bc5a36ff52be', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 15:51:38.7448450', N'36', N'::1', null, N'Tiger_App', N'f62b18a9920f48fab9b5562d55caf6b2', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/identity/users', N'System.NotImplementedException: The method or operation is not implemented.
   at Tiger.Books.Demo.TestAppService.GetListAsync(GetIdentityUsersInput input) in D:\data\learning\project\tiger-admin\aspnet-core\src\Tiger.Application\Books\Demo\TestAppService.cs:line 65
   at Volo.Abp.Identity.IdentityUserController.GetListAsync(GetIdentityUsersInput input)
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.ExecuteAsync(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'501')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'9360BDF1-F046-E1F0-7C9E-39FE7F05435C', N'{}', N'8991dfaf3af0459ea41ca043b8acb70c', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 15:52:07.7582193', N'14', N'::1', null, N'Tiger_App', N'32cd8874683a4559ab606b4a95267c10', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/identity/users', N'System.NotImplementedException: The method or operation is not implemented.
   at Tiger.Books.Demo.TestAppService.GetListAsync(GetIdentityUsersInput input) in D:\data\learning\project\tiger-admin\aspnet-core\src\Tiger.Application\Books\Demo\TestAppService.cs:line 65
   at Volo.Abp.Identity.IdentityUserController.GetListAsync(GetIdentityUsersInput input)
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.ExecuteAsync(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'501')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'8DC6A965-C175-6151-6C05-39FE804B9888', N'{}', N'c9598122d3294895ba29a29a2390db16', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 21:48:34.2631316', N'43', N'::1', null, N'Tiger_App', N'0789a20bc2d74c0cb7270dfb65fb0981', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/identity/users', N'System.NotImplementedException: The method or operation is not implemented.
   at Tiger.Books.Demo.TestAppService.GetListAsync(GetIdentityUsersInput input) in D:\data\learning\project\tiger-admin\aspnet-core\src\Tiger.Application\Books\Demo\TestAppService.cs:line 65
   at Volo.Abp.Identity.IdentityUserController.GetListAsync(GetIdentityUsersInput input)
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.ExecuteAsync(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'501')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'421F4EDE-A370-BF48-3633-39FE804BBEE6', N'{}', N'5bb0e1ee67714f2fb9c980c500a85b2f', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 21:48:44.1184065', N'16', N'::1', null, N'Tiger_App', N'c92341b9c9d04676b093013fe1ab6951', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'GET', N'/api/identity/users', N'System.NotImplementedException: The method or operation is not implemented.
   at Tiger.Books.Demo.TestAppService.GetListAsync(GetIdentityUsersInput input) in D:\data\learning\project\tiger-admin\aspnet-core\src\Tiger.Application\Books\Demo\TestAppService.cs:line 65
   at Volo.Abp.Identity.IdentityUserController.GetListAsync(GetIdentityUsersInput input)
   at lambda_method(Closure , Object , Object[] )
   at Microsoft.Extensions.Internal.ObjectMethodExecutor.ExecuteAsync(Object target, Object[] parameters)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'501')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'55BEF81A-4780-E0FF-F9E3-39FE80673C66', N'{}', N'c3850930deac4350ac86ec92a55f1b06', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 22:18:45.3592195', N'363', N'::1', null, null, N'7dc4101a6ab644e0801fc1e48edc3b14', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/basic/product', N'Volo.Abp.Validation.AbpValidationException: ModelState is not valid! See ValidationErrors for details.
   at Volo.Abp.AspNetCore.Mvc.Validation.ModelStateValidator.Validate(ModelStateDictionary modelState)
   at Volo.Abp.AspNetCore.Mvc.Validation.AbpValidationActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'400')
GO
GO
INSERT INTO [dbo].[AbpAuditLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [ApplicationName], [UserId], [UserName], [TenantId], [TenantName], [ImpersonatorUserId], [ImpersonatorTenantId], [ExecutionTime], [ExecutionDuration], [ClientIpAddress], [ClientName], [ClientId], [CorrelationId], [BrowserInfo], [HttpMethod], [Url], [Exceptions], [Comments], [HttpStatusCode]) VALUES (N'C009AB9B-98F4-018C-02F2-39FE80677EAF', N'{}', N'6d8475226ed442e8b7ca24fd0b0e0661', null, N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, null, null, N'2021-08-22 22:19:02.6736250', N'30', N'::1', null, null, N'3b886d0d782a43b2a9a7123730b0fdec', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'POST', N'/api/basic/product', N'Volo.Abp.Validation.AbpValidationException: ModelState is not valid! See ValidationErrors for details.
   at Volo.Abp.AspNetCore.Mvc.Validation.ModelStateValidator.Validate(ModelStateDictionary modelState)
   at Volo.Abp.AspNetCore.Mvc.Validation.AbpValidationActionFilter.OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync()
--- End of stack trace from previous location where exception was thrown ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeNextExceptionFilterAsync>g__Awaited|25_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)', N'', N'400')
GO
GO

-- ----------------------------
-- Table structure for AbpBackgroundJobs
-- ----------------------------
DROP TABLE [dbo].[AbpBackgroundJobs]
GO
CREATE TABLE [dbo].[AbpBackgroundJobs] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[JobName] nvarchar(128) NOT NULL ,
[JobArgs] nvarchar(MAX) NOT NULL ,
[TryCount] smallint NOT NULL DEFAULT (CONVERT([smallint],(0))) ,
[CreationTime] datetime2(7) NOT NULL ,
[NextTryTime] datetime2(7) NOT NULL ,
[LastTryTime] datetime2(7) NULL ,
[IsAbandoned] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[Priority] tinyint NOT NULL DEFAULT (CONVERT([tinyint],(15))) 
)


GO

-- ----------------------------
-- Records of AbpBackgroundJobs
-- ----------------------------

-- ----------------------------
-- Table structure for AbpClaimTypes
-- ----------------------------
DROP TABLE [dbo].[AbpClaimTypes]
GO
CREATE TABLE [dbo].[AbpClaimTypes] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[Name] nvarchar(256) NOT NULL ,
[Required] bit NOT NULL ,
[IsStatic] bit NOT NULL ,
[Regex] nvarchar(512) NULL ,
[RegexDescription] nvarchar(128) NULL ,
[Description] nvarchar(256) NULL ,
[ValueType] int NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpClaimTypes
-- ----------------------------
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'D2BA328C-59C0-C692-F980-39FE7D867C31', N'{}', N'e177ee41d96248fd9e4938e452685245', N'sub', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'59F2C60C-C94E-2804-CC48-39FE7D867D18', N'{}', N'0cc8a652bfad442fa4a2caaee8acd7b1', N'name', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'E9CBB6AC-2AEE-FD5B-5A6C-39FE7D867D1A', N'{}', N'89245595f1374527b0a0d9d298f7ebd0', N'family_name', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'B4BC1A80-A6FA-A44D-0DC6-39FE7D867D1B', N'{}', N'a41ef3382ebb4b729ad234effbd60a1c', N'given_name', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'5C4F7BDF-EA99-5A63-4FFE-39FE7D867D1D', N'{}', N'eaf0a088c92644ca8013278ea4ab9cf2', N'middle_name', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'89C39A68-0F51-583D-587F-39FE7D867D1E', N'{}', N'afd79819ddc346a58e26f02d7e753a96', N'nickname', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'5BF62290-BFA6-C571-81FC-39FE7D867D1F', N'{}', N'fb271bfc5e9e42a3ba81af573aa71717', N'preferred_username', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'0D1CC950-DF7E-94C8-0604-39FE7D867D21', N'{}', N'c520d73e94f04a56b39426bea2f4db15', N'profile', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'A7F87442-75EC-B7FC-BBE0-39FE7D867D23', N'{}', N'2e2eef1c57a94665b033b1591e87a959', N'picture', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'20390DA5-89D3-B5F3-0419-39FE7D867D24', N'{}', N'79e49e9b93824791b57bd309ee084256', N'website', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'2F063E27-F41D-F360-F578-39FE7D867D26', N'{}', N'494734de13724b788863a123f6f1f65c', N'gender', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'66C7F6D5-F929-1388-99BA-39FE7D867D28', N'{}', N'8dc75a1906e44823bbbcaad1a023246c', N'birthdate', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'3F926DC7-D303-3A69-35D2-39FE7D867D2A', N'{}', N'92b9db8dd1cd480ea55cd931d7c0381a', N'zoneinfo', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'0B055A2E-055E-B537-8290-39FE7D867D2C', N'{}', N'152f0bcf8ae04f3c923157543b284ee3', N'locale', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'DAA90C82-998E-1AAF-0D1D-39FE7D867D2E', N'{}', N'7291337563c24b8a99e98319ed2366e0', N'updated_at', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'462933BA-EC4F-4D09-F376-39FE7D867D35', N'{}', N'7626ba1c1b484df695079b8f1e510eeb', N'email', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'59E56EF0-6AC7-DA8F-A128-39FE7D867D36', N'{}', N'c11c5ce9376346ceb065354f44f1c77e', N'email_verified', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'CFF77E56-0A7B-7E6C-2635-39FE7D867D3A', N'{}', N'139d79eac27e4afabe2a92bc1c43b5bb', N'address', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'A34AD2BA-FD82-E6DF-0390-39FE7D867D3E', N'{}', N'5216beb776e745f1808247360a0eeb60', N'phone_number', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'66924006-666C-27EB-80C5-39FE7D867D3F', N'{}', N'74f12d0d242b445aa744ec0e567e3784', N'phone_number_verified', N'0', N'1', null, null, null, N'0')
GO
GO
INSERT INTO [dbo].[AbpClaimTypes] ([Id], [ExtraProperties], [ConcurrencyStamp], [Name], [Required], [IsStatic], [Regex], [RegexDescription], [Description], [ValueType]) VALUES (N'918DF35C-6132-2BF7-69B1-39FE7D867D42', N'{}', N'399296b5ab364d99ac2884a34e9efe02', N'role', N'0', N'1', null, null, null, N'0')
GO
GO

-- ----------------------------
-- Table structure for AbpEntityChanges
-- ----------------------------
DROP TABLE [dbo].[AbpEntityChanges]
GO
CREATE TABLE [dbo].[AbpEntityChanges] (
[Id] uniqueidentifier NOT NULL ,
[AuditLogId] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[ChangeTime] datetime2(7) NOT NULL ,
[ChangeType] tinyint NOT NULL ,
[EntityTenantId] uniqueidentifier NULL ,
[EntityId] nvarchar(128) NOT NULL ,
[EntityTypeFullName] nvarchar(128) NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AbpEntityChanges
-- ----------------------------

-- ----------------------------
-- Table structure for AbpEntityPropertyChanges
-- ----------------------------
DROP TABLE [dbo].[AbpEntityPropertyChanges]
GO
CREATE TABLE [dbo].[AbpEntityPropertyChanges] (
[Id] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[EntityChangeId] uniqueidentifier NOT NULL ,
[NewValue] nvarchar(512) NULL ,
[OriginalValue] nvarchar(512) NULL ,
[PropertyName] nvarchar(128) NOT NULL ,
[PropertyTypeFullName] nvarchar(64) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpEntityPropertyChanges
-- ----------------------------

-- ----------------------------
-- Table structure for AbpFeatureValues
-- ----------------------------
DROP TABLE [dbo].[AbpFeatureValues]
GO
CREATE TABLE [dbo].[AbpFeatureValues] (
[Id] uniqueidentifier NOT NULL ,
[Name] nvarchar(128) NOT NULL ,
[Value] nvarchar(128) NOT NULL ,
[ProviderName] nvarchar(64) NULL ,
[ProviderKey] nvarchar(64) NULL 
)


GO

-- ----------------------------
-- Records of AbpFeatureValues
-- ----------------------------

-- ----------------------------
-- Table structure for AbpOrganizationUnitRoles
-- ----------------------------
DROP TABLE [dbo].[AbpOrganizationUnitRoles]
GO
CREATE TABLE [dbo].[AbpOrganizationUnitRoles] (
[RoleId] uniqueidentifier NOT NULL ,
[OrganizationUnitId] uniqueidentifier NOT NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AbpOrganizationUnitRoles
-- ----------------------------

-- ----------------------------
-- Table structure for AbpOrganizationUnits
-- ----------------------------
DROP TABLE [dbo].[AbpOrganizationUnits]
GO
CREATE TABLE [dbo].[AbpOrganizationUnits] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[TenantId] uniqueidentifier NULL ,
[ParentId] uniqueidentifier NULL ,
[Code] nvarchar(95) NOT NULL ,
[DisplayName] nvarchar(128) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpOrganizationUnits
-- ----------------------------

-- ----------------------------
-- Table structure for AbpPermissionGrants
-- ----------------------------
DROP TABLE [dbo].[AbpPermissionGrants]
GO
CREATE TABLE [dbo].[AbpPermissionGrants] (
[Id] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[Name] nvarchar(128) NOT NULL ,
[ProviderName] nvarchar(64) NOT NULL ,
[ProviderKey] nvarchar(64) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpPermissionGrants
-- ----------------------------
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'34DE4BD6-6655-A324-4EFD-39FE7D86781A', null, N'AbpIdentity.Roles', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'8A916E07-C003-3F75-1622-39FE7D86781F', null, N'AbpIdentity.Roles.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'820CE56C-A92D-1C86-FB6A-39FE7D867821', null, N'AbpIdentity.Roles.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'130AC0B4-26C0-C577-D422-39FE7D867822', null, N'AbpIdentity.Roles.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'C286416B-8B14-CCE9-7EC6-39FE7D867824', null, N'AbpIdentity.Roles.ManagePermissions', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'0AEB975B-7688-426A-187A-39FE7D867826', null, N'AbpIdentity.Users', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'94C6F840-A7EB-E85E-AA56-39FE7D867827', null, N'AbpIdentity.Users.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'F209B5AC-E08F-AC72-C618-39FE7D867828', null, N'AbpIdentity.Users.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'8C7D83BB-38B4-3014-BDC9-39FE7D867829', null, N'AbpIdentity.Users.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'F84112FB-C1E0-618A-A8C1-39FE7D86782A', null, N'AbpIdentity.Users.ManagePermissions', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'7AFC722F-A455-CE82-1338-39FE7D86782B', null, N'AbpIdentity.OrganitaionUnits', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'EB03BF18-D72B-B76A-2498-39FE7D86782D', null, N'AbpIdentity.OrganitaionUnits.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'B54CDDE5-AD01-2723-A6EA-39FE7D86782F', null, N'AbpIdentity.OrganitaionUnits.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'D5AA2729-0C92-0ADC-8BAB-39FE7D867830', null, N'AbpIdentity.OrganitaionUnits.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'96F58B55-C5F8-B46B-A2DF-39FE7D867831', null, N'FeatureManagement.ManageHostFeatures', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'A7BC9563-9245-7FB2-906F-39FE7D867832', null, N'AbpTenantManagement.Tenants', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'EE2149E7-B8F8-C254-74B7-39FE7D867833', null, N'AbpTenantManagement.Tenants.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'34220559-1762-0918-847F-39FE7D867834', null, N'AbpTenantManagement.Tenants.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'0D72C6A3-6BF1-8066-2C13-39FE7D867835', null, N'AbpTenantManagement.Tenants.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'E27811B0-F90E-55DC-0C69-39FE7D867836', null, N'AbpTenantManagement.Tenants.ManageFeatures', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'DACDBAED-0160-B63F-D935-39FE7D867837', null, N'AbpTenantManagement.Tenants.ManageConnectionStrings', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'7A55FCEC-6437-E2AB-5AA8-39FE7D867838', null, N'BookStore.Authors', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'CD099B89-29D2-2087-D011-39FE7D867839', null, N'BookStore.Authors.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'A1D9BBA8-18F5-E0BF-2D7E-39FE7D86783A', null, N'BookStore.Authors.Edit', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'EAD1642C-6839-C79C-24C1-39FE7D86783C', null, N'BookStore.Authors.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'BA1F8CEF-0376-2023-7EEA-39FE7D86783D', null, N'Basic.Products', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'56A277D1-996E-4575-F28E-39FE7D86783E', null, N'Basic.Products.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'CDB50212-3F6F-64ED-8C76-39FE7D867840', null, N'Basic.Products.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'BA574428-7406-F9E6-DC5C-39FE7D867841', null, N'Basic.Products.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'E677CEFA-B097-733D-FBB1-39FE7D867842', null, N'Basic.ProductAttributes', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'1B74E167-48E3-81C7-ACC9-39FE7D867843', null, N'Basic.ProductAttributes.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'34D1BF12-F003-EAEE-A7FB-39FE7D867844', null, N'Basic.ProductAttributes.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'B9CD197F-74A8-17F8-DD62-39FE7D867845', null, N'Basic.ProductAttributes.Delete', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'78368120-A144-3556-15B0-39FE7D867846', null, N'AbpAuditLogging.Default', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'C96ABFBE-18C4-F1F9-73FC-39FE7D867847', null, N'AbpAuditLogging.Default.Create', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'35A349B7-E1FB-4F50-B51A-39FE7D867848', null, N'AbpAuditLogging.Default.Update', N'R', N'admin')
GO
GO
INSERT INTO [dbo].[AbpPermissionGrants] ([Id], [TenantId], [Name], [ProviderName], [ProviderKey]) VALUES (N'34E8CABA-FC6F-1A18-D665-39FE7D867849', null, N'AbpAuditLogging.Default.Delete', N'R', N'admin')
GO
GO

-- ----------------------------
-- Table structure for AbpRoleClaims
-- ----------------------------
DROP TABLE [dbo].[AbpRoleClaims]
GO
CREATE TABLE [dbo].[AbpRoleClaims] (
[Id] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[ClaimType] nvarchar(256) NOT NULL ,
[ClaimValue] nvarchar(1024) NULL ,
[RoleId] uniqueidentifier NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpRoleClaims
-- ----------------------------

-- ----------------------------
-- Table structure for AbpRoles
-- ----------------------------
DROP TABLE [dbo].[AbpRoles]
GO
CREATE TABLE [dbo].[AbpRoles] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[TenantId] uniqueidentifier NULL ,
[Name] nvarchar(256) NOT NULL ,
[NormalizedName] nvarchar(256) NOT NULL ,
[IsDefault] bit NOT NULL ,
[IsStatic] bit NOT NULL ,
[IsPublic] bit NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpRoles
-- ----------------------------
INSERT INTO [dbo].[AbpRoles] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [Name], [NormalizedName], [IsDefault], [IsStatic], [IsPublic]) VALUES (N'4FEF5E89-ECF8-B91A-C257-39FE7D8677CE', N'{}', N'a6e86805-1b52-4537-ad12-602fc75550f6', null, N'admin', N'ADMIN', N'0', N'1', N'1')
GO
GO

-- ----------------------------
-- Table structure for AbpSecurityLogs
-- ----------------------------
DROP TABLE [dbo].[AbpSecurityLogs]
GO
CREATE TABLE [dbo].[AbpSecurityLogs] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[TenantId] uniqueidentifier NULL ,
[ApplicationName] nvarchar(96) NULL ,
[Identity] nvarchar(96) NULL ,
[Action] nvarchar(96) NULL ,
[UserId] uniqueidentifier NULL ,
[UserName] nvarchar(256) NULL ,
[TenantName] nvarchar(64) NULL ,
[ClientId] nvarchar(64) NULL ,
[CorrelationId] nvarchar(64) NULL ,
[ClientIpAddress] nvarchar(64) NULL ,
[BrowserInfo] nvarchar(512) NULL ,
[CreationTime] datetime2(7) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpSecurityLogs
-- ----------------------------
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'EC1F2028-2E6C-1922-BF1C-39FE7D7FA19C', N'{}', N'457a4d0f52064ebc80940958f91ae3be', null, null, N'Identity', N'LoginFailed', null, N'admin', null, null, N'f495e03efe04496f87a9d77d21fef70f', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 08:46:32.8586605')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'58CCA0EA-9331-AE9B-A8E7-39FE7D801AF9', N'{}', N'aa5f60c70be94df7a47b6047ca8fc428', null, null, N'Identity', N'LoginFailed', null, N'admin', null, null, N'd1538115ddca45fd919474dcf532dd78', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 08:47:03.9289358')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'B166F078-7749-BFF9-370B-39FE7D871155', N'{}', N'c76385d3d2c84ebe81aa8faa40091fac', null, null, N'Identity', N'LoginSucceeded', N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, null, N'830dff95d3404a79a38438c87f9ddabe', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 08:54:40.2127627')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'B1BC3BD3-481C-D2B5-F511-39FE7F03070E', N'{}', N'afcc6d8ccc82405b8cb616749c8d9c9f', null, null, N'IdentityServer', N'LoginSucceeded', N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, N'Tiger_App', N'fdf2942817c443e9b800bf5d488bad6c', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 15:49:41.2606575')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'B731C71A-AA53-D87F-8053-39FE804B7CA2', N'{}', N'7e392d66174b40b6b441f84120be8106', null, null, N'Identity', N'Logout', N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, N'Tiger_App', N'3d61fee6fa57473f8ce0c98a1413e047', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 21:48:27.1678809')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'189B0FF8-431C-1F9C-CB1A-39FE804B7D29', N'{}', N'ae75e1a916b74ba38065c1acb817e809', null, null, N'Identity', N'Logout', N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, N'Tiger_App', N'652d5659960445298d97cffa8df66167', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 21:48:27.3050464')
GO
GO
INSERT INTO [dbo].[AbpSecurityLogs] ([Id], [ExtraProperties], [ConcurrencyStamp], [TenantId], [ApplicationName], [Identity], [Action], [UserId], [UserName], [TenantName], [ClientId], [CorrelationId], [ClientIpAddress], [BrowserInfo], [CreationTime]) VALUES (N'D992EBB2-FF89-8D74-690F-39FE804B8626', N'{}', N'3debd11029a94ce9b697ad9c85d400a4', null, null, N'IdentityServer', N'LoginSucceeded', N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'admin', null, N'Tiger_App', N'b87c758828d14ed3a8fb8a7ec93df9f2', N'::1', N'Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/92.0.4515.159 Safari/537.36', N'2021-08-22 21:48:29.6061735')
GO
GO

-- ----------------------------
-- Table structure for AbpSettings
-- ----------------------------
DROP TABLE [dbo].[AbpSettings]
GO
CREATE TABLE [dbo].[AbpSettings] (
[Id] uniqueidentifier NOT NULL ,
[Name] nvarchar(128) NOT NULL ,
[Value] nvarchar(2048) NOT NULL ,
[ProviderName] nvarchar(64) NULL ,
[ProviderKey] nvarchar(64) NULL 
)


GO

-- ----------------------------
-- Records of AbpSettings
-- ----------------------------

-- ----------------------------
-- Table structure for AbpTenantConnectionStrings
-- ----------------------------
DROP TABLE [dbo].[AbpTenantConnectionStrings]
GO
CREATE TABLE [dbo].[AbpTenantConnectionStrings] (
[TenantId] uniqueidentifier NOT NULL ,
[Name] nvarchar(64) NOT NULL ,
[Value] nvarchar(1024) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpTenantConnectionStrings
-- ----------------------------

-- ----------------------------
-- Table structure for AbpTenants
-- ----------------------------
DROP TABLE [dbo].[AbpTenants]
GO
CREATE TABLE [dbo].[AbpTenants] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[Name] nvarchar(64) NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpTenants
-- ----------------------------

-- ----------------------------
-- Table structure for AbpUserClaims
-- ----------------------------
DROP TABLE [dbo].[AbpUserClaims]
GO
CREATE TABLE [dbo].[AbpUserClaims] (
[Id] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[ClaimType] nvarchar(256) NOT NULL ,
[ClaimValue] nvarchar(1024) NULL ,
[UserId] uniqueidentifier NOT NULL 
)


GO

-- ----------------------------
-- Records of AbpUserClaims
-- ----------------------------

-- ----------------------------
-- Table structure for AbpUserLogins
-- ----------------------------
DROP TABLE [dbo].[AbpUserLogins]
GO
CREATE TABLE [dbo].[AbpUserLogins] (
[UserId] uniqueidentifier NOT NULL ,
[LoginProvider] nvarchar(64) NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[ProviderKey] nvarchar(196) NOT NULL ,
[ProviderDisplayName] nvarchar(128) NULL 
)


GO

-- ----------------------------
-- Records of AbpUserLogins
-- ----------------------------

-- ----------------------------
-- Table structure for AbpUserOrganizationUnits
-- ----------------------------
DROP TABLE [dbo].[AbpUserOrganizationUnits]
GO
CREATE TABLE [dbo].[AbpUserOrganizationUnits] (
[UserId] uniqueidentifier NOT NULL ,
[OrganizationUnitId] uniqueidentifier NOT NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AbpUserOrganizationUnits
-- ----------------------------

-- ----------------------------
-- Table structure for AbpUserRoles
-- ----------------------------
DROP TABLE [dbo].[AbpUserRoles]
GO
CREATE TABLE [dbo].[AbpUserRoles] (
[UserId] uniqueidentifier NOT NULL ,
[RoleId] uniqueidentifier NOT NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AbpUserRoles
-- ----------------------------
INSERT INTO [dbo].[AbpUserRoles] ([UserId], [RoleId], [TenantId]) VALUES (N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'4FEF5E89-ECF8-B91A-C257-39FE7D8677CE', null)
GO
GO

-- ----------------------------
-- Table structure for AbpUsers
-- ----------------------------
DROP TABLE [dbo].[AbpUsers]
GO
CREATE TABLE [dbo].[AbpUsers] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[TenantId] uniqueidentifier NULL ,
[UserName] nvarchar(256) NOT NULL ,
[NormalizedUserName] nvarchar(256) NOT NULL ,
[Name] nvarchar(64) NULL ,
[Surname] nvarchar(64) NULL ,
[Email] nvarchar(256) NOT NULL ,
[NormalizedEmail] nvarchar(256) NOT NULL ,
[EmailConfirmed] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[PasswordHash] nvarchar(256) NULL ,
[SecurityStamp] nvarchar(256) NOT NULL ,
[IsExternal] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[PhoneNumber] nvarchar(16) NULL ,
[PhoneNumberConfirmed] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[TwoFactorEnabled] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[LockoutEnd] datetimeoffset(7) NULL ,
[LockoutEnabled] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[AccessFailedCount] int NOT NULL DEFAULT ((0)) 
)


GO

-- ----------------------------
-- Records of AbpUsers
-- ----------------------------
INSERT INTO [dbo].[AbpUsers] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [TenantId], [UserName], [NormalizedUserName], [Name], [Surname], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [IsExternal], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7938E66E-5F62-2B4A-BF1E-39FE7D8677B6', N'{}', N'0c610379e09a4754877e3be42f3402f3', N'2021-08-22 08:54:00.9011330', null, N'2021-08-22 08:54:00.9518598', null, N'0', null, null, null, N'admin', N'ADMIN', N'admin', null, N'admin@abp.io', N'ADMIN@ABP.IO', N'0', N'AQAAAAEAACcQAAAAEO86Cn80jTm171T8/F1/M+XftrO7w1Na6w1R9DjhyqqCJBvx3Cp3KMSj0dYkn6my4Q==', N'N4DIWBARQM4U3U7EZE4IOTMOLNB4LK7J', N'0', null, N'0', N'0', null, N'1', N'0')
GO
GO

-- ----------------------------
-- Table structure for AbpUserTokens
-- ----------------------------
DROP TABLE [dbo].[AbpUserTokens]
GO
CREATE TABLE [dbo].[AbpUserTokens] (
[UserId] uniqueidentifier NOT NULL ,
[LoginProvider] nvarchar(64) NOT NULL ,
[Name] nvarchar(128) NOT NULL ,
[TenantId] uniqueidentifier NULL ,
[Value] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AbpUserTokens
-- ----------------------------

-- ----------------------------
-- Table structure for AppAuthors
-- ----------------------------
DROP TABLE [dbo].[AppAuthors]
GO
CREATE TABLE [dbo].[AppAuthors] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[Name] nvarchar(64) NOT NULL ,
[BirthDate] datetime2(7) NOT NULL ,
[ShortBio] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AppAuthors
-- ----------------------------
INSERT INTO [dbo].[AppAuthors] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [BirthDate], [ShortBio]) VALUES (N'DE4C0F69-3A41-D6AD-04A8-39FE7D86802B', N'{}', N'3719193936184cdcb079d46eafa3adee', N'2021-08-22 08:54:03.1270111', null, null, null, N'0', null, null, N'George Orwell', N'1903-06-25 00:00:00.0000000', N'Orwell produced literary criticism and poetry, fiction and polemical journalism; and is best known for the allegorical novella Animal Farm (1945) and the dystopian novel Nineteen Eighty-Four (1949).')
GO
GO
INSERT INTO [dbo].[AppAuthors] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [BirthDate], [ShortBio]) VALUES (N'E676360A-237C-4064-41A4-39FE7D868044', N'{}', N'f7a29a15ea84476f89630469b406601c', N'2021-08-22 08:54:03.1270921', null, null, null, N'0', null, null, N'Douglas Adams', N'1952-03-11 00:00:00.0000000', N'Douglas Adams was an English author, screenwriter, essayist, humorist, satirist and dramatist. Adams was an advocate for environmentalism and conservation, a lover of fast cars, technological innovation and the Apple Macintosh, and a self-proclaimed ''radical atheist''.')
GO
GO

-- ----------------------------
-- Table structure for AppBooks
-- ----------------------------
DROP TABLE [dbo].[AppBooks]
GO
CREATE TABLE [dbo].[AppBooks] (
[Id] uniqueidentifier NOT NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[Name] nvarchar(128) NOT NULL ,
[Type] int NOT NULL ,
[PublishDate] datetime2(7) NOT NULL ,
[Price] real NOT NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[TenantId] uniqueidentifier NULL ,
[AuthorId] uniqueidentifier NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000') ,
[AuthorId1] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AppBooks
-- ----------------------------
INSERT INTO [dbo].[AppBooks] ([Id], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [Name], [Type], [PublishDate], [Price], [IsDeleted], [TenantId], [AuthorId], [AuthorId1]) VALUES (N'539E2621-A290-494A-A84E-0A965B80A6D0', N'2021-08-22 08:54:03.1791344', null, null, null, N'The Hitchhiker''s Guide to the Galaxy', N'7', N'1995-09-27 00:00:00.0000000', N'42', N'0', null, N'E676360A-237C-4064-41A4-39FE7D868044', null)
GO
GO
INSERT INTO [dbo].[AppBooks] ([Id], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [Name], [Type], [PublishDate], [Price], [IsDeleted], [TenantId], [AuthorId], [AuthorId1]) VALUES (N'7181A4A6-F79E-4210-BF34-97DBCC48A0FF', N'2021-08-22 08:54:03.1271706', null, null, null, N'1984', N'3', N'1949-06-08 00:00:00.0000000', N'19.84', N'0', null, N'DE4C0F69-3A41-D6AD-04A8-39FE7D86802B', null)
GO
GO

-- ----------------------------
-- Table structure for AppCategorys
-- ----------------------------
DROP TABLE [dbo].[AppCategorys]
GO
CREATE TABLE [dbo].[AppCategorys] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[ParentId] uniqueidentifier NOT NULL ,
[Name] nvarchar(MAX) NOT NULL ,
[Level] int NOT NULL ,
[ProductCount] int NOT NULL ,
[ShowStatus] int NOT NULL ,
[Sort] int NOT NULL ,
[Icon] nvarchar(MAX) NULL ,
[Keyword] nvarchar(MAX) NULL DEFAULT (N'False') ,
[Description] nvarchar(MAX) NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AppCategorys
-- ----------------------------

-- ----------------------------
-- Table structure for AppCommentReplys
-- ----------------------------
DROP TABLE [dbo].[AppCommentReplys]
GO
CREATE TABLE [dbo].[AppCommentReplys] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[CommentId] uniqueidentifier NOT NULL ,
[MemberNickName] nvarchar(MAX) NULL ,
[MemberIcon] nvarchar(MAX) NULL ,
[Content] nvarchar(MAX) NULL ,
[Type] int NOT NULL 
)


GO

-- ----------------------------
-- Records of AppCommentReplys
-- ----------------------------

-- ----------------------------
-- Table structure for AppComments
-- ----------------------------
DROP TABLE [dbo].[AppComments]
GO
CREATE TABLE [dbo].[AppComments] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[ProductId] uniqueidentifier NOT NULL ,
[OrderId] uniqueidentifier NOT NULL ,
[Pics] nvarchar(MAX) NULL ,
[Star] int NOT NULL ,
[ShowStatus] int NOT NULL ,
[MemberNickName] nvarchar(MAX) NULL ,
[MemberIP] nvarchar(MAX) NULL ,
[MemberIcon] nvarchar(MAX) NULL ,
[Content] nvarchar(MAX) NULL ,
[ProductAttribute] nvarchar(MAX) NULL ,
[CollectCount] int NOT NULL ,
[ReadCount] int NOT NULL ,
[ReplayCount] int NOT NULL 
)


GO

-- ----------------------------
-- Records of AppComments
-- ----------------------------

-- ----------------------------
-- Table structure for AppOrderItems
-- ----------------------------
DROP TABLE [dbo].[AppOrderItems]
GO
CREATE TABLE [dbo].[AppOrderItems] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[OrderId] int NOT NULL ,
[OrderId1] uniqueidentifier NULL ,
[OrderSn] nvarchar(MAX) NULL ,
[ProductId] int NOT NULL ,
[ProductId1] uniqueidentifier NULL ,
[ProductPic] nvarchar(MAX) NULL ,
[ProductName] nvarchar(MAX) NULL ,
[ProductBrand] nvarchar(MAX) NULL ,
[ProductSn] nvarchar(MAX) NULL ,
[ProductPrice] decimal(18,2) NOT NULL ,
[ProductQuantity] int NOT NULL ,
[ProductSkuId] int NOT NULL ,
[ProductSkuCode] nvarchar(MAX) NULL ,
[ProductCategoryId] int NOT NULL ,
[PromotionName] nvarchar(MAX) NULL ,
[PromotionAmount] decimal(18,2) NOT NULL ,
[CouponAmount] decimal(18,2) NOT NULL ,
[IntegrationAmount] decimal(18,2) NOT NULL ,
[RealAmount] decimal(18,2) NOT NULL ,
[GiftIntegration] decimal(18,2) NOT NULL ,
[GiftGrowth] decimal(18,2) NOT NULL ,
[ProductAttr] nvarchar(MAX) NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AppOrderItems
-- ----------------------------

-- ----------------------------
-- Table structure for AppOrderOperateHistorys
-- ----------------------------
DROP TABLE [dbo].[AppOrderOperateHistorys]
GO
CREATE TABLE [dbo].[AppOrderOperateHistorys] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[OrderId] int NOT NULL ,
[OrderId1] uniqueidentifier NULL ,
[OperateMan] nvarchar(MAX) NULL ,
[OrderStatus] int NOT NULL ,
[Note] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AppOrderOperateHistorys
-- ----------------------------

-- ----------------------------
-- Table structure for AppOrders
-- ----------------------------
DROP TABLE [dbo].[AppOrders]
GO
CREATE TABLE [dbo].[AppOrders] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[MemberId] int NOT NULL ,
[CouponId] int NOT NULL ,
[OrderSn] nvarchar(MAX) NULL ,
[TotalAmount] decimal(18,2) NOT NULL ,
[PayAmount] decimal(18,2) NOT NULL ,
[FreightAmount] decimal(18,2) NOT NULL ,
[PromotionAmount] decimal(18,2) NOT NULL ,
[IntegrationAmount] decimal(18,2) NOT NULL ,
[CouponAmount] decimal(18,2) NOT NULL ,
[DiscountAmount] decimal(18,2) NOT NULL ,
[PayType] int NOT NULL ,
[SourceType] int NOT NULL ,
[Status] int NOT NULL ,
[OrderType] int NOT NULL ,
[DeliveryCompany] nvarchar(MAX) NULL ,
[DeliverySn] nvarchar(MAX) NULL ,
[AutoConfirmDay] int NOT NULL ,
[Integration] int NOT NULL ,
[Growth] int NOT NULL ,
[PromotionInfo] int NOT NULL ,
[BillType] int NOT NULL ,
[BillHeader] nvarchar(MAX) NULL ,
[BillContent] nvarchar(MAX) NULL ,
[BillReceiverPhone] nvarchar(MAX) NULL ,
[BillReceiverEmail] nvarchar(MAX) NULL ,
[ReceiverName] nvarchar(MAX) NULL ,
[ReceiverPhone] nvarchar(MAX) NULL ,
[ReceiverPostCode] nvarchar(MAX) NULL ,
[ReceiverProvince] nvarchar(MAX) NULL ,
[ReceiverCity] nvarchar(MAX) NULL ,
[ReceiverRegion] nvarchar(MAX) NULL ,
[ReceiverDetailAddress] nvarchar(MAX) NULL ,
[Note] nvarchar(MAX) NULL ,
[ConfirmStatus] int NOT NULL ,
[UseIntegration] int NOT NULL ,
[PaymentTime] datetime2(7) NOT NULL ,
[DeliveryTime] datetime2(7) NOT NULL ,
[ReceiveTime] datetime2(7) NOT NULL ,
[CommentTime] datetime2(7) NOT NULL ,
[TenantId] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AppOrders
-- ----------------------------

-- ----------------------------
-- Table structure for AppOrderSettings
-- ----------------------------
DROP TABLE [dbo].[AppOrderSettings]
GO
CREATE TABLE [dbo].[AppOrderSettings] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[FlashOrderOverTime] int NOT NULL ,
[NormalOrderOverTime] int NOT NULL ,
[ComfirmOvertime] int NOT NULL ,
[CommentOvertime] int NOT NULL 
)


GO

-- ----------------------------
-- Records of AppOrderSettings
-- ----------------------------

-- ----------------------------
-- Table structure for AppProductAttributes
-- ----------------------------
DROP TABLE [dbo].[AppProductAttributes]
GO
CREATE TABLE [dbo].[AppProductAttributes] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[Name] varchar(200) NOT NULL ,
[SelectType] int NOT NULL ,
[InputType] int NOT NULL ,
[InputList] nvarchar(MAX) NULL ,
[Sort] int NOT NULL ,
[FilterType] int NOT NULL ,
[SearchType] int NOT NULL ,
[HandAddStatus] int NOT NULL ,
[Type] int NOT NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[TenantId] uniqueidentifier NULL ,
[ProductAttributeTypeId] uniqueidentifier NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000') 
)


GO

-- ----------------------------
-- Records of AppProductAttributes
-- ----------------------------

-- ----------------------------
-- Table structure for AppProductAttributeTypes
-- ----------------------------
DROP TABLE [dbo].[AppProductAttributeTypes]
GO
CREATE TABLE [dbo].[AppProductAttributeTypes] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[TenantId] uniqueidentifier NULL ,
[Name] nvarchar(450) NOT NULL ,
[AttributeCount] int NOT NULL ,
[ParamCount] int NOT NULL 
)


GO

-- ----------------------------
-- Records of AppProductAttributeTypes
-- ----------------------------

-- ----------------------------
-- Table structure for AppProductAttributeValues
-- ----------------------------
DROP TABLE [dbo].[AppProductAttributeValues]
GO
CREATE TABLE [dbo].[AppProductAttributeValues] (
[Id] uniqueidentifier NOT NULL ,
[ProductId] uniqueidentifier NOT NULL ,
[ProductAttributeId] uniqueidentifier NOT NULL ,
[Value] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of AppProductAttributeValues
-- ----------------------------

-- ----------------------------
-- Table structure for AppProducts
-- ----------------------------
DROP TABLE [dbo].[AppProducts]
GO
CREATE TABLE [dbo].[AppProducts] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[Name] nvarchar(450) NOT NULL ,
[CreationTime] datetime2(7) NOT NULL DEFAULT ('0001-01-01T00:00:00.0000000') ,
[CreatorId] uniqueidentifier NULL ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[DetailDesc] nvarchar(MAX) NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[Picture] nvarchar(MAX) NULL ,
[Price] decimal(18,2) NOT NULL DEFAULT ((0.0)) ,
[ProductSn] nvarchar(MAX) NULL ,
[ProductTagId] uniqueidentifier NULL ,
[PublishStatus] int NOT NULL DEFAULT ((0)) ,
[Sort] int NOT NULL DEFAULT ((0)) ,
[SubTitle] nvarchar(MAX) NULL ,
[Unit] nvarchar(MAX) NULL ,
[OrderId] uniqueidentifier NULL ,
[TenantId] uniqueidentifier NULL ,
[AlbumPics] nvarchar(MAX) NULL ,
[CategoryName] nvarchar(MAX) NULL ,
[DetailTitle] nvarchar(MAX) NULL ,
[GiftGrowth] int NOT NULL DEFAULT ((0)) ,
[GiftIntegration] int NOT NULL DEFAULT ((0)) ,
[Keywords] nvarchar(MAX) NULL ,
[LowStock] int NOT NULL DEFAULT ((0)) ,
[NewStatus] int NOT NULL DEFAULT ((0)) ,
[Note] nvarchar(MAX) NULL ,
[OriPrice] decimal(18,2) NOT NULL DEFAULT ((0.0)) ,
[PreviewStatus] int NOT NULL DEFAULT ((0)) ,
[ProductAttributeTypeId] uniqueidentifier NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000') ,
[ProductCategoryId] uniqueidentifier NOT NULL DEFAULT ('00000000-0000-0000-0000-000000000000') ,
[PromotionEndTime] datetime2(7) NOT NULL DEFAULT ('0001-01-01T00:00:00.0000000') ,
[PromotionPerLimit] int NOT NULL DEFAULT ((0)) ,
[PromotionPrice] decimal(18,2) NOT NULL DEFAULT ((0.0)) ,
[PromotionStartTime] datetime2(7) NOT NULL DEFAULT ('0001-01-01T00:00:00.0000000') ,
[PromotionType] int NOT NULL DEFAULT ((0)) ,
[PurchasePrice] decimal(18,2) NOT NULL DEFAULT ((0.0)) ,
[RecommandStatus] int NOT NULL DEFAULT ((0)) ,
[Sale] int NOT NULL DEFAULT ((0)) ,
[Stock] int NOT NULL DEFAULT ((0)) ,
[VerifyStatus] int NOT NULL DEFAULT ((0)) ,
[Weight] decimal(18,2) NOT NULL DEFAULT ((0.0)) 
)


GO

-- ----------------------------
-- Records of AppProducts
-- ----------------------------

-- ----------------------------
-- Table structure for AppProductTagRelation
-- ----------------------------
DROP TABLE [dbo].[AppProductTagRelation]
GO
CREATE TABLE [dbo].[AppProductTagRelation] (
[Id] uniqueidentifier NOT NULL ,
[ProductId] int NOT NULL ,
[ProductId1] uniqueidentifier NULL ,
[ProductTagId] int NOT NULL ,
[ProductTagId1] uniqueidentifier NULL 
)


GO

-- ----------------------------
-- Records of AppProductTagRelation
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerApiClaims
-- ----------------------------
DROP TABLE [dbo].[IdentityServerApiClaims]
GO
CREATE TABLE [dbo].[IdentityServerApiClaims] (
[Type] nvarchar(200) NOT NULL ,
[ApiResourceId] uniqueidentifier NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerApiClaims
-- ----------------------------
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'email', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'email_verified', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'name', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'phone_number', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'phone_number_verified', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO
INSERT INTO [dbo].[IdentityServerApiClaims] ([Type], [ApiResourceId]) VALUES (N'role', N'4583A5E4-8DB0-08A1-6239-39FE7D867D74')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerApiResources
-- ----------------------------
DROP TABLE [dbo].[IdentityServerApiResources]
GO
CREATE TABLE [dbo].[IdentityServerApiResources] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[Name] nvarchar(200) NOT NULL ,
[DisplayName] nvarchar(200) NULL ,
[Description] nvarchar(1000) NULL ,
[Enabled] bit NOT NULL ,
[Properties] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerApiResources
-- ----------------------------
INSERT INTO [dbo].[IdentityServerApiResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Properties]) VALUES (N'4583A5E4-8DB0-08A1-6239-39FE7D867D74', N'{}', N'2c906767c5754c28806215d481707b37', N'2021-08-22 08:54:02.3863841', null, N'2021-08-22 08:54:29.0083644', null, N'0', null, null, N'Tiger', N'Tiger API', null, N'1', N'{}')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerApiScopeClaims
-- ----------------------------
DROP TABLE [dbo].[IdentityServerApiScopeClaims]
GO
CREATE TABLE [dbo].[IdentityServerApiScopeClaims] (
[Type] nvarchar(200) NOT NULL ,
[ApiResourceId] uniqueidentifier NOT NULL ,
[Name] nvarchar(200) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerApiScopeClaims
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerApiScopes
-- ----------------------------
DROP TABLE [dbo].[IdentityServerApiScopes]
GO
CREATE TABLE [dbo].[IdentityServerApiScopes] (
[ApiResourceId] uniqueidentifier NOT NULL ,
[Name] nvarchar(200) NOT NULL ,
[DisplayName] nvarchar(200) NULL ,
[Description] nvarchar(1000) NULL ,
[Required] bit NOT NULL ,
[Emphasize] bit NOT NULL ,
[ShowInDiscoveryDocument] bit NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerApiScopes
-- ----------------------------
INSERT INTO [dbo].[IdentityServerApiScopes] ([ApiResourceId], [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument]) VALUES (N'4583A5E4-8DB0-08A1-6239-39FE7D867D74', N'Tiger', N'Tiger API', null, N'0', N'0', N'1')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerApiSecrets
-- ----------------------------
DROP TABLE [dbo].[IdentityServerApiSecrets]
GO
CREATE TABLE [dbo].[IdentityServerApiSecrets] (
[Type] nvarchar(250) NOT NULL ,
[Value] nvarchar(4000) NOT NULL ,
[ApiResourceId] uniqueidentifier NOT NULL ,
[Description] nvarchar(2000) NULL ,
[Expiration] datetime2(7) NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerApiSecrets
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerClientClaims
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientClaims]
GO
CREATE TABLE [dbo].[IdentityServerClientClaims] (
[ClientId] uniqueidentifier NOT NULL ,
[Type] nvarchar(250) NOT NULL ,
[Value] nvarchar(250) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientClaims
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerClientCorsOrigins
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientCorsOrigins]
GO
CREATE TABLE [dbo].[IdentityServerClientCorsOrigins] (
[ClientId] uniqueidentifier NOT NULL ,
[Origin] nvarchar(150) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientCorsOrigins
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerClientGrantTypes
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientGrantTypes]
GO
CREATE TABLE [dbo].[IdentityServerClientGrantTypes] (
[ClientId] uniqueidentifier NOT NULL ,
[GrantType] nvarchar(250) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientGrantTypes
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'hybrid')
GO
GO
INSERT INTO [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'authorization_code')
GO
GO
INSERT INTO [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'client_credentials')
GO
GO
INSERT INTO [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'password')
GO
GO
INSERT INTO [dbo].[IdentityServerClientGrantTypes] ([ClientId], [GrantType]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'authorization_code')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerClientIdPRestrictions
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientIdPRestrictions]
GO
CREATE TABLE [dbo].[IdentityServerClientIdPRestrictions] (
[ClientId] uniqueidentifier NOT NULL ,
[Provider] nvarchar(200) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientIdPRestrictions
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris]
GO
CREATE TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] (
[ClientId] uniqueidentifier NOT NULL ,
[PostLogoutRedirectUri] nvarchar(2000) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'https://localhost:44358/signout-callback-oidc')
GO
GO
INSERT INTO [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'http://localhost:9528')
GO
GO
INSERT INTO [dbo].[IdentityServerClientPostLogoutRedirectUris] ([ClientId], [PostLogoutRedirectUri]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'https://localhost:44307/authentication/logout-callback')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerClientProperties
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientProperties]
GO
CREATE TABLE [dbo].[IdentityServerClientProperties] (
[ClientId] uniqueidentifier NOT NULL ,
[Key] nvarchar(250) NOT NULL ,
[Value] nvarchar(2000) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientProperties
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerClientRedirectUris
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientRedirectUris]
GO
CREATE TABLE [dbo].[IdentityServerClientRedirectUris] (
[ClientId] uniqueidentifier NOT NULL ,
[RedirectUri] nvarchar(2000) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientRedirectUris
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'https://localhost:44358/signin-oidc')
GO
GO
INSERT INTO [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'http://localhost:9528')
GO
GO
INSERT INTO [dbo].[IdentityServerClientRedirectUris] ([ClientId], [RedirectUri]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'https://localhost:44307/authentication/login-callback')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerClients
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClients]
GO
CREATE TABLE [dbo].[IdentityServerClients] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[ClientId] nvarchar(200) NOT NULL ,
[ClientName] nvarchar(200) NULL ,
[Description] nvarchar(1000) NULL ,
[ClientUri] nvarchar(2000) NULL ,
[LogoUri] nvarchar(2000) NULL ,
[Enabled] bit NOT NULL ,
[ProtocolType] nvarchar(200) NOT NULL ,
[RequireClientSecret] bit NOT NULL ,
[RequireConsent] bit NOT NULL ,
[AllowRememberConsent] bit NOT NULL ,
[AlwaysIncludeUserClaimsInIdToken] bit NOT NULL ,
[RequirePkce] bit NOT NULL ,
[AllowPlainTextPkce] bit NOT NULL ,
[AllowAccessTokensViaBrowser] bit NOT NULL ,
[FrontChannelLogoutUri] nvarchar(2000) NULL ,
[FrontChannelLogoutSessionRequired] bit NOT NULL ,
[BackChannelLogoutUri] nvarchar(2000) NULL ,
[BackChannelLogoutSessionRequired] bit NOT NULL ,
[AllowOfflineAccess] bit NOT NULL ,
[IdentityTokenLifetime] int NOT NULL ,
[AccessTokenLifetime] int NOT NULL ,
[AuthorizationCodeLifetime] int NOT NULL ,
[ConsentLifetime] int NULL ,
[AbsoluteRefreshTokenLifetime] int NOT NULL ,
[SlidingRefreshTokenLifetime] int NOT NULL ,
[RefreshTokenUsage] int NOT NULL ,
[UpdateAccessTokenClaimsOnRefresh] bit NOT NULL ,
[RefreshTokenExpiration] int NOT NULL ,
[AccessTokenType] int NOT NULL ,
[EnableLocalLogin] bit NOT NULL ,
[IncludeJwtId] bit NOT NULL ,
[AlwaysSendClientClaims] bit NOT NULL ,
[ClientClaimsPrefix] nvarchar(200) NULL ,
[PairWiseSubjectSalt] nvarchar(200) NULL ,
[UserSsoLifetime] int NULL ,
[UserCodeType] nvarchar(100) NULL ,
[DeviceCodeLifetime] int NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClients
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'{}', N'623973fda6d54e76b663d43a5b11de02', N'2021-08-22 08:54:02.6471035', null, N'2021-08-22 08:54:29.0094871', null, N'0', null, null, N'Tiger_Web', N'Tiger_Web', N'Tiger_Web', null, null, N'1', N'oidc', N'1', N'0', N'1', N'1', N'0', N'0', N'0', N'https://localhost:44358/Account/FrontChannelLogout', N'1', null, N'1', N'1', N'300', N'31536000', N'300', null, N'31536000', N'1296000', N'1', N'0', N'1', N'0', N'1', N'0', N'0', N'client_', null, null, null, N'300')
GO
GO
INSERT INTO [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'{}', N'f167c5abe1694e3dbe9bb3a8cc0886bf', N'2021-08-22 08:54:02.6931794', null, N'2021-08-22 08:54:29.0096006', null, N'0', null, null, N'Tiger_App', N'Tiger_App', N'Tiger_App', null, null, N'1', N'oidc', N'0', N'0', N'1', N'1', N'0', N'0', N'0', null, N'1', null, N'1', N'1', N'300', N'31536000', N'300', null, N'31536000', N'1296000', N'1', N'0', N'1', N'0', N'1', N'0', N'0', N'client_', null, null, null, N'300')
GO
GO
INSERT INTO [dbo].[IdentityServerClients] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [ClientId], [ClientName], [Description], [ClientUri], [LogoUri], [Enabled], [ProtocolType], [RequireClientSecret], [RequireConsent], [AllowRememberConsent], [AlwaysIncludeUserClaimsInIdToken], [RequirePkce], [AllowPlainTextPkce], [AllowAccessTokensViaBrowser], [FrontChannelLogoutUri], [FrontChannelLogoutSessionRequired], [BackChannelLogoutUri], [BackChannelLogoutSessionRequired], [AllowOfflineAccess], [IdentityTokenLifetime], [AccessTokenLifetime], [AuthorizationCodeLifetime], [ConsentLifetime], [AbsoluteRefreshTokenLifetime], [SlidingRefreshTokenLifetime], [RefreshTokenUsage], [UpdateAccessTokenClaimsOnRefresh], [RefreshTokenExpiration], [AccessTokenType], [EnableLocalLogin], [IncludeJwtId], [AlwaysSendClientClaims], [ClientClaimsPrefix], [PairWiseSubjectSalt], [UserSsoLifetime], [UserCodeType], [DeviceCodeLifetime]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'{}', N'0aeb2c885c42453db6dd1e0041506ea6', N'2021-08-22 08:54:02.7332591', null, N'2021-08-22 08:54:29.0096977', null, N'0', null, null, N'Tiger_Blazor', N'Tiger_Blazor', N'Tiger_Blazor', null, null, N'1', N'oidc', N'0', N'0', N'1', N'1', N'0', N'0', N'0', null, N'1', null, N'1', N'1', N'300', N'31536000', N'300', null, N'31536000', N'1296000', N'1', N'0', N'1', N'0', N'1', N'0', N'0', N'client_', null, null, null, N'300')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerClientScopes
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientScopes]
GO
CREATE TABLE [dbo].[IdentityServerClientScopes] (
[ClientId] uniqueidentifier NOT NULL ,
[Scope] nvarchar(200) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientScopes
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'address')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'email')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'openid')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'phone')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'profile')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'role')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'3CC48834-3317-4CF5-6560-39FE7D867E6B', N'Tiger')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'address')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'email')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'openid')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'phone')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'profile')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'role')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', N'Tiger')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'address')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'email')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'openid')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'phone')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'profile')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'role')
GO
GO
INSERT INTO [dbo].[IdentityServerClientScopes] ([ClientId], [Scope]) VALUES (N'5C5E9EBE-EA53-F002-01FB-39FE7D867EEB', N'Tiger')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerClientSecrets
-- ----------------------------
DROP TABLE [dbo].[IdentityServerClientSecrets]
GO
CREATE TABLE [dbo].[IdentityServerClientSecrets] (
[Type] nvarchar(250) NOT NULL ,
[Value] nvarchar(4000) NOT NULL ,
[ClientId] uniqueidentifier NOT NULL ,
[Description] nvarchar(2000) NULL ,
[Expiration] datetime2(7) NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerClientSecrets
-- ----------------------------
INSERT INTO [dbo].[IdentityServerClientSecrets] ([Type], [Value], [ClientId], [Description], [Expiration]) VALUES (N'SharedSecret', N'E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=', N'3CC48834-3317-4CF5-6560-39FE7D867E6B', null, null)
GO
GO
INSERT INTO [dbo].[IdentityServerClientSecrets] ([Type], [Value], [ClientId], [Description], [Expiration]) VALUES (N'SharedSecret', N'E5Xd4yMqjP5kjWFKrYgySBju6JVfCzMyFp7n2QmMrME=', N'D28B7387-0E89-A3AC-20F0-39FE7D867EB8', null, null)
GO
GO

-- ----------------------------
-- Table structure for IdentityServerDeviceFlowCodes
-- ----------------------------
DROP TABLE [dbo].[IdentityServerDeviceFlowCodes]
GO
CREATE TABLE [dbo].[IdentityServerDeviceFlowCodes] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[DeviceCode] nvarchar(200) NOT NULL ,
[UserCode] nvarchar(200) NOT NULL ,
[SubjectId] nvarchar(200) NULL ,
[ClientId] nvarchar(200) NOT NULL ,
[Expiration] datetime2(7) NOT NULL ,
[Data] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerDeviceFlowCodes
-- ----------------------------

-- ----------------------------
-- Table structure for IdentityServerIdentityClaims
-- ----------------------------
DROP TABLE [dbo].[IdentityServerIdentityClaims]
GO
CREATE TABLE [dbo].[IdentityServerIdentityClaims] (
[Type] nvarchar(200) NOT NULL ,
[IdentityResourceId] uniqueidentifier NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerIdentityClaims
-- ----------------------------
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'sub', N'D5755458-FCCE-DFEB-8615-39FE7D867D04')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'birthdate', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'family_name', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'gender', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'given_name', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'locale', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'middle_name', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'name', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'nickname', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'picture', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'preferred_username', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'profile', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'updated_at', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'website', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'zoneinfo', N'102A02F4-98E5-E28D-5989-39FE7D867D2F')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'email', N'8412D1BF-4AAF-E31A-C2B0-39FE7D867D39')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'email_verified', N'8412D1BF-4AAF-E31A-C2B0-39FE7D867D39')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'address', N'B7B73D28-42E3-F853-806F-39FE7D867D3C')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'phone_number', N'E41BB243-9B37-08BD-09B1-39FE7D867D41')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'phone_number_verified', N'E41BB243-9B37-08BD-09B1-39FE7D867D41')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityClaims] ([Type], [IdentityResourceId]) VALUES (N'role', N'5E434C09-B868-04A5-A580-39FE7D867D43')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerIdentityResources
-- ----------------------------
DROP TABLE [dbo].[IdentityServerIdentityResources]
GO
CREATE TABLE [dbo].[IdentityServerIdentityResources] (
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[CreatorId] uniqueidentifier NULL ,
[LastModificationTime] datetime2(7) NULL ,
[LastModifierId] uniqueidentifier NULL ,
[IsDeleted] bit NOT NULL DEFAULT (CONVERT([bit],(0))) ,
[DeleterId] uniqueidentifier NULL ,
[DeletionTime] datetime2(7) NULL ,
[Name] nvarchar(200) NOT NULL ,
[DisplayName] nvarchar(200) NULL ,
[Description] nvarchar(1000) NULL ,
[Enabled] bit NOT NULL ,
[Required] bit NOT NULL ,
[Emphasize] bit NOT NULL ,
[ShowInDiscoveryDocument] bit NOT NULL ,
[Properties] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerIdentityResources
-- ----------------------------
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'D5755458-FCCE-DFEB-8615-39FE7D867D04', N'{}', N'c9b721d901f546e8ace345a17dd15565', N'2021-08-22 08:54:02.3862206', null, null, null, N'0', null, null, N'openid', N'Your user identifier', null, N'1', N'1', N'0', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'102A02F4-98E5-E28D-5989-39FE7D867D2F', N'{}', N'1d00da9259634c4c8efdad3f87b77d30', N'2021-08-22 08:54:02.3863297', null, null, null, N'0', null, null, N'profile', N'User profile', N'Your user profile information (first name, last name, etc.)', N'1', N'0', N'1', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'8412D1BF-4AAF-E31A-C2B0-39FE7D867D39', N'{}', N'62fe9dbaf713491cbe00d5bc2ccb616a', N'2021-08-22 08:54:02.3863594', null, null, null, N'0', null, null, N'email', N'Your email address', null, N'1', N'0', N'1', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'B7B73D28-42E3-F853-806F-39FE7D867D3C', N'{}', N'7fe3e1c4a8e0441eb24b37857d75c7a9', N'2021-08-22 08:54:02.3863619', null, null, null, N'0', null, null, N'address', N'Your postal address', null, N'1', N'0', N'1', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'E41BB243-9B37-08BD-09B1-39FE7D867D41', N'{}', N'8009839cc30142ac8dd7d7dca8a1ccb7', N'2021-08-22 08:54:02.3863636', null, null, null, N'0', null, null, N'phone', N'Your phone number', null, N'1', N'0', N'1', N'1', N'{}')
GO
GO
INSERT INTO [dbo].[IdentityServerIdentityResources] ([Id], [ExtraProperties], [ConcurrencyStamp], [CreationTime], [CreatorId], [LastModificationTime], [LastModifierId], [IsDeleted], [DeleterId], [DeletionTime], [Name], [DisplayName], [Description], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument], [Properties]) VALUES (N'5E434C09-B868-04A5-A580-39FE7D867D43', N'{}', N'928a11a0feef4c958fedd51900f874de', N'2021-08-22 08:54:02.3863655', null, null, null, N'0', null, null, N'role', N'Roles of the user', null, N'1', N'0', N'0', N'1', N'{}')
GO
GO

-- ----------------------------
-- Table structure for IdentityServerPersistedGrants
-- ----------------------------
DROP TABLE [dbo].[IdentityServerPersistedGrants]
GO
CREATE TABLE [dbo].[IdentityServerPersistedGrants] (
[Key] nvarchar(200) NOT NULL ,
[Id] uniqueidentifier NOT NULL ,
[ExtraProperties] nvarchar(MAX) NULL ,
[ConcurrencyStamp] nvarchar(40) NULL ,
[Type] nvarchar(50) NOT NULL ,
[SubjectId] nvarchar(200) NULL ,
[ClientId] nvarchar(200) NOT NULL ,
[CreationTime] datetime2(7) NOT NULL ,
[Expiration] datetime2(7) NULL ,
[Data] nvarchar(MAX) NOT NULL 
)


GO

-- ----------------------------
-- Records of IdentityServerPersistedGrants
-- ----------------------------

-- ----------------------------
-- Table structure for ProductTag
-- ----------------------------
DROP TABLE [dbo].[ProductTag]
GO
CREATE TABLE [dbo].[ProductTag] (
[Id] uniqueidentifier NOT NULL ,
[Name] nvarchar(MAX) NULL ,
[Color] nvarchar(MAX) NULL 
)


GO

-- ----------------------------
-- Records of ProductTag
-- ----------------------------

-- ----------------------------
-- Table structure for sysdiagrams
-- ----------------------------
DROP TABLE [dbo].[sysdiagrams]
GO
CREATE TABLE [dbo].[sysdiagrams] (
[name] sysname NOT NULL ,
[principal_id] int NOT NULL ,
[diagram_id] int NOT NULL IDENTITY(1,1) ,
[version] int NULL ,
[definition] varbinary(MAX) NULL 
)


GO

-- ----------------------------
-- Records of sysdiagrams
-- ----------------------------
SET IDENTITY_INSERT [dbo].[sysdiagrams] ON
GO
SET IDENTITY_INSERT [dbo].[sysdiagrams] OFF
GO

-- ----------------------------
-- Indexes structure for table __EFMigrationsHistory
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table __EFMigrationsHistory
-- ----------------------------
ALTER TABLE [dbo].[__EFMigrationsHistory] ADD PRIMARY KEY ([MigrationId])
GO

-- ----------------------------
-- Indexes structure for table AbpAuditLogActions
-- ----------------------------
CREATE INDEX [IX_AbpAuditLogActions_AuditLogId] ON [dbo].[AbpAuditLogActions]
([AuditLogId] ASC) 
GO
CREATE INDEX [IX_AbpAuditLogActions_TenantId_ServiceName_MethodName_ExecutionTime] ON [dbo].[AbpAuditLogActions]
([TenantId] ASC, [ServiceName] ASC, [MethodName] ASC, [ExecutionTime] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpAuditLogActions
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogActions] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpAuditLogs
-- ----------------------------
CREATE INDEX [IX_AbpAuditLogs_TenantId_ExecutionTime] ON [dbo].[AbpAuditLogs]
([TenantId] ASC, [ExecutionTime] ASC) 
GO
CREATE INDEX [IX_AbpAuditLogs_TenantId_UserId_ExecutionTime] ON [dbo].[AbpAuditLogs]
([TenantId] ASC, [UserId] ASC, [ExecutionTime] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpAuditLogs
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogs] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpBackgroundJobs
-- ----------------------------
CREATE INDEX [IX_AbpBackgroundJobs_IsAbandoned_NextTryTime] ON [dbo].[AbpBackgroundJobs]
([IsAbandoned] ASC, [NextTryTime] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpBackgroundJobs
-- ----------------------------
ALTER TABLE [dbo].[AbpBackgroundJobs] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpClaimTypes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AbpClaimTypes
-- ----------------------------
ALTER TABLE [dbo].[AbpClaimTypes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpEntityChanges
-- ----------------------------
CREATE INDEX [IX_AbpEntityChanges_AuditLogId] ON [dbo].[AbpEntityChanges]
([AuditLogId] ASC) 
GO
CREATE INDEX [IX_AbpEntityChanges_TenantId_EntityTypeFullName_EntityId] ON [dbo].[AbpEntityChanges]
([TenantId] ASC, [EntityTypeFullName] ASC, [EntityId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpEntityChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityChanges] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpEntityPropertyChanges
-- ----------------------------
CREATE INDEX [IX_AbpEntityPropertyChanges_EntityChangeId] ON [dbo].[AbpEntityPropertyChanges]
([EntityChangeId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpEntityPropertyChanges
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityPropertyChanges] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpFeatureValues
-- ----------------------------
CREATE INDEX [IX_AbpFeatureValues_Name_ProviderName_ProviderKey] ON [dbo].[AbpFeatureValues]
([Name] ASC, [ProviderName] ASC, [ProviderKey] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpFeatureValues
-- ----------------------------
ALTER TABLE [dbo].[AbpFeatureValues] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpOrganizationUnitRoles
-- ----------------------------
CREATE INDEX [IX_AbpOrganizationUnitRoles_RoleId_OrganizationUnitId] ON [dbo].[AbpOrganizationUnitRoles]
([RoleId] ASC, [OrganizationUnitId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpOrganizationUnitRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD PRIMARY KEY ([OrganizationUnitId], [RoleId])
GO

-- ----------------------------
-- Indexes structure for table AbpOrganizationUnits
-- ----------------------------
CREATE INDEX [IX_AbpOrganizationUnits_Code] ON [dbo].[AbpOrganizationUnits]
([Code] ASC) 
GO
CREATE INDEX [IX_AbpOrganizationUnits_ParentId] ON [dbo].[AbpOrganizationUnits]
([ParentId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnits] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpPermissionGrants
-- ----------------------------
CREATE INDEX [IX_AbpPermissionGrants_Name_ProviderName_ProviderKey] ON [dbo].[AbpPermissionGrants]
([Name] ASC, [ProviderName] ASC, [ProviderKey] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpPermissionGrants
-- ----------------------------
ALTER TABLE [dbo].[AbpPermissionGrants] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpRoleClaims
-- ----------------------------
CREATE INDEX [IX_AbpRoleClaims_RoleId] ON [dbo].[AbpRoleClaims]
([RoleId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpRoleClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpRoleClaims] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpRoles
-- ----------------------------
CREATE INDEX [IX_AbpRoles_NormalizedName] ON [dbo].[AbpRoles]
([NormalizedName] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpRoles] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpSecurityLogs
-- ----------------------------
CREATE INDEX [IX_AbpSecurityLogs_TenantId_Action] ON [dbo].[AbpSecurityLogs]
([TenantId] ASC, [Action] ASC) 
GO
CREATE INDEX [IX_AbpSecurityLogs_TenantId_ApplicationName] ON [dbo].[AbpSecurityLogs]
([TenantId] ASC, [ApplicationName] ASC) 
GO
CREATE INDEX [IX_AbpSecurityLogs_TenantId_Identity] ON [dbo].[AbpSecurityLogs]
([TenantId] ASC, [Identity] ASC) 
GO
CREATE INDEX [IX_AbpSecurityLogs_TenantId_UserId] ON [dbo].[AbpSecurityLogs]
([TenantId] ASC, [UserId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpSecurityLogs
-- ----------------------------
ALTER TABLE [dbo].[AbpSecurityLogs] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpSettings
-- ----------------------------
CREATE INDEX [IX_AbpSettings_Name_ProviderName_ProviderKey] ON [dbo].[AbpSettings]
([Name] ASC, [ProviderName] ASC, [ProviderKey] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpSettings
-- ----------------------------
ALTER TABLE [dbo].[AbpSettings] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpTenantConnectionStrings
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AbpTenantConnectionStrings
-- ----------------------------
ALTER TABLE [dbo].[AbpTenantConnectionStrings] ADD PRIMARY KEY ([TenantId], [Name])
GO

-- ----------------------------
-- Indexes structure for table AbpTenants
-- ----------------------------
CREATE INDEX [IX_AbpTenants_Name] ON [dbo].[AbpTenants]
([Name] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpTenants
-- ----------------------------
ALTER TABLE [dbo].[AbpTenants] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpUserClaims
-- ----------------------------
CREATE INDEX [IX_AbpUserClaims_UserId] ON [dbo].[AbpUserClaims]
([UserId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpUserClaims
-- ----------------------------
ALTER TABLE [dbo].[AbpUserClaims] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpUserLogins
-- ----------------------------
CREATE INDEX [IX_AbpUserLogins_LoginProvider_ProviderKey] ON [dbo].[AbpUserLogins]
([LoginProvider] ASC, [ProviderKey] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpUserLogins
-- ----------------------------
ALTER TABLE [dbo].[AbpUserLogins] ADD PRIMARY KEY ([UserId], [LoginProvider])
GO

-- ----------------------------
-- Indexes structure for table AbpUserOrganizationUnits
-- ----------------------------
CREATE INDEX [IX_AbpUserOrganizationUnits_UserId_OrganizationUnitId] ON [dbo].[AbpUserOrganizationUnits]
([UserId] ASC, [OrganizationUnitId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpUserOrganizationUnits
-- ----------------------------
ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD PRIMARY KEY ([OrganizationUnitId], [UserId])
GO

-- ----------------------------
-- Indexes structure for table AbpUserRoles
-- ----------------------------
CREATE INDEX [IX_AbpUserRoles_RoleId_UserId] ON [dbo].[AbpUserRoles]
([RoleId] ASC, [UserId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpUserRoles
-- ----------------------------
ALTER TABLE [dbo].[AbpUserRoles] ADD PRIMARY KEY ([UserId], [RoleId])
GO

-- ----------------------------
-- Indexes structure for table AbpUsers
-- ----------------------------
CREATE INDEX [IX_AbpUsers_Email] ON [dbo].[AbpUsers]
([Email] ASC) 
GO
CREATE INDEX [IX_AbpUsers_NormalizedEmail] ON [dbo].[AbpUsers]
([NormalizedEmail] ASC) 
GO
CREATE INDEX [IX_AbpUsers_NormalizedUserName] ON [dbo].[AbpUsers]
([NormalizedUserName] ASC) 
GO
CREATE INDEX [IX_AbpUsers_UserName] ON [dbo].[AbpUsers]
([UserName] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AbpUsers
-- ----------------------------
ALTER TABLE [dbo].[AbpUsers] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AbpUserTokens
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AbpUserTokens
-- ----------------------------
ALTER TABLE [dbo].[AbpUserTokens] ADD PRIMARY KEY ([UserId], [LoginProvider], [Name])
GO

-- ----------------------------
-- Indexes structure for table AppAuthors
-- ----------------------------
CREATE INDEX [IX_AppAuthors_Name] ON [dbo].[AppAuthors]
([Name] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppAuthors
-- ----------------------------
ALTER TABLE [dbo].[AppAuthors] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppBooks
-- ----------------------------
CREATE INDEX [IX_AppBooks_AuthorId] ON [dbo].[AppBooks]
([AuthorId] ASC) 
GO
CREATE INDEX [IX_AppBooks_AuthorId1] ON [dbo].[AppBooks]
([AuthorId1] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppBooks
-- ----------------------------
ALTER TABLE [dbo].[AppBooks] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppCategorys
-- ----------------------------
CREATE INDEX [IX_AppCategorys_ParentId] ON [dbo].[AppCategorys]
([ParentId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppCategorys
-- ----------------------------
ALTER TABLE [dbo].[AppCategorys] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppCommentReplys
-- ----------------------------
CREATE INDEX [IX_AppCommentReplys_CommentId] ON [dbo].[AppCommentReplys]
([CommentId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppCommentReplys
-- ----------------------------
ALTER TABLE [dbo].[AppCommentReplys] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppComments
-- ----------------------------
CREATE INDEX [IX_AppComments_ProductId] ON [dbo].[AppComments]
([ProductId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppComments
-- ----------------------------
ALTER TABLE [dbo].[AppComments] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppOrderItems
-- ----------------------------
CREATE INDEX [IX_AppOrderItems_OrderId1] ON [dbo].[AppOrderItems]
([OrderId1] ASC) 
GO
CREATE INDEX [IX_AppOrderItems_ProductId1] ON [dbo].[AppOrderItems]
([ProductId1] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppOrderItems
-- ----------------------------
ALTER TABLE [dbo].[AppOrderItems] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppOrderOperateHistorys
-- ----------------------------
CREATE INDEX [IX_AppOrderOperateHistorys_OrderId1] ON [dbo].[AppOrderOperateHistorys]
([OrderId1] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppOrderOperateHistorys
-- ----------------------------
ALTER TABLE [dbo].[AppOrderOperateHistorys] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppOrders
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AppOrders
-- ----------------------------
ALTER TABLE [dbo].[AppOrders] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppOrderSettings
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table AppOrderSettings
-- ----------------------------
ALTER TABLE [dbo].[AppOrderSettings] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppProductAttributes
-- ----------------------------
CREATE INDEX [IX_AppProductAttributes_Name] ON [dbo].[AppProductAttributes]
([Name] ASC) 
GO
CREATE INDEX [IX_AppProductAttributes_ProductAttributeTypeId] ON [dbo].[AppProductAttributes]
([ProductAttributeTypeId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppProductAttributes
-- ----------------------------
ALTER TABLE [dbo].[AppProductAttributes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppProductAttributeTypes
-- ----------------------------
CREATE INDEX [IX_AppProductAttributeTypes_Name] ON [dbo].[AppProductAttributeTypes]
([Name] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppProductAttributeTypes
-- ----------------------------
ALTER TABLE [dbo].[AppProductAttributeTypes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppProductAttributeValues
-- ----------------------------
CREATE INDEX [IX_AppProductAttributeValues_ProductAttributeId] ON [dbo].[AppProductAttributeValues]
([ProductAttributeId] ASC) 
GO
CREATE INDEX [IX_AppProductAttributeValues_ProductId] ON [dbo].[AppProductAttributeValues]
([ProductId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppProductAttributeValues
-- ----------------------------
ALTER TABLE [dbo].[AppProductAttributeValues] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppProducts
-- ----------------------------
CREATE INDEX [IX_AppProducts_Name] ON [dbo].[AppProducts]
([Name] ASC) 
GO
CREATE INDEX [IX_AppProducts_ProductTagId] ON [dbo].[AppProducts]
([ProductTagId] ASC) 
GO
CREATE INDEX [IX_AppProducts_OrderId] ON [dbo].[AppProducts]
([OrderId] ASC) 
GO
CREATE INDEX [IX_AppProducts_ProductCategoryId] ON [dbo].[AppProducts]
([ProductCategoryId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppProducts
-- ----------------------------
ALTER TABLE [dbo].[AppProducts] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table AppProductTagRelation
-- ----------------------------
CREATE INDEX [IX_AppProductTagRelation_ProductId1] ON [dbo].[AppProductTagRelation]
([ProductId1] ASC) 
GO
CREATE INDEX [IX_AppProductTagRelation_ProductTagId1] ON [dbo].[AppProductTagRelation]
([ProductTagId1] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table AppProductTagRelation
-- ----------------------------
ALTER TABLE [dbo].[AppProductTagRelation] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerApiClaims
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerApiClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiClaims] ADD PRIMARY KEY ([ApiResourceId], [Type])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerApiResources
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerApiResources
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiResources] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerApiScopeClaims
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerApiScopeClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeClaims] ADD PRIMARY KEY ([ApiResourceId], [Name], [Type])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerApiScopes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerApiScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopes] ADD PRIMARY KEY ([ApiResourceId], [Name])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerApiSecrets
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerApiSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiSecrets] ADD PRIMARY KEY ([ApiResourceId], [Type], [Value])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientClaims
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientClaims] ADD PRIMARY KEY ([ClientId], [Type], [Value])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientCorsOrigins
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientCorsOrigins
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientCorsOrigins] ADD PRIMARY KEY ([ClientId], [Origin])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientGrantTypes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientGrantTypes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientGrantTypes] ADD PRIMARY KEY ([ClientId], [GrantType])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientIdPRestrictions
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientIdPRestrictions
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientIdPRestrictions] ADD PRIMARY KEY ([ClientId], [Provider])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientPostLogoutRedirectUris
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientPostLogoutRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] ADD PRIMARY KEY ([ClientId], [PostLogoutRedirectUri])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientProperties
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientProperties
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientProperties] ADD PRIMARY KEY ([ClientId], [Key])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientRedirectUris
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientRedirectUris
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientRedirectUris] ADD PRIMARY KEY ([ClientId], [RedirectUri])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClients
-- ----------------------------
CREATE INDEX [IX_IdentityServerClients_ClientId] ON [dbo].[IdentityServerClients]
([ClientId] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table IdentityServerClients
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClients] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientScopes
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientScopes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientScopes] ADD PRIMARY KEY ([ClientId], [Scope])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerClientSecrets
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerClientSecrets
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientSecrets] ADD PRIMARY KEY ([ClientId], [Type], [Value])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerDeviceFlowCodes
-- ----------------------------
CREATE UNIQUE INDEX [IX_IdentityServerDeviceFlowCodes_DeviceCode] ON [dbo].[IdentityServerDeviceFlowCodes]
([DeviceCode] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO
CREATE INDEX [IX_IdentityServerDeviceFlowCodes_Expiration] ON [dbo].[IdentityServerDeviceFlowCodes]
([Expiration] ASC) 
GO
CREATE UNIQUE INDEX [IX_IdentityServerDeviceFlowCodes_UserCode] ON [dbo].[IdentityServerDeviceFlowCodes]
([UserCode] ASC) 
WITH (IGNORE_DUP_KEY = ON)
GO

-- ----------------------------
-- Primary Key structure for table IdentityServerDeviceFlowCodes
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerDeviceFlowCodes] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerIdentityClaims
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerIdentityClaims
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityClaims] ADD PRIMARY KEY ([IdentityResourceId], [Type])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerIdentityResources
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table IdentityServerIdentityResources
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityResources] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table IdentityServerPersistedGrants
-- ----------------------------
CREATE INDEX [IX_IdentityServerPersistedGrants_Expiration] ON [dbo].[IdentityServerPersistedGrants]
([Expiration] ASC) 
GO
CREATE INDEX [IX_IdentityServerPersistedGrants_SubjectId_ClientId_Type] ON [dbo].[IdentityServerPersistedGrants]
([SubjectId] ASC, [ClientId] ASC, [Type] ASC) 
GO

-- ----------------------------
-- Primary Key structure for table IdentityServerPersistedGrants
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerPersistedGrants] ADD PRIMARY KEY ([Key])
GO

-- ----------------------------
-- Indexes structure for table ProductTag
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table ProductTag
-- ----------------------------
ALTER TABLE [dbo].[ProductTag] ADD PRIMARY KEY ([Id])
GO

-- ----------------------------
-- Indexes structure for table sysdiagrams
-- ----------------------------

-- ----------------------------
-- Primary Key structure for table sysdiagrams
-- ----------------------------
ALTER TABLE [dbo].[sysdiagrams] ADD PRIMARY KEY ([diagram_id])
GO

-- ----------------------------
-- Uniques structure for table sysdiagrams
-- ----------------------------
ALTER TABLE [dbo].[sysdiagrams] ADD UNIQUE ([principal_id] ASC, [name] ASC)
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpAuditLogActions]
-- ----------------------------
ALTER TABLE [dbo].[AbpAuditLogActions] ADD FOREIGN KEY ([AuditLogId]) REFERENCES [dbo].[AbpAuditLogs] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpEntityChanges]
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityChanges] ADD FOREIGN KEY ([AuditLogId]) REFERENCES [dbo].[AbpAuditLogs] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpEntityPropertyChanges]
-- ----------------------------
ALTER TABLE [dbo].[AbpEntityPropertyChanges] ADD FOREIGN KEY ([EntityChangeId]) REFERENCES [dbo].[AbpEntityChanges] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpOrganizationUnitRoles]
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD FOREIGN KEY ([OrganizationUnitId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AbpOrganizationUnitRoles] ADD FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpOrganizationUnits]
-- ----------------------------
ALTER TABLE [dbo].[AbpOrganizationUnits] ADD FOREIGN KEY ([ParentId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpRoleClaims]
-- ----------------------------
ALTER TABLE [dbo].[AbpRoleClaims] ADD FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpTenantConnectionStrings]
-- ----------------------------
ALTER TABLE [dbo].[AbpTenantConnectionStrings] ADD FOREIGN KEY ([TenantId]) REFERENCES [dbo].[AbpTenants] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpUserClaims]
-- ----------------------------
ALTER TABLE [dbo].[AbpUserClaims] ADD FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpUserLogins]
-- ----------------------------
ALTER TABLE [dbo].[AbpUserLogins] ADD FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpUserOrganizationUnits]
-- ----------------------------
ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD FOREIGN KEY ([OrganizationUnitId]) REFERENCES [dbo].[AbpOrganizationUnits] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AbpUserOrganizationUnits] ADD FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpUserRoles]
-- ----------------------------
ALTER TABLE [dbo].[AbpUserRoles] ADD FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AbpRoles] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AbpUserRoles] ADD FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AbpUserTokens]
-- ----------------------------
ALTER TABLE [dbo].[AbpUserTokens] ADD FOREIGN KEY ([UserId]) REFERENCES [dbo].[AbpUsers] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppBooks]
-- ----------------------------
ALTER TABLE [dbo].[AppBooks] ADD FOREIGN KEY ([AuthorId]) REFERENCES [dbo].[AppAuthors] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppBooks] ADD FOREIGN KEY ([AuthorId1]) REFERENCES [dbo].[AppAuthors] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppCategorys]
-- ----------------------------
ALTER TABLE [dbo].[AppCategorys] ADD FOREIGN KEY ([ParentId]) REFERENCES [dbo].[AppCategorys] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppCommentReplys]
-- ----------------------------
ALTER TABLE [dbo].[AppCommentReplys] ADD FOREIGN KEY ([CommentId]) REFERENCES [dbo].[AppComments] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppComments]
-- ----------------------------
ALTER TABLE [dbo].[AppComments] ADD FOREIGN KEY ([ProductId]) REFERENCES [dbo].[AppProducts] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppOrderItems]
-- ----------------------------
ALTER TABLE [dbo].[AppOrderItems] ADD FOREIGN KEY ([OrderId1]) REFERENCES [dbo].[AppOrders] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppOrderItems] ADD FOREIGN KEY ([ProductId1]) REFERENCES [dbo].[AppProducts] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppOrderOperateHistorys]
-- ----------------------------
ALTER TABLE [dbo].[AppOrderOperateHistorys] ADD FOREIGN KEY ([OrderId1]) REFERENCES [dbo].[AppOrders] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppProductAttributes]
-- ----------------------------
ALTER TABLE [dbo].[AppProductAttributes] ADD FOREIGN KEY ([ProductAttributeTypeId]) REFERENCES [dbo].[AppProductAttributeTypes] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppProductAttributeValues]
-- ----------------------------
ALTER TABLE [dbo].[AppProductAttributeValues] ADD FOREIGN KEY ([ProductAttributeId]) REFERENCES [dbo].[AppProductAttributes] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppProductAttributeValues] ADD FOREIGN KEY ([ProductId]) REFERENCES [dbo].[AppProducts] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppProducts]
-- ----------------------------
ALTER TABLE [dbo].[AppProducts] ADD FOREIGN KEY ([ProductCategoryId]) REFERENCES [dbo].[AppCategorys] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppProducts] ADD FOREIGN KEY ([OrderId]) REFERENCES [dbo].[AppOrders] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppProducts] ADD FOREIGN KEY ([ProductTagId]) REFERENCES [dbo].[ProductTag] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[AppProductTagRelation]
-- ----------------------------
ALTER TABLE [dbo].[AppProductTagRelation] ADD FOREIGN KEY ([ProductId1]) REFERENCES [dbo].[AppProducts] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO
ALTER TABLE [dbo].[AppProductTagRelation] ADD FOREIGN KEY ([ProductTagId1]) REFERENCES [dbo].[ProductTag] ([Id]) ON DELETE NO ACTION ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerApiClaims]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiClaims] ADD FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerApiScopeClaims]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopeClaims] ADD FOREIGN KEY ([ApiResourceId], [Name]) REFERENCES [dbo].[IdentityServerApiScopes] ([ApiResourceId], [Name]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerApiScopes]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiScopes] ADD FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerApiSecrets]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerApiSecrets] ADD FOREIGN KEY ([ApiResourceId]) REFERENCES [dbo].[IdentityServerApiResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientClaims]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientClaims] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientCorsOrigins]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientCorsOrigins] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientGrantTypes]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientGrantTypes] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientIdPRestrictions]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientIdPRestrictions] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientPostLogoutRedirectUris]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientPostLogoutRedirectUris] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientProperties]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientProperties] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientRedirectUris]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientRedirectUris] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientScopes]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientScopes] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerClientSecrets]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerClientSecrets] ADD FOREIGN KEY ([ClientId]) REFERENCES [dbo].[IdentityServerClients] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO

-- ----------------------------
-- Foreign Key structure for table [dbo].[IdentityServerIdentityClaims]
-- ----------------------------
ALTER TABLE [dbo].[IdentityServerIdentityClaims] ADD FOREIGN KEY ([IdentityResourceId]) REFERENCES [dbo].[IdentityServerIdentityResources] ([Id]) ON DELETE CASCADE ON UPDATE NO ACTION
GO
