using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Content;

namespace Tiger.Module.OssManagement
{
    public interface IOssObjectAppService : IApplicationService
    {
        Task<OssObjectDto> CreateAsync(CreateOssObjectInput input);

        Task<OssObjectDto> GetAsync(GetOssObjectInput input);

        Task<IRemoteStreamContent> GetContentAsync(GetOssObjectInput input);

        Task DeleteAsync(GetOssObjectInput input);

        Task BulkDeleteAsync(BulkDeleteOssObjectInput input);
    }
}
