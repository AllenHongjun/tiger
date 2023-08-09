using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Volo.Abp.IdentityServer.Clients;
using Tiger.Volo.Abp.IdentityServer.Clients.Dto;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.AuditLogging;
using Volo.Abp.Identity;

namespace Tiger.Volo.Abp.IdentityServer
{   
    /// <summary>
    /// IdentityServer4客户端
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("Client")]
    [Route("api/identity-server/clients")]
    public class ClientController : AbpController, IClientAppService
    {
        public ClientController(IClientAppService clientAppService)
        {
            ClientAppService=clientAppService;
        }

        protected IClientAppService ClientAppService { get; }
        


        /// <summary>
        /// 克隆客户端
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("clone/{id}")]
        public async Task<ClientDto> CloneAsync(Guid id, ClientCloneDto input)
        {
            return await ClientAppService.CloneAsync(id, input);
        }

        /// <summary>
        /// 新增客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public async Task<ClientDto> CreateAsync(ClientCreateDto input)
        {
            return await ClientAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await ClientAppService.DeleteAsync(id);
            return;
        }

        /// <summary>
        /// 获取所有跨域地址
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-all-distinct-allowed-cors-origins")]
        public async Task<ListResultDto<string>> GetAllDistinctAllowedCorsOriginsAsync()
        {
            return await ClientAppService.GetAllDistinctAllowedCorsOriginsAsync();
        }

        /// <summary>
        /// 获取可用的Api资源列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-assignable-api-resource")]
        public async Task<ListResultDto<string>> GetAssignableApiResourcesAsync()
        {
            return await ClientAppService.GetAssignableApiResourcesAsync();
        }

        /// <summary>
        /// 获取可用的身份资源
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-assignable-identity-resource")]
        public async Task<ListResultDto<string>> GetAssignableIdentityResourcesAsync()
        {
            return await ClientAppService.GetAssignableIdentityResourcesAsync();
        }


        /// <summary>
        /// 获取客户端详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{id}")]
        public async Task<ClientDto> GetAsync(Guid id)
        {
            return await ClientAppService.GetAsync(id);
        }

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public async Task<PagedResultDto<ClientDto>> GetListAsync(ClientGetByPagedDto input)
        {
            return await ClientAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新客户端
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPut]
        [Route("{id}")]
        public async Task<ClientDto> UpdateAsync(Guid id, ClientUpdateDto input)
        {
            return await ClientAppService.UpdateAsync(id, input);
        }
    }
}
