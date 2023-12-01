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
    /// 试卷大题
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/test-paper-sections")]
    public class TestPaperSectionController : AbpController, ITestPaperSectionAppService
    {
        #region 构造函数和字段
        public TestPaperSectionController(ITestPaperSectionAppService testPaperSectionAppService)
        {
            TestPaperSectionAppService=testPaperSectionAppService;
        }

        protected ITestPaperSectionAppService TestPaperSectionAppService { get; }
        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TestPaperSectionDto> CreateAsync(CreateUpdateTestPaperSectionDto input)
        {
            return TestPaperSectionAppService.CreateAsync(input);
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
            await TestPaperSectionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TestPaperSectionDto> GetAsync(Guid id)
        {
            return TestPaperSectionAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<TestPaperSectionDto>> GetListAsync(TestPaperSectionGetListInput input)
        {
            return TestPaperSectionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TestPaperSectionDto> UpdateAsync(Guid id, CreateUpdateTestPaperSectionDto input)
        {
            return TestPaperSectionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 查询所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public ListResultDto<TestPaperSectionDto> GetAll(TestPaperSectionGetListInput input)
        {
            return TestPaperSectionAppService.GetAll(input);
        }

        /// <summary>
        /// 下移大题
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        [Route("move-down/{id}")]
        public async Task MoveAsync(Guid id, string type = "down")
        {
            await TestPaperSectionAppService.MoveAsync(id, type);
        }

        /// <summary>
        /// 更新大题描述
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}/description")]

        public async Task<TestPaperSectionDto> UpdateDescriptionAsync(Guid id, UpdateTestPaperSectionDescriptionDto input)
        {
            return await TestPaperSectionAppService.UpdateDescriptionAsync(id, input);
        } 
        #endregion
    }
}
