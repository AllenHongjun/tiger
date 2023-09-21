using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Books;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

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
