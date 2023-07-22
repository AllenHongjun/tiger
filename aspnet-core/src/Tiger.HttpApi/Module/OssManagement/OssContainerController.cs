using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp;

namespace Tiger.Module.OssManagement
{   
    /// <summary>
    /// Oss对象存储
    /// </summary>
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.OssManagementGroupName)]
    [RemoteService(Name = OssManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("oss-management")]
    [Route("api/oss-management/containes")]
    public class OssContainerController : AbpController, IOssContainerAppService
    {
        protected IOssContainerAppService OssContainerAppService { get; }

        public OssContainerController(
            IOssContainerAppService ossContainerAppService)
        {
            OssContainerAppService = ossContainerAppService;
        }

        [HttpPost]
        [Route("{name}")]
        public async virtual Task<OssContainerDto> CreateAsync(string name)
        {
            return await OssContainerAppService.CreateAsync(name);
        }

        [HttpDelete]
        [Route("{name}")]
        public async virtual Task DeleteAsync(string name)
        {
            await OssContainerAppService.DeleteAsync(name);
        }

        [HttpGet]
        [Route("{name}")]
        public async virtual Task<OssContainerDto> GetAsync(string name)
        {
            return await OssContainerAppService.GetAsync(name);
        }

        [HttpGet]
        public async virtual Task<OssContainersResultDto> GetListAsync(GetOssContainersInput input)
        {
            return await OssContainerAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("objects")]
        public async virtual Task<OssObjectsResultDto> GetObjectListAsync(GetOssObjectsInput input)
        {
            return await OssContainerAppService.GetObjectListAsync(input);
        }
    }
}
