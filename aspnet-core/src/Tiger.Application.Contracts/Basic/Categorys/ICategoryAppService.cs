using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.Categorys
{
    public interface ICategoryAppService:
        ICrudAppService<
            CategoryDto, // Used to show category
            Guid,
            GetCategoryListDto,
            PagedAndSortedResultRequestDto,
            CreateUpdateCategoryDto>
    {
        
    }
}
