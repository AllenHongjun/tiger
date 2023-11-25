using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.Exams.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 抽题策略
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/test-paper-strategies")]
    public class TestPaperStrategyController : AbpController, ITestPaperStrategyAppService
    {
        public TestPaperStrategyController(ITestPaperStrategyAppService testPaperStrategyAppService)
        {
            TestPaperStrategyAppService=testPaperStrategyAppService;
        }

        protected ITestPaperStrategyAppService TestPaperStrategyAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TestPaperStrategyDto> CreateAsync(CreateUpdateTestPaperStrategyDto input)
        {
            return await TestPaperStrategyAppService.CreateAsync(input);
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
            await TestPaperStrategyAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public async Task<TestPaperStrategyDto> GetAsync(Guid id)
        {
            return await TestPaperStrategyAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<TestPaperStrategyDto>> GetListAsync(TestPaperStrategyGetListInput input)
        {
            return await TestPaperStrategyAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<TestPaperStrategyDto> UpdateAsync(Guid id, CreateUpdateTestPaperStrategyDto input)
        {
            return await TestPaperStrategyAppService.UpdateAsync(id, input);
        }
    }
}
