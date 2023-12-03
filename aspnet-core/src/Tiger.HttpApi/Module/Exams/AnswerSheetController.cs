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

namespace Tiger.Module.AnswerSheets
{
    /// <summary>
    /// 答卷
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/answer-sheets")]
    public class AnswerSheetController : AbpController, IAnswerSheetAppService
    {
        public AnswerSheetController(IAnswerSheetAppService answerSheetAppService)
        {
            AnswerSheetAppService=answerSheetAppService;
        }

        protected IAnswerSheetAppService AnswerSheetAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AnswerSheetDto> CreateAsync(CreateUpdateAnswerSheetDto input)
        {
            return await AnswerSheetAppService.CreateAsync(input);
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
            await AnswerSheetAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public async Task<AnswerSheetDto> GetAsync(Guid id)
        {
            return await AnswerSheetAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<AnswerSheetDto>> GetListAsync(AnswerSheetGetListInput input)
        {
            return await AnswerSheetAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<AnswerSheetDto> UpdateAsync(Guid id, CreateUpdateAnswerSheetDto input)
        {
            return await AnswerSheetAppService.UpdateAsync(id, input);
        }


        /// <summary>
        /// 获取考生成绩面板数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{examId}/exam-score-panel-data")]
        public ExamScorePanelDto GetExamScorePanelData(Guid examId)
        {
            return AnswerSheetAppService.GetExamScorePanelData(examId);
        }
    }
}
