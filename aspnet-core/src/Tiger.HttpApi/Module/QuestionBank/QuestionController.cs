using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

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

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
        {
            return QuestionAppService.CreateAsync(input);
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
            return QuestionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<QuestionDto> GetAsync(Guid id)
        {
            return QuestionAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<QuestionDto>> GetListAsync(QuestionGetListInput input)
        {
            return QuestionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<QuestionDto> UpdateAsync(Guid id, CreateUpdateQuestionDto input)
        {
            return QuestionAppService.UpdateAsync(id, input);
        }
    }
}
