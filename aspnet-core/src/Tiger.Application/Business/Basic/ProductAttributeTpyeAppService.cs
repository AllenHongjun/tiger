using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.ProductAttributeTpyes;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性分类
    /// </summary>
    [RemoteService(false)]
    public class ProductAttributeTpyeAppService
        : CrudAppService<
            ProductAttributeType, //The  entity
            ProductAttributeTypeDto,
            Guid, //Primary key 
            GetProductAttributeTypeListDto,
            CreateUpdateProductAttributeTypeDto, //Used for paging/sorting
            CreateUpdateProductAttributeTypeDto>, //Used to create/update
        IProductAttributeTypeAppService
    {
        public ProductAttributeTpyeAppService(IRepository<ProductAttributeType, Guid> repository) : base(repository)
        {
        }
    }
}
