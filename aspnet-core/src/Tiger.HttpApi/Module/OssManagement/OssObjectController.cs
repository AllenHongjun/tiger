using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Permissions;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Auditing;
using Volo.Abp;
using System.IO;
using Volo.Abp.Content;

namespace Tiger.Module.OssManagement
{
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.OssManagementGroupName)]
    [RemoteService(Name = OssManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("oss-management")]
    [Route("api/oss-management/objects")]
    public class OssObjectController : AbpController, IOssObjectAppService
    {
        protected IFileUploader FileUploader { get; }
        protected IOssObjectAppService OssObjectAppService { get; }

        public OssObjectController(
            IFileUploader fileUploader,
            IOssObjectAppService ossObjectAppService)
        {
            FileUploader = fileUploader;
            OssObjectAppService = ossObjectAppService;
        }

        /// <summary>
        /// 创建对象（文件上传）
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async virtual Task<OssObjectDto> CreateAsync([FromForm] CreateOssObjectInput input)
        {
            return await OssObjectAppService.CreateAsync(input);
        }

        /// <summary>
        /// 对象分片上传
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("upload")]
        [DisableAuditing]
        [Authorize(AbpOssManagementPermissions.OssObject.Create)]
        public async virtual Task UploadAsync([FromForm] UploadFileChunkInput input)
        {
            await FileUploader.UploadAsync(input);
        }

        [HttpPost]
        [Route("bulk-delete")]
        public async virtual Task BulkDeleteAsync(BulkDeleteOssObjectInput input)
        {
            await OssObjectAppService.BulkDeleteAsync(input);
        }

        [HttpDelete]
        public async virtual Task DeleteAsync(GetOssObjectInput input)
        {
            await OssObjectAppService.DeleteAsync(input);
        }

        [HttpGet]
        public async virtual Task<OssObjectDto> GetAsync(GetOssObjectInput input)
        {
            return await OssObjectAppService.GetAsync(input);
        }

        [HttpGet]
        [Route("download")]
        public async virtual Task<IFormFile> GetContentAsync(GetOssObjectInput input)
        {
            return await OssObjectAppService.GetContentAsync(input);
        }
    }
}
