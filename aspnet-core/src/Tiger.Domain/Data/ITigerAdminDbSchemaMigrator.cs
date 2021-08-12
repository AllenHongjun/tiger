using System.Threading.Tasks;

namespace Tiger.Data
{
    public interface ITigerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
