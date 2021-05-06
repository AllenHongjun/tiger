using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TigerAdmin.Data;
using Volo.Abp.DependencyInjection;

namespace TigerAdmin.EntityFrameworkCore
{
    public class EntityFrameworkCoreTigerAdminDbSchemaMigrator
        : ITigerAdminDbSchemaMigrator, ITransientDependency
    {
        private readonly IServiceProvider _serviceProvider;

        public EntityFrameworkCoreTigerAdminDbSchemaMigrator(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task MigrateAsync()
        {
            /* We intentionally resolving the TigerAdminMigrationsDbContext
             * from IServiceProvider (instead of directly injecting it)
             * to properly get the connection string of the current tenant in the
             * current scope.
             */

            await _serviceProvider
                .GetRequiredService<TigerAdminMigrationsDbContext>()
                .Database
                .MigrateAsync();
        }
    }
}