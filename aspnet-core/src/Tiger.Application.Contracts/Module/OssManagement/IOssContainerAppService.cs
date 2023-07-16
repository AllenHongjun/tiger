using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.OssManagement
{
    public interface IOssContainerAppService : IApplicationService
    {
        Task<OssContainerDto> CreateAsync(string name);

        Task<OssContainerDto> GetAsync(string name);

        Task DeleteAsync(string name);

        Task<OssContainersResultDto> GetListAsync(GetOssContainersInput input);

        Task<OssObjectsResultDto> GetObjectListAsync(GetOssObjectsInput input);
    }
}
