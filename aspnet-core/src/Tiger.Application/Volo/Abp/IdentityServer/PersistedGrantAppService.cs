using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.Grants;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.Grants;

namespace Tiger.Volo.Abp.IdentityServer
{
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(BookStorePermissions.Books.Default)]
    public class PersistedGrantAppService :
        CrudAppService<
            PersistedGrant,
            PersistedGrantDto,
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdatePersistedGrantDto>, IPersistedGrantAppService
    {
        public PersistedGrantAppService(IRepository<PersistedGrant, Guid> repository) : base(repository)
        {
        }
    }
}
