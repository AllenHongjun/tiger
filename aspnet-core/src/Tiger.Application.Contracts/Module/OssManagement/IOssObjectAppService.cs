using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.OssManagement
{
    public interface IOssObjectAppService : IApplicationService
    {
        Task<OssObjectDto> CreateAsync(CreateOssObjectInput input);

        Task<OssObjectDto> GetAsync(GetOssObjectInput input);

        Task<Stream> GetContentAsync(GetOssObjectInput input);

        Task DeleteAsync(GetOssObjectInput input);

        Task BulkDeleteAsync(BulkDeleteOssObjectInput input);
    }
}
