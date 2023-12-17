using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.Exams;
using Tiger.Module.Exams.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.Examinees
{
    /// <summary>
    /// 考试关联人员管理(考生管理)
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/examinees")]
    public class ExamineeController : AbpController, IExamineeAppService
    {
        public ExamineeController(IExamineeAppService ExamineeAppService)
        {
            this.ExamineeAppService=ExamineeAppService;
        }

        protected IExamineeAppService ExamineeAppService { get; }

        

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ExamineeDto> CreateAsync(CreateUpdateExamineeDto input)
        {
            return ExamineeAppService.CreateAsync(input);
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
            return ExamineeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ExamineeDto> GetAsync(Guid id)
        {
            return ExamineeAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<ExamineeDto>> GetListAsync(ExamineeGetListInput input)
        {
            return ExamineeAppService.GetListAsync(input);
        }
        

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ExamineeDto> UpdateAsync(Guid id, CreateUpdateExamineeDto input)
        {
            return ExamineeAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 批量创建考试关联的考生
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("batch-create")]
        public Task BulkCreateAsync(ExamineeBatchInputDto input)
        {
            return ExamineeAppService.BulkCreateAsync(input);
        }
    }
}
