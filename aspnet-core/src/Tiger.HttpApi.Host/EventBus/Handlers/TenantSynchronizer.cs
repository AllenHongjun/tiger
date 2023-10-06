using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Datas;
using Tiger.EntityFrameworkCore;
using Tiger.Volo.Abp.Sass.MultiTenancy;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using IdentityRole = Volo.Abp.Identity.IdentityRole;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using TenantEto = Tiger.Volo.Abp.Sass.Tenants.TenantEto;

namespace Tiger.EventBus.Handlers
{
    /// <summary>
    /// 租户同步
    /// </summary>
    /// <remarks>
    /// 事件处理程序  https://docs.abp.io/zh-Hans/abp/3.2/Local-Event-Bus
    /// </remarks>
    public class TenantSynchronizer :
        IDistributedEventHandler<CreateEventData>,
        IDistributedEventHandler<EntityDeletedEto<TenantEto>>,
        ITransientDependency
    {
        private const string ModelDatabaseProviderAnnotationKey = "_Abp_DatabaseProvider";

        protected ILogger<TenantSynchronizer> Logger { get; }
        protected ICurrentTenant CurrentTenant { get; }
        protected IGuidGenerator GuidGenerator { get; }
        protected IUnitOfWorkManager UnitOfWorkManager { get; }
        protected IDbSchemaMigrator DbSchemaMigrator { get; }
        protected IPermissionGrantRepository PermissionGrantRepository { get; }
        protected IPermissionDefinitionManager PermissionDefinitionManager { get; }
        protected IdentityUserManager IdentityUserManager { get; }
        protected IdentityRoleManager IdentityRoleManager { get; }
        public TenantSynchronizer(
            ICurrentTenant currentTenant,
            IGuidGenerator guidGenerator,
            IUnitOfWorkManager unitOfWorkManager,
            IDbSchemaMigrator dbSchemaMigrator,
            IPermissionGrantRepository permissionGrantRepository,
            IPermissionDefinitionManager permissionDefinitionManager,
            ILogger<TenantSynchronizer> logger,
            IdentityUserManager identityUserManager,
            IdentityRoleManager identityRoleManager)
        {
            CurrentTenant = currentTenant;
            GuidGenerator = guidGenerator;
            UnitOfWorkManager = unitOfWorkManager;
            DbSchemaMigrator = dbSchemaMigrator;
            PermissionGrantRepository = permissionGrantRepository;
            PermissionDefinitionManager = permissionDefinitionManager;

            Logger = logger;
            IdentityUserManager=identityUserManager;
            IdentityRoleManager=identityRoleManager;
        }

        public async Task HandleEventAsync(EntityDeletedEto<TenantEto> eventData)
        {
            using var unitOfWork = UnitOfWorkManager.Begin();
            // 订阅租户删除事件,删除管理员角色所有权限
            // TODO: 租户貌似不存在了,删除应该会失败
            // 有缓存存在的话,可以获取到租户连接字符串
            using (CurrentTenant.Change(eventData.Entity.Id))
            {
                // var grantPermissions = await PermissionGrantRepository.GetListAsync("R", "admin");

                // EfCore MySql 批量删除还是一条一条的语句?
                // PermissionGrantRepository.GetDbSet().RemoveRange(grantPermissions);
                var dbContext = PermissionGrantRepository.GetDbContext();
                var dbProvider = (EfCoreDatabaseProvider?)dbContext.Model[ModelDatabaseProviderAnnotationKey];
                if (dbProvider != null)
                {
                    var permissionEntityType = dbContext.Model.FindEntityType(typeof(PermissionGrant));
                    var permissionTableName = permissionEntityType.GetTableName();
                    var batchRmovePermissionSql = string.Empty;
                    switch (dbProvider)
                    {
                        case EfCoreDatabaseProvider.MySql:
                            batchRmovePermissionSql = BuildMySqlBatchDeleteScript(permissionTableName, eventData.Entity.Id);
                            break;
                        case EfCoreDatabaseProvider.SqlServer:
                            batchRmovePermissionSql = BuildSqlServerBatchDeleteScript(permissionTableName, eventData.Entity.Id);
                            break;
                        default:
                            Logger.LogWarning($"Tenant permissions data has not removed, Because database provider: {dbProvider} batch statements are not defined!");
                            return;
                    }

                    await dbContext.Database.ExecuteSqlRawAsync(batchRmovePermissionSql);

                    await unitOfWork.SaveChangesAsync();

                    Logger.LogInformation("The tenant permissions data has removed!");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        /// <remarks>
        /// 订阅租户创建事件
        /// </remarks>
        public async Task HandleEventAsync(CreateEventData eventData)
        {
            using (var unitOfWork = UnitOfWorkManager.Begin())
            {
                // 订阅租户新增事件,置入管理员角色所有权限
                using (CurrentTenant.Change(eventData.Id, eventData.Name))
                {
                    Logger.LogInformation("Migrating the new tenant database with platform...");
                    // 迁移租户数据
                    await DbSchemaMigrator.MigrateAsync<TigerDbContext>(
                        (connectionString, builder) =>
                        {
                            //builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

                            return new TigerDbContext(builder.Options);
                        });

                    Logger.LogInformation("Migrated the new tenant database with platform.");

                    Logger.LogInformation("Seeding the new tenant admin role permissions...");
                    var definitionPermissions = PermissionDefinitionManager.GetPermissions();
                    var grantPermissions = definitionPermissions
                        .Where(p => p.MultiTenancySide.HasFlag(MultiTenancySides.Tenant))
                        .Select(p => p.Name).ToArray();
                    //var grantPermissions = new List<PermissionGrant>();
                    //foreach (var permission in definitionPermissions)
                    //{
                    //    var permissionGrant = new PermissionGrant(GuidGenerator.Create(),
                    //            permission.Name, "R", "admin", eventData.Id);
                    //    grantPermissions.Add(permissionGrant);
                    //}
                    // TODO: MySql 批量新增还是一条一条的语句?
                    // await PermissionGrantRepository.GetDbSet().AddRangeAsync(grantPermissions);

                    var dbContext =  PermissionGrantRepository.GetDbContext();
                    var dbProvider = (EfCoreDatabaseProvider?)dbContext.Model[ModelDatabaseProviderAnnotationKey];
                    if (dbProvider != null)
                    {
                        var permissionEntityType = dbContext.Model.FindEntityType(typeof(PermissionGrant));
                        var permissionTableName = permissionEntityType.GetTableName();
                        var batchInsertPermissionSql = string.Empty;
                        switch (dbProvider)
                        {
                            case EfCoreDatabaseProvider.MySql:
                                batchInsertPermissionSql = BuildMySqlBatchInsertScript(permissionTableName, eventData.Id, grantPermissions);
                                break;
                            case EfCoreDatabaseProvider.SqlServer:
                                batchInsertPermissionSql = BuildSqlServerBatchInsertScript(permissionTableName, eventData.Id, grantPermissions);
                                break;
                            default:
                                Logger.LogWarning($"Tenant permissions data has not initialized, Because database provider: {dbProvider} batch statements are not defined!");
                                return;
                        }
                        await dbContext.Database.ExecuteSqlRawAsync(batchInsertPermissionSql);

                        await unitOfWork.SaveChangesAsync();

                        Logger.LogInformation("The new tenant permissions data initialized!");
                    }
                }
            }
        }


        /// <summary>
        /// 创建租户管理员的账号
        /// </summary>
        /// <param name="eventData"></param>
        /// <returns></returns>
        /// <remarks>
        /// 测试在一个事件处理程序中能否两个方法订阅同一个事件
        /// </remarks>
        public async Task SeedTenantAdminAsync(CreateEventData eventData)
        {
            using (CurrentTenant.Change(eventData.Id, eventData.Name))
            {
                // 创建租户的默认管理员账号密码 默认 admin
                const string tenantAdminUserName = "admin";
                const string tenantAdminRoleName = "admin";
                var tenantAdminRoleId = Guid.Empty;

                if (!await IdentityRoleManager.RoleExistsAsync(tenantAdminRoleName))
                {
                    tenantAdminRoleId = GuidGenerator.Create();
                    var tenantAdminRole = new IdentityRole(tenantAdminRoleId, tenantAdminRoleName, eventData.Id)
                    {
                        IsStatic = true,
                        IsPublic = true
                    };
                    (await IdentityRoleManager.CreateAsync(tenantAdminRole)).CheckErrors();
                }
                else
                {
                    var tenantAdminRole = await IdentityRoleManager.FindByNameAsync(tenantAdminRoleName);
                    tenantAdminRoleId = tenantAdminRole.Id;
                }

                var tenantAdminUser = await IdentityUserManager.FindByNameAsync(eventData.AdminEmailAddress);
                if (tenantAdminUser == null)
                {
                    tenantAdminUser = new IdentityUser(
                        eventData.AdminUserId,
                        tenantAdminUserName,
                        eventData.AdminEmailAddress,
                        eventData.Id);

                    tenantAdminUser.AddRole(tenantAdminRoleId);
                    // 创建租户管理用户
                    await IdentityUserManager.CreateAsync(tenantAdminUser, eventData.AdminPassword);
                }
            }
        }

        protected virtual string BuildMySqlBatchInsertScript(string tableName, Guid tenantId, string[] permissions)
        {
            var batchInsertPermissionSql = new StringBuilder(128);
            batchInsertPermissionSql.AppendLine($"INSERT INTO `{tableName}`(`Id`, `TenantId`, `Name`, `ProviderName`, `ProviderKey`)");
            batchInsertPermissionSql.AppendLine("VALUES");
            for (int i = 0; i < permissions.Length; i++)
            {
                batchInsertPermissionSql.AppendLine($"(UUID(), '{tenantId}','{permissions[i]}','R','admin')");
                if (i < permissions.Length - 1)
                {
                    batchInsertPermissionSql.AppendLine(",");
                }
            }
            return batchInsertPermissionSql.ToString();
        }

        protected virtual string BuildSqlServerBatchInsertScript(string tableName, Guid tenantId, string[] permissions)
        {
            var batchInsertPermissionSql = new StringBuilder(128);
            batchInsertPermissionSql.AppendLine($"INSERT INTO {tableName}(Id, TenantId, Name, ProviderName, ProviderKey)");
            batchInsertPermissionSql.Append("VALUES");
            for (int i = 0; i < permissions.Length; i++)
            {
                batchInsertPermissionSql.AppendLine($"(NEWID(), '{tenantId}','{permissions[i]}','R','admin')");
                if (i < permissions.Length - 1)
                {
                    batchInsertPermissionSql.AppendLine(",");
                }
            }
            return batchInsertPermissionSql.ToString();
        }

        protected virtual string BuildMySqlBatchDeleteScript(string tableName, Guid tenantId)
        {
            var batchRemovePermissionSql = new StringBuilder(128);
            batchRemovePermissionSql.AppendLine($"DELETE FROM `{tableName}` WHERE `TenantId` = '{tenantId}'");
            batchRemovePermissionSql.AppendLine("AND `ProviderName`='R' AND `ProviderKey`='admin'");
            return batchRemovePermissionSql.ToString();
        }

        protected virtual string BuildSqlServerBatchDeleteScript(string tableName, Guid tenantId)
        {
            var batchRemovePermissionSql = new StringBuilder(128);
            batchRemovePermissionSql.AppendLine($"DELETE {tableName} WHERE TenantId = '{tenantId}'");
            batchRemovePermissionSql.AppendLine("AND ProviderName='R' AND ProviderKey='admin'");
            return batchRemovePermissionSql.ToString();
        }
    }
}
