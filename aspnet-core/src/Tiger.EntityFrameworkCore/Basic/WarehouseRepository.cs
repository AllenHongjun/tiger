using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Basic
{
    public class WarehouseRepository 
        : EfCoreRepository<TigerDbContext, Warehouse, Guid>, 
        IWarehouseRepository
    {
        public WarehouseRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}