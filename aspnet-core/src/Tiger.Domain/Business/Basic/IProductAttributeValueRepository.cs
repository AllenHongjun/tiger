using System;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Business.Basic
{
    public interface IProductAttributeValueRepository : IRepository<ProductAttributeValue, Guid>
    {
    }
}