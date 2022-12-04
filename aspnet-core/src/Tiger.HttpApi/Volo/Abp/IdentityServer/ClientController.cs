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
    /// IdentityServer客户端
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("Client")]
    [Route("/v1/identity-server/client")]
    public class ClientController : AbpController, IClientAppService
    {   
        /// <summary>
        /// 克隆客户端
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        [Route("clone/{id}")]
        public Task<ClientDto> CloneAsync(Guid id, ClientCloneDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 新增客户端
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpPost]
        public Task<ClientDto> CreateAsync(CreateUpdateClientDto input)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 删除客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取所有跨域地址
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-all-distinct-allowed-cors-origins")]
        public Task<ListResultDto<string>> GetAllDistinctAllowedCorsOriginsAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取可用的Api资源列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-assignable-api-resource")]
        public Task<ListResultDto<string>> GetAssignableApiResourceAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取可用的身份资源
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("get-assignable-identity-resource")]
        public Task<ListResultDto<string>> GetAssignableIdentityResourceAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取客户端详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("{id}")]
        public Task<ClientDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        public Task<PagedResultDto<ClientDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
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
        public Task<ClientDto> UpdateAsync(Guid id, CreateUpdateClientDto input)
        {
            throw new NotImplementedException();
        }
    }
}
