using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Stocks
{
    public interface IShipmentDetailRepository : IRepository<ShipmentDetail, Guid>
    {
        Task<ShipmentHeader> GetAsync(Guid id);
    }
}