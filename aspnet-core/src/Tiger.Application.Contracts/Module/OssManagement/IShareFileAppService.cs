using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Dtos;
using Volo.Abp.Application.Services;

namespace Tiger.Module.OssManagement
{
    public interface IShareFileAppService : IApplicationService
    {
        Task<GetFileShareDto> GetAsync(string url);
    }
}
