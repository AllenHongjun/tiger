using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Tiger.Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.Devices;
using Volo.Abp.IdentityServer.IdentityResources;

namespace Tiger.Volo.Abp.IdentityServer
{
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin")]
    //[Authorize(BookStorePermissions.Books.Default)]
    public class IdentityResourceAppService :
        CrudAppService<
            IdentityResource,
            IdentityResourceDto,
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateIdentityResourceDto>, IIdentityResourceAppService
    {
        public IdentityResourceAppService(IRepository<IdentityResource, Guid> repository) : base(repository)
        {
        }
    }
}
