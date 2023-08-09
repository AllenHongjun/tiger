using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.ApiResources.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.IdentityServer
{

    /// <summary>
    /// Api资源
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("ApiResource")]
    [Route("api/identity-server/api-resources")]
    public class ApiResourceController : AbpController, IApiResourceAppService
    {
        public ApiResourceController(IApiResourceAppService apiResourceAppService)
        {
            ApiResourceAppService=apiResourceAppService;
        }

        protected IApiResourceAppService ApiResourceAppService { get; }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ApiResourceDto> CreateAsync(ApiResourceCreateDto input)
        {
            return await ApiResourceAppService.CreateAsync(input);
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
            await ApiResourceAppService.DeleteAsync(id);
            return;
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}")]
        public async Task<ApiResourceDto> GetAsync(Guid id)
        {
            return await ApiResourceAppService.GetAsync(id);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<ApiResourceDto>> GetListAsync(ApiResourceGetByPagedInputDto input)
        {
            return await ApiResourceAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<ApiResourceDto> UpdateAsync(Guid id, ApiResourceUpdateDto input)
        {
            return await ApiResourceAppService.UpdateAsync(id, input);
        }
    }
}
