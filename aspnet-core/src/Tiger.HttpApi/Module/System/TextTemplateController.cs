using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Tiger.Module.System.TextTemplate;
using Tiger.Module.System.TextTemplate.Dtos;
using Tiger.Module.System.TextTemplate.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{   
    /// <summary>
    /// 文本模板
    /// </summary>
    [Controller]
    [Authorize(AbpTextTemplatePermissions.TextTemplate.Default)]
    [RemoteService(Name = AbpTextTemplatieRemoteServiceConsts.RemoteServiceName)]
    [Area(AbpTextTemplatieRemoteServiceConsts.ModuleName)]
    [Route("api/text-template/templates")]
    public class TextTemplateController : AbpController, ITextTemplateAppService
    {
        public TextTemplateController(ITextTemplateAppService textTemplateAppService)
        {
            TextTemplateAppService=textTemplateAppService;
        }

        protected ITextTemplateAppService TextTemplateAppService { get; }

        /// <summary>
        /// 获取文本模板定义
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{name}")]
        public Task<TextTemplateDefinitionDto> GetAsync(string name)
        {
            return TextTemplateAppService.GetAsync(name);
        }

        /// <summary>
        /// 获取文本模板内容
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [Route("content/{Name}")]
        [Route("content/{Culture}/{Name}")]
        public Task<TextTemplateContentDto> GetContentAsync(TextTemplateContentGetInput input)
        {
            return TextTemplateAppService.GetContentAsync(input);
        }

        /// <summary>
        /// 获取文本模板分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<PagedResultDto<TextTemplateDefinitionDto>> GetListAsync(TextTemplateGetListInput input)
        {
            return TextTemplateAppService.GetListAsync(input);
        }


        /// <summary>
        /// 重置默认值
        /// </summary>
        /// <param name="restoreInput"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("restore-to-default")]
        [Authorize(AbpTextTemplatePermissions.TextTemplate.Delete)]
        public Task RestoreToDefaultAsync(TextTemplateRestoreInput restoreInput)
        {
            return TextTemplateAppService.RestoreToDefaultAsync(restoreInput);
        }

        /// <summary>
        /// 更新文本模板定义
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(AbpTextTemplatePermissions.TextTemplate.Update)]
        public Task<TextTemplateDefinitionDto> UpdateAsync(TextTemplateUpdateInput input)
        {
            return TextTemplateAppService.UpdateAsync(input);
        }
    }
}
