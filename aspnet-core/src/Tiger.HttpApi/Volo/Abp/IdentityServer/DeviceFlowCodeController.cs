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
    [Route("api/identity-server/device-flow-codes")]
    public class DeviceFlowCodeController : AbpController, IDeviceFlowCodeAppService
    {
        public DeviceFlowCodeController(IDeviceFlowCodeAppService deviceFlowCodeAppService)
        {
            DeviceFlowCodeAppService=deviceFlowCodeAppService;
        }

        protected IDeviceFlowCodeAppService DeviceFlowCodeAppService { get; }

        [HttpPost]
        public async Task<DeviceFlowCodeDto> CreateAsync(CreateUpdateDeviceFlowCodeDto input)
        {
            return await DeviceFlowCodeAppService.CreateAsync(input);
        }

        [HttpDelete]
        public async Task DeleteAsync(Guid id)
        {
            await DeviceFlowCodeAppService.DeleteAsync(id);
            return;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<DeviceFlowCodeDto> GetAsync(Guid id)
        {
            return await DeviceFlowCodeAppService.GetAsync(id);
        }

        [HttpGet]
        public async Task<PagedResultDto<DeviceFlowCodeDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return await DeviceFlowCodeAppService.GetListAsync(input);
        }

        [HttpPut]
        public async Task<DeviceFlowCodeDto> UpdateAsync(Guid id, CreateUpdateDeviceFlowCodeDto input)
        {
            return await DeviceFlowCodeAppService.UpdateAsync(id, input);
        }
    }
}
