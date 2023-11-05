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
        public TestPaperController(ITestPaperAppService schoolAppService)
        {
            TestPaperAppService=schoolAppService;
        }

        protected ITestPaperAppService TestPaperAppService { get; }

        [HttpPost]
        public Task<TestPaperDto> CreateAsync(CreateUpdateTestPaperDto input)
        {
            return TestPaperAppService.CreateAsync(input);
        }

        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return TestPaperAppService.DeleteAsync(id);
        }

        [HttpGet]
        [Route("id")]
        public Task<TestPaperDto> GetAsync(Guid id)
        {
            return TestPaperAppService.GetAsync(id);
        }

        [HttpGet]
        public Task<PagedResultDto<TestPaperDto>> GetListAsync(TestPaperGetListInput input)
        {
            return TestPaperAppService.GetListAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public Task<TestPaperDto> UpdateAsync(Guid id, CreateUpdateTestPaperDto input)
        {
            return TestPaperAppService.UpdateAsync(id, input);
        }
    }
}
