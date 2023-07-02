using System;
using System.Linq;
using System.Threading.Tasks;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Volo.Abp.Identity.Post;

public class PostRepository : EfCoreRepository<TigerDbContext, Post, Guid>, IPostRepository
{
    public PostRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    //public override async Task<IQueryable<Post>> WithDetailsAsync()
    //{
    //    return (await GetQueryableAsync()).IncludeDetails();
    //}
}