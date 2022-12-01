using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.ApiResources;
using Tiger.Volo.Abp.IdentityServer.Clients;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.Clients;

namespace Tiger.Volo.Abp.IdentityServer
{
    [RemoteService(true)]
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
        public ClientAppService(IRepository<Client, Guid> repository) : base(repository)
        {
        }
    }
}
