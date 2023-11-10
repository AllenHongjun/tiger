using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Tiger.Module.Train;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Module.Teachings;

public class CourseRepository : EfCoreRepository<TigerDbContext, Course, Guid>, ICourseRepository
{
    public CourseRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public override IQueryable<Course> WithDetails()
    {
        return  GetQueryable().IncludeDetails();
    }
}