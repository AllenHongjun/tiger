using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.ProductAttributeTpyes;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{   
    /// <summary>
    /// 商品属性分类
    /// </summary>
    public class ProductAttributeTpyeAppService
        : CrudAppService<
            ProductAttributeType, //The  entity
            ProductAttributeTypeDto,
            Guid, //Primary key 
            GetProductAttributeTpyeListDto,
            CreateUpdateProductAttributeTypeDto, //Used for paging/sorting
            CreateUpdateProductAttributeTypeDto>, //Used to create/update
        IProductAttributeTypeAppService
    {
        public ProductAttributeTpyeAppService(IRepository<ProductAttributeType, Guid> repository) : base(repository)
        {
        }
    }
}
