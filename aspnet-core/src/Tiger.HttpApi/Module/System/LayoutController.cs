using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Platform;
using Tiger.Module.System.Platform.Layouts;
using Tiger.Module.System.Platform.Layouts.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{
    /// <summary>
    /// 布局
    /// </summary>
    [RemoteService(Name = PlatformRemoteServiceConsts.RemoteServiceName)]
    [Area("platform")]
    [Route("api/platform/layouts")]
    
    public class LayoutController:AbpController, ILayoutAppService
    {
        public LayoutController(ILayoutAppService layoutAppService)
        {
            LayoutAppService=layoutAppService;
        }

        protected ILayoutAppService LayoutAppService { get; }

        /// <summary>
        /// 创建布局
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<LayoutDto> CreateAsync(LayoutCreateDto input)
        {   
            return await LayoutAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除布局
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            return LayoutAppService.DeleteAsync(id);
        }

        /// <summary>
        /// 获取所有布局
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all")]
        public Task<ListResultDto<LayoutDto>> GetAllListAsync()
        {
            return LayoutAppService.GetAllListAsync();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<LayoutDto> GetAsync(Guid id)
        {
            return await LayoutAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取布局
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<LayoutDto>> GetListAsync(GetLayoutListInput input)
        {
            return await LayoutAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新布局
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<LayoutDto> UpdateAsync(Guid id, LayoutUpdateDto input)
        {
            return await LayoutAppService.UpdateAsync(id, input);
        }
    }
}
