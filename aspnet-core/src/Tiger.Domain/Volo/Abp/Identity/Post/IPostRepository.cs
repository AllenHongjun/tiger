using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Volo.Abp.Identity.Post;

public interface IPostRepository : IRepository<Post, Guid>
{
}
