using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Basic.Categorys
{
    public interface ICategoryAppService:
        ICrudAppService<
           
            CategoryDto, // Used to show category
         
            Guid,
            GetCategoryListDto,
            CreateUpdateCategoryDto,
            CreateUpdateCategoryDto>
    {

        /// <summary>
        /// 获取分类列表树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetAllListAsync(GetCategoryListDto input);

        /// <summary>
        /// 获取分类明细树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<CategoryDto> GetDetailsAsync(Guid id);

        /// <summary>
        /// 根据父级id查找子分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        Task<List<CategoryDto>> GetChildrenAsync(Guid parentId);

        
    }
}
