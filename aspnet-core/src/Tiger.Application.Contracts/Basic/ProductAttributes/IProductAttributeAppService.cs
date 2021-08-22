using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.ProductAttributes
{
    public interface IProductAttributeAppService
        : ICrudAppService<
            ProductAttributeDto, // Used to show <ProductAttributeDto
            Guid,
            GetProductAttributeListDto,
            
            CreateUpdateProductAttributeDto,
            CreateUpdateProductAttributeDto
            >
    {

    }
}
