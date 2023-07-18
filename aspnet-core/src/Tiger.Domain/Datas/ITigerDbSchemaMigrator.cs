using System.Threading.Tasks;

namespace Tiger.Datas
{
    public interface ITigerDbSchemaMigrator
    {
        Task MigrateAsync();
    }
}
