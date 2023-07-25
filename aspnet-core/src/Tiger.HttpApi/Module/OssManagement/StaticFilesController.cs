using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.OssManagement.Dtos;
using Tiger.Module.OssManagement.Localization;
using Tiger.Module.OssManagement.Permissions;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp;
using System.IO;
using Volo.Abp.Content;

namespace Tiger.Module.OssManagement
{
    [ApiExplorerSettings(GroupName = ApiExplorerConsts.OssManagementGroupName)]
    [RemoteService(Name = OssManagementRemoteServiceConsts.RemoteServiceName)]
    [Area("oss-management")]
    [Route("api/files/static")]
    public class StaticFilesController : AbpController, IStaticFilesAppService
    {
        private readonly IOssObjectAppService _ossObjectAppService;
        private readonly IStaticFilesAppService _staticFilesAppServic;

        public StaticFilesController(
            IOssObjectAppService ossObjectAppService,
            IStaticFilesAppService staticFilesAppServic)
        {
            _ossObjectAppService = ossObjectAppService;
            _staticFilesAppServic = staticFilesAppServic;

            LocalizationResource = typeof(AbpOssManagementResource);
        }

        [HttpPost]
        [Authorize(AbpOssManagementPermissions.OssObject.Create)]
        public async virtual Task<OssObjectDto> UploadAsync([FromForm] CreateOssObjectInput input)
        {
            return await _ossObjectAppService.CreateAsync(input);
        }

        [HttpGet]
        [Route("{Bucket}/{Name}")]
        [Route("{Bucket}/{Name}/{Process}")]
        [Route("{Bucket}/p/{Path}/{Name}")]
        [Route("{Bucket}/p/{Path}/{Name}/{Process}")]
        public async virtual Task<IFormFile> GetAsync([FromRoute] GetStaticFileInput input)
        {
            return await _staticFilesAppServic.GetAsync(input);
        }
    }
}
