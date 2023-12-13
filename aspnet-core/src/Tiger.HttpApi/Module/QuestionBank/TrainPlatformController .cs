using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Tiger.Module.QuestionBank;
using Tiger.Module.QuestionBank.Dtos;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.TrainPlatformBank
{
    /// <summary>
    /// 题目
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.ExamGroupName)]
    [RemoteService(Name = QuestionBankRemoteServiceConsts.RemoteServiceName)]
    [Area(QuestionBankRemoteServiceConsts.ModuleName)]
    [Route($"api/{QuestionBankRemoteServiceConsts.ModuleName}/train-platforms")]
    public class TrainPlatformController : AbpController, ITrainPlatformAppService
    {
        #region 构造函数和字段
        public TrainPlatformController(ITrainPlatformAppService questionAppService)
        {
            TrainPlatformAppService=questionAppService;
        }

        protected ITrainPlatformAppService TrainPlatformAppService { get; }
        #endregion

        #region CRUD
        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<TrainPlatformDto> CreateAsync(CreateUpdateTrainPlatformDto input)
        {
            return TrainPlatformAppService.CreateAsync(input);
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
            return TrainPlatformAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public Task<TrainPlatformDto> GetAsync(Guid id)
        {
            return TrainPlatformAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<TrainPlatformDto>> GetListAsync(TrainPlatformGetListInput input)
        {
            return TrainPlatformAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public Task<TrainPlatformDto> UpdateAsync(Guid id, CreateUpdateTrainPlatformDto input)
        {
            return TrainPlatformAppService.UpdateAsync(id, input);
        }
        
        #endregion
        

        
    }
}
