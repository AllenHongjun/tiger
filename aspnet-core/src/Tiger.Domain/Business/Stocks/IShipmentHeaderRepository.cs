using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Stocks
{
    public interface IShipmentHeaderRepository : IRepository<ShipmentHeader, Guid>
    {
        Task<ShipmentHeader> GetAsync(Guid id);
    }
}