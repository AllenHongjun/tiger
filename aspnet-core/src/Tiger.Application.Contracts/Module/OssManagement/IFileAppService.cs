using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.OssManagement
{
    public interface IFileAppService : IApplicationService
    {
        Task<OssObjectDto> UploadAsync(UploadFileInput input);

        Task<Stream> GetAsync(GetPublicFileInput input);

        Task<ListResultDto<OssObjectDto>> GetListAsync(GetFilesInput input);

        Task UploadAsync(UploadFileChunkInput input);

        Task DeleteAsync(GetPublicFileInput input);
    }
}
