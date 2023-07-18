using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tiger.Datas
{
    /* This is used if database provider does't define
     * ITigerDbSchemaMigrator implementation.
     */
    public class NullTigerDbSchemaMigrator : ITigerDbSchemaMigrator, ITransientDependency
    {
        public Task MigrateAsync()
        {
            return Task.CompletedTask;
        }
    }
}