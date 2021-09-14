using System;
using Tiger.Business.Basic.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Business.Basic
{
    public interface IProductAttributeValueAppService :
        ICrudAppService< 
            ProductAttributeValueDto, 
            Guid, 
            PagedAndSortedResultRequestDto,
            CreateUpdateProductAttributeValueDto,
            CreateUpdateProductAttributeValueDto>
    {

    }
}