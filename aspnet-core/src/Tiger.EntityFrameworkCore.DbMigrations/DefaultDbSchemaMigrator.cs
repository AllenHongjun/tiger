using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tiger.Datas;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.MultiTenancy;

namespace Tiger
{
    public class DefaultDbSchemaMigrator : IDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ICurrentTenant _currentTenant;
        private readonly AbpDbConnectionOptions _dbConnectionOptions;

        public DefaultDbSchemaMigrator(
            ICurrentTenant currentTenant,
            IServiceProvider serviceProvider,
            IOptions<AbpDbConnectionOptions> dbConnectionOptions)
        {
            _currentTenant = currentTenant;
            _serviceProvider = serviceProvider;
            _dbConnectionOptions = dbConnectionOptions.Value;
        }

        public async virtual Task MigrateAsync<TDbContext>(
            [NotNull] Func<string, DbContextOptionsBuilder<TDbContext>, TDbContext> configureDbContext)
            where TDbContext : AbpDbContext<TDbContext>
        {
            var connectionStringName = ConnectionStringNameAttribute.GetConnStringName<TDbContext>();

            string connectionString = null;
            if (_currentTenant.IsAvailable)
            {
                var connectionStringResolver = _serviceProvider.GetRequiredService<IConnectionStringResolver>();
                connectionString = connectionStringResolver.Resolve(connectionStringName);
            }
            else
            {
                connectionString = _dbConnectionOptions.ConnectionStrings.GetValueOrDefault(connectionStringName);
            }

            var defaultConnectionString = _dbConnectionOptions.ConnectionStrings.Default;
            // 租户连接字符串与默认连接字符串相同,则不执行迁移脚本
            if (string.Equals(
                connectionString,
                defaultConnectionString,
                StringComparison.InvariantCultureIgnoreCase))
            {
                return;
            }

            connectionString??= defaultConnectionString;

            var dbContextBuilder = new DbContextOptionsBuilder<TDbContext>();
            using var dbContext = configureDbContext(connectionString, dbContextBuilder);

            await dbContext.Database.MigrateAsync();
        }
    }
}
