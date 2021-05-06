using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace TigerAdmin.Data
{
    /* This is used if database provider does't define
     * ITigerAdminDbSchemaMigrator implementation.
     */
    public class NullTigerAdminDbSchemaMigrator : ITigerAdminDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}