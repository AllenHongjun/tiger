using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Stocks
{
    public interface IShipmentDetailRepository : IRepository<ShipmentDetail, Guid>
    {
    }
}