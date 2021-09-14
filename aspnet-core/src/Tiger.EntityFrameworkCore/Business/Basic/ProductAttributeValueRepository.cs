using System;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Basic
{
    public class ProductAttributeValueRepository : EfCoreRepository<TigerDbContext, ProductAttributeValue, Guid>, IProductAttributeValueRepository
    {
        public ProductAttributeValueRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}