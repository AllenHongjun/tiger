using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Dtos;

namespace Tiger.Module.OssManagement
{
    public interface IPrivateFileAppService : IFileAppService
    {
        Task<FileShareDto> ShareAsync(FileShareInput input);

        Task<ListResultDto<MyFileShareDto>> GetShareListAsync();
    }
}
