using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ICheckHeaderRepository : IRepository<CheckHeader, Guid>
    {
    }
}