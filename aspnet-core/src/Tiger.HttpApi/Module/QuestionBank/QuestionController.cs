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
    /// 题目
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = QuestionBankRemoteServiceConsts.RemoteServiceName)]
    [Area(QuestionBankRemoteServiceConsts.ModuleName)]
    [Route($"api/{QuestionBankRemoteServiceConsts.ModuleName}/questions")]
    public class QuestionController : AbpController, IQuestionAppService
    {
        public QuestionController(IQuestionAppService questionAppService)
        {
            QuestionAppService=questionAppService;
        }

        protected IQuestionAppService QuestionAppService { get; }

        [HttpPost]
        public Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
        {
            return QuestionAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return QuestionAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("id")]
        public Task<QuestionDto> GetAsync(Guid id)
        {
            return QuestionAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<QuestionDto>> GetListAsync(QuestionGetListInput input)
        {
            return QuestionAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<QuestionDto> UpdateAsync(Guid id, CreateUpdateQuestionDto input)
        {
            return QuestionAppService.UpdateAsync(id, input);
        }
    }
}
