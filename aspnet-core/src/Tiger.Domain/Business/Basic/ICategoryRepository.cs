using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Basic
{
    public interface ICategoryRepository : IRepository<Category, Guid>
    {
        Task<List<Category>> GetChildrenAsync(Guid parentId);
    }
}
