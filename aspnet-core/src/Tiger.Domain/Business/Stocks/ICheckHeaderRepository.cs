using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ICheckHeaderRepository : IRepository<CheckHeader, Guid>
    {
        Task<CheckHeader> GetAsync(Guid id);
    }
}