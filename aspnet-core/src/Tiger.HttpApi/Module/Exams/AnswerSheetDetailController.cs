using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;
using Tiger.Module.Exams.Dtos;
using Tiger.Module.Exams;

namespace Tiger.Module.AnswerSheetDetails
{
    /// <summary>
    /// 答卷详情
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/answersheet-detail")]
    public class AnswerSheetDetailController : AbpController, IAnswerSheetDetailAppService
    {
        public AnswerSheetDetailController(IAnswerSheetDetailAppService answerSheetDetailAppService)
        {
            AnswerSheetDetailAppService=answerSheetDetailAppService;
        }

        protected IAnswerSheetDetailAppService AnswerSheetDetailAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AnswerSheetDetailDto> CreateAsync(CreateUpdateAnswerSheetDetailDto input)
        {
            return await AnswerSheetDetailAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await AnswerSheetDetailAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public async Task<AnswerSheetDetailDto> GetAsync(Guid id)
        {
            return await AnswerSheetDetailAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<AnswerSheetDetailDto>> GetListAsync(AnswerSheetDetailGetListInput input)
        {
            return await AnswerSheetDetailAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<AnswerSheetDetailDto> UpdateAsync(Guid id, CreateUpdateAnswerSheetDetailDto input)
        {
            return await AnswerSheetDetailAppService.UpdateAsync(id, input);
        }
    }
}
