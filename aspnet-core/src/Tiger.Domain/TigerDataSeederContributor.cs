using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tiger
{
    /// <summary>
    /// 生成数据表之前，给表添加数据。
    /// </summary>
    public class TigerDataSeederContributor : IDataSeedContributor, ITransientDependency
    {


        public TigerDataSeederContributor()
        {
        }

        public Task SeedAsync(DataSeedContext context)
        {
            return Task.CompletedTask;
        }
    }
}
