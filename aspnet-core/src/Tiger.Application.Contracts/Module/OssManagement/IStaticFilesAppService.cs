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
    public interface IStaticFilesAppService : IApplicationService
    {
        Task<IFormFile> GetAsync(GetStaticFileInput input);
    }
}
