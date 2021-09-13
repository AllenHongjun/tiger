using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Business.Basic.Dtos;
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
        /// <summary>
        /// 根据属性类型查询所有的属性和值 返回指定类型的规格属性
        /// </summary>
        /// <param name="productAttributeTypeId"></param>
        /// <returns></returns>
        public ProductAttributeResultDto[] GetProductAttributeDtoByTypeId(Guid productAttributeTypeId);
    }
}
