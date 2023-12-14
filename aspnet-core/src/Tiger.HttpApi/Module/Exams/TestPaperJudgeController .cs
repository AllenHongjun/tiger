using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.Exams.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.Exams
{
    /// <summary>
    /// 试卷评委管理
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/test-paper-judges")]
    public class TestPaperJudgeController : AbpController, ITestPaperJudgeAppService
    {
        #region 构造函数和字段
        public TestPaperJudgeController(ITestPaperJudgeAppService TestPaperJudgeAppService)
        {
            TestPaperJudgeAppService=TestPaperJudgeAppService;
        }

        protected ITestPaperJudgeAppService TestPaperJudgeAppService { get; }
        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TestPaperJudgeDto> CreateAsync(CreateUpdateTestPaperJudgeDto input)
        {
            return TestPaperJudgeAppService.CreateAsync(input);
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
            await TestPaperJudgeAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TestPaperJudgeDto> GetAsync(Guid id)
        {
            return TestPaperJudgeAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<TestPaperJudgeDto>> GetListAsync(TestPaperJudgeGetListInput input)
        {
            return TestPaperJudgeAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TestPaperJudgeDto> UpdateAsync(Guid id, CreateUpdateTestPaperJudgeDto input)
        {
            return TestPaperJudgeAppService.UpdateAsync(id, input);
        }
        
        #endregion
    }
}
