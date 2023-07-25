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
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Content;

namespace Tiger.Module.OssManagement
{
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.OssManagementGroupName)]
    [RemoteService(Name = OssManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("oss-management")]
    [Route("api/files/public")]
    [AllowAnonymous]
    public class PublicFilesController : AbpController, IPublicFileAppService
    {
        private readonly IPublicFileAppService _publicFileAppService;

        public PublicFilesController(
            IPublicFileAppService publicFileAppService)
        {
            _publicFileAppService = publicFileAppService;

            LocalizationResource = typeof(AbpOssManagementResource);
        }


        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        public async virtual Task<OssObjectDto> UploadAsync([FromForm] UploadFileInput input)
        {
            return await _publicFileAppService.UploadAsync(input);
        }

        /// <summary>
        /// 上传文件（分片）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload-chuck")]
        public async virtual Task UploadAsync([FromForm] UploadFileChunkInput input)
        {
            await _publicFileAppService.UploadAsync(input);
        }

        [HttpGet]
        [Route("search")]
        public async virtual Task<ListResultDto<OssObjectDto>> GetListAsync(GetFilesInput input)
        {
            return await _publicFileAppService.GetListAsync(input);
        }


        [HttpGet]
        [Route("{Name}")]
        [Route("{Name}/{Process}")]
        [Route("p/{Path}/{Name}")]
        [Route("p/{Path}/{Name}/{Process}")]
        public async virtual Task<IFormFile> GetAsync([FromRoute] GetPublicFileInput input)
        {
            return await _publicFileAppService.GetAsync(input);
        }

        [HttpDelete]
        public async virtual Task DeleteAsync(GetPublicFileInput input)
        {
            await _publicFileAppService.DeleteAsync(input);
        }
    }
}
