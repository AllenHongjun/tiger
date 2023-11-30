using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.QuestionBank
{
    /// <summary>
    /// 题目
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = QuestionBankRemoteServiceConsts.RemoteServiceName)]
    [Area(QuestionBankRemoteServiceConsts.ModuleName)]
    [Route($"api/{QuestionBankRemoteServiceConsts.ModuleName}/questions")]
    public class QuestionController : AbpController, IQuestionAppService
    {
        #region 构造函数和字段
        public QuestionController(IQuestionAppService questionAppService)
        {
            QuestionAppService=questionAppService;
        }

        protected IQuestionAppService QuestionAppService { get; }
        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<QuestionDto> CreateAsync(CreateUpdateQuestionDto input)
        {
            return QuestionAppService.CreateAsync(input);
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
            return QuestionAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<QuestionDto> GetAsync(Guid id)
        {
            return QuestionAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<QuestionDto>> GetListAsync(QuestionGetListInput input)
        {
            return QuestionAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<QuestionDto> UpdateAsync(Guid id, CreateUpdateQuestionDto input)
        {
            return QuestionAppService.UpdateAsync(id, input);
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("batch-delete")]
        public async Task BulkDeleteAsync(QuestionBatchInput input)
        {
            await QuestionAppService.BulkDeleteAsync(input);
        }

        /// <summary>
        /// 根据分类id和题目类型获取不同难度题目数量
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("different-degree-question-count")]
        public async Task<DifferentDegreeQuestionCountDto> GetDifferentDegreeQuestionCount(GetDifferentDegreeQuestionCountInput input)
        {
            return await QuestionAppService.GetDifferentDegreeQuestionCount(input);
        }
        #endregion

        #region 导入导出
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("export-to-xlsx")]
        public async Task<IActionResult> ExportToXlsxAsync(QuestionGetListInput input)
        {
            return await QuestionAppService.ExportToXlsxAsync(input);
        }

        /// <summary>
        /// 导入
        /// </summary>
        /// <param name="importexcelfile"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("import-from-xlsx")]
        public async Task ImportFromXlsxAsync(IFormFile importexcelfile)
        {
            await QuestionAppService.ImportFromXlsxAsync(importexcelfile);
        } 
        #endregion

        
    }
}
