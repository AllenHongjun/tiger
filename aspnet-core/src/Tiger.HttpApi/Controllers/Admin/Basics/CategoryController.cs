using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Basic.Categorys;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Identity;

namespace Tiger.Controllers.Admin.Basics
{
    /// <summary>
    /// 产品分类
    /// </summary>

    [RemoteService(Name = "Category")]
    [Area("Basics")]
    [ControllerName("Category")]
    [Route("api/basic/category")]
    [ApiExplorerSettings(GroupName = "admin-basic")]
    public class CategoryController : TigerController, ICategoryAppService
    {
        protected readonly ICategoryAppService _categoryAppService;

        public CategoryController(ICategoryAppService categoryAppService)
        {
            _categoryAppService = categoryAppService;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<CategoryDto> CreateAsync(CreateUpdateCategoryDto input)
        {
            return _categoryAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _categoryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取单条
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<CategoryDto> GetAsync(Guid id)
        {
            return _categoryAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("all")]
        public Task<PagedResultDto<CategoryDto>> GetListAsync(GetCategoryListDto input)
        {
            return _categoryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<CategoryDto> UpdateAsync(Guid id, CreateUpdateCategoryDto input)
        {
            return _categoryAppService.UpdateAsync(id, input);
        }


        /// <summary>
        /// 获取分类列表树
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [Route("all")]
        [HttpGet]
        public Task<List<CategoryDto>> GetAllListAsync(GetCategoryListDto input)
        {
            return _categoryAppService.GetAllListAsync(input);
        }

        /// <summary>
        /// 获取分类明细树
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("details/{id}")]
        [HttpGet]
        public Task<CategoryDto> GetDetailsAsync(Guid id)
        {
            return _categoryAppService.GetDetailsAsync(id);
        }

        /// <summary>
        /// 根据父级id查找子分类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [Route("children/{parentId}")]
        [HttpGet]
        public Task<List<CategoryDto>> GetChildrenAsync(Guid parentId)
        {
            return _categoryAppService.GetChildrenAsync(parentId);
        }
    }
}
