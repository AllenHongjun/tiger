
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Books;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.ApiResources;

namespace Tiger.Volo.Abp.IdentityServer.ApiResources
{
    [RemoteService(true)]
    [ApiExplorerSettings(GroupName = "admin")]
    // 增加授权
    //[Authorize(BookStorePermissions.Books.Default)]
    public class ApiResourceAppService :
        CrudAppService<
            ApiResource, //The Book entity
            ApiResourceDto, //Used to show books
            Guid, //Primary key of the book entity
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateApiResourceDto>, IApiResourceAppService
    {
        public ApiResourceAppService(IRepository<ApiResource, Guid> repository) : base(repository)
        {
        }
    }
}
