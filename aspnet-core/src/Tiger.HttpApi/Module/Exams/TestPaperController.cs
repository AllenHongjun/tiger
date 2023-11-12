using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.Exams;
using Tiger.Module.Exams.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.TestPapers
{
    /// <summary>
    /// 试卷管理
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/test-papers")]
    public class TestPaperController : AbpController, ITestPaperAppService
    {
        public TestPaperController(ITestPaperAppService testPaperAppService)
        {
            TestPaperAppService=testPaperAppService;
        }

        protected ITestPaperAppService TestPaperAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TestPaperDto> CreateAsync(CreateUpdateTestPaperDto input)
        {
            return TestPaperAppService.CreateAsync(input);
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
            return TestPaperAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TestPaperDto> GetAsync(Guid id)
        {
            return TestPaperAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<TestPaperDto>> GetListAsync(TestPaperGetListInput input)
        {
            return TestPaperAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TestPaperDto> UpdateAsync(Guid id, CreateUpdateTestPaperDto input)
        {
            return TestPaperAppService.UpdateAsync(id, input);
        }
    }
}
