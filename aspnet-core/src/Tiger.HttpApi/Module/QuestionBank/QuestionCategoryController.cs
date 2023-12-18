using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.Teachings.Dtos;
using Tiger.Module.Teachings;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using Tiger.Module.QuestionBank.Dtos;
using Microsoft.AspNetCore.Http;
using Tiger.Volo.Abp.Identity.ClaimTypes.Dto;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目分类
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = QuestionBankRemoteServiceConsts.RemoteServiceName)]
    [Area(QuestionBankRemoteServiceConsts.ModuleName)]
    [Route($"api/{QuestionBankRemoteServiceConsts.ModuleName}/question-categories")]
    public class QuestionCategoryController : AbpController, IQuestionCategoryAppService
    {
        #region 字段和构造函数
        public QuestionCategoryController(IQuestionCategoryAppService questionCategoryAppService)
        {
            QuestionCategoryAppService=questionCategoryAppService;
        }

        protected IQuestionCategoryAppService QuestionCategoryAppService { get; }

        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<QuestionCategoryDto> CreateAsync(CreateUpdateQuestionCategoryDto input)
        {
            return QuestionCategoryAppService.CreateAsync(input);
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
            return QuestionCategoryAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 查询所有题目分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public ListResultDto<QuestionCategoryDto> GetAllList(QuestionCategoryGetListInput input)
        {
            return QuestionCategoryAppService.GetAllList(input);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<QuestionCategoryDto> GetAsync(Guid id)
        {
            return QuestionCategoryAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<QuestionCategoryDto>> GetListAsync(QuestionCategoryGetListInput input)
        {
            return QuestionCategoryAppService.GetListAsync(input);
        }

        /// <summary>
        /// 根据父级id查询分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("by-parentId")]
        public ListResultDto<QuestionCategoryDto> GetListByParentId(QuestionCategoryGetListInput input)
        {
            return QuestionCategoryAppService.GetListByParentId(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<QuestionCategoryDto> UpdateAsync(Guid id, CreateUpdateQuestionCategoryDto input)
        {
            return QuestionCategoryAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 切换启用
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/toggle-enable")]
        public async Task ToggleEnable(Guid id)
        {
            await QuestionCategoryAppService.ToggleEnable(id);
        }

        /// <summary>
        /// 更改序号
        /// </summary>
        /// <param name="id"></param>
        /// <param name="sort"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        [Route("{id}/update-sort/{sort}")]
        public async Task UpdateSort(Guid id, int sort)
        {
            await QuestionCategoryAppService.UpdateSort(id,sort);
        }
        #endregion

        #region xlsx导入导出
        /// <summary>
        /// 从 xlsx 导入
        /// </summary>
        /// <param name="importexcelfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import-from-xlsx")]
        public async Task ImportFromXlsxAsync(IFormFile importexcelfile)
        {
            await QuestionCategoryAppService.ImportFromXlsxAsync(importexcelfile);
        }

        /// <summary>
        /// xlsx 导入模板
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("export-xlxs-template")]
        public async Task<IActionResult> ImportRoleTemplateAsync()
        {
            QuestionCategoryGetListInput input = new QuestionCategoryGetListInput()
            {
                MaxResultCount = 1
            };
            return await QuestionCategoryAppService.ExportToXlsxAsync(input);
        }

        /// <summary>
        /// 导出到 xlsx
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export-to-xlsx")]
        public async Task<IActionResult> ExportToXlsxAsync(QuestionCategoryGetListInput input)
        {
            return await QuestionCategoryAppService.ExportToXlsxAsync(input);
        }

        
        #endregion
    }
}
