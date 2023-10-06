using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Datas
{
    public interface IDbSchemaMigrator
    {
        Task MigrateAsync<TDbContext>(
            [NotNull] Func<string, DbContextOptionsBuilder<TDbContext>, TDbContext> configureDbContext)
            where TDbContext : AbpDbContext<TDbContext>;
    }
}
