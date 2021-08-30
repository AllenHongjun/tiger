using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic;
using Tiger.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tiger.Business.Basic
{
    public class ProductAttributeTypeRepository
        : EfCoreRepository<TigerDbContext, ProductAttributeType, Guid>,
            IProductAttributeTypeRepository
    {
        public ProductAttributeTypeRepository(IDbContextProvider<TigerDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
