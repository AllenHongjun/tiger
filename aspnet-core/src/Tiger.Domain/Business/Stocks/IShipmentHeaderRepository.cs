using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Stocks
{
    public interface IShipmentHeaderRepository : IRepository<ShipmentHeader, Guid>
    {
    }
}