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
        public QuestionCategoryController(IQuestionCategoryAppService questionCategoryAppService)
        {
            QuestionCategoryAppService=questionCategoryAppService;
        }

        protected IQuestionCategoryAppService QuestionCategoryAppService { get; }

        [HttpPost]
        public Task<QuestionCategoryDto> CreateAsync(CreateUpdateQuestionCategoryDto input)
        {
            return QuestionCategoryAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return QuestionCategoryAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("id")]
        public Task<QuestionCategoryDto> GetAsync(Guid id)
        {
            return QuestionCategoryAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<QuestionCategoryDto>> GetListAsync(QuestionCategoryGetListInput input)
        {
            return QuestionCategoryAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<QuestionCategoryDto> UpdateAsync(Guid id, CreateUpdateQuestionCategoryDto input)
        {
            return QuestionCategoryAppService.UpdateAsync(id, input);
        }
    }
}
