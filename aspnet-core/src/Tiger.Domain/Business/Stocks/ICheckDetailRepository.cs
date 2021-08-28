using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface ICheckDetailRepository : IRepository<CheckDetail, Guid>
    {
    }
}