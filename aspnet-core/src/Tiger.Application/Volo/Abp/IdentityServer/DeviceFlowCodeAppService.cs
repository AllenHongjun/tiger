using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using Tiger.Volo.Abp.IdentityServer.Clients;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.IdentityServer.Devices;

namespace Tiger.Volo.Abp.IdentityServer
{
    [RemoteService(false)]
    //[Authorize(BookStorePermissions.Books.Default)]
    public class DeviceFlowCodeAppService :
        CrudAppService<
            DeviceFlowCodes,
            DeviceFlowCodeDto,
            Guid, //Primary key 
            PagedAndSortedResultRequestDto, //Used for paging/sorting
            CreateUpdateDeviceFlowCodeDto>, IDeviceFlowCodeAppService
    {
        public DeviceFlowCodeAppService(IRepository<DeviceFlowCodes, Guid> repository) : base(repository)
        {
        }
    }
}
