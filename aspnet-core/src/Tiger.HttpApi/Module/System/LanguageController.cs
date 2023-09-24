using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tiger.Module.System.Localization;
using Tiger.Module.System.Localization.Dtos;
using Tiger.Module.System.Localization.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Mvc;

namespace Tiger.Module.System
{
    /// <summary>
    /// 语言管理
    /// </summary>
    [Controller]
    [Authorize(LocalizationPermissions.Languages.Default)]
    [RemoteService(Name = "Language")]
    [Area("Localization")]
    [Route("api/localization/languages")]
    public class LanguageController : AbpController, ILanguageAppService
    {
        public LanguageController(ILanguageAppService languageAppService)
        {
            LanguageAppService=languageAppService;
        }

        protected ILanguageAppService LanguageAppService { get; }


        /// <summary>
        /// 添加语言
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(LocalizationPermissions.Languages.Create)]
        public async Task<LanguageDto> CreateAsync(CreateLanguageDto input)
        {
            return await LanguageAppService.CreateAsync(input);
        }

        /// <summary>
        /// 删除语言
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteAsync(Guid id)
        {
            await LanguageAppService.DeleteAsync(id);
            return;
        }

        /// <summary>
        /// 获取语言列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<PagedResultDto<LanguageDto>> GetListAsync(LanguageGetListInput input)
        {
            return await LanguageAppService.GetListAsync(input);
        }

        /// <summary>
        /// 更新语言
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id}")]
        public async Task<LanguageDto> UpdateAsync(Guid id, UpdateLanguageDto input)
        {
            return await LanguageAppService.UpdateAsync(id, input);
        }
    }
}
