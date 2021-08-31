using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.ProductAttributeTypes;
using Tiger.Business.Basic;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    /// <summary>
    /// 商品属性分类
    /// </summary>
    [RemoteService(false)]
    public class ProductAttributeTypeAppService
        : CrudAppService<
            ProductAttributeType, //The  entity
            ProductAttributeTypeDto,
            Guid, //Primary key 
            GetProductAttributeTypeListDto,
            CreateUpdateProductAttributeTypeDto, //Used for paging/sorting
            CreateUpdateProductAttributeTypeDto>, //Used to create/update
        IProductAttributeTypeAppService
    {
        private readonly IProductAttributeTypeRepository _productAttributeTpyeRepository;
        public ProductAttributeTypeAppService(IProductAttributeTypeRepository productAttributeTpyeRepository) : base(productAttributeTpyeRepository)
        {
            _productAttributeTpyeRepository = productAttributeTpyeRepository;
        }
    }
}
