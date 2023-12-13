using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.QuestionBank;

public class TrainPlatformRepository : EfCoreRepository<TigerDbContext, TrainPlatform, Guid>, ITrainPlatformRepository
{
    public TrainPlatformRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<TrainPlatform> WithDetails()
    {
        return GetQueryable().IncludeDetails();
    }
}