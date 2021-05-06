using System.Threading.Tasks;

namespace TigerAdmin.Data
{
    public interface ITigerAdminDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
