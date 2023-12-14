using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.Exams;
using Tiger.Module.Exams.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.ExamModifyTimes
{
    /// <summary>
    /// 考试时间调整
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/exam-modify-times")]
    public class ExamModifyTimeController : AbpController, IExamModifyTimeAppService
    {
        public ExamModifyTimeController(IExamModifyTimeAppService ExamModifyTimeAppService)
        {
            this.ExamModifyTimeAppService=ExamModifyTimeAppService;
        }

        protected IExamModifyTimeAppService ExamModifyTimeAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ExamModifyTimeDto> CreateAsync(CreateUpdateExamModifyTimeDto input)
        {
            return ExamModifyTimeAppService.CreateAsync(input);
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
            return ExamModifyTimeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ExamModifyTimeDto> GetAsync(Guid id)
        {
            return ExamModifyTimeAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<ExamModifyTimeDto>> GetListAsync(ExamModifyTimeGetListInput input)
        {
            return ExamModifyTimeAppService.GetListAsync(input);
        }
        

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ExamModifyTimeDto> UpdateAsync(Guid id, CreateUpdateExamModifyTimeDto input)
        {
            return ExamModifyTimeAppService.UpdateAsync(id, input);
        }
    }
}
