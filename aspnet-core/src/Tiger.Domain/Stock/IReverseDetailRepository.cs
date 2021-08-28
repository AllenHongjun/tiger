using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Stock
{
    public interface IReverseDetailRepository : IRepository<ReverseDetail, Guid>
    {
    }
}