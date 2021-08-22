using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.ProductAttributeTpyes
{
    /// <summary>
    /// 产品属性
    /// </summary>
    public interface IProductAttributeTypeAppService:
        ICrudAppService<
            ProductAttributeTypeDto, // Used to show ProductAttributeTpye
            Guid,
            GetProductAttributeTpyeListDto,
            CreateUpdateProductAttributeTypeDto,
            CreateUpdateProductAttributeTypeDto>
    {

    }
}
