using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Localization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp;
using System.IO;

namespace Tiger.Module.OssManagement
{
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.OssManagementGroupName)]
    [RemoteService(Name = OssManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("oss-management")]
    [Route("api/files/private")]
    public class PrivateFilesController : AbpController, IPrivateFileAppService
    {
        private readonly IPrivateFileAppService _service;

        public PrivateFilesController(
            IPrivateFileAppService service)
        {
            _service = service;

            LocalizationResource = typeof(AbpOssManagementResource);
        }

        [HttpPost]
        public async virtual Task<OssObjectDto> UploadAsync([FromForm] UploadFileInput input)
        {
            return await _service.UploadAsync(input);
        }

        [HttpPost]
        [Route("upload")]
        public async virtual Task UploadAsync([FromForm] UploadFileChunkInput input)
        {
            await _service.UploadAsync(input);
        }

        [HttpGet]
        [Route("search")]
        public async virtual Task<ListResultDto<OssObjectDto>> GetListAsync(GetFilesInput input)
        {
            return await _service.GetListAsync(input);
        }

        [HttpGet]
        [Route("{Name}")]
        [Route("{Name}/{Process}")]
        [Route("p/{Path}/{Name}")]
        [Route("p/{Path}/{Name}/{Process}")]
        public async virtual Task<Stream> GetAsync([FromRoute] GetPublicFileInput input)
        {
            return await _service.GetAsync(input);
        }

        [HttpDelete]
        public async virtual Task DeleteAsync(GetPublicFileInput input)
        {
            await _service.DeleteAsync(input);
        }

        [HttpGet]
        [Route("share")]
        public async virtual Task<ListResultDto<MyFileShareDto>> GetShareListAsync()
        {
            return await _service.GetShareListAsync();
        }

        [HttpPost]
        [Route("share")]
        public async virtual Task<FileShareDto> ShareAsync(FileShareInput input)
        {
            return await _service.ShareAsync(input);
        }
    }
}
