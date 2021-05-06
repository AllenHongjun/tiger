using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace TigerAdmin.EntityFrameworkCore
{
    /* This class is needed for EF Core console commands
     * (like Add-Migration and Update-Database commands) */
    public class TigerAdminMigrationsDbContextFactory : IDesignTimeDbContextFactory<TigerAdminMigrationsDbContext>
    {
        public TigerAdminMigrationsDbContext CreateDbContext(string[] args)
        {
            TigerAdminEfCoreEntityExtensionMappings.Configure();

            var configuration = BuildConfiguration();

            var builder = new DbContextOptionsBuilder<TigerAdminMigrationsDbContext>()
                .UseSqlServer(configuration.GetConnectionString("Default"));

            return new TigerAdminMigrationsDbContext(builder.Options);
        }

        private static IConfigurationRoot BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../TigerAdmin.DbMigrator/"))
                .AddJsonFile("appsettings.json", optional: false);

            return builder.Build();
        }
    }
}
