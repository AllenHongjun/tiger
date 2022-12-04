using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Volo.Abp.IdentityServer.Devices;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Volo.Abp.IdentityServer
{
    /// <summary>
    /// 设备流代码
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.IdentityServerGroupName)]
    [RemoteService(Name = IdentityServerClientConsts.RemoteServiceName)]
    [Area("identity-server")]
    [ControllerName("DeviceFlowCode")]
    [Route("/v1/identity-server/device-flow-code")]
    public class DeviceFlowCodeController : AbpController, IDeviceFlowCodeAppService
    {
        [HttpPost]
        public Task<DeviceFlowCodeDto> CreateAsync(CreateUpdateDeviceFlowCodeDto input)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route("{id}")]
        public Task<DeviceFlowCodeDto> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Task<PagedResultDto<DeviceFlowCodeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<DeviceFlowCodeDto> UpdateAsync(Guid id, CreateUpdateDeviceFlowCodeDto input)
        {
            throw new NotImplementedException();
        }
    }
}
