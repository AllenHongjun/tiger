using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.ProductAttributeTypes
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public interface IProductAttributeTypeAppService:
        ICrudAppService<
            ProductAttributeTypeDto, // Used to show ProductAttributeTpye
            Guid,
            GetProductAttributeTypeListDto,
            CreateUpdateProductAttributeTypeDto,
            CreateUpdateProductAttributeTypeDto>
    {

    }
}
