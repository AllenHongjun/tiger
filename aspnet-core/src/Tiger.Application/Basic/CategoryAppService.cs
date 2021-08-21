using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Basic.Categorys;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace Tiger.Basic
{
    public class CategoryAppService :
        CrudAppService<
            Category, //The  entity
            CategoryDto,
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateCategoryDto>, //Used to create/update
        ICategoryAppService
    {
        public CategoryAppService(IRepository<Category, Guid> repository) : base(repository)
        {
        }
    }
}
