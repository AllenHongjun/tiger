﻿using Microsoft.AspNetCore.Mvc;
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
    /// 试卷题目
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = ExamRemoteServiceConsts.RemoteServiceName)]
    [Area(ExamRemoteServiceConsts.ModuleName)]
    [Route($"api/{ExamRemoteServiceConsts.ModuleName}/test-paper-questions")]
    public class TestPaperQuestionController : AbpController, ITestPaperQuestionAppService
    {
        #region 构造函数和字段
        public TestPaperQuestionController(ITestPaperQuestionAppService testPaperQuestionAppService)
        {
            TestPaperQuestionAppService=testPaperQuestionAppService;
        }

        protected ITestPaperQuestionAppService TestPaperQuestionAppService { get; }
        #endregion



        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<TestPaperQuestionDto> CreateAsync(CreateUpdateTestPaperQuestionDto input)
        {
            return await TestPaperQuestionAppService.CreateAsync(input);
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
            await TestPaperQuestionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("id")]
        public async Task<TestPaperQuestionDto> GetAsync(Guid id)
        {
            return await TestPaperQuestionAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<TestPaperQuestionDto>> GetListAsync(TestPaperQuestionGetListInput input)
        {
            return await TestPaperQuestionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<TestPaperQuestionDto> UpdateAsync(Guid id, CreateUpdateTestPaperQuestionDto input)
        {
            return await TestPaperQuestionAppService.UpdateAsync(id, input);
        } 
        #endregion

        /// <summary>
        /// 手动确认选题
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("comfirm-select")]
        public async Task ComfirmSelect(TestPaperQuestionComfirmSelectDto input)
        {
             await TestPaperQuestionAppService.ComfirmSelect(input);
        }

        /// <summary>
        /// 根据大题id查询试题
        /// </summary>
        /// <param name="testPaperSectionId"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("all/{testPaperSectionId}")]
        public async Task<ListResultDto<TestPaperQuestionDto>> GetAllAsync(Guid testPaperSectionId)
        {
            return await TestPaperQuestionAppService.GetAllAsync(testPaperSectionId);
        }
    }
}
