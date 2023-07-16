using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.OssManagement.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Http;
using Volo.Abp.Localization;
using Volo.Abp;

namespace Tiger.Module.OssManagement
{
    [Area("oss-management")]
    [Route("api/files/share")]
    [RemoteService(false)]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ShareFilesController : AbpController
    {
        private readonly IShareFileAppService _service;

        public ShareFilesController(
           IShareFileAppService service)
        {
            _service = service;

            LocalizationResource = typeof(AbpOssManagementResource);
        }

        [HttpGet]
        [Route("{url}")]
        public async virtual Task<IActionResult> GetAsync(string url)
        {
            var ossObject = await _service.GetAsync(url);

            if (ossObject.Content.IsNullOrEmpty())
            {
                return NotFound();
            }

            return File(ossObject.Content, MimeTypes.GetByExtension(Path.GetExtension(ossObject.Name)));
        }
    }
}
