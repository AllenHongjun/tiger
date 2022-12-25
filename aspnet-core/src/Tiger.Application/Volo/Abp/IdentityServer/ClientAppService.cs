using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.Clients;
using Tiger.Volo.Abp.IdentityServer.Clients.Dto;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{   
    /// <summary>
    /// 客户端服务
    /// </summary>
    /// <remarks>
    /// 数据格式转换 转化为数据层需要的输入格式 或者输出格式
    /// </remarks>
    [RemoteService(false)]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(BookStorePermissions.Books.Default)]
    public class ClientAppService :
        CrudAppService<
            Client,
            ClientDto,
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateClientDto>, IClientAppService
    {
        protected IClientRepository ClientRepository { get; }

        protected ITigerApiResourceRepository ApiResourceRepository { get; }

        protected ITigerIdentityResourceRepository IdentityResourceRepository { get; }

        public ClientAppService(
            IRepository<Client, Guid> repository,
            ITigerIdentityResourceRepository identityResourceRepository,
            ITigerApiResourceRepository apiResourceRepository,
            IClientRepository clientRepository) : base(repository)
        {
            IdentityResourceRepository=identityResourceRepository;
            ApiResourceRepository=apiResourceRepository;
            ClientRepository=clientRepository;
        }


        /// <summary>
        /// 删除客户端
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task DeleteAsync(Guid id)
        {
            var client = await ClientRepository.GetAsync(id);
            await ClientRepository.DeleteAsync(client);
            await CurrentUnitOfWork.SaveChangesAsync();

        }

        /// <summary>
        /// 获取客户端详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await ClientRepository.GetAsync(id);

            return ObjectMapper.Map<Client, ClientDto>(client);
        }

        /// <summary>
        /// 获取客户端列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<ClientDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var clients = await ClientRepository.GetListAsync(
                 input.Sorting,
                 input.SkipCount,
                 input.MaxResultCount
                 );

            var clientCount = await ClientRepository.GetCountAsync();

            return new PagedResultDto<ClientDto>(clientCount,
                ObjectMapper.Map<List<Client>, List<ClientDto>>(clients));
        }

        /// <summary>
        /// 客户端添加
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="UserFriendlyException"></exception>
        public override async  Task<ClientDto> CreateAsync(CreateUpdateClientDto input)
        {   
            var isExists = await ClientRepository.CheckClientIdExistAsync(input.ClientId);
            if (isExists)
            {
                throw new UserFriendlyException("客户端Id已经存在");
            }

            var client = new Client(GuidGenerator.Create(), input.ClientId)
            {
                ClientName = input.ClientName,
                Description = input.Description,
            };

            //foreach (var grantType in input.AllowOfflineAccess)
            //{

            //}

            client = await ClientRepository.InsertAsync(client);

            await CurrentUnitOfWork.SaveChangesAsync();

            return ObjectMapper.Map<Client, ClientDto>(client);

        }

        public override Task<ClientDto> UpdateAsync(Guid id, CreateUpdateClientDto input)
        {
            throw new NotImplementedException();
        }


        public Task<ClientDto> CloneAsync(Guid id, ClientCloneDto input)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// 获取所有跨域地址
        /// </summary>
        /// <returns></returns>
        public async Task<ListResultDto<string>> GetAllDistinctAllowedCorsOriginsAsync()
        {
            var corsOrigins = await ClientRepository.GetAllDistinctAllowedCorsOriginsAsync();

            return new ListResultDto<string>(corsOrigins);
        }

        /// <summary>
        /// 获取可用的Api资源列表
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ListResultDto<string>> GetAssignableApiResourceAsync()
        {
            var apiResourceNames = await ApiResourceRepository.GetNamesAsync();

            return new ListResultDto<string>(apiResourceNames);
        }

        /// <summary>
        /// 获取可用的身份资源
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<ListResultDto<string>> GetAssignableIdentityResourceAsync()
        {
            var identityResourceName = await IdentityResourceRepository.GetNameAsync();

            return new ListResultDto<string>(identityResourceName);
        }
    }
}
