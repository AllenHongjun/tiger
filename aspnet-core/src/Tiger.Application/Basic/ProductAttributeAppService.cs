using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.ProductAttributes;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    [RemoteService(false)]
    public class ProductAttributeAppService
        :CrudAppService<
            ProductAttribute, //The  entity
            ProductAttributeDto,
            Guid, //Primary key 
            GetProductAttributeListDto,
            CreateUpdateProductAttributeDto, //Used for paging/sorting
            CreateUpdateProductAttributeDto>, //Used to create/update
        IProductAttributeAppService
    {
        public ProductAttributeAppService(IRepository<ProductAttribute, Guid> repository) : base(repository)
        {
        }

        
    }
}
